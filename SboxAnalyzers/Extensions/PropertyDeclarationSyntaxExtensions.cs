using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SboxAnalyzers.Extensions;

/// <summary>
/// Contains extension methods for <see cref="PropertyDeclarationSyntax"/>s.
/// </summary>
internal static class PropertyDeclarationSyntaxExtensions
{
	/// <summary>
	/// Tries to get an attribute from the <see cref="PropertyDeclarationSyntax"/>.
	/// </summary>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to get the attribute from.</param>
	/// <param name="attributeName">The name of the attribute.</param>
	/// <param name="attributeSyntax">The <see cref="AttributeSyntax"/> that was found.</param>
	/// <returns>Whether or not an <see cref="AttributeSyntax"/> was found.</returns>
	internal static bool TryGetAttribute( this PropertyDeclarationSyntax syntax, string attributeName, out AttributeSyntax? attributeSyntax )
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
	/// Returns whether or not a <see cref="PropertyDeclarationSyntax"/> has an attribute.
	/// </summary>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	/// <param name="attributeName">The name of the attribute to find.</param>
	/// <returns>Whether or not a <see cref="PropertyDeclarationSyntax"/> has the attribute.</returns>
	internal static bool HasAttribute( this PropertyDeclarationSyntax syntax, string attributeName )
	{
		return syntax.TryGetAttribute( attributeName, out _ );
	}

	/// <summary>
	/// Returns whether or not a <see cref="PropertyDeclarationSyntax"/> is implemented as an auto-property.
	/// </summary>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	/// <returns>Whether or not a <see cref="PropertyDeclarationSyntax"/> is implemented as an auto-property.</returns>
	internal static bool IsAutoProperty( this PropertyDeclarationSyntax syntax )
	{
		if ( syntax.AccessorList is null )
			return false;

		var getter = syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.GetAccessorDeclaration )
			.FirstOrDefault();
		var setter = syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.SetAccessorDeclaration )
			.FirstOrDefault();

		if ( getter is not null && !getter.IsAutoImplemented() )
			return false;

		if ( setter is not null && !setter.IsAutoImplemented() )
			return false;

		return true;
	}

	/// <summary>
	/// Returns whether or not a <see cref="PropertyDeclarationSyntax"/> has a getter.
	/// </summary>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	/// <returns>Whether or not a <see cref="PropertyDeclarationSyntax"/> has a getter.</returns>
	internal static bool HasGetter( this PropertyDeclarationSyntax syntax )
	{
		if ( syntax.AccessorList is null )
			return false;

		return syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.GetAccessorDeclaration )
			.FirstOrDefault() is not null;
	}

	/// <summary>
	/// Returns whether or not a <see cref="PropertyDeclarationSyntax"/> has a setter.
	/// </summary>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	/// <returns>Whether or not a <see cref="PropertyDeclarationSyntax"/> has a setter.</returns>
	internal static bool HasSetter( this PropertyDeclarationSyntax syntax )
	{
		if ( syntax.AccessorList is null )
			return false;

		return syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.SetAccessorDeclaration )
			.FirstOrDefault() is not null;
	}
}
