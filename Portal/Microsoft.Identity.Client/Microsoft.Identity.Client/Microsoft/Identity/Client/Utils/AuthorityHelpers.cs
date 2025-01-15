using System;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001BF RID: 447
	internal static class AuthorityHelpers
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x00043953 File Offset: 0x00041B53
		public static string GetTenantId(Uri authorityUri)
		{
			return AuthorityInfo.FromAuthorityUri(authorityUri.ToString(), false).CreateAuthority().TenantId;
		}
	}
}
