using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace SboxAnalyzers.Extensions;

internal static class ITypeSymbolExtensions
{
	internal static string ToNameString( this ITypeSymbol symbol, bool useTypeArguments )
	{
		var namespaces = new List<INamespaceSymbol>();

		var namespaceSymbol = symbol.ContainingNamespace;
		while ( namespaceSymbol is not null && !namespaceSymbol.IsGlobalNamespace )
		{
			namespaces.Add( namespaceSymbol );
			namespaceSymbol = namespaceSymbol.ContainingNamespace;
		}

		var types = new List<INamedTypeSymbol>();

		var typeSymbol = symbol.ContainingType;
		while ( typeSymbol is not null )
		{
			types.Add( typeSymbol );
			typeSymbol = typeSymbol.ContainingType;
		}

		namespaces.Reverse();
		types.Reverse();

		var sb = new StringBuilder();

		foreach ( var @namespace in namespaces )
		{
			sb.Append( @namespace.Name );
			sb.Append( '.' );
		}

		foreach ( var type in types )
		{
			sb.Append( type.Name );
			sb.Append( '.' );
		}

		sb.Append( symbol.Name );

		if ( symbol is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.IsGenericType )
		{
			if ( useTypeArguments && namedTypeSymbol.TypeArguments.Length > 0 )
			{
				sb.Append( '<' );

				sb.Append( namedTypeSymbol.TypeArguments[0].ToNameString( true ) );

				for ( var i = 1; i < namedTypeSymbol.TypeArguments.Length; i++ )
				{
					sb.Append( ", " );
					sb.Append( namedTypeSymbol.TypeArguments[i].ToNameString( true ) );
				}

				sb.Append( '>' );
			}
			else if ( namedTypeSymbol.TypeParameters.Length > 0 )
			{
				sb.Append( '<' );

				sb.Append( namedTypeSymbol.TypeParameters[0].Name );

				for ( var i = 1; i < namedTypeSymbol.TypeParameters.Length; i++ )
				{
					sb.Append( ", " );
					sb.Append( namedTypeSymbol.TypeParameters[i].Name );
				}

				sb.Append( '>' );
			}
		}

		return sb.ToString();
	}
}
