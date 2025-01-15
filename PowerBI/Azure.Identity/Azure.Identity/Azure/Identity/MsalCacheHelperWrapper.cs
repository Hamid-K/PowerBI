using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Azure.Identity
{
	// Token: 0x02000073 RID: 115
	internal class MsalCacheHelperWrapper
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x0000B938 File Offset: 0x00009B38
		public virtual async Task InitializeAsync(StorageCreationProperties storageCreationProperties, TraceSource logger = null)
		{
			MsalCacheHelper msalCacheHelper = await MsalCacheHelper.CreateAsync(storageCreationProperties, logger).ConfigureAwait(false);
			this._helper = msalCacheHelper;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000B98B File Offset: 0x00009B8B
		public virtual void VerifyPersistence()
		{
			this._helper.VerifyPersistence();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000B998 File Offset: 0x00009B98
		public virtual void RegisterCache(ITokenCache tokenCache)
		{
			this._helper.RegisterCache(tokenCache);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000B9A6 File Offset: 0x00009BA6
		public virtual void UnregisterCache(ITokenCache tokenCache)
		{
			this._helper.UnregisterCache(tokenCache);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public virtual byte[] LoadUnencryptedTokenCache()
		{
			return this._helper.LoadUnencryptedTokenCache();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000B9C1 File Offset: 0x00009BC1
		public virtual void SaveUnencryptedTokenCache(byte[] tokenCache)
		{
			this._helper.SaveUnencryptedTokenCache(tokenCache);
		}

		// Token: 0x0400023E RID: 574
		private MsalCacheHelper _helper;
	}
}
