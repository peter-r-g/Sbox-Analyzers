using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace SboxAccessListAnalyzer;

/// <summary>
/// Contains extension methods for <see cref="ISymbol"/>s.
/// </summary>
internal static class ISymbolExtensions
{
	/// <summary>
	/// Returns whether or not the symbol is whitelisted.
	/// </summary>
	/// <param name="symbol">The symbol to check.</param>
	/// <param name="syntax">The syntax that is associated with the symbols usage.</param>
	/// <returns>Whether or not the symbol is whitelisted.</returns>
	internal static bool IsWhitelisted( this ISymbol symbol, SyntaxNode syntax )
	{
		if ( AccessManager.TryGetCached( symbol, out var result ) )
			return result;

		var symbolName = symbol.ToNameString( syntax );
		// Check it at face value first.
		{
			var firstCheck = symbolName;
			if ( symbol.ContainingAssembly is not null )
				firstCheck = symbol.ContainingAssembly.Name + '/' + firstCheck;

			if ( AccessManager.IsWhitelisted( firstCheck ) )
			{
				AccessManager.TryCache( symbol, true );
				return true;
			}
		}

		// Check it with different assemblies.
		foreach ( var alternateAssembly in AccessManager.AlternateAssemblies )
		{
			if ( !AccessManager.IsWhitelisted( alternateAssembly + '/' + symbolName ) )
				continue;

			AccessManager.TryCache( symbol, true );
			return true;
		}

		AccessManager.TryCache( symbol, false );
		return false;
	}

	/// <summary>
	/// Returns a string representation of the symbol that matches the <see cref="Sandbox.Rules"/>.
	/// </summary>
	/// <param name="symbol">The symbol to get a string representation from.</param>
	/// <param name="syntax">The syntax that is associated with the symbols usage.</param>
	/// <returns>A string representation of the symbol that matches the <see cref="Sandbox.Rules"/>.</returns>
	/// <exception cref="NotImplementedException">Thrown when the type of symbol passed is not currently supported.</exception>
	internal static string ToNameString( this ISymbol symbol, SyntaxNode syntax )
	{
		switch ( symbol )
		{
			case INamedTypeSymbol namedTypeSymbol:
				return namedTypeSymbol.GetNamespaceString() + '.' + namedTypeSymbol.GetTypeString();
			case IMethodSymbol methodSymbol:
				return methodSymbol.GetNamespaceString() + '.' + methodSymbol.GetTypeString() + '.' + methodSymbol.GetMethodString();
			case INamespaceSymbol namespaceSymbol:
				return namespaceSymbol.GetNamespaceString();
			case IPropertySymbol propertySymbol:
				var isGetter = syntax is MemberAccessExpressionSyntax;
				var isSetter = false;

				if ( syntax.Parent is AssignmentExpressionSyntax assignment && assignment.Left.IsEquivalentTo( syntax ) )
				{
					isGetter = false;
					isSetter = true;
				}

				return propertySymbol.GetNamespaceString() + '.' + propertySymbol.GetTypeString() + '.' + propertySymbol.GetPropertyString( isGetter, isSetter );
			case IFieldSymbol fieldSymbol:
				return fieldSymbol.GetNamespaceString() + '.' + fieldSymbol.GetTypeString() + '.' + fieldSymbol.GetFieldString();
			default:
				throw new NotImplementedException( symbol.GetType().FullName );
		}
	}

	/// <summary>
	/// Returns a string representation of a symbols namespace that matches the <see cref="Sandbox.Rules"/>.
	/// </summary>
	/// <param name="symbol">The symbol whose namespace to get.</param>
	/// <returns>A string representation of a symbols namespace that matches the <see cref="Sandbox.Rules"/>.</returns>
	private static string GetNamespaceString( this ISymbol symbol )
	{
		var namespaces = new List<INamespaceSymbol>();

		// Recurse through the namespaces of the symbol.
		var currentNamespace = symbol.ContainingNamespace;
		while ( currentNamespace is not null && !currentNamespace.IsGlobalNamespace )
		{
			namespaces.Add( currentNamespace );
			currentNamespace = currentNamespace.ContainingNamespace;
		}

		// Flip to correct order.
		namespaces.Reverse();

		// Include symbol if it is a namespace.
		if ( symbol is INamespaceSymbol namespaceSymbol )
			namespaces.Add( namespaceSymbol );

		// Edge cases.
		if ( namespaces.Count == 0 )
			return string.Empty;
		else if ( namespaces.Count == 1 )
			return namespaces[0].Name;

		// Build string and return.
		var sb = new StringBuilder();
		sb.Append( namespaces[0].Name );
		
		for ( var i = 1; i < namespaces.Count; i++ )
		{
			sb.Append( '.' );
			sb.Append( namespaces[i].Name );
		}

		return sb.ToString();
	}

	/// <summary>
	/// Returns a string representation of a symbols containing type(s) that matches the <see cref="Sandbox.Rules"/>.
	/// </summary>
	/// <param name="symbol">The symbol whose containing type(s) to get.</param>
	/// <returns>A string representation of a symbols containing type(s) that matches the <see cref="Sandbox.Rules"/>.</returns>
	private static string GetTypeString( this ISymbol symbol )
	{
		var types = new List<INamedTypeSymbol>();

		// Recurse through the types of the symbol.
		var currentType = symbol.ContainingType;
		while ( currentType is not null )
		{
			types.Add( currentType );
			currentType = currentType.ContainingType;
		}

		// Flip to correct order.
		types.Reverse();

		// Include symbol if it is a type.
		if ( symbol is INamedTypeSymbol namedTypeSymbol )
			types.Add( namedTypeSymbol );

		// Edge cases.
		if ( types.Count == 0 )
			return string.Empty;
		else if ( types.Count == 1 )
			return types[0].Name;

		// Build string and return.
		var sb = new StringBuilder();
		sb.Append( types[0].Name );

		for ( var i = 1; i < types.Count; i++ )
		{
			sb.Append( '.' );
			sb.Append( types[i].Name );
		}

		return sb.ToString();
	}

	/// <summary>
	/// Returns a string representation of a <see cref="IMethodSymbol"/> that matches the <see cref="Sandbox.Rules"/>.
	/// </summary>
	/// <param name="symbol">The <see cref="IMethodSymbol"/> to parse.</param>
	/// <returns>A string representation of a <see cref="IMethodSymbol"/> that matches the <see cref="Sandbox.Rules"/>.</returns>
	private static string GetMethodString( this IMethodSymbol symbol )
	{
		// Fast path.
		if ( symbol.Parameters.Length == 0 )
			return symbol.Name + "()";

		var sb = new StringBuilder();

		// Method name and open.
		sb.Append( symbol.Name );
		sb.Append( "( " );

		// Single parameter.
		if ( symbol.Parameters.Length == 1 )
		{
			sb.Append( GetNamespaceString( symbol.Parameters[0] ) );
			sb.Append( '.' );
			sb.Append( GetTypeString( symbol.Parameters[0] ) );
		}
		// Multiple parameters.
		else
		{
			sb.Append( GetNamespaceString( symbol.Parameters[0] ) );
			sb.Append( '.' );
			sb.Append( GetTypeString( symbol.Parameters[0] ) );

			for ( var i = 1; i < symbol.Parameters.Length; i++ )
			{
				sb.Append( ", " );
				sb.Append( GetNamespaceString( symbol.Parameters[i] ) );
				sb.Append( '.' );
				sb.Append( GetTypeString( symbol.Parameters[i] ) );
			}
		}

		// Close and return.
		sb.Append( " )" );

		return sb.ToString();
	}

	/// <summary>
	/// Returns a string representation of a <see cref="IPropertySymbol"/> that matches the <see cref="Sandbox.Rules"/>.
	/// </summary>
	/// <param name="symbol">The <see cref="IPropertySymbol"/> to parse.</param>
	/// <param name="isGetter">Whether or not to return as a getter method of the property.</param>
	/// <param name="isSetter">Whether or not to return as a setter method of the property.</param>
	/// <returns>A string representation of a <see cref="IPropertySymbol"/> that matches the <see cref="Sandbox.Rules"/>.</returns>
	private static string GetPropertyString( this IPropertySymbol symbol, bool isGetter, bool isSetter )
	{
		if ( isGetter )
			return "get_" + symbol.Name + "()";
		else if ( isSetter )
			return "set_" + symbol.Name + "()";

		return symbol.Name;
	}

	/// <summary>
	/// Returns a string representation of a <see cref="IFieldSymbol"/> that matches the <see cref="Sandbox.Rules"/>.
	/// </summary>
	/// <param name="symbol">The <see cref="IFieldSymbol"/> to parse.</param>
	/// <returns>A string representation of a <see cref="IFieldSymbol"/> that matches the rules.</returns>
	private static string GetFieldString( this IFieldSymbol symbol )
	{
		return symbol.Name;
	}
}
