using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = SboxAccessListAnalyzer.Test.CSharpAnalyzerVerifier<SboxAccessListAnalyzer.SboxAccessListAnalyzer>;

namespace SboxAccessListAnalyzer.Test
{
	[TestClass]
	public class SboxAccessListAnalyzerUnitTest
	{
		[TestMethod]
		public async Task WhitelistNamespace()
		{
			var test = "using System.Collections.Immutable;";
			await VerifyCS.VerifyAnalyzerAsync( test );
		}

		[TestMethod]
		public async Task WhitelistType()
		{
			var test = @"
			public class Test
			{
				private readonly object TestObject;
			}";

			await VerifyCS.VerifyAnalyzerAsync( test );
		}

		[TestMethod]
		public async Task WhitelistUri()
		{
			var test = @"
			using System;

			public class Test
			{
				private readonly Uri TestUri;
			}";

			await VerifyCS.VerifyAnalyzerAsync( test );
		}

		[TestMethod]
		public async Task WhitelistEquality()
		{
			var test = @"
			using System.Reflection;

			public class Test
			{
				private bool TestMethod()
				{
					return typeof( object ) == typeof( object );
				}
			}";

			await VerifyCS.VerifyAnalyzerAsync( test );
		}

		[TestMethod]
		public async Task UnwhitelistType()
		{
			var test = @"
			using System.Reflection;

			public class Test
			{
				private readonly {|#0:Assembly|} Assembly;
			}";

			var expected = VerifyCS.Diagnostic( SboxAccessListAnalyzer.DiagnosticId )
				.WithLocation( 0 )
				.WithArguments( "System.Reflection.Assembly" );
			await VerifyCS.VerifyAnalyzerAsync( test, expected );
		}

		[TestMethod]
		public async Task UnwhitelistQualifiedMethod()
		{
			var test = @"
			using System.Reflection;

			public class Test
			{
				private readonly object TestObject = Assembly.GetExecutingAssembly();
			}";

			var expected = VerifyCS.Diagnostic( SboxAccessListAnalyzer.DiagnosticId )
				.WithLocation( 6, 42 )
				.WithArguments( "System.Reflection.Assembly.GetExecutingAssembly()" );
			await VerifyCS.VerifyAnalyzerAsync( test, expected );
		}

		[TestMethod]
		public async Task UnwhitelistPropertyGetter()
		{
			var test = @"
			using System;

			public class Test
			{
				public void TestMethod()
				{
					var type = typeof( object );
					var baseType = type.BaseType;
				}
			}";

			var expected = VerifyCS.Diagnostic( SboxAccessListAnalyzer.DiagnosticId )
				.WithLocation( 9, 21 )
				.WithArguments( "System.Type.get_BaseType()" );
			await VerifyCS.VerifyAnalyzerAsync( test, expected );
		}

		[TestMethod]
		public async Task UnwhitelistPropertySetter()
		{
			var test = @"
			using System;
			
			public class Test
			{
				public void TestMethod()
				{
					Environment.CurrentDirectory = ""Test"";
				}
			}";

			var expected = VerifyCS.Diagnostic( SboxAccessListAnalyzer.DiagnosticId )
				.WithLocation( 8, 6 )
				.WithArguments( "System.Environment.set_CurrentDirectory()" );
			await VerifyCS.VerifyAnalyzerAsync( test, expected );
		}
	}
}
