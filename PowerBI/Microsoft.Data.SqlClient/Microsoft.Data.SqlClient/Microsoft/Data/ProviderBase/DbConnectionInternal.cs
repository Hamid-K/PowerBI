using System;
using System.Data;
using System.Data.Common;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000171 RID: 369
	internal abstract class DbConnectionInternal
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x0006EC82 File Offset: 0x0006CE82
		protected DbConnectionInternal()
			: this(ConnectionState.Open, true, false)
		{
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x0006EC8D File Offset: 0x0006CE8D
		internal DbConnectionInternal(ConnectionState state, bool hidePassword, bool allowSetConnectionString)
		{
			this._allowSetConnectionString = allowSetConnectionString;
			this._hidePassword = hidePassword;
			this._state = state;
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0006ECC7 File Offset: 0x0006CEC7
		internal bool AllowSetConnectionString
		{
			get
			{
				return this._allowSetConnectionString;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0006ECD0 File Offset: 0x0006CED0
		internal bool CanBePooled
		{
			get
			{
				DbConnection dbConnection;
				return !this._connectionIsDoomed && !this._cannotBePooled && !this._owningObject.TryGetTarget(out dbConnection);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0006ECFF File Offset: 0x0006CEFF
		// (set) Token: 0x06001B21 RID: 6945 RVA: 0x0006ED08 File Offset: 0x0006CF08
		protected internal Transaction EnlistedTransaction
		{
			get
			{
				return this._enlistedTransaction;
			}
			set
			{
				Transaction enlistedTransaction = this._enlistedTransaction;
				if ((null == enlistedTransaction && null != value) || (null != enlistedTransaction && !enlistedTransaction.Equals(value)))
				{
					Transaction transaction = null;
					Transaction transaction2 = null;
					try
					{
						if (null != value)
						{
							transaction = value.Clone();
						}
						lock (this)
						{
							transaction2 = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, transaction);
							this._enlistedTransactionOriginal = value;
							value = transaction;
							transaction = null;
						}
					}
					finally
					{
						if (null != transaction2 && transaction2 != this._enlistedTransaction)
						{
							transaction2.Dispose();
						}
						if (null != transaction && transaction != this._enlistedTransaction)
						{
							transaction.Dispose();
						}
					}
					if (null != value)
					{
						SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionInternal.set_EnlistedTransaction|RES|CPOOL> {0}, Transaction {1}, Enlisting.", this.ObjectID, value.GetHashCode());
						this.TransactionOutcomeEnlist(value);
					}
				}
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0006EE08 File Offset: 0x0006D008
		protected bool EnlistedTransactionDisposed
		{
			get
			{
				bool flag2;
				try
				{
					Transaction enlistedTransactionOriginal = this._enlistedTransactionOriginal;
					bool flag = enlistedTransactionOriginal != null && enlistedTransactionOriginal.TransactionInformation == null;
					flag2 = flag;
				}
				catch (ObjectDisposedException)
				{
					flag2 = true;
				}
				return flag2;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0006EE50 File Offset: 0x0006D050
		internal bool IsTxRootWaitingForTxEnd
		{
			get
			{
				return this._isInStasis;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		protected virtual bool UnbindOnTransactionCompletion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0001996E File Offset: 0x00017B6E
		protected internal virtual bool IsNonPoolableTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0001996E File Offset: 0x00017B6E
		internal virtual bool IsTransactionRoot
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0006EE58 File Offset: 0x0006D058
		protected internal bool IsConnectionDoomed
		{
			get
			{
				return this._connectionIsDoomed;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0006EE60 File Offset: 0x0006D060
		internal bool IsEmancipated
		{
			get
			{
				DbConnection dbConnection;
				return !this.IsTxRootWaitingForTxEnd && this._pooledCount < 1 && !this._owningObject.TryGetTarget(out dbConnection);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001B29 RID: 6953 RVA: 0x0006EE90 File Offset: 0x0006D090
		internal bool IsInPool
		{
			get
			{
				return this._pooledCount == 1;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0006EE9B File Offset: 0x0006D09B
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x0006EEA4 File Offset: 0x0006D0A4
		protected internal DbConnection Owner
		{
			get
			{
				DbConnection dbConnection;
				if (this._owningObject.TryGetTarget(out dbConnection))
				{
					return dbConnection;
				}
				return null;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0006EEC3 File Offset: 0x0006D0C3
		internal DbConnectionPool Pool
		{
			get
			{
				return this._connectionPool;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x0006EECB File Offset: 0x0006D0CB
		protected DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._performanceCounters;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		protected virtual bool ReadyToPrepareTransaction
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x0006EED3 File Offset: 0x0006D0D3
		protected internal DbReferenceCollection ReferenceCollection
		{
			get
			{
				return this._referenceCollection;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001B30 RID: 6960
		public abstract string ServerVersion { get; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00025577 File Offset: 0x00023777
		public virtual string ServerVersionNormalized
		{
			get
			{
				throw ADP.NotSupported();
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0006EEDB File Offset: 0x0006D0DB
		public bool ShouldHidePassword
		{
			get
			{
				return this._hidePassword;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001B33 RID: 6963 RVA: 0x0006EEE3 File Offset: 0x0006D0E3
		public ConnectionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0001996E File Offset: 0x00017B6E
		internal virtual bool IsAccessTokenExpired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001B35 RID: 6965
		protected abstract void Activate(Transaction transaction);

		// Token: 0x06001B36 RID: 6966 RVA: 0x0006EEEB File Offset: 0x0006D0EB
		internal void ActivateConnection(Transaction transaction)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.ActivateConnection|RES|INFO|CPOOL> {0}, Activating", this.ObjectID);
			this.Activate(transaction);
			this.PerformanceCounters.NumberOfActiveConnections.Increment();
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0006EF19 File Offset: 0x0006D119
		internal void AddWeakReference(object value, int tag)
		{
			if (this._referenceCollection == null)
			{
				this._referenceCollection = this.CreateReferenceCollection();
				if (this._referenceCollection == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateReferenceCollectionReturnedNull);
				}
			}
			this._referenceCollection.Add(value, tag);
		}

		// Token: 0x06001B38 RID: 6968
		public abstract DbTransaction BeginTransaction(global::System.Data.IsolationLevel il);

		// Token: 0x06001B39 RID: 6969 RVA: 0x0006EF4C File Offset: 0x0006D14C
		public virtual void ChangeDatabase(string value)
		{
			throw ADP.MethodNotImplemented("ChangeDatabase");
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0006EF58 File Offset: 0x0006D158
		internal virtual void CloseConnection(DbConnection owningObject, DbConnectionFactory connectionFactory)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.CloseConnection|RES|CPOOL> {0} Closing.", this.ObjectID);
			if (connectionFactory.SetInnerConnectionFrom(owningObject, DbConnectionOpenBusy.SingletonInstance, this))
			{
				lock (this)
				{
					object obj = this.ObtainAdditionalLocksForClose();
					try
					{
						this.PrepareForCloseConnection();
						DbConnectionPool pool = this.Pool;
						this.DetachCurrentTransactionIfEnded();
						if (pool != null)
						{
							pool.PutObject(this, owningObject);
						}
						else
						{
							this.Deactivate();
							this.PerformanceCounters.HardDisconnectsPerSecond.Increment();
							this._owningObject.SetTarget(null);
							if (this.IsTransactionRoot)
							{
								this.SetInStasis();
							}
							else
							{
								this.PerformanceCounters.NumberOfNonPooledConnections.Decrement();
								if (base.GetType() != typeof(SqlInternalConnectionSmi))
								{
									this.Dispose();
								}
							}
						}
					}
					finally
					{
						this.ReleaseAdditionalLocksForClose(obj);
						connectionFactory.SetInnerConnectionEvent(owningObject, DbConnectionClosedPreviouslyOpened.SingletonInstance);
					}
				}
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void PrepareForReplaceConnection()
		{
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected virtual void PrepareForCloseConnection()
		{
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000021D8 File Offset: 0x000003D8
		protected virtual object ObtainAdditionalLocksForClose()
		{
			return null;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected virtual void ReleaseAdditionalLocksForClose(object lockToken)
		{
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0006F05C File Offset: 0x0006D25C
		protected virtual DbReferenceCollection CreateReferenceCollection()
		{
			throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToConstructReferenceCollectionOnStaticObject);
		}

		// Token: 0x06001B40 RID: 6976
		protected abstract void Deactivate();

		// Token: 0x06001B41 RID: 6977 RVA: 0x0006F068 File Offset: 0x0006D268
		internal void DeactivateConnection()
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.DeactivateConnection|RES|INFO|CPOOL> {0}, Deactivating", this.ObjectID);
			if (this.PerformanceCounters != null)
			{
				this.PerformanceCounters.NumberOfActiveConnections.Decrement();
			}
			if (!this._connectionIsDoomed && this.Pool.UseLoadBalancing && DateTime.UtcNow.Ticks - this._createTime.Ticks > this.Pool.LoadBalanceTimeout.Ticks)
			{
				this.DoNotPoolThisConnection();
			}
			this.Deactivate();
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0006F0F4 File Offset: 0x0006D2F4
		internal virtual void DelegatedTransactionEnded()
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.DelegatedTransactionEnded|RES|CPOOL> {0}, Delegated Transaction Completed.", this.ObjectID);
			if (1 != this._pooledCount)
			{
				DbConnection dbConnection;
				if (-1 == this._pooledCount && !this._owningObject.TryGetTarget(out dbConnection))
				{
					this.TerminateStasis(false);
					this.Deactivate();
					this.PerformanceCounters.NumberOfNonPooledConnections.Decrement();
					this.Dispose();
				}
				return;
			}
			this.TerminateStasis(true);
			this.Deactivate();
			DbConnectionPool pool = this.Pool;
			if (pool == null)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectWithoutPool);
			}
			pool.PutObjectFromTransactedPool(this);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0006F184 File Offset: 0x0006D384
		public virtual void Dispose()
		{
			this._connectionPool = null;
			this._performanceCounters = null;
			this._connectionIsDoomed = true;
			this._enlistedTransactionOriginal = null;
			Transaction transaction = Interlocked.Exchange<Transaction>(ref this._enlistedTransaction, null);
			if (transaction != null)
			{
				transaction.Dispose();
			}
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0006F1C9 File Offset: 0x0006D3C9
		protected internal void DoNotPoolThisConnection()
		{
			this._cannotBePooled = true;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.DoNotPoolThisConnection|RES|INFO|CPOOL> {0}, Marking pooled object as non-poolable so it will be disposed", this.ObjectID);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x0006F1E7 File Offset: 0x0006D3E7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected internal void DoomThisConnection()
		{
			this._connectionIsDoomed = true;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.DoomThisConnection|RES|INFO|CPOOL> {0}, Dooming", this.ObjectID);
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x0006F205 File Offset: 0x0006D405
		protected internal void UnDoomThisConnection()
		{
			this._connectionIsDoomed = false;
		}

		// Token: 0x06001B47 RID: 6983
		public abstract void EnlistTransaction(Transaction transaction);

		// Token: 0x06001B48 RID: 6984 RVA: 0x0006F210 File Offset: 0x0006D410
		protected internal virtual DataTable GetSchema(DbConnectionFactory factory, DbConnectionPoolGroup poolGroup, DbConnection outerConnection, string collectionName, string[] restrictions)
		{
			DbMetaDataFactory metaDataFactory = factory.GetMetaDataFactory(poolGroup, this);
			return metaDataFactory.GetSchema(outerConnection, collectionName, restrictions);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x0006F231 File Offset: 0x0006D431
		internal void MakeNonPooledObject(DbConnection owningObject, DbConnectionPoolCounters performanceCounters)
		{
			this._connectionPool = null;
			this._performanceCounters = performanceCounters;
			this._owningObject.SetTarget(owningObject);
			this._pooledCount = -1;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0006F254 File Offset: 0x0006D454
		internal void MakePooledConnection(DbConnectionPool connectionPool)
		{
			this._createTime = DateTime.UtcNow;
			this._connectionPool = connectionPool;
			this._performanceCounters = connectionPool.PerformanceCounters;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0006F274 File Offset: 0x0006D474
		internal void NotifyWeakReference(int message)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Notify(message);
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0006F292 File Offset: 0x0006D492
		internal virtual void OpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory)
		{
			if (!this.TryOpenConnection(outerConnection, connectionFactory, null, null))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.SynchronousConnectReturnedPending);
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006E037 File Offset: 0x0006C237
		internal virtual bool TryOpenConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.ConnectionAlreadyOpen(this.State);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0006F2A8 File Offset: 0x0006D4A8
		internal virtual bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			throw ADP.MethodNotImplemented("TryReplaceConnection");
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0006F2B4 File Offset: 0x0006D4B4
		protected bool TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			if (connectionFactory.SetInnerConnectionFrom(outerConnection, DbConnectionClosedConnecting.SingletonInstance, this))
			{
				DbConnectionInternal dbConnectionInternal = null;
				try
				{
					connectionFactory.PermissionDemand(outerConnection);
					if (!connectionFactory.TryGetConnection(outerConnection, retry, userOptions, this, out dbConnectionInternal))
					{
						return false;
					}
				}
				catch
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw;
				}
				if (dbConnectionInternal == null)
				{
					connectionFactory.SetInnerConnectionTo(outerConnection, this);
					throw ADP.InternalConnectionError(ADP.ConnectionError.GetConnectionReturnsNull);
				}
				connectionFactory.SetInnerConnectionEvent(outerConnection, dbConnectionInternal);
				return true;
			}
			return true;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0006F328 File Offset: 0x0006D528
		internal void PrePush(object expectedOwner)
		{
			DbConnection dbConnection;
			bool flag = this._owningObject.TryGetTarget(out dbConnection);
			if (expectedOwner == null)
			{
				if (flag)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasOwner);
				}
			}
			else if (flag && dbConnection != expectedOwner)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
			if (this._pooledCount != 0)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PushingObjectSecondTime);
			}
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionInternal.PrePush|RES|CPOOL> {0}, Preparing to push into pool, owning connection {1}, pooledCount={2}", this.ObjectID, 0, this._pooledCount);
			this._pooledCount++;
			this._owningObject.SetTarget(null);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0006F3A4 File Offset: 0x0006D5A4
		internal void PostPop(DbConnection newOwner)
		{
			DbConnection dbConnection;
			if (this._owningObject.TryGetTarget(out dbConnection))
			{
				throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectHasOwner);
			}
			this._owningObject.SetTarget(newOwner);
			this._pooledCount--;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int>("<prov.DbConnectionInternal.PostPop|RES|CPOOL> {0}, Preparing to pop from pool,  owning connection {1}, pooledCount={2}", this.ObjectID, 0, this._pooledCount);
			if (this.Pool != null)
			{
				if (this._pooledCount != 0)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.PooledObjectInPoolMoreThanOnce);
				}
			}
			else if (-1 != this._pooledCount)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.NonPooledObjectUsedMoreThanOnce);
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0006F424 File Offset: 0x0006D624
		internal void RemoveWeakReference(object value)
		{
			DbReferenceCollection referenceCollection = this.ReferenceCollection;
			if (referenceCollection != null)
			{
				referenceCollection.Remove(value);
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected virtual void CleanupTransactionOnCompletion(Transaction transaction)
		{
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0006F444 File Offset: 0x0006D644
		internal void DetachCurrentTransactionIfEnded()
		{
			Transaction enlistedTransaction = this.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				bool flag;
				try
				{
					flag = enlistedTransaction.TransactionInformation.Status > TransactionStatus.Active;
				}
				catch (TransactionException)
				{
					flag = true;
				}
				if (flag)
				{
					this.DetachTransaction(enlistedTransaction, true);
				}
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0006F494 File Offset: 0x0006D694
		internal void DetachTransaction(Transaction transaction, bool isExplicitlyReleasing)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionInternal.DetachTransaction|RES|CPOOL> {0}, Transaction Completed. (pooledCount={1})", this.ObjectID, this._pooledCount);
			lock (transaction)
			{
				DbConnection owner = this.Owner;
				if (isExplicitlyReleasing || this.UnbindOnTransactionCompletion || owner == null)
				{
					Transaction enlistedTransaction = this._enlistedTransaction;
					if (enlistedTransaction != null && transaction.Equals(enlistedTransaction))
					{
						enlistedTransaction.TransactionCompleted -= this._transactionCompletedEventHandler;
						this.EnlistedTransaction = null;
						if (this.IsTxRootWaitingForTxEnd)
						{
							this.DelegatedTransactionEnded();
						}
					}
				}
			}
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0006F534 File Offset: 0x0006D734
		internal void CleanupConnectionOnTransactionCompletion(Transaction transaction)
		{
			this.DetachTransaction(transaction, false);
			DbConnectionPool pool = this.Pool;
			if (pool != null)
			{
				pool.TransactionEnded(transaction, this);
			}
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0006F55C File Offset: 0x0006D75C
		private void TransactionCompletedEvent(object sender, TransactionEventArgs e)
		{
			Transaction transaction = e.Transaction;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int>("<prov.DbConnectionInternal.TransactionCompletedEvent|RES|CPOOL> {0}, Transaction Completed. (pooledCount = {1})", this.ObjectID, this._pooledCount);
			this.CleanupTransactionOnCompletion(transaction);
			this.CleanupConnectionOnTransactionCompletion(transaction);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0006F599 File Offset: 0x0006D799
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private void TransactionOutcomeEnlist(Transaction transaction)
		{
			if (this._transactionCompletedEventHandler == null)
			{
				this._transactionCompletedEventHandler = new TransactionCompletedEventHandler(this.TransactionCompletedEvent);
			}
			transaction.TransactionCompleted += this._transactionCompletedEventHandler;
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x0006F5C1 File Offset: 0x0006D7C1
		internal void SetInStasis()
		{
			this._isInStasis = true;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.SetInStasis|RES|CPOOL> {0}, Non-Pooled Connection has Delegated Transaction, waiting to Dispose.", this.ObjectID);
			this.PerformanceCounters.NumberOfStasisConnections.Increment();
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0006F5F0 File Offset: 0x0006D7F0
		private void TerminateStasis(bool returningToPool)
		{
			if (returningToPool)
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.TerminateStasis|RES|CPOOL> {0}, Delegated Transaction has ended, connection is closed.  Returning to general pool.", this.ObjectID);
			}
			else
			{
				SqlClientEventSource.Log.TryPoolerTraceEvent<int>("<prov.DbConnectionInternal.TerminateStasis|RES|CPOOL> {0}, Delegated Transaction has ended, connection is closed/leaked.  Disposing.", this.ObjectID);
			}
			this.PerformanceCounters.NumberOfStasisConnections.Decrement();
			this._isInStasis = false;
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal virtual bool IsConnectionAlive(bool throwOnException = false)
		{
			return true;
		}

		// Token: 0x04000AFE RID: 2814
		private static int _objectTypeCount;

		// Token: 0x04000AFF RID: 2815
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionInternal._objectTypeCount);

		// Token: 0x04000B00 RID: 2816
		private TransactionCompletedEventHandler _transactionCompletedEventHandler;

		// Token: 0x04000B01 RID: 2817
		internal static readonly StateChangeEventArgs StateChangeClosed = new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed);

		// Token: 0x04000B02 RID: 2818
		internal static readonly StateChangeEventArgs StateChangeOpen = new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open);

		// Token: 0x04000B03 RID: 2819
		private readonly bool _allowSetConnectionString;

		// Token: 0x04000B04 RID: 2820
		private readonly bool _hidePassword;

		// Token: 0x04000B05 RID: 2821
		private readonly ConnectionState _state;

		// Token: 0x04000B06 RID: 2822
		private readonly WeakReference<DbConnection> _owningObject = new WeakReference<DbConnection>(null, false);

		// Token: 0x04000B07 RID: 2823
		private DbConnectionPool _connectionPool;

		// Token: 0x04000B08 RID: 2824
		private DbConnectionPoolCounters _performanceCounters;

		// Token: 0x04000B09 RID: 2825
		private DbReferenceCollection _referenceCollection;

		// Token: 0x04000B0A RID: 2826
		private int _pooledCount;

		// Token: 0x04000B0B RID: 2827
		private bool _connectionIsDoomed;

		// Token: 0x04000B0C RID: 2828
		private bool _cannotBePooled;

		// Token: 0x04000B0D RID: 2829
		private bool _isInStasis;

		// Token: 0x04000B0E RID: 2830
		private DateTime _createTime;

		// Token: 0x04000B0F RID: 2831
		private Transaction _enlistedTransaction;

		// Token: 0x04000B10 RID: 2832
		private Transaction _enlistedTransactionOriginal;
	}
}
