using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Cache.Items;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Cache
{
	// Token: 0x020002AC RID: 684
	internal interface ITokenCacheAccessor
	{
		// Token: 0x060019B2 RID: 6578
		void SaveAccessToken(MsalAccessTokenCacheItem item);

		// Token: 0x060019B3 RID: 6579
		void SaveRefreshToken(MsalRefreshTokenCacheItem item);

		// Token: 0x060019B4 RID: 6580
		void SaveIdToken(MsalIdTokenCacheItem item);

		// Token: 0x060019B5 RID: 6581
		void SaveAccount(MsalAccountCacheItem item);

		// Token: 0x060019B6 RID: 6582
		void SaveAppMetadata(MsalAppMetadataCacheItem item);

		// Token: 0x060019B7 RID: 6583
		MsalIdTokenCacheItem GetIdToken(MsalAccessTokenCacheItem accessTokenCacheItem);

		// Token: 0x060019B8 RID: 6584
		MsalAccountCacheItem GetAccount(MsalAccountCacheItem accountCacheItem);

		// Token: 0x060019B9 RID: 6585
		MsalAppMetadataCacheItem GetAppMetadata(MsalAppMetadataCacheItem appMetadataItem);

		// Token: 0x060019BA RID: 6586
		void DeleteAccessToken(MsalAccessTokenCacheItem item);

		// Token: 0x060019BB RID: 6587
		void DeleteRefreshToken(MsalRefreshTokenCacheItem item);

		// Token: 0x060019BC RID: 6588
		void DeleteIdToken(MsalIdTokenCacheItem item);

		// Token: 0x060019BD RID: 6589
		void DeleteAccount(MsalAccountCacheItem item);

		// Token: 0x060019BE RID: 6590
		List<MsalAccessTokenCacheItem> GetAllAccessTokens(string optionalPartitionKey = null, ILoggerAdapter requestlogger = null);

		// Token: 0x060019BF RID: 6591
		List<MsalRefreshTokenCacheItem> GetAllRefreshTokens(string optionalPartitionKey = null, ILoggerAdapter requestlogger = null);

		// Token: 0x060019C0 RID: 6592
		List<MsalIdTokenCacheItem> GetAllIdTokens(string optionalPartitionKey = null, ILoggerAdapter requestlogger = null);

		// Token: 0x060019C1 RID: 6593
		List<MsalAccountCacheItem> GetAllAccounts(string optionalPartitionKey = null, ILoggerAdapter requestlogger = null);

		// Token: 0x060019C2 RID: 6594
		List<MsalAppMetadataCacheItem> GetAllAppMetadata();

		// Token: 0x060019C3 RID: 6595
		void Clear(ILoggerAdapter requestlogger = null);

		// Token: 0x060019C4 RID: 6596
		bool HasAccessOrRefreshTokens();
	}
}
