using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FD RID: 253
	internal abstract class AuthenticationHandle
	{
		// Token: 0x06000EB5 RID: 3765 RVA: 0x00031D28 File Offset: 0x0002FF28
		internal static string ConvertIdentityProviderToTokenScheme(string identityProvider)
		{
			if (identityProvider == "PowerBIEmbed")
			{
				return "EmbedToken";
			}
			return "Bearer";
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00031D42 File Offset: 0x0002FF42
		protected AuthenticationHandle(AuthenticationEndpoint endpoint, string provider, string tenant)
		{
			this.Endpoint = endpoint;
			this.Provider = provider ?? string.Empty;
			this.Tenant = tenant ?? string.Empty;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00031D71 File Offset: 0x0002FF71
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00031D79 File Offset: 0x0002FF79
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00031D82 File Offset: 0x0002FF82
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00031D8A File Offset: 0x0002FF8A
		public string Provider { get; private set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00031D93 File Offset: 0x0002FF93
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00031D9B File Offset: 0x0002FF9B
		public string Tenant { get; private set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000EBD RID: 3773
		public abstract string Principal { get; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000EBE RID: 3774
		public abstract string AuthenticationScheme { get; }

		// Token: 0x06000EBF RID: 3775
		public abstract string GetAccessToken();

		// Token: 0x06000EC0 RID: 3776
		public abstract long GetRefreshByTimeAsFileTime();

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00031DA4 File Offset: 0x0002FFA4
		internal virtual void AddUserRelatedProperties(AdomdPropertyCollection properties)
		{
			properties.Add("AadUserIdentityProvider", this.Provider);
			properties.Add("AadUserTenantId", this.Tenant);
		}

		// Token: 0x04000867 RID: 2151
		internal const string IdentityProviderPowerBIEmbed = "PowerBIEmbed";

		// Token: 0x04000868 RID: 2152
		internal const string AuthenticationScheme_AAD = "Bearer";

		// Token: 0x04000869 RID: 2153
		internal const string AuthenticationScheme_MWC = "MwcToken";

		// Token: 0x0400086A RID: 2154
		internal const string AuthenticationScheme_Embed = "EmbedToken";

		// Token: 0x0400086B RID: 2155
		private const string UserProperty_TenantId = "AadUserTenantId";

		// Token: 0x0400086C RID: 2156
		private const string UserProperty_IdentityProvider = "AadUserIdentityProvider";
	}
}
