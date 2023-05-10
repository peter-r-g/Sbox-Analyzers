using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;
using System.Diagnostics;

namespace SboxAnalyzers.Extensions;

internal static class INamedTypeSymbolExtensions
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
		"System.Collections.Generic.IList<T>",
		"System.Collections.Generic.IDictionary<TKey, TValue>"
		);

	internal static bool IsNetworkable( this ITypeSymbol symbol, in SyntaxNodeAnalysisContext context )
	{
		if ( symbol.TypeKind == TypeKind.Enum )
			return true;

		if ( symbol is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.TypeArguments.Length > 0 )
		{
			foreach ( var typeArgument in namedTypeSymbol.TypeArguments )
			{
				if ( !typeArgument.IsNetworkable( context ) )
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
}
