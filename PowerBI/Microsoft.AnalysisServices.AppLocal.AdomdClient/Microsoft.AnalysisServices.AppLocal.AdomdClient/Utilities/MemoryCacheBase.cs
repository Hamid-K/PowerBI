using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x0200014A RID: 330
	internal abstract class MemoryCacheBase
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x0003893C File Offset: 0x00036B3C
		protected MemoryCacheBase(MemoryCacheRetentionPolicy retentionPolicy)
		{
			this.retentionPolicy = retentionPolicy;
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0003894B File Offset: 0x00036B4B
		public int Count
		{
			get
			{
				return this.GetItemCount();
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00038954 File Offset: 0x00036B54
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

		// Token: 0x0600106D RID: 4205 RVA: 0x000389E8 File Offset: 0x00036BE8
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

		// Token: 0x0600106E RID: 4206 RVA: 0x00038A18 File Offset: 0x00036C18
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

		// Token: 0x0600106F RID: 4207 RVA: 0x00038AA4 File Offset: 0x00036CA4
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

		// Token: 0x06001070 RID: 4208 RVA: 0x00038AEC File Offset: 0x00036CEC
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

		// Token: 0x06001071 RID: 4209
		protected abstract int GetItemCount();

		// Token: 0x06001072 RID: 4210
		protected abstract bool TryLocateInCache(string key, out object item, out DateTime utcExpiration);

		// Token: 0x06001073 RID: 4211
		protected abstract void UpdateExpirationTime(string key, DateTime utcExpiration);

		// Token: 0x06001074 RID: 4212
		protected abstract void InsertToCache(string key, object item, DateTime utcExpiration, int capacityLimit, out bool wasCacheEmpty, out object prevItem);

		// Token: 0x06001075 RID: 4213
		protected abstract bool RemoveFromCache(string key, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06001076 RID: 4214
		protected abstract bool EvictFromCache(string key, bool lockCache, DateTime? utcExpiration, bool returnItem, out bool isCacheEmpty, out object item);

		// Token: 0x06001077 RID: 4215
		protected abstract IEnumerable ClearCache();

		// Token: 0x06001078 RID: 4216
		protected abstract void SetEvictionTimer(Timer timer);

		// Token: 0x06001079 RID: 4217
		protected abstract void ResetEvictionTimer();

		// Token: 0x0600107A RID: 4218
		protected abstract IList<KeyValuePair<string, DateTime>> GetItemsExpirationInfo();

		// Token: 0x0600107B RID: 4219 RVA: 0x00038B50 File Offset: 0x00036D50
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

		// Token: 0x0600107C RID: 4220 RVA: 0x00038BF4 File Offset: 0x00036DF4
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

		// Token: 0x04000B15 RID: 2837
		private const int EvictionTimerIntervalInMilliseconds = 30000;

		// Token: 0x04000B16 RID: 2838
		private readonly MemoryCacheRetentionPolicy retentionPolicy;
	}
}
