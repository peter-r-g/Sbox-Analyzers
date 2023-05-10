using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SboxAnalyzers.Extensions;
using System.Collections.Immutable;
using System.Linq;

namespace SboxAnalyzers;

[DiagnosticAnalyzer( LanguageNames.CSharp )]
public class SboxNetPropertyAnalyzer : DiagnosticAnalyzer
{
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Diagnostics.NetProperty.Diagnostics;

	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSyntaxNodeAction( AnalyzePropertyDeclaration, SyntaxKind.PropertyDeclaration );
	}

	private static void AnalyzePropertyDeclaration( SyntaxNodeAnalysisContext context )
	{
		var propertyDeclaration = (PropertyDeclarationSyntax)context.Node;

		CheckLocalAttribute( context, propertyDeclaration );
		CheckChangeAttribute( context, propertyDeclaration );
		CheckNetAttribute( context, propertyDeclaration );
	}

	private static void CheckLocalAttribute( in SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax )
	{
		if ( syntax.TryGetAttribute( "Local", out var localAttribute ) )
			context.ReportDiagnostic( Diagnostic.Create( Diagnostics.NetProperty.LocalRule, localAttribute!.GetLocation() ) );
	}

	private static void CheckChangeAttribute( in SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax )
	{
		if ( !syntax.TryGetAttribute( "Change", out var changeAttribute ) )
			return;

		var changeMethod = "On" + syntax.Identifier.ValueText + "Changed";
		var customChangeMethodExpression = changeAttribute!.ArgumentList?.Arguments.FirstOrDefault()?.Expression;
		if ( customChangeMethodExpression is not null )
		{
			var customChangeMethod = context.SemanticModel.GetConstantValue( customChangeMethodExpression );
			if ( customChangeMethod.HasValue )
				changeMethod = (string)customChangeMethod.Value;
		}

		var containingType = (TypeDeclarationSyntax)syntax.Parent;
		var containsChangeMethod = false;
		foreach ( var method in containingType.Members.OfType<MethodDeclarationSyntax>() )
		{
			if ( method.Identifier.ValueText != changeMethod )
				continue;

			containsChangeMethod = true;
			var parameterList = method.ParameterList;

			if ( parameterList is null )
			{
				var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.ChangeParameterCountRule,
					method.Identifier.GetLocation(),
					0 );
				context.ReportDiagnostic( diagnostic );

				break;
			}

			if ( parameterList.Parameters.Count != 2 )
			{
				var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.ChangeParameterCountRule,
					method.Identifier.GetLocation(),
					method.ParameterList.Parameters.Count );
				context.ReportDiagnostic( diagnostic );

				break;
			}

			for ( var i = 0; i < 2; i++ )
			{
				var parameter = parameterList.Parameters[i];
				if ( !parameter.Type.IsEqual( syntax.Type, context ) )
				{
					var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.ChangeParameterTypeRule,
						parameter.Type.GetLocation(),
						parameter.Type.ToString() );
					context.ReportDiagnostic( diagnostic );
				}
			}

			break;
		}

		if ( !containsChangeMethod )
			context.ReportDiagnostic( Diagnostic.Create( Diagnostics.NetProperty.ChangeMissingRule, changeAttribute.GetLocation(), changeMethod ) );
	}

	private static void CheckNetAttribute( in SyntaxNodeAnalysisContext context, PropertyDeclarationSyntax syntax )
	{
		if ( !syntax.HasAttribute( "Net" ) )
			return;

		if ( syntax.TryGetModifier( SyntaxKind.StaticKeyword, out var token ) )
			context.ReportDiagnostic( Diagnostic.Create( Diagnostics.NetProperty.StaticRule, token.GetLocation() ) );

		if ( !syntax.IsAutoProperty() || !syntax.HasGetter() || !syntax.HasSetter() )
		{
			var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.AutoPropertyRule,
				syntax.AccessorList?.GetLocation() ?? syntax.GetLocation() );
			context.ReportDiagnostic( diagnostic );
		}

		if ( !syntax.Type.IsNetworkable( context ) )
		{
			var diagnostic = Diagnostic.Create( Diagnostics.NetProperty.NetworkableRule,
				syntax.Type.GetLocation(),
				syntax.Type.ToNameString( true, context ) );
			context.ReportDiagnostic( diagnostic );
		}
	}
}
