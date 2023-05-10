using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SboxAnalyzers.Extensions;

internal static class MemberDeclarationSyntaxExtensions
{
	internal static bool TryGetModifier( this MemberDeclarationSyntax syntax, SyntaxKind syntaxKind, out SyntaxToken token )
	{
		foreach ( var modifier in syntax.Modifiers )
		{
			if ( !modifier.IsKind( SyntaxKind.StaticKeyword ) )
				continue;

			token = modifier;
			return true;
		}

		token = default;
		return false;
	}
}
