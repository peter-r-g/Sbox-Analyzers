using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = SboxAnalyzers.Test.CSharpAnalyzerVerifier<SboxAnalyzers.ServerCmdAnalyzer>;

namespace SboxAnalyzers.Test;

[TestClass]
public class ServerCmdAnalyzerUnitTest
{
	[TestMethod]
	public async Task Error_ServerCommandParameter()
	{
		var test = @"
			using System;

			public static class ConCmd
			{
				[AttributeUsage( AttributeTargets.Method )]
				public class ServerAttribute : Attribute
				{
				}
			}

			public class Test
			{
				[ConCmd.Server]
				public void Test1( {|#0:object|} obj )
				{
				}
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.ServerCmd.UnsupportedId )
			.WithLocation( 0 )
			.WithArguments( "System.Object" );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}
}
