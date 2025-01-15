using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Transactions;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000EC RID: 236
	internal sealed class SqlDelegatedTransaction : IPromotableSinglePhaseNotification, ITransactionPromoter
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00048414 File Offset: 0x00046614
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0004841C File Offset: 0x0004661C
		internal SqlDelegatedTransaction(SqlInternalConnection connection, Transaction tx)
		{
			this._connection = connection;
			this._atomicTransaction = tx;
			this._active = false;
			global::System.Transactions.IsolationLevel isolationLevel = tx.IsolationLevel;
			switch (isolationLevel)
			{
			case global::System.Transactions.IsolationLevel.Serializable:
				this._isolationLevel = global::System.Data.IsolationLevel.Serializable;
				return;
			case global::System.Transactions.IsolationLevel.RepeatableRead:
				this._isolationLevel = global::System.Data.IsolationLevel.RepeatableRead;
				return;
			case global::System.Transactions.IsolationLevel.ReadCommitted:
				this._isolationLevel = global::System.Data.IsolationLevel.ReadCommitted;
				return;
			case global::System.Transactions.IsolationLevel.ReadUncommitted:
				this._isolationLevel = global::System.Data.IsolationLevel.ReadUncommitted;
				return;
			case global::System.Transactions.IsolationLevel.Snapshot:
				this._isolationLevel = global::System.Data.IsolationLevel.Snapshot;
				return;
			default:
				throw SQL.UnknownSysTxIsolationLevel(isolationLevel);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000484B9 File Offset: 0x000466B9
		internal Transaction Transaction
		{
			get
			{
				return this._atomicTransaction;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000484C4 File Offset: 0x000466C4
		public void Initialize()
		{
			SqlInternalConnection connection = this._connection;
			SqlConnection connection2 = connection.Connection;
			SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.Initialize|RES|CPOOL> {0}, Connection {1}, delegating transaction.", this.ObjectID, connection.ObjectID);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (connection.IsEnlistedInTransaction)
				{
					SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.Initialize|RES|CPOOL> {0}, Connection {1}, was enlisted, now defecting.", this.ObjectID, connection.ObjectID);
					connection.EnlistNull();
				}
				this._internalTransaction = new SqlInternalTransaction(connection, TransactionType.Delegated, null);
				connection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, null, this._isolationLevel, this._internalTransaction, true);
				if (connection.CurrentTransaction == null)
				{
					connection.DoomThisConnection();
					throw ADP.InternalError(ADP.InternalErrorCode.UnknownTransactionFailure);
				}
				this._active = true;
			}
			catch (OutOfMemoryException ex)
			{
				connection2.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				connection2.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				connection2.Abort(ex3);
				throw;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000485B4 File Offset: 0x000467B4
		internal bool IsActive
		{
			get
			{
				return this._active;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000485BC File Offset: 0x000467BC
		public byte[] Promote()
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			byte[] array = null;
			if (validConnection != null)
			{
				SqlConnection connection = validConnection.Connection;
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.Promote|RES|CPOOL> {0}, Connection {1}, promoting transaction.", this.ObjectID, validConnection.ObjectID);
				RuntimeHelpers.PrepareConstrainedRegions();
				Exception ex;
				try
				{
					SqlInternalConnection sqlInternalConnection = validConnection;
					lock (sqlInternalConnection)
					{
						try
						{
							this.ValidateActiveOnConnection(validConnection);
							validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Promote, null, global::System.Data.IsolationLevel.Unspecified, this._internalTransaction, true);
							array = validConnection.PromotedDTCToken;
							if (validConnection.IsGlobalTransaction)
							{
								if (SysTxForGlobalTransactions.SetDistributedTransactionIdentifier == null)
								{
									throw SQL.UnsupportedSysTxForGlobalTransactions();
								}
								if (!validConnection.IsGlobalTransactionsEnabledForServer)
								{
									throw SQL.GlobalTransactionsNotEnabled();
								}
								SysTxForGlobalTransactions.SetDistributedTransactionIdentifier.Invoke(this._atomicTransaction, new object[]
								{
									this,
									this.GetGlobalTxnIdentifierFromToken()
								});
							}
							ex = null;
						}
						catch (SqlException ex2)
						{
							ex = ex2;
							ADP.TraceExceptionWithoutRethrow(ex2);
							validConnection.DoomThisConnection();
						}
						catch (InvalidOperationException ex3)
						{
							ex = ex3;
							ADP.TraceExceptionWithoutRethrow(ex3);
							validConnection.DoomThisConnection();
						}
					}
				}
				catch (OutOfMemoryException ex4)
				{
					connection.Abort(ex4);
					throw;
				}
				catch (StackOverflowException ex5)
				{
					connection.Abort(ex5);
					throw;
				}
				catch (ThreadAbortException ex6)
				{
					connection.Abort(ex6);
					throw;
				}
				if (ex != null)
				{
					try
					{
						Transaction transaction = this.Transaction;
						bool flag2;
						if (transaction == null)
						{
							flag2 = false;
						}
						else
						{
							TransactionInformation transactionInformation = transaction.TransactionInformation;
							TransactionStatus? transactionStatus = ((transactionInformation != null) ? new TransactionStatus?(transactionInformation.Status) : null);
							TransactionStatus transactionStatus2 = TransactionStatus.Aborted;
							flag2 = (transactionStatus.GetValueOrDefault() == transactionStatus2) & (transactionStatus != null);
						}
						if (flag2)
						{
							throw SQL.PromotionFailed(ex);
						}
						return array;
					}
					catch (TransactionException ex7)
					{
						SqlClientEventSource.Log.TryTraceEvent<int, Guid?, string>("SqlDelegatedTransaction.Promote | RES | CPOOL | Object Id {0}, Client Connection Id {1}, Transaction exception occurred: {2}.", this.ObjectID, (connection != null) ? new Guid?(connection.ClientConnectionId) : null, ex7.Message);
						throw SQL.PromotionFailed(ex);
					}
				}
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.Promote|RES|CPOOL> {0}, Connection {1}, aborted during promote.", this.ObjectID, validConnection.ObjectID);
			}
			else
			{
				SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlDelegatedTransaction.Promote|RES|CPOOL> {0}, Connection null, aborted before promoting.", this.ObjectID);
			}
			return array;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00048800 File Offset: 0x00046A00
		public void Rollback(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			if (validConnection != null)
			{
				SqlConnection connection = validConnection.Connection;
				RuntimeHelpers.PrepareConstrainedRegions();
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.Rollback|RES|CPOOL> {0}, Connection {1}, rolling back transaction.", this.ObjectID, validConnection.ObjectID);
				try
				{
					SqlInternalConnection sqlInternalConnection = validConnection;
					lock (sqlInternalConnection)
					{
						try
						{
							this.ValidateActiveOnConnection(validConnection);
							this._active = false;
							this._connection = null;
							if (!this._internalTransaction.IsAborted)
							{
								validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, null, global::System.Data.IsolationLevel.Unspecified, this._internalTransaction, true);
							}
						}
						catch (SqlException ex)
						{
							ADP.TraceExceptionWithoutRethrow(ex);
							validConnection.DoomThisConnection();
						}
						catch (InvalidOperationException ex2)
						{
							ADP.TraceExceptionWithoutRethrow(ex2);
							validConnection.DoomThisConnection();
						}
					}
					validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
					enlistment.Aborted();
					return;
				}
				catch (OutOfMemoryException ex3)
				{
					connection.Abort(ex3);
					throw;
				}
				catch (StackOverflowException ex4)
				{
					connection.Abort(ex4);
					throw;
				}
				catch (ThreadAbortException ex5)
				{
					connection.Abort(ex5);
					throw;
				}
			}
			enlistment.Aborted();
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlDelegatedTransaction.Rollback|RES|CPOOL> {0}, Connection null, aborted before rollback.", this.ObjectID);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00048950 File Offset: 0x00046B50
		public void SinglePhaseCommit(SinglePhaseEnlistment enlistment)
		{
			SqlInternalConnection validConnection = this.GetValidConnection();
			if (validConnection != null)
			{
				SqlConnection connection = validConnection.Connection;
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.SinglePhaseCommit|RES|CPOOL> {0}, Connection {1}, committing transaction.", this.ObjectID, validConnection.ObjectID);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (validConnection.IsConnectionDoomed)
					{
						SqlInternalConnection sqlInternalConnection = validConnection;
						lock (sqlInternalConnection)
						{
							this._active = false;
							this._connection = null;
						}
						enlistment.Aborted(SQL.ConnectionDoomed());
					}
					else
					{
						SqlInternalConnection sqlInternalConnection2 = validConnection;
						Exception ex;
						lock (sqlInternalConnection2)
						{
							try
							{
								this.ValidateActiveOnConnection(validConnection);
								this._active = false;
								this._connection = null;
								validConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, global::System.Data.IsolationLevel.Unspecified, this._internalTransaction, true);
								ex = null;
							}
							catch (SqlException ex2)
							{
								ex = ex2;
								ADP.TraceExceptionWithoutRethrow(ex2);
								validConnection.DoomThisConnection();
							}
							catch (InvalidOperationException ex3)
							{
								ex = ex3;
								ADP.TraceExceptionWithoutRethrow(ex3);
								validConnection.DoomThisConnection();
							}
						}
						if (ex != null)
						{
							if (this._internalTransaction.IsCommitted)
							{
								enlistment.Committed();
							}
							else if (this._internalTransaction.IsAborted)
							{
								enlistment.Aborted(ex);
							}
							else
							{
								enlistment.InDoubt(ex);
							}
						}
						validConnection.CleanupConnectionOnTransactionCompletion(this._atomicTransaction);
						if (ex == null)
						{
							enlistment.Committed();
						}
					}
					return;
				}
				catch (OutOfMemoryException ex4)
				{
					connection.Abort(ex4);
					throw;
				}
				catch (StackOverflowException ex5)
				{
					connection.Abort(ex5);
					throw;
				}
				catch (ThreadAbortException ex6)
				{
					connection.Abort(ex6);
					throw;
				}
			}
			enlistment.Aborted();
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlDelegatedTransaction.SinglePhaseCommit|RES|CPOOL> {0}, Connection null, aborted before commit.", this.ObjectID);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00048B28 File Offset: 0x00046D28
		internal void TransactionEnded(Transaction transaction)
		{
			SqlInternalConnection connection = this._connection;
			if (connection != null)
			{
				SqlClientEventSource.Log.TryTraceEvent<int, int>("<sc.SqlDelegatedTransaction.TransactionEnded|RES|CPOOL> {0}, Connection {1}, transaction completed externally.", this.ObjectID, connection.ObjectID);
				SqlInternalConnection sqlInternalConnection = connection;
				lock (sqlInternalConnection)
				{
					if (this._atomicTransaction.Equals(transaction))
					{
						this._active = false;
						this._connection = null;
					}
					connection.DoomThisConnection();
				}
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00048BA4 File Offset: 0x00046DA4
		private SqlInternalConnection GetValidConnection()
		{
			SqlInternalConnection connection = this._connection;
			if (connection == null && this._atomicTransaction.TransactionInformation.Status != TransactionStatus.Aborted)
			{
				throw ADP.ObjectDisposed(this);
			}
			return connection;
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00048BD8 File Offset: 0x00046DD8
		private void ValidateActiveOnConnection(SqlInternalConnection connection)
		{
			if (!this._active || connection != this._connection || connection.DelegatedTransaction != this)
			{
				if (connection != null)
				{
					connection.DoomThisConnection();
				}
				if (connection != this._connection && this._connection != null)
				{
					this._connection.DoomThisConnection();
				}
				throw ADP.InternalError(ADP.InternalErrorCode.UnpooledObjectHasWrongOwner);
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00048C34 File Offset: 0x00046E34
		private Guid GetGlobalTxnIdentifierFromToken()
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(this._connection.PromotedDTCToken, 4, array, 0, array.Length);
			return new Guid(array);
		}

		// Token: 0x04000777 RID: 1911
		private static int _objectTypeCount;

		// Token: 0x04000778 RID: 1912
		private readonly int _objectID = Interlocked.Increment(ref SqlDelegatedTransaction._objectTypeCount);

		// Token: 0x04000779 RID: 1913
		private const int _globalTransactionsTokenVersionSizeInBytes = 4;

		// Token: 0x0400077A RID: 1914
		private SqlInternalConnection _connection;

		// Token: 0x0400077B RID: 1915
		private global::System.Data.IsolationLevel _isolationLevel;

		// Token: 0x0400077C RID: 1916
		private SqlInternalTransaction _internalTransaction;

		// Token: 0x0400077D RID: 1917
		private Transaction _atomicTransaction;

		// Token: 0x0400077E RID: 1918
		private bool _active;
	}
}
