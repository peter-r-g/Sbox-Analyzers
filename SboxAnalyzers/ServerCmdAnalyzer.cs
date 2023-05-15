using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SboxAnalyzers.Extensions;
using System.Collections.Immutable;
using System.Linq;

namespace SboxAnalyzers;

/// <summary>
/// A Roslyn analyzer for checking event method arguments.
/// </summary>
[DiagnosticAnalyzer( LanguageNames.CSharp )]
public class ServerCmdAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc/>
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => Diagnostics.ServerCmd.AllRules;

	/// <inheritdoc/>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSyntaxNodeAction( AnalyzeMethodDeclaration, SyntaxKind.MethodDeclaration );
	}

	/// <summary>
	/// Analyzes a <see cref="MethodDeclarationSyntax"/> to check if it is a correctly implemented server command.
	/// </summary>
	/// <param name="context">The context relating to the syntax node being analyzed.</param>
	private static void AnalyzeMethodDeclaration( SyntaxNodeAnalysisContext context )
	{
		var methodDeclaration = (MethodDeclarationSyntax)context.Node;
		if ( methodDeclaration.ParameterList is null )
			return;

		// Check that all parameters are networkable in server commands.
		foreach ( var parameter in methodDeclaration.ParameterList.Parameters )
		{
			if ( parameter.Type is null )
				continue;

			if ( parameter.Type.IsServerCommandSupported( context.SemanticModel ) )
				continue;

			var diagnostic = Diagnostic.Create( Diagnostics.ServerCmd.UnsupportedRule,
				parameter.Type.GetLocation(),
				parameter.Type.ToNameString( true, context.SemanticModel ) );
			context.ReportDiagnostic( diagnostic );
		}
	}
}
