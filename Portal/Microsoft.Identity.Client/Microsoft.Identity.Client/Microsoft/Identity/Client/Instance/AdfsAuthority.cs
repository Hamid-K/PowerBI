using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x0200026B RID: 619
	internal class AdfsAuthority : Authority
	{
		// Token: 0x0600186E RID: 6254 RVA: 0x00051245 File Offset: 0x0004F445
		public AdfsAuthority(AuthorityInfo authorityInfo)
			: base(authorityInfo)
		{
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0005124E File Offset: 0x0004F44E
		internal override Task<string> GetTokenEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/token", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0005126F File Offset: 0x0004F46F
		internal override Task<string> GetAuthorizationEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/authorize", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00051290 File Offset: 0x0004F490
		internal override Task<string> GetDeviceCodeEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/devicecode", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000512B1 File Offset: 0x0004F4B1
		internal override string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant)
		{
			return base.AuthorityInfo.CanonicalAuthority.ToString();
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000512C3 File Offset: 0x0004F4C3
		internal override string TenantId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000B10 RID: 2832
		private const string TokenEndpointTemplate = "{0}oauth2/token";

		// Token: 0x04000B11 RID: 2833
		private const string AuthorizationEndpointTemplate = "{0}oauth2/authorize";

		// Token: 0x04000B12 RID: 2834
		private const string DeviceCodeEndpointTemplate = "{0}oauth2/devicecode";
	}
}
