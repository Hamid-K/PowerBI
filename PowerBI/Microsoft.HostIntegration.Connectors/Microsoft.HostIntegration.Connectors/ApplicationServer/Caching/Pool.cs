using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F5 RID: 501
	internal class Pool<T> where T : class
	{
		// Token: 0x06001063 RID: 4195 RVA: 0x00036C20 File Offset: 0x00034E20
		internal Pool(long poolSize)
		{
			this.PoolSize = ((poolSize < 2147483647L) ? poolSize : 2147483647L);
			this.ObjectPool = new PoolQueue<T>(this.PoolSize, default(T));
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00036C7B File Offset: 0x00034E7B
		internal long Count
		{
			get
			{
				return this.ObjectPool.Count;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00036C88 File Offset: 0x00034E88
		internal long Size
		{
			get
			{
				return this.PoolSize;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00036C90 File Offset: 0x00034E90
		internal virtual T GetObjectFromPool()
		{
			return this.ObjectPool.Dequeue();
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00036C9D File Offset: 0x00034E9D
		internal bool PutObjectInPool(T obj)
		{
			return this.ObjectPool.Enqueue(obj);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00036CAB File Offset: 0x00034EAB
		internal bool IsEmpty()
		{
			return this.Count <= 0L;
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00036CBA File Offset: 0x00034EBA
		protected long SafeCount
		{
			get
			{
				return this.ObjectPool.SafeCount;
			}
		}

		// Token: 0x04000AC2 RID: 2754
		public string LogSource = "";

		// Token: 0x04000AC3 RID: 2755
		public string PoolName = "";

		// Token: 0x04000AC4 RID: 2756
		internal long PoolSize;

		// Token: 0x04000AC5 RID: 2757
		private PoolQueue<T> ObjectPool;
	}
}
