using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x0200013F RID: 319
	internal abstract class MemoryCacheBase
	{
		// Token: 0x060010F8 RID: 4344 RVA: 0x0003B240 File Offset: 0x00039440
		protected MemoryCacheBase(MemoryCacheRetentionPolicy retentionPolicy)
		{
			this.retentionPolicy = retentionPolicy;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0003B24F File Offset: 0x0003944F
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003B258 File Offset: 0x00039458
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

		// Token: 0x060010FB RID: 4347 RVA: 0x0003B2EC File Offset: 0x000394EC
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

		// Token: 0x060010FC RID: 4348 RVA: 0x0003B31C File Offset: 0x0003951C
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

		// Token: 0x060010FD RID: 4349 RVA: 0x0003B3A8 File Offset: 0x000395A8
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

		// Token: 0x060010FE RID: 4350 RVA: 0x0003B3F0 File Offset: 0x000395F0
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

		// Token: 0x060010FF RID: 4351
		protected abstract int GetItemCount();

		// Token: 0x06001100 RID: 4352
		protected abstract bool TryLocateInCache(string key, out object item, out DateTime utcExpiration);

		// Token: 0x06001101 RID: 4353
		protected abstract void UpdateExpirationTime(string key, DateTime utcExpiration);

		// Token: 0x06001102 RID: 4354
		protected abstract void InsertToCache(string key, object item, DateTime utcExpiration, int capacityLimit, out bool wasCacheEmpty, out object prevItem);

		// Token: 0x06001103 RID: 4355
		protected abstract bool RemoveFromCache(string key, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06001104 RID: 4356
		protected abstract bool EvictFromCache(string key, bool lockCache, DateTime? utcExpiration, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06001105 RID: 4357
		protected abstract IEnumerable ClearCache();

		// Token: 0x06001106 RID: 4358
		protected abstract void SetEvictionTimer(Timer timer);

		// Token: 0x06001107 RID: 4359
		protected abstract void ResetEvictionTimer();

		// Token: 0x06001108 RID: 4360
		protected abstract IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo();

		// Token: 0x06001109 RID: 4361 RVA: 0x0003B454 File Offset: 0x00039654
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

		// Token: 0x0600110A RID: 4362 RVA: 0x0003B4F8 File Offset: 0x000396F8
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

		// Token: 0x04000ACE RID: 2766
		private const int EvictionTimerIntervalInMilliseconds = 30000;

		// Token: 0x04000ACF RID: 2767
		private readonly MemoryCacheRetentionPolicy retentionPolicy;
	}
}
