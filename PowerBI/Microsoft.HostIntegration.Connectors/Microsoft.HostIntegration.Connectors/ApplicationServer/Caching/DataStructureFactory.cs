using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000210 RID: 528
	internal sealed class DataStructureFactory
	{
		// Token: 0x06001111 RID: 4369 RVA: 0x00038249 File Offset: 0x00036449
		public static IHashtable CreateHashtable(IDirectoryNodeFactory directorNodeFactory)
		{
			return new MultiDirectoryHashtable(directorNodeFactory);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00038251 File Offset: 0x00036451
		public static IHashtable CreateHashtable(IStoreSchema iHashTableSchema, IDirectoryNodeFactory directoryNodeFactory)
		{
			return new MultiDirectoryHashtable(iHashTableSchema, directoryNodeFactory);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0003825A File Offset: 0x0003645A
		public static IBaseHashTable CreateBaseHashTable(IDirectoryNodeFactory directoryNodeFactory)
		{
			return new BaseHashTable(directoryNodeFactory);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00038262 File Offset: 0x00036462
		public static IMultiLevelHashTable CreateMultiLevelHashTable(int level, IDirectoryNodeFactory directoryNodeFactory)
		{
			return new DMMultiLevelHashTable(level, directoryNodeFactory);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0003826B File Offset: 0x0003646B
		public static IMultiLevelHashTable CreateMultiLevelHashTable(IIndexStoreSchema iSchema, IDirectoryNodeFactory directoryNodeFactory)
		{
			return new DMMultiLevelHashTable(iSchema, directoryNodeFactory);
		}
	}
}
