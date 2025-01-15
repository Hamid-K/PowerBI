using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A2 RID: 162
	internal sealed class EvictionManager : IDisposable
	{
		// Token: 0x060003B5 RID: 949 RVA: 0x00012E84 File Offset: 0x00011084
		public EvictionManager(LocalCacheStore lcs, EvictionParametrs prop)
		{
			this._localStore = lcs;
			this._evictionProp = prop;
			this._memoryPressureMonitor = new ClientMemoryPressureMonitor(new ClientMemoryPressureCallback(this.OnMemoryPressure));
			this._evictionGenerations = EvictionGenerations.GetEvictionGenerations(prop.ExpiryInterval, null);
			this._lWmCountLocalCacheItem = prop.HWObjectCount - (long)((double)prop.HWObjectCount * ((double)prop.PcntCleanup / 100.0));
			this._expirationTimerCallback = new TimerCallback(this.RemoveExpiredObjectCallback);
			this._evictionTimerCallback = new TimerCallback(this.RunPeriodicEviction);
			this.InitializePeriodicTimers(this._evictionProp.ExpiryInterval, this._evictionProp.EvictionInterval);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00012F4C File Offset: 0x0001114C
		public void Dispose()
		{
			this._memoryPressureMonitor.Dispose();
			this._expirationTimer.Dispose();
			this._evictionTimer.Dispose();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012F70 File Offset: 0x00011170
		internal void OnMemoryPressure(ClientMemoryPressureLevel memPressure)
		{
			if (this.TakeEvictionInProgressLatch())
			{
				try
				{
					long num = this._localStore.ActualItemCount();
					this._localStore.CountLocalCacheItem = num;
					long num2 = (long)((double)num * ((double)this._evictionProp.PcntCleanup / 100.0));
					if (num2 > 0L)
					{
						long num3 = num - num2;
						this.EvictExpireCompact(num2, num3);
					}
				}
				finally
				{
					this.ReleaseEvictionInProgressLatch();
				}
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012FE4 File Offset: 0x000111E4
		internal void OnCacheItemCountExceeded()
		{
			this.TryQueueEvictionItem();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012FF0 File Offset: 0x000111F0
		public void RemoveExpiredObjectCallback(object ignoredState)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Expiry Callback starting");
			}
			if (!this.TakeEvictionInProgressLatch())
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose(this._logSource, "Expiry thread exiting: could not take the latch.");
				}
				return;
			}
			try
			{
				if (this.TimeSinceLastEviction() > this._evictionProp.ExpiryInterval)
				{
					this.RemoveExpiredObjects();
					if (this._localStore.CountItemRemovedSinceCompaction >= this._itemToRemoveBeforeCompaction)
					{
						this._localStore.ObjectManager.DoCompaction();
						this._localStore.CountItemRemovedSinceCompaction = 0L;
					}
				}
			}
			finally
			{
				this.ReleaseEvictionInProgressLatch();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x000130A0 File Offset: 0x000112A0
		public void RunPeriodicEviction(object state)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Running periodic eviction");
			}
			if (this.TimeSinceLastEviction() >= this._evictionProp.EvictionInterval)
			{
				this.TryQueueEvictionItem();
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000130DC File Offset: 0x000112DC
		public void OnCacheItemCountChange()
		{
			long countLocalCacheItem = this._localStore.CountLocalCacheItem;
			if (countLocalCacheItem > this._evictionProp.HWObjectCount)
			{
				this.OnCacheItemCountExceeded();
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001310C File Offset: 0x0001130C
		public void EvictExpireWorkItem(object state)
		{
			try
			{
				long num = this._localStore.ActualItemCount();
				this._localStore.CountLocalCacheItem = num;
				if (num - this._evictionProp.HWObjectCount > 0L)
				{
					this.EvictExpireCompact(num - this._lWmCountLocalCacheItem, this._lWmCountLocalCacheItem);
				}
			}
			finally
			{
				this.ReleaseEvictionInProgressLatch();
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013170 File Offset: 0x00011370
		public void EvictExpireCompact(long countItemsToEvict, long lwmCountLocalCacheItem)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "InsideEvictExpireCompact");
			}
			countItemsToEvict -= this.RemoveExpiredObjects();
			if (countItemsToEvict > 0L)
			{
				this.EvictExpire(countItemsToEvict, lwmCountLocalCacheItem);
			}
			if (this._localStore.CountItemRemovedSinceCompaction > this._itemToRemoveBeforeCompaction)
			{
				this._localStore.ObjectManager.DoCompaction();
				this._localStore.CountItemRemovedSinceCompaction = 0L;
			}
			this._lastEvictionRun = DateTime.Now;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000131EC File Offset: 0x000113EC
		public long EvictExpire(long countItemToEvict, long lWmCountLocalCacheItem)
		{
			long num = 0L;
			EvictionCandidateSupplier evictionCandidateSupplier = new EvictionCandidateSupplier(this._localStore.GetRegionNames(), this._localStore.ObjectManager, this._evictionGenerations, null, null);
			IEnumerator<AOMCacheItem> enumerator = evictionCandidateSupplier.StartEnumeration(countItemToEvict, EvictExpireState.EVICT);
			while (this._localStore.CountLocalCacheItem > lWmCountLocalCacheItem && enumerator.MoveNext())
			{
				AOMCacheItem aomcacheItem = enumerator.Current;
				try
				{
					OMRegion omregion = aomcacheItem.Region as OMRegion;
					if (omregion.InternalDelete(aomcacheItem.Key, aomcacheItem.Version, null) != null)
					{
						num += 1L;
						countItemToEvict -= 1L;
					}
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode != 2009)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError(this._logSource, "Unrecoverable exception after evicting {0} items, current Items: {1}, Exception {2}", new object[]
							{
								num,
								this._localStore.CountLocalCacheItem,
								ex
							});
						}
						throw;
					}
				}
			}
			this._evictionGenerations = evictionCandidateSupplier.GetUpdatedStatistics();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "{0} Items Evicted, , Total count: {1}", new object[]
				{
					num,
					this._localStore.CountLocalCacheItem
				});
			}
			return num;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013338 File Offset: 0x00011538
		public long RemoveExpiredObjects()
		{
			long num = 0L;
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose(this._logSource, "Inside RemoveExpiredObjects");
			}
			EvictionCandidateSupplier evictionCandidateSupplier = new EvictionCandidateSupplier(this._localStore.GetRegionNames(), this._localStore.ObjectManager, this._evictionGenerations, null, null);
			IEnumerator<AOMCacheItem> enumerator = evictionCandidateSupplier.StartEnumeration(2147483647L, EvictExpireState.EXPIRE);
			while (enumerator.MoveNext())
			{
				AOMCacheItem aomcacheItem = enumerator.Current;
				try
				{
					OMRegion omregion = aomcacheItem.Region as OMRegion;
					if (omregion.InternalDelete(aomcacheItem.Key, aomcacheItem.Version, null) != null)
					{
						num += 1L;
					}
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode != 2009)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError(this._logSource, "Unrecoverable exception after expiring {0} items, current Items: {1}, Exception {2}", new object[]
							{
								num,
								this._localStore.CountLocalCacheItem,
								ex
							});
						}
						throw;
					}
				}
			}
			this._evictionGenerations = evictionCandidateSupplier.GetUpdatedStatistics();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(this._logSource, "{0} Items Expired, Total count: {1}", new object[]
				{
					num,
					this._localStore.CountLocalCacheItem
				});
			}
			return num;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001348C File Offset: 0x0001168C
		private TimeSpan TimeSinceLastEviction()
		{
			return DateTime.Now - this._lastEvictionRun;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001349E File Offset: 0x0001169E
		private void InitializePeriodicTimers(TimeSpan expiryInterval, TimeSpan evictionInterval)
		{
			this._expirationTimer = new global::System.Threading.Timer(this._expirationTimerCallback, null, expiryInterval, expiryInterval);
			this._evictionTimer = new global::System.Threading.Timer(this._evictionTimerCallback, null, evictionInterval, evictionInterval);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000134C8 File Offset: 0x000116C8
		private bool TakeEvictionInProgressLatch()
		{
			return Interlocked.CompareExchange(ref this._evictionInProgress, 1, 0) == 0;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000134DC File Offset: 0x000116DC
		private void ReleaseEvictionInProgressLatch()
		{
			Interlocked.CompareExchange(ref this._evictionInProgress, 0, 1);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000134EC File Offset: 0x000116EC
		internal bool TryQueueEvictionItem()
		{
			bool flag = false;
			if (this.TakeEvictionInProgressLatch())
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(this.EvictExpireWorkItem), null);
				flag = true;
			}
			return flag;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00013519 File Offset: 0x00011719
		internal EvictionParametrs EvictionConfiguration
		{
			get
			{
				return this._evictionProp;
			}
		}

		// Token: 0x040002E5 RID: 741
		private EvictionParametrs _evictionProp;

		// Token: 0x040002E6 RID: 742
		private LocalCacheStore _localStore;

		// Token: 0x040002E7 RID: 743
		private ClientMemoryPressureMonitor _memoryPressureMonitor;

		// Token: 0x040002E8 RID: 744
		private EvictionGenerations _evictionGenerations;

		// Token: 0x040002E9 RID: 745
		private int _evictionInProgress;

		// Token: 0x040002EA RID: 746
		private long _lWmCountLocalCacheItem;

		// Token: 0x040002EB RID: 747
		private global::System.Threading.Timer _expirationTimer;

		// Token: 0x040002EC RID: 748
		private global::System.Threading.Timer _evictionTimer;

		// Token: 0x040002ED RID: 749
		private long _itemToRemoveBeforeCompaction = 100000L;

		// Token: 0x040002EE RID: 750
		private TimerCallback _expirationTimerCallback;

		// Token: 0x040002EF RID: 751
		private TimerCallback _evictionTimerCallback;

		// Token: 0x040002F0 RID: 752
		private DateTime _lastEvictionRun;

		// Token: 0x040002F1 RID: 753
		private string _logSource = "LocalCacheEviction";
	}
}
