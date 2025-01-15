using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Instance.Oidc;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x02000271 RID: 625
	internal class GenericAuthority : Authority
	{
		// Token: 0x06001895 RID: 6293 RVA: 0x000516B3 File Offset: 0x0004F8B3
		internal GenericAuthority(AuthorityInfo authorityInfo)
			: base(authorityInfo)
		{
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000516BC File Offset: 0x0004F8BC
		internal override string TenantId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x000516C0 File Offset: 0x0004F8C0
		internal override async Task<string> GetTokenEndpointAsync(RequestContext requestContext)
		{
			return (await OidcRetrieverWithCache.GetOidcAsync(base.AuthorityInfo.CanonicalAuthority.AbsoluteUri, requestContext).ConfigureAwait(false)).TokenEndpoint;
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0005170C File Offset: 0x0004F90C
		internal override async Task<string> GetAuthorizationEndpointAsync(RequestContext requestContext)
		{
			return (await OidcRetrieverWithCache.GetOidcAsync(base.AuthorityInfo.CanonicalAuthority.AbsoluteUri, requestContext).ConfigureAwait(false)).AuthorizationEndpoint;
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00051757 File Offset: 0x0004F957
		internal override Task<string> GetDeviceCodeEndpointAsync(RequestContext requestContext)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0005175E File Offset: 0x0004F95E
		internal override string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant)
		{
			return base.AuthorityInfo.CanonicalAuthority.ToString();
		}
	}
}
