// Decompiled with JetBrains decompiler
// Type: Sandbox.AccessRules
// Assembly: Sandbox.Access, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 22D7DDC1-E8C0-4F80-BD1E-BAC555C4B1E8
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\sbox\bin\managed\Sandbox.Access.dll
// XML documentation location: C:\Program Files (x86)\Steam\steamapps\common\sbox\bin\managed\Sandbox.Access.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace Sandbox
{
	public class AccessRules
	{
		public List<string> AssemblyWhitelist = new List<string>();
		public List<Regex> Whitelist = new List<Regex>();
		public List<Regex> Blacklist = new List<Regex>();

		private void InitAssemblyList()
		{
			this.AssemblyWhitelist.Add( "System.Private.CoreLib" );
			this.AssemblyWhitelist.Add( "Sandbox.Game" );
			this.AssemblyWhitelist.Add( "Sandbox.Engine" );
			this.AssemblyWhitelist.Add( "Sandbox.Reflection" );
			this.AssemblyWhitelist.Add( "System.Text.Json" );
			this.AssemblyWhitelist.Add( "System.Numerics.Vectors" );
			this.AssemblyWhitelist.Add( "System.ComponentModel.Annotations" );
			this.AssemblyWhitelist.Add( "System.Runtime" );
			this.AssemblyWhitelist.Add( "Sandbox.System" );
			this.AssemblyWhitelist.Add( "Sandbox.Filesystem" );
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

		public AccessRules()
		{
			this.InitAssemblyList();
			this.AddRange( (IEnumerable<string>)Rules.BaseAccess );
			this.AddRange( (IEnumerable<string>)Rules.Types );
			this.AddRange( (IEnumerable<string>)Rules.Reflection );
			this.AddRange( (IEnumerable<string>)Rules.Exceptions );
			this.AddRange( (IEnumerable<string>)Rules.Diagnostics );
			this.AddRange( (IEnumerable<string>)Rules.Async );
		}

		private void AddRange( IEnumerable<string> rules )
		{
			foreach ( string rule in rules )
				this.AddRule( rule );
		}

		private void AddRule( string line )
		{
			string str1 = line.Trim();
			int num = str1.StartsWith( "!" ) ? 1 : 0;
			if ( num != 0 )
			{
				string str2 = str1;
				str1 = str2.Substring( 1, str2.Length - 1 );
			}
			Regex regex = new Regex( "^" + Regex.Escape( str1 ).Replace( "\\*", ".*" ) + "$" );
			if ( num != 0 )
				this.Blacklist.Add( regex );
			else
				this.Whitelist.Add( regex );
		}

		/// <summary>Returns true if call is in the whitelist</summary>
		public bool IsInWhitelist( string test )
		{
			return !this.Blacklist.Any<Regex>( (Func<Regex, bool>)(x => x.IsMatch( test )) ) && this.Whitelist.Any<Regex>( (Func<Regex, bool>)(x => x.IsMatch( test )) );
		}
	}
}
