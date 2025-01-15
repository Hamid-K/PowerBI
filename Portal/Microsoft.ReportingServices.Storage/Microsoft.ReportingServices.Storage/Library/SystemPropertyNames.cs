using System;
using System.Collections;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001A RID: 26
	internal static class SystemPropertyNames
	{
		// Token: 0x040000FA RID: 250
		internal static readonly string[] SystemNonNullablePropertyNames = new string[]
		{
			"SiteName", "EnableMyReports", "MyReportsRole", "UseSessionCookies", "SessionTimeout", "SystemReportTimeout", "EnableExecutionLogging", "ExecutionLogDaysKept", "ExecutionLogLevel", "SystemSnapshotLimit",
			"EnableClientPrinting", "RDLXReportTimeout"
		};

		// Token: 0x040000FB RID: 251
		internal static readonly string[] SystemNonPermissionedPropertyNames = new string[]
		{
			"SiteName", "EnableClientPrinting", "EnableIntegratedSecurity", "SharePointIntegrated", "ResourceUrl", "AuthorizationUrl", "PowerBIEndpoint", "RedirectUrls", "LogoutUrl", "TokenUrl",
			"ClientId", "ClientSecret", "AppObjectId", "TenantName", "TenantId", "MaxFileSizeMb", "OAuthClientId", "OAuthClientSecret", "OAuthTenant", "OAuthTokenUrl",
			"OAuthAuthorizationUrl", "OAuthFederationMetadataUrl", "OAuthResourceUrl", "OAuthNativeClientId", "OAuthGraphUrl", "OAuthSessionCookieName", "OAuthLogoutUrl", "RequireIntune", "SystemReportTimeout", "SystemSnapshotLimit",
			"OfficeOnlineDiscoveryUrl", "ExcelWopiClientUrl", "ShowDownloadMenu", "CustomUrlLabel", "CustomUrlValue", "TileViewByDefault", "EnablePowerBIReportMigrate", "PowerBIMigrateUrl", "PowerBIMigrateCountLimit", "EnableListHistorySnapshotsSize"
		};

		// Token: 0x040000FC RID: 252
		internal static readonly string[] SystemReadOnlyPropertyNames = new string[]
		{
			"SharePointIntegrated", "ResourceUrl", "AuthorizationUrl", "PowerBIEndpoint", "RedirectUrls", "LogoutUrl", "TokenUrl", "ClientId", "ClientSecret", "AppObjectId",
			"TenantName", "TenantId"
		};

		// Token: 0x040000FD RID: 253
		private static readonly string[] SystemNativeModeNotSupportedPropertyNames = new string[] { "RDLXReportTimeout" };

		// Token: 0x040000FE RID: 254
		internal static readonly string[] SystemSharePointModeNotSupportedPropertyNames = new string[]
		{
			"SiteName", "EnableMyReports", "MyReportsRole", "ResourceUrl", "AuthorizationUrl", "PowerBIEndpoint", "RedirectUrls", "LogoutUrl", "TokenUrl", "ClientId",
			"ClientSecret", "AppObjectId", "TenantName", "TenantId", "OAuthClientId", "OAuthClientSecret", "OAuthTenant", "OAuthTokenUrl", "OAuthAuthorizationUrl", "OAuthFederationMetadataUrl",
			"OAuthGraphUrl", "OAuthSessionCookieName", "OAuthLogoutUrl"
		};

		// Token: 0x040000FF RID: 255
		internal static readonly Hashtable SystemNonNullableProperties = PropertyHashTable.Create(SystemPropertyNames.SystemNonNullablePropertyNames);

		// Token: 0x04000100 RID: 256
		internal static readonly Hashtable SystemNonPermissionedProperties = PropertyHashTable.Create(SystemPropertyNames.SystemNonPermissionedPropertyNames);

		// Token: 0x04000101 RID: 257
		internal static readonly Hashtable SystemReadOnlyProperties = PropertyHashTable.Create(SystemPropertyNames.SystemReadOnlyPropertyNames);

		// Token: 0x04000102 RID: 258
		internal static readonly Hashtable SystemNativeModeNotSupportedProperties = PropertyHashTable.Create(SystemPropertyNames.SystemNativeModeNotSupportedPropertyNames);

		// Token: 0x04000103 RID: 259
		internal static readonly Hashtable SystemSharePointModeNotSupportedProperties = PropertyHashTable.Create(SystemPropertyNames.SystemSharePointModeNotSupportedPropertyNames);

		// Token: 0x04000104 RID: 260
		internal const string SiteNameProperty = "SiteName";

		// Token: 0x04000105 RID: 261
		internal const string SnapshotCompressionProperty = "SnapshotCompression";

		// Token: 0x04000106 RID: 262
		internal const string ChunkSegmentingProperty = "EnableChunkSegmenting";

		// Token: 0x04000107 RID: 263
		internal const string ChunkSegmentSizeProperty = "ChunkSegmentSize";

		// Token: 0x04000108 RID: 264
		internal const string EnableMyReportsProperty = "EnableMyReports";

		// Token: 0x04000109 RID: 265
		internal const string MyReportsRoleProperty = "MyReportsRole";

		// Token: 0x0400010A RID: 266
		internal const string UseSessionCookiesProperty = "UseSessionCookies";

		// Token: 0x0400010B RID: 267
		internal const string SessionTimeoutProperty = "SessionTimeout";

		// Token: 0x0400010C RID: 268
		internal const string SessionAccessTimeoutProperty = "SessionAccessTimeout";

		// Token: 0x0400010D RID: 269
		internal const string SystemReportTimeoutProperty = "SystemReportTimeout";

		// Token: 0x0400010E RID: 270
		internal const string EnableExecutionLoggingProperty = "EnableExecutionLogging";

		// Token: 0x0400010F RID: 271
		internal const string ExecutionLogDaysKeptProperty = "ExecutionLogDaysKept";

		// Token: 0x04000110 RID: 272
		internal const string ExecutionLogLevelProperty = "ExecutionLogLevel";

		// Token: 0x04000111 RID: 273
		internal const string SystemSnapshotLimitProperty = "SystemSnapshotLimit";

		// Token: 0x04000112 RID: 274
		internal const string ResponseBufferSizeKbProperty = "ResponseBufferSizeKb";

		// Token: 0x04000113 RID: 275
		internal const string SqlStreamingBufferSizeProperty = "SqlStreamingBufferSize";

		// Token: 0x04000114 RID: 276
		internal const string EnableIntegratedSecurity = "EnableIntegratedSecurity";

		// Token: 0x04000115 RID: 277
		internal const string ExternalImagesTimeout = "ExternalImagesTimeout";

		// Token: 0x04000116 RID: 278
		internal const string EnableClientPrintingProperty = "EnableClientPrinting";

		// Token: 0x04000117 RID: 279
		internal const string StoredParametersThresholdProperty = "StoredParametersThreshold";

		// Token: 0x04000118 RID: 280
		internal const string StoredParametersLifetimeProperty = "StoredParametersLifetime";

		// Token: 0x04000119 RID: 281
		internal const string EnableRemoteErrorsProperty = "EnableRemoteErrors";

		// Token: 0x0400011A RID: 282
		internal const string SharePointIntegratedProperty = "SharePointIntegrated";

		// Token: 0x0400011B RID: 283
		internal const string EnableLoadReportDefinition = "EnableLoadReportDefinition";

		// Token: 0x0400011C RID: 284
		internal const string EnableTestConnectionDetailedErrorsProperty = "EnableTestConnectionDetailedErrors";

		// Token: 0x0400011D RID: 285
		internal const string EditSessionTimeoutProperty = "EditSessionTimeout";

		// Token: 0x0400011E RID: 286
		internal const string EditSessionCacheLimitProperty = "EditSessionCacheLimit";

		// Token: 0x0400011F RID: 287
		internal const string OfficeOnlineDiscoveryUrlProperty = "OfficeOnlineDiscoveryUrl";

		// Token: 0x04000120 RID: 288
		internal const string ExcelWopiClientUrl = "ExcelWopiClientUrl";

		// Token: 0x04000121 RID: 289
		internal const string RdlxReportTimeoutProperty = "RDLXReportTimeout";

		// Token: 0x04000122 RID: 290
		internal const string MaxFileSizeMb = "MaxFileSizeMb";

		// Token: 0x04000123 RID: 291
		internal const string RequireIntune = "RequireIntune";

		// Token: 0x04000124 RID: 292
		internal const string EnableCustomVisuals = "EnableCustomVisuals";

		// Token: 0x04000125 RID: 293
		internal const string TrustedFileFormat = "TrustedFileFormat";

		// Token: 0x04000126 RID: 294
		internal const string EnablePowerBIReportExportData = "EnablePowerBIReportExportData";

		// Token: 0x04000127 RID: 295
		internal const string EnablePowerBIReportExportUnderlyingData = "EnablePowerBIReportExportUnderlyingData";

		// Token: 0x04000128 RID: 296
		internal const string ShowDownloadMenu = "ShowDownloadMenu";

		// Token: 0x04000129 RID: 297
		internal const string AllowedResourceExtensionsForUpload = "AllowedResourceExtensionsForUpload";

		// Token: 0x0400012A RID: 298
		internal const string RestrictedResourceMimeTypeForUpload = "RestrictedResourceMimeTypeForUpload";

		// Token: 0x0400012B RID: 299
		internal const string CustomHeaders = "CustomHeaders";

		// Token: 0x0400012C RID: 300
		internal const string CustomUrlLabel = "CustomUrlLabel";

		// Token: 0x0400012D RID: 301
		internal const string CustomUrlValue = "CustomUrlValue";

		// Token: 0x0400012E RID: 302
		internal const string TileViewByDefault = "TileViewByDefault";

		// Token: 0x0400012F RID: 303
		internal const string EnablePowerBIReportMigrate = "EnablePowerBIReportMigrate";

		// Token: 0x04000130 RID: 304
		internal const string PowerBIMigrateUrl = "PowerBIMigrateUrl";

		// Token: 0x04000131 RID: 305
		internal const string PowerBIMigrateCountLimit = "PowerBIMigrateCountLimit";

		// Token: 0x04000132 RID: 306
		internal const string EnableListHistorySnapshotsSize = "EnableListHistorySnapshotsSize";

		// Token: 0x04000133 RID: 307
		internal const string CleanupBatchSize = "CleanupBatchSize";

		// Token: 0x04000134 RID: 308
		internal const string CleanupMaxLimit = "CleanupMaxLimit";

		// Token: 0x04000135 RID: 309
		internal const string ResourceUrl = "ResourceUrl";

		// Token: 0x04000136 RID: 310
		internal const string AuthorizationUrl = "AuthorizationUrl";

		// Token: 0x04000137 RID: 311
		internal const string PowerBIEndpoint = "PowerBIEndpoint";

		// Token: 0x04000138 RID: 312
		internal const string RedirectUrls = "RedirectUrls";

		// Token: 0x04000139 RID: 313
		internal const string LogoutUrl = "LogoutUrl";

		// Token: 0x0400013A RID: 314
		internal const string TokenUrl = "TokenUrl";

		// Token: 0x0400013B RID: 315
		internal const string ClientId = "ClientId";

		// Token: 0x0400013C RID: 316
		internal const string ClientSecret = "ClientSecret";

		// Token: 0x0400013D RID: 317
		internal const string AppObjectId = "AppObjectId";

		// Token: 0x0400013E RID: 318
		internal const string TenantName = "TenantName";

		// Token: 0x0400013F RID: 319
		internal const string TenantId = "TenantId";

		// Token: 0x04000140 RID: 320
		internal const string OAuthClientId = "OAuthClientId";

		// Token: 0x04000141 RID: 321
		internal const string OAuthClientSecret = "OAuthClientSecret";

		// Token: 0x04000142 RID: 322
		internal const string OAuthTenant = "OAuthTenant";

		// Token: 0x04000143 RID: 323
		internal const string OAuthTokenUrl = "OAuthTokenUrl";

		// Token: 0x04000144 RID: 324
		internal const string OAuthAuthorizationUrl = "OAuthAuthorizationUrl";

		// Token: 0x04000145 RID: 325
		internal const string OAuthFederationMetadataUrl = "OAuthFederationMetadataUrl";

		// Token: 0x04000146 RID: 326
		internal const string OAuthResourceUrl = "OAuthResourceUrl";

		// Token: 0x04000147 RID: 327
		internal const string OAuthNativeClientId = "OAuthNativeClientId";

		// Token: 0x04000148 RID: 328
		internal const string OAuthGraphUrl = "OAuthGraphUrl";

		// Token: 0x04000149 RID: 329
		internal const string OAuthSessionCookieName = "OAuthSessionCookieName";

		// Token: 0x0400014A RID: 330
		internal const string OAuthLogoutUrl = "OAuthLogoutUrl";

		// Token: 0x0400014B RID: 331
		internal const string AccessControlAllowCredentials = "AccessControlAllowCredentials";

		// Token: 0x0400014C RID: 332
		internal const string AccessControlAllowHeaders = "AccessControlAllowHeaders";

		// Token: 0x0400014D RID: 333
		internal const string AccessControlAllowMethods = "AccessControlAllowMethods";

		// Token: 0x0400014E RID: 334
		internal const string AccessControlAllowOrigin = "AccessControlAllowOrigin";

		// Token: 0x0400014F RID: 335
		internal const string AccessControlExposeHeaders = "AccessControlExposeHeaders";

		// Token: 0x04000150 RID: 336
		internal const string AccessControlMaxAge = "AccessControlMaxAge";
	}
}
