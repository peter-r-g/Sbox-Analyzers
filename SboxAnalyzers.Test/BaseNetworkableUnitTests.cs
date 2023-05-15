using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = SboxAnalyzers.Test.CSharpAnalyzerVerifier<SboxAnalyzers.Analyzers.BaseNetworkableAnalyzer>;

namespace SboxAnalyzers.Test;

[TestClass]
public class BaseNetworkableAnalyzerUnitTests
{
	[TestMethod]
	public async Task Error_NoParameterlessConstructor()
	{
		var test = @"
			public class BaseNetworkable
			{
			}

			public class {|#0:Entity|} : BaseNetworkable
			{
				public Entity( int parameter )
				{
				}
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.BaseNetworkables.NoParameterlessConstructorRule )
			.WithLocation( 0 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}
}
