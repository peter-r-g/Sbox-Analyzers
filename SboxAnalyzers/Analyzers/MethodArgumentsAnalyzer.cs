using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SboxAnalyzers.Extensions;
using System.Collections.Immutable;
using System.Linq;

namespace SboxAnalyzers.Analyzers;

/// <summary>
/// A Roslyn analyzer for checking event method arguments.
/// </summary>
[DiagnosticAnalyzer( LanguageNames.CSharp )]
public class MethodArgumentsAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc/>
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Diagnostics.MethodArguments.AllRules;

	/// <inheritdoc/>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSyntaxNodeAction( AnalyzeMethodDeclaration, SyntaxKind.MethodDeclaration );
	}

	/// <summary>
	/// Analyzes a <see cref="MethodDeclarationSyntax"/> to check if it is a correctly implemented event listener.
	/// </summary>
	/// <param name="context">The context relating to the syntax node being analyzed.</param>
	private static void AnalyzeMethodDeclaration( SyntaxNodeAnalysisContext context )
	{
		var methodDeclaration = (MethodDeclarationSyntax)context.Node;
		var methodParameters = methodDeclaration.ParameterList?.Parameters;

		// Get all event attributes on the method.
		foreach ( var eventAttribute in methodDeclaration.GetAttributesOfType( Constants.EventAttribute, context.SemanticModel ) )
		{
			var eventTypeSymbol = context.SemanticModel.GetSymbolInfo( eventAttribute ).Symbol!.ContainingType;
			var methodArgumentsAttribute = eventTypeSymbol.GetAttributes()
				.FirstOrDefault( a => a.AttributeClass?.Name == Constants.MethodArgumentsAttribute + "Attribute" );

			if ( methodArgumentsAttribute is null )
			{
				// Warn if no method arguments attribute on event and containing one or more parameters.
				if ( methodParameters is not null && methodParameters.Value.Count > 0 )
				{
					var diagnostic = Diagnostic.Create( Diagnostics.MethodArguments.ListenerMethodArgumentsUndefinedRule,
						methodDeclaration.ParameterList!.GetLocation() );
					context.ReportDiagnostic( diagnostic );
				}

				continue;
			}

			var typeArguments = methodArgumentsAttribute.ConstructorArguments[0].Values;

			if ( methodParameters is null || typeArguments.Length != methodParameters.Value.Count )
			{
				// Error if method parameter count does not match method arguments type count.
				if ( typeArguments.Length != (methodParameters?.Count ?? 0) )
				{
					var diagnostic = Diagnostic.Create( Diagnostics.MethodArguments.ListenerParameterCountMismatchRule,
						methodDeclaration.Identifier.GetLocation(),
						typeArguments.Length );
					context.ReportDiagnostic( diagnostic );
				}

				if ( methodParameters is null )
					continue;
			}

			// Check each parameter for type match.
			for ( var i = 0; i < typeArguments.Length; i++ )
			{
				var argument = typeArguments[i];
				if ( argument.Value is not ITypeSymbol argumentSymbol )
					continue;

				var parameter = methodParameters.Value[i];
				if ( parameter.Type is null )
					continue;

				var parameterSymbol = context.SemanticModel.GetSymbolInfo( parameter ).Symbol;
				if ( SymbolEqualityComparer.Default.Equals( parameterSymbol, argumentSymbol ) )
					continue;

				// Error when method parameter type does not match.
				var diagnostic = Diagnostic.Create( Diagnostics.MethodArguments.ListenerParameterTypeMismatchRule,
					parameter.Type.GetLocation(),
					argumentSymbol.ToNameString( true ) );
				context.ReportDiagnostic( diagnostic );
			}
		}
	}
}
