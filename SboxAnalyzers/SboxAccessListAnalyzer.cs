using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using SboxAnalyzers.Extensions;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;

namespace SboxAnalyzers;

/// <summary>
/// A Roslyn analyzer for checking a compilation for any code usage that does not conform to the S&box code access list.
/// </summary>
[DiagnosticAnalyzer( LanguageNames.CSharp )]
public class SboxAccessListAnalyzer : DiagnosticAnalyzer
{
	/// <summary>
	/// The unique ID for the diagnostic message created by this analyzer.
	/// </summary>
	public const string DiagnosticId = "SBOXAL";
	/// <summary>
	/// The category that this diagnostic falls under.
	/// </summary>
	private const string Category = "Code Access";

	/// <summary>
	/// A localized title for the diagnostic.
	/// </summary>
	private static readonly LocalizableString Title = new LocalizableResourceString( nameof( Resources.AnalyzerTitle ), Resources.ResourceManager, typeof( Resources ) );
	/// <summary>
	/// A localized message format for the diagnostic.
	/// </summary>
	private static readonly LocalizableString MessageFormat = new LocalizableResourceString( nameof( Resources.AnalyzerMessageFormat ), Resources.ResourceManager, typeof( Resources ) );
	/// <summary>
	/// A localized description for the diagnostic.
	/// </summary>
	private static readonly LocalizableString Description = new LocalizableResourceString( nameof( Resources.AnalyzerDescription ), Resources.ResourceManager, typeof( Resources ) );
	/// <summary>
	/// The diagnostic descriptor for the analyzer.
	/// </summary>
	private static readonly DiagnosticDescriptor Rule = new( DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description );

	///<inheritdoc/>
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => supportedDiagnosics;
	/// <summary>
	/// Contains all of the diagnostics that this analyzer handles.
	/// </summary>
	private ImmutableArray<DiagnosticDescriptor> supportedDiagnosics = ImmutableArray.Create( Rule );

	/// <summary>
	/// A thread-safe bag containing all diagnostics that have been reported.
	/// </summary>
	private static ConcurrentBag<(string Name, Location Location)> ReportedDiagnostics { get; } = new();

	/// <inheritdoc/>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterCompilationStartAction( OnCompileStart );
	}

	/// <summary>
	/// Invoked when a new compile is starting.
	/// </summary>
	/// <param name="context">The context around the new compilation.</param>
	private static void OnCompileStart( CompilationStartAnalysisContext context )
	{
		context.RegisterSemanticModelAction( AnalyzeSemantics );

		// Clear any old diagnostics from a previous compile.
		while ( !ReportedDiagnostics.IsEmpty )
			ReportedDiagnostics.TryTake( out _ );

		var thisAssembly = context.Compilation.Assembly;

		// Setup access manager for new compilation.
		if ( thisAssembly.Name == "Menu Addon" )
			AccessManager.Config = "menu";
		else
			AccessManager.Config = "unknown";
	}

	/// <summary>
	/// Analyzes the syntax tree for any symbols that are not allowed.
	/// </summary>
	/// <param name="context">The semantic context of the compilation.</param>
	private static void AnalyzeSemantics( SemanticModelAnalysisContext context )
	{
		var semanticModel = context.SemanticModel;
		var thisAssembly = semanticModel.Compilation.Assembly;

		var root = semanticModel.SyntaxTree.GetRoot();
		var nodesToCheck = new List<(ISymbol, SyntaxNode)>();

		foreach ( var node in root.DescendantNodesAndSelf() )
		{
			// We do not want to double up on diagnostics in this case.
			if ( node is IdentifierNameSyntax && node.Parent is not VariableDeclarationSyntax )
				continue;

			var symbol = semanticModel.GetSymbolInfo( node ).Symbol;
			if ( symbol is null )
				continue;

			var containingAssembly = symbol.ContainingAssembly;
			if ( containingAssembly is null )
				continue;

			// FIXME: This feels like a really bad way of skipping "trusted" assemblies.
			var reference = semanticModel.Compilation.GetMetadataReference( containingAssembly ) as PortableExecutableReference;
			if ( reference is not null && reference.FilePath is not null && !reference.FilePath.Contains( "Microsoft.NETCore.App.Ref" ) )
				continue;

			// We do not care about checking our own assembly symbols.
			if ( SymbolEqualityComparer.Default.Equals( thisAssembly, containingAssembly ) )
				continue;

			// FIXME: For some reason, generated code is being analyzed even though it is disabled.
			var fileName = Path.GetFileNameWithoutExtension( node.GetLocation().SourceTree?.FilePath );
			if ( fileName is not null && AccessManager.InternalFilesToIgnore.Contains( fileName ) )
				continue;

			nodesToCheck.Add( (symbol, node) );
		}

		foreach ( var (symbol, node) in nodesToCheck )
		{
			if ( symbol.IsWhitelisted( node ) )
				continue;

			var symbolName = symbol.ToRuleString( node );
			var location = node.GetLocation();

			// Avoid multiplying diagnostics.
			if ( HasBeenReported( symbolName, location ) )
				continue;

			ReportedDiagnostics.Add( (symbolName, location) );
			Diagnostic diagnostic = Diagnostic.Create( Rule, location, symbolName );
			context.ReportDiagnostic( diagnostic );
		}
	}

	/// <summary>
	/// Returns whether or not a diagnostic of similiar information has already been reported.
	/// </summary>
	/// <param name="symbolName">The formatted name of the symbol.</param>
	/// <param name="location">The syntax location this comes from.</param>
	/// <returns>Whether or not a diagnostic of similiar information has already been reported.</returns>
	private static bool HasBeenReported( string symbolName, Location location )
	{
		foreach ( var reportedDiagnostic in ReportedDiagnostics )
		{
			if ( reportedDiagnostic.Name != symbolName )
				continue;

			if ( reportedDiagnostic.Location.SourceSpan.Start != location.SourceSpan.Start )
				continue;

			return true;
		}

		return false;
	}
}
