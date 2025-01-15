using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x0200026A RID: 618
	internal class AadAuthority : Authority
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x00051075 File Offset: 0x0004F275
		internal AadAuthority(AuthorityInfo authorityInfo)
			: base(authorityInfo)
		{
			this.TenantId = AuthorityInfo.GetFirstPathSegment(base.AuthorityInfo.CanonicalAuthority);
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00051094 File Offset: 0x0004F294
		internal override string TenantId { get; }

		// Token: 0x06001861 RID: 6241 RVA: 0x0005109C File Offset: 0x0004F29C
		internal bool IsWorkAndSchoolOnly()
		{
			return !this.TenantId.Equals("common", StringComparison.OrdinalIgnoreCase) && !AadAuthority.IsConsumers(this.TenantId);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000510C1 File Offset: 0x0004F2C1
		internal bool IsConsumers()
		{
			return AadAuthority.IsConsumers(this.TenantId);
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000510CE File Offset: 0x0004F2CE
		internal static bool IsConsumers(string tenantId)
		{
			return tenantId.Equals("consumers", StringComparison.OrdinalIgnoreCase) || tenantId.Equals("9188040d-6c67-4c5b-b112-36a304b66dad", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000510EC File Offset: 0x0004F2EC
		internal bool IsCommonOrganizationsOrConsumersTenant()
		{
			return AadAuthority.IsCommonOrganizationsOrConsumersTenant(this.TenantId);
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x000510F9 File Offset: 0x0004F2F9
		internal static bool IsCommonOrganizationsOrConsumersTenant(string tenantId)
		{
			return !string.IsNullOrEmpty(tenantId) && (AadAuthority.IsCommonOrOrganizationsTenant(tenantId) || AadAuthority.IsConsumers(tenantId));
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x00051115 File Offset: 0x0004F315
		internal bool IsOrganizationsTenantWithMsaPassthroughEnabled(bool isMsaPassthrough, string accountTenantId)
		{
			return accountTenantId != null && isMsaPassthrough && this.TenantId.Equals("organizations", StringComparison.OrdinalIgnoreCase) && AadAuthority.IsConsumers(accountTenantId);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0005113A File Offset: 0x0004F33A
		internal bool IsCommonOrOrganizationsTenant()
		{
			return AadAuthority.IsCommonOrOrganizationsTenant(this.TenantId);
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00051147 File Offset: 0x0004F347
		internal static bool IsCommonOrOrganizationsTenant(string tenantId)
		{
			return !string.IsNullOrEmpty(tenantId) && AadAuthority.s_tenantlessTenantNames.Contains(tenantId);
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00051160 File Offset: 0x0004F360
		internal override string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant = false)
		{
			if (!string.IsNullOrEmpty(tenantId) && (forceSpecifiedTenant || this.IsCommonOrganizationsOrConsumersTenant()))
			{
				Uri canonicalAuthority = base.AuthorityInfo.CanonicalAuthority;
				return string.Format(CultureInfo.InvariantCulture, "https://{0}/{1}/", canonicalAuthority.Authority, tenantId);
			}
			return base.AuthorityInfo.CanonicalAuthority.AbsoluteUri;
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x000511B3 File Offset: 0x0004F3B3
		internal override Task<string> GetTokenEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/v2.0/token", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x000511D4 File Offset: 0x0004F3D4
		internal override Task<string> GetAuthorizationEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/v2.0/authorize", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x000511F5 File Offset: 0x0004F3F5
		internal override Task<string> GetDeviceCodeEndpointAsync(RequestContext requestContext)
		{
			return Task.FromResult<string>(string.Format(CultureInfo.InvariantCulture, "{0}oauth2/v2.0/devicecode", base.AuthorityInfo.CanonicalAuthority));
		}

		// Token: 0x04000B09 RID: 2825
		public const string DefaultTrustedHost = "login.microsoftonline.com";

		// Token: 0x04000B0A RID: 2826
		public const string AADCanonicalAuthorityTemplate = "https://{0}/{1}/";

		// Token: 0x04000B0B RID: 2827
		private const string TokenEndpointTemplate = "{0}oauth2/v2.0/token";

		// Token: 0x04000B0C RID: 2828
		private const string DeviceCodeEndpointTemplate = "{0}oauth2/v2.0/devicecode";

		// Token: 0x04000B0D RID: 2829
		private const string AuthorizationEndpointTemplate = "{0}oauth2/v2.0/authorize";

		// Token: 0x04000B0E RID: 2830
		private static readonly ISet<string> s_tenantlessTenantNames = new HashSet<string>(new string[] { "common", "organizations", "consumers" }, StringComparer.OrdinalIgnoreCase);
	}
}
