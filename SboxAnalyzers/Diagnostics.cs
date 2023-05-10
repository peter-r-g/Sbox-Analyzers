using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace SboxAnalyzers;

public static class Diagnostics
{
	public static class NetProperty
	{
		public const string LocalDiagnosticId = "SBNP01";
		public const string ChangeMissingDiagnosticId = "SBNP02";
		public const string ChangeParameterCountDiagnosticId = "SBNP03";
		public const string ChangeParameterTypeDiagnosticId = "SBNP04";
		public const string StaticDiagnosticId = "SBNP05";
		public const string NetworkableDiagnosticId = "SBNP06";
		public const string AutoPropertyDiagnosticId = "SBNP07";

		private const string Category = "Networking";

		private static readonly LocalizableString LocalTitle = new LocalizableResourceString( nameof( NetPropertyResources.LocalTitle ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString LocalMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.LocalMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString LocalDescription = new LocalizableResourceString( nameof( NetPropertyResources.LocalDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor LocalRule = new(
			LocalDiagnosticId,
			LocalTitle,
			LocalMessageFormat,
			Category,
			DiagnosticSeverity.Warning,
			isEnabledByDefault: true,
			description: LocalDescription );

		private static readonly LocalizableString ChangeMissingTitle = new LocalizableResourceString( nameof( NetPropertyResources.ChangeMissingDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString ChangeMissingMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.ChangeMissingMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString ChangeMissingDescription = new LocalizableResourceString( nameof( NetPropertyResources.ChangeMissingDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor ChangeMissingRule = new(
			ChangeMissingDiagnosticId,
			ChangeMissingTitle,
			ChangeMissingMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: ChangeMissingDescription );

		private static readonly LocalizableString ChangeParameterCountTitle = new LocalizableResourceString( nameof( NetPropertyResources.ChangeParameterCountDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString ChangeParameterCountMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.ChangeParameterCountMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString ChangeParameterCountDescription = new LocalizableResourceString( nameof( NetPropertyResources.ChangeParameterCountDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor ChangeParameterCountRule = new(
			ChangeParameterCountDiagnosticId,
			ChangeParameterCountTitle,
			ChangeParameterCountMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: ChangeParameterCountDescription );

		private static readonly LocalizableString ChangeParameterTypeTitle = new LocalizableResourceString( nameof( NetPropertyResources.ChangeParameterTypeDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString ChangeParameterTypeMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.ChangeParameterTypeMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString ChangeParameterTypeDescription = new LocalizableResourceString( nameof( NetPropertyResources.ChangeParameterTypeDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor ChangeParameterTypeRule = new(
			ChangeParameterTypeDiagnosticId,
			ChangeParameterTypeTitle,
			ChangeParameterTypeMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: ChangeParameterTypeDescription );

		private static readonly LocalizableString StaticTitle = new LocalizableResourceString( nameof( NetPropertyResources.StaticDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString StaticMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.StaticMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString StaticDescription = new LocalizableResourceString( nameof( NetPropertyResources.StaticDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor StaticRule = new(
			StaticDiagnosticId,
			StaticTitle,
			StaticMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: StaticDescription );

		private static readonly LocalizableString NetworkableTitle = new LocalizableResourceString( nameof( NetPropertyResources.NetworkableDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString NetworkableMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.NetworkableMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString NetworkableDescription = new LocalizableResourceString( nameof( NetPropertyResources.NetworkableDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor NetworkableRule = new(
			NetworkableDiagnosticId,
			NetworkableTitle,
			NetworkableMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: NetworkableDescription );

		private static readonly LocalizableString AutoPropertyTitle = new LocalizableResourceString( nameof( NetPropertyResources.AutoPropertyDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString AutoPropertyMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.AutoPropertyMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString AutoPropertyDescription = new LocalizableResourceString( nameof( NetPropertyResources.AutoPropertyDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor AutoPropertyRule = new(
			AutoPropertyDiagnosticId,
			AutoPropertyTitle,
			AutoPropertyMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: AutoPropertyDescription );

		internal static readonly ImmutableArray<DiagnosticDescriptor> Diagnostics = ImmutableArray.Create(
			LocalRule,
			ChangeMissingRule,
			ChangeParameterCountRule,
			ChangeParameterTypeRule,
			StaticRule,
			NetworkableRule,
			AutoPropertyRule );
	}
}
