using System;
using System.Data;
using System.Threading;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000087 RID: 135
	internal sealed class SqlInternalTransaction
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00020E8F File Offset: 0x0001F08F
		// (set) Token: 0x06000B49 RID: 2889 RVA: 0x00020E97 File Offset: 0x0001F097
		internal bool RestoreBrokenConnection { get; set; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		internal bool ConnectionHasBeenRestored { get; set; }

		// Token: 0x06000B4C RID: 2892 RVA: 0x00020EB1 File Offset: 0x0001F0B1
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction)
			: this(innerConnection, type, outerTransaction, 0L)
		{
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00020EC0 File Offset: 0x0001F0C0
		internal SqlInternalTransaction(SqlInternalConnection innerConnection, TransactionType type, SqlTransaction outerTransaction, long transactionId)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int, int, int?, int>("SqlInternalTransaction.ctor | RES | CPOOL | Object Id {0}, Created for connection {1}, outer transaction {2}, Type {3}", this.ObjectID, innerConnection.ObjectID, (outerTransaction != null) ? new int?(outerTransaction.ObjectID) : null, (int)type);
			this._innerConnection = innerConnection;
			this._transactionType = type;
			if (outerTransaction != null)
			{
				this._parent = new WeakReference<SqlTransaction>(outerTransaction);
			}
			this._transactionId = transactionId;
			this.RestoreBrokenConnection = false;
			this.ConnectionHasBeenRestored = false;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00020F4B File Offset: 0x0001F14B
		internal bool HasParentTransaction
		{
			get
			{
				return this._transactionType == TransactionType.LocalFromAPI || (this._transactionType == TransactionType.LocalFromTSQL && this._parent != null);
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00020F6C File Offset: 0x0001F16C
		internal bool IsAborted
		{
			get
			{
				return this._transactionState == TransactionState.Aborted;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00020F77 File Offset: 0x0001F177
		internal bool IsActive
		{
			get
			{
				return this._transactionState == TransactionState.Active;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00020F82 File Offset: 0x0001F182
		internal bool IsCommitted
		{
			get
			{
				return this._transactionState == TransactionState.Committed;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00020F8D File Offset: 0x0001F18D
		internal bool IsCompleted
		{
			get
			{
				return this._transactionState == TransactionState.Aborted || this._transactionState == TransactionState.Committed || this._transactionState == TransactionState.Unknown;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x00020FAC File Offset: 0x0001F1AC
		internal bool IsDelegated
		{
			get
			{
				return this._transactionType == TransactionType.Delegated;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00020FB7 File Offset: 0x0001F1B7
		internal bool IsDistributed
		{
			get
			{
				return this._transactionType == TransactionType.Distributed;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x00020FC2 File Offset: 0x0001F1C2
		internal bool IsContext
		{
			get
			{
				return this._transactionType == TransactionType.Context;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00020FCD File Offset: 0x0001F1CD
		internal bool IsLocal
		{
			get
			{
				return this._transactionType == TransactionType.LocalFromTSQL || this._transactionType == TransactionType.LocalFromAPI || this.IsContext;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x00020FEC File Offset: 0x0001F1EC
		internal bool IsOrphaned
		{
			get
			{
				SqlTransaction sqlTransaction;
				return this._parent != null && !this._parent.TryGetTarget(out sqlTransaction);
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06000B58 RID: 2904 RVA: 0x0002101B File Offset: 0x0001F21B
		internal bool IsZombied
		{
			get
			{
				return this._innerConnection == null;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x00021026 File Offset: 0x0001F226
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06000B5A RID: 2906 RVA: 0x0002102E File Offset: 0x0001F22E
		internal int OpenResultsCount
		{
			get
			{
				return this._openResultCount;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x00021038 File Offset: 0x0001F238
		internal SqlTransaction Parent
		{
			get
			{
				SqlTransaction sqlTransaction = null;
				SqlTransaction sqlTransaction2;
				if (this._parent != null && this._parent.TryGetTarget(out sqlTransaction2))
				{
					sqlTransaction = sqlTransaction2;
				}
				return sqlTransaction;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00021061 File Offset: 0x0001F261
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x00021069 File Offset: 0x0001F269
		internal long TransactionId
		{
			get
			{
				return this._transactionId;
			}
			set
			{
				this._transactionId = value;
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00021072 File Offset: 0x0001F272
		internal void Activate()
		{
			this._transactionState = TransactionState.Active;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002107C File Offset: 0x0001F27C
		private void CheckTransactionLevelAndZombie()
		{
			try
			{
				if (!this.IsZombied && this.GetServerTransactionLevel() == 0)
				{
					this.Zombie();
				}
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(ex);
				this.Zombie();
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000210CC File Offset: 0x0001F2CC
		internal void CloseFromConnection()
		{
			SqlInternalConnection innerConnection = this._innerConnection;
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("SqlInternalTransaction.CloseFromConnection | RES | CPOOL | Object Id {0}, Closing transaction", this.ObjectID);
			bool flag = true;
			try
			{
				innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
			}
			catch (Exception ex)
			{
				flag = ADP.IsCatchableExceptionType(ex);
				throw;
			}
			finally
			{
				if (flag)
				{
					this.Zombie();
				}
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00021134 File Offset: 0x0001F334
		internal void Commit()
		{
			using (TryEventScope.Create<int>("SqlInternalTransaction.Commit | API | Object Id {0}", this.ObjectID))
			{
				if (this._innerConnection.IsLockedForBulkCopy)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				this._innerConnection.ValidateConnectionForExecute(null);
				try
				{
					this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Commit, null, IsolationLevel.Unspecified, null, false);
					if (!this.IsZombied && !this._innerConnection.Is2005OrNewer)
					{
						this.Zombie();
					}
					else
					{
						this.ZombieParent();
					}
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this.CheckTransactionLevelAndZombie();
					}
					throw;
				}
			}
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000211DC File Offset: 0x0001F3DC
		internal void Completed(TransactionState transactionState)
		{
			this._transactionState = transactionState;
			this.Zombie();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x000211EC File Offset: 0x0001F3EC
		internal int DecrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Decrement(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00021210 File Offset: 0x0001F410
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002121F File Offset: 0x0001F41F
		private void Dispose(bool disposing)
		{
			SqlClientEventSource.Log.TryPoolerTraceEvent<int>("SqlInternalTransaction.Dispose | RES | CPOOL | Object Id {0}, Disposing", this.ObjectID);
			if (disposing && this._innerConnection != null)
			{
				this._disposing = true;
				this.Rollback();
			}
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00021250 File Offset: 0x0001F450
		private int GetServerTransactionLevel()
		{
			int num;
			using (SqlCommand sqlCommand = new SqlCommand("set @out = @@trancount", (SqlConnection)this._innerConnection.Owner))
			{
				sqlCommand.Transaction = this.Parent;
				SqlParameter sqlParameter = new SqlParameter("@out", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				sqlCommand.Parameters.Add(sqlParameter);
				sqlCommand.RunExecuteReader(CommandBehavior.Default, RunBehavior.UntilDone, false, "GetServerTransactionLevel");
				num = (int)sqlParameter.Value;
			}
			return num;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000212DC File Offset: 0x0001F4DC
		internal int IncrementAndObtainOpenResultCount()
		{
			int num = Interlocked.Increment(ref this._openResultCount);
			if (num < 0)
			{
				throw SQL.OpenResultCountExceeded();
			}
			return num;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00021300 File Offset: 0x0001F500
		internal void InitParent(SqlTransaction transaction)
		{
			this._parent = new WeakReference<SqlTransaction>(transaction);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00021310 File Offset: 0x0001F510
		internal void Rollback()
		{
			using (TryEventScope.Create<int>("SqlInternalTransaction.Rollback | API | Object Id {0}", this.ObjectID))
			{
				if (this._innerConnection.IsLockedForBulkCopy)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				this._innerConnection.ValidateConnectionForExecute(null);
				try
				{
					this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.IfRollback, null, IsolationLevel.Unspecified, null, false);
					this.Zombie();
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					this.CheckTransactionLevelAndZombie();
					if (!this._disposing)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000213A8 File Offset: 0x0001F5A8
		internal void Rollback(string transactionName)
		{
			using (TryEventScope.Create<int, string>("SqlInternalTransaction.Rollback | API | Object Id {0}, Transaction Name {1}", this.ObjectID, transactionName))
			{
				if (this._innerConnection.IsLockedForBulkCopy)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				this._innerConnection.ValidateConnectionForExecute(null);
				if (string.IsNullOrEmpty(transactionName))
				{
					throw SQL.NullEmptyTransactionName();
				}
				try
				{
					this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Rollback, transactionName, IsolationLevel.Unspecified, null, false);
					if (!this.IsZombied && !this._innerConnection.Is2005OrNewer)
					{
						this.CheckTransactionLevelAndZombie();
					}
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this.CheckTransactionLevelAndZombie();
					}
					throw;
				}
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00021458 File Offset: 0x0001F658
		internal void Save(string savePointName)
		{
			using (TryEventScope.Create<int, string>("SqlInternalTransaction.Save | API | Object Id {0}, Save Point Name {1}", this.ObjectID, savePointName))
			{
				this._innerConnection.ValidateConnectionForExecute(null);
				if (string.IsNullOrEmpty(savePointName))
				{
					throw SQL.NullEmptyTransactionName();
				}
				try
				{
					this._innerConnection.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Save, savePointName, IsolationLevel.Unspecified, null, false);
				}
				catch (Exception ex)
				{
					if (ADP.IsCatchableExceptionType(ex))
					{
						this.CheckTransactionLevelAndZombie();
					}
					throw;
				}
			}
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000214DC File Offset: 0x0001F6DC
		internal void Zombie()
		{
			this.ZombieParent();
			SqlInternalConnection innerConnection = this._innerConnection;
			this._innerConnection = null;
			if (innerConnection != null)
			{
				innerConnection.DisconnectTransaction(this);
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00021508 File Offset: 0x0001F708
		private void ZombieParent()
		{
			SqlTransaction sqlTransaction;
			if (this._parent != null && this._parent.TryGetTarget(out sqlTransaction))
			{
				sqlTransaction.Zombie();
			}
			this._parent = null;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002153C File Offset: 0x0001F73C
		internal string TraceString()
		{
			return string.Format(null, "(ObjId={0}, tranId={1}, state={2}, type={3}, open={4}, disp={5}", new object[] { this.ObjectID, this._transactionId, this._transactionState, this._transactionType, this._openResultCount, this._disposing });
		}

		// Token: 0x040002C7 RID: 711
		internal const long NullTransactionId = 0L;

		// Token: 0x040002C8 RID: 712
		private TransactionState _transactionState;

		// Token: 0x040002C9 RID: 713
		private readonly TransactionType _transactionType;

		// Token: 0x040002CA RID: 714
		private long _transactionId;

		// Token: 0x040002CB RID: 715
		private int _openResultCount;

		// Token: 0x040002CC RID: 716
		private SqlInternalConnection _innerConnection;

		// Token: 0x040002CD RID: 717
		private bool _disposing;

		// Token: 0x040002CE RID: 718
		private WeakReference<SqlTransaction> _parent;

		// Token: 0x040002CF RID: 719
		private static int s_objectTypeCount;

		// Token: 0x040002D0 RID: 720
		internal readonly int _objectID = Interlocked.Increment(ref SqlInternalTransaction.s_objectTypeCount);
	}
}
