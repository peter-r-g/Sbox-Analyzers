# S&box AccessList Analyzer
A Roslyn analyzer that checks if you are using any code that S&box does not allow.

![Analyzer showcase](https://user-images.githubusercontent.com/11802285/236940698-d990d019-7d09-4df8-9c4c-39bf5a24e7ff.gif)

## Installation
You can get the analyzer on [NuGet](https://www.nuget.org/packages/SboxAccessListAnalyzer) and [GitHub](https://github.com/peter-r-g/Sbox-AccessList-Analyzer/releases).

If installing through NuGet, you can add the following to your csproj file:
```csproj
<PackageReference Include="SboxAccessListAnalyzer" Version="1.0.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

If installing through the DLL, you can add the following to your csproj file:
```csproj
<Analyzer Include="path\to\the\analyzer.dll" />
```

## License
Distributed under the MIT License. See the [license](https://github.com/peter-r-g/Sbox-AccessList-Analyzer/blob/master/LICENSE.md) for more information.

## Contributing
Contributions are very much welcomed! When making your pull request please make sure your code is neatly formatted and passes all tests.

## Known Issues
* There are bound to be false positives that come up. If you experience one, please submit an [issue](https://github.com/peter-r-g/Sbox-AccessList-Analyzer/issues) so that it can be looked into!
