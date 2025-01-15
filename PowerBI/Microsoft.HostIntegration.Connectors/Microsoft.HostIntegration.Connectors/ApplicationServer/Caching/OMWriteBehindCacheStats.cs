using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200028B RID: 651
	internal sealed class OMWriteBehindCacheStats
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x000481AB File Offset: 0x000463AB
		internal OMWriteBehindCacheStats(string cacheName)
		{
			this._cacheName = cacheName;
			this._flushedItemCount = new SafeValueStore();
			this._droppedItemCount = new SafeValueStore();
			this._wbQueueCount = new SafeValueStore();
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000481DC File Offset: 0x000463DC
		internal void InitializePerfCounters()
		{
			this._flushedItemCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.FLUSHED_ITEM_COUNT, this._cacheName, new PerfCounterValue(this.GetFlushedItemCount));
			this._droppedtemCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.DROPPED_ITEM_COUNT, this._cacheName, new PerfCounterValue(this.GetDroppedItemCount));
			this._wbQueueCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.WB_QUEUE_ITEM_COUNT, this._cacheName, new PerfCounterValue(this.GetWBQueueCount));
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00048246 File Offset: 0x00046446
		internal void IncreaseFlushedItemCountBy(int increase)
		{
			this._flushedItemCount.Add((long)increase);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00048255 File Offset: 0x00046455
		internal void IncreaseDroppedItemCountBy(int increase)
		{
			this._droppedItemCount.Add((long)increase);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00048264 File Offset: 0x00046464
		internal void IncreaseWBQueueCountBy(int increase)
		{
			this._wbQueueCount.Add((long)increase);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00048273 File Offset: 0x00046473
		internal long GetFlushedItemCount()
		{
			return this._flushedItemCount.GetValue();
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00048280 File Offset: 0x00046480
		internal long GetDroppedItemCount()
		{
			return this._droppedItemCount.GetValue();
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0004828D File Offset: 0x0004648D
		internal long GetWBQueueCount()
		{
			return this._wbQueueCount.GetValue();
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0004829A File Offset: 0x0004649A
		internal void Delete()
		{
			this._flushedItemCounter.Delete();
			this._droppedtemCounter.Delete();
			this._wbQueueCounter.Delete();
		}

		// Token: 0x04000D2C RID: 3372
		private string _cacheName;

		// Token: 0x04000D2D RID: 3373
		private IValueStore _flushedItemCount;

		// Token: 0x04000D2E RID: 3374
		private IValueStore _droppedItemCount;

		// Token: 0x04000D2F RID: 3375
		private IValueStore _wbQueueCount;

		// Token: 0x04000D30 RID: 3376
		private DelegateBasedCachePerfCounter _flushedItemCounter;

		// Token: 0x04000D31 RID: 3377
		private DelegateBasedCachePerfCounter _droppedtemCounter;

		// Token: 0x04000D32 RID: 3378
		private DelegateBasedCachePerfCounter _wbQueueCounter;
	}
}
