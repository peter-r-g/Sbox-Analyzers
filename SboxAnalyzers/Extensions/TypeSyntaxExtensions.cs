using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SboxAnalyzers.Extensions;

internal static class TypeSyntaxExtensions
{
	internal static bool IsNetworkable( this TypeSyntax syntax, in SyntaxNodeAnalysisContext context )
	{
		var symbol = context.SemanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not INamedTypeSymbol namedTypeSymbol )
			return false;

		return namedTypeSymbol.IsNetworkable( context );
	}

	internal static bool IsEqual( this TypeSyntax syntax, TypeSyntax other, in SyntaxNodeAnalysisContext context )
	{
		var symbolInfo1 = context.SemanticModel.GetSymbolInfo( syntax );
		var symbolInfo2 = context.SemanticModel.GetSymbolInfo( other );

		if ( symbolInfo1.Symbol is not ISymbol symbol1 || symbolInfo2.Symbol is not ISymbol symbol2 )
			return false;

		return SymbolEqualityComparer.Default.Equals( symbol1, symbol2 );
	}

	internal static string ToNameString( this TypeSyntax syntax, bool useTypeArguments, in SyntaxNodeAnalysisContext context )
	{
		var symbol = context.SemanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not ITypeSymbol typeSymbol )
			return string.Empty;

		return typeSymbol.ToNameString( useTypeArguments );
	}
}
