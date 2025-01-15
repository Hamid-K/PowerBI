using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000020 RID: 32
	public abstract class DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType> : IIdentifiable where EntityRecordType : EntityRecord
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003DC8 File Offset: 0x00001FC8
		protected DatabaseClientCacheBase(IDictionary<CacheEnumerator, int> maxNumberOfRecordInCaches, IEnumerable<CacheEnumerator> pools, string databaseClientName)
		{
			this.m_lock = new object();
			this.m_pools = new Dictionary<CacheEnumerator, QuickAccessPool<KeyType, EntityRecordType>>();
			foreach (CacheEnumerator cacheEnumerator in pools)
			{
				this.m_pools.Add(cacheEnumerator, new QuickAccessPool<KeyType, EntityRecordType>(maxNumberOfRecordInCaches[cacheEnumerator], PoolPolicy.PreferMostRecentlyUsed));
			}
			this.Name = "Cache_{0}".FormatWithInvariantCulture(new object[] { databaseClientName });
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003E58 File Offset: 0x00002058
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003E60 File Offset: 0x00002060
		public string Name { get; private set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x00003E6C File Offset: 0x0000206C
		public void Clear()
		{
			IDictionary<CacheEnumerator, QuickAccessPool<KeyType, EntityRecordType>> pools = this.m_pools;
			lock (pools)
			{
				foreach (QuickAccessPool<KeyType, EntityRecordType> quickAccessPool in this.m_pools.Values)
				{
					quickAccessPool.Clear();
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003EE4 File Offset: 0x000020E4
		protected IAsyncResult BeginResolveOperation<TKey, TValue>(CacheEnumerator cache, TKey key, Sequencer.AsyncBeginFunction<TKey> op, AsyncCallback asyncCallback, object asyncState) where TValue : EntityRecordType
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				EntityRecordType entityRecordType;
				if (this.m_pools[cache].TryTouch(this.GetKey(key), out entityRecordType))
				{
					TValue tvalue = entityRecordType as TValue;
					return new DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<TValue>(asyncCallback, asyncState, tvalue);
				}
			}
			return op(key, asyncCallback, asyncState);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F6C File Offset: 0x0000216C
		protected T EndResolveOperation<T>(CacheEnumerator cache, IAsyncResult asyncResult, DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.EndFunction<T> op) where T : EntityRecordType
		{
			DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<T> cacheCompletedAsyncResult = asyncResult as DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<T>;
			if (cacheCompletedAsyncResult != null)
			{
				return cacheCompletedAsyncResult.End();
			}
			return this.EndCreateOrUpdateOperation<T>(cache, asyncResult, op);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F94 File Offset: 0x00002194
		protected T EndCreateOrUpdateOperation<T>(CacheEnumerator cache, IAsyncResult asyncResult, DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.EndFunction<T> op) where T : EntityRecordType
		{
			T t = op(asyncResult);
			this.AddItem(cache, (EntityRecordType)((object)t));
			return t;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FBC File Offset: 0x000021BC
		protected IEnumerable<T> EndMultiUpdateOperation<T>(CacheEnumerator cache, IAsyncResult asyncResult, DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.EndFunction<IEnumerable<T>> op) where T : EntityRecordType
		{
			IEnumerable<T> enumerable = op(asyncResult);
			foreach (T t in enumerable)
			{
				this.AddItem(cache, (EntityRecordType)((object)t));
			}
			return enumerable;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004018 File Offset: 0x00002218
		protected virtual EntityRecordType RemoveItem(CacheEnumerator cache, KeyType key)
		{
			object @lock = this.m_lock;
			EntityRecordType entityRecordType2;
			lock (@lock)
			{
				EntityRecordType entityRecordType;
				this.m_pools[cache].TryCheckOut(key, out entityRecordType);
				entityRecordType2 = entityRecordType;
			}
			return entityRecordType2;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000406C File Offset: 0x0000226C
		protected void AddItem(CacheEnumerator cache, EntityRecordType item)
		{
			if (item == null)
			{
				return;
			}
			KeyType key = this.GetKey(item);
			this.Replace(cache, key, item);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004098 File Offset: 0x00002298
		protected virtual void Replace(CacheEnumerator cache, KeyType key, EntityRecordType item)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_pools[cache].Remove(key);
				this.m_pools[cache].CheckIn(key, item);
			}
		}

		// Token: 0x060000AC RID: 172
		protected abstract KeyType GetKey(object result);

		// Token: 0x060000AD RID: 173 RVA: 0x000040F8 File Offset: 0x000022F8
		protected IAsyncResult BeginRemoveOperation(CacheEnumerator cache, KeyType key, Sequencer.AsyncBeginFunction<KeyType> op, AsyncCallback asyncCallback, object asyncState)
		{
			this.RemoveItem(cache, key);
			return op(key, asyncCallback, asyncState);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004110 File Offset: 0x00002310
		protected IAsyncResult BeginRemoveByCheckOperation(CacheEnumerator cache, Func<EntityRecordType, bool> check, Sequencer.AsyncBeginFunction op, AsyncCallback asyncCallback, object asyncState)
		{
			this.OperateOnValues(cache, delegate(IEnumerable<EntityRecordType> values)
			{
				foreach (EntityRecordType entityRecordType in values.Where(check))
				{
					this.RemoveItem(cache, this.GetKey(entityRecordType));
				}
			});
			return op(asyncCallback, asyncState);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000415C File Offset: 0x0000235C
		protected IAsyncResult BeginResolveFromValues<TValue>(CacheEnumerator cache, Func<EntityRecordType, bool> check, Sequencer.AsyncBeginFunction op, AsyncCallback asyncCallback, object asyncState) where TValue : EntityRecordType
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				EntityRecordType entityRecordType = this.m_pools[cache].Values.FirstOrDefault(check);
				if (entityRecordType != null)
				{
					return new DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<TValue>(asyncCallback, asyncState, entityRecordType as TValue);
				}
			}
			return op(asyncCallback, asyncState);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000041E0 File Offset: 0x000023E0
		protected void OperateOnValues(CacheEnumerator cache, Action<IEnumerable<EntityRecordType>> op)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				op(this.m_pools[cache].Values.ToList<EntityRecordType>());
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004238 File Offset: 0x00002438
		protected IAsyncResult BeginGetOrAddOperation<TValue>(CacheEnumerator cache, Func<EntityRecordType, bool> checkingOperation, Sequencer.AsyncBeginFunction databaseOperation, AsyncCallback asyncCallback, object asyncState) where TValue : EntityRecordType
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				EntityRecordType entityRecordType = this.m_pools[cache].Values.FirstOrDefault(checkingOperation);
				if (entityRecordType != null)
				{
					return new DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<TValue>(asyncCallback, asyncState, entityRecordType as TValue);
				}
			}
			return databaseOperation(asyncCallback, asyncState);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000042BC File Offset: 0x000024BC
		protected CreateOrGetResult<T> EndGetOrAddOperation<T>(CacheEnumerator cache, IAsyncResult asyncResult, DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.EndFunction<CreateOrGetResult<T>> op) where T : EntityRecordType
		{
			DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<T> cacheCompletedAsyncResult = asyncResult as DatabaseClientCacheBase<CacheEnumerator, KeyType, EntityRecordType>.CacheCompletedAsyncResult<T>;
			if (cacheCompletedAsyncResult != null)
			{
				return new CreateOrGetResult<T>(false, cacheCompletedAsyncResult.End());
			}
			CreateOrGetResult<T> createOrGetResult = op(asyncResult);
			this.AddItem(cache, (EntityRecordType)((object)createOrGetResult.Record));
			return createOrGetResult;
		}

		// Token: 0x04000060 RID: 96
		private readonly IDictionary<CacheEnumerator, QuickAccessPool<KeyType, EntityRecordType>> m_pools;

		// Token: 0x04000061 RID: 97
		private readonly object m_lock;

		// Token: 0x02000581 RID: 1409
		// (Invoke) Token: 0x06002A80 RID: 10880
		protected delegate T EndFunction<T>(IAsyncResult result);

		// Token: 0x02000582 RID: 1410
		protected sealed class CacheCompletedAsyncResult<T> : CompletedAsyncResult<T>
		{
			// Token: 0x06002A83 RID: 10883 RVA: 0x00098727 File Offset: 0x00096927
			public CacheCompletedAsyncResult(AsyncCallback callback, object asyncState, T res)
				: base(SyncOrNot.Sync, callback, asyncState, res)
			{
			}
		}
	}
}
