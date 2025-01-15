using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000172 RID: 370
	internal sealed class DbConnectionPool
	{
		// Token: 0x06001B5D RID: 7005 RVA: 0x0006F660 File Offset: 0x0006D860
		internal DbConnectionPool(DbConnectionFactory connectionFactory, DbConnectionPoolGroup connectionPoolGroup, DbConnectionPoolIdentity identity, DbConnectionPoolProviderInfo connectionPoolProviderInfo)
		{
			if (identity != null && identity.IsRestricted)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToPoolOnRestrictedToken);
			}
			this._state = DbConnectionPool.State.Initializing;
			Random random = DbConnectionPool._random;
			lock (random)
			{
				this._cleanupWait = DbConnectionPool._random.Next(12, 24) * 10 * 1000;
			}
			this._connectionFactory = connectionFactory;
			this._connectionPoolGroup = connectionPoolGroup;
			this._connectionPoolGroupOptions = connectionPoolGroup.PoolGroupOptions;
			this._connectionPoolProviderInfo = connectionPoolProviderInfo;
			this._identity = identity;
			this._waitHandles = new DbConnectionPool.PoolWaitHandles();
			this._errorWait = 5000;
			this._errorTimer = null;
			this._objectList = new List<DbConnectionInternal>(this.MaxPoolSize);
			this._pooledDbAuthenticationContexts = new ConcurrentDictionary<DbConnectionPoolAuthenticationContextKey, DbConnectionPoolAuthenticationContext>(4 * Environment.ProcessorCount, 2);
			if (ADP.s_isPlatformNT5)
			{
				this._transactedConnectionPool = new DbConnectionPool.TransactedConnectionPool(this);
			}
			this._poolCreateRequest = new WaitCallback(this.PoolCreateRequest);
			this._state = DbConnectionPool.State.Running;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.DbConnectionPool|RES|CPOOL> {0}, Constructed.", this.ObjectID);
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0006F7B4 File Offset: 0x0006D9B4
		private int CreationTimeout
		{
			get
			{
				return this.PoolGroupOptions.CreationTimeout;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001B5F RID: 7007 RVA: 0x0006F7C1 File Offset: 0x0006D9C1
		internal int Count
		{
			get
			{
				return this._totalObjects;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0006F7C9 File Offset: 0x0006D9C9
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return this._connectionFactory;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001B61 RID: 7009 RVA: 0x0006F7D1 File Offset: 0x0006D9D1
		internal bool ErrorOccurred
		{
			get
			{
				return this._errorOccurred;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0006F7DB File Offset: 0x0006D9DB
		private bool HasTransactionAffinity
		{
			get
			{
				return this.PoolGroupOptions.HasTransactionAffinity;
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x0006F7E8 File Offset: 0x0006D9E8
		internal TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this.PoolGroupOptions.LoadBalanceTimeout;
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0006F7F8 File Offset: 0x0006D9F8
		private bool NeedToReplenish
		{
			get
			{
				if (DbConnectionPool.State.Running != this._state)
				{
					return false;
				}
				int count = this.Count;
				if (count >= this.MaxPoolSize)
				{
					return false;
				}
				if (count < this.MinPoolSize)
				{
					return true;
				}
				int num = this._stackNew.Count + this._stackOld.Count;
				int waitCount = this._waitCount;
				return num < waitCount || (num == waitCount && count > 1);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001B65 RID: 7013 RVA: 0x0006F860 File Offset: 0x0006DA60
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0006F868 File Offset: 0x0006DA68
		internal bool IsRunning
		{
			get
			{
				return DbConnectionPool.State.Running == this._state;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0006F873 File Offset: 0x0006DA73
		private int MaxPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MaxPoolSize;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0006F880 File Offset: 0x0006DA80
		private int MinPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MinPoolSize;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0006F88D File Offset: 0x0006DA8D
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x0006F895 File Offset: 0x0006DA95
		internal DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._connectionFactory.PerformanceCounters;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0006F8A2 File Offset: 0x0006DAA2
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._connectionPoolGroup;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0006F8AA File Offset: 0x0006DAAA
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._connectionPoolGroupOptions;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0006F8B2 File Offset: 0x0006DAB2
		internal DbConnectionPoolProviderInfo ProviderInfo
		{
			get
			{
				return this._connectionPoolProviderInfo;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0006F8BA File Offset: 0x0006DABA
		internal ConcurrentDictionary<DbConnectionPoolAuthenticationContextKey, DbConnectionPoolAuthenticationContext> AuthenticationContexts
		{
			get
			{
				return this._pooledDbAuthenticationContexts;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0006F8C2 File Offset: 0x0006DAC2
		internal bool UseLoadBalancing
		{
			get
			{
				return this.PoolGroupOptions.UseLoadBalancing;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0006F8CF File Offset: 0x0006DACF
		private bool UsingIntegrateSecurity
		{
			get
			{
				return this._identity != null && DbConnectionPoolIdentity.NoIdentity != this._identity;
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0006F8EC File Offset: 0x0006DAEC
		private void CleanupCallback(object state)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.CleanupCallback|RES|INFO|CPOOL> {0}", this.ObjectID);
			while (this.Count > this.MinPoolSize && this._waitHandles.PoolSemaphore.WaitOne(0, false))
			{
				DbConnectionInternal dbConnectionInternal;
				if (!this._stackOld.TryPop(out dbConnectionInternal))
				{
					this._waitHandles.PoolSemaphore.Release(1);
					break;
				}
				this.PerformanceCounters.NumberOfFreeConnections.Decrement();
				bool flag = true;
				DbConnectionInternal dbConnectionInternal2 = dbConnectionInternal;
				lock (dbConnectionInternal2)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.DestroyObject(dbConnectionInternal);
				}
				else
				{
					dbConnectionInternal.SetInStasis();
				}
			}
			if (this._waitHandles.PoolSemaphore.WaitOne(0, false))
			{
				DbConnectionInternal dbConnectionInternal3;
				while (this._stackNew.TryPop(out dbConnectionInternal3))
				{
					SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.CleanupCallback|RES|INFO|CPOOL> {0}, ChangeStacks={1}", this.ObjectID, dbConnectionInternal3.ObjectID);
					this._stackOld.Push(dbConnectionInternal3);
				}
				this._waitHandles.PoolSemaphore.Release(1);
			}
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0006FA18 File Offset: 0x0006DC18
		internal void Clear()
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.Clear|RES|CPOOL> {0}, Clearing.", this.ObjectID);
			List<DbConnectionInternal> objectList = this._objectList;
			DbConnectionInternal dbConnectionInternal;
			lock (objectList)
			{
				int count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						dbConnectionInternal.DoNotPoolThisConnection();
					}
				}
				goto IL_007C;
			}
			IL_0065:
			this.PerformanceCounters.NumberOfFreeConnections.Decrement();
			this.DestroyObject(dbConnectionInternal);
			IL_007C:
			if (!this._stackNew.TryPop(out dbConnectionInternal))
			{
				while (this._stackOld.TryPop(out dbConnectionInternal))
				{
					this.PerformanceCounters.NumberOfFreeConnections.Decrement();
					this.DestroyObject(dbConnectionInternal);
				}
				this.ReclaimEmancipatedObjects();
				SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.Clear|RES|CPOOL> {0}, Cleared.", this.ObjectID);
				return;
			}
			goto IL_0065;
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0006FB04 File Offset: 0x0006DD04
		private Timer CreateCleanupTimer()
		{
			return new Timer(new TimerCallback(this.CleanupCallback), null, this._cleanupWait, this._cleanupWait);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0006FB24 File Offset: 0x0006DD24
		private bool IsBlockingPeriodEnabled()
		{
			SqlConnectionString sqlConnectionString = this._connectionPoolGroup.ConnectionOptions as SqlConnectionString;
			if (sqlConnectionString == null)
			{
				return true;
			}
			switch (sqlConnectionString.PoolBlockingPeriod)
			{
			case PoolBlockingPeriod.Auto:
				return !ADP.IsAzureSqlServerEndpoint(sqlConnectionString.DataSource);
			case PoolBlockingPeriod.AlwaysBlock:
				return true;
			case PoolBlockingPeriod.NeverBlock:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0006FB78 File Offset: 0x0006DD78
		private DbConnectionInternal CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			try
			{
				dbConnectionInternal = this._connectionFactory.CreatePooledConnection(this, owningObject, this._connectionPoolGroup.ConnectionOptions, this._connectionPoolGroup.PoolKey, userOptions);
				if (dbConnectionInternal == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateObjectReturnedNull);
				}
				if (!dbConnectionInternal.CanBePooled)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NewObjectCannotBePooled);
				}
				dbConnectionInternal.PrePush(null);
				List<DbConnectionInternal> objectList = this._objectList;
				lock (objectList)
				{
					if (oldConnection != null && oldConnection.Pool == this)
					{
						this._objectList.Remove(oldConnection);
					}
					this._objectList.Add(dbConnectionInternal);
					this._totalObjects = this._objectList.Count;
					this.PerformanceCounters.NumberOfPooledConnections.Increment();
				}
				if (oldConnection != null)
				{
					DbConnectionPool pool = oldConnection.Pool;
					if (pool != null && pool != this)
					{
						List<DbConnectionInternal> objectList2 = pool._objectList;
						lock (objectList2)
						{
							pool._objectList.Remove(oldConnection);
							pool._totalObjects = pool._objectList.Count;
						}
					}
				}
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int?>("<prov.DbConnectionPool.CreateObject|RES|CPOOL> {0}, Connection {1}, Added to pool.", this.ObjectID, (dbConnectionInternal != null) ? new int?(dbConnectionInternal.ObjectID) : null);
				this._errorWait = 5000;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionForCapture(ex);
				if (!this.IsBlockingPeriodEnabled())
				{
					throw;
				}
				if (dbConnectionInternal != null && dbConnectionInternal.IsConnectionAlive(false))
				{
					dbConnectionInternal.Dispose();
				}
				dbConnectionInternal = null;
				this._resError = ex;
				Timer timer = new Timer(new TimerCallback(this.ErrorCallback), null, -1, -1);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this._waitHandles.ErrorEvent.Set();
					this._errorOccurred = true;
					this._errorTimer = timer;
					bool flag3 = timer.Change(this._errorWait, this._errorWait);
				}
				if (30000 < this._errorWait)
				{
					this._errorWait = 60000;
				}
				else
				{
					this._errorWait *= 2;
				}
				throw;
			}
			return dbConnectionInternal;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0006FDE0 File Offset: 0x0006DFE0
		private void DeactivateObject(DbConnectionInternal obj)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.DeactivateObject|RES|CPOOL> {0}, Connection {1}, Deactivating.", this.ObjectID, obj.ObjectID);
			obj.DeactivateConnection();
			bool flag = false;
			bool flag2 = false;
			if (obj.IsConnectionDoomed)
			{
				flag2 = true;
			}
			else
			{
				lock (obj)
				{
					if (this._state == DbConnectionPool.State.ShuttingDown)
					{
						if (obj.IsTransactionRoot)
						{
							obj.SetInStasis();
						}
						else
						{
							flag2 = true;
						}
					}
					else if (obj.IsNonPoolableTransactionRoot)
					{
						obj.SetInStasis();
					}
					else if (obj.CanBePooled)
					{
						Transaction enlistedTransaction = obj.EnlistedTransaction;
						if (null != enlistedTransaction)
						{
							this._transactedConnectionPool.PutTransactedObject(enlistedTransaction, obj);
						}
						else
						{
							flag = true;
						}
					}
					else if (obj.IsTransactionRoot && !obj.IsConnectionDoomed)
					{
						obj.SetInStasis();
					}
					else
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.PutNewObject(obj);
				return;
			}
			if (flag2)
			{
				this.DestroyObject(obj);
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0006FEE8 File Offset: 0x0006E0E8
		internal void DestroyObject(DbConnectionInternal obj)
		{
			if (obj.IsTxRootWaitingForTxEnd)
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> {0}, Connection {1}, Has Delegated Transaction, waiting to Dispose.", this.ObjectID, obj.ObjectID);
				return;
			}
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> {0}, Connection {1}, Removing from pool.", this.ObjectID, obj.ObjectID);
			bool flag = false;
			List<DbConnectionInternal> objectList = this._objectList;
			lock (objectList)
			{
				flag = this._objectList.Remove(obj);
				this._totalObjects = this._objectList.Count;
			}
			if (flag)
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> {0}, Connection {1}, Removed from pool.", this.ObjectID, obj.ObjectID);
				this.PerformanceCounters.NumberOfPooledConnections.Decrement();
			}
			obj.Dispose();
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> {0}, Connection {1}, Disposed.", this.ObjectID, obj.ObjectID);
			this.PerformanceCounters.HardDisconnectsPerSecond.Increment();
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0006FFE0 File Offset: 0x0006E1E0
		private void ErrorCallback(object state)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.ErrorCallback|RES|CPOOL> {0}, Resetting Error handling.", this.ObjectID);
			this._errorOccurred = false;
			this._waitHandles.ErrorEvent.Reset();
			Timer errorTimer = this._errorTimer;
			this._errorTimer = null;
			if (errorTimer != null)
			{
				errorTimer.Dispose();
			}
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00070033 File Offset: 0x0006E233
		private Exception TryCloneCachedException()
		{
			if (this._resError == null)
			{
				return null;
			}
			if (this._resError.GetType() == typeof(SqlException))
			{
				return ((SqlException)this._resError).InternalClone();
			}
			return this._resError;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00070074 File Offset: 0x0006E274
		private void WaitForPendingOpen()
		{
			DbConnectionPool.PendingGetConnection pendingGetConnection;
			do
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						flag = Interlocked.CompareExchange(ref this._pendingOpensWaiting, 1, 0) == 0;
					}
					if (!flag)
					{
						break;
					}
					while (this._pendingOpens.TryDequeue(out pendingGetConnection))
					{
						if (!pendingGetConnection.Completion.Task.IsCompleted)
						{
							uint num;
							if (pendingGetConnection.DueTime == -1L)
							{
								num = uint.MaxValue;
							}
							else
							{
								num = (uint)Math.Max(ADP.TimerRemainingMilliseconds(pendingGetConnection.DueTime), 0L);
							}
							DbConnectionInternal dbConnectionInternal = null;
							bool flag2 = false;
							Exception ex = null;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								bool flag3 = true;
								bool flag4 = false;
								ADP.SetCurrentTransaction(pendingGetConnection.Completion.Task.AsyncState as Transaction);
								flag2 = !this.TryGetConnection(pendingGetConnection.Owner, num, flag3, flag4, pendingGetConnection.UserOptions, out dbConnectionInternal);
							}
							catch (OutOfMemoryException)
							{
								if (dbConnectionInternal != null)
								{
									dbConnectionInternal.DoomThisConnection();
								}
								throw;
							}
							catch (StackOverflowException)
							{
								if (dbConnectionInternal != null)
								{
									dbConnectionInternal.DoomThisConnection();
								}
								throw;
							}
							catch (ThreadAbortException)
							{
								if (dbConnectionInternal != null)
								{
									dbConnectionInternal.DoomThisConnection();
								}
								throw;
							}
							catch (Exception ex2)
							{
								ex = ex2;
							}
							if (ex != null)
							{
								pendingGetConnection.Completion.TrySetException(ex);
							}
							else if (flag2)
							{
								pendingGetConnection.Completion.TrySetException(ADP.ExceptionWithStackTrace(ADP.PooledOpenTimeout()));
							}
							else if (!pendingGetConnection.Completion.TrySetResult(dbConnectionInternal))
							{
								this.PutObject(dbConnectionInternal, pendingGetConnection.Owner);
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						Interlocked.Exchange(ref this._pendingOpensWaiting, 0);
					}
				}
			}
			while (this._pendingOpens.TryPeek(out pendingGetConnection));
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00070274 File Offset: 0x0006E474
		internal bool TryGetConnection(DbConnection owningObject, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			uint num = 0U;
			bool flag = false;
			if (retry == null)
			{
				num = (uint)this.CreationTimeout;
				if (num == 0U)
				{
					num = uint.MaxValue;
				}
				flag = true;
			}
			if (this._state != DbConnectionPool.State.Running)
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, DbConnectionInternal State != Running.", this.ObjectID);
				connection = null;
				return true;
			}
			bool flag2 = true;
			if (this.TryGetConnection(owningObject, num, flag, flag2, userOptions, out connection))
			{
				return true;
			}
			if (retry == null)
			{
				return true;
			}
			DbConnectionPool.PendingGetConnection pendingGetConnection = new DbConnectionPool.PendingGetConnection((this.CreationTimeout == 0) ? (-1L) : (ADP.TimerCurrent() + ADP.TimerFromSeconds(this.CreationTimeout / 1000)), owningObject, retry, userOptions);
			this._pendingOpens.Enqueue(pendingGetConnection);
			if (this._pendingOpensWaiting == 0)
			{
				new Thread(new ThreadStart(this.WaitForPendingOpen))
				{
					IsBackground = true
				}.Start();
			}
			connection = null;
			return false;
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0007033C File Offset: 0x0006E53C
		private bool TryGetConnection(DbConnection owningObject, uint waitForMultipleObjectsTimeout, bool allowCreate, bool onlyOneCheckConnection, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			Transaction transaction = null;
			this.PerformanceCounters.SoftConnectsPerSecond.Increment();
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Getting connection.", this.ObjectID);
			if (this.HasTransactionAffinity)
			{
				dbConnectionInternal = this.GetFromTransactedPool(out transaction);
			}
			if (dbConnectionInternal == null)
			{
				Interlocked.Increment(ref this._waitCount);
				uint num = (allowCreate ? 3U : 2U);
				for (;;)
				{
					int num2 = 3;
					int num3 = 0;
					bool flag = false;
					int num4 = 0;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._waitHandles.DangerousAddRef(ref flag);
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							num2 = SafeNativeMethods.WaitForMultipleObjectsEx(num, this._waitHandles.DangerousGetHandle(), false, waitForMultipleObjectsTimeout, false);
							if (num2 == -1)
							{
								num4 = Marshal.GetHRForLastWin32Error();
							}
						}
						switch (num2)
						{
						case -1:
							SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Wait failed.", this.ObjectID);
							Interlocked.Decrement(ref this._waitCount);
							Marshal.ThrowExceptionForHR(num4);
							break;
						case 0:
							Interlocked.Decrement(ref this._waitCount);
							dbConnectionInternal = this.GetFromGeneralPool();
							if (dbConnectionInternal == null || dbConnectionInternal.IsConnectionAlive(false))
							{
								goto IL_0352;
							}
							SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Connection {1}, found dead and removed.", this.ObjectID, dbConnectionInternal.ObjectID);
							this.DestroyObject(dbConnectionInternal);
							dbConnectionInternal = null;
							if (onlyOneCheckConnection)
							{
								if (this._waitHandles.CreationSemaphore.WaitOne((int)waitForMultipleObjectsTimeout))
								{
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Creating new connection.", this.ObjectID);
										dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
										goto IL_0390;
									}
									finally
									{
										this._waitHandles.CreationSemaphore.Release(1);
									}
								}
								SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Wait timed out.", this.ObjectID);
								connection = null;
								return false;
							}
							goto IL_0352;
						case 1:
							SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Errors are set.", this.ObjectID);
							Interlocked.Decrement(ref this._waitCount);
							throw this.TryCloneCachedException();
						case 2:
							SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Creating new connection.", this.ObjectID);
							try
							{
								dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
							}
							catch
							{
								if (dbConnectionInternal == null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
								throw;
							}
							finally
							{
								if (dbConnectionInternal != null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
							}
							if (dbConnectionInternal == null && this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
							{
								num = 2U;
								goto IL_0390;
							}
							goto IL_0352;
						default:
							switch (num2)
							{
							case 128:
								SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Semaphore handle abandonded.", this.ObjectID);
								Interlocked.Decrement(ref this._waitCount);
								throw new AbandonedMutexException(0, this._waitHandles.PoolSemaphore);
							case 129:
								SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Error handle abandonded.", this.ObjectID);
								Interlocked.Decrement(ref this._waitCount);
								throw new AbandonedMutexException(1, this._waitHandles.ErrorEvent);
							case 130:
								SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Creation handle abandoned.", this.ObjectID);
								Interlocked.Decrement(ref this._waitCount);
								throw new AbandonedMutexException(2, this._waitHandles.CreationSemaphore);
							default:
								if (num2 == 258)
								{
									SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, Wait timed out.", this.ObjectID);
									Interlocked.Decrement(ref this._waitCount);
									connection = null;
									return false;
								}
								break;
							}
							break;
						}
						SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.GetConnection|RES|CPOOL> {0}, WaitForMultipleObjects={1}", this.ObjectID, num2);
						Interlocked.Decrement(ref this._waitCount);
						throw ADP.InternalError(ADP.InternalErrorCode.UnexpectedWaitAnyResult);
						IL_0352:;
					}
					finally
					{
						if (2 == num2 && SafeNativeMethods.ReleaseSemaphore(this._waitHandles.CreationHandle.DangerousGetHandle(), 1, IntPtr.Zero) == 0)
						{
							num3 = Marshal.GetHRForLastWin32Error();
						}
						if (flag)
						{
							this._waitHandles.DangerousRelease();
						}
					}
					IL_0390:
					if (num3 != 0)
					{
						Marshal.ThrowExceptionForHR(num3);
					}
					if (dbConnectionInternal != null && dbConnectionInternal.IsAccessTokenExpired)
					{
						this.DestroyObject(dbConnectionInternal);
						dbConnectionInternal = null;
					}
					if (dbConnectionInternal != null)
					{
						goto IL_03B5;
					}
				}
				bool flag2;
				return flag2;
			}
			IL_03B5:
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, transaction);
			}
			connection = dbConnectionInternal;
			return true;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00070790 File Offset: 0x0006E990
		private void PrepareConnection(DbConnection owningObject, DbConnectionInternal obj, Transaction transaction)
		{
			lock (obj)
			{
				obj.PostPop(owningObject);
			}
			try
			{
				obj.ActivateConnection(transaction);
			}
			catch
			{
				this.PutObject(obj, owningObject);
				throw;
			}
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x000707F0 File Offset: 0x0006E9F0
		internal DbConnectionInternal ReplaceConnection(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			this.PerformanceCounters.SoftConnectsPerSecond.Increment();
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.ReplaceConnection|RES|CPOOL> {0}, replacing connection.", this.ObjectID);
			DbConnectionInternal dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, oldConnection);
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, oldConnection.EnlistedTransaction);
				oldConnection.PrepareForReplaceConnection();
				oldConnection.DeactivateConnection();
				oldConnection.Dispose();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00070850 File Offset: 0x0006EA50
		private DbConnectionInternal GetFromGeneralPool()
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (!this._stackNew.TryPop(out dbConnectionInternal) && !this._stackOld.TryPop(out dbConnectionInternal))
			{
				dbConnectionInternal = null;
			}
			if (dbConnectionInternal != null)
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.GetFromGeneralPool|RES|CPOOL> {0}, Connection {1}, Popped from general pool.", this.ObjectID, dbConnectionInternal.ObjectID);
				this.PerformanceCounters.NumberOfFreeConnections.Decrement();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x000708B0 File Offset: 0x0006EAB0
		private DbConnectionInternal GetFromTransactedPool(out Transaction transaction)
		{
			transaction = ADP.GetCurrentTransaction();
			DbConnectionInternal dbConnectionInternal = null;
			if (null != transaction && this._transactedConnectionPool != null)
			{
				dbConnectionInternal = this._transactedConnectionPool.GetTransactedObject(transaction);
				if (dbConnectionInternal != null)
				{
					SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.GetFromTransactedPool|RES|CPOOL> {0}, Connection {1}, Popped from transacted pool.", this.ObjectID, dbConnectionInternal.ObjectID);
					this.PerformanceCounters.NumberOfFreeConnections.Decrement();
					if (dbConnectionInternal.IsTransactionRoot)
					{
						try
						{
							dbConnectionInternal.IsConnectionAlive(true);
							return dbConnectionInternal;
						}
						catch
						{
							SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.GetFromTransactedPool|RES|CPOOL> {0}, Connection {1}, found dead and removed.", this.ObjectID, dbConnectionInternal.ObjectID);
							this.DestroyObject(dbConnectionInternal);
							throw;
						}
					}
					if (!dbConnectionInternal.IsConnectionAlive(false))
					{
						SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.GetFromTransactedPool|RES|CPOOL> {0}, Connection {1}, found dead and removed.", this.ObjectID, dbConnectionInternal.ObjectID);
						this.DestroyObject(dbConnectionInternal);
						dbConnectionInternal = null;
					}
				}
			}
			return dbConnectionInternal;
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00070994 File Offset: 0x0006EB94
		private void PoolCreateRequest(object state)
		{
			long num = SqlClientEventSource.Log.TryPoolerScopeEnterEvent<int>("<prov.DbConnectionPool.PoolCreateRequest|RES|INFO|CPOOL> {0}", this.ObjectID);
			try
			{
				if (DbConnectionPool.State.Running == this._state)
				{
					if (!this._pendingOpens.IsEmpty && this._pendingOpensWaiting == 0)
					{
						new Thread(new ThreadStart(this.WaitForPendingOpen))
						{
							IsBackground = true
						}.Start();
					}
					this.ReclaimEmancipatedObjects();
					if (!this.ErrorOccurred && this.NeedToReplenish)
					{
						if (!this.UsingIntegrateSecurity || this._identity.Equals(DbConnectionPoolIdentity.GetCurrent()))
						{
							bool flag = false;
							int num2 = 3;
							uint creationTimeout = (uint)this.CreationTimeout;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								this._waitHandles.DangerousAddRef(ref flag);
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
								}
								finally
								{
									num2 = SafeNativeMethods.WaitForSingleObjectEx(this._waitHandles.CreationHandle.DangerousGetHandle(), creationTimeout, false);
								}
								if (num2 == 0)
								{
									if (!this.ErrorOccurred)
									{
										while (this.NeedToReplenish)
										{
											DbConnectionInternal dbConnectionInternal = this.CreateObject(null, null, null);
											if (dbConnectionInternal == null)
											{
												break;
											}
											this.PutNewObject(dbConnectionInternal);
										}
									}
								}
								else if (258 == num2)
								{
									this.QueuePoolCreateRequest();
								}
								else
								{
									SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.PoolCreateRequest|RES|CPOOL> {0}, PoolCreateRequest called WaitForSingleObject failed {1}", this.ObjectID, num2);
								}
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								SqlClientEventSource.Log.TryPoolerTraceEvent<int, Exception>("<prov.DbConnectionPool.PoolCreateRequest|RES|CPOOL> {0}, PoolCreateRequest called CreateConnection which threw an exception: {1}", this.ObjectID, ex);
							}
							finally
							{
								if (num2 == 0)
								{
									num2 = SafeNativeMethods.ReleaseSemaphore(this._waitHandles.CreationHandle.DangerousGetHandle(), 1, IntPtr.Zero);
								}
								if (flag)
								{
									this._waitHandles.DangerousRelease();
								}
							}
						}
					}
				}
			}
			finally
			{
				SqlClientEventSource.Log.TryPoolerScopeLeaveEvent(num);
			}
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x00070B90 File Offset: 0x0006ED90
		internal void PutNewObject(DbConnectionInternal obj)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.PutNewObject|RES|CPOOL> {0}, Connection {1}, Pushing to general pool.", this.ObjectID, obj.ObjectID);
			this._stackNew.Push(obj);
			this._waitHandles.PoolSemaphore.Release(1);
			this.PerformanceCounters.NumberOfFreeConnections.Increment();
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x00070BE8 File Offset: 0x0006EDE8
		internal void PutObject(DbConnectionInternal obj, object owningObject)
		{
			this.PerformanceCounters.SoftDisconnectsPerSecond.Increment();
			lock (obj)
			{
				obj.PrePush(owningObject);
			}
			this.DeactivateObject(obj);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00070C3C File Offset: 0x0006EE3C
		internal void PutObjectFromTransactedPool(DbConnectionInternal obj)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.PutObjectFromTransactedPool|RES|CPOOL> {0}, Connection {1}, Transaction has ended.", this.ObjectID, obj.ObjectID);
			if (this._state == DbConnectionPool.State.Running && obj.CanBePooled)
			{
				this.PutNewObject(obj);
				return;
			}
			this.DestroyObject(obj);
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06001B85 RID: 7045 RVA: 0x00070C8A File Offset: 0x0006EE8A
		private void QueuePoolCreateRequest()
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				ThreadPool.QueueUserWorkItem(this._poolCreateRequest);
			}
		}

		// Token: 0x06001B86 RID: 7046 RVA: 0x00070CA4 File Offset: 0x0006EEA4
		private bool ReclaimEmancipatedObjects()
		{
			bool flag = false;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.ReclaimEmancipatedObjects|RES|CPOOL> {0}", this.ObjectID);
			List<DbConnectionInternal> list = new List<DbConnectionInternal>();
			List<DbConnectionInternal> objectList = this._objectList;
			int num;
			lock (objectList)
			{
				num = this._objectList.Count;
				for (int i = 0; i < num; i++)
				{
					DbConnectionInternal dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						bool flag3 = false;
						try
						{
							Monitor.TryEnter(dbConnectionInternal, ref flag3);
							if (flag3 && dbConnectionInternal.IsEmancipated)
							{
								dbConnectionInternal.PrePush(null);
								list.Add(dbConnectionInternal);
							}
						}
						finally
						{
							if (flag3)
							{
								Monitor.Exit(dbConnectionInternal);
							}
						}
					}
				}
			}
			num = list.Count;
			for (int j = 0; j < num; j++)
			{
				DbConnectionInternal dbConnectionInternal2 = list[j];
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.ReclaimEmancipatedObjects|RES|CPOOL> {0}, Connection {1}, Reclaiming.", this.ObjectID, dbConnectionInternal2.ObjectID);
				this.PerformanceCounters.NumberOfReclaimedConnections.Increment();
				flag = true;
				dbConnectionInternal2.DetachCurrentTransactionIfEnded();
				this.DeactivateObject(dbConnectionInternal2);
			}
			return flag;
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		internal void Startup()
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.Startup|RES|INFO|CPOOL> {0}, CleanupWait={1}", this.ObjectID, this._cleanupWait);
			this._cleanupTimer = this.CreateCleanupTimer();
			if (this.NeedToReplenish)
			{
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00070E08 File Offset: 0x0006F008
		internal void Shutdown()
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionPool.Shutdown|RES|INFO|CPOOL> {0}", this.ObjectID);
			this._state = DbConnectionPool.State.ShuttingDown;
			Timer cleanupTimer = this._cleanupTimer;
			this._cleanupTimer = null;
			if (cleanupTimer != null)
			{
				cleanupTimer.Dispose();
			}
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00070E48 File Offset: 0x0006F048
		internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactionEnded|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Transaction Completed", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
			DbConnectionPool.TransactedConnectionPool transactedConnectionPool = this._transactedConnectionPool;
			if (transactedConnectionPool != null)
			{
				transactedConnectionPool.TransactionEnded(transaction, transactedObject);
			}
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00070E88 File Offset: 0x0006F088
		private DbConnectionInternal UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection = null)
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (this.ErrorOccurred)
			{
				throw this.TryCloneCachedException();
			}
			if ((oldConnection != null || this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && (oldConnection != null || (this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				dbConnectionInternal = this.CreateObject(owningObject, userOptions, oldConnection);
			}
			return dbConnectionInternal;
		}

		// Token: 0x04000B11 RID: 2833
		private const int MAX_Q_SIZE = 1048576;

		// Token: 0x04000B12 RID: 2834
		private const int SEMAPHORE_HANDLE = 0;

		// Token: 0x04000B13 RID: 2835
		private const int ERROR_HANDLE = 1;

		// Token: 0x04000B14 RID: 2836
		private const int CREATION_HANDLE = 2;

		// Token: 0x04000B15 RID: 2837
		private const int BOGUS_HANDLE = 3;

		// Token: 0x04000B16 RID: 2838
		private const int WAIT_OBJECT_0 = 0;

		// Token: 0x04000B17 RID: 2839
		private const int WAIT_TIMEOUT = 258;

		// Token: 0x04000B18 RID: 2840
		private const int WAIT_ABANDONED = 128;

		// Token: 0x04000B19 RID: 2841
		private const int WAIT_FAILED = -1;

		// Token: 0x04000B1A RID: 2842
		private const int ERROR_WAIT_DEFAULT = 5000;

		// Token: 0x04000B1B RID: 2843
		private static readonly Random _random = new Random(5101977);

		// Token: 0x04000B1C RID: 2844
		private readonly int _cleanupWait;

		// Token: 0x04000B1D RID: 2845
		private readonly DbConnectionPoolIdentity _identity;

		// Token: 0x04000B1E RID: 2846
		private readonly DbConnectionFactory _connectionFactory;

		// Token: 0x04000B1F RID: 2847
		private readonly DbConnectionPoolGroup _connectionPoolGroup;

		// Token: 0x04000B20 RID: 2848
		private readonly DbConnectionPoolGroupOptions _connectionPoolGroupOptions;

		// Token: 0x04000B21 RID: 2849
		private DbConnectionPoolProviderInfo _connectionPoolProviderInfo;

		// Token: 0x04000B22 RID: 2850
		private readonly ConcurrentDictionary<DbConnectionPoolAuthenticationContextKey, DbConnectionPoolAuthenticationContext> _pooledDbAuthenticationContexts;

		// Token: 0x04000B23 RID: 2851
		private DbConnectionPool.State _state;

		// Token: 0x04000B24 RID: 2852
		private readonly ConcurrentStack<DbConnectionInternal> _stackOld = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04000B25 RID: 2853
		private readonly ConcurrentStack<DbConnectionInternal> _stackNew = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04000B26 RID: 2854
		private readonly ConcurrentQueue<DbConnectionPool.PendingGetConnection> _pendingOpens = new ConcurrentQueue<DbConnectionPool.PendingGetConnection>();

		// Token: 0x04000B27 RID: 2855
		private int _pendingOpensWaiting;

		// Token: 0x04000B28 RID: 2856
		private readonly WaitCallback _poolCreateRequest;

		// Token: 0x04000B29 RID: 2857
		private int _waitCount;

		// Token: 0x04000B2A RID: 2858
		private readonly DbConnectionPool.PoolWaitHandles _waitHandles;

		// Token: 0x04000B2B RID: 2859
		private Exception _resError;

		// Token: 0x04000B2C RID: 2860
		private volatile bool _errorOccurred;

		// Token: 0x04000B2D RID: 2861
		private int _errorWait;

		// Token: 0x04000B2E RID: 2862
		private Timer _errorTimer;

		// Token: 0x04000B2F RID: 2863
		private Timer _cleanupTimer;

		// Token: 0x04000B30 RID: 2864
		private readonly DbConnectionPool.TransactedConnectionPool _transactedConnectionPool;

		// Token: 0x04000B31 RID: 2865
		private readonly List<DbConnectionInternal> _objectList;

		// Token: 0x04000B32 RID: 2866
		private int _totalObjects;

		// Token: 0x04000B33 RID: 2867
		private static int _objectTypeCount;

		// Token: 0x04000B34 RID: 2868
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool._objectTypeCount);

		// Token: 0x0200026F RID: 623
		private enum State
		{
			// Token: 0x04001747 RID: 5959
			Initializing,
			// Token: 0x04001748 RID: 5960
			Running,
			// Token: 0x04001749 RID: 5961
			ShuttingDown
		}

		// Token: 0x02000270 RID: 624
		private sealed class TransactedConnectionList : List<DbConnectionInternal>
		{
			// Token: 0x06001F27 RID: 7975 RVA: 0x0007F312 File Offset: 0x0007D512
			internal TransactedConnectionList(int initialAllocation, Transaction tx)
				: base(initialAllocation)
			{
				this._transaction = tx;
			}

			// Token: 0x06001F28 RID: 7976 RVA: 0x0007F322 File Offset: 0x0007D522
			internal void Dispose()
			{
				if (null != this._transaction)
				{
					this._transaction.Dispose();
				}
			}

			// Token: 0x0400174A RID: 5962
			private Transaction _transaction;
		}

		// Token: 0x02000271 RID: 625
		private sealed class PendingGetConnection
		{
			// Token: 0x06001F29 RID: 7977 RVA: 0x0007F33D File Offset: 0x0007D53D
			public PendingGetConnection(long dueTime, DbConnection owner, TaskCompletionSource<DbConnectionInternal> completion, DbConnectionOptions userOptions)
			{
				this.DueTime = dueTime;
				this.Owner = owner;
				this.Completion = completion;
				this.UserOptions = userOptions;
			}

			// Token: 0x17000A47 RID: 2631
			// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0007F362 File Offset: 0x0007D562
			// (set) Token: 0x06001F2B RID: 7979 RVA: 0x0007F36A File Offset: 0x0007D56A
			public long DueTime { get; private set; }

			// Token: 0x17000A48 RID: 2632
			// (get) Token: 0x06001F2C RID: 7980 RVA: 0x0007F373 File Offset: 0x0007D573
			// (set) Token: 0x06001F2D RID: 7981 RVA: 0x0007F37B File Offset: 0x0007D57B
			public DbConnection Owner { get; private set; }

			// Token: 0x17000A49 RID: 2633
			// (get) Token: 0x06001F2E RID: 7982 RVA: 0x0007F384 File Offset: 0x0007D584
			// (set) Token: 0x06001F2F RID: 7983 RVA: 0x0007F38C File Offset: 0x0007D58C
			public TaskCompletionSource<DbConnectionInternal> Completion { get; private set; }

			// Token: 0x17000A4A RID: 2634
			// (get) Token: 0x06001F30 RID: 7984 RVA: 0x0007F395 File Offset: 0x0007D595
			// (set) Token: 0x06001F31 RID: 7985 RVA: 0x0007F39D File Offset: 0x0007D59D
			public DbConnectionOptions UserOptions { get; private set; }
		}

		// Token: 0x02000272 RID: 626
		private sealed class TransactedConnectionPool
		{
			// Token: 0x06001F32 RID: 7986 RVA: 0x0007F3A8 File Offset: 0x0007D5A8
			internal TransactedConnectionPool(DbConnectionPool pool)
			{
				this._pool = pool;
				this._transactedCxns = new Dictionary<Transaction, DbConnectionPool.TransactedConnectionList>();
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.TransactedConnectionPool.TransactedConnectionPool|RES|CPOOL> {0}, Constructed for connection pool {1}", this.ObjectID, this._pool.ObjectID);
			}

			// Token: 0x17000A4B RID: 2635
			// (get) Token: 0x06001F33 RID: 7987 RVA: 0x0007F3FD File Offset: 0x0007D5FD
			internal int ObjectID
			{
				get
				{
					return this._objectID;
				}
			}

			// Token: 0x17000A4C RID: 2636
			// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0007F405 File Offset: 0x0007D605
			internal DbConnectionPool Pool
			{
				get
				{
					return this._pool;
				}
			}

			// Token: 0x06001F35 RID: 7989 RVA: 0x0007F410 File Offset: 0x0007D610
			internal DbConnectionInternal GetTransactedObject(Transaction transaction)
			{
				DbConnectionInternal dbConnectionInternal = null;
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				DbConnectionPool.TransactedConnectionList transactedConnectionList;
				lock (transactedCxns)
				{
					flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList);
				}
				if (flag)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
					lock (transactedConnectionList2)
					{
						int num = transactedConnectionList.Count - 1;
						if (0 <= num)
						{
							dbConnectionInternal = transactedConnectionList[num];
							transactedConnectionList.RemoveAt(num);
						}
					}
				}
				if (dbConnectionInternal != null)
				{
					SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.GetTransactedObject|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Popped.", this.ObjectID, transaction.GetHashCode(), dbConnectionInternal.ObjectID);
				}
				return dbConnectionInternal;
			}

			// Token: 0x06001F36 RID: 7990 RVA: 0x0007F4D0 File Offset: 0x0007D6D0
			internal void PutTransactedObject(Transaction transaction, DbConnectionInternal transactedObject)
			{
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
						lock (transactedConnectionList2)
						{
							SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Pushing.", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
							transactedConnectionList.Add(transactedObject);
						}
					}
				}
				if (!flag)
				{
					Transaction transaction2 = null;
					DbConnectionPool.TransactedConnectionList transactedConnectionList3 = null;
					try
					{
						transaction2 = transaction.Clone();
						transactedConnectionList3 = new DbConnectionPool.TransactedConnectionList(2, transaction2);
						Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns2 = this._transactedCxns;
						lock (transactedCxns2)
						{
							DbConnectionPool.TransactedConnectionList transactedConnectionList;
							if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
							{
								DbConnectionPool.TransactedConnectionList transactedConnectionList4 = transactedConnectionList;
								lock (transactedConnectionList4)
								{
									SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Pushing.", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
									transactedConnectionList.Add(transactedObject);
									goto IL_0167;
								}
							}
							SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Adding List to transacted pool.", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
							transactedConnectionList3.Add(transactedObject);
							this._transactedCxns.Add(transaction2, transactedConnectionList3);
							transaction2 = null;
						}
					}
					finally
					{
						if (null != transaction2)
						{
							if (transactedConnectionList3 != null)
							{
								transactedConnectionList3.Dispose();
							}
							else
							{
								transaction2.Dispose();
							}
						}
					}
					IL_0167:
					SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Added.", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
				}
				this.Pool.PerformanceCounters.NumberOfFreeConnections.Increment();
			}

			// Token: 0x06001F37 RID: 7991 RVA: 0x0007F6BC File Offset: 0x0007D8BC
			internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.TransactionEnded|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Transaction Completed", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
				int num = -1;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						bool flag2 = false;
						DbConnectionPool.TransactedConnectionList transactedConnectionList2 = transactedConnectionList;
						lock (transactedConnectionList2)
						{
							num = transactedConnectionList.IndexOf(transactedObject);
							if (num >= 0)
							{
								transactedConnectionList.RemoveAt(num);
							}
							if (0 >= transactedConnectionList.Count)
							{
								SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionPool.TransactedConnectionPool.TransactionEnded|RES|CPOOL> {0}, Transaction {1}, Removing List from transacted pool.", this.ObjectID, transaction.GetHashCode());
								this._transactedCxns.Remove(transaction);
								flag2 = true;
							}
						}
						if (flag2)
						{
							transactedConnectionList.Dispose();
						}
					}
					else
					{
						SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionPool.TransactedConnectionPool.TransactionEnded|RES|CPOOL> {0}, Transaction {1}, Connection {2}, Transacted pool not yet created prior to transaction completing. Connection may be leaked.", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
					}
				}
				if (0 <= num)
				{
					this.Pool.PerformanceCounters.NumberOfFreeConnections.Decrement();
					this.Pool.PutObjectFromTransactedPool(transactedObject);
				}
			}

			// Token: 0x0400174F RID: 5967
			private Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> _transactedCxns;

			// Token: 0x04001750 RID: 5968
			private DbConnectionPool _pool;

			// Token: 0x04001751 RID: 5969
			private static int _objectTypeCount;

			// Token: 0x04001752 RID: 5970
			internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool.TransactedConnectionPool._objectTypeCount);
		}

		// Token: 0x02000273 RID: 627
		private sealed class PoolWaitHandles : DbBuffer
		{
			// Token: 0x06001F38 RID: 7992 RVA: 0x0007F7F0 File Offset: 0x0007D9F0
			internal PoolWaitHandles()
				: base(3 * IntPtr.Size)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				this._poolSemaphore = new Semaphore(0, 1048576);
				this._errorEvent = new ManualResetEvent(false);
				this._creationSemaphore = new Semaphore(1, 1);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._poolHandle = this._poolSemaphore.SafeWaitHandle;
					this._errorHandle = this._errorEvent.SafeWaitHandle;
					this._creationHandle = this._creationSemaphore.SafeWaitHandle;
					this._poolHandle.DangerousAddRef(ref flag);
					this._errorHandle.DangerousAddRef(ref flag2);
					this._creationHandle.DangerousAddRef(ref flag3);
					int size = IntPtr.Size;
					base.WriteIntPtr(0, this._poolHandle.DangerousGetHandle());
					base.WriteIntPtr(IntPtr.Size, this._errorHandle.DangerousGetHandle());
					base.WriteIntPtr(2 * IntPtr.Size, this._creationHandle.DangerousGetHandle());
				}
				finally
				{
					if (flag)
					{
						this._releaseFlags |= 1;
					}
					if (flag2)
					{
						this._releaseFlags |= 2;
					}
					if (flag3)
					{
						this._releaseFlags |= 4;
					}
				}
			}

			// Token: 0x17000A4D RID: 2637
			// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0007F928 File Offset: 0x0007DB28
			internal SafeHandle CreationHandle
			{
				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
				get
				{
					return this._creationHandle;
				}
			}

			// Token: 0x17000A4E RID: 2638
			// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0007F930 File Offset: 0x0007DB30
			internal Semaphore CreationSemaphore
			{
				get
				{
					return this._creationSemaphore;
				}
			}

			// Token: 0x17000A4F RID: 2639
			// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0007F938 File Offset: 0x0007DB38
			internal ManualResetEvent ErrorEvent
			{
				get
				{
					return this._errorEvent;
				}
			}

			// Token: 0x17000A50 RID: 2640
			// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0007F940 File Offset: 0x0007DB40
			internal Semaphore PoolSemaphore
			{
				get
				{
					return this._poolSemaphore;
				}
			}

			// Token: 0x06001F3D RID: 7997 RVA: 0x0007F948 File Offset: 0x0007DB48
			protected override bool ReleaseHandle()
			{
				if ((1 & this._releaseFlags) != 0)
				{
					this._poolHandle.DangerousRelease();
				}
				if ((2 & this._releaseFlags) != 0)
				{
					this._errorHandle.DangerousRelease();
				}
				if ((4 & this._releaseFlags) != 0)
				{
					this._creationHandle.DangerousRelease();
				}
				return base.ReleaseHandle();
			}

			// Token: 0x04001753 RID: 5971
			private readonly Semaphore _poolSemaphore;

			// Token: 0x04001754 RID: 5972
			private readonly ManualResetEvent _errorEvent;

			// Token: 0x04001755 RID: 5973
			private readonly Semaphore _creationSemaphore;

			// Token: 0x04001756 RID: 5974
			private readonly SafeHandle _poolHandle;

			// Token: 0x04001757 RID: 5975
			private readonly SafeHandle _errorHandle;

			// Token: 0x04001758 RID: 5976
			private readonly SafeHandle _creationHandle;

			// Token: 0x04001759 RID: 5977
			private readonly int _releaseFlags;
		}
	}
}
