using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FD RID: 253
	internal abstract class AuthenticationHandle
	{
		// Token: 0x06000EA8 RID: 3752 RVA: 0x000319F8 File Offset: 0x0002FBF8
		internal static string ConvertIdentityProviderToTokenScheme(string identityProvider)
		{
			if (identityProvider == "PowerBIEmbed")
			{
				return "EmbedToken";
			}
			return "Bearer";
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00031A12 File Offset: 0x0002FC12
		protected AuthenticationHandle(AuthenticationEndpoint endpoint, string provider, string tenant)
		{
			this.Endpoint = endpoint;
			this.Provider = provider ?? string.Empty;
			this.Tenant = tenant ?? string.Empty;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00031A41 File Offset: 0x0002FC41
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00031A49 File Offset: 0x0002FC49
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x00031A52 File Offset: 0x0002FC52
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00031A5A File Offset: 0x0002FC5A
		public string Provider { get; private set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x00031A63 File Offset: 0x0002FC63
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00031A6B File Offset: 0x0002FC6B
		public string Tenant { get; private set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000EB0 RID: 3760
		public abstract string Principal { get; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000EB1 RID: 3761
		public abstract string AuthenticationScheme { get; }

		// Token: 0x06000EB2 RID: 3762
		public abstract string GetAccessToken();

		// Token: 0x06000EB3 RID: 3763
		public abstract long GetRefreshByTimeAsFileTime();

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00031A74 File Offset: 0x0002FC74
		internal virtual void AddUserRelatedProperties(AdomdPropertyCollection properties)
		{
			properties.Add("AadUserIdentityProvider", this.Provider);
			properties.Add("AadUserTenantId", this.Tenant);
		}

		// Token: 0x0400085A RID: 2138
		internal const string IdentityProviderPowerBIEmbed = "PowerBIEmbed";

		// Token: 0x0400085B RID: 2139
		internal const string AuthenticationScheme_AAD = "Bearer";

		// Token: 0x0400085C RID: 2140
		internal const string AuthenticationScheme_MWC = "MwcToken";

		// Token: 0x0400085D RID: 2141
		internal const string AuthenticationScheme_Embed = "EmbedToken";

		// Token: 0x0400085E RID: 2142
		private const string UserProperty_TenantId = "AadUserTenantId";

		// Token: 0x0400085F RID: 2143
		private const string UserProperty_IdentityProvider = "AadUserIdentityProvider";
	}
}
