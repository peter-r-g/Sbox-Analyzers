; Shipped analyzer releases
; https://github.com/dotnet/roslyn-analyzers/blob/main/src/Microsoft.CodeAnalysis.Analyzers/ReleaseTrackingAnalyzers.Help.md

## Release 3.0.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
SB9009 | Events | Error | Incorrect event listener parameter count.
SB9010 | Events | Error | Incorrect event listener parameter type.
SB9011 | Events | Warning | Listener uses event with no MethodArgumentsAttribute.
SB9012 | Commands | Error | Server command parameter type is unsupported.
SB9013 | BaseNetworkables | Error | BaseNetworkable derived type has no parameterless constructor.

## Release 2.0.0

### New Rules

Rule ID | Category | Severity | Notes
--------|----------|----------|-------
SB9001 | Code Access | Error | Accesslist violation.
SB9002 | Networking | Warning | Local attribute usage.
SB9003 | Networking  | Error | Change method callback missing.
SB9004 | Networking | Error | Change method parameter count mismatch.
SB9005 | Networking | Error | Change method parameter type mismatch.
SB9006 | Networking | Error | Static networked property.
SB9007 | Networking | Error | Property type not networkable.
SB9008 | Networking | Error | Property not auto-implemented.