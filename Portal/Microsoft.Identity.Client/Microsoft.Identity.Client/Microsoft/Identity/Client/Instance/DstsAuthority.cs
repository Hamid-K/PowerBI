using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x02000270 RID: 624
	internal class DstsAuthority : Authority
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x000515CE File Offset: 0x0004F7CE
		public DstsAuthority(AuthorityInfo authorityInfo)
			: base(authorityInfo)
		{
			this.TenantId = AuthorityInfo.GetSecondPathSegment(base.AuthorityInfo.CanonicalAuthority);
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000515F0 File Offset: 0x0004F7F0
		internal override string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant = false)
		{
			if (!string.IsNullOrEmpty(tenantId) && (forceSpecifiedTenant || AadAuthority.IsCommonOrganizationsOrConsumersTenant(this.TenantId)))
			{
				Uri canonicalAuthority = base.AuthorityInfo.CanonicalAuthority;
				return string.Format(CultureInfo.InvariantCulture, "https://{0}/dstsv2/{1}/", canonicalAuthority.Authority, tenantId);
			}
			return base.AuthorityInfo.CanonicalAuthority.ToString();
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00051648 File Offset: 0x0004F848
		internal override Task<string> GetTokenEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/v2.0/token", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x00051669 File Offset: 0x0004F869
		internal override Task<string> GetAuthorizationEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/v2.0/authorize", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0005168A File Offset: 0x0004F88A
		internal override Task<string> GetDeviceCodeEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/v2.0/devicecode", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000516AB File Offset: 0x0004F8AB
		internal override string TenantId { get; }

		// Token: 0x04000B1D RID: 2845
		public const string DstsCanonicalAuthorityTemplate = "https://{0}/dstsv2/{1}/";

		// Token: 0x04000B1E RID: 2846
		private const string TokenEndpointTemplate = "{0}oauth2/v2.0/token";

		// Token: 0x04000B1F RID: 2847
		private const string AuthorizationEndpointTemplate = "{0}oauth2/v2.0/authorize";

		// Token: 0x04000B20 RID: 2848
		private const string DeviceCodeEndpointTemplate = "{0}oauth2/v2.0/devicecode";
	}
}
