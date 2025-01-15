using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002DC RID: 732
	internal interface IMemoryManager
	{
		// Token: 0x06001B04 RID: 6916
		ICacheItemFactory GetCacheItemFactory();

		// Token: 0x06001B05 RID: 6917
		IDirectoryNodeFactory GetDirectoryNodeFactory();

		// Token: 0x06001B06 RID: 6918
		IWriteBackItemFactory GetWriteBackItemFactory();

		// Token: 0x06001B07 RID: 6919
		object GetStats();
	}
}
