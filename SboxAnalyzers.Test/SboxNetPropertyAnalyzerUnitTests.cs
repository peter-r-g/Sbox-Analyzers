using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using VerifyCS = SboxAnalyzers.Test.CSharpAnalyzerVerifier<SboxAnalyzers.SboxNetPropertyAnalyzer>;

namespace SboxAnalyzers.Test;

[TestClass]
public class SboxNetPropertyAnalyzerUnitTest
{
	[TestMethod]
	public async Task Error_AutoProperties()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public class Test
			{
				[Net] public int Test1 {|#0:{ get; }|}
				[Net] public int Test2
				{|#1:{
					get => backingField;
					set => backingField = value;
				}|}

				private int backingField;
			}";

		var expected1 = VerifyCS.Diagnostic( Diagnostics.NetProperty.AutoPropertyDiagnosticId )
			.WithLocation( 0 );
		var expected2 = VerifyCS.Diagnostic( Diagnostics.NetProperty.AutoPropertyDiagnosticId )
			.WithLocation( 1 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected1, expected2 );
	}

	[TestMethod]
	public async Task Error_ChangeMethodParameterCountMismatch()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class ChangeAttribute : Attribute
			{
				public string MethodName { get; }
	
				public ChangeAttribute()
				{
				}
	
				public ChangeAttribute( string methodName )
				{
					MethodName = methodName;
				}
			}

			public class Test
			{
				[Change] public int Test1 { get; set; }
				[Change] public int Test2 { get; set; }

				private void {|#0:OnTest1Changed|}()
				{
				}

				private void {|#1:OnTest2Changed|}( int oldValue )
				{
				}
			}";

		var expected1 = VerifyCS.Diagnostic( Diagnostics.NetProperty.ChangeParameterCountDiagnosticId )
			.WithLocation( 0 )
			.WithArguments( 0 );
		var expected2 = VerifyCS.Diagnostic( Diagnostics.NetProperty.ChangeParameterCountDiagnosticId )
			.WithLocation( 1 )
			.WithArguments( 1 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected1, expected2 );
	}

	[TestMethod]
	public async Task Error_ChangeMethodParameterTypeMismatch()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class ChangeAttribute : Attribute
			{
				public string MethodName { get; }
	
				public ChangeAttribute()
				{
				}
	
				public ChangeAttribute( string methodName )
				{
					MethodName = methodName;
				}
			}

			public class Test
			{
				[Change] public int Test1 { get; set; }

				private void OnTest1Changed( {|#0:short|} oldValue, int newValue )
				{
				}
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.NetProperty.ChangeParameterTypeDiagnosticId )
			.WithLocation( 0 )
			.WithArguments( "short" );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}

	[TestMethod]
	public async Task Error_MissingChangeMethod()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class ChangeAttribute : Attribute
			{
				public string MethodName { get; }
	
				public ChangeAttribute()
				{
				}
	
				public ChangeAttribute( string methodName )
				{
					MethodName = methodName;
				}
			}

			public class Test
			{
				[{|#0:Change|}] public object Test1 { get; set; }
				[{|#1:Change( ""OnSomeObjectChanged"" )|}] public object Test2 { get; set; }
			}";

		var expected1 = VerifyCS.Diagnostic( Diagnostics.NetProperty.ChangeMissingDiagnosticId )
			.WithLocation( 0 )
			.WithArguments( "OnTest1Changed" );
		var expected2 = VerifyCS.Diagnostic( Diagnostics.NetProperty.ChangeMissingDiagnosticId )
			.WithLocation( 1 )
			.WithArguments( "OnSomeObjectChanged" );
		await VerifyCS.VerifyAnalyzerAsync( test, expected1, expected2 );
	}

	[TestMethod]
	public async Task Error_NetworkStaticProperty()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public class Test
			{
				[Net] public {|#0:static|} int Test1 { get; set; }
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.NetProperty.StaticDiagnosticId )
			.WithLocation( 0 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}

	[TestMethod]
	public async Task Error_UnNetworkableCollection()
	{
		var test = @"
			using System;
			using System.Collections.Generic;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public struct UnNetworkableType
			{
				public int Test1;
				public object Test2;
			}

			public class Test
			{
				[Net] public {|#0:IList<object>|} Test1 { get; set; }
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.NetProperty.NetworkableDiagnosticId )
			.WithLocation( 0 )
			.WithArguments( "System.Collections.Generic.IList<System.Object>" );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}

	[TestMethod]
	public async Task Error_UnNetworkableTypes()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public struct UnNetworkableType
			{
				public int Test1;
				public object Test2;
			}

			public class Test
			{
				[Net] public {|#0:object|} Test1 { get; set; }
				[Net] public {|#1:UnNetworkableType|} Test2 { get; set; }
			}";

		var expected1 = VerifyCS.Diagnostic( Diagnostics.NetProperty.NetworkableDiagnosticId )
			.WithLocation( 0 )
			.WithArguments( "System.Object" );
		var expected2 = VerifyCS.Diagnostic( Diagnostics.NetProperty.NetworkableDiagnosticId )
			.WithLocation( 1 )
			.WithArguments( "UnNetworkableType" );
		await VerifyCS.VerifyAnalyzerAsync( test, expected1, expected2 );
	}

	[TestMethod]
	public async Task Pass_AutoProperty()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public class Test
			{
				[Net] public int Test1 { get; set; }
			}";

		await VerifyCS.VerifyAnalyzerAsync( test );
	}

	[TestMethod]
	public async Task Pass_ChangeMethod()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class ChangeAttribute : Attribute
			{
				public string MethodName { get; }
	
				public ChangeAttribute()
				{
				}
	
				public ChangeAttribute( string methodName )
				{
					MethodName = methodName;
				}
			}

			public class Test
			{
				[Change] public object Test1 { get; set; }
				[Change( ""OnSomeObjectChanged"" )] public object Test2 { get; set; }

				private void OnTest1Changed( object oldObj, object newObj )
				{
				}

				private void OnSomeObjectChanged( object oldObj, object newObj )
				{
				}
			}";

		await VerifyCS.VerifyAnalyzerAsync( test );
	}

	[TestMethod]
	public async Task Pass_NetworkableStruct()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public struct NetworkableType
			{
				public int Test1;
				public short Test2;
			}

			public class Test
			{
				[Net] public NetworkableType Test1 { get; set; }
			}";

		await VerifyCS.VerifyAnalyzerAsync( test );
	}

	[TestMethod]
	public async Task Pass_NetworkableCollections()
	{
		var test = @"
			using System;
			using System.Collections.Generic;

			[AttributeUsage( AttributeTargets.Property )]
			public class NetAttribute : Attribute
			{
			}

			public class Test
			{
				[Net] public IList<int> Test1 { get; set; }
				[Net] public IDictionary<string, int> Test2 { get; set; }
			}";

		await VerifyCS.VerifyAnalyzerAsync( test );
	}

	[TestMethod]
	public async Task Warn_LocalAttribute()
	{
		var test = @"
			using System;

			[AttributeUsage( AttributeTargets.Property )]
			public class LocalAttribute : Attribute
			{
			}

			public class Test
			{
				[{|#0:Local|}] public object TestObject { get; set; }
			}";

		var expected = VerifyCS.Diagnostic( Diagnostics.NetProperty.LocalDiagnosticId )
			.WithLocation( 0 );
		await VerifyCS.VerifyAnalyzerAsync( test, expected );
	}
}
