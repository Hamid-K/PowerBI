using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000AD RID: 173
	public enum CacheOperationType
	{
		// Token: 0x04000313 RID: 787
		Unknown,
		// Token: 0x04000314 RID: 788
		Get,
		// Token: 0x04000315 RID: 789
		Put,
		// Token: 0x04000316 RID: 790
		Add,
		// Token: 0x04000317 RID: 791
		BulkGet,
		// Token: 0x04000318 RID: 792
		Remove,
		// Token: 0x04000319 RID: 793
		ResetObjectTimeout,
		// Token: 0x0400031A RID: 794
		GetIfNewer,
		// Token: 0x0400031B RID: 795
		GetAndLock,
		// Token: 0x0400031C RID: 796
		PutAndUnlock,
		// Token: 0x0400031D RID: 797
		Unlock,
		// Token: 0x0400031E RID: 798
		LockedRemove,
		// Token: 0x0400031F RID: 799
		GetCacheItem,
		// Token: 0x04000320 RID: 800
		CreateRegion,
		// Token: 0x04000321 RID: 801
		RemoveRegion,
		// Token: 0x04000322 RID: 802
		ClearRegion,
		// Token: 0x04000323 RID: 803
		AsyncGet,
		// Token: 0x04000324 RID: 804
		ClearCache,
		// Token: 0x04000325 RID: 805
		Increment,
		// Token: 0x04000326 RID: 806
		Concatenate,
		// Token: 0x04000327 RID: 807
		ContainsKey,
		// Token: 0x04000328 RID: 808
		GetAllKeys
	}
}
