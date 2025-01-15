using System;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Threading;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x0200062E RID: 1582
	internal class QueryCacheManager : IDisposable
	{
		// Token: 0x06004C34 RID: 19508 RVA: 0x0010C368 File Offset: 0x0010A568
		internal static QueryCacheManager Create()
		{
			QueryCacheConfig queryCache = AppConfig.DefaultInstance.QueryCache;
			int queryCacheSize = queryCache.GetQueryCacheSize();
			int num = queryCache.GetCleaningIntervalInSeconds() * 1000;
			return new QueryCacheManager(queryCacheSize, 0.8f, num);
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x0010C3A0 File Offset: 0x0010A5A0
		private QueryCacheManager(int maximumSize, float loadFactor, int recycleMillis)
		{
			this._maxNumberOfEntries = maximumSize;
			this._sweepingTriggerHighMark = (int)((float)this._maxNumberOfEntries * loadFactor);
			this._evictionTimer = new QueryCacheManager.EvictionTimer(this, recycleMillis);
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x0010C3F0 File Offset: 0x0010A5F0
		internal bool TryLookupAndAdd(QueryCacheEntry inQueryCacheEntry, out QueryCacheEntry outQueryCacheEntry)
		{
			outQueryCacheEntry = null;
			object cacheDataLock = this._cacheDataLock;
			bool flag2;
			lock (cacheDataLock)
			{
				if (!this._cacheData.TryGetValue(inQueryCacheEntry.QueryCacheKey, out outQueryCacheEntry))
				{
					this._cacheData.Add(inQueryCacheEntry.QueryCacheKey, inQueryCacheEntry);
					if (this._cacheData.Count > this._sweepingTriggerHighMark)
					{
						this._evictionTimer.Start();
					}
					flag2 = false;
				}
				else
				{
					outQueryCacheEntry.QueryCacheKey.UpdateHit();
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x0010C484 File Offset: 0x0010A684
		internal bool TryCacheLookup<TK, TE>(TK key, out TE value) where TK : QueryCacheKey
		{
			value = default(TE);
			QueryCacheEntry queryCacheEntry = null;
			bool flag = this.TryInternalCacheLookup(key, out queryCacheEntry);
			if (flag)
			{
				value = (TE)((object)queryCacheEntry.GetTarget());
			}
			return flag;
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x0010C4BC File Offset: 0x0010A6BC
		internal void Clear()
		{
			object cacheDataLock = this._cacheDataLock;
			lock (cacheDataLock)
			{
				this._cacheData.Clear();
			}
		}

		// Token: 0x06004C39 RID: 19513 RVA: 0x0010C504 File Offset: 0x0010A704
		private bool TryInternalCacheLookup(QueryCacheKey queryCacheKey, out QueryCacheEntry queryCacheEntry)
		{
			queryCacheEntry = null;
			bool flag = false;
			object cacheDataLock = this._cacheDataLock;
			lock (cacheDataLock)
			{
				flag = this._cacheData.TryGetValue(queryCacheKey, out queryCacheEntry);
			}
			if (flag)
			{
				queryCacheEntry.QueryCacheKey.UpdateHit();
			}
			return flag;
		}

		// Token: 0x06004C3A RID: 19514 RVA: 0x0010C564 File Offset: 0x0010A764
		private static void CacheRecyclerHandler(object state)
		{
			((QueryCacheManager)state).SweepCache();
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x0010C574 File Offset: 0x0010A774
		private void SweepCache()
		{
			if (!this._evictionTimer.Suspend())
			{
				return;
			}
			bool flag = false;
			object cacheDataLock = this._cacheDataLock;
			lock (cacheDataLock)
			{
				if (this._cacheData.Count > this._sweepingTriggerHighMark)
				{
					uint num = 0U;
					List<QueryCacheKey> list = new List<QueryCacheKey>(this._cacheData.Count);
					list.AddRange(this._cacheData.Keys);
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].HitCount == 0U)
						{
							this._cacheData.Remove(list[i]);
							num += 1U;
						}
						else
						{
							int num2 = list[i].AgingIndex + 1;
							if (num2 > QueryCacheManager._agingMaxIndex)
							{
								num2 = QueryCacheManager._agingMaxIndex;
							}
							list[i].AgingIndex = num2;
							list[i].HitCount = list[i].HitCount >> QueryCacheManager._agingFactor[num2];
						}
					}
				}
				else
				{
					this._evictionTimer.Stop();
					flag = true;
				}
			}
			if (!flag)
			{
				this._evictionTimer.Resume();
			}
		}

		// Token: 0x06004C3C RID: 19516 RVA: 0x0010C6BC File Offset: 0x0010A8BC
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			if (this._evictionTimer.Stop())
			{
				this.Clear();
			}
		}

		// Token: 0x04001AAA RID: 6826
		private readonly object _cacheDataLock = new object();

		// Token: 0x04001AAB RID: 6827
		private readonly Dictionary<QueryCacheKey, QueryCacheEntry> _cacheData = new Dictionary<QueryCacheKey, QueryCacheEntry>(32);

		// Token: 0x04001AAC RID: 6828
		private readonly int _maxNumberOfEntries;

		// Token: 0x04001AAD RID: 6829
		private readonly int _sweepingTriggerHighMark;

		// Token: 0x04001AAE RID: 6830
		private readonly QueryCacheManager.EvictionTimer _evictionTimer;

		// Token: 0x04001AAF RID: 6831
		private static readonly int[] _agingFactor = new int[] { 1, 1, 2, 4, 8, 16 };

		// Token: 0x04001AB0 RID: 6832
		private static readonly int _agingMaxIndex = QueryCacheManager._agingFactor.Length - 1;

		// Token: 0x02000C59 RID: 3161
		private sealed class EvictionTimer
		{
			// Token: 0x06006AA1 RID: 27297 RVA: 0x0016C2FC File Offset: 0x0016A4FC
			internal EvictionTimer(QueryCacheManager cacheManager, int recyclePeriod)
			{
				this._cacheManager = cacheManager;
				this._period = recyclePeriod;
			}

			// Token: 0x06006AA2 RID: 27298 RVA: 0x0016C320 File Offset: 0x0016A520
			internal void Start()
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._timer == null)
					{
						this._timer = new Timer(new TimerCallback(QueryCacheManager.CacheRecyclerHandler), this._cacheManager, this._period, this._period);
					}
				}
			}

			// Token: 0x06006AA3 RID: 27299 RVA: 0x0016C38C File Offset: 0x0016A58C
			internal bool Stop()
			{
				object sync = this._sync;
				bool flag2;
				lock (sync)
				{
					if (this._timer != null)
					{
						this._timer.Dispose();
						this._timer = null;
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x06006AA4 RID: 27300 RVA: 0x0016C3E8 File Offset: 0x0016A5E8
			internal bool Suspend()
			{
				object sync = this._sync;
				bool flag2;
				lock (sync)
				{
					if (this._timer != null)
					{
						this._timer.Change(-1, -1);
						flag2 = true;
					}
					else
					{
						flag2 = false;
					}
				}
				return flag2;
			}

			// Token: 0x06006AA5 RID: 27301 RVA: 0x0016C440 File Offset: 0x0016A640
			internal void Resume()
			{
				object sync = this._sync;
				lock (sync)
				{
					if (this._timer != null)
					{
						this._timer.Change(this._period, this._period);
					}
				}
			}

			// Token: 0x040030D9 RID: 12505
			private readonly object _sync = new object();

			// Token: 0x040030DA RID: 12506
			private readonly int _period;

			// Token: 0x040030DB RID: 12507
			private readonly QueryCacheManager _cacheManager;

			// Token: 0x040030DC RID: 12508
			private Timer _timer;
		}
	}
}
