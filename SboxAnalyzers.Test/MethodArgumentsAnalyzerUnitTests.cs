using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = SboxAnalyzers.Test.CSharpAnalyzerVerifier<SboxAnalyzers.Analyzers.MethodArgumentsAnalyzer>;

namespace SboxAnalyzers.Test;

[TestClass]
public class MethodArgumentsAnalyzerUnitTest
{
	[TestMethod]
	public async Task Error_ParameterCountMismatch()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Method )]
			public class EventAttribute : Attribute
			{
				public string EventName { get; }

				public EventAttribute( string eventName )
				{
					EventName = eventName;
				}
			}

			[AttributeUsage( AttributeTargets.Class )]
			public class MethodArgumentsAttribute : Attribute
			{
				public Type[] ArgumentTypes { get; }

				public MethodArgumentsAttribute( params Type[] argumentTypes )
				{
					ArgumentTypes = argumentTypes;
				}
			}

			[MethodArguments( typeof(int), typeof(object) )]
			public class TestEventAttribute : EventAttribute
			{
				public TestEventAttribute() : base( ""test.event"" )
				{
				}
			}

			public class Test
			{
				[TestEventAttribute]
				public void {|#0:TestMethod1|}( int parameter1 )
				{
				}
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.MethodArguments.ListenerParameterCountMismatchRule )
			.WithLocation( 0 )
			.WithArguments( 2 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}

	[TestMethod]
	public async Task Error_ParameterTypeMismatch()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Method )]
			public class EventAttribute : Attribute
			{
				public string EventName { get; }

				public EventAttribute( string eventName )
				{
					EventName = eventName;
				}
			}

			[AttributeUsage( AttributeTargets.Class )]
			public class MethodArgumentsAttribute : Attribute
			{
				public Type[] ArgumentTypes { get; }

				public MethodArgumentsAttribute( params Type[] argumentTypes )
				{
					ArgumentTypes = argumentTypes;
				}
			}

			[MethodArguments( typeof(int) )]
			public class TestEventAttribute : EventAttribute
			{
				public TestEventAttribute() : base( ""test.event"" )
				{
				}
			}

			public class Test
			{
				[TestEventAttribute]
				public void TestMethod1( {|#0:short|} parameter1 )
				{
				}
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.MethodArguments.ListenerParameterTypeMismatchRule )
			.WithLocation( 0 )
			.WithArguments( "System.Int32" );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}

	[TestMethod]
	public async Task Pass_BasicEvent()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Method )]
			public class EventAttribute : Attribute
			{
				public string EventName { get; }

				public EventAttribute( string eventName )
				{
					EventName = eventName;
				}
			}

			public class Test
			{
				[Event( ""test.event"" )]
				public void TestMethod1()
				{
				}
			}";

		await VerifyCS.VerifyAnalyzerAsync( test );
	}

	[TestMethod]
	public async Task Warn_ParameteredEventNoMethodArguments()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Method )]
			public class EventAttribute : Attribute
			{
				public string EventName { get; }

				public EventAttribute( string eventName )
				{
					EventName = eventName;
				}
			}

			public class Test
			{
				[Event( ""test.event"" )]
				public void TestMethod1{|#0:( int parameter1, short parameter2 )|}
				{
				}
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.MethodArguments.ListenerMethodArgumentsUndefinedRule )
			.WithLocation( 0 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}
}
