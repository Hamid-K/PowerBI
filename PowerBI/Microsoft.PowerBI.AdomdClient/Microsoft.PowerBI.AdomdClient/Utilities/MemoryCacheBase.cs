using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014A RID: 330
	internal abstract class MemoryCacheBase
	{
		// Token: 0x0600105D RID: 4189 RVA: 0x0003860C File Offset: 0x0003680C
		protected MemoryCacheBase(MemoryCacheRetentionPolicy retentionPolicy)
		{
			this.retentionPolicy = retentionPolicy;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0003861B File Offset: 0x0003681B
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00038624 File Offset: 0x00036824
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

		// Token: 0x06001060 RID: 4192 RVA: 0x000386B8 File Offset: 0x000368B8
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

		// Token: 0x06001061 RID: 4193 RVA: 0x000386E8 File Offset: 0x000368E8
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

		// Token: 0x06001062 RID: 4194 RVA: 0x00038774 File Offset: 0x00036974
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

		// Token: 0x06001063 RID: 4195 RVA: 0x000387BC File Offset: 0x000369BC
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

		// Token: 0x06001064 RID: 4196
		protected abstract int GetItemCount();

		// Token: 0x06001065 RID: 4197
		protected abstract bool TryLocateInCache(string key, out object item, out DateTime utcExpiration);

		// Token: 0x06001066 RID: 4198
		protected abstract void UpdateExpirationTime(string key, DateTime utcExpiration);

		// Token: 0x06001067 RID: 4199
		protected abstract void InsertToCache(string key, object item, DateTime utcExpiration, int capacityLimit, out bool wasCacheEmpty, out object prevItem);

		// Token: 0x06001068 RID: 4200
		protected abstract bool RemoveFromCache(string key, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06001069 RID: 4201
		protected abstract bool EvictFromCache(string key, bool lockCache, DateTime? utcExpiration, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x0600106A RID: 4202
		protected abstract IEnumerable ClearCache();

		// Token: 0x0600106B RID: 4203
		protected abstract void SetEvictionTimer(Timer timer);

		// Token: 0x0600106C RID: 4204
		protected abstract void ResetEvictionTimer();

		// Token: 0x0600106D RID: 4205
		protected abstract IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo();

		// Token: 0x0600106E RID: 4206 RVA: 0x00038820 File Offset: 0x00036A20
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

		// Token: 0x0600106F RID: 4207 RVA: 0x000388C4 File Offset: 0x00036AC4
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

		// Token: 0x04000B08 RID: 2824
		private const int EvictionTimerIntervalInMilliseconds = 30000;

		// Token: 0x04000B09 RID: 2825
		private readonly MemoryCacheRetentionPolicy retentionPolicy;
	}
}
