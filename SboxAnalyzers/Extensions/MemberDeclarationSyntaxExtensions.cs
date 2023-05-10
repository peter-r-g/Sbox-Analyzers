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
	/// Tries to get an attribute from the <see cref="MemberDeclarationSyntax"/>.
	/// </summary>
	/// <param name="syntax">The <see cref="MemberDeclarationSyntax"/> to get the attribute from.</param>
	/// <param name="attributeName">The name of the attribute.</param>
	/// <param name="attributeSyntax">The <see cref="AttributeSyntax"/> that was found.</param>
	/// <returns>Whether or not an <see cref="AttributeSyntax"/> was found.</returns>
	internal static bool TryGetAttribute( this MemberDeclarationSyntax syntax, string attributeName, out AttributeSyntax? attributeSyntax )
	{
		foreach ( var attributeList in syntax.AttributeLists )
		{
			foreach ( var attribute in attributeList.Attributes )
			{
				if ( attribute.Name.ToString() != attributeName )
					continue;

				attributeSyntax = attribute;
				return true;
			}
		}

		// Both "Local" and "LocalAttribute" are valid, need to check both cases.
		if ( !attributeName.EndsWith( "Attribute" ) )
			return syntax.TryGetAttribute( attributeName + "Attribute", out attributeSyntax );

		attributeSyntax = null;
		return false;
	}

	/// <summary>
	/// Returns whether or not a <see cref="MemberDeclarationSyntax"/> has an attribute.
	/// </summary>
	/// <param name="syntax">The <see cref="MemberDeclarationSyntax"/> to check.</param>
	/// <param name="attributeName">The name of the attribute to find.</param>
	/// <returns>Whether or not a <see cref="MemberDeclarationSyntax"/> has the attribute.</returns>
	internal static bool HasAttribute( this MemberDeclarationSyntax syntax, string attributeName )
	{
		return syntax.TryGetAttribute( attributeName, out _ );
	}

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
