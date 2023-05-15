using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SboxAnalyzers.Extensions;

/// <summary>
/// Contains extension methods for <see cref="TypeSyntax"/>s.
/// </summary>
internal static class TypeSyntaxExtensions
{
	/// <summary>
	/// Returns whether or not a <see cref="TypeSyntax"/> is networkable.
	/// </summary>
	/// <param name="syntax">The <see cref="TypeSyntax"/> to check.</param>
	/// <param name="semanticModel">The semantic model context around the compilation.</param>
	/// <returns>Whether or not a <see cref="TypeSyntax"/> is networkable.</returns>
	internal static bool IsNetworkable( this TypeSyntax syntax, SemanticModel semanticModel )
	{
		var symbol = semanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not INamedTypeSymbol namedTypeSymbol )
			return false;

		return namedTypeSymbol.IsNetworkable();
	}

	/// <summary>
	/// Returns whether or not a <see cref="TypeSyntax"/> is supported in server commands.
	/// </summary>
	/// <param name="syntax">The <see cref="TypeSyntax"/> to check.</param>
	/// <param name="semanticModel">The semantic model context around the compilation.</param>
	/// <returns>Whether or not a <see cref="TypeSyntax"/> is supported in server commands.</returns>
	internal static bool IsServerCommandSupported( this TypeSyntax syntax, SemanticModel semanticModel )
	{
		var symbol = semanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not INamedTypeSymbol namedTypeSymbol )
			return false;

		return namedTypeSymbol.IsServerCommandSupported();
	}

	/// <summary>
	/// Returns whether or not a <see cref="TypeSyntax"/> is equal to another <see cref="TypeSyntax"/>.
	/// </summary>
	/// <param name="syntax">The first syntax to compare.</param>
	/// <param name="other">The second syntax to compare.</param>
	/// <param name="semanticModel">The semantic model context around the compilation.</param>
	/// <returns>Whether or not the <see cref="TypeSyntax"/>s are equal.</returns>
	internal static bool IsEqual( this TypeSyntax syntax, TypeSyntax other, SemanticModel semanticModel )
	{
		var symbolInfo1 = semanticModel.GetSymbolInfo( syntax );
		var symbolInfo2 = semanticModel.GetSymbolInfo( other );

		if ( symbolInfo1.Symbol is not ISymbol symbol1 || symbolInfo2.Symbol is not ISymbol symbol2 )
			return false;

		return SymbolEqualityComparer.Default.Equals( symbol1, symbol2 );
	}

	/// <summary>
	/// Returns a type string of a <see cref="TypeSyntax"/>.
	/// </summary>
	/// <param name="syntax">The syntax to stringify.</param>
	/// <param name="useTypeArguments">Whether or not to use the constructed type arguments.</param>
	/// <param name="semanticModel">The semantic model context around the compilation.</param>
	/// <returns>A type string of a <see cref="TypeSyntax"/>.</returns>
	internal static string ToNameString( this TypeSyntax syntax, bool useTypeArguments, SemanticModel semanticModel )
	{
		var symbol = semanticModel.GetSymbolInfo( syntax );
		if ( symbol.Symbol is not ITypeSymbol typeSymbol )
			return string.Empty;

		return typeSymbol.ToNameString( useTypeArguments );
	}
}
