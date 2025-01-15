using System;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000084 RID: 132
	internal abstract class SqlInternalConnection : DbConnectionInternal
	{
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000206B0 File Offset: 0x0001E8B0
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x000206B8 File Offset: 0x0001E8B8
		internal string CurrentDatabase { get; set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000206C1 File Offset: 0x0001E8C1
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000206C9 File Offset: 0x0001E8C9
		internal string CurrentDataSource { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000206D2 File Offset: 0x0001E8D2
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000206DA File Offset: 0x0001E8DA
		internal SqlDelegatedTransaction DelegatedTransaction { get; set; }

		// Token: 0x06000B19 RID: 2841 RVA: 0x000206E3 File Offset: 0x0001E8E3
		internal SqlInternalConnection(SqlConnectionString connectionOptions)
		{
			this._connectionOptions = connectionOptions;
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x000206F2 File Offset: 0x0001E8F2
		internal SqlConnection Connection
		{
			get
			{
				return (SqlConnection)base.Owner;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x000206FF File Offset: 0x0001E8FF
		internal SqlConnectionString ConnectionOptions
		{
			get
			{
				return this._connectionOptions;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06000B1C RID: 2844
		internal abstract SqlInternalTransaction CurrentTransaction { get; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00020707 File Offset: 0x0001E907
		internal virtual SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06000B1E RID: 2846
		internal abstract SqlInternalTransaction PendingTransaction { get; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0002070F File Offset: 0x0001E90F
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00020718 File Offset: 0x0001E918
		internal override bool IsTransactionRoot
		{
			get
			{
				SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
				return delegatedTransaction != null && delegatedTransaction.IsActive;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00020738 File Offset: 0x0001E938
		internal bool HasLocalTransaction
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.IsLocal;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0002075C File Offset: 0x0001E95C
		internal bool HasLocalTransactionFromAPI
		{
			get
			{
				SqlInternalTransaction currentTransaction = this.CurrentTransaction;
				return currentTransaction != null && currentTransaction.HasParentTransaction;
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0002077E File Offset: 0x0001E97E
		internal bool IsEnlistedInTransaction
		{
			get
			{
				return this._isEnlistedInTransaction;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06000B24 RID: 2852
		internal abstract bool IsLockedForBulkCopy { get; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06000B25 RID: 2853
		internal abstract bool Is2008OrNewer { get; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00020786 File Offset: 0x0001E986
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0002078E File Offset: 0x0001E98E
		internal byte[] PromotedDTCToken
		{
			get
			{
				return this._promotedDTCToken;
			}
			set
			{
				this._promotedDTCToken = value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00020797 File Offset: 0x0001E997
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0002079F File Offset: 0x0001E99F
		internal bool IsGlobalTransaction
		{
			get
			{
				return this._isGlobalTransaction;
			}
			set
			{
				this._isGlobalTransaction = value;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x000207A8 File Offset: 0x0001E9A8
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x000207B0 File Offset: 0x0001E9B0
		internal bool IsGlobalTransactionsEnabledForServer
		{
			get
			{
				return this._isGlobalTransactionEnabledForServer;
			}
			set
			{
				this._isGlobalTransactionEnabledForServer = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06000B2C RID: 2860
		internal abstract bool Is2000 { get; }

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06000B2D RID: 2861
		internal abstract bool Is2005OrNewer { get; }

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000207B9 File Offset: 0x0001E9B9
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x000207C1 File Offset: 0x0001E9C1
		internal bool IsAzureSQLConnection
		{
			get
			{
				return this._isAzureSQLConnection;
			}
			set
			{
				this._isAzureSQLConnection = value;
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000207CA File Offset: 0x0001E9CA
		public override DbTransaction BeginTransaction(global::System.Data.IsolationLevel iso)
		{
			return this.BeginSqlTransaction(iso, null, false);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000207D8 File Offset: 0x0001E9D8
		internal virtual SqlTransaction BeginSqlTransaction(global::System.Data.IsolationLevel iso, string transactionName, bool shouldReconnect)
		{
			SqlStatistics sqlStatistics = null;
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			SqlTransaction sqlTransaction2;
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this.Connection);
				sqlStatistics = SqlStatistics.StartTimer(this.Connection.Statistics);
				SqlConnection.ExecutePermission.Demand();
				this.ValidateConnectionForExecute(null);
				if (this.HasLocalTransactionFromAPI)
				{
					throw ADP.ParallelTransactionsNotSupported(this.Connection);
				}
				if (iso == global::System.Data.IsolationLevel.Unspecified)
				{
					iso = global::System.Data.IsolationLevel.ReadCommitted;
				}
				SqlTransaction sqlTransaction = new SqlTransaction(this, this.Connection, iso, this.AvailableInternalTransaction);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = shouldReconnect;
				this.ExecuteTransaction(SqlInternalConnection.TransactionRequest.Begin, transactionName, iso, sqlTransaction.InternalTransaction, false);
				sqlTransaction.InternalTransaction.RestoreBrokenConnection = false;
				sqlTransaction2 = sqlTransaction;
			}
			catch (OutOfMemoryException ex)
			{
				this.Connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this.Connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this.Connection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			finally
			{
				SqlStatistics.StopTimer(sqlStatistics);
			}
			return sqlTransaction2;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000208F0 File Offset: 0x0001EAF0
		public override void ChangeDatabase(string database)
		{
			if (string.IsNullOrEmpty(database))
			{
				throw ADP.EmptyDatabaseName();
			}
			this.ValidateConnectionForExecute(null);
			this.ChangeDatabaseInternal(database);
		}

		// Token: 0x06000B33 RID: 2867
		protected abstract void ChangeDatabaseInternal(string database);

		// Token: 0x06000B34 RID: 2868 RVA: 0x00020910 File Offset: 0x0001EB10
		protected override void CleanupTransactionOnCompletion(Transaction transaction)
		{
			SqlDelegatedTransaction delegatedTransaction = this.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.TransactionEnded(transaction);
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002092E File Offset: 0x0001EB2E
		protected override DbReferenceCollection CreateReferenceCollection()
		{
			return new SqlReferenceCollection();
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00020938 File Offset: 0x0001EB38
		protected override void Deactivate()
		{
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				SqlClientEventSource log = SqlClientEventSource.Log;
				string text = "SqlInternalConnection.Deactivate | ADV | Object Id {0} deactivating, Client Connection Id {1}";
				int objectID = base.ObjectID;
				SqlConnection connection = this.Connection;
				log.TryAdvancedTraceEvent<int, Guid?>(text, objectID, (connection != null) ? new Guid?(connection.ClientConnectionId) : null);
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this.Connection);
				SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
				if (sqlReferenceCollection != null)
				{
					sqlReferenceCollection.Deactivate();
				}
				this.InternalDeactivate();
			}
			catch (OutOfMemoryException)
			{
				base.DoomThisConnection();
				throw;
			}
			catch (StackOverflowException)
			{
				base.DoomThisConnection();
				throw;
			}
			catch (ThreadAbortException)
			{
				base.DoomThisConnection();
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				base.DoomThisConnection();
				ADP.TraceExceptionWithoutRethrow(ex);
			}
		}

		// Token: 0x06000B37 RID: 2871
		internal abstract void DisconnectTransaction(SqlInternalTransaction internalTransaction);

		// Token: 0x06000B38 RID: 2872 RVA: 0x00020A18 File Offset: 0x0001EC18
		public override void Dispose()
		{
			this._whereAbouts = null;
			base.Dispose();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00020A28 File Offset: 0x0001EC28
		protected void Enlist(Transaction tx)
		{
			if (null == tx)
			{
				if (this.IsEnlistedInTransaction)
				{
					this.EnlistNull();
					return;
				}
				Transaction enlistedTransaction = base.EnlistedTransaction;
				if (enlistedTransaction != null && enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					this.EnlistNull();
					return;
				}
			}
			else if (!tx.Equals(base.EnlistedTransaction))
			{
				this.EnlistNonNull(tx);
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00020A88 File Offset: 0x0001EC88
		private void EnlistNonNull(Transaction tx)
		{
			SqlClientEventSource log = SqlClientEventSource.Log;
			string text = "SqlInternalConnection.EnlistNonNull | ADV | Object {0}, Transaction Id {1}, attempting to delegate.";
			int objectID = base.ObjectID;
			string text2;
			if (tx == null)
			{
				text2 = null;
			}
			else
			{
				TransactionInformation transactionInformation = tx.TransactionInformation;
				text2 = ((transactionInformation != null) ? transactionInformation.LocalIdentifier : null);
			}
			log.TryAdvancedTraceEvent<int, string>(text, objectID, text2);
			bool flag = false;
			SqlDelegatedTransaction sqlDelegatedTransaction = new SqlDelegatedTransaction(this, tx);
			try
			{
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.EnlistPromotableSinglePhase == null)
					{
						flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
					}
					else
					{
						flag = (bool)SysTxForGlobalTransactions.EnlistPromotableSinglePhase.Invoke(tx, new object[]
						{
							sqlDelegatedTransaction,
							SqlInternalConnection.s_globalTransactionTMID
						});
					}
				}
				else
				{
					flag = tx.EnlistPromotableSinglePhase(sqlDelegatedTransaction);
				}
				if (flag)
				{
					this.DelegatedTransaction = sqlDelegatedTransaction;
					SqlClientEventSource log2 = SqlClientEventSource.Log;
					string text3 = "SqlInternalConnection.EnlistNonNull | ADV | Object Id {0}, Client Connection Id {1} delegated to transaction {1} with transactionId {2}";
					int objectID2 = base.ObjectID;
					SqlConnection connection = this.Connection;
					Guid? guid = ((connection != null) ? new Guid?(connection.ClientConnectionId) : null);
					int? num = ((sqlDelegatedTransaction != null) ? new int?(sqlDelegatedTransaction.ObjectID) : null);
					string text4;
					if (sqlDelegatedTransaction == null)
					{
						text4 = null;
					}
					else
					{
						Transaction transaction = sqlDelegatedTransaction.Transaction;
						if (transaction == null)
						{
							text4 = null;
						}
						else
						{
							TransactionInformation transactionInformation2 = transaction.TransactionInformation;
							text4 = ((transactionInformation2 != null) ? transactionInformation2.LocalIdentifier : null);
						}
					}
					log2.TryAdvancedTraceEvent<int, Guid?, int?, string>(text3, objectID2, guid, num, text4);
				}
			}
			catch (SqlException ex)
			{
				if (ex.Class >= 20)
				{
					throw;
				}
				SqlInternalConnectionTds sqlInternalConnectionTds = this as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					TdsParser parser = sqlInternalConnectionTds.Parser;
					if (parser == null || parser.State != TdsParserState.OpenLoggedIn)
					{
						throw;
					}
				}
				ADP.TraceExceptionWithoutRethrow(ex);
			}
			if (!flag)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("SqlInternalConnection.EnlistNonNull | ADV | Object Id {0}, delegation not possible, enlisting.", base.ObjectID);
				byte[] array;
				if (this._isGlobalTransaction)
				{
					if (SysTxForGlobalTransactions.GetPromotedToken == null)
					{
						throw SQL.UnsupportedSysTxForGlobalTransactions();
					}
					array = (byte[])SysTxForGlobalTransactions.GetPromotedToken.Invoke(tx, null);
				}
				else
				{
					if (this._whereAbouts == null)
					{
						byte[] dtcaddress = this.GetDTCAddress();
						byte[] array2 = dtcaddress;
						if (array2 == null)
						{
							throw SQL.CannotGetDTCAddress();
						}
						this._whereAbouts = array2;
					}
					array = SqlInternalConnection.GetTransactionCookie(tx, this._whereAbouts);
				}
				this.PropagateTransactionCookie(array);
				this._isEnlistedInTransaction = true;
				SqlClientEventSource log3 = SqlClientEventSource.Log;
				string text5 = "SqlInternalConnection.EnlistNonNull | ADV | Object Id {0}, Client Connection Id {1}, Enlisted in transaction with transactionId {2}";
				int objectID3 = base.ObjectID;
				SqlConnection connection2 = this.Connection;
				Guid? guid2 = ((connection2 != null) ? new Guid?(connection2.ClientConnectionId) : null);
				string text6;
				if (tx == null)
				{
					text6 = null;
				}
				else
				{
					TransactionInformation transactionInformation3 = tx.TransactionInformation;
					text6 = ((transactionInformation3 != null) ? transactionInformation3.LocalIdentifier : null);
				}
				log3.TryAdvancedTraceEvent<int, Guid?, string>(text5, objectID3, guid2, text6);
			}
			base.EnlistedTransaction = tx;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00020CC8 File Offset: 0x0001EEC8
		internal void EnlistNull()
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("SqlInternalConnection.EnlistNull | ADV | Object Id {0}, unenlisting.", base.ObjectID);
			this.PropagateTransactionCookie(null);
			this._isEnlistedInTransaction = false;
			base.EnlistedTransaction = null;
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("SqlInternalConnection.EnlistNull | ADV | Object Id {0}, unenlisted.", base.ObjectID);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00020D14 File Offset: 0x0001EF14
		public override void EnlistTransaction(Transaction transaction)
		{
			this.ValidateConnectionForExecute(null);
			if (this.HasLocalTransaction)
			{
				throw ADP.LocalTransactionPresent();
			}
			if (null != transaction && transaction.Equals(base.EnlistedTransaction))
			{
				return;
			}
			TdsParser tdsParser = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				tdsParser = SqlInternalConnection.GetBestEffortCleanupTarget(this.Connection);
				this.Enlist(transaction);
			}
			catch (OutOfMemoryException ex)
			{
				this.Connection.Abort(ex);
				throw;
			}
			catch (StackOverflowException ex2)
			{
				this.Connection.Abort(ex2);
				throw;
			}
			catch (ThreadAbortException ex3)
			{
				this.Connection.Abort(ex3);
				SqlInternalConnection.BestEffortCleanup(tdsParser);
				throw;
			}
		}

		// Token: 0x06000B3D RID: 2877
		internal abstract void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, global::System.Data.IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest);

		// Token: 0x06000B3E RID: 2878 RVA: 0x00020DC8 File Offset: 0x0001EFC8
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			SqlDataReader sqlDataReader = null;
			SqlReferenceCollection sqlReferenceCollection = (SqlReferenceCollection)base.ReferenceCollection;
			if (sqlReferenceCollection != null)
			{
				sqlDataReader = sqlReferenceCollection.FindLiveReader(command);
			}
			return sqlDataReader;
		}

		// Token: 0x06000B3F RID: 2879
		protected abstract byte[] GetDTCAddress();

		// Token: 0x06000B40 RID: 2880 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		private static byte[] GetTransactionCookie(Transaction transaction, byte[] whereAbouts)
		{
			byte[] array = null;
			if (null != transaction)
			{
				array = TransactionInterop.GetExportCookie(transaction, whereAbouts);
			}
			return array;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected virtual void InternalDeactivate()
		{
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00020E14 File Offset: 0x0001F014
		internal void OnError(SqlException exception, bool breakConnection, Action<Action> wrapCloseInAction = null)
		{
			if (breakConnection)
			{
				base.DoomThisConnection();
			}
			SqlConnection connection = this.Connection;
			if (connection != null)
			{
				connection.OnError(exception, breakConnection, wrapCloseInAction);
				return;
			}
			if (exception.Class >= 11)
			{
				throw exception;
			}
		}

		// Token: 0x06000B43 RID: 2883
		protected abstract void PropagateTransactionCookie(byte[] transactionCookie);

		// Token: 0x06000B44 RID: 2884
		internal abstract void ValidateConnectionForExecute(SqlCommand command);

		// Token: 0x06000B45 RID: 2885 RVA: 0x00020E4C File Offset: 0x0001F04C
		internal static TdsParser GetBestEffortCleanupTarget(SqlConnection connection)
		{
			if (connection != null)
			{
				SqlInternalConnectionTds sqlInternalConnectionTds = connection.InnerConnection as SqlInternalConnectionTds;
				if (sqlInternalConnectionTds != null)
				{
					return sqlInternalConnectionTds.Parser;
				}
			}
			return null;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00020E73 File Offset: 0x0001F073
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void BestEffortCleanup(TdsParser target)
		{
			if (target != null)
			{
				target.BestEffortCleanup();
			}
		}

		// Token: 0x040002B0 RID: 688
		private readonly SqlConnectionString _connectionOptions;

		// Token: 0x040002B1 RID: 689
		private bool _isEnlistedInTransaction;

		// Token: 0x040002B2 RID: 690
		private byte[] _promotedDTCToken;

		// Token: 0x040002B3 RID: 691
		private byte[] _whereAbouts;

		// Token: 0x040002B4 RID: 692
		private bool _isGlobalTransaction;

		// Token: 0x040002B5 RID: 693
		private bool _isGlobalTransactionEnabledForServer;

		// Token: 0x040002B6 RID: 694
		private static readonly Guid s_globalTransactionTMID = new Guid("1c742caf-6680-40ea-9c26-6b6846079764");

		// Token: 0x040002BA RID: 698
		private bool _isAzureSQLConnection;

		// Token: 0x020001D1 RID: 465
		internal enum TransactionRequest
		{
			// Token: 0x0400141D RID: 5149
			Begin,
			// Token: 0x0400141E RID: 5150
			Promote,
			// Token: 0x0400141F RID: 5151
			Commit,
			// Token: 0x04001420 RID: 5152
			Rollback,
			// Token: 0x04001421 RID: 5153
			IfRollback,
			// Token: 0x04001422 RID: 5154
			Save
		}
	}
}
