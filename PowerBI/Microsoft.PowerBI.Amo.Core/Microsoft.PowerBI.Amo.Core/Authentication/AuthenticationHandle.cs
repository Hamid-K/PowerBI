using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F2 RID: 242
	internal abstract class AuthenticationHandle
	{
		// Token: 0x06000F46 RID: 3910 RVA: 0x000346D0 File Offset: 0x000328D0
		internal static string ConvertIdentityProviderToTokenScheme(string identityProvider)
		{
			if (identityProvider == "PowerBIEmbed")
			{
				return "EmbedToken";
			}
			return "Bearer";
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000346EA File Offset: 0x000328EA
		protected AuthenticationHandle(AuthenticationEndpoint endpoint, string provider, string tenant)
		{
			this.Endpoint = endpoint;
			this.Provider = provider ?? string.Empty;
			this.Tenant = tenant ?? string.Empty;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00034719 File Offset: 0x00032919
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00034721 File Offset: 0x00032921
		public AuthenticationEndpoint Endpoint { get; private set; }

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0003472A File Offset: 0x0003292A
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00034732 File Offset: 0x00032932
		public string Provider { get; private set; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003473B File Offset: 0x0003293B
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x00034743 File Offset: 0x00032943
		public string Tenant { get; private set; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000F4E RID: 3918
		public abstract string Principal { get; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000F4F RID: 3919
		public abstract string AuthenticationScheme { get; }

		// Token: 0x06000F50 RID: 3920
		public abstract string GetAccessToken();

		// Token: 0x06000F51 RID: 3921
		public abstract long GetRefreshByTimeAsFileTime();

		// Token: 0x04000823 RID: 2083
		internal const string IdentityProviderPowerBIEmbed = "PowerBIEmbed";

		// Token: 0x04000824 RID: 2084
		internal const string AuthenticationScheme_AAD = "Bearer";

		// Token: 0x04000825 RID: 2085
		internal const string AuthenticationScheme_MWC = "MwcToken";

		// Token: 0x04000826 RID: 2086
		internal const string AuthenticationScheme_Embed = "EmbedToken";
	}
}
