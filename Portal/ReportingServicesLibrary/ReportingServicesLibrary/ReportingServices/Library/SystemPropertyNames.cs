using System;
using System.Collections;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000080 RID: 128
	internal static class SystemPropertyNames
	{
		// Token: 0x0400028B RID: 651
		internal static readonly string[] SystemNonNullablePropertyNames = new string[]
		{
			"SiteName", "EnableMyReports", "MyReportsRole", "UseSessionCookies", "SessionTimeout", "SystemReportTimeout", "EnableExecutionLogging", "ExecutionLogDaysKept", "ExecutionLogLevel", "SystemSnapshotLimit",
			"EnableClientPrinting", "RDLXReportTimeout"
		};

		// Token: 0x0400028C RID: 652
		internal static readonly string[] SystemNonPermissionedPropertyNames = new string[]
		{
			"SiteName", "EnableClientPrinting", "EnableIntegratedSecurity", "SharePointIntegrated", "ResourceUrl", "AuthorizationUrl", "PowerBIEndpoint", "RedirectUrls", "LogoutUrl", "TokenUrl",
			"ClientId", "ClientSecret", "AppObjectId", "TenantName", "TenantId", "MaxFileSizeMb", "OAuthClientId", "OAuthClientSecret", "OAuthTenant", "OAuthTokenUrl",
			"OAuthAuthorizationUrl", "OAuthFederationMetadataUrl", "OAuthResourceUrl", "OAuthNativeClientId", "OAuthGraphUrl", "OAuthSessionCookieName", "OAuthLogoutUrl", "RequireIntune", "SystemReportTimeout", "SystemSnapshotLimit",
			"OfficeOnlineDiscoveryUrl", "ExcelWopiClientUrl", "ShowDownloadMenu", "CustomUrlLabel", "CustomUrlValue", "TileViewByDefault", "EnablePowerBIReportMigrate", "PowerBIMigrateUrl", "PowerBIMigrateCountLimit", "EnableListHistorySnapshotsSize"
		};

		// Token: 0x0400028D RID: 653
		internal static readonly string[] SystemReadOnlyPropertyNames = new string[]
		{
			"SharePointIntegrated", "ResourceUrl", "AuthorizationUrl", "PowerBIEndpoint", "RedirectUrls", "LogoutUrl", "TokenUrl", "ClientId", "ClientSecret", "AppObjectId",
			"TenantName", "TenantId"
		};

		// Token: 0x0400028E RID: 654
		private static readonly string[] SystemNativeModeNotSupportedPropertyNames = new string[] { "RDLXReportTimeout" };

		// Token: 0x0400028F RID: 655
		internal static readonly string[] SystemSharePointModeNotSupportedPropertyNames = new string[]
		{
			"SiteName", "EnableMyReports", "MyReportsRole", "ResourceUrl", "AuthorizationUrl", "PowerBIEndpoint", "RedirectUrls", "LogoutUrl", "TokenUrl", "ClientId",
			"ClientSecret", "AppObjectId", "TenantName", "TenantId", "OAuthClientId", "OAuthClientSecret", "OAuthTenant", "OAuthTokenUrl", "OAuthAuthorizationUrl", "OAuthFederationMetadataUrl",
			"OAuthGraphUrl", "OAuthSessionCookieName", "OAuthLogoutUrl"
		};

		// Token: 0x04000290 RID: 656
		internal static readonly Hashtable SystemNonNullableProperties = PropertyHashTable.Create(SystemPropertyNames.SystemNonNullablePropertyNames);

		// Token: 0x04000291 RID: 657
		internal static readonly Hashtable SystemNonPermissionedProperties = PropertyHashTable.Create(SystemPropertyNames.SystemNonPermissionedPropertyNames);

		// Token: 0x04000292 RID: 658
		internal static readonly Hashtable SystemReadOnlyProperties = PropertyHashTable.Create(SystemPropertyNames.SystemReadOnlyPropertyNames);

		// Token: 0x04000293 RID: 659
		internal static readonly Hashtable SystemNativeModeNotSupportedProperties = PropertyHashTable.Create(SystemPropertyNames.SystemNativeModeNotSupportedPropertyNames);

		// Token: 0x04000294 RID: 660
		internal static readonly Hashtable SystemSharePointModeNotSupportedProperties = PropertyHashTable.Create(SystemPropertyNames.SystemSharePointModeNotSupportedPropertyNames);

		// Token: 0x04000295 RID: 661
		internal const string SiteNameProperty = "SiteName";

		// Token: 0x04000296 RID: 662
		internal const string SnapshotCompressionProperty = "SnapshotCompression";

		// Token: 0x04000297 RID: 663
		internal const string ChunkSegmentingProperty = "EnableChunkSegmenting";

		// Token: 0x04000298 RID: 664
		internal const string ChunkSegmentSizeProperty = "ChunkSegmentSize";

		// Token: 0x04000299 RID: 665
		internal const string EnableMyReportsProperty = "EnableMyReports";

		// Token: 0x0400029A RID: 666
		internal const string MyReportsRoleProperty = "MyReportsRole";

		// Token: 0x0400029B RID: 667
		internal const string UseSessionCookiesProperty = "UseSessionCookies";

		// Token: 0x0400029C RID: 668
		internal const string SessionTimeoutProperty = "SessionTimeout";

		// Token: 0x0400029D RID: 669
		internal const string SessionAccessTimeoutProperty = "SessionAccessTimeout";

		// Token: 0x0400029E RID: 670
		internal const string SystemReportTimeoutProperty = "SystemReportTimeout";

		// Token: 0x0400029F RID: 671
		internal const string EnableExecutionLoggingProperty = "EnableExecutionLogging";

		// Token: 0x040002A0 RID: 672
		internal const string ExecutionLogDaysKeptProperty = "ExecutionLogDaysKept";

		// Token: 0x040002A1 RID: 673
		internal const string ExecutionLogLevelProperty = "ExecutionLogLevel";

		// Token: 0x040002A2 RID: 674
		internal const string SystemSnapshotLimitProperty = "SystemSnapshotLimit";

		// Token: 0x040002A3 RID: 675
		internal const string ResponseBufferSizeKbProperty = "ResponseBufferSizeKb";

		// Token: 0x040002A4 RID: 676
		internal const string SqlStreamingBufferSizeProperty = "SqlStreamingBufferSize";

		// Token: 0x040002A5 RID: 677
		internal const string EnableIntegratedSecurity = "EnableIntegratedSecurity";

		// Token: 0x040002A6 RID: 678
		internal const string ExternalImagesTimeout = "ExternalImagesTimeout";

		// Token: 0x040002A7 RID: 679
		internal const string EnableClientPrintingProperty = "EnableClientPrinting";

		// Token: 0x040002A8 RID: 680
		internal const string StoredParametersThresholdProperty = "StoredParametersThreshold";

		// Token: 0x040002A9 RID: 681
		internal const string StoredParametersLifetimeProperty = "StoredParametersLifetime";

		// Token: 0x040002AA RID: 682
		internal const string EnableRemoteErrorsProperty = "EnableRemoteErrors";

		// Token: 0x040002AB RID: 683
		internal const string SharePointIntegratedProperty = "SharePointIntegrated";

		// Token: 0x040002AC RID: 684
		internal const string EnableLoadReportDefinition = "EnableLoadReportDefinition";

		// Token: 0x040002AD RID: 685
		internal const string EnableTestConnectionDetailedErrorsProperty = "EnableTestConnectionDetailedErrors";

		// Token: 0x040002AE RID: 686
		internal const string EditSessionTimeoutProperty = "EditSessionTimeout";

		// Token: 0x040002AF RID: 687
		internal const string EditSessionCacheLimitProperty = "EditSessionCacheLimit";

		// Token: 0x040002B0 RID: 688
		internal const string OfficeOnlineDiscoveryUrlProperty = "OfficeOnlineDiscoveryUrl";

		// Token: 0x040002B1 RID: 689
		internal const string ExcelWopiClientUrl = "ExcelWopiClientUrl";

		// Token: 0x040002B2 RID: 690
		internal const string RdlxReportTimeoutProperty = "RDLXReportTimeout";

		// Token: 0x040002B3 RID: 691
		internal const string MaxFileSizeMb = "MaxFileSizeMb";

		// Token: 0x040002B4 RID: 692
		internal const string RequireIntune = "RequireIntune";

		// Token: 0x040002B5 RID: 693
		internal const string EnableCustomVisuals = "EnableCustomVisuals";

		// Token: 0x040002B6 RID: 694
		internal const string TrustedFileFormat = "TrustedFileFormat";

		// Token: 0x040002B7 RID: 695
		internal const string EnablePowerBIReportExportData = "EnablePowerBIReportExportData";

		// Token: 0x040002B8 RID: 696
		internal const string EnablePowerBIReportExportUnderlyingData = "EnablePowerBIReportExportUnderlyingData";

		// Token: 0x040002B9 RID: 697
		internal const string ShowDownloadMenu = "ShowDownloadMenu";

		// Token: 0x040002BA RID: 698
		internal const string AllowedResourceExtensionsForUpload = "AllowedResourceExtensionsForUpload";

		// Token: 0x040002BB RID: 699
		internal const string RestrictedResourceMimeTypeForUpload = "RestrictedResourceMimeTypeForUpload";

		// Token: 0x040002BC RID: 700
		internal const string CustomHeaders = "CustomHeaders";

		// Token: 0x040002BD RID: 701
		internal const string CustomUrlLabel = "CustomUrlLabel";

		// Token: 0x040002BE RID: 702
		internal const string CustomUrlValue = "CustomUrlValue";

		// Token: 0x040002BF RID: 703
		internal const string TileViewByDefault = "TileViewByDefault";

		// Token: 0x040002C0 RID: 704
		internal const string EnablePowerBIReportMigrate = "EnablePowerBIReportMigrate";

		// Token: 0x040002C1 RID: 705
		internal const string PowerBIMigrateUrl = "PowerBIMigrateUrl";

		// Token: 0x040002C2 RID: 706
		internal const string PowerBIMigrateCountLimit = "PowerBIMigrateCountLimit";

		// Token: 0x040002C3 RID: 707
		internal const string EnableListHistorySnapshotsSize = "EnableListHistorySnapshotsSize";

		// Token: 0x040002C4 RID: 708
		internal const string CleanupBatchSize = "CleanupBatchSize";

		// Token: 0x040002C5 RID: 709
		internal const string CleanupMaxLimit = "CleanupMaxLimit";

		// Token: 0x040002C6 RID: 710
		internal const string ResourceUrl = "ResourceUrl";

		// Token: 0x040002C7 RID: 711
		internal const string AuthorizationUrl = "AuthorizationUrl";

		// Token: 0x040002C8 RID: 712
		internal const string PowerBIEndpoint = "PowerBIEndpoint";

		// Token: 0x040002C9 RID: 713
		internal const string RedirectUrls = "RedirectUrls";

		// Token: 0x040002CA RID: 714
		internal const string LogoutUrl = "LogoutUrl";

		// Token: 0x040002CB RID: 715
		internal const string TokenUrl = "TokenUrl";

		// Token: 0x040002CC RID: 716
		internal const string ClientId = "ClientId";

		// Token: 0x040002CD RID: 717
		internal const string ClientSecret = "ClientSecret";

		// Token: 0x040002CE RID: 718
		internal const string AppObjectId = "AppObjectId";

		// Token: 0x040002CF RID: 719
		internal const string TenantName = "TenantName";

		// Token: 0x040002D0 RID: 720
		internal const string TenantId = "TenantId";

		// Token: 0x040002D1 RID: 721
		internal const string OAuthClientId = "OAuthClientId";

		// Token: 0x040002D2 RID: 722
		internal const string OAuthClientSecret = "OAuthClientSecret";

		// Token: 0x040002D3 RID: 723
		internal const string OAuthTenant = "OAuthTenant";

		// Token: 0x040002D4 RID: 724
		internal const string OAuthTokenUrl = "OAuthTokenUrl";

		// Token: 0x040002D5 RID: 725
		internal const string OAuthAuthorizationUrl = "OAuthAuthorizationUrl";

		// Token: 0x040002D6 RID: 726
		internal const string OAuthFederationMetadataUrl = "OAuthFederationMetadataUrl";

		// Token: 0x040002D7 RID: 727
		internal const string OAuthResourceUrl = "OAuthResourceUrl";

		// Token: 0x040002D8 RID: 728
		internal const string OAuthNativeClientId = "OAuthNativeClientId";

		// Token: 0x040002D9 RID: 729
		internal const string OAuthGraphUrl = "OAuthGraphUrl";

		// Token: 0x040002DA RID: 730
		internal const string OAuthSessionCookieName = "OAuthSessionCookieName";

		// Token: 0x040002DB RID: 731
		internal const string OAuthLogoutUrl = "OAuthLogoutUrl";

		// Token: 0x040002DC RID: 732
		internal const string AccessControlAllowCredentials = "AccessControlAllowCredentials";

		// Token: 0x040002DD RID: 733
		internal const string AccessControlAllowHeaders = "AccessControlAllowHeaders";

		// Token: 0x040002DE RID: 734
		internal const string AccessControlAllowMethods = "AccessControlAllowMethods";

		// Token: 0x040002DF RID: 735
		internal const string AccessControlAllowOrigin = "AccessControlAllowOrigin";

		// Token: 0x040002E0 RID: 736
		internal const string AccessControlExposeHeaders = "AccessControlExposeHeaders";

		// Token: 0x040002E1 RID: 737
		internal const string AccessControlMaxAge = "AccessControlMaxAge";
	}
}
