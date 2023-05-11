using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SboxAnalyzers.Extensions;
using System.Collections.Immutable;
using System.Linq;

namespace SboxAnalyzers;

/// <summary>
/// A Roslyn analyzer for checking networked properties.
/// </summary>
[DiagnosticAnalyzer( LanguageNames.CSharp )]
public class NetPropertyAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc/>
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Diagnostics.NetProperty.AllRules;

	/// <inheritdoc/>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSyntaxNodeAction( AnalyzePropertyDeclaration, SyntaxKind.PropertyDeclaration );
	}

	/// <summary>
	/// Analyzes a <see cref="PropertyDeclarationSyntax"/> to check if it is a correctly implemented networked property.
	/// </summary>
	/// <param name="context">The context relating to the syntax node being analyzed.</param>
	private static void AnalyzePropertyDeclaration( SyntaxNodeAnalysisContext context )
	{
		var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;

		CheckLocalAttribute( context, propertyDeclaration );
		CheckChangeAttribute( context, propertyDeclaration );
		CheckNetAttribute( context, propertyDeclaration );
	}

	/// <summary>
	/// Checks if a local attribute is being used on the property and warns if it is.
	/// </summary>
	/// <param name="context">The context relating to the syntax node being analyzed.</param>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	private static void CheckLocalAttribute( in SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax )
	{
		if ( syntax.TryGetAttribute( "Local", out var localAttribute ) )
			context.ReportDiagnostic( Diagnostic.Create( Diagnostics.NetProperty.LocalRule, localAttribute!.GetLocation() ) );
	}

	/// <summary>
	/// Checks if a change attribute is being used on the property and ensures it is being used correctly.
	/// </summary>
	/// <param name="context">The context relating to the syntax node being analyzed.</param>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	private static void CheckChangeAttribute( in SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax )
	{
		if ( !syntax.TryGetAttribute( "Change", out var changeAttribute ) )
			return;

		// Get the name for the change callback method.
		var changeMethod = "On" + syntax.Identifier.ValueText + "Changed";
		var customChangeMethodExpression = changeAttribute!.ArgumentList?.Arguments.FirstOrDefault()?.Expression;
		if ( customChangeMethodExpression is not null )
		{
			var customChangeMethod = context.SemanticModel.GetConstantValue( customChangeMethodExpression );
			if ( customChangeMethod.HasValue )
			{
				if ( customChangeMethod.Value is string customChangeMethodName )
					changeMethod = customChangeMethodName;
			}
		}

		// Check the containing type for the change callback method.
		if ( syntax.Parent is not TypeDeclarationSyntax containingType )
			return;

		var containsChangeMethod = false;
		foreach ( var method in containingType.Members.OfType<MethodDeclarationSyntax>() )
		{
			if ( method.Identifier.ValueText != changeMethod )
				continue;

			containsChangeMethod = true;
			var parameterList = method.ParameterList;

			// Has no parameters, should have two.
			if ( parameterList is null )
			{
				var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.ChangeParameterCountRule,
					method.Identifier.GetLocation(),
					0 );
				context.ReportDiagnostic( diagnostic );

				break;
			}

			// Has an incorrect amount of parameters, should have two.
			if ( parameterList.Parameters.Count != 2 )
			{
				var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.ChangeParameterCountRule,
					method.Identifier.GetLocation(),
					method.ParameterList.Parameters.Count );
				context.ReportDiagnostic( diagnostic );

				break;
			}

			// Check that the parameter types are correct.
			for ( var i = 0; i < 2; i++ )
			{
				var parameterType = parameterList.Parameters[i].Type;
				if ( parameterType is null )
					continue;

				if ( !parameterType.IsEqual( syntax.Type, context.SemanticModel ) )
				{
					var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.ChangeParameterTypeRule,
						parameterType.GetLocation(),
						parameterType.ToString() );
					context.ReportDiagnostic( diagnostic );
				}
			}

			break;
		}

		// Missing the change callback method entirely.
		if ( !containsChangeMethod )
			context.ReportDiagnostic( Diagnostic.Create( Diagnostics.NetProperty.ChangeMissingRule, changeAttribute.GetLocation(), changeMethod ) );
	}

	/// <summary>
	/// Checks if a net attribute is being used on the property and ensures it is being used correctly.
	/// </summary>
	/// <param name="context">The context relating to the syntax node being analyzed.</param>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	private static void CheckNetAttribute( in SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax )
	{
		if ( !syntax.HasAttribute( "Net" ) )
			return;

		// Check if the property is declared as static.
		if ( syntax.TryGetModifier( SyntaxKind.StaticKeyword, out var token ) )
			context.ReportDiagnostic( Diagnostic.Create( Diagnostics.NetProperty.StaticRule, token.GetLocation() ) );

		// Check if the property is not implemented as a { get; set; } auto-property.
		if ( !syntax.IsAutoProperty() || !syntax.HasGetter() || !syntax.HasSetter() || syntax.HasInitSetter() )
		{
			var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.AutoPropertyRule,
				syntax.AccessorList?.GetLocation() ?? syntax.GetLocation() );
			context.ReportDiagnostic( diagnostic );
		}

		// Check that the property type is networkable.
		if ( !syntax.Type.IsNetworkable( context.SemanticModel ) )
		{
			var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.NetworkableRule,
				syntax.Type.GetLocation(),
				syntax.Type.ToNameString( true, context.SemanticModel ) );
			context.ReportDiagnostic( diagnostic );
		}
	}
}
