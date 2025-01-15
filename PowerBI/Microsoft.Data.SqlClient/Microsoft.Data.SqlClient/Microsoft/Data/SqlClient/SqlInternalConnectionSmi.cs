using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.SqlClient.Server;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000ED RID: 237
	internal sealed class SqlInternalConnectionSmi : SqlInternalConnection
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00048C68 File Offset: 0x00046E68
		internal SqlInternalConnectionSmi(SqlConnectionString connectionOptions, SmiContext smiContext)
			: base(connectionOptions)
		{
			this._smiContext = smiContext;
			this._smiContext.OutOfScope += this.OnOutOfScope;
			this._smiConnection = this._smiContext.ContextConnection;
			this._smiEventSink = new SqlInternalConnectionSmi.EventSink(this);
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionSmi.ctor|ADV> {0}, constructed new SMI internal connection", base.ObjectID);
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00048CCC File Offset: 0x00046ECC
		internal SmiContext InternalContext
		{
			get
			{
				return this._smiContext;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00048CD4 File Offset: 0x00046ED4
		internal SmiConnection SmiConnection
		{
			get
			{
				return this._smiConnection;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00048CDC File Offset: 0x00046EDC
		internal SmiEventSink CurrentEventSink
		{
			get
			{
				return this._smiEventSink;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00048CE4 File Offset: 0x00046EE4
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._currentTransaction;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0001996E File Offset: 0x00017B6E
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x0001996E File Offset: 0x00017B6E
		internal override bool Is2000
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		internal override bool Is2005OrNewer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00048CEC File Offset: 0x00046EEC
		internal override bool Is2008OrNewer
		{
			get
			{
				return SmiContextFactory.Instance.NegotiatedSmiVersion >= 210UL;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00020707 File Offset: 0x0001E907
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this.CurrentTransaction;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00048D03 File Offset: 0x00046F03
		public override string ServerVersion
		{
			get
			{
				return SmiContextFactory.Instance.ServerVersion;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00048D0F File Offset: 0x00046F0F
		protected override bool UnbindOnTransactionCompletion
		{
			get
			{
				return base.ConnectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ImplicitUnbind;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00048D1F File Offset: 0x00046F1F
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x00048D27 File Offset: 0x00046F27
		private Transaction ContextTransaction { get; set; }

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00048D30 File Offset: 0x00046F30
		private Transaction InternalEnlistedTransaction
		{
			get
			{
				Transaction transaction = base.EnlistedTransaction;
				if (null == transaction)
				{
					transaction = this.ContextTransaction;
				}
				return transaction;
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected override void Activate(Transaction transaction)
		{
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00048D58 File Offset: 0x00046F58
		internal void Activate()
		{
			int num = Interlocked.Exchange(ref this._isInUse, 1);
			if (num != 0)
			{
				throw SQL.ContextConnectionIsInUse();
			}
			base.CurrentDatabase = this._smiConnection.GetCurrentDatabase(this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00048DA0 File Offset: 0x00046FA0
		internal void AutomaticEnlistment()
		{
			Transaction currentTransaction = ADP.GetCurrentTransaction();
			Transaction contextTransaction = this._smiContext.ContextTransaction;
			long contextTransactionId = this._smiContext.ContextTransactionId;
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long, int, int>("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> {0}, contextTransactionId=0x{1}, contextTransaction={2}, currentSystemTransaction={3}.", base.ObjectID, contextTransactionId, (null != contextTransaction) ? contextTransaction.GetHashCode() : 0, (null != currentTransaction) ? currentTransaction.GetHashCode() : 0);
			if (contextTransactionId != 0L)
			{
				if (null != currentTransaction && contextTransaction != currentTransaction)
				{
					throw SQL.NestedTransactionScopesNotSupported();
				}
				SqlClientEventSource.Log.TryTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> {0}, using context transaction with transactionId=0x{1}", base.ObjectID, contextTransactionId);
				this._currentTransaction = new SqlInternalTransaction(this, TransactionType.Context, null, contextTransactionId);
				this.ContextTransaction = contextTransaction;
				return;
			}
			else
			{
				if (null == currentTransaction)
				{
					this._currentTransaction = null;
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> {0}, no transaction.", base.ObjectID);
					return;
				}
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionSmi.AutomaticEnlistment|ADV> {0}, using current System.Transaction.", base.ObjectID);
				base.Enlist(currentTransaction);
				return;
			}
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00048E8F File Offset: 0x0004708F
		protected override void ChangeDatabaseInternal(string database)
		{
			this._smiConnection.SetCurrentDatabase(database, this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00048EB0 File Offset: 0x000470B0
		protected override void InternalDeactivate()
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionSmi.Deactivate|ADV> {0}, Deactivating.", base.ObjectID);
			if (!this.IsNonPoolableTransactionRoot)
			{
				base.Enlist(null);
			}
			if (this._currentTransaction != null)
			{
				if (this._currentTransaction.IsContext)
				{
					this._currentTransaction = null;
				}
				else if (this._currentTransaction.IsLocal)
				{
					this._currentTransaction.CloseFromConnection();
				}
			}
			this.ContextTransaction = null;
			this._isInUse = 0;
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00048F25 File Offset: 0x00047125
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionSmi.DelegatedTransactionEnded|ADV> {0}, cleaning up after Delegated Transaction Completion", base.ObjectID);
			this._currentTransaction = null;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00048F49 File Offset: 0x00047149
		internal override void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.SqlInternalConnectionSmi.DisconnectTransaction|ADV> {0}, Disconnecting Transaction {1}.", base.ObjectID, internalTransaction.ObjectID);
			if (this._currentTransaction != null && this._currentTransaction == internalTransaction)
			{
				this._currentTransaction = null;
			}
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00048F7E File Offset: 0x0004717E
		public override void Dispose()
		{
			this._smiContext.OutOfScope -= this.OnOutOfScope;
			base.Dispose();
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00048FA0 File Offset: 0x000471A0
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, global::System.Data.IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, SqlInternalConnection.TransactionRequest, string, global::System.Data.IsolationLevel, int, long>("<sc.SqlInternalConnectionSmi.ExecuteTransaction|ADV> {0}, transactionRequest={1}, transactionName='{2}', isolationLevel={3}, internalTransaction=#{4} transactionId=0x{5}.", base.ObjectID, transactionRequest, transactionName, iso, (internalTransaction != null) ? internalTransaction.ObjectID : 0, (internalTransaction != null) ? internalTransaction.TransactionId : 0L);
			switch (transactionRequest)
			{
			case SqlInternalConnection.TransactionRequest.Begin:
				try
				{
					this._pendingTransaction = internalTransaction;
					this._smiConnection.BeginTransaction(transactionName, iso, this._smiEventSink);
					goto IL_00FF;
				}
				finally
				{
					this._pendingTransaction = null;
				}
				break;
			case SqlInternalConnection.TransactionRequest.Promote:
				base.PromotedDTCToken = this._smiConnection.PromoteTransaction(this._currentTransaction.TransactionId, this._smiEventSink);
				goto IL_00FF;
			case SqlInternalConnection.TransactionRequest.Commit:
				break;
			case SqlInternalConnection.TransactionRequest.Rollback:
			case SqlInternalConnection.TransactionRequest.IfRollback:
				this._smiConnection.RollbackTransaction(this._currentTransaction.TransactionId, transactionName, this._smiEventSink);
				goto IL_00FF;
			case SqlInternalConnection.TransactionRequest.Save:
				this._smiConnection.CreateTransactionSavePoint(this._currentTransaction.TransactionId, transactionName, this._smiEventSink);
				goto IL_00FF;
			default:
				goto IL_00FF;
			}
			this._smiConnection.CommitTransaction(this._currentTransaction.TransactionId, this._smiEventSink);
			IL_00FF:
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x000490C8 File Offset: 0x000472C8
		protected override byte[] GetDTCAddress()
		{
			byte[] dtcaddress = this._smiConnection.GetDTCAddress(this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
			if (dtcaddress != null)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<byte[], ushort>("<sc.SqlInternalConnectionSmi.GetDTCAddress|ADV> whereAbouts = {0}, Length = {1}", dtcaddress, (ushort)dtcaddress.Length);
			}
			else
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent("<sc.SqlInternalConnectionSmi.GetDTCAddress|ADV> whereAbouts=null");
			}
			return dtcaddress;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004911C File Offset: 0x0004731C
		internal void GetCurrentTransactionPair(out long transactionId, out Transaction transaction)
		{
			lock (this)
			{
				transactionId = ((this.CurrentTransaction != null) ? this.CurrentTransaction.TransactionId : 0L);
				transaction = null;
				if (transactionId != 0L)
				{
					transaction = this.InternalEnlistedTransaction;
				}
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0004917C File Offset: 0x0004737C
		private void OnOutOfScope(object s, EventArgs e)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionSmi.OutOfScope|ADV> {0} context is out of scope", base.ObjectID);
			base.DelegatedTransaction = null;
			DbConnection owner = base.Owner;
			try
			{
				if (owner != null && 1 == this._isInUse)
				{
					owner.Close();
				}
			}
			finally
			{
				this.ContextTransaction = null;
				this._isInUse = 0;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000491E0 File Offset: 0x000473E0
		protected override void PropagateTransactionCookie(byte[] transactionCookie)
		{
			if (transactionCookie != null)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<byte[], ushort>("<sc.SqlInternalConnectionSmi.PropagateTransactionCookie|ADV> transactionCookie", transactionCookie, (ushort)transactionCookie.Length);
			}
			else
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent("<sc.SqlInternalConnectionSmi.PropagateTransactionCookie|ADV> null");
			}
			this._smiConnection.EnlistTransaction(transactionCookie, this._smiEventSink);
			this._smiEventSink.ProcessMessagesAndThrow();
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00049234 File Offset: 0x00047434
		private void TransactionEndedByServer(long transactionId, TransactionState transactionState)
		{
			SqlDelegatedTransaction delegatedTransaction = base.DelegatedTransaction;
			if (delegatedTransaction != null)
			{
				delegatedTransaction.Transaction.Rollback();
				base.DelegatedTransaction = null;
			}
			this.TransactionEnded(transactionId, transactionState);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00049265 File Offset: 0x00047465
		private void TransactionEnded(long transactionId, TransactionState transactionState)
		{
			if (this._currentTransaction != null)
			{
				this._currentTransaction.Completed(transactionState);
				this._currentTransaction = null;
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00049284 File Offset: 0x00047484
		private void TransactionStarted(long transactionId, bool isDistributed)
		{
			this._currentTransaction = this._pendingTransaction;
			this._pendingTransaction = null;
			if (this._currentTransaction != null)
			{
				this._currentTransaction.TransactionId = transactionId;
			}
			else
			{
				TransactionType transactionType = (isDistributed ? TransactionType.Distributed : TransactionType.LocalFromTSQL);
				this._currentTransaction = new SqlInternalTransaction(this, transactionType, null, transactionId);
			}
			this._currentTransaction.Activate();
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x000492DC File Offset: 0x000474DC
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			SqlDataReader sqlDataReader = base.FindLiveReader(null);
			if (sqlDataReader != null)
			{
				TdsParser parser = command.Connection.Parser;
				throw ADP.OpenReaderExists(parser.MARSOn);
			}
		}

		// Token: 0x0400077F RID: 1919
		private SmiContext _smiContext;

		// Token: 0x04000780 RID: 1920
		private SmiConnection _smiConnection;

		// Token: 0x04000781 RID: 1921
		private SmiEventSink_Default _smiEventSink;

		// Token: 0x04000782 RID: 1922
		private int _isInUse;

		// Token: 0x04000783 RID: 1923
		private SqlInternalTransaction _pendingTransaction;

		// Token: 0x04000784 RID: 1924
		private SqlInternalTransaction _currentTransaction;

		// Token: 0x02000244 RID: 580
		private sealed class EventSink : SmiEventSink_Default
		{
			// Token: 0x17000A3E RID: 2622
			// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x00048D03 File Offset: 0x00046F03
			internal override string ServerVersion
			{
				get
				{
					return SmiContextFactory.Instance.ServerVersion;
				}
			}

			// Token: 0x06001EB3 RID: 7859 RVA: 0x0007D8F0 File Offset: 0x0007BAF0
			protected override void DispatchMessages(bool ignoreNonFatalMessages)
			{
				SqlException ex = base.ProcessMessages(false, ignoreNonFatalMessages);
				if (ex != null)
				{
					SqlConnection connection = this._connection.Connection;
					if (connection != null && connection.FireInfoMessageEventOnUserErrors)
					{
						connection.OnInfoMessage(new SqlInfoMessageEventArgs(ex));
						return;
					}
					this._connection.OnError(ex, false, null);
				}
			}

			// Token: 0x06001EB4 RID: 7860 RVA: 0x0007D93B File Offset: 0x0007BB3B
			internal EventSink(SqlInternalConnectionSmi connection)
			{
				this._connection = connection;
			}

			// Token: 0x06001EB5 RID: 7861 RVA: 0x0007D94A File Offset: 0x0007BB4A
			internal override void DefaultDatabaseChanged(string databaseName)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, string>("<sc.SqlInternalConnectionSmi.EventSink.DefaultDatabaseChanged|ADV> {0}, databaseName='{1}'.", this._connection.ObjectID, databaseName);
				this._connection.CurrentDatabase = databaseName;
			}

			// Token: 0x06001EB6 RID: 7862 RVA: 0x0007D973 File Offset: 0x0007BB73
			internal override void TransactionCommitted(long transactionId)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.EventSink.TransactionCommitted|ADV> {0}, transactionId=0x{1}.", this._connection.ObjectID, transactionId);
				this._connection.TransactionEnded(transactionId, TransactionState.Committed);
			}

			// Token: 0x06001EB7 RID: 7863 RVA: 0x0007D99D File Offset: 0x0007BB9D
			internal override void TransactionDefected(long transactionId)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.EventSink.TransactionDefected|ADV> {0}, transactionId=0x{1}.", this._connection.ObjectID, transactionId);
				this._connection.TransactionEnded(transactionId, TransactionState.Unknown);
			}

			// Token: 0x06001EB8 RID: 7864 RVA: 0x0007D9C7 File Offset: 0x0007BBC7
			internal override void TransactionEnlisted(long transactionId)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.EventSink.TransactionEnlisted|ADV> {0}, transactionId=0x{1}.", this._connection.ObjectID, transactionId);
				this._connection.TransactionStarted(transactionId, true);
			}

			// Token: 0x06001EB9 RID: 7865 RVA: 0x0007D9F1 File Offset: 0x0007BBF1
			internal override void TransactionEnded(long transactionId)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.EventSink.TransactionEnded|ADV> {0}, transactionId=0x{1}.", this._connection.ObjectID, transactionId);
				this._connection.TransactionEndedByServer(transactionId, TransactionState.Unknown);
			}

			// Token: 0x06001EBA RID: 7866 RVA: 0x0007DA1B File Offset: 0x0007BC1B
			internal override void TransactionRolledBack(long transactionId)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.EventSink.TransactionRolledBack|ADV> {0}, transactionId=0x{1}.", this._connection.ObjectID, transactionId);
				this._connection.TransactionEndedByServer(transactionId, TransactionState.Aborted);
			}

			// Token: 0x06001EBB RID: 7867 RVA: 0x0007DA45 File Offset: 0x0007BC45
			internal override void TransactionStarted(long transactionId)
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionSmi.EventSink.TransactionStarted|ADV> {0}, transactionId=0x{1}.", this._connection.ObjectID, transactionId);
				this._connection.TransactionStarted(transactionId, false);
			}

			// Token: 0x04001673 RID: 5747
			private SqlInternalConnectionSmi _connection;
		}
	}
}
