using Microsoft.CodeAnalysis;
using Sandbox;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace SboxAnalyzers;

/// <summary>
/// Encapsulates all of the required functionality to check the S&box access list.
/// </summary>
internal class AccessManager
{
	/// <summary>
	/// The current configuration that the <see cref="AccessRules"/> are using.
	/// </summary>
	internal static string Config
	{
		get => config;
		set
		{
			if ( config == value )
				return;

			config = value;
			Rules = new AccessRules( config );
			WhitelistCache.Clear();
		}
	}
	/// <summary>
	/// See <see cref="Config"/>.
	/// </summary>
	private static string config = "unknown";

	/// <summary>
	/// An <see cref="ImmutableArray{string}"/> containing all of the assemblies that types could also be resolved from.
	/// </summary>
	internal static ImmutableArray<string> AlternateAssemblies { get; } = ImmutableArray.Create(
		"System.Private.CoreLib", // Many C# types resolve to this assembly.
		"System.Private.Uri" // S&box System.Uri resolves to this assembly.
	);

	/// <summary>
	/// An <see cref="ImmutableArray{T}"/> containing names of files to ignore when checking.
	/// </summary>
	internal static ImmutableArray<string> InternalFilesToIgnore { get; } = ImmutableArray.Create(
		"_RPCRead",
		"_RPCReadStatic",
		"_BuildNetworkTable"
	);

	/// <summary>
	/// The <see cref="AccessRules"/> instance.
	/// </summary>
	private static AccessRules Rules { get; set; } = new AccessRules( config );
	/// <summary>
	/// A thread-safe dictionary containing a cache of all checked symbols and their whitelist result.
	/// </summary>
	private static ConcurrentDictionary<ISymbol, bool> WhitelistCache { get; } = new ConcurrentDictionary<ISymbol, bool>();

	/// <summary>
	/// Returns whether or not the string passed is contained in the whitelist.
	/// </summary>
	/// <param name="str">The string to test against the whitelist.</param>
	/// <returns>Whether or not the string passed is contained in the whitelist.</returns>
	internal static bool IsWhitelisted( string str ) => Rules.IsInWhitelist( str );

	/// <summary>
	/// Tries to cache the symbol along with its whitelisted result.
	/// </summary>
	/// <param name="symbol">The symbol to cache.</param>
	/// <param name="whitelisted">The result of the whitelist.</param>
	/// <returns>Whether or not the result was cached.</returns>
	internal static bool TryCache( ISymbol symbol, bool whitelisted ) => WhitelistCache.TryAdd( symbol, whitelisted );

	/// <summary>
	/// Tries to get a symbols whitelist result from the cache.
	/// </summary>
	/// <param name="symbol">The symbol key to get from the cache.</param>
	/// <param name="whitelisted">The result of the symbol keys whitelist.</param>
	/// <returns>Whether or not the symbol is contained in the cache.</returns>
	internal static bool TryGetCached( ISymbol symbol, out bool whitelisted ) => WhitelistCache.TryGetValue( symbol, out whitelisted );
}
