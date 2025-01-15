using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200034D RID: 845
	internal class ObjectMemoryManager : IMemoryManager
	{
		// Token: 0x06001DF8 RID: 7672 RVA: 0x00059E5E File Offset: 0x0005805E
		internal ObjectMemoryManager()
		{
			this._cacheItemFactory = new ObjectCacheItemFactory();
			this._directorNodeFactory = new ObjectDirectoryNodeFactory();
			this._writeBackItemNodeFactory = new ObjectWriteBackItemFactory();
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x00059E87 File Offset: 0x00058087
		public ICacheItemFactory GetCacheItemFactory()
		{
			return this._cacheItemFactory;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00059E8F File Offset: 0x0005808F
		public IDirectoryNodeFactory GetDirectoryNodeFactory()
		{
			return this._directorNodeFactory;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x00059E97 File Offset: 0x00058097
		public IWriteBackItemFactory GetWriteBackItemFactory()
		{
			return this._writeBackItemNodeFactory;
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x00003CAB File Offset: 0x00001EAB
		public object GetStats()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040010DC RID: 4316
		private ObjectCacheItemFactory _cacheItemFactory;

		// Token: 0x040010DD RID: 4317
		private ObjectDirectoryNodeFactory _directorNodeFactory;

		// Token: 0x040010DE RID: 4318
		private ObjectWriteBackItemFactory _writeBackItemNodeFactory;
	}
}
