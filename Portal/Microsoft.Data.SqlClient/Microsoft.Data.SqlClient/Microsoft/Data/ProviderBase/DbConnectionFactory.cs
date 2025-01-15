using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000170 RID: 368
	internal abstract class DbConnectionFactory
	{
		// Token: 0x06001AF8 RID: 6904 RVA: 0x0006E11E File Offset: 0x0006C31E
		protected DbConnectionFactory()
			: this(DbConnectionPoolCountersNoCounters.SingletonInstance)
		{
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x0006E12C File Offset: 0x0006C32C
		protected DbConnectionFactory(DbConnectionPoolCounters performanceCounters)
		{
			this._performanceCounters = performanceCounters;
			this._connectionPoolGroups = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>();
			this._poolsToRelease = new List<DbConnectionPool>();
			this._poolGroupsToRelease = new List<DbConnectionPoolGroup>();
			this._pruningTimer = this.CreatePruningTimer();
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x0006E183 File Offset: 0x0006C383
		internal DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._performanceCounters;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06001AFB RID: 6907
		public abstract DbProviderFactory ProviderFactory { get; }

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06001AFC RID: 6908 RVA: 0x0006E18B File Offset: 0x0006C38B
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x0006E194 File Offset: 0x0006C394
		public void ClearAllPools()
		{
			using (TryEventScope.Create("<prov.DbConnectionFactory.ClearAllPools|API>", "ClearAllPools"))
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					DbConnectionPoolGroup value = keyValuePair.Value;
					if (value != null)
					{
						value.Clear();
					}
				}
			}
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x0006E21C File Offset: 0x0006C41C
		public void ClearPool(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			using (TryEventScope.Create<int>("<prov.DbConnectionFactory.ClearPool|API> {0}", this.GetObjectId(connection)))
			{
				DbConnectionPoolGroup connectionPoolGroup = this.GetConnectionPoolGroup(connection);
				if (connectionPoolGroup != null)
				{
					connectionPoolGroup.Clear();
				}
			}
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x0006E274 File Offset: 0x0006C474
		public void ClearPool(DbConnectionPoolKey key)
		{
			ADP.CheckArgumentNull(key.ConnectionString, "key.ConnectionString");
			using (TryEventScope.Create("<prov.DbConnectionFactory.ClearPool|API> connectionString", "ClearPool"))
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				DbConnectionPoolGroup dbConnectionPoolGroup;
				if (connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
				{
					dbConnectionPoolGroup.Clear();
				}
			}
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000021D8 File Offset: 0x000003D8
		internal virtual DbConnectionPoolProviderInfo CreateConnectionPoolProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
		protected virtual DbMetaDataFactory CreateMetaDataFactory(DbConnectionInternal internalConnection, out bool cacheMetaDataFactory)
		{
			cacheMetaDataFactory = false;
			throw ADP.NotSupported();
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0006E2E0 File Offset: 0x0006C4E0
		internal DbConnectionInternal CreateNonPooledConnection(DbConnection owningConnection, DbConnectionPoolGroup poolGroup, DbConnectionOptions userOptions)
		{
			DbConnectionOptions connectionOptions = poolGroup.ConnectionOptions;
			DbConnectionPoolGroupProviderInfo providerInfo = poolGroup.ProviderInfo;
			DbConnectionPoolKey poolKey = poolGroup.PoolKey;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(connectionOptions, poolKey, providerInfo, null, owningConnection, userOptions);
			if (dbConnectionInternal != null)
			{
				this.PerformanceCounters.HardConnectsPerSecond.Increment();
				dbConnectionInternal.MakeNonPooledObject(owningConnection, this.PerformanceCounters);
			}
			SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionFactory.CreateNonPooledConnection|RES|CPOOL> {0}, Non-pooled database connection created.", this.ObjectID);
			return dbConnectionInternal;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0006E348 File Offset: 0x0006C548
		internal DbConnectionInternal CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
		{
			DbConnectionPoolGroupProviderInfo providerInfo = pool.PoolGroup.ProviderInfo;
			DbConnectionInternal dbConnectionInternal = this.CreateConnection(options, poolKey, providerInfo, pool, owningObject, userOptions);
			if (dbConnectionInternal != null)
			{
				this.PerformanceCounters.HardConnectsPerSecond.Increment();
				dbConnectionInternal.MakePooledConnection(pool);
			}
			SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionFactory.CreatePooledConnection|RES|CPOOL> {0}, Pooled database connection created.", this.ObjectID);
			return dbConnectionInternal;
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000021D8 File Offset: 0x000003D8
		internal virtual DbConnectionPoolGroupProviderInfo CreateConnectionPoolGroupProviderInfo(DbConnectionOptions connectionOptions)
		{
			return null;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0006E3A0 File Offset: 0x0006C5A0
		private Timer CreatePruningTimer()
		{
			TimerCallback timerCallback = new TimerCallback(this.PruneConnectionPoolGroups);
			return new Timer(timerCallback, null, 240000, 30000);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0006E3CC File Offset: 0x0006C5CC
		protected DbConnectionOptions FindConnectionOptions(DbConnectionPoolKey key)
		{
			if (!ADP.IsEmpty(key.ConnectionString))
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				DbConnectionPoolGroup dbConnectionPoolGroup;
				if (connectionPoolGroups.TryGetValue(key, out dbConnectionPoolGroup))
				{
					return dbConnectionPoolGroup.ConnectionOptions;
				}
			}
			return null;
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0006E400 File Offset: 0x0006C600
		private static Task<DbConnectionInternal> GetCompletedTask()
		{
			if (DbConnectionFactory.s_completedTask == null)
			{
				TaskCompletionSource<DbConnectionInternal> taskCompletionSource = new TaskCompletionSource<DbConnectionInternal>();
				taskCompletionSource.SetResult(null);
				DbConnectionFactory.s_completedTask = taskCompletionSource.Task;
			}
			return DbConnectionFactory.s_completedTask;
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0006E434 File Offset: 0x0006C634
		internal bool TryGetConnection(DbConnection owningConnection, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, out DbConnectionInternal connection)
		{
			DbConnectionFactory.<>c__DisplayClass31_0 CS$<>8__locals1 = new DbConnectionFactory.<>c__DisplayClass31_0();
			CS$<>8__locals1.retry = retry;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.owningConnection = owningConnection;
			CS$<>8__locals1.userOptions = userOptions;
			CS$<>8__locals1.oldConnection = oldConnection;
			connection = null;
			int num = 10;
			int num2 = 1;
			for (;;)
			{
				CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
				DbConnectionPool connectionPool = this.GetConnectionPool(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup);
				if (connectionPool == null)
				{
					CS$<>8__locals1.poolGroup = this.GetConnectionPoolGroup(CS$<>8__locals1.owningConnection);
					if (CS$<>8__locals1.retry != null)
					{
						break;
					}
					connection = this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
					this.PerformanceCounters.NumberOfNonPooledConnections.Increment();
				}
				else
				{
					if (((SqlConnection)CS$<>8__locals1.owningConnection).ForceNewConnection)
					{
						connection = connectionPool.ReplaceConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.userOptions, CS$<>8__locals1.oldConnection);
					}
					else if (!connectionPool.TryGetConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.retry, CS$<>8__locals1.userOptions, out connection))
					{
						return false;
					}
					if (connection == null)
					{
						if (connectionPool.IsRunning)
						{
							goto Block_8;
						}
						Thread.Sleep(num2);
						num2 *= 2;
					}
				}
				if (connection != null || num-- <= 0)
				{
					goto IL_0286;
				}
			}
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			Task<DbConnectionInternal>[] array = DbConnectionFactory.s_pendingOpenNonPooled;
			Task<DbConnectionInternal> task3;
			lock (array)
			{
				int i;
				for (i = 0; i < DbConnectionFactory.s_pendingOpenNonPooled.Length; i++)
				{
					Task task4 = DbConnectionFactory.s_pendingOpenNonPooled[i];
					if (task4 == null)
					{
						DbConnectionFactory.s_pendingOpenNonPooled[i] = DbConnectionFactory.GetCompletedTask();
						break;
					}
					if (task4.IsCompleted)
					{
						break;
					}
				}
				if (i == DbConnectionFactory.s_pendingOpenNonPooled.Length)
				{
					i = DbConnectionFactory.s_pendingOpenNonPooledNext++ % DbConnectionFactory.s_pendingOpenNonPooled.Length;
				}
				Task<DbConnectionInternal> task2 = DbConnectionFactory.s_pendingOpenNonPooled[i];
				Func<Task<DbConnectionInternal>, DbConnectionInternal> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = delegate(Task<DbConnectionInternal> _)
					{
						Transaction currentTransaction = ADP.GetCurrentTransaction();
						DbConnectionInternal dbConnectionInternal2;
						try
						{
							ADP.SetCurrentTransaction(CS$<>8__locals1.retry.Task.AsyncState as Transaction);
							DbConnectionInternal dbConnectionInternal = CS$<>8__locals1.<>4__this.CreateNonPooledConnection(CS$<>8__locals1.owningConnection, CS$<>8__locals1.poolGroup, CS$<>8__locals1.userOptions);
							if (CS$<>8__locals1.oldConnection != null && CS$<>8__locals1.oldConnection.State == ConnectionState.Open)
							{
								CS$<>8__locals1.oldConnection.PrepareForReplaceConnection();
								CS$<>8__locals1.oldConnection.Dispose();
							}
							dbConnectionInternal2 = dbConnectionInternal;
						}
						finally
						{
							ADP.SetCurrentTransaction(currentTransaction);
						}
						return dbConnectionInternal2;
					});
				}
				task3 = task2.ContinueWith<DbConnectionInternal>(func, cancellationTokenSource.Token, TaskContinuationOptions.LongRunning, TaskScheduler.Default);
				DbConnectionFactory.s_pendingOpenNonPooled[i] = task3;
			}
			if (CS$<>8__locals1.owningConnection.ConnectionTimeout > 0)
			{
				int num3 = CS$<>8__locals1.owningConnection.ConnectionTimeout * 1000;
				cancellationTokenSource.CancelAfter(num3);
			}
			task3.ContinueWith(delegate(Task<DbConnectionInternal> task)
			{
				cancellationTokenSource.Dispose();
				if (task.IsCanceled)
				{
					CS$<>8__locals1.retry.TrySetException(ADP.ExceptionWithStackTrace(ADP.NonPooledOpenTimeout()));
					return;
				}
				if (task.IsFaulted)
				{
					CS$<>8__locals1.retry.TrySetException(task.Exception.InnerException);
					return;
				}
				if (CS$<>8__locals1.retry.TrySetResult(task.Result))
				{
					CS$<>8__locals1.<>4__this.PerformanceCounters.NumberOfNonPooledConnections.Increment();
					return;
				}
				task.Result.DoomThisConnection();
				task.Result.Dispose();
			}, TaskScheduler.Default);
			return false;
			Block_8:
			SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionFactory.GetConnection|RES|CPOOL> {0}, GetConnection failed because a pool timeout occurred.", this.ObjectID);
			throw ADP.PooledOpenTimeout();
			IL_0286:
			if (connection == null)
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<prov.DbConnectionFactory.GetConnection|RES|CPOOL> {0}, GetConnection failed because a pool timeout occurred and all retries were exhausted.", this.ObjectID);
				throw ADP.PooledOpenTimeout();
			}
			return true;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		private DbConnectionPool GetConnectionPool(DbConnection owningObject, DbConnectionPoolGroup connectionPoolGroup)
		{
			if (connectionPoolGroup.IsDisabled && connectionPoolGroup.PoolGroupOptions != null)
			{
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<prov.DbConnectionFactory.GetConnectionPool|RES|INFO|CPOOL> {0}, DisabledPoolGroup={1}", this.ObjectID, connectionPoolGroup.ObjectID);
				DbConnectionPoolGroupOptions poolGroupOptions = connectionPoolGroup.PoolGroupOptions;
				DbConnectionOptions connectionOptions = connectionPoolGroup.ConnectionOptions;
				connectionPoolGroup = this.GetConnectionPoolGroup(connectionPoolGroup.PoolKey, poolGroupOptions, ref connectionOptions);
				this.SetConnectionPoolGroup(owningObject, connectionPoolGroup);
			}
			return connectionPoolGroup.GetConnectionPool(this);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x0006E760 File Offset: 0x0006C960
		internal DbConnectionPoolGroup GetConnectionPoolGroup(DbConnectionPoolKey key, DbConnectionPoolGroupOptions poolOptions, ref DbConnectionOptions userConnectionOptions)
		{
			if (ADP.IsEmpty(key.ConnectionString))
			{
				return null;
			}
			Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = this._connectionPoolGroups;
			DbConnectionPoolGroup dbConnectionPoolGroup;
			if (!dictionary.TryGetValue(key, out dbConnectionPoolGroup) || (dbConnectionPoolGroup.IsDisabled && dbConnectionPoolGroup.PoolGroupOptions != null))
			{
				DbConnectionOptions dbConnectionOptions = this.CreateConnectionOptions(key.ConnectionString, userConnectionOptions);
				if (dbConnectionOptions == null)
				{
					throw ADP.InternalConnectionError(ADP.ConnectionError.ConnectionOptionsMissing);
				}
				string text = key.ConnectionString;
				if (userConnectionOptions == null)
				{
					userConnectionOptions = dbConnectionOptions;
					text = dbConnectionOptions.Expand();
					if (text != key.ConnectionString)
					{
						DbConnectionPoolKey dbConnectionPoolKey = (DbConnectionPoolKey)((ICloneable)key).Clone();
						dbConnectionPoolKey.ConnectionString = text;
						return this.GetConnectionPoolGroup(dbConnectionPoolKey, null, ref userConnectionOptions);
					}
				}
				if (poolOptions == null && ADP.s_isWindowsNT)
				{
					if (dbConnectionPoolGroup != null)
					{
						poolOptions = dbConnectionPoolGroup.PoolGroupOptions;
					}
					else
					{
						poolOptions = this.CreateConnectionPoolGroupOptions(dbConnectionOptions);
					}
				}
				lock (this)
				{
					dictionary = this._connectionPoolGroups;
					if (!dictionary.TryGetValue(key, out dbConnectionPoolGroup))
					{
						DbConnectionPoolGroup dbConnectionPoolGroup2 = new DbConnectionPoolGroup(dbConnectionOptions, key, poolOptions);
						dbConnectionPoolGroup2.ProviderInfo = this.CreateConnectionPoolGroupProviderInfo(dbConnectionOptions);
						Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary2 = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(1 + dictionary.Count);
						foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in dictionary)
						{
							dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
						}
						dictionary2.Add(key, dbConnectionPoolGroup2);
						this.PerformanceCounters.NumberOfActiveConnectionPoolGroups.Increment();
						dbConnectionPoolGroup = dbConnectionPoolGroup2;
						this._connectionPoolGroups = dictionary2;
					}
					return dbConnectionPoolGroup;
				}
			}
			if (userConnectionOptions == null)
			{
				userConnectionOptions = dbConnectionPoolGroup.ConnectionOptions;
			}
			return dbConnectionPoolGroup;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x0006E904 File Offset: 0x0006CB04
		internal DbMetaDataFactory GetMetaDataFactory(DbConnectionPoolGroup connectionPoolGroup, DbConnectionInternal internalConnection)
		{
			DbMetaDataFactory dbMetaDataFactory = connectionPoolGroup.MetaDataFactory;
			if (dbMetaDataFactory == null)
			{
				bool flag = false;
				dbMetaDataFactory = this.CreateMetaDataFactory(internalConnection, out flag);
				if (flag)
				{
					connectionPoolGroup.MetaDataFactory = dbMetaDataFactory;
				}
			}
			return dbMetaDataFactory;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x0006E934 File Offset: 0x0006CB34
		private void PruneConnectionPoolGroups(object state)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> {0}", this.ObjectID);
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (this._poolsToRelease.Count != 0)
				{
					DbConnectionPool[] array = this._poolsToRelease.ToArray();
					foreach (DbConnectionPool dbConnectionPool in array)
					{
						if (dbConnectionPool != null)
						{
							dbConnectionPool.Clear();
							if (dbConnectionPool.Count == 0)
							{
								this._poolsToRelease.Remove(dbConnectionPool);
								SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> {0}, ReleasePool={1}", this.ObjectID, dbConnectionPool.ObjectID);
								this.PerformanceCounters.NumberOfInactiveConnectionPools.Decrement();
							}
						}
					}
				}
			}
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				if (this._poolGroupsToRelease.Count != 0)
				{
					DbConnectionPoolGroup[] array3 = this._poolGroupsToRelease.ToArray();
					foreach (DbConnectionPoolGroup dbConnectionPoolGroup in array3)
					{
						if (dbConnectionPoolGroup != null && dbConnectionPoolGroup.Clear() == 0)
						{
							this._poolGroupsToRelease.Remove(dbConnectionPoolGroup);
							SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<prov.DbConnectionFactory.PruneConnectionPoolGroups|RES|INFO|CPOOL> {0}, ReleasePoolGroup={1}", this.ObjectID, dbConnectionPoolGroup.ObjectID);
							this.PerformanceCounters.NumberOfInactiveConnectionPoolGroups.Decrement();
						}
					}
				}
			}
			lock (this)
			{
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> connectionPoolGroups = this._connectionPoolGroups;
				Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> dictionary = new Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup>(connectionPoolGroups.Count);
				foreach (KeyValuePair<DbConnectionPoolKey, DbConnectionPoolGroup> keyValuePair in connectionPoolGroups)
				{
					if (keyValuePair.Value != null)
					{
						if (keyValuePair.Value.Prune())
						{
							this.PerformanceCounters.NumberOfActiveConnectionPoolGroups.Decrement();
							this.QueuePoolGroupForRelease(keyValuePair.Value);
						}
						else
						{
							dictionary.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
				}
				this._connectionPoolGroups = dictionary;
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x0006EB80 File Offset: 0x0006CD80
		internal void QueuePoolForRelease(DbConnectionPool pool, bool clearing)
		{
			pool.Shutdown();
			List<DbConnectionPool> poolsToRelease = this._poolsToRelease;
			lock (poolsToRelease)
			{
				if (clearing)
				{
					pool.Clear();
				}
				this._poolsToRelease.Add(pool);
			}
			this.PerformanceCounters.NumberOfInactiveConnectionPools.Increment();
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x0006EBE8 File Offset: 0x0006CDE8
		internal void QueuePoolGroupForRelease(DbConnectionPoolGroup poolGroup)
		{
			SqlClientEventSource.Log.TryTraceEvent<int, int>("<prov.DbConnectionFactory.QueuePoolGroupForRelease|RES|INFO|CPOOL> {0}, poolGroup={1}", this.ObjectID, poolGroup.ObjectID);
			List<DbConnectionPoolGroup> poolGroupsToRelease = this._poolGroupsToRelease;
			lock (poolGroupsToRelease)
			{
				this._poolGroupsToRelease.Add(poolGroup);
			}
			this.PerformanceCounters.NumberOfInactiveConnectionPoolGroups.Increment();
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0006EC5C File Offset: 0x0006CE5C
		protected virtual DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
		{
			return this.CreateConnection(options, poolKey, poolGroupProviderInfo, pool, owningConnection);
		}

		// Token: 0x06001B10 RID: 6928
		protected abstract DbConnectionInternal CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection);

		// Token: 0x06001B11 RID: 6929
		protected abstract DbConnectionOptions CreateConnectionOptions(string connectionString, DbConnectionOptions previous);

		// Token: 0x06001B12 RID: 6930
		protected abstract DbConnectionPoolGroupOptions CreateConnectionPoolGroupOptions(DbConnectionOptions options);

		// Token: 0x06001B13 RID: 6931
		internal abstract DbConnectionPoolGroup GetConnectionPoolGroup(DbConnection connection);

		// Token: 0x06001B14 RID: 6932
		internal abstract DbConnectionInternal GetInnerConnection(DbConnection connection);

		// Token: 0x06001B15 RID: 6933
		protected abstract int GetObjectId(DbConnection connection);

		// Token: 0x06001B16 RID: 6934
		internal abstract void PermissionDemand(DbConnection outerConnection);

		// Token: 0x06001B17 RID: 6935
		internal abstract void SetConnectionPoolGroup(DbConnection outerConnection, DbConnectionPoolGroup poolGroup);

		// Token: 0x06001B18 RID: 6936
		internal abstract void SetInnerConnectionEvent(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x06001B19 RID: 6937
		internal abstract bool SetInnerConnectionFrom(DbConnection owningObject, DbConnectionInternal to, DbConnectionInternal from);

		// Token: 0x06001B1A RID: 6938
		internal abstract void SetInnerConnectionTo(DbConnection owningObject, DbConnectionInternal to);

		// Token: 0x04000AF2 RID: 2802
		private Dictionary<DbConnectionPoolKey, DbConnectionPoolGroup> _connectionPoolGroups;

		// Token: 0x04000AF3 RID: 2803
		private readonly List<DbConnectionPool> _poolsToRelease;

		// Token: 0x04000AF4 RID: 2804
		private readonly List<DbConnectionPoolGroup> _poolGroupsToRelease;

		// Token: 0x04000AF5 RID: 2805
		private readonly DbConnectionPoolCounters _performanceCounters;

		// Token: 0x04000AF6 RID: 2806
		private readonly Timer _pruningTimer;

		// Token: 0x04000AF7 RID: 2807
		private const int PruningDueTime = 240000;

		// Token: 0x04000AF8 RID: 2808
		private const int PruningPeriod = 30000;

		// Token: 0x04000AF9 RID: 2809
		private static int _objectTypeCount;

		// Token: 0x04000AFA RID: 2810
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionFactory._objectTypeCount);

		// Token: 0x04000AFB RID: 2811
		private static int s_pendingOpenNonPooledNext = 0;

		// Token: 0x04000AFC RID: 2812
		private static Task<DbConnectionInternal>[] s_pendingOpenNonPooled = new Task<DbConnectionInternal>[Environment.ProcessorCount];

		// Token: 0x04000AFD RID: 2813
		private static Task<DbConnectionInternal> s_completedTask;
	}
}
