using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001E8 RID: 488
	internal class ObjectWriteBackItemFactory : IWriteBackItemFactory
	{
		// Token: 0x06000FC8 RID: 4040 RVA: 0x00035F31 File Offset: 0x00034131
		public WriteBackItem GetWriteBackItem(AOMCacheItem item, StoreOperation operation, int writeFailCount, long firstWrite)
		{
			return new WriteBackItem(item, operation, writeFailCount, firstWrite);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00035F3D File Offset: 0x0003413D
		public WriteBackItem GetWriteBackItem(WriteBackItem item)
		{
			return new WriteBackItem(item);
		}
	}
}
