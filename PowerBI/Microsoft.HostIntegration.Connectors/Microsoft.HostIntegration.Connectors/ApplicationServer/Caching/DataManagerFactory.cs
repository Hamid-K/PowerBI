using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200020F RID: 527
	internal sealed class DataManagerFactory
	{
		// Token: 0x0600110B RID: 4363 RVA: 0x00038225 File Offset: 0x00036425
		public static IDataManager CreateDataManager(IDirectoryNodeFactory directoryNodeFactory)
		{
			return new DataManager(directoryNodeFactory);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0003822D File Offset: 0x0003642D
		public static IIndexSchema CreateIndexSchema()
		{
			return new DMIndexSchema();
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00038234 File Offset: 0x00036434
		public static IContainerSchema CreateContainerSchema()
		{
			return new DMHashContainerSchema();
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003823B File Offset: 0x0003643B
		public static IStoreSchema CreateStoreSchema()
		{
			return new MultiDirectoryHashtableSchema();
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x00038242 File Offset: 0x00036442
		public static IIndexStoreSchema CreateIndexStoreSchema()
		{
			return new DMMultiLevelHashTableSchema();
		}
	}
}
