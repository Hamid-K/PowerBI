using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200028A RID: 650
	internal class OMRegionStats
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x00047E07 File Offset: 0x00046007
		internal OMNamedCacheStats NamedCacheStats
		{
			get
			{
				return this._cacheStats;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x00047E0F File Offset: 0x0004600F
		public long Size
		{
			get
			{
				return this._size.GetValue();
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x00047E1C File Offset: 0x0004601C
		public long Count
		{
			get
			{
				return this._count.GetValue();
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x00047E29 File Offset: 0x00046029
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x00047E36 File Offset: 0x00046036
		public long Miss
		{
			get
			{
				return this._miss.GetValue();
			}
			internal set
			{
				this._miss.SetValue(value);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x00047E44 File Offset: 0x00046044
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x00047E51 File Offset: 0x00046051
		public long TotalReqs
		{
			get
			{
				return this._totalReqs.GetValue();
			}
			internal set
			{
				this._totalReqs.SetValue(value);
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00047E5F File Offset: 0x0004605F
		public void TotalReqsIncr()
		{
			this._totalReqs.Increment();
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00047E6C File Offset: 0x0004606C
		// (set) Token: 0x060017A8 RID: 6056 RVA: 0x00047E79 File Offset: 0x00046079
		public long TotalRestReqs
		{
			get
			{
				return this._totalRestReqs.GetValue();
			}
			internal set
			{
				this._totalRestReqs.SetValue(value);
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00047E87 File Offset: 0x00046087
		public void TotalRestReqsIncr()
		{
			this._totalRestReqs.Increment();
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x00047E94 File Offset: 0x00046094
		// (set) Token: 0x060017AB RID: 6059 RVA: 0x00047EA1 File Offset: 0x000460A1
		public long GetReqs
		{
			get
			{
				return this._getReqs.GetValue();
			}
			internal set
			{
				this._getReqs.SetValue(value);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x00047EAF File Offset: 0x000460AF
		// (set) Token: 0x060017AD RID: 6061 RVA: 0x00047EBC File Offset: 0x000460BC
		public long AddReqs
		{
			get
			{
				return this._addReqs.GetValue();
			}
			internal set
			{
				this._addReqs.SetValue(value);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x00047ECA File Offset: 0x000460CA
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x00047ED7 File Offset: 0x000460D7
		public long UpsertReqs
		{
			get
			{
				return this._upsertReqs.GetValue();
			}
			internal set
			{
				this._upsertReqs.SetValue(value);
			}
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00047EE5 File Offset: 0x000460E5
		public void UpsertReqsIncr()
		{
			this._upsertReqs.Increment();
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00047EF2 File Offset: 0x000460F2
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x00047EFF File Offset: 0x000460FF
		public long DelReqs
		{
			get
			{
				return this._delReqs.GetValue();
			}
			internal set
			{
				this._delReqs.SetValue(value);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x00047F0D File Offset: 0x0004610D
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x00047F1A File Offset: 0x0004611A
		public long EvictReqs
		{
			get
			{
				return this._evictReqs.GetValue();
			}
			internal set
			{
				this._evictReqs.SetValue(value);
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00047F28 File Offset: 0x00046128
		public OMRegionStats(OMNamedCacheStats cacheStats)
		{
			this._cacheStats = cacheStats;
			this._addReqs = new SafeValueStore();
			this._count = new SafeValueStore();
			this._delReqs = new SafeValueStore();
			this._evictReqs = new SafeValueStore();
			this._getReqs = new SafeValueStore();
			this._miss = new SafeValueStore();
			this._size = new SafeValueStore();
			this._totalReqs = new SafeValueStore();
			this._totalRestReqs = new SafeValueStore();
			this._upsertReqs = new SafeValueStore();
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00047FB0 File Offset: 0x000461B0
		internal void IncrCount()
		{
			this._count.Increment();
			this._cacheStats.IncrCount();
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x00047FC8 File Offset: 0x000461C8
		internal void DecrCount()
		{
			this._count.Decrement();
			if (this._count.GetValue() < 0L && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("OMRegionStats", "OMRegionStats.Count is negative. Current: {0}, Last increment: -1", new object[] { this._count.GetValue() });
			}
			this._cacheStats.DecrCount();
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0004802C File Offset: 0x0004622C
		internal void IncrWBCount()
		{
			this._cacheStats.IncrWBCount();
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x00048039 File Offset: 0x00046239
		internal void DecrWBCount()
		{
			this._cacheStats.DecrWBCount();
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00048046 File Offset: 0x00046246
		internal void IncrSize(int size)
		{
			this._size.Add((long)size);
			this._cacheStats.IncrSize((long)size);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00048064 File Offset: 0x00046264
		internal void DecrSize(int size)
		{
			this._size.Add((long)(-(long)size));
			if (this._size.GetValue() < 0L && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("OMRegionStats", "OMRegionStats.Size is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._size.GetValue(),
					-size
				});
			}
			this._cacheStats.DecrSize((long)size);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x000480D7 File Offset: 0x000462D7
		internal void IncrMiss()
		{
			this._miss.Increment();
			this._cacheStats.IncrMiss();
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x000480EF File Offset: 0x000462EF
		internal void IncrTotalReqs()
		{
			this._totalReqs.Increment();
			this._cacheStats.IncrTotalReqs();
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00048107 File Offset: 0x00046307
		internal void IncrGetReqs()
		{
			this._getReqs.Increment();
			this._totalReqs.Increment();
			this._cacheStats.IncrGetReqs();
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0004812A File Offset: 0x0004632A
		internal void IncrAddReqs()
		{
			this._addReqs.Increment();
			this._totalReqs.Increment();
			this._cacheStats.IncrAddReqs();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0004814D File Offset: 0x0004634D
		internal void IncrDelReqs()
		{
			this._delReqs.Increment();
			this._totalReqs.Increment();
			this._cacheStats.IncrDelReqs();
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00048170 File Offset: 0x00046370
		internal void IncrUpsertReqs()
		{
			this._upsertReqs.Increment();
			this._totalReqs.Increment();
			this._cacheStats.IncrUpsertReqs();
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00048193 File Offset: 0x00046393
		internal void IncrEvictReqs()
		{
			this._evictReqs.Increment();
			this._cacheStats.IncrEvictReqs();
		}

		// Token: 0x04000D21 RID: 3361
		private readonly OMNamedCacheStats _cacheStats;

		// Token: 0x04000D22 RID: 3362
		private IValueStore _size;

		// Token: 0x04000D23 RID: 3363
		private IValueStore _count;

		// Token: 0x04000D24 RID: 3364
		private IValueStore _miss;

		// Token: 0x04000D25 RID: 3365
		private IValueStore _totalReqs;

		// Token: 0x04000D26 RID: 3366
		private IValueStore _totalRestReqs;

		// Token: 0x04000D27 RID: 3367
		private IValueStore _getReqs;

		// Token: 0x04000D28 RID: 3368
		private IValueStore _addReqs;

		// Token: 0x04000D29 RID: 3369
		private IValueStore _upsertReqs;

		// Token: 0x04000D2A RID: 3370
		private IValueStore _delReqs;

		// Token: 0x04000D2B RID: 3371
		private IValueStore _evictReqs;
	}
}
