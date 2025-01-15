using System;

namespace Microsoft.Identity.Client.Instance
{
	// Token: 0x0200026E RID: 622
	internal class B2CAuthority : AadAuthority
	{
		// Token: 0x06001889 RID: 6281 RVA: 0x000514B7 File Offset: 0x0004F6B7
		internal B2CAuthority(AuthorityInfo authorityInfo)
			: base(authorityInfo)
		{
			this.TenantId = base.AuthorityInfo.CanonicalAuthority.Segments[2].TrimEnd(new char[] { '/' });
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x000514E8 File Offset: 0x0004F6E8
		internal override string TenantId { get; }

		// Token: 0x0600188B RID: 6283 RVA: 0x000514F0 File Offset: 0x0004F6F0
		internal override string GetTenantedAuthority(string tenantId, bool forceSpecifiedTenant = false)
		{
			return base.AuthorityInfo.CanonicalAuthority.ToString();
		}

		// Token: 0x04000B1A RID: 2842
		public const string Prefix = "tfp";

		// Token: 0x04000B1B RID: 2843
		public const string B2CCanonicalAuthorityTemplate = "https://{0}/{1}/{2}/{3}/";
	}
}
