using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using System;

namespace SboxAnalyzers.Test;

public static partial class CSharpAnalyzerVerifier<TAnalyzer>
	where TAnalyzer : DiagnosticAnalyzer, new()
{
	public class Test : CSharpAnalyzerTest<TAnalyzer, MSTestVerifier>
	{
		public Test()
		{
			SolutionTransforms.Add( ( solution, projectId ) =>
			{
				var compilationOptions = solution.GetProject( projectId )?.CompilationOptions;
				if ( compilationOptions is null )
					throw new NullReferenceException( "Unreachable: Test project is null" );

				compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
					compilationOptions.SpecificDiagnosticOptions.SetItems( CSharpVerifierHelper.NullableWarnings ) );
				solution = solution.WithProjectCompilationOptions( projectId, compilationOptions );

				return solution;
			} );
		}
	}
}
