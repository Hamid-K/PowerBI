using System;
using System.Security.Claims;
using Microsoft.Identity.Client.Cache.Items;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200017B RID: 379
	public class TenantProfile
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x0003EB8F File Offset: 0x0003CD8F
		internal TenantProfile(MsalIdTokenCacheItem msalIdTokenCacheItem)
		{
			this._msalIdTokenCacheItem = msalIdTokenCacheItem;
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x0003EB9E File Offset: 0x0003CD9E
		public string Oid
		{
			get
			{
				MsalIdTokenCacheItem msalIdTokenCacheItem = this._msalIdTokenCacheItem;
				if (msalIdTokenCacheItem == null)
				{
					return null;
				}
				return msalIdTokenCacheItem.IdToken.ObjectId;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0003EBB6 File Offset: 0x0003CDB6
		public string TenantId
		{
			get
			{
				MsalIdTokenCacheItem msalIdTokenCacheItem = this._msalIdTokenCacheItem;
				if (msalIdTokenCacheItem == null)
				{
					return null;
				}
				return msalIdTokenCacheItem.IdToken.TenantId;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x0003EBCE File Offset: 0x0003CDCE
		public ClaimsPrincipal ClaimsPrincipal
		{
			get
			{
				MsalIdTokenCacheItem msalIdTokenCacheItem = this._msalIdTokenCacheItem;
				if (msalIdTokenCacheItem == null)
				{
					return null;
				}
				return msalIdTokenCacheItem.IdToken.ClaimsPrincipal;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0003EBE6 File Offset: 0x0003CDE6
		public bool IsHomeTenant
		{
			get
			{
				MsalIdTokenCacheItem msalIdTokenCacheItem = this._msalIdTokenCacheItem;
				string tenantId = AccountId.ParseFromString((msalIdTokenCacheItem != null) ? msalIdTokenCacheItem.HomeAccountId : null).TenantId;
				MsalIdTokenCacheItem msalIdTokenCacheItem2 = this._msalIdTokenCacheItem;
				return string.Equals(tenantId, (msalIdTokenCacheItem2 != null) ? msalIdTokenCacheItem2.IdToken.TenantId : null, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x040006D4 RID: 1748
		private readonly MsalIdTokenCacheItem _msalIdTokenCacheItem;
	}
}
