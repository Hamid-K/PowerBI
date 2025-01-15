using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Instance;
using Microsoft.Identity.Client.Utils;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002A3 RID: 675
	internal class AdalUsersForMsal
	{
		// Token: 0x0600197C RID: 6524 RVA: 0x00053489 File Offset: 0x00051689
		public AdalUsersForMsal(IEnumerable<AdalUserForMsalEntry> userEntries)
		{
			if (userEntries == null)
			{
				throw new ArgumentNullException("userEntries");
			}
			this._userEntries = userEntries;
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000534A8 File Offset: 0x000516A8
		public IDictionary<string, AdalUserInfo> GetUsersWithClientInfo(IEnumerable<string> envAliases)
		{
			return this._userEntries.Where(delegate(AdalUserForMsalEntry u)
			{
				if (!string.IsNullOrEmpty(u.Authority) && !string.IsNullOrEmpty(u.ClientInfo))
				{
					IEnumerable<string> envAliases2 = envAliases;
					return envAliases2 == null || envAliases2.ContainsOrdinalIgnoreCase(Authority.GetEnvironment(u.Authority));
				}
				return false;
			}).ToLookup((AdalUserForMsalEntry u) => u.ClientInfo, (AdalUserForMsalEntry u) => u.UserInfo).ToDictionary((IGrouping<string, AdalUserInfo> group) => group.Key, (IGrouping<string, AdalUserInfo> group) => group.First<AdalUserInfo>());
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00053560 File Offset: 0x00051760
		public IEnumerable<AdalUserInfo> GetUsersWithoutClientInfo(IEnumerable<string> envAliases)
		{
			return from u in this._userEntries.Where(delegate(AdalUserForMsalEntry u)
				{
					if (!string.IsNullOrEmpty(u.Authority) && string.IsNullOrEmpty(u.ClientInfo))
					{
						IEnumerable<string> envAliases2 = envAliases;
						return envAliases2 == null || envAliases2.ContainsOrdinalIgnoreCase(Authority.GetEnvironment(u.Authority));
					}
					return false;
				})
				select u.UserInfo;
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000535B8 File Offset: 0x000517B8
		public ISet<string> GetAdalUserEnvironments()
		{
			return new HashSet<string>(from u in this._userEntries
				where !string.IsNullOrEmpty(u.Authority)
				select Authority.GetEnvironment(u.Authority), StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x04000B77 RID: 2935
		private readonly IEnumerable<AdalUserForMsalEntry> _userEntries;
	}
}
