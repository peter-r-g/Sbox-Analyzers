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
		var initSetter = syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.InitAccessorDeclaration )
			.FirstOrDefault();

		if ( getter is not null && !getter.IsAutoImplemented() )
			return false;

		if ( setter is not null && !setter.IsAutoImplemented() )
			return false;

		if ( initSetter is not null && !initSetter.IsAutoImplemented() )
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

	/// <summary>
	/// Returns whether or not a <see cref="PropertyDeclarationSyntax"/> has a init setter.
	/// </summary>
	/// <param name="syntax">The <see cref="PropertyDeclarationSyntax"/> to check.</param>
	/// <returns>Whether or not a <see cref="PropertyDeclarationSyntax"/> has a init setter.</returns>
	internal static bool HasInitSetter( this PropertyDeclarationSyntax syntax )
	{
		if ( syntax.AccessorList is null )
			return false;

		return syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.InitAccessorDeclaration )
			.FirstOrDefault() is not null;
	}
}
