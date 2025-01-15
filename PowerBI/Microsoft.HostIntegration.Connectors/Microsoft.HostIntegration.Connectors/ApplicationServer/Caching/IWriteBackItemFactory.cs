using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E7 RID: 487
	internal interface IWriteBackItemFactory
	{
		// Token: 0x06000FC6 RID: 4038
		WriteBackItem GetWriteBackItem(AOMCacheItem item, StoreOperation operation, int writeFailCount, long firstWrite);

		// Token: 0x06000FC7 RID: 4039
		WriteBackItem GetWriteBackItem(WriteBackItem item);
	}
}
