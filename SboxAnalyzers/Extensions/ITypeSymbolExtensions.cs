using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace SboxAnalyzers.Extensions;

/// <summary>
/// Contains extension methods for <see cref="ITypeSymbol"/>s.
/// </summary>
internal static class ITypeSymbolExtensions
{
	/// <summary>
	/// Contains a set of all types that can be networked in S&box.
	/// </summary>
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

	/// <summary>
	/// Contains a set of all types that are supported for server commands in S&box.
	/// </summary>
	internal static ImmutableHashSet<string> ServerCommandSupportedTypes = ImmutableHashSet.Create(
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
		"Sandbox.Vector4",
		"Sandbox.Rotation",
		"Sandbox.Angles",
		"Sandbox.Color",
		"Sandbox.RangedFloat"
		);

	/// <summary>
	/// Returns whether or not the <see cref="ITypeSymbol"/> can be networked.
	/// </summary>
	/// <param name="symbol">The symbol to check if it is networkable.</param>
	/// <returns>Whether or not the <see cref="ITypeSymbol"/> can be networked.</returns>
	internal static bool IsNetworkable( this ITypeSymbol symbol)
	{
		// Fast path, enums can be networked.
		if ( symbol.TypeKind == TypeKind.Enum )
			return true;

		// Check that if the symbol is a generic, make sure its arguments are networkable.
		if ( symbol is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.TypeArguments.Length > 0 )
		{
			foreach ( var typeArgument in namedTypeSymbol.TypeArguments )
			{
				if ( !typeArgument.IsNetworkable() )
					return false;
			}
		}

		// Fast path, check if the type string is contained in the set.
		if ( NetworkableTypes.Contains( symbol.ToNameString( false ) ) )
			return true;

		// Check all interfaces of the type, see if one matches the set.
		foreach ( var @interface in symbol.AllInterfaces )
		{
			if ( NetworkableTypes.Contains( @interface.ToNameString( false ) ) )
				return true;
		}

		// Check classes base types, see if one matches the set.
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
		// Structs can be networked if they are unmanaged.
		else if ( symbol.IsValueType )
			return symbol.IsUnmanagedType;

		return false;
	}

	/// <summary>
	/// Returns whether or not the <see cref="ITypeSymbol"/> is supported in server commands.
	/// </summary>
	/// <param name="symbol">The symbol to check if it is networkable in server commands.</param>
	/// <returns>Whether or not the <see cref="ITypeSymbol"/> is supported in server commands.</returns>
	internal static bool IsServerCommandSupported( this ITypeSymbol symbol )
	{
		// Fast path, enums are supported.
		if ( symbol.TypeKind == TypeKind.Enum )
			return true;

		return ServerCommandSupportedTypes.Contains( symbol.ToNameString( false ) );
	}

	/// <summary>
	/// Returns a type string of a <see cref="ITypeSymbol"/>.
	/// </summary>
	/// <param name="symbol">The symbol to stringify.</param>
	/// <param name="useTypeArguments">Whether or not to use the constructed type arguments.</param>
	/// <returns>A type string of a <see cref="ITypeSymbol"/>.</returns>
	internal static string ToNameString( this ITypeSymbol symbol, bool useTypeArguments )
	{
		// Get namespaces.
		var namespaces = new List<INamespaceSymbol>();

		var namespaceSymbol = symbol.ContainingNamespace;
		while ( namespaceSymbol is not null && !namespaceSymbol.IsGlobalNamespace )
		{
			namespaces.Add( namespaceSymbol );
			namespaceSymbol = namespaceSymbol.ContainingNamespace;
		}

		// Get types.
		var types = new List<INamedTypeSymbol>();

		var typeSymbol = symbol.ContainingType;
		while ( typeSymbol is not null )
		{
			types.Add( typeSymbol );
			typeSymbol = typeSymbol.ContainingType;
		}

		// Reverse them to the right order.
		namespaces.Reverse();
		types.Reverse();

		// Build string.
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

		// Include generics if applicable.
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
