using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SboxAnalyzers.Extensions;

internal static class AccessorDeclarationSyntaxExtensions
{
	internal static bool IsAutoImplemented( this AccessorDeclarationSyntax syntax )
	{
		return syntax.Body is null && syntax.ExpressionBody is null;
	}
}
