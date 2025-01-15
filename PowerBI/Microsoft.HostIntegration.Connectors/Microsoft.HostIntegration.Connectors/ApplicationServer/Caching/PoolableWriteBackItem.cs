using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F1 RID: 497
	internal class PoolableWriteBackItem : WriteBackItem
	{
		// Token: 0x06001056 RID: 4182 RVA: 0x00036AA9 File Offset: 0x00034CA9
		public PoolableWriteBackItem(WriteBackItemPool parentPool)
		{
			this._parentPool = parentPool;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00036AB8 File Offset: 0x00034CB8
		~PoolableWriteBackItem()
		{
			if (!AppDomain.CurrentDomain.IsFinalizingForUnload() && base.IsInUse())
			{
				base.Clean();
				this._parentPool.PutObjectInPool(this);
				GC.ReRegisterForFinalize(this);
			}
		}

		// Token: 0x04000AB9 RID: 2745
		private WriteBackItemPool _parentPool;
	}
}
