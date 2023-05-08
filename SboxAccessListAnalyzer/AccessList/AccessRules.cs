// Decompiled with JetBrains decompiler
// Type: Sandbox.AccessRules
// Assembly: Sandbox.Access, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 2DFCFBC9-66F4-43AC-BC15-AB403E66F12A
// Assembly location: D:\SteamLibrary\steamapps\common\sbox\bin\managed\Sandbox.Access.dll
// XML documentation location: D:\SteamLibrary\steamapps\common\sbox\bin\managed\Sandbox.Access.xml
// Decompiled 12:09PM EST on 05/05/2023

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Sandbox
{
	public class AccessRules
	{
		public List<string> AssemblyWhitelist = new List<string>();
		public List<Regex> Whitelist = new List<Regex>();

		private void InitAssemblyList()
		{
			this.AssemblyWhitelist.Add( "System.Private.CoreLib" );
			this.AssemblyWhitelist.Add( "Sandbox.Game" );
			this.AssemblyWhitelist.Add( "Sandbox.Engine" );
			this.AssemblyWhitelist.Add( "Sandbox.Event" );
			this.AssemblyWhitelist.Add( "Sandbox.Bind" );
			this.AssemblyWhitelist.Add( "Sandbox.Reflection" );
			this.AssemblyWhitelist.Add( "System.Text.Json" );
			this.AssemblyWhitelist.Add( "System.Numerics.Vectors" );
			this.AssemblyWhitelist.Add( "System.ComponentModel.Annotations" );
			this.AssemblyWhitelist.Add( "System.Runtime" );
			this.AssemblyWhitelist.Add( "Sandbox.System" );
			this.AssemblyWhitelist.Add( "System.Linq" );
			this.AssemblyWhitelist.Add( "System.Runtime.Extensions" );
			this.AssemblyWhitelist.Add( "System.Collections" );
			this.AssemblyWhitelist.Add( "System.Collections.Concurrent" );
			this.AssemblyWhitelist.Add( "System.Text.RegularExpressions" );
			this.AssemblyWhitelist.Add( "System.ComponentModel.Primitives" );
			this.AssemblyWhitelist.Add( "System.IO.Compression" );
			this.AssemblyWhitelist.Add( "System.Collections.Specialized" );
			this.AssemblyWhitelist.Add( "System.Web.HttpUtility" );
			this.AssemblyWhitelist.Add( "System.Private.Uri" );
			this.AssemblyWhitelist.Add( "System.ComponentModel.Primitives" );
			this.AssemblyWhitelist.Add( "System.ObjectModel" );
			this.AssemblyWhitelist.Add( "System.Collections.Immutable" );
			this.AssemblyWhitelist.Add( "System.Security.Cryptography" );
			this.AssemblyWhitelist.Add( "System.Threading.Channels" );
			this.AssemblyWhitelist.Add( "System.Threading" );
			this.AssemblyWhitelist.Add( "System.Memory" );
			this.AssemblyWhitelist.Add( "System.Net.Http" );
			this.AssemblyWhitelist.Add( "System.Net.Http.Json" );
			this.AssemblyWhitelist.Add( "System.Net.Primitives" );
		}

		public AccessRules( string config )
		{
			this.InitAssemblyList();
			this.AddRange( (IEnumerable<string>)Rules.BaseAccess );
			this.AddRange( (IEnumerable<string>)Rules.Types );
			this.AddRange( (IEnumerable<string>)Rules.Reflection );
			this.AddRange( (IEnumerable<string>)Rules.Exceptions );
			this.AddRange( (IEnumerable<string>)Rules.Diagnostics );
			this.AddRange( (IEnumerable<string>)Rules.Async );
			if ( !(config == "menu") )
				return;
			this.AddRule( "Sandbox.Menu/*" );
			this.AssemblyWhitelist.Add( "Sandbox.Menu" );
		}

		private void AddRange( IEnumerable<string> rules )
		{
			foreach ( string rule in rules )
				this.AddRule( rule );
		}

		private void AddRule( string line ) => this.Whitelist.Add( new Regex( "^" + Regex.Escape( line.Trim() ).Replace( "\\*", ".*" ) + "$" ) );

		/// <summary>Returns true if call is in the whitelist</summary>
		public bool IsInWhitelist( string test ) => this.Whitelist.Any<Regex>( (Func<Regex, bool>)(x => x.IsMatch( test )) );
	}
}
