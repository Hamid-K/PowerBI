using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.AzureClient.Utilities
{
	// Token: 0x0200002E RID: 46
	internal abstract class MemoryCacheBase
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00007388 File Offset: 0x00005588
		protected MemoryCacheBase(MemoryCacheRetentionPolicy retentionPolicy)
		{
			this.retentionPolicy = retentionPolicy;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007397 File Offset: 0x00005597
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000073A0 File Offset: 0x000055A0
		public bool Lookup(string key, out object item)
		{
			DateTime dateTime;
			if (!this.TryLocateInCache(key, out item, out dateTime))
			{
				return false;
			}
			if (this.retentionPolicy.HasActiveRetention)
			{
				bool flag;
				if (this.retentionPolicy.HasItemExpired(ref dateTime, out flag))
				{
					bool flag2;
					object obj;
					this.EvictFromCache(key, true, new DateTime?(dateTime), false, out flag2, out obj);
					if (flag2)
					{
						this.ResetEvictionTimer();
					}
					IDisposable disposable = item as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					item = null;
					return false;
				}
				if (flag)
				{
					this.UpdateExpirationTime(key, dateTime);
				}
			}
			else if (this.retentionPolicy.HasCapacityLimit)
			{
				this.UpdateExpirationTime(key, DateTime.UtcNow);
			}
			return true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007434 File Offset: 0x00005634
		public bool Lookup<TResult>(string key, out TResult item)
		{
			object obj;
			if (!this.Lookup(key, out obj))
			{
				item = default(TResult);
				return false;
			}
			item = (TResult)((object)obj);
			return true;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007464 File Offset: 0x00005664
		public void Insert(string key, object item)
		{
			DateTime utcNow;
			int num;
			this.retentionPolicy.GetCacheInsertionLimitation(out utcNow, out num);
			if (!this.retentionPolicy.HasActiveRetention && num > 0)
			{
				utcNow = DateTime.UtcNow;
			}
			bool flag;
			object obj;
			this.InsertToCache(key, item, utcNow, num, out flag, out obj);
			if (flag && this.retentionPolicy.HasActiveRetention)
			{
				this.SetEvictionTimer(new Timer(new TimerCallback(MemoryCacheBase.EvictionTimer), this, 30000, 30000));
			}
			if (obj != null)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000074F0 File Offset: 0x000056F0
		public bool Remove(string key, bool disposeItem = true)
		{
			bool flag;
			object obj;
			if (!this.RemoveFromCache(key, disposeItem, out flag, out obj))
			{
				return false;
			}
			if (flag && this.retentionPolicy.HasActiveRetention)
			{
				this.ResetEvictionTimer();
			}
			if (disposeItem)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return true;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007538 File Offset: 0x00005738
		public void Clear()
		{
			this.ResetEvictionTimer();
			foreach (object obj in this.ClearCache())
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06000171 RID: 369
		protected abstract int GetItemCount();

		// Token: 0x06000172 RID: 370
		protected abstract bool TryLocateInCache(string key, out object item, out DateTime utcExpiration);

		// Token: 0x06000173 RID: 371
		protected abstract void UpdateExpirationTime(string key, DateTime utcExpiration);

		// Token: 0x06000174 RID: 372
		protected abstract void InsertToCache(string key, object item, DateTime utcExpiration, int capacityLimit, out bool wasCacheEmpty, out object prevItem);

		// Token: 0x06000175 RID: 373
		protected abstract bool RemoveFromCache(string key, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06000176 RID: 374
		protected abstract bool EvictFromCache(string key, bool lockCache, DateTime? utcExpiration, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06000177 RID: 375
		protected abstract IEnumerable ClearCache();

		// Token: 0x06000178 RID: 376
		protected abstract void SetEvictionTimer(Timer timer);

		// Token: 0x06000179 RID: 377
		protected abstract void ResetEvictionTimer();

		// Token: 0x0600017A RID: 378
		protected abstract IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo();

		// Token: 0x0600017B RID: 379 RVA: 0x0000759C File Offset: 0x0000579C
		protected void ReduceCacheSize(int itemsToRemove, IList<KeyValuePair<string, DateTime>> expirationInfo)
		{
			while (itemsToRemove > 0)
			{
				int num = 0;
				for (int i = 1; i < expirationInfo.Count; i++)
				{
					if (expirationInfo[num].Value.CompareTo(expirationInfo[i].Value) > 0)
					{
						num = i;
					}
				}
				bool flag;
				object obj;
				if (this.EvictFromCache(expirationInfo[num].Key, false, null, true, out flag, out obj))
				{
					IDisposable disposable = obj as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				if (itemsToRemove > 1)
				{
					expirationInfo.RemoveAt(num);
				}
				itemsToRemove--;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007640 File Offset: 0x00005840
		private static void EvictionTimer(object context)
		{
			MemoryCacheBase memoryCacheBase = (MemoryCacheBase)context;
			IList<KeyValuePair<string, DateTime>> itemsExpirationInfo = memoryCacheBase.GetItemsExpirationInfo();
			if (itemsExpirationInfo.Count == 0)
			{
				memoryCacheBase.ResetEvictionTimer();
			}
			foreach (KeyValuePair<string, DateTime> keyValuePair in itemsExpirationInfo)
			{
				DateTime value = keyValuePair.Value;
				bool flag;
				if (memoryCacheBase.retentionPolicy.HasItemExpired(ref value, out flag))
				{
					bool flag2;
					object obj;
					if (memoryCacheBase.EvictFromCache(keyValuePair.Key, true, new DateTime?(value), true, out flag2, out obj))
					{
						IDisposable disposable = obj as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					if (flag2)
					{
						memoryCacheBase.ResetEvictionTimer();
						break;
					}
				}
			}
		}

		// Token: 0x040000D3 RID: 211
		private const int EvictionTimerIntervalInMilliseconds = 30000;

		// Token: 0x040000D4 RID: 212
		private readonly MemoryCacheRetentionPolicy retentionPolicy;
	}
}
