using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SboxAnalyzers.Extensions;

/// <summary>
/// Contains extension methods for <see cref="MemberDeclarationSyntax"/>s.
/// </summary>
internal static class MemberDeclarationSyntaxExtensions
{
	/// <summary>
	/// Tries to get a syntax modifier from a <see cref="MemberDeclarationSyntax"/>.
	/// </summary>
	/// <param name="syntax">The member to get a modifier from.</param>
	/// <param name="syntaxKind">The kind of modifier to get from the member.</param>
	/// <param name="token">The modifier token that was found on the member.</param>
	/// <returns>Whether or not a modifier token was found on the member.</returns>
	internal static bool TryGetModifier( this MemberDeclarationSyntax syntax, SyntaxKind syntaxKind, out SyntaxToken token )
	{
		foreach ( var modifier in syntax.Modifiers )
		{
			if ( !modifier.IsKind( syntaxKind ) )
				continue;

			token = modifier;
			return true;
		}

		token = default;
		return false;
	}
}
