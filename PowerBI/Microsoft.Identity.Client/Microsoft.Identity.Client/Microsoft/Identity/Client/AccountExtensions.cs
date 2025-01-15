using System;
using System.Collections.Generic;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000115 RID: 277
	public static class AccountExtensions
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x00036DFB File Offset: 0x00034FFB
		public static IEnumerable<TenantProfile> GetTenantProfiles(this IAccount account)
		{
			Account account2 = account as Account;
			if (account2 == null)
			{
				return null;
			}
			return account2.TenantProfiles;
		}
	}
}
