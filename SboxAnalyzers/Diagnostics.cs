using Microsoft.CodeAnalysis;
using SboxAnalyzers.Resources;
using System.Collections.Immutable;

namespace SboxAnalyzers;

/// <summary>
/// Contains information for all diagnostics the analyzers check.
/// </summary>
public static class Diagnostics
{
	/// <summary>
	/// Contains information for analyzers relating to the S&box code accesslist.
	/// </summary>
	public static class AccessList
	{
		/// <summary>
		/// The unique ID for the diagnostic message created by this analyzer.
		/// </summary>
		public const string Id = "SB9001";
		/// <summary>
		/// The category that this diagnostic falls under.
		/// </summary>
		private const string Category = "Code Access";

		private static readonly LocalizableString Title = new LocalizableResourceString( nameof( AccessListResources.AnalyzerTitle ), AccessListResources.ResourceManager, typeof( AccessListResources ) );
		private static readonly LocalizableString MessageFormat = new LocalizableResourceString( nameof( AccessListResources.AnalyzerMessageFormat ), AccessListResources.ResourceManager, typeof( AccessListResources ) );
		private static readonly LocalizableString Description = new LocalizableResourceString( nameof( AccessListResources.AnalyzerDescription ), AccessListResources.ResourceManager, typeof( AccessListResources ) );
		internal static readonly DiagnosticDescriptor Rule = new(
			Id,
			Title,
			MessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: Description );

		internal static readonly ImmutableArray<DiagnosticDescriptor> AllRules = ImmutableArray.Create( Rule );
	}

	/// <summary>
	/// Contains information for analyzers relating to event method arguments.
	/// </summary>
	public static class MethodArguments
	{
		/// <summary>
		/// An error diagnostic to notify that an event listeners parameter count is incorrect.
		/// </summary>
		public const string ListenerParameterCountMismatchId = "SB9009";
		/// <summary>
		/// An error diagnostic to notify that an event listeners parameter type is incorrect.
		/// </summary>
		public const string ListenerParameterTypeMismatchId = "SB9010";
		/// <summary>
		/// A warning diagnostic to notify that an event listener has parameters but the event has no MethodArgumentsAttribute to compare against.
		/// </summary>
		public const string ListenerMethodArgumentsUndefinedId = "SB9011";
		/// <summary>
		/// The category that this diagnostic falls under.
		/// </summary>
		private const string Category = "Events";

		private static readonly LocalizableString ListenerParameterCountMismatchTitle = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerParameterCountMismatchTitle ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		private static readonly LocalizableString ListenerParameterCountMismatchMessageFormat = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerParameterCountMismatchMessageFormat ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		private static readonly LocalizableString ListenerParameterCountMismatchDescription = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerParameterCountMismatchDescription ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		public static readonly DiagnosticDescriptor ListenerParameterCountMismatchRule = new(
			ListenerParameterCountMismatchId,
			ListenerParameterCountMismatchTitle,
			ListenerParameterCountMismatchMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: ListenerParameterCountMismatchDescription );

		private static readonly LocalizableString ListenerParameterTypeMismatchTitle = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerParameterTypeMismatchTitle ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		private static readonly LocalizableString ListenerParameterTypeMismatchMessageFormat = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerParameterTypeMismatchMessageFormat ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		private static readonly LocalizableString ListenerParameterTypeMismatchDescription = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerParameterTypeMismatchDescription ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		public static readonly DiagnosticDescriptor ListenerParameterTypeMismatchRule = new(
			ListenerParameterTypeMismatchId,
			ListenerParameterTypeMismatchTitle,
			ListenerParameterTypeMismatchMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: ListenerParameterTypeMismatchDescription );

		private static readonly LocalizableString ListenerMethodArgumentsUndefinedTitle = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerMethodArgumentsUndefinedTitle ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		private static readonly LocalizableString ListenerMethodArgumentsUndefinedMessageFormat = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerMethodArgumentsUndefinedMessageFormat ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		private static readonly LocalizableString ListenerMethodArgumentsUndefinedDescription = new LocalizableResourceString( nameof( MethodArgumentsResources.ListenerMethodArgumentsUndefinedDescription ), MethodArgumentsResources.ResourceManager, typeof( MethodArgumentsResources ) );
		public static readonly DiagnosticDescriptor ListenerMethodArgumentsUndefinedRule = new(
			ListenerMethodArgumentsUndefinedId,
			ListenerMethodArgumentsUndefinedTitle,
			ListenerMethodArgumentsUndefinedMessageFormat,
			Category,
			DiagnosticSeverity.Warning,
			isEnabledByDefault: true,
			description: ListenerMethodArgumentsUndefinedDescription );

		internal static readonly ImmutableArray<DiagnosticDescriptor> AllRules = ImmutableArray.Create(
			ListenerParameterCountMismatchRule,
			ListenerParameterTypeMismatchRule,
			ListenerMethodArgumentsUndefinedRule );
	}

	/// <summary>
	/// Contains information for analyzers relating to networked properties.
	/// </summary>
	public static class NetProperty
	{
		/// <summary>
		/// A warning diagnostic to notify that the LocalAttribute is not implemented.
		/// </summary>
		public const string LocalAttributeUsageId = "SB9002";
		/// <summary>
		/// An error diagnostic to notify that the change method defined in a ChangeAttribute does not exist.
		/// </summary>
		public const string ChangeMethodMissingId = "SB9003";
		/// <summary>
		/// An error diagnostic to notify that the change method defined in a ChangeAttribute has an incorrect number of parameters.
		/// </summary>
		public const string ChangeMethodParameterCountMismatchId = "SB9004";
		/// <summary>
		/// An error diagnostic to notify that the change method defined in a ChangeAttribute has the incorrect types in its parameters.
		/// </summary>
		public const string ChangeMethodParameterTypeMismatchId = "SB9005";
		/// <summary>
		/// An error diagnostic to notify that a networked property cannot be static.
		/// </summary>
		public const string IsStaticId = "SB9006";
		/// <summary>
		/// An error diagnostic to notify that a networked property has a non-networkable type.
		/// </summary>
		public const string NotNetworkableId = "SB9007";
		/// <summary>
		/// An error diagnostic to notify that a networked property is not implemented as an auto-property.
		/// </summary>
		public const string NotAutoPropertyId = "SB9008";

		/// <summary>
		/// The category that all of the diagnostics fit into.
		/// </summary>
		private const string Category = "Networking";

		private static readonly LocalizableString LocalTitle = new LocalizableResourceString( nameof( NetPropertyResources.LocalTitle ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString LocalMessageFormat = new LocalizableResourceString( nameof( NetPropertyResources.LocalMessageFormat ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		private static readonly LocalizableString LocalDescription = new LocalizableResourceString( nameof( NetPropertyResources.LocalDescription ), NetPropertyResources.ResourceManager, typeof( NetPropertyResources ) );
		internal static readonly DiagnosticDescriptor LocalRule = new(
			LocalAttributeUsageId,
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
			ChangeMethodMissingId,
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
			ChangeMethodParameterCountMismatchId,
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
			ChangeMethodParameterTypeMismatchId,
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
			IsStaticId,
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
			NotNetworkableId,
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
			NotAutoPropertyId,
			AutoPropertyTitle,
			AutoPropertyMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: AutoPropertyDescription );

		internal static readonly ImmutableArray<DiagnosticDescriptor> AllRules = ImmutableArray.Create(
			LocalRule,
			ChangeMissingRule,
			ChangeParameterCountRule,
			ChangeParameterTypeRule,
			StaticRule,
			NetworkableRule,
			AutoPropertyRule );
	}

	/// <summary>
	/// Contains information for analyzers relating to server commands.
	/// </summary>
	public static class ServerCmd
	{
		/// <summary>
		/// An error diagnostic to notify that a server commands parameter is not supported.
		/// </summary>
		public const string UnsupportedId = "SB9012";
		/// <summary>
		/// The category that all of the diagnostics fit into.
		/// </summary>
		private const string Category = "Commands";

		private static readonly LocalizableString UnsupportedTitle = new LocalizableResourceString( nameof( ServerCmdResources.UnsupportedTitle ), ServerCmdResources.ResourceManager, typeof( ServerCmdResources ) );
		private static readonly LocalizableString UnsupportedMessageFormat = new LocalizableResourceString( nameof( ServerCmdResources.UnsupportedMessageFormat ), ServerCmdResources.ResourceManager, typeof( ServerCmdResources ) );
		private static readonly LocalizableString UnsupportedDescription = new LocalizableResourceString( nameof( ServerCmdResources.UnsupportedDescription ), ServerCmdResources.ResourceManager, typeof( ServerCmdResources ) );
		internal static readonly DiagnosticDescriptor UnsupportedRule = new(
			UnsupportedId,
			UnsupportedTitle,
			UnsupportedMessageFormat,
			Category,
			DiagnosticSeverity.Error,
			isEnabledByDefault: true,
			description: UnsupportedDescription );

		internal static readonly ImmutableArray<DiagnosticDescriptor> AllRules = ImmutableArray.Create(
			UnsupportedRule );
	}
}
