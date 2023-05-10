using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SboxAnalyzers.Extensions;

internal static class PropertyDeclarationSyntaxExtensions
{
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

		if ( !attributeName.EndsWith( "Attribute" ) )
			return syntax.TryGetAttribute( attributeName + "Attribute", out attributeSyntax );

		attributeSyntax = null;
		return false;
	}

	internal static bool HasAttribute( this PropertyDeclarationSyntax syntax, string attributeName )
	{
		return syntax.TryGetAttribute( attributeName, out _ );
	}

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

	internal static bool HasGetter( this PropertyDeclarationSyntax syntax )
	{
		if ( syntax.AccessorList is null )
			return false;

		return syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.GetAccessorDeclaration )
			.FirstOrDefault() is not null;
	}

	internal static bool HasSetter( this PropertyDeclarationSyntax syntax )
	{
		if ( syntax.AccessorList is null )
			return false;

		return syntax.AccessorList.Accessors.Where( a => a.Kind() == SyntaxKind.SetAccessorDeclaration )
			.FirstOrDefault() is not null;
	}
}
