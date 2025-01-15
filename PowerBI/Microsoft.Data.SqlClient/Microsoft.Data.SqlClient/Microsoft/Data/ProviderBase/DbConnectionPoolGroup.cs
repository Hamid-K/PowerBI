using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000161 RID: 353
	internal sealed class DbConnectionPoolGroup
	{
		// Token: 0x06001A73 RID: 6771 RVA: 0x0006C227 File Offset: 0x0006A427
		internal DbConnectionPoolGroup(DbConnectionOptions connectionOptions, DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolGroupOptions)
		{
			this._connectionOptions = connectionOptions;
			this._poolKey = key;
			this._poolGroupOptions = poolGroupOptions;
			this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
			this._state = 1;
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0006C266 File Offset: 0x0006A466
		internal DbConnectionOptions ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0006C26E File Offset: 0x0006A46E
		internal DbConnectionPoolKey PoolKey
		{
			get
			{
				return this._poolKey;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x0006C276 File Offset: 0x0006A476
		// (set) Token: 0x06001A77 RID: 6775 RVA: 0x0006C27E File Offset: 0x0006A47E
		internal DbConnectionPoolGroupProviderInfo ProviderInfo
		{
			get
			{
				return this._providerInfo;
			}
			set
			{
				this._providerInfo = value;
				if (value != null)
				{
					this._providerInfo.PoolGroup = this;
				}
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x0006C296 File Offset: 0x0006A496
		internal bool IsDisabled
		{
			get
			{
				return 4 == this._state;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0006C2A1 File Offset: 0x0006A4A1
		internal int ObjectID { get; } = Interlocked.Increment(ref DbConnectionPoolGroup.s_objectTypeCount);

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001A7A RID: 6778 RVA: 0x0006C2A9 File Offset: 0x0006A4A9
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._poolGroupOptions;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x0006C2B1 File Offset: 0x0006A4B1
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x0006C2B9 File Offset: 0x0006A4B9
		internal DbMetaDataFactory MetaDataFactory
		{
			get
			{
				return this._metaDataFactory;
			}
			set
			{
				this._metaDataFactory = value;
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0006C2C4 File Offset: 0x0006A4C4
		internal int Clear()
		{
			ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = null;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					concurrentDictionary = this._poolCollection;
					this._poolCollection = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
				}
			}
			if (concurrentDictionary != null)
			{
				foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in concurrentDictionary)
				{
					DbConnectionPool value = keyValuePair.Value;
					if (value != null)
					{
						DbConnectionFactory connectionFactory = value.ConnectionFactory;
						connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Decrement();
						connectionFactory.QueuePoolForRelease(value, true);
					}
				}
			}
			return this._poolCollection.Count;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0006C38C File Offset: 0x0006A58C
		internal DbConnectionPool GetConnectionPool(DbConnectionFactory connectionFactory)
		{
			DbConnectionPool dbConnectionPool = null;
			if (this._poolGroupOptions != null)
			{
				DbConnectionPoolIdentity dbConnectionPoolIdentity = DbConnectionPoolIdentity.NoIdentity;
				if (this._poolGroupOptions.PoolByIdentity)
				{
					dbConnectionPoolIdentity = DbConnectionPoolIdentity.GetCurrent();
					if (dbConnectionPoolIdentity.IsRestricted)
					{
						dbConnectionPoolIdentity = null;
					}
				}
				if (dbConnectionPoolIdentity != null && !this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
				{
					lock (this)
					{
						if (!this._poolCollection.TryGetValue(dbConnectionPoolIdentity, out dbConnectionPool))
						{
							DbConnectionPoolProviderInfo dbConnectionPoolProviderInfo = connectionFactory.CreateConnectionPoolProviderInfo(this.ConnectionOptions);
							DbConnectionPool dbConnectionPool2 = new DbConnectionPool(connectionFactory, this, dbConnectionPoolIdentity, dbConnectionPoolProviderInfo);
							if (this.MarkPoolGroupAsActive())
							{
								dbConnectionPool2.Startup();
								bool flag2 = this._poolCollection.TryAdd(dbConnectionPoolIdentity, dbConnectionPool2);
								SqlClientEventSource.Log.EnterActiveConnectionPool();
								connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Increment();
								dbConnectionPool = dbConnectionPool2;
							}
							else
							{
								dbConnectionPool2.Shutdown();
							}
						}
					}
				}
			}
			if (dbConnectionPool == null)
			{
				lock (this)
				{
					this.MarkPoolGroupAsActive();
				}
			}
			return dbConnectionPool;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x0006C4B0 File Offset: 0x0006A6B0
		private bool MarkPoolGroupAsActive()
		{
			if (2 == this._state)
			{
				this._state = 1;
				SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionPoolGroup.ClearInternal|RES|INFO|CPOOL> {0}, Active", this.ObjectID);
			}
			return 1 == this._state;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
		internal bool Prune()
		{
			bool flag2;
			lock (this)
			{
				if (this._poolCollection.Count > 0)
				{
					ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> concurrentDictionary = new ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool>();
					foreach (KeyValuePair<DbConnectionPoolIdentity, DbConnectionPool> keyValuePair in this._poolCollection)
					{
						DbConnectionPool value = keyValuePair.Value;
						if (value != null)
						{
							if (!value.ErrorOccurred && value.Count == 0)
							{
								DbConnectionFactory connectionFactory = value.ConnectionFactory;
								connectionFactory.PerformanceCounters.NumberOfActiveConnectionPools.Decrement();
								connectionFactory.QueuePoolForRelease(value, false);
							}
							else
							{
								concurrentDictionary.TryAdd(keyValuePair.Key, keyValuePair.Value);
							}
						}
					}
					this._poolCollection = concurrentDictionary;
				}
				if (this._poolCollection.Count == 0)
				{
					if (1 == this._state)
					{
						this._state = 2;
						SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionPoolGroup.ClearInternal|RES|INFO|CPOOL> {0}, Idle", this.ObjectID);
					}
					else if (2 == this._state)
					{
						this._state = 4;
						SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionPoolGroup.ReadyToRemove|RES|INFO|CPOOL> {0}, Disabled", this.ObjectID);
					}
				}
				flag2 = 4 == this._state;
			}
			return flag2;
		}

		// Token: 0x04000ABC RID: 2748
		private readonly DbConnectionOptions _connectionOptions;

		// Token: 0x04000ABD RID: 2749
		private readonly DbConnectionPoolKey _poolKey;

		// Token: 0x04000ABE RID: 2750
		private readonly DbConnectionPoolGroupOptions _poolGroupOptions;

		// Token: 0x04000ABF RID: 2751
		private ConcurrentDictionary<DbConnectionPoolIdentity, DbConnectionPool> _poolCollection;

		// Token: 0x04000AC0 RID: 2752
		private int _state;

		// Token: 0x04000AC1 RID: 2753
		private DbConnectionPoolGroupProviderInfo _providerInfo;

		// Token: 0x04000AC2 RID: 2754
		private DbMetaDataFactory _metaDataFactory;

		// Token: 0x04000AC3 RID: 2755
		private static int s_objectTypeCount;

		// Token: 0x04000AC4 RID: 2756
		private const int PoolGroupStateActive = 1;

		// Token: 0x04000AC5 RID: 2757
		private const int PoolGroupStateIdle = 2;

		// Token: 0x04000AC6 RID: 2758
		private const int PoolGroupStateDisabled = 4;
	}
}
