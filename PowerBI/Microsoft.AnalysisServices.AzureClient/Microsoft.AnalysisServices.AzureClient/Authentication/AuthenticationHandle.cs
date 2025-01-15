using System;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000017 RID: 23
	internal abstract class AuthenticationHandle
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002D4E File Offset: 0x00000F4E
		internal static string ConvertIdentityProviderToTokenScheme(string identityProvider)
		{
			if (identityProvider == "PowerBIEmbed")
			{
				return "EmbedToken";
			}
			return "Bearer";
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002D68 File Offset: 0x00000F68
		protected AuthenticationHandle(AuthenticationEndpoint endpoint, string provider, string tenant)
		{
			this.Endpoint = endpoint;
			this.Provider = provider ?? string.Empty;
			this.Tenant = tenant ?? string.Empty;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002D97 File Offset: 0x00000F97
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002D9F File Offset: 0x00000F9F
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002DA8 File Offset: 0x00000FA8
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public string Provider { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002DB9 File Offset: 0x00000FB9
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002DC1 File Offset: 0x00000FC1
		public string Tenant { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000083 RID: 131
		public abstract string Principal { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000084 RID: 132
		public abstract string AuthenticationScheme { get; }

		// Token: 0x06000085 RID: 133
		public abstract string GetAccessToken();

		// Token: 0x06000086 RID: 134
		public abstract long GetRefreshByTimeAsFileTime();

		// Token: 0x0400003B RID: 59
		internal const string IdentityProviderPowerBIEmbed = "PowerBIEmbed";

		// Token: 0x0400003C RID: 60
		internal const string AuthenticationScheme_AAD = "Bearer";

		// Token: 0x0400003D RID: 61
		internal const string AuthenticationScheme_MWC = "MwcToken";

		// Token: 0x0400003E RID: 62
		internal const string AuthenticationScheme_Embed = "EmbedToken";
	}
}
