using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SboxAnalyzers.Extensions;

internal static class TypeSyntaxExtensions
{
	internal static bool IsNetworkable( this TypeSyntax syntax, SemanticModel semanticModel )
	{
		var symbol = semanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not INamedTypeSymbol namedTypeSymbol )
			return false;

		return namedTypeSymbol.IsNetworkable();
	}
	
	internal static bool IsEqual( this TypeSyntax syntax, TypeSyntax other, SemanticModel semanticModel )
	{
		var symbolInfo1 = semanticModel.GetSymbolInfo( syntax );
		var symbolInfo2 = semanticModel.GetSymbolInfo( other );

		if ( symbolInfo1.Symbol is not ISymbol symbol1 || symbolInfo2.Symbol is not ISymbol symbol2 )
			return false;

		return SymbolEqualityComparer.Default.Equals( symbol1, symbol2 );
	}

	internal static string ToNameString( this TypeSyntax syntax, bool useTypeArguments, SemanticModel semanticModel )
	{
		var symbol = semanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not ITypeSymbol typeSymbol )
			return string.Empty;

		return typeSymbol.ToNameString( useTypeArguments );
	}
}
