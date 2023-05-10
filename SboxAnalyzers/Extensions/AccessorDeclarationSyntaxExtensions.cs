using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SboxAnalyzers.Extensions;

/// <summary>
/// Contains extension methods for <see cref="AccessorDeclarationSyntax"/>s.
/// </summary>
internal static class AccessorDeclarationSyntaxExtensions
{
	/// <summary>
	/// Returns whether or not the <see cref="AccessorDeclarationSyntax"/> is auto-implemented.
	/// </summary>
	/// <param name="syntax">The <see cref="AccessorDeclarationSyntax"/> to check.</param>
	/// <returns>Whether or not the <see cref="AccessorDeclarationSyntax"/> is auto-implemented.</returns>
	internal static bool IsAutoImplemented( this AccessorDeclarationSyntax syntax )
	{
		return syntax.Body is null && syntax.ExpressionBody is null;
	}
}
