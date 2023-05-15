using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace SboxAnalyzers.Analyzers;

/// <summary>
/// A Roslyn analyzer for checking event method arguments.
/// </summary>
[DiagnosticAnalyzer( LanguageNames.CSharp )]
public class BaseNetworkableAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc/>
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Diagnostics.BaseNetworkables.AllRules;

	/// <inheritdoc/>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSyntaxNodeAction( AnalyzeClassDeclaration, SyntaxKind.ClassDeclaration );
	}

	private static void AnalyzeClassDeclaration( SyntaxNodeAnalysisContext context )
	{
		var classDeclaration = (ClassDeclarationSyntax)context.Node;
		var classSymbol = context.SemanticModel.GetDeclaredSymbol( classDeclaration );
		if ( classSymbol is null )
			return;

		// Check if the class is derived from BaseNetworkable.
		var isBaseNetworkable = false;
		var currentSymbol = classSymbol;
		while ( currentSymbol is not null )
		{
			if ( currentSymbol.Name != Constants.BaseNetworkableType )
			{
				currentSymbol = currentSymbol.BaseType;
				continue;
			}

			isBaseNetworkable = true;
			break;
		}

		if ( !isBaseNetworkable )
			return;

		// Check if the class has a parameterless constructor.
		foreach ( var constructor in classSymbol.InstanceConstructors )
		{
			if ( constructor.Parameters.Length == 0 )
				return;
		}

		var diagnostic = Diagnostic.Create( Diagnostics.BaseNetworkables.NoParameterlessConstructorRule,
			classDeclaration.Identifier.GetLocation() );
		context.ReportDiagnostic( diagnostic );
	}
}
