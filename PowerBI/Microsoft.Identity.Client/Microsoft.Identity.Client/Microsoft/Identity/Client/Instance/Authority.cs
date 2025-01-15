using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x0200026C RID: 620
	[DebuggerDisplay("{AuthorityInfo.CanonicalAuthority}")]
	internal abstract class Authority
	{
		// Token: 0x06001874 RID: 6260 RVA: 0x000512C6 File Offset: 0x0004F4C6
		protected Authority(AuthorityInfo authorityInfo)
		{
			if (authorityInfo == null)
			{
				throw new ArgumentNullException("authorityInfo");
			}
			this.AuthorityInfo = new AuthorityInfo(authorityInfo);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000512E8 File Offset: 0x0004F4E8
		public AuthorityInfo AuthorityInfo { get; }

		// Token: 0x06001876 RID: 6262 RVA: 0x000512F0 File Offset: 0x0004F4F0
		public static Task<Authority> CreateAuthorityForRequestAsync(RequestContext requestContext, AuthorityInfo requestAuthorityInfo, IAccount account = null)
		{
			return AuthorityInfo.AuthorityInfoHelper.CreateAuthorityForRequestAsync(requestContext, requestAuthorityInfo, account);
		}

		// Token: 0x06001877 RID: 6263 RVA: 0x000512FA File Offset: 0x0004F4FA
		public static Authority CreateAuthority(string authority, bool validateAuthority = false)
		{
			return AuthorityInfo.FromAuthorityUri(authority, validateAuthority).CreateAuthority();
		}

		// Token: 0x06001878 RID: 6264 RVA: 0x00051308 File Offset: 0x0004F508
		public static Authority CreateAuthority(AuthorityInfo authorityInfo)
		{
			return authorityInfo.CreateAuthority();
		}

		// Token: 0x06001879 RID: 6265 RVA: 0x00051310 File Offset: 0x0004F510
		internal static Authority CreateAuthorityWithTenant(AuthorityInfo authorityInfo, string tenantId)
		{
			Authority authority = Authority.CreateAuthority(authorityInfo);
			if (string.IsNullOrEmpty(tenantId))
			{
				return authority;
			}
			string tenantedAuthority = authority.GetTenantedAuthority(tenantId, false);
			return Authority.CreateAuthority(new AuthorityInfo(authority.AuthorityInfo.AuthorityType, tenantedAuthority, authority.AuthorityInfo.ValidateAuthority));
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x00051358 File Offset: 0x0004F558
		internal static Authority CreateAuthorityWithEnvironment(AuthorityInfo authorityInfo, string environment)
		{
			if (!authorityInfo.IsInstanceDiscoverySupported)
			{
				return Authority.CreateAuthority(authorityInfo);
			}
			return Authority.CreateAuthority(new UriBuilder(authorityInfo.CanonicalAuthority)
			{
				Host = environment
			}.Uri.AbsoluteUri, authorityInfo.ValidateAuthority);
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600187B RID: 6267
		internal abstract string TenantId { get; }

		// Token: 0x0600187C RID: 6268
		internal abstract string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant);

		// Token: 0x0600187D RID: 6269
		internal abstract Task<string> GetTokenEndpointAsync(RequestContext requestContext);

		// Token: 0x0600187E RID: 6270
		internal abstract Task<string> GetAuthorizationEndpointAsync(RequestContext requestContext);

		// Token: 0x0600187F RID: 6271
		internal abstract Task<string> GetDeviceCodeEndpointAsync(RequestContext requestContext);

		// Token: 0x06001880 RID: 6272 RVA: 0x00051390 File Offset: 0x0004F590
		internal static string GetEnvironment(string authority)
		{
			return new Uri(authority).Host;
		}
	}
}
