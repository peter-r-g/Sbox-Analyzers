; Unshipped analyzer release
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 3.0.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
SB9009 | Events | Error | Incorrect event listener parameter count.
SB9010 | Events | Error | Incorrect event listener parameter type.
SB9011 | Events | Warning | Listener uses event with no MethodArgumentsAttribute.
SB9012 | Commands | Error | Server command parameter type is unsupported.