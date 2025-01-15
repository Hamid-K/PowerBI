using System;

namespace Microsoft.Mashup.OAuth
{
	// Token: 0x02000007 RID: 7
	public static class AadSettings
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002BE4 File Offset: 0x00000DE4
		// Note: this type is marked as 'beforefieldinit'.
		static AadSettings()
		{
			ISecureTokenService[] array = new SecureTokenService[]
			{
				AadSettings.windowsNet,
				AadSettings.microsoftOnlineCom,
				AadSettings.microsoftOnlineDe,
				AadSettings.microsoftOnlineUs,
				AadSettings.microsoftOnlineUsGov,
				AadSettings.chinaCloudApiCn,
				AadSettings.partnerMicrosoftOnlineCn,
				new SecureTokenService("https://hybridproxy.powerbi.com", "https://{TENANT}.hybridproxy.powerbi.com/AuthorizationService/Oauth2/authorize?api-version=2013-12-01", "https://{TENANT}.hybridproxy.powerbi.com/AuthorizationService/oauth2/token?api-version=2013-12-01", "https://{TENANT}.hybridproxy.powerbi.com/AuthorizationService/oauth2/logout?api-version=2013-12-01"),
				new SecureTokenService("https://corp.sts.microsoft.com", "https://corp.sts.microsoft.com/adfs/oauth2/authorize", "https://corp.sts.microsoft.com/adfs/oauth2/token", "https://corp.sts.microsoft.com/adfs/ls/?wa=wsignout1.0")
			};
			AadSettings.Production = new OAuthSettings(array, new TrustedResource[]
			{
				new TrustedResource("https://marketing-infra.dynamics.com/", new string[] { "https://*.marketing.dynamics.com", "https://*.marketing-preview.dynamics.com" }),
				new TrustedResource("https://insights.sellerdashboard.microsoft.com/", new string[] { "https://insights.sellerdashboard.microsoft.com" }),
				new TrustedResource("https://management.core.windows.net/", new string[] { "https://management.azure.com" })
			});
			AadSettings.INTSecureTokenServices = new SecureTokenService[]
			{
				new SecureTokenService("https://oauthfeed", "https://{TENANT}/Authorize", "https://{TENANT}/TokenUrl", "https://{TENANT}/LogoutUrl"),
				new SecureTokenService("https://hybridproxy.int.powerbi-int.com", "https://{TENANT}.hybridproxy.int.powerbi-int.com/AuthorizationService/Oauth2/authorize?api-version=2013-12-01", "https://{TENANT}.hybridproxy.int.powerbi-int.com/AuthorizationService/oauth2/token?api-version=2013-12-01", "https://{TENANT}.hybridproxy.int.powerbi-int.com/AuthorizationService/oauth2/logout?api-version=2013-12-01"),
				new SecureTokenService("https://hybridproxy.int2.powerbi-int.com", "https://{TENANT}.hybridproxy.int2.powerbi-int.com/AuthorizationService/Oauth2/authorize?api-version=2013-12-01", "https://{TENANT}.hybridproxy.int2.powerbi-int.com/AuthorizationService/oauth2/token?api-version=2013-12-01", "https://{TENANT}.hybridproxy.int2.powerbi-int.com/AuthorizationService/oauth2/logout?api-version=2013-12-01"),
				AadSettings.windowsNet,
				AadSettings.microsoftOnlineCom,
				AadSettings.microsoftOnlineDe,
				AadSettings.microsoftOnlineUs,
				AadSettings.microsoftOnlineUsGov,
				AadSettings.chinaCloudApiCn,
				AadSettings.partnerMicrosoftOnlineCn,
				new SecureTokenService("https://hybridproxy.powerbi.com", "https://{TENANT}.hybridproxy.powerbi.com/AuthorizationService/Oauth2/authorize?api-version=2013-12-01", "https://{TENANT}.hybridproxy.powerbi.com/AuthorizationService/oauth2/token?api-version=2013-12-01", "https://{TENANT}.hybridproxy.powerbi.com/AuthorizationService/oauth2/logout?api-version=2013-12-01"),
				new SecureTokenService("https://login.windows-ppe.net", "https://login.windows-ppe.net/{TENANT}/oauth2/authorize", "https://login.windows-ppe.net/{TENANT}/oauth2/token", "https://login.windows-ppe.net/{TENANT}/oauth2/logout")
			};
			array = AadSettings.INTSecureTokenServices;
			AadSettings.Integration = new OAuthSettings(array, new TrustedResource[]
			{
				new TrustedResource("Microsoft.CRM", new string[] { "https://oauthfeed" }),
				new TrustedResource("https://marketing-infra.dynamics.com/", new string[] { "https://*.marketing.dynamics.com", "https://*.marketing-tie.dynamics.com", "https://*.marketing-preview.dynamics.com", "https://*.mdmtest.selfhost.corp.microsoft.com", "https://*.europe.corp.microsoft.com" }),
				new TrustedResource("https://management.core.windows.net/", new string[] { "https://management.azure.com", "https://api-dogfood.resources.windows-int.net" })
			});
			AadSettings.NoTrustedResources = new OAuthSettings(AadSettings.Production.AllowedSecureTokenServices, new TrustedResource[0]);
			array = new SecureTokenService[] { SecureTokenService.CreateTenant(AadSettings.windowsNet, "common") };
			AadSettings.CommonSettings = new OAuthSettings(array);
			array = new SecureTokenService[] { SecureTokenService.CreateTenant(AadSettings.microsoftOnlineDe, "common") };
			AadSettings.CommonDeSettings = new OAuthSettings(array);
			array = new SecureTokenService[] { SecureTokenService.CreateTenant(AadSettings.microsoftOnlineUs, "common") };
			AadSettings.CommonUsGovSettings = new OAuthSettings(array);
			array = new SecureTokenService[] { SecureTokenService.CreateTenant(AadSettings.chinaCloudApiCn, "common") };
			AadSettings.CommonCnSettings = new OAuthSettings(array);
			array = new SecureTokenService[]
			{
				new SecureTokenService("https://login.windows-ppe.net", "https://login.windows-ppe.net/common/oauth2/authorize", "https://login.windows-ppe.net/common/oauth2/token", "https://login.windows-ppe.net/common/oauth2/logout")
			};
			AadSettings.CommonSettingsPPE = new OAuthSettings(array);
		}

		// Token: 0x0400001B RID: 27
		private static SecureTokenService windowsNet = new SecureTokenService("https://login.windows.net");

		// Token: 0x0400001C RID: 28
		private static SecureTokenService microsoftOnlineCom = new SecureTokenService("https://login.microsoftonline.com");

		// Token: 0x0400001D RID: 29
		private static SecureTokenService microsoftOnlineDe = new SecureTokenService("https://login.microsoftonline.de");

		// Token: 0x0400001E RID: 30
		private static SecureTokenService microsoftOnlineUs = new SecureTokenService("https://login.microsoftonline.us");

		// Token: 0x0400001F RID: 31
		private static SecureTokenService microsoftOnlineUsGov = new SecureTokenService("https://login-us.microsoftonline.com");

		// Token: 0x04000020 RID: 32
		private static SecureTokenService chinaCloudApiCn = new SecureTokenService("https://login.chinacloudapi.cn");

		// Token: 0x04000021 RID: 33
		private static SecureTokenService partnerMicrosoftOnlineCn = new SecureTokenService("https://login.partner.microsoftonline.cn");

		// Token: 0x04000022 RID: 34
		public static OAuthSettings Production;

		// Token: 0x04000023 RID: 35
		private static SecureTokenService[] INTSecureTokenServices;

		// Token: 0x04000024 RID: 36
		public static OAuthSettings Integration;

		// Token: 0x04000025 RID: 37
		public static OAuthSettings NoTrustedResources;

		// Token: 0x04000026 RID: 38
		public static OAuthSettings CommonSettings;

		// Token: 0x04000027 RID: 39
		public static OAuthSettings CommonDeSettings;

		// Token: 0x04000028 RID: 40
		public static OAuthSettings CommonUsGovSettings;

		// Token: 0x04000029 RID: 41
		public static OAuthSettings CommonCnSettings;

		// Token: 0x0400002A RID: 42
		public static OAuthSettings CommonSettingsPPE;
	}
}
