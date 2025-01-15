using System;

namespace Azure.Identity
{
	// Token: 0x02000028 RID: 40
	public static class AzureAuthorityHosts
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000045FD File Offset: 0x000027FD
		public static Uri AzurePublicCloud { get; } = new Uri("https://login.microsoftonline.com/");

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004604 File Offset: 0x00002804
		public static Uri AzureChina { get; } = new Uri("https://login.chinacloudapi.cn/");

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000460B File Offset: 0x0000280B
		public static Uri AzureGermany { get; } = new Uri("https://login.microsoftonline.de/");

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004612 File Offset: 0x00002812
		public static Uri AzureGovernment { get; } = new Uri("https://login.microsoftonline.us/");

		// Token: 0x060000D4 RID: 212 RVA: 0x00004619 File Offset: 0x00002819
		internal static Uri GetDefault()
		{
			if (EnvironmentVariables.AuthorityHost != null)
			{
				return new Uri(EnvironmentVariables.AuthorityHost);
			}
			return AzureAuthorityHosts.AzurePublicCloud;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004634 File Offset: 0x00002834
		internal static string GetDefaultScope(Uri authorityHost)
		{
			string absoluteUri = authorityHost.AbsoluteUri;
			if (absoluteUri == "https://login.microsoftonline.com/")
			{
				return "https://management.azure.com//.default";
			}
			if (absoluteUri == "https://login.chinacloudapi.cn/")
			{
				return "https://management.chinacloudapi.cn/.default";
			}
			if (absoluteUri == "https://login.microsoftonline.de/")
			{
				return "https://management.microsoftazure.de/.default";
			}
			if (!(absoluteUri == "https://login.microsoftonline.us/"))
			{
				return null;
			}
			return "https://management.usgovcloudapi.net/.default";
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004697 File Offset: 0x00002897
		internal static Uri GetDeviceCodeRedirectUri(Uri authorityHost)
		{
			return new Uri(authorityHost, "/common/oauth2/nativeclient");
		}

		// Token: 0x04000076 RID: 118
		private const string AzurePublicCloudHostUrl = "https://login.microsoftonline.com/";

		// Token: 0x04000077 RID: 119
		private const string AzureChinaHostUrl = "https://login.chinacloudapi.cn/";

		// Token: 0x04000078 RID: 120
		private const string AzureGermanyHostUrl = "https://login.microsoftonline.de/";

		// Token: 0x04000079 RID: 121
		private const string AzureGovernmentHostUrl = "https://login.microsoftonline.us/";
	}
}
