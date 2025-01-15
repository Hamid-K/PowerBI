using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.BIServer.HostingEnvironment.HostingInfo
{
	// Token: 0x02000046 RID: 70
	public static class CatalogConfigDefaults
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00005FE0 File Offset: 0x000041E0
		static CatalogConfigDefaults()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.EditSessionCacheLimit, "5");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.EditSessionTimeout, "7200");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ExecutionLogDaysKept, "60");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ExecutionLogLevel, "Normal");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ExternalImagesTimeout, "600");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.InterProcessTimeoutMinutes, "30");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.MaxFileSizeMb, "1000");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ModelCleanupCycleMinutes, "15");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ModelExpirationMinutes, "60");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.MyReportsRole, "My Reports");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.OfficeAccessTokenExpirationSeconds, "300");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.RDLXReportTimeout, "1800");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ScheduleRefreshTimeoutMinutes, "120");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.SessionTimeout, "600");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.ShowDownloadMenu, "true");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.SiteName, "Default");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.SnapshotCompression, "SQL");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.StoredParametersLifetime, "180");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.StoredParametersThreshold, "1500");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.SystemReportTimeout, "1800");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.SystemSnapshotLimit, "-1");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.TileViewByDefault, "True");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.TimerInitialDelaySeconds, "60");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.TrustedFileFormat, "jpg, jpeg, jpe, wav, bmp, img, gif, json, mp4, web, png");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.SupportedHyperlinkSchemes, "http,https,mailto");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.AllowedResourceExtensionsForUpload, "*,*.xml,*.xsd,*.xsl,*.png,*.gif,*.jpg,*.tif,*.jpeg,*.tiff,*.bmp,*.pdf,*.svg,*.rtf,*.txt,*.doc,*.docx,*.pps,*.ppt,*.pptx,*.rsmobile");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.RestrictedResourceMimeTypeForUpload, "text/html");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.CustomHeaders, "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=((?![?]).)*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)((\\/powerbi.*$)|(.*OpType=Calendar.*)))))</Pattern> <Value>SAMEORIGIN</Value> </Header> <Header> <Name>X-Content-Type-Options</Name> <Pattern>.*((\\.js$)|(\\.css$)|(\\.html$))</Pattern> <Value>nosniff</Value> </Header> </CustomHeaders>");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.CustomUrlLabel, "");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.CustomUrlValue, "");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.EnablePowerBIReportMigrate, "true");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.PowerBIMigrateUrl, "https://app.powerbi.com/");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.PowerBIMigrateCountLimit, "100");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.CleanupBatchSize, "20");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.CleanupMaxLimit, "200");
			CatalogConfigDefaults.AddDefault(dictionary, ConfigSettings.DefaultTilesSectionsOrder, "folders,kpis,powerbireports,paginatedreports,excel,datasets,datasources,resources");
			CatalogConfigDefaults.AddDefault(dictionary, CorsCongfigSettings.AccessControlAllowOrigin, "");
			CatalogConfigDefaults.AddDefault(dictionary, CorsCongfigSettings.AccessControlAllowCredentials, "false");
			CatalogConfigDefaults.AddDefault(dictionary, CorsCongfigSettings.AccessControlExposeHeaders, "");
			CatalogConfigDefaults.AddDefault(dictionary, CorsCongfigSettings.AccessControlMaxAge, "600");
			CatalogConfigDefaults.AddDefault(dictionary, CorsCongfigSettings.AccessControlAllowMethods, "GET, PUT, POST, PATCH, DELETE");
			CatalogConfigDefaults.AddDefault(dictionary, CorsCongfigSettings.AccessControlAllowHeaders, "");
			CatalogConfigDefaults._current = dictionary;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000062DE File Offset: 0x000044DE
		private static void AddDefault(Dictionary<string, string> defaults, Enum configKey, string configValue)
		{
			defaults.Add(configKey.ToString(CultureInfo.InvariantCulture), configValue);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000062F2 File Offset: 0x000044F2
		public static IReadOnlyDictionary<string, string> Current
		{
			get
			{
				return CatalogConfigDefaults._current;
			}
		}

		// Token: 0x040000F8 RID: 248
		public const string TrustedFileFormats = "jpg, jpeg, jpe, wav, bmp, img, gif, json, mp4, web, png";

		// Token: 0x040000F9 RID: 249
		public const string SupportedHyperlinkSchemes = "http,https,mailto";

		// Token: 0x040000FA RID: 250
		public const string AllowedResourceExtensionsForUpload = "*,*.xml,*.xsd,*.xsl,*.png,*.gif,*.jpg,*.tif,*.jpeg,*.tiff,*.bmp,*.pdf,*.svg,*.rtf,*.txt,*.doc,*.docx,*.pps,*.ppt,*.pptx,*.rsmobile";

		// Token: 0x040000FB RID: 251
		public const string DefaultSiteName = "Default";

		// Token: 0x040000FC RID: 252
		public const string RestrictedResourceMimeTypeForUpload = "text/html";

		// Token: 0x040000FD RID: 253
		public const string DefaultTilesSectionsOrder = "folders,kpis,powerbireports,paginatedreports,excel,datasets,datasources,resources";

		// Token: 0x040000FE RID: 254
		public const string DefaultCustomHeaders = "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=((?![?]).)*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)((\\/powerbi.*$)|(.*OpType=Calendar.*)))))</Pattern> <Value>SAMEORIGIN</Value> </Header> <Header> <Name>X-Content-Type-Options</Name> <Pattern>.*((\\.js$)|(\\.css$)|(\\.html$))</Pattern> <Value>nosniff</Value> </Header> </CustomHeaders>";

		// Token: 0x040000FF RID: 255
		private static IReadOnlyDictionary<string, string> _current;
	}
}
