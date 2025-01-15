using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.BIServer.HostingEnvironment.HostingInfo
{
	// Token: 0x02000043 RID: 67
	public static class CatalogConfigUpgrade
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public static void Upgrade(IDictionary<string, string> currentConfiguration)
		{
			CatalogConfigUpgrade.AddPngToListOfTrustedFileFormats(currentConfiguration);
			CatalogConfigUpgrade.EnsureHtmlMimeTypeRestrictionMatchHtmlExtensionPolicy(currentConfiguration);
			CatalogConfigUpgrade.UpdateSettingIfEqualsOldDefault(currentConfiguration, ConfigSettings.SiteName, "SQL Server Reporting Services", "Default");
			CatalogConfigUpgrade.UpdateSettingIfEqualsOldDefault(currentConfiguration, ConfigSettings.CustomHeaders, CatalogConfigUpgrade.OriginalCustomHeaders, "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=((?![?]).)*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)((\\/powerbi.*$)|(.*OpType=Calendar.*)))))</Pattern> <Value>SAMEORIGIN</Value> </Header> <Header> <Name>X-Content-Type-Options</Name> <Pattern>.*((\\.js$)|(\\.css$)|(\\.html$))</Pattern> <Value>nosniff</Value> </Header> </CustomHeaders>");
			CatalogConfigUpgrade.RemovePowerBIRegistration(currentConfiguration);
			CatalogConfigUpgrade.RemoveSetting(currentConfiguration, "EnablePowerBIReportEmbeddedModels");
			CatalogConfigUpgrade.RemoveSetting(currentConfiguration, "EnableReportDesignClientDownload");
			CatalogConfigUpgrade.RemoveSetting(currentConfiguration, "ReportBuilderLaunchURL");
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005B2C File Offset: 0x00003D2C
		private static void AddPngToListOfTrustedFileFormats(IDictionary<string, string> currentConfiguration)
		{
			if (currentConfiguration.ContainsKey(ConfigSettings.TrustedFileFormat.ToString(CultureInfo.InvariantCulture)))
			{
				IEnumerable<string> enumerable = from x in "jpg, jpeg, wav, pdf, img, gif, json, mp4, webm".Split(new char[] { ',' })
					select x.Trim().ToUpperInvariant();
				IEnumerable<string> enumerable2 = from x in currentConfiguration[ConfigSettings.TrustedFileFormat.ToString(CultureInfo.InvariantCulture)].Split(new char[] { ',' })
					select x.Trim().ToUpperInvariant();
				if (enumerable.Count<string>() == enumerable2.Count<string>() && !enumerable.Except(enumerable2).Any<string>())
				{
					currentConfiguration[ConfigSettings.TrustedFileFormat.ToString(CultureInfo.InvariantCulture)] = "jpg, jpeg, jpe, wav, bmp, img, gif, json, mp4, web, png";
				}
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00005C20 File Offset: 0x00003E20
		private static void EnsureHtmlMimeTypeRestrictionMatchHtmlExtensionPolicy(IDictionary<string, string> currentConfiguration)
		{
			string text = ConfigSettings.RestrictedResourceMimeTypeForUpload.ToString(CultureInfo.InvariantCulture);
			string text2 = ConfigSettings.AllowedResourceExtensionsForUpload.ToString(CultureInfo.InvariantCulture);
			if (currentConfiguration.ContainsKey(text) && currentConfiguration.ContainsKey(text2))
			{
				string text3 = currentConfiguration[text];
				IEnumerable<string> enumerable = from x in text3.Split(new char[] { ',' })
					select x.Trim().ToUpperInvariant();
				IEnumerable<string> enumerable2 = from x in currentConfiguration[text2].Split(new char[] { ',' })
					select x.Trim().ToUpperInvariant();
				bool flag = enumerable.Contains("text/html".ToUpperInvariant());
				bool flag2 = enumerable2.Contains("*.html".ToUpperInvariant());
				string text4 = text3;
				if (!flag && !flag2)
				{
					text4 = ((text3 == string.Empty) ? "text/html" : string.Format("{0}, {1}", text3, "text/html"));
				}
				else if (flag && flag2)
				{
					text4 = string.Join(",", (from x in text3.Split(new char[] { ',' })
						where x.Trim().ToUpperInvariant() != "text/html".ToUpperInvariant()
						select x).ToList<string>());
				}
				currentConfiguration[text] = text4;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005D98 File Offset: 0x00003F98
		private static void UpdateSettingIfEqualsOldDefault(IDictionary<string, string> configuration, ConfigSettings key, string oldValue, string newValue)
		{
			if (configuration.ContainsKey(key.ToString(CultureInfo.InvariantCulture)) && configuration[key.ToString(CultureInfo.InvariantCulture)].Equals(oldValue, StringComparison.InvariantCultureIgnoreCase))
			{
				configuration[key.ToString(CultureInfo.InvariantCulture)] = newValue;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005DFC File Offset: 0x00003FFC
		private static void UpdateSettingIfEqualsOldDefault(IDictionary<string, string> configuration, ConfigSettings key, List<string> oldValues, string newValue)
		{
			if (configuration.ContainsKey(key.ToString(CultureInfo.InvariantCulture)) && oldValues.Contains(configuration[key.ToString(CultureInfo.InvariantCulture)]))
			{
				configuration[key.ToString(CultureInfo.InvariantCulture)] = newValue;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005E5C File Offset: 0x0000405C
		private static void RemoveSetting(IDictionary<string, string> configuration, string key)
		{
			configuration.Remove(key);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005E68 File Offset: 0x00004068
		private static void RemovePowerBIRegistration(IDictionary<string, string> configuration)
		{
			CatalogConfigUpgrade.RemoveSetting(configuration, "AppObjectId");
			CatalogConfigUpgrade.RemoveSetting(configuration, "AuthorizationUrl");
			CatalogConfigUpgrade.RemoveSetting(configuration, "ClientId");
			CatalogConfigUpgrade.RemoveSetting(configuration, "ClientSecret");
			CatalogConfigUpgrade.RemoveSetting(configuration, "RedirectUrls");
			CatalogConfigUpgrade.RemoveSetting(configuration, "ResourceUrl");
			CatalogConfigUpgrade.RemoveSetting(configuration, "TenantId");
			CatalogConfigUpgrade.RemoveSetting(configuration, "TenantName");
			CatalogConfigUpgrade.RemoveSetting(configuration, "TokenUrl");
		}

		// Token: 0x040000EC RID: 236
		public const string OriginalTrustedFileFormats = "jpg, jpeg, wav, pdf, img, gif, json, mp4, webm";

		// Token: 0x040000ED RID: 237
		public const string OriginalDefaultSiteName = "SQL Server Reporting Services";

		// Token: 0x040000EE RID: 238
		public static List<string> OriginalCustomHeaders = new List<string> { "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=.*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(mobilereport|report|excel|pages|powerbi)\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)\\/powerbi.*$)))</Pattern> <Value>SAMEORIGIN</Value> </Header> </CustomHeaders>", "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=.*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)\\/powerbi.*$)))</Pattern> <Value>SAMEORIGIN</Value> </Header> </CustomHeaders>", "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=.*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)\\/powerbi.*$)))</Pattern> <Value>SAMEORIGIN</Value> </Header> <Header> <Name>X-Content-Type-Options</Name> <Pattern>.*((\\.js$)|(\\.css$)|(\\.html$))</Pattern> <Value>nosniff</Value> </Header> </CustomHeaders>", "<CustomHeaders> <Header> <Name>X-Frame-Options</Name> <Pattern>(?(?=.*api.*|.*rs:embed=true.*|.*rc:toolbar=false.*)(^((?!(.+)((\\/api)|(\\/(.+)(rs:embed=true|rc:toolbar=false)))).*$))|(^(?!(http|https):\\/\\/([^\\/]+)((\\/powerbi.*$)|(.*OpType=Calendar.*)))))</Pattern> <Value>SAMEORIGIN</Value> </Header> <Header> <Name>X-Content-Type-Options</Name> <Pattern>.*((\\.js$)|(\\.css$)|(\\.html$))</Pattern> <Value>nosniff</Value> </Header> </CustomHeaders>" };
	}
}
