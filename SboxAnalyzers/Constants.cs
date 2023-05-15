using System.Collections.Immutable;

namespace SboxAnalyzers;

/// <summary>
/// Contains all of the information that is used for analysis of S&box code.
/// </summary>
internal static class Constants
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
	/// The name of an attribute that is used to decorate net properties.
	/// </summary>
	internal const string ChangeAttribute = "Change";
	/// <summary>
	/// The name of an attribute that is used to decorate net properties.
	/// </summary>
	internal const string LocalAttribute = "Local";
	/// <summary>
	/// The name of an attribute that is used to decorate net properties.
	/// </summary>
	internal const string NetAttribute = "Net";
	/// <summary>
	/// The name of the attribute that is used to decorate event listeners.
	/// </summary>
	internal const string EventAttribute = "Event";
	/// <summary>
	/// The name of the attribute that is used to decorate events with expected type parameters.
	/// </summary>
	internal const string MethodArgumentsAttribute = "MethodArguments";
	/// <summary>
	/// The name of the attribute that is used to decorate a method intended to be a server command.
	/// </summary>
	internal const string ServerCommandAttribute = "ConCmd.Server";
}
