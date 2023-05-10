using System;
using System.Collections.Concurrent;
using System.IO;

namespace SboxAnalyzers;

/// <summary>
/// A basic logger used for debugging purposes.
/// </summary>
internal static class Logs
{
	/// <summary>
	/// Whether or not debugging is enabled.
	/// </summary>
#if DEBUG
	internal const bool Enabled = true;
#else
	internal const bool Enabled = false;
#endif

	private static string LogPath => Path.Combine( Environment.CurrentDirectory, "_ASSEMBLY_NAME_ _CURRENT_TIME_.txt" );

	/// <summary>
	/// A thread-safe queue containing all of the messages to log.
	/// </summary>
	private static ConcurrentQueue<string> logs { get; } = new();

	/// <summary>
	/// Adds a new message to the log.
	/// </summary>
	/// <param name="str">The message to log.</param>
	/// <exception cref="InvalidOperationException">Thrown when trying to log when logs are disabled.</exception>
	/// <exception cref="ArgumentNullException">Thrown when the message passed is null.</exception>
	internal static void Add( string str )
	{
		if ( !Enabled )
			throw new InvalidOperationException( "Logs are not enabled" );

		if ( str is null )
			throw new ArgumentNullException( nameof( str ) );

		logs.Enqueue( str );
	}

	/// <summary>
	/// Flushes all logs to the disk at the specified path.
	/// </summary>
	/// <param name="assemblyName">The name of the assembly that is related to the flushed logs.</param>
	/// <exception cref="InvalidOperationException">Thrown when trying to flush when logs are disabled.</exception>
	internal static void Flush( string assemblyName )
	{
		if ( !Enabled )
			throw new InvalidOperationException( "Logs are not enabled" );

		var path = LogPath.Replace( "_ASSEMBLY_NAME_", assemblyName )
			.Replace( "_CURRENT_TIME_", DateTime.Now.ToString().Replace( ':', '-' ) );

		while ( logs.TryDequeue( out var log ) )
			File.AppendAllText( path, log + Environment.NewLine );
	}
}
