using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SboxAnalyzers.Extensions;

internal static class ITypeSymbolExtensions
{
	internal static ImmutableHashSet<string> NetworkableTypes = ImmutableHashSet.Create(
		typeof( sbyte ).FullName,
		typeof( byte ).FullName,
		typeof( short ).FullName,
		typeof( ushort ).FullName,
		typeof( int ).FullName,
		typeof( uint ).FullName,
		typeof( long ).FullName,
		typeof( ulong ).FullName,
		typeof( float ).FullName,
		typeof( double ).FullName,
		typeof( bool ).FullName,
		typeof( string ).FullName,
		"Sandbox.Vector2",
		"Sandbox.Vector3",
		"Sandbox.Rotation",
		"Sandbox.Angles",
		"Sandbox.Transform",
		"Sandbox.BaseNetworkable",
		"Sandbox.Entity",
		"Sandbox.Material",
		"Sandbox.Model",
		"Sandbox.GameResource",
		"System.Collections.Generic.IList<T>",
		"System.Collections.Generic.IDictionary<TKey, TValue>"
		);

	internal static bool IsNetworkable( this ITypeSymbol symbol)
	{
		if ( symbol.TypeKind == TypeKind.Enum )
			return true;

		if ( symbol is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.TypeArguments.Length > 0 )
		{
			foreach ( var typeArgument in namedTypeSymbol.TypeArguments )
			{
				if ( !typeArgument.IsNetworkable() )
					return false;
			}
		}

		if ( NetworkableTypes.Contains( symbol.ToNameString( false ) ) )
			return true;

		foreach ( var @interface in symbol.AllInterfaces )
		{
			if ( NetworkableTypes.Contains( @interface.ToNameString( false ) ) )
				return true;
		}

		if ( symbol.IsReferenceType )
		{
			var currentType = symbol.BaseType;
			while ( currentType is not null )
			{
				if ( NetworkableTypes.Contains( currentType.ToNameString( false ) ) )
					return true;

				currentType = currentType.BaseType;
			}
		}
		else if ( symbol.IsValueType )
			return symbol.IsUnmanagedType;

		return false;
	}

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
