using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.Common;
using Microsoft.Data.ProviderBase;
using Microsoft.Identity.Client;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000F0 RID: 240
	internal sealed class SqlInternalConnectionTds : SqlInternalConnection, IDisposable
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001271 RID: 4721 RVA: 0x0004944C File Offset: 0x0004764C
		private int accessTokenExpirationBufferTime
		{
			get
			{
				if (base.ConnectionOptions.ConnectTimeout != 0 && base.ConnectionOptions.ConnectTimeout < 600)
				{
					return base.ConnectionOptions.ConnectTimeout;
				}
				return 600;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0004947E File Offset: 0x0004767E
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00049486 File Offset: 0x00047686
		internal bool IsSQLDNSCachingSupported
		{
			get
			{
				return this._serverSupportsDNSCaching;
			}
			set
			{
				this._serverSupportsDNSCaching = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0004948F File Offset: 0x0004768F
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00049497 File Offset: 0x00047697
		internal bool IsSQLDNSRetryEnabled
		{
			get
			{
				return this._SQLDNSRetryEnabled;
			}
			set
			{
				this._SQLDNSRetryEnabled = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x000494A0 File Offset: 0x000476A0
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x000494A8 File Offset: 0x000476A8
		internal bool IsDNSCachingBeforeRedirectSupported
		{
			get
			{
				return this.DNSCachingBeforeRedirect;
			}
			set
			{
				this.DNSCachingBeforeRedirect = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x000494B1 File Offset: 0x000476B1
		internal SessionData CurrentSessionData
		{
			get
			{
				if (this._currentSessionData != null)
				{
					this._currentSessionData._database = base.CurrentDatabase;
					this._currentSessionData._language = this._currentLanguage;
				}
				return this._currentSessionData;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001279 RID: 4729 RVA: 0x000494E3 File Offset: 0x000476E3
		internal SqlConnectionTimeoutErrorInternal TimeoutErrorInternal
		{
			get
			{
				return this.timeoutErrorInternal;
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x000494EB File Offset: 0x000476EB
		static SqlInternalConnectionTds()
		{
			SqlInternalConnectionTds.populateTransientErrors();
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00049518 File Offset: 0x00047718
		internal SqlInternalConnectionTds(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, object providerInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString userConnectionOptions = null, SessionData reconnectSessionData = null, ServerCertificateValidationCallback serverCallback = null, ClientCertificateRetrievalCallback clientCallback = null, DbConnectionPool pool = null, string accessToken = null, SqlClientOriginalNetworkAddressInfo originalNetworkAddressInfo = null, bool applyTransientFaultHandling = false)
			: base(connectionOptions)
		{
			this._dbConnectionPool = pool;
			if (connectionOptions.ConnectRetryCount > 0)
			{
				this._recoverySessionData = reconnectSessionData;
				if (reconnectSessionData == null)
				{
					this._currentSessionData = new SessionData();
				}
				else
				{
					this._currentSessionData = new SessionData(this._recoverySessionData);
					this._originalDatabase = this._recoverySessionData._initialDatabase;
					this._originalLanguage = this._recoverySessionData._initialLanguage;
				}
			}
			if (connectionOptions.UserInstance && InOutOfProcHelper.InProc)
			{
				throw SQL.UserInstanceNotAvailableInProc();
			}
			if (accessToken != null)
			{
				this._accessTokenInBytes = Encoding.Unicode.GetBytes(accessToken);
			}
			this._activeDirectoryAuthTimeoutRetryHelper = new ActiveDirectoryAuthenticationTimeoutRetryHelper();
			this._sqlAuthenticationProviderManager = SqlAuthenticationProviderManager.Instance;
			this._serverCallback = serverCallback;
			this._clientCallback = clientCallback;
			this._originalNetworkAddressInfo = originalNetworkAddressInfo;
			this._identity = identity;
			this._poolGroupProviderInfo = (SqlConnectionPoolGroupProviderInfo)providerInfo;
			this._fResetConnection = connectionOptions.ConnectionReset;
			if (this._fResetConnection && this._recoverySessionData == null)
			{
				this._originalDatabase = connectionOptions.InitialCatalog;
				this._originalLanguage = connectionOptions.CurrentLanguage;
			}
			this.timeoutErrorInternal = new SqlConnectionTimeoutErrorInternal();
			this._credential = credential;
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				this._timeout = TimeoutTimer.StartSecondsTimeout(connectionOptions.ConnectTimeout);
				int num = (applyTransientFaultHandling ? (connectionOptions.ConnectRetryCount + 1) : 1);
				int num2 = connectionOptions.ConnectRetryInterval * 1000;
				for (int i = 0; i < num; i++)
				{
					try
					{
						this.OpenLoginEnlist(this._timeout, connectionOptions, credential, newPassword, newSecurePassword, redirectedUserInstance);
						break;
					}
					catch (SqlException ex)
					{
						if (i + 1 == num || !applyTransientFaultHandling || this._timeout.IsExpired || this._timeout.MillisecondsRemaining < (long)num2 || !this.IsTransientError(ex))
						{
							throw;
						}
						Thread.Sleep(num2);
					}
				}
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
				throw;
			}
			finally
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.ctor|ADV> {0}, constructed new TDS internal connection", base.ObjectID);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00049798 File Offset: 0x00047998
		private static void populateTransientErrors()
		{
			SqlInternalConnectionTds.transientErrors.Add(4060);
			SqlInternalConnectionTds.transientErrors.Add(10928);
			SqlInternalConnectionTds.transientErrors.Add(10929);
			SqlInternalConnectionTds.transientErrors.Add(40197);
			SqlInternalConnectionTds.transientErrors.Add(40020);
			SqlInternalConnectionTds.transientErrors.Add(40143);
			SqlInternalConnectionTds.transientErrors.Add(40166);
			SqlInternalConnectionTds.transientErrors.Add(40540);
			SqlInternalConnectionTds.transientErrors.Add(40501);
			SqlInternalConnectionTds.transientErrors.Add(40613);
			SqlInternalConnectionTds.transientErrors.Add(42108);
			SqlInternalConnectionTds.transientErrors.Add(42109);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00049868 File Offset: 0x00047A68
		private bool IsTransientError(SqlException exc)
		{
			if (exc == null)
			{
				return false;
			}
			foreach (object obj in exc.Errors)
			{
				SqlError sqlError = (SqlError)obj;
				if (SqlInternalConnectionTds.transientErrors.Contains(sqlError.Number))
				{
					base.UnDoomThisConnection();
					return true;
				}
			}
			return false;
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x000498E0 File Offset: 0x00047AE0
		internal Guid ClientConnectionId
		{
			get
			{
				return this._clientConnectionId;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x000498E8 File Offset: 0x00047AE8
		internal Guid OriginalClientConnectionId
		{
			get
			{
				return this._originalClientConnectionId;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x000498F0 File Offset: 0x00047AF0
		internal string RoutingDestination
		{
			get
			{
				return this._routingDestination;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x000498F8 File Offset: 0x00047AF8
		internal RoutingInfo RoutingInfo
		{
			get
			{
				return this._routingInfo;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x00049900 File Offset: 0x00047B00
		internal override SqlInternalTransaction CurrentTransaction
		{
			get
			{
				return this._parser.CurrentTransaction;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0004990D File Offset: 0x00047B0D
		internal override SqlInternalTransaction AvailableInternalTransaction
		{
			get
			{
				if (!this._parser._fResetConnection)
				{
					return this.CurrentTransaction;
				}
				return null;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x00049926 File Offset: 0x00047B26
		internal override SqlInternalTransaction PendingTransaction
		{
			get
			{
				return this._parser.PendingTransaction;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x00049933 File Offset: 0x00047B33
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0004993B File Offset: 0x00047B3B
		internal string InstanceName
		{
			get
			{
				return this._instanceName;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00049943 File Offset: 0x00047B43
		internal override bool IsLockedForBulkCopy
		{
			get
			{
				return !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00049964 File Offset: 0x00047B64
		protected internal override bool IsNonPoolableTransactionRoot
		{
			get
			{
				return this.IsTransactionRoot && (!this.Is2008OrNewer || base.Pool == null);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x00049983 File Offset: 0x00047B83
		internal override bool Is2000
		{
			get
			{
				return this._loginAck.isVersion8;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600128A RID: 4746 RVA: 0x00049990 File Offset: 0x00047B90
		internal override bool Is2005OrNewer
		{
			get
			{
				return this._parser.Is2005OrNewer;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0004999D File Offset: 0x00047B9D
		internal override bool Is2008OrNewer
		{
			get
			{
				return this._parser.Is2008OrNewer;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000499AA File Offset: 0x00047BAA
		internal int PacketSize
		{
			get
			{
				return this._currentPacketSize;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x000499B2 File Offset: 0x00047BB2
		internal TdsParser Parser
		{
			get
			{
				return this._parser;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600128E RID: 4750 RVA: 0x000499BA File Offset: 0x00047BBA
		internal string ServerProvidedFailOverPartner
		{
			get
			{
				return this._currentFailoverPartner;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x000499C2 File Offset: 0x00047BC2
		internal SqlConnectionPoolGroupProviderInfo PoolGroupProviderInfo
		{
			get
			{
				return this._poolGroupProviderInfo;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x000499CC File Offset: 0x00047BCC
		protected override bool ReadyToPrepareTransaction
		{
			get
			{
				return base.FindLiveReader(null) == null;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x000499E5 File Offset: 0x00047BE5
		public override string ServerVersion
		{
			get
			{
				return string.Format("{0:00}.{1:00}.{2:0000}", this._loginAck.majorVersion, (short)this._loginAck.minorVersion, this._loginAck.buildNum);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00049A21 File Offset: 0x00047C21
		public int ServerProcessId
		{
			get
			{
				return this.Parser._physicalStateObj._spid;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0001996E File Offset: 0x00017B6E
		protected override bool UnbindOnTransactionCompletion
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00049A34 File Offset: 0x00047C34
		internal override bool IsAccessTokenExpired
		{
			get
			{
				return this._federatedAuthenticationInfoRequested && DateTime.FromFileTimeUtc(this._fedAuthToken.expirationFileTime) < DateTime.UtcNow.AddSeconds((double)this.accessTokenExpirationBufferTime);
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00049A74 File Offset: 0x00047C74
		protected override void ChangeDatabaseInternal(string database)
		{
			database = SqlConnection.FixupDatabaseTransactionName(database);
			Task task = this._parser.TdsExecuteSQLBatch("use " + database, base.ConnectionOptions.ConnectTimeout, null, this._parser._physicalStateObj, true, false, null);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00049AD8 File Offset: 0x00047CD8
		public override void Dispose()
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.Dispose|ADV> {0} disposing", base.ObjectID);
			try
			{
				TdsParser tdsParser = Interlocked.Exchange<TdsParser>(ref this._parser, null);
				if (tdsParser != null)
				{
					tdsParser.Disconnect();
				}
			}
			finally
			{
				this._loginAck = null;
				this._fConnectionOpen = false;
			}
			base.Dispose();
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00049B38 File Offset: 0x00047D38
		internal override void ValidateConnectionForExecute(SqlCommand command)
		{
			TdsParser parser = this._parser;
			if (parser == null || parser.State == TdsParserState.Broken || parser.State == TdsParserState.Closed)
			{
				throw ADP.ClosedConnectionError();
			}
			SqlDataReader sqlDataReader = null;
			if (parser.MARSOn)
			{
				if (command != null)
				{
					sqlDataReader = base.FindLiveReader(command);
				}
			}
			else
			{
				if (this._asyncCommandCount > 0)
				{
					throw SQL.MARSUnsupportedOnConnection();
				}
				sqlDataReader = base.FindLiveReader(null);
			}
			if (sqlDataReader != null)
			{
				throw ADP.OpenReaderExists(parser.MARSOn);
			}
			if (!parser.MARSOn && parser._physicalStateObj._pendingData)
			{
				parser.DrainData(parser._physicalStateObj);
			}
			parser.RollbackOrphanedAPITransactions();
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00049BCC File Offset: 0x00047DCC
		internal void CheckEnlistedTransactionBinding()
		{
			Transaction enlistedTransaction = base.EnlistedTransaction;
			if (enlistedTransaction != null)
			{
				bool flag = base.ConnectionOptions.TransactionBinding == SqlConnectionString.TransactionBindingEnum.ExplicitUnbind;
				if (flag)
				{
					Transaction transaction = Transaction.Current;
					if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active || !enlistedTransaction.Equals(transaction))
					{
						throw ADP.TransactionConnectionMismatch();
					}
				}
				else if (enlistedTransaction.TransactionInformation.Status != TransactionStatus.Active)
				{
					if (base.EnlistedTransactionDisposed)
					{
						base.DetachTransaction(enlistedTransaction, true);
						return;
					}
					throw ADP.TransactionCompletedButNotDisposed();
				}
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00049C44 File Offset: 0x00047E44
		internal override bool IsConnectionAlive(bool throwOnException)
		{
			return this._parser._physicalStateObj.IsConnectionAlive(throwOnException);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00049C66 File Offset: 0x00047E66
		protected override void Activate(Transaction transaction)
		{
			this.FailoverPermissionDemand();
			if (null != transaction)
			{
				if (base.ConnectionOptions.Enlist)
				{
					base.Enlist(transaction);
					return;
				}
			}
			else
			{
				base.Enlist(null);
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00049C93 File Offset: 0x00047E93
		protected override void InternalDeactivate()
		{
			if (this._asyncCommandCount != 0)
			{
				base.DoomThisConnection();
			}
			if (!this.IsNonPoolableTransactionRoot && this._parser != null)
			{
				this._parser.Deactivate(base.IsConnectionDoomed);
				if (!base.IsConnectionDoomed)
				{
					this.ResetConnection();
				}
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00049CD4 File Offset: 0x00047ED4
		private void ResetConnection()
		{
			if (this._fResetConnection)
			{
				if (this.Is2000)
				{
					this._parser.PrepareResetConnection(this.IsTransactionRoot && !this.IsNonPoolableTransactionRoot);
				}
				else if (!base.IsEnlistedInTransaction)
				{
					try
					{
						Task task = this._parser.TdsExecuteSQLBatch("sp_reset_connection", 30, null, this._parser._physicalStateObj, true, false, null);
						this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
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
				base.CurrentDatabase = this._originalDatabase;
				this._currentLanguage = this._originalLanguage;
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00049DA0 File Offset: 0x00047FA0
		internal void DecrementAsyncCount()
		{
			Interlocked.Decrement(ref this._asyncCommandCount);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00049DAE File Offset: 0x00047FAE
		internal void IncrementAsyncCount()
		{
			Interlocked.Increment(ref this._asyncCommandCount);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00049DBC File Offset: 0x00047FBC
		internal override void DisconnectTransaction(SqlInternalTransaction internalTransaction)
		{
			TdsParser parser = this.Parser;
			if (parser != null)
			{
				parser.DisconnectTransaction(internalTransaction);
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00049DDA File Offset: 0x00047FDA
		internal void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, global::System.Data.IsolationLevel iso)
		{
			this.ExecuteTransaction(transactionRequest, name, iso, null, false);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00049DE8 File Offset: 0x00047FE8
		internal override void ExecuteTransaction(SqlInternalConnection.TransactionRequest transactionRequest, string name, global::System.Data.IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			if (base.IsConnectionDoomed)
			{
				if (transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback)
				{
					return;
				}
				throw SQL.ConnectionDoomed();
			}
			else
			{
				if ((transactionRequest == SqlInternalConnection.TransactionRequest.Commit || transactionRequest == SqlInternalConnection.TransactionRequest.Rollback || transactionRequest == SqlInternalConnection.TransactionRequest.IfRollback) && !this.Parser.MARSOn && this.Parser._physicalStateObj.BcpLock)
				{
					throw SQL.ConnectionLockedForBcpEvent();
				}
				string text = ((name == null) ? string.Empty : name);
				if (!this._parser.Is2005OrNewer)
				{
					this.ExecuteTransactionPre2005(transactionRequest, text, iso, internalTransaction);
					return;
				}
				this.ExecuteTransaction2005(transactionRequest, text, iso, internalTransaction, isDelegateControlRequest);
				return;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00049E70 File Offset: 0x00048070
		internal void ExecuteTransactionPre2005(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, global::System.Data.IsolationLevel iso, SqlInternalTransaction internalTransaction)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (iso <= global::System.Data.IsolationLevel.ReadUncommitted)
			{
				if (iso == global::System.Data.IsolationLevel.Unspecified)
				{
					goto IL_00DA;
				}
				if (iso == global::System.Data.IsolationLevel.Chaos)
				{
					throw SQL.NotSupportedIsolationLevel(iso);
				}
				if (iso == global::System.Data.IsolationLevel.ReadUncommitted)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
					stringBuilder.Append(";");
					goto IL_00DA;
				}
			}
			else if (iso <= global::System.Data.IsolationLevel.RepeatableRead)
			{
				if (iso == global::System.Data.IsolationLevel.ReadCommitted)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL READ COMMITTED");
					stringBuilder.Append(";");
					goto IL_00DA;
				}
				if (iso == global::System.Data.IsolationLevel.RepeatableRead)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL REPEATABLE READ");
					stringBuilder.Append(";");
					goto IL_00DA;
				}
			}
			else
			{
				if (iso == global::System.Data.IsolationLevel.Serializable)
				{
					stringBuilder.Append("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE");
					stringBuilder.Append(";");
					goto IL_00DA;
				}
				if (iso == global::System.Data.IsolationLevel.Snapshot)
				{
					throw SQL.SnapshotNotSupported(global::System.Data.IsolationLevel.Snapshot);
				}
			}
			throw ADP.InvalidIsolationLevel(iso);
			IL_00DA:
			if (!ADP.IsEmpty(transactionName))
			{
				transactionName = " " + SqlConnection.FixupDatabaseTransactionName(transactionName);
			}
			switch (transactionRequest)
			{
			case SqlInternalConnection.TransactionRequest.Begin:
				stringBuilder.Append("BEGIN TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.Commit:
				stringBuilder.Append("COMMIT TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.Rollback:
				stringBuilder.Append("ROLLBACK TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.IfRollback:
				stringBuilder.Append("IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			case SqlInternalConnection.TransactionRequest.Save:
				stringBuilder.Append("SAVE TRANSACTION");
				stringBuilder.Append(transactionName);
				break;
			}
			Task task = this._parser.TdsExecuteSQLBatch(stringBuilder.ToString(), base.ConnectionOptions.ConnectTimeout, null, this._parser._physicalStateObj, true, false, null);
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
			if (transactionRequest == SqlInternalConnection.TransactionRequest.Begin)
			{
				this._parser.CurrentTransaction = internalTransaction;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0004A054 File Offset: 0x00048254
		internal void ExecuteTransaction2005(SqlInternalConnection.TransactionRequest transactionRequest, string transactionName, global::System.Data.IsolationLevel iso, SqlInternalTransaction internalTransaction, bool isDelegateControlRequest)
		{
			TdsEnums.TransactionManagerRequestType transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Begin;
			if (iso <= global::System.Data.IsolationLevel.ReadUncommitted)
			{
				if (iso == global::System.Data.IsolationLevel.Unspecified)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.Unspecified;
					goto IL_007E;
				}
				if (iso == global::System.Data.IsolationLevel.Chaos)
				{
					throw SQL.NotSupportedIsolationLevel(iso);
				}
				if (iso == global::System.Data.IsolationLevel.ReadUncommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.ReadUncommitted;
					goto IL_007E;
				}
			}
			else if (iso <= global::System.Data.IsolationLevel.RepeatableRead)
			{
				if (iso == global::System.Data.IsolationLevel.ReadCommitted)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.ReadCommitted;
					goto IL_007E;
				}
				if (iso == global::System.Data.IsolationLevel.RepeatableRead)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.RepeatableRead;
					goto IL_007E;
				}
			}
			else
			{
				if (iso == global::System.Data.IsolationLevel.Serializable)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.Serializable;
					goto IL_007E;
				}
				if (iso == global::System.Data.IsolationLevel.Snapshot)
				{
					TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel = TdsEnums.TransactionManagerIsolationLevel.Snapshot;
					goto IL_007E;
				}
			}
			throw ADP.InvalidIsolationLevel(iso);
			IL_007E:
			TdsParserStateObject tdsParserStateObject = this._parser._physicalStateObj;
			TdsParser parser = this._parser;
			bool flag = false;
			bool releaseConnectionLock = false;
			if (!this.ThreadHasParserLockForClose)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
				releaseConnectionLock = true;
			}
			try
			{
				switch (transactionRequest)
				{
				case SqlInternalConnection.TransactionRequest.Begin:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Begin;
					break;
				case SqlInternalConnection.TransactionRequest.Promote:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Promote;
					break;
				case SqlInternalConnection.TransactionRequest.Commit:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Commit;
					break;
				case SqlInternalConnection.TransactionRequest.Rollback:
				case SqlInternalConnection.TransactionRequest.IfRollback:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Rollback;
					break;
				case SqlInternalConnection.TransactionRequest.Save:
					transactionManagerRequestType = TdsEnums.TransactionManagerRequestType.Save;
					break;
				}
				if ((internalTransaction != null && internalTransaction.RestoreBrokenConnection) & releaseConnectionLock)
				{
					Task task = internalTransaction.Parent.Connection.ValidateAndReconnect(delegate
					{
						this.ThreadHasParserLockForClose = false;
						this._parserLock.Release();
						releaseConnectionLock = false;
					}, 0);
					if (task != null)
					{
						AsyncHelper.WaitForCompletion(task, 0, null, true);
						internalTransaction.ConnectionHasBeenRestored = true;
						return;
					}
				}
				if (internalTransaction != null && internalTransaction.IsDelegated)
				{
					if (this._parser.MARSOn)
					{
						tdsParserStateObject = this._parser.GetSession(this);
						flag = true;
					}
					if (internalTransaction.OpenResultsCount != 0)
					{
						SqlClientEventSource.Log.TryTraceEvent<int, int, bool>("<sc.SqlInternalConnectionTds.ExecuteTransaction2005|DATA|CATCH> {0}, Connection is marked to be doomed when closed. Transaction ended with OpenResultsCount {1} > 0, MARSOn {2}", base.ObjectID, internalTransaction.OpenResultsCount, this._parser.MARSOn);
						throw SQL.CannotCompleteDelegatedTransactionWithOpenResults(this, this._parser.MARSOn);
					}
				}
				TdsEnums.TransactionManagerIsolationLevel transactionManagerIsolationLevel;
				this._parser.TdsExecuteTransactionManagerRequest(null, transactionManagerRequestType, transactionName, transactionManagerIsolationLevel, base.ConnectionOptions.ConnectTimeout, internalTransaction, tdsParserStateObject, isDelegateControlRequest);
			}
			finally
			{
				if (flag)
				{
					parser.PutSession(tdsParserStateObject);
				}
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004A278 File Offset: 0x00048478
		internal override void DelegatedTransactionEnded()
		{
			base.DelegatedTransactionEnded();
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004A280 File Offset: 0x00048480
		protected override byte[] GetDTCAddress()
		{
			return this._parser.GetDTCAddress(base.ConnectionOptions.ConnectTimeout, this._parser.GetSession(this));
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004A2B1 File Offset: 0x000484B1
		protected override void PropagateTransactionCookie(byte[] cookie)
		{
			this._parser.PropagateDistributedTransaction(cookie, base.ConnectionOptions.ConnectTimeout, this._parser._physicalStateObj);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004A2D8 File Offset: 0x000484D8
		private void CompleteLogin(bool enlistOK)
		{
			this._parser.Run(RunBehavior.UntilDone, null, null, null, this._parser._physicalStateObj);
			if (this._routingInfo == null)
			{
				if (this._federatedAuthenticationRequested && !this._federatedAuthenticationAcknowledged)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.CompleteLogin|ERR> {0}, Server did not acknowledge the federated authentication request", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.FedAuthNotAcknowledged);
				}
				if (this._federatedAuthenticationInfoRequested && !this._federatedAuthenticationInfoReceived)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.CompleteLogin|ERR> {0}, Server never sent the requested federated authentication info", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.FedAuthInfoNotReceived);
				}
				if (!this._sessionRecoveryAcknowledged)
				{
					this._currentSessionData = null;
					if (this._recoverySessionData != null)
					{
						throw SQL.CR_NoCRAckAtReconnection(this);
					}
				}
				if (this._currentSessionData != null && this._recoverySessionData == null)
				{
					this._currentSessionData._initialDatabase = base.CurrentDatabase;
					this._currentSessionData._initialCollation = this._currentSessionData._collation;
					this._currentSessionData._initialLanguage = this._currentLanguage;
				}
				bool flag = (this._parser.EncryptionOptions & EncryptionOptions.OPTIONS_MASK) == EncryptionOptions.ON;
				if (this._recoverySessionData != null && this._recoverySessionData._encrypted != flag)
				{
					throw SQL.CR_EncryptionChanged(this);
				}
				if (this._currentSessionData != null)
				{
					this._currentSessionData._encrypted = flag;
				}
				this._recoverySessionData = null;
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_EnableMars;
			this._parser.EnableMars();
			this._fConnectionOpen = true;
			SqlClientEventSource.Log.TryAdvancedTraceEvent("<sc.SqlInternalConnectionTds.CompleteLogin|ADV> Post-Login Phase: Server connection obtained.");
			if (enlistOK && base.ConnectionOptions.Enlist && this._routingInfo == null)
			{
				this._parser._physicalStateObj.SniContext = SniContext.Snix_AutoEnlist;
				Transaction currentTransaction = ADP.GetCurrentTransaction();
				base.Enlist(currentTransaction);
			}
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004A490 File Offset: 0x00048690
		private void Login(ServerInfo server, TimeoutTimer timeout, string newPassword, SecureString newSecurePassword, SqlConnectionEncryptOption encrypt)
		{
			SqlLogin sqlLogin = new SqlLogin();
			base.CurrentDatabase = server.ResolvedDatabaseName;
			this._currentPacketSize = base.ConnectionOptions.PacketSize;
			this._currentLanguage = base.ConnectionOptions.CurrentLanguage;
			int num = 0;
			if (!timeout.IsInfinite)
			{
				long num2 = timeout.MillisecondsRemaining / 1000L;
				if (num2 == 0L && LocalAppContextSwitches.UseMinimumLoginTimeout)
				{
					num2 = 1L;
				}
				if (2147483647L > num2)
				{
					num = (int)num2;
				}
			}
			sqlLogin.authentication = base.ConnectionOptions.Authentication;
			sqlLogin.timeout = num;
			sqlLogin.userInstance = base.ConnectionOptions.UserInstance;
			sqlLogin.hostName = base.ConnectionOptions.ObtainWorkstationId();
			sqlLogin.userName = base.ConnectionOptions.UserID;
			sqlLogin.password = base.ConnectionOptions.Password;
			sqlLogin.applicationName = base.ConnectionOptions.ApplicationName;
			sqlLogin.language = this._currentLanguage;
			if (!sqlLogin.userInstance)
			{
				sqlLogin.database = base.CurrentDatabase;
				sqlLogin.attachDBFilename = base.ConnectionOptions.AttachDBFilename;
			}
			sqlLogin.serverName = server.UserServerName;
			sqlLogin.useReplication = base.ConnectionOptions.Replication;
			sqlLogin.useSSPI = base.ConnectionOptions.IntegratedSecurity || (base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated && !this._fedAuthRequired);
			sqlLogin.packetSize = this._currentPacketSize;
			sqlLogin.newPassword = newPassword;
			sqlLogin.readOnlyIntent = base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly;
			sqlLogin.credential = this._credential;
			if (newSecurePassword != null)
			{
				sqlLogin.newSecurePassword = newSecurePassword;
			}
			TdsEnums.FeatureExtension featureExtension = TdsEnums.FeatureExtension.None;
			if (base.ConnectionOptions.ConnectRetryCount > 0)
			{
				featureExtension |= TdsEnums.FeatureExtension.SessionRecovery;
				this._sessionRecoveryRequested = true;
			}
			if (base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryPassword || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryServicePrincipal || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryMSI || base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryDefault || (base.ConnectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated && this._fedAuthRequired))
			{
				featureExtension |= TdsEnums.FeatureExtension.FedAuth;
				this._federatedAuthenticationInfoRequested = true;
				this._fedAuthFeatureExtensionData = new FederatedAuthenticationFeatureExtensionData
				{
					libraryType = TdsEnums.FedAuthLibrary.MSAL,
					authentication = base.ConnectionOptions.Authentication,
					fedAuthRequiredPreLoginResponse = this._fedAuthRequired
				};
			}
			if (this._accessTokenInBytes != null)
			{
				featureExtension |= TdsEnums.FeatureExtension.FedAuth;
				this._fedAuthFeatureExtensionData = new FederatedAuthenticationFeatureExtensionData
				{
					libraryType = TdsEnums.FedAuthLibrary.SecurityToken,
					fedAuthRequiredPreLoginResponse = this._fedAuthRequired,
					accessToken = this._accessTokenInBytes
				};
				this._federatedAuthenticationRequested = true;
			}
			featureExtension |= TdsEnums.FeatureExtension.Tce | TdsEnums.FeatureExtension.GlobalTransactions | TdsEnums.FeatureExtension.DataClassification;
			featureExtension |= TdsEnums.FeatureExtension.UTF8Support;
			if (base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly)
			{
				featureExtension |= TdsEnums.FeatureExtension.AzureSQLSupport;
			}
			featureExtension |= TdsEnums.FeatureExtension.SQLDNSCaching;
			this._parser.TdsLogin(sqlLogin, featureExtension, this._recoverySessionData, this._fedAuthFeatureExtensionData, this._originalNetworkAddressInfo, encrypt);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0004A786 File Offset: 0x00048986
		private void LoginFailure()
		{
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.LoginFailure|RES|CPOOL> {0}", base.ObjectID);
			if (this._parser != null)
			{
				this._parser.Disconnect();
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0004A7B0 File Offset: 0x000489B0
		private void OpenLoginEnlist(TimeoutTimer timeout, SqlConnectionString connectionOptions, SqlCredential credential, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance)
		{
			ServerInfo serverInfo = new ServerInfo(connectionOptions);
			bool flag;
			string text;
			if (this.PoolGroupProviderInfo != null)
			{
				flag = this.PoolGroupProviderInfo.UseFailoverPartner;
				text = this.PoolGroupProviderInfo.FailoverPartner;
			}
			else
			{
				flag = false;
				text = base.ConnectionOptions.FailoverPartner;
			}
			this.timeoutErrorInternal.SetInternalSourceType(flag ? SqlConnectionInternalSourceType.Failover : SqlConnectionInternalSourceType.Principle);
			bool flag2 = !ADP.IsEmpty(text);
			try
			{
				this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
				if (flag2)
				{
					this.timeoutErrorInternal.SetFailoverScenario(true);
					this.LoginWithFailover(flag, serverInfo, text, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				else
				{
					this.timeoutErrorInternal.SetFailoverScenario(false);
					this.LoginNoFailover(serverInfo, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
				}
				if (!base.IsAzureSQLConnection && base.ConnectionOptions.ApplicationIntent == ApplicationIntent.ReadOnly)
				{
					if (!string.IsNullOrEmpty(base.ConnectionOptions.FailoverPartner))
					{
						throw SQL.ROR_FailoverNotSupportedConnString();
					}
					if (this.ServerProvidedFailOverPartner != null)
					{
						throw SQL.ROR_FailoverNotSupportedServer(this);
					}
				}
				this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			}
			catch (Exception ex)
			{
				if (ADP.IsCatchableExceptionType(ex))
				{
					this.LoginFailure();
				}
				throw;
			}
			this.timeoutErrorInternal.SetAllCompleteMarker();
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0004A8D4 File Offset: 0x00048AD4
		private bool IsDoNotRetryConnectError(SqlException exc)
		{
			return 18456 == exc.Number || 18488 == exc.Number || 1346 == exc.Number || exc._doNotReconnect;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004A908 File Offset: 0x00048B08
		private void LoginNoFailover(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			int num = 0;
			ServerInfo serverInfo2 = serverInfo;
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, string>("<sc.SqlInternalConnectionTds.LoginNoFailover|ADV> {0}, host={1}", base.ObjectID, serverInfo.UserServerName);
			int num2 = 100;
			this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance, connectionOptions);
			bool flag = this.ShouldDisableTnir(connectionOptions);
			long num3 = 0L;
			bool flag2 = connectionOptions.MultiSubnetFailover || (connectionOptions.TransparentNetworkIPResolution && !flag);
			int num5;
			TimeoutTimer timeoutTimer;
			TimeoutTimer timeoutTimer2;
			checked
			{
				if (flag2)
				{
					float num4 = (connectionOptions.MultiSubnetFailover ? 0.08f : 0.125f);
					if (timeout.IsInfinite)
					{
						num3 = (long)(unchecked(num4 * 15000f));
					}
					else
					{
						num3 = (long)(unchecked(num4 * (float)timeout.MillisecondsRemaining));
					}
				}
				num5 = 0;
				timeoutTimer = null;
				timeoutTimer2 = timeout;
			}
			for (;;)
			{
				bool flag3 = connectionOptions.TransparentNetworkIPResolution && !flag && num5 == 1;
				if (flag2)
				{
					int num6;
					num5 = (num6 = num5 + 1);
					if (connectionOptions.TransparentNetworkIPResolution)
					{
						num6 = 1 << num5 - 1;
					}
					checked
					{
						long num7 = num3 * unchecked((long)num6);
						long millisecondsRemaining = timeout.MillisecondsRemaining;
						if (flag3)
						{
							num7 = Math.Max(500L, num7);
						}
						if (num7 > millisecondsRemaining)
						{
							num7 = millisecondsRemaining;
						}
						timeoutTimer = TimeoutTimer.StartMillisecondsTimeout(num7);
					}
				}
				if (this._parser != null)
				{
					this._parser.Disconnect();
				}
				this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
				try
				{
					if (flag2)
					{
						timeoutTimer2 = timeoutTimer;
					}
					this.AttemptOneLogin(serverInfo, newPassword, newSecurePassword, !flag2, timeoutTimer2, false, flag3, flag);
					if (connectionOptions.MultiSubnetFailover && this.ServerProvidedFailOverPartner != null)
					{
						throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
					}
					if (this._routingInfo == null)
					{
						goto IL_0346;
					}
					SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SqlInternalConnectionTds.LoginNoFailover> Routed to {0}", serverInfo.ExtendedServerName);
					if (num > 10)
					{
						throw SQL.ROR_RecursiveRoutingNotSupported(this);
					}
					if (timeout.IsExpired)
					{
						throw SQL.ROR_TimeoutAfterRoutingInfo(this);
					}
					serverInfo = new ServerInfo(base.ConnectionOptions, this._routingInfo, serverInfo.ResolvedServerName, serverInfo.ServerSPN);
					this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.RoutingDestination);
					this._originalClientConnectionId = this._clientConnectionId;
					this._routingDestination = serverInfo.UserServerName;
					this._currentPacketSize = base.ConnectionOptions.PacketSize;
					this._currentLanguage = (this._originalLanguage = base.ConnectionOptions.CurrentLanguage);
					base.CurrentDatabase = (this._originalDatabase = base.ConnectionOptions.InitialCatalog);
					this._currentFailoverPartner = null;
					this._instanceName = string.Empty;
					num++;
					continue;
				}
				catch (SqlException ex)
				{
					if (this.AttemptRetryADAuthWithTimeoutError(ex, connectionOptions, timeout))
					{
						continue;
					}
					if (this._parser == null || this._parser.State != TdsParserState.Closed || this.IsDoNotRetryConnectError(ex) || timeout.IsExpired)
					{
						throw;
					}
					if (timeout.MillisecondsRemaining <= (long)num2)
					{
						throw;
					}
				}
				if (this.ServerProvidedFailOverPartner != null)
				{
					break;
				}
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.SqlInternalConnectionTds.LoginNoFailover|ADV> {0}, sleeping {1}[milisec]", base.ObjectID, num2);
				Thread.Sleep(num2);
				num2 = ((num2 < 500) ? (num2 * 2) : 1000);
			}
			if (connectionOptions.MultiSubnetFailover)
			{
				throw SQL.MultiSubnetFailoverWithFailoverPartner(true, this);
			}
			this.timeoutErrorInternal.ResetAndRestartPhase();
			this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PreLoginBegin);
			this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
			this.timeoutErrorInternal.SetFailoverScenario(true);
			this.LoginWithFailover(true, serverInfo, this.ServerProvidedFailOverPartner, newPassword, newSecurePassword, redirectedUserInstance, connectionOptions, credential, timeout);
			return;
			IL_0346:
			this._activeDirectoryAuthTimeoutRetryHelper.State = ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn;
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverCheck(false, connectionOptions, this.ServerProvidedFailOverPartner);
			}
			base.CurrentDataSource = serverInfo2.UserServerName;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004ACAC File Offset: 0x00048EAC
		private bool ShouldDisableTnir(SqlConnectionString connectionOptions)
		{
			bool flag = ADP.IsAzureSqlServerEndpoint(connectionOptions.DataSource);
			bool flag2 = this._accessTokenInBytes != null || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryPassword || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryIntegrated || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryInteractive || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryServicePrincipal || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryManagedIdentity || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryMSI || connectionOptions.Authentication == SqlAuthenticationMethod.ActiveDirectoryDefault;
			return !connectionOptions.Parsetable.ContainsKey("Transparent Network IP Resolution") && (flag || flag2);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0004AD33 File Offset: 0x00048F33
		private bool AttemptRetryADAuthWithTimeoutError(SqlException sqlex, SqlConnectionString connectionOptions, TimeoutTimer timeout)
		{
			if (!this._activeDirectoryAuthTimeoutRetryHelper.CanRetryWithSqlException(sqlex))
			{
				return false;
			}
			timeout.Reset();
			this._dbConnectionPoolAuthenticationContextKey = null;
			base.UnDoomThisConnection();
			this._activeDirectoryAuthTimeoutRetryHelper.State = ActiveDirectoryAuthenticationTimeoutRetryState.Retrying;
			return true;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0004AD68 File Offset: 0x00048F68
		private void LoginWithFailover(bool useFailoverHost, ServerInfo primaryServerInfo, string failoverHost, string newPassword, SecureString newSecurePassword, bool redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, bool, string, string>("<sc.SqlInternalConnectionTds.LoginWithFailover|ADV> {0}, useFailover={1}[bool], primary={2}, failover={3}", base.ObjectID, useFailoverHost, primaryServerInfo.UserServerName, failoverHost);
			int num = 100;
			string networkLibrary = base.ConnectionOptions.NetworkLibrary;
			ServerInfo serverInfo = new ServerInfo(connectionOptions, failoverHost, connectionOptions.FailoverPartnerSPN);
			this.ResolveExtendedServerName(primaryServerInfo, !redirectedUserInstance, connectionOptions);
			if (this.ServerProvidedFailOverPartner == null)
			{
				this.ResolveExtendedServerName(serverInfo, !redirectedUserInstance && failoverHost != primaryServerInfo.UserServerName, connectionOptions);
			}
			checked
			{
				long num2;
				if (timeout.IsInfinite)
				{
					num2 = (long)(unchecked(0.08f * (float)ADP.TimerFromSeconds(15)));
				}
				else
				{
					num2 = (long)(unchecked(0.08f * (float)timeout.MillisecondsRemaining));
				}
				bool flag = false;
				int num3 = 0;
				for (;;)
				{
					long num4 = num2 * unchecked((long)(checked(num3 / 2 + 1)));
					long millisecondsRemaining = timeout.MillisecondsRemaining;
					if (num4 > millisecondsRemaining)
					{
						num4 = millisecondsRemaining;
					}
					TimeoutTimer timeoutTimer = TimeoutTimer.StartMillisecondsTimeout(num4);
					if (this._parser != null)
					{
						this._parser.Disconnect();
					}
					this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
					ServerInfo serverInfo2;
					if (useFailoverHost)
					{
						if (!flag)
						{
							this.FailoverPermissionDemand();
							flag = true;
						}
						if (this.ServerProvidedFailOverPartner != null && serverInfo.ResolvedServerName != this.ServerProvidedFailOverPartner)
						{
							SqlClientEventSource.Log.TryAdvancedTraceEvent<int, string>("<sc.SqlInternalConnectionTds.LoginWithFailover|ADV> {0}, new failover partner={1}", base.ObjectID, this.ServerProvidedFailOverPartner);
							serverInfo.SetDerivedNames(networkLibrary, this.ServerProvidedFailOverPartner);
						}
						serverInfo2 = serverInfo;
						this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Failover);
					}
					else
					{
						serverInfo2 = primaryServerInfo;
						this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.Principle);
					}
					unchecked
					{
						try
						{
							this.AttemptOneLogin(serverInfo2, newPassword, newSecurePassword, false, timeoutTimer, true, true, false);
							int num5 = 0;
							while (this._routingInfo != null)
							{
								if (num5 > 10)
								{
									throw SQL.ROR_RecursiveRoutingNotSupported(this);
								}
								num5++;
								SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SqlInternalConnectionTds.LoginWithFailover> Routed to {0}", this._routingInfo.ServerName);
								if (this._parser != null)
								{
									this._parser.Disconnect();
								}
								this._parser = new TdsParser(base.ConnectionOptions.MARS, base.ConnectionOptions.Asynchronous);
								serverInfo2 = new ServerInfo(base.ConnectionOptions, this._routingInfo, serverInfo2.ResolvedServerName, serverInfo2.ServerSPN);
								this.timeoutErrorInternal.SetInternalSourceType(SqlConnectionInternalSourceType.RoutingDestination);
								this._originalClientConnectionId = this._clientConnectionId;
								this._routingDestination = serverInfo2.UserServerName;
								this._currentPacketSize = base.ConnectionOptions.PacketSize;
								this._currentLanguage = (this._originalLanguage = base.ConnectionOptions.CurrentLanguage);
								base.CurrentDatabase = (this._originalDatabase = base.ConnectionOptions.InitialCatalog);
								this._currentFailoverPartner = null;
								this._instanceName = string.Empty;
								this.AttemptOneLogin(serverInfo2, newPassword, newSecurePassword, false, timeoutTimer, true, true, false);
							}
							break;
						}
						catch (SqlException ex)
						{
							if (this.AttemptRetryADAuthWithTimeoutError(ex, connectionOptions, timeout))
							{
								continue;
							}
							if (this.IsDoNotRetryConnectError(ex) || timeout.IsExpired)
							{
								throw;
							}
							if (!ADP.IsAzureSqlServerEndpoint(connectionOptions.DataSource) && base.IsConnectionDoomed)
							{
								throw;
							}
							if (1 == num3 % 2 && timeout.MillisecondsRemaining <= (long)num)
							{
								throw;
							}
						}
						if (1 == num3 % 2)
						{
							SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.SqlInternalConnectionTds.LoginWithFailover|ADV> {0}, sleeping {1}[milisec]", base.ObjectID, num);
							Thread.Sleep(num);
							num = ((num < 500) ? (num * 2) : 1000);
						}
						num3++;
						useFailoverHost = !useFailoverHost;
					}
				}
				this._activeDirectoryAuthTimeoutRetryHelper.State = ActiveDirectoryAuthenticationTimeoutRetryState.HasLoggedIn;
				if (useFailoverHost && this.ServerProvidedFailOverPartner == null)
				{
					throw SQL.InvalidPartnerConfiguration(failoverHost, base.CurrentDatabase);
				}
				if (this.PoolGroupProviderInfo != null)
				{
					this.PoolGroupProviderInfo.FailoverCheck(useFailoverHost, connectionOptions, this.ServerProvidedFailOverPartner);
				}
				base.CurrentDataSource = (useFailoverHost ? failoverHost : primaryServerInfo.UserServerName);
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0004B130 File Offset: 0x00049330
		private void ResolveExtendedServerName(ServerInfo serverInfo, bool aliasLookup, SqlConnectionString options)
		{
			if (serverInfo.ExtendedServerName == null)
			{
				string text = serverInfo.UserServerName;
				string text2 = serverInfo.UserProtocol;
				if (aliasLookup)
				{
					if (this._currentSessionData != null && !string.IsNullOrEmpty(text))
					{
						Tuple<string, string> tuple;
						if (this._currentSessionData._resolvedAliases.TryGetValue(text, out tuple))
						{
							text = tuple.Item1;
							text2 = tuple.Item2;
						}
						else
						{
							TdsParserStaticMethods.AliasRegistryLookup(ref text, ref text2);
							this._currentSessionData._resolvedAliases.Add(serverInfo.UserServerName, new Tuple<string, string>(text, text2));
						}
					}
					else
					{
						TdsParserStaticMethods.AliasRegistryLookup(ref text, ref text2);
					}
					if (options.EnforceLocalHost)
					{
						SqlConnectionString.VerifyLocalHostAndFixup(ref text, true, true);
					}
				}
				serverInfo.SetDerivedNames(text2, text);
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0004B1D8 File Offset: 0x000493D8
		private void AttemptOneLogin(ServerInfo serverInfo, string newPassword, SecureString newSecurePassword, bool ignoreSniOpenTimeout, TimeoutTimer timeout, bool withFailover = false, bool isFirstTransparentAttempt = true, bool disableTnir = false)
		{
			SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long, string>("<sc.SqlInternalConnectionTds.AttemptOneLogin|ADV> {0}, timout={1}[msec], server={2}", base.ObjectID, timeout.MillisecondsRemaining, serverInfo.ExtendedServerName);
			this._routingInfo = null;
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Connect;
			this._parser.Connect(serverInfo, this, ignoreSniOpenTimeout, timeout.LegacyTimerExpire, base.ConnectionOptions, withFailover, isFirstTransparentAttempt, this._serverCallback, this._clientCallback, this._originalNetworkAddressInfo != null, disableTnir);
			this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ConsumePreLoginHandshake);
			this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.LoginBegin);
			this._parser._physicalStateObj.SniContext = SniContext.Snix_Login;
			this.Login(serverInfo, timeout, newPassword, newSecurePassword, base.ConnectionOptions.Encrypt);
			this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.ProcessConnectionAuth);
			this.timeoutErrorInternal.SetAndBeginPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
			this.CompleteLogin(!base.ConnectionOptions.Pooling);
			this.timeoutErrorInternal.EndPhase(SqlConnectionTimeoutErrorPhase.PostLogin);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004B2CD File Offset: 0x000494CD
		internal void FailoverPermissionDemand()
		{
			if (this.PoolGroupProviderInfo != null)
			{
				this.PoolGroupProviderInfo.FailoverPermissionDemand();
			}
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004B2E4 File Offset: 0x000494E4
		protected override object ObtainAdditionalLocksForClose()
		{
			bool flag = !this.ThreadHasParserLockForClose;
			if (flag)
			{
				this._parserLock.Wait(false);
				this.ThreadHasParserLockForClose = true;
			}
			return flag;
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004B317 File Offset: 0x00049517
		protected override void ReleaseAdditionalLocksForClose(object lockToken)
		{
			if ((bool)lockToken)
			{
				this.ThreadHasParserLockForClose = false;
				this._parserLock.Release();
			}
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0004B334 File Offset: 0x00049534
		internal bool GetSessionAndReconnectIfNeeded(SqlConnection parent, int timeout = 0)
		{
			if (this.ThreadHasParserLockForClose)
			{
				return false;
			}
			this._parserLock.Wait(false);
			this.ThreadHasParserLockForClose = true;
			bool releaseConnectionLock = true;
			bool flag;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					Task task = parent.ValidateAndReconnect(delegate
					{
						this.ThreadHasParserLockForClose = false;
						this._parserLock.Release();
						releaseConnectionLock = false;
					}, timeout);
					if (task != null)
					{
						AsyncHelper.WaitForCompletion(task, timeout, null, true);
						flag = true;
					}
					else
					{
						flag = false;
					}
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
					throw;
				}
			}
			finally
			{
				if (releaseConnectionLock)
				{
					this.ThreadHasParserLockForClose = false;
					this._parserLock.Release();
				}
			}
			return flag;
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0004B40C File Offset: 0x0004960C
		internal void BreakConnection()
		{
			SqlConnection connection = base.Connection;
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.BreakConnection|RES|CPOOL> {0}, Breaking connection.", base.ObjectID);
			base.DoomThisConnection();
			if (connection != null)
			{
				connection.Close();
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0004B444 File Offset: 0x00049644
		internal bool IgnoreEnvChange
		{
			get
			{
				return this._routingInfo != null;
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0004B450 File Offset: 0x00049650
		internal void OnEnvChange(SqlEnvChange rec)
		{
			switch (rec._type)
			{
			case 1:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalDatabase = rec._newValue;
				}
				base.CurrentDatabase = rec._newValue;
				return;
			case 2:
				if (!this._fConnectionOpen && this._recoverySessionData == null)
				{
					this._originalLanguage = rec._newValue;
				}
				this._currentLanguage = rec._newValue;
				return;
			case 3:
			case 5:
			case 6:
			case 8:
			case 9:
			case 10:
			case 11:
			case 12:
			case 14:
			case 16:
			case 17:
				break;
			case 4:
				this._currentPacketSize = int.Parse(rec._newValue, CultureInfo.InvariantCulture);
				return;
			case 7:
				if (this._currentSessionData != null)
				{
					this._currentSessionData._collation = rec._newCollation;
					return;
				}
				break;
			case 13:
				this._currentFailoverPartner = rec._newValue;
				return;
			case 15:
			{
				byte[] array;
				if (rec._newBinRented)
				{
					array = new byte[rec._newLength];
					Buffer.BlockCopy(rec._newBinValue, 0, array, 0, array.Length);
				}
				else
				{
					array = rec._newBinValue;
					rec._newBinValue = null;
				}
				base.PromotedDTCToken = array;
				return;
			}
			case 18:
				if (this._currentSessionData != null)
				{
					this._currentSessionData.Reset();
					return;
				}
				break;
			case 19:
				this._instanceName = rec._newValue;
				return;
			case 20:
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnEnvChange|ADV> {0}, Received routing info", base.ObjectID);
				if (string.IsNullOrEmpty(rec._newRoutingInfo.ServerName) || rec._newRoutingInfo.Protocol != 0 || rec._newRoutingInfo.Port == 0)
				{
					throw SQL.ROR_InvalidRoutingInfo(this);
				}
				this._routingInfo = rec._newRoutingInfo;
				break;
			default:
				return;
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0004B604 File Offset: 0x00049804
		internal void OnLoginAck(SqlLoginAck rec)
		{
			this._loginAck = rec;
			if (this._recoverySessionData != null && this._recoverySessionData._tdsVersion != rec.tdsVersion)
			{
				throw SQL.CR_TDSVersionNotPreserved(this);
			}
			if (this._currentSessionData != null)
			{
				this._currentSessionData._tdsVersion = rec.tdsVersion;
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004B654 File Offset: 0x00049854
		internal void OnFedAuthInfo(SqlFedAuthInfo fedAuthInfo)
		{
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFedAuthInfo> {0}, Generating federated authentication token", base.ObjectID);
			DbConnectionPoolAuthenticationContext dbConnectionPoolAuthenticationContext = null;
			bool flag = false;
			bool flag2 = false;
			if (this._dbConnectionPool != null)
			{
				this._dbConnectionPoolAuthenticationContextKey = new DbConnectionPoolAuthenticationContextKey(fedAuthInfo.stsurl, fedAuthInfo.spn);
				if (this._dbConnectionPool.AuthenticationContexts.TryGetValue(this._dbConnectionPoolAuthenticationContextKey, out dbConnectionPoolAuthenticationContext))
				{
					TimeSpan timeSpan = dbConnectionPoolAuthenticationContext.ExpirationTime.Subtract(DateTime.UtcNow);
					if (timeSpan <= SqlInternalConnectionTds._dbAuthenticationContextUnLockedRefreshTimeSpan)
					{
						if (SqlClientEventSource.Log.IsTraceEnabled())
						{
							SqlClientEventSource.Log.TraceEvent<int, string, string>("<sc.SqlInternalConnectionTds.OnFedAuthInfo> {0}, The expiration time is less than 10 mins, so trying to get new access token regardless of if an other thread is also trying to update it.The expiration time is {1}. Current Time is {2}.", base.ObjectID, dbConnectionPoolAuthenticationContext.ExpirationTime.ToLongTimeString(), DateTime.UtcNow.ToLongTimeString());
						}
						flag = true;
					}
					else if (timeSpan <= SqlInternalConnectionTds._dbAuthenticationContextLockedRefreshTimeSpan)
					{
						if (SqlClientEventSource.Log.IsAdvancedTraceOn())
						{
							SqlClientEventSource.Log.AdvancedTraceEvent<int, string, string>("<sc.SqlInternalConnectionTds.OnFedAuthInfo|ADV> {0}, The authentication context needs a refresh.The expiration time is {1}. Current Time is {2}.", base.ObjectID, dbConnectionPoolAuthenticationContext.ExpirationTime.ToLongTimeString(), DateTime.UtcNow.ToLongTimeString());
						}
						flag2 = this.TryGetFedAuthTokenLocked(fedAuthInfo, dbConnectionPoolAuthenticationContext, out this._fedAuthToken);
						if (flag2)
						{
							SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFedAuthInfo> {0}, The attempt to get a new access token succeeded under the locked mode.", base.ObjectID);
						}
					}
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFedAuthInfo> {0}, Found an authentication context in the cache that does not need a refresh at this time. Re-using the cached token.", base.ObjectID);
				}
			}
			if (dbConnectionPoolAuthenticationContext == null || flag)
			{
				this._fedAuthToken = this.GetFedAuthToken(fedAuthInfo);
				if (this._dbConnectionPool != null)
				{
				}
			}
			else if (!flag2)
			{
				this._fedAuthToken = new SqlFedAuthToken();
				this._fedAuthToken.accessToken = dbConnectionPoolAuthenticationContext.AccessToken;
				this._fedAuthToken.expirationFileTime = dbConnectionPoolAuthenticationContext.ExpirationTime.ToFileTime();
			}
			this._parser.SendFedAuthToken(this._fedAuthToken);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0004B810 File Offset: 0x00049A10
		internal bool TryGetFedAuthTokenLocked(SqlFedAuthInfo fedAuthInfo, DbConnectionPoolAuthenticationContext dbConnectionPoolAuthenticationContext, out SqlFedAuthToken fedAuthToken)
		{
			fedAuthToken = null;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (dbConnectionPoolAuthenticationContext.LockToUpdate())
				{
					if (SqlClientEventSource.Log.IsTraceEnabled())
					{
						SqlClientEventSource.Log.TraceEvent<int, string, string>("<sc.SqlInternalConnectionTds.TryGetFedAuthTokenLocked> {0}, Acquired the lock to update the authentication context.The expiration time is {1}. Current Time is {2}.", base.ObjectID, dbConnectionPoolAuthenticationContext.ExpirationTime.ToLongTimeString(), DateTime.UtcNow.ToLongTimeString());
					}
					flag = true;
				}
				else
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.TryGetFedAuthTokenLocked> {0}, Refreshing the context is already in progress by another thread.", base.ObjectID);
				}
				if (flag)
				{
					fedAuthToken = this.GetFedAuthToken(fedAuthInfo);
				}
			}
			finally
			{
				if (flag)
				{
					dbConnectionPoolAuthenticationContext.ReleaseLockToUpdate();
				}
			}
			return flag;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0004B8B0 File Offset: 0x00049AB0
		internal SqlFedAuthToken GetFedAuthToken(SqlFedAuthInfo fedAuthInfo)
		{
			SqlInternalConnectionTds.<>c__DisplayClass159_0 CS$<>8__locals1 = new SqlInternalConnectionTds.<>c__DisplayClass159_0();
			int num = 100;
			int num2 = 0;
			SqlFedAuthToken sqlFedAuthToken = new SqlFedAuthToken();
			string text = null;
			CS$<>8__locals1.authProvider = this._sqlAuthenticationProviderManager.GetProvider(base.ConnectionOptions.Authentication);
			if (CS$<>8__locals1.authProvider == null)
			{
				throw SQL.CannotFindAuthProvider(base.ConnectionOptions.Authentication.ToString());
			}
			while (num2 <= 1 && (long)num <= this._timeout.MillisecondsRemaining)
			{
				num2++;
				try
				{
					SqlInternalConnectionTds.<>c__DisplayClass159_1 CS$<>8__locals2 = new SqlInternalConnectionTds.<>c__DisplayClass159_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.authParamsBuilder = new SqlAuthenticationParameters.Builder(base.ConnectionOptions.Authentication, fedAuthInfo.spn, fedAuthInfo.stsurl, base.ConnectionOptions.DataSource, base.ConnectionOptions.InitialCatalog).WithConnectionId(this._clientConnectionId).WithConnectionTimeout(base.ConnectionOptions.ConnectTimeout);
					switch (base.ConnectionOptions.Authentication)
					{
					case SqlAuthenticationMethod.ActiveDirectoryPassword:
					case SqlAuthenticationMethod.ActiveDirectoryServicePrincipal:
						if (this._activeDirectoryAuthTimeoutRetryHelper.State == ActiveDirectoryAuthenticationTimeoutRetryState.Retrying)
						{
							sqlFedAuthToken = this._activeDirectoryAuthTimeoutRetryHelper.CachedToken;
						}
						else
						{
							if (this._credential != null)
							{
								text = this._credential.UserId;
								CS$<>8__locals2.authParamsBuilder.WithUserId(text).WithPassword(this._credential.Password);
								sqlFedAuthToken = Task.Run<SqlAuthenticationToken>(delegate
								{
									SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__2>d <<GetFedAuthToken>b__2>d;
									<<GetFedAuthToken>b__2>d.<>t__builder = AsyncTaskMethodBuilder<SqlAuthenticationToken>.Create();
									<<GetFedAuthToken>b__2>d.<>4__this = CS$<>8__locals2;
									<<GetFedAuthToken>b__2>d.<>1__state = -1;
									<<GetFedAuthToken>b__2>d.<>t__builder.Start<SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__2>d>(ref <<GetFedAuthToken>b__2>d);
									return <<GetFedAuthToken>b__2>d.<>t__builder.Task;
								}).GetAwaiter().GetResult()
									.ToSqlFedAuthToken();
							}
							else
							{
								text = base.ConnectionOptions.UserID;
								CS$<>8__locals2.authParamsBuilder.WithUserId(text).WithPassword(base.ConnectionOptions.Password);
								sqlFedAuthToken = Task.Run<SqlAuthenticationToken>(delegate
								{
									SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__3>d <<GetFedAuthToken>b__3>d;
									<<GetFedAuthToken>b__3>d.<>t__builder = AsyncTaskMethodBuilder<SqlAuthenticationToken>.Create();
									<<GetFedAuthToken>b__3>d.<>4__this = CS$<>8__locals2;
									<<GetFedAuthToken>b__3>d.<>1__state = -1;
									<<GetFedAuthToken>b__3>d.<>t__builder.Start<SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__3>d>(ref <<GetFedAuthToken>b__3>d);
									return <<GetFedAuthToken>b__3>d.<>t__builder.Task;
								}).GetAwaiter().GetResult()
									.ToSqlFedAuthToken();
							}
							this._activeDirectoryAuthTimeoutRetryHelper.CachedToken = sqlFedAuthToken;
						}
						break;
					case SqlAuthenticationMethod.ActiveDirectoryIntegrated:
						text = "NT Authority\\Anonymous Logon";
						if (this._activeDirectoryAuthTimeoutRetryHelper.State == ActiveDirectoryAuthenticationTimeoutRetryState.Retrying)
						{
							sqlFedAuthToken = this._activeDirectoryAuthTimeoutRetryHelper.CachedToken;
						}
						else
						{
							sqlFedAuthToken = Task.Run<SqlAuthenticationToken>(delegate
							{
								SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__0>d <<GetFedAuthToken>b__0>d;
								<<GetFedAuthToken>b__0>d.<>t__builder = AsyncTaskMethodBuilder<SqlAuthenticationToken>.Create();
								<<GetFedAuthToken>b__0>d.<>4__this = CS$<>8__locals2;
								<<GetFedAuthToken>b__0>d.<>1__state = -1;
								<<GetFedAuthToken>b__0>d.<>t__builder.Start<SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__0>d>(ref <<GetFedAuthToken>b__0>d);
								return <<GetFedAuthToken>b__0>d.<>t__builder.Task;
							}).GetAwaiter().GetResult()
								.ToSqlFedAuthToken();
							this._activeDirectoryAuthTimeoutRetryHelper.CachedToken = sqlFedAuthToken;
						}
						break;
					case SqlAuthenticationMethod.ActiveDirectoryInteractive:
					case SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow:
					case SqlAuthenticationMethod.ActiveDirectoryManagedIdentity:
					case SqlAuthenticationMethod.ActiveDirectoryMSI:
					case SqlAuthenticationMethod.ActiveDirectoryDefault:
						if (this._activeDirectoryAuthTimeoutRetryHelper.State == ActiveDirectoryAuthenticationTimeoutRetryState.Retrying)
						{
							sqlFedAuthToken = this._activeDirectoryAuthTimeoutRetryHelper.CachedToken;
						}
						else
						{
							CS$<>8__locals2.authParamsBuilder.WithUserId(base.ConnectionOptions.UserID);
							sqlFedAuthToken = Task.Run<SqlAuthenticationToken>(delegate
							{
								SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__1>d <<GetFedAuthToken>b__1>d;
								<<GetFedAuthToken>b__1>d.<>t__builder = AsyncTaskMethodBuilder<SqlAuthenticationToken>.Create();
								<<GetFedAuthToken>b__1>d.<>4__this = CS$<>8__locals2;
								<<GetFedAuthToken>b__1>d.<>1__state = -1;
								<<GetFedAuthToken>b__1>d.<>t__builder.Start<SqlInternalConnectionTds.<>c__DisplayClass159_1.<<GetFedAuthToken>b__1>d>(ref <<GetFedAuthToken>b__1>d);
								return <<GetFedAuthToken>b__1>d.<>t__builder.Task;
							}).GetAwaiter().GetResult()
								.ToSqlFedAuthToken();
							this._activeDirectoryAuthTimeoutRetryHelper.CachedToken = sqlFedAuthToken;
						}
						break;
					default:
						throw SQL.UnsupportedAuthenticationSpecified(base.ConnectionOptions.Authentication);
					}
					break;
				}
				catch (MsalServiceException ex)
				{
					if (ex.StatusCode != 429)
					{
						SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SqlInternalConnectionTds.GetFedAuthToken.MsalServiceException error:> {0}", ex.ErrorCode);
						throw ADP.CreateSqlException(ex, base.ConnectionOptions, this, text);
					}
					RetryConditionHeaderValue retryAfter = ex.Headers.RetryAfter;
					if (retryAfter.Delta != null)
					{
						num = retryAfter.Delta.Value.Milliseconds;
					}
					else if (retryAfter.Date != null)
					{
						num = Convert.ToInt32(retryAfter.Date.Value.Offset.TotalMilliseconds);
					}
					if ((long)num >= this._timeout.MillisecondsRemaining)
					{
						SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SqlInternalConnectionTds.GetFedAuthToken.MsalServiceException error:> Timeout: {0}", ex.ErrorCode);
						throw SQL.ActiveDirectoryTokenRetrievingTimeout(Enum.GetName(typeof(SqlAuthenticationMethod), base.ConnectionOptions.Authentication), ex.ErrorCode, ex);
					}
					Thread.Sleep(num);
				}
				catch (MsalException ex2)
				{
					if ("unknown_error" != ex2.ErrorCode || this._timeout.IsExpired || this._timeout.MillisecondsRemaining <= (long)num)
					{
						SqlClientEventSource.Log.TryTraceEvent<string>("<sc.SqlInternalConnectionTds.GetFedAuthToken.MSALException error:> {0}", ex2.ErrorCode);
						throw ADP.CreateSqlException(ex2, base.ConnectionOptions, this, text);
					}
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, int>("<sc.SqlInternalConnectionTds.GetFedAuthToken|ADV> {0}, sleeping {1}[Milliseconds]", base.ObjectID, num);
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int, long>("<sc.SqlInternalConnectionTds.GetFedAuthToken|ADV> {0}, remaining {1}[Milliseconds]", base.ObjectID, this._timeout.MillisecondsRemaining);
					Thread.Sleep(num);
					num *= 2;
				}
				catch (Exception ex3)
				{
					throw SqlException.CreateException(new SqlErrorCollection
					{
						new SqlError(0, 0, 20, base.ConnectionOptions.DataSource, ex3.Message, "AcquireToken", 0, null)
					}, "", this, ex3);
				}
			}
			if (this._dbConnectionPool != null)
			{
				DateTime dateTime = DateTime.FromFileTimeUtc(sqlFedAuthToken.expirationFileTime);
				this._newDbConnectionPoolAuthenticationContext = new DbConnectionPoolAuthenticationContext(sqlFedAuthToken.accessToken, dateTime);
			}
			SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.GetFedAuthToken> {0}, Finished generating federated authentication token.", base.ObjectID);
			return sqlFedAuthToken;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0004BE00 File Offset: 0x0004A000
		internal void OnFeatureExtAck(int featureId, byte[] data)
		{
			if (this._routingInfo != null && 11 != featureId)
			{
				return;
			}
			switch (featureId)
			{
			case 1:
			{
				if (!this._sessionRecoveryRequested)
				{
					throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnrequestedFeatureAckReceived, featureId);
				}
				this._sessionRecoveryAcknowledged = true;
				int i = 0;
				while (i < data.Length)
				{
					byte b = data[i];
					i++;
					byte b2 = data[i];
					i++;
					int num;
					if (b2 == 255)
					{
						num = BitConverter.ToInt32(data, i);
						i += 4;
					}
					else
					{
						num = (int)b2;
					}
					byte[] array = new byte[num];
					Buffer.BlockCopy(data, i, array, 0, num);
					i += num;
					if (this._recoverySessionData == null)
					{
						this._currentSessionData._initialState[(int)b] = array;
					}
					else
					{
						this._currentSessionData._delta[(int)b] = new SessionStateRecord
						{
							_data = array,
							_dataLength = num,
							_recoverable = true,
							_version = 0U
						};
						this._currentSessionData._deltaDirty = true;
					}
				}
				return;
			}
			case 2:
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for federated authentication", base.ObjectID);
				if (!this._federatedAuthenticationRequested)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Did not request federated authentication", base.ObjectID);
					throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnrequestedFeatureAckReceived, featureId);
				}
				TdsEnums.FedAuthLibrary libraryType = this._fedAuthFeatureExtensionData.libraryType;
				if (libraryType - TdsEnums.FedAuthLibrary.SecurityToken > 1)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Attempting to use unknown federated authentication library", base.ObjectID);
					throw SQL.ParsingErrorLibraryType(ParsingErrorState.FedAuthFeatureAckUnknownLibraryType, (int)this._fedAuthFeatureExtensionData.libraryType);
				}
				if (data.Length != 0)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Federated authentication feature extension ack for MSAL and Security Token includes extra data", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.FedAuthFeatureAckContainsExtraData);
				}
				this._federatedAuthenticationAcknowledged = true;
				if (this._newDbConnectionPoolAuthenticationContext != null)
				{
					DbConnectionPoolAuthenticationContext dbConnectionPoolAuthenticationContext = this._dbConnectionPool.AuthenticationContexts.AddOrUpdate(this._dbConnectionPoolAuthenticationContextKey, this._newDbConnectionPoolAuthenticationContext, (DbConnectionPoolAuthenticationContextKey key, DbConnectionPoolAuthenticationContext oldValue) => DbConnectionPoolAuthenticationContext.ChooseAuthenticationContextToUpdate(oldValue, this._newDbConnectionPoolAuthenticationContext));
					return;
				}
				return;
			}
			case 4:
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for TCE", base.ObjectID);
				if (data.Length < 1)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Unknown version number for TCE", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.TceUnknownVersion);
				}
				byte b3 = data[0];
				if (b3 == 0 || b3 > 3)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Invalid version number for TCE", base.ObjectID);
					throw SQL.ParsingErrorValue(ParsingErrorState.TceInvalidVersion, (int)b3);
				}
				this._tceVersionSupported = b3;
				this._parser.IsColumnEncryptionSupported = true;
				this._parser.TceVersionSupported = this._tceVersionSupported;
				this._parser.AreEnclaveRetriesSupported = this._tceVersionSupported == 3;
				if (data.Length > 1)
				{
					this._parser.EnclaveType = Encoding.Unicode.GetString(data, 2, data.Length - 2);
					return;
				}
				return;
			}
			case 5:
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for GlobalTransactions", base.ObjectID);
				if (data.Length < 1)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Unknown version number for GlobalTransactions", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				base.IsGlobalTransaction = true;
				if (1 == data[0])
				{
					base.IsGlobalTransactionsEnabledForServer = true;
					return;
				}
				return;
			case 8:
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for AzureSQLSupport", base.ObjectID);
				if (data.Length < 1)
				{
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				base.IsAzureSQLConnection = true;
				if ((data[0] & 1) == 1 && SqlClientEventSource.Log.IsTraceEnabled())
				{
					SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, FailoverPartner enabled with Readonly intent for AzureSQL DB", base.ObjectID);
					return;
				}
				return;
			case 9:
			{
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for DATACLASSIFICATION", base.ObjectID);
				if (data.Length < 1)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Unknown token for DATACLASSIFICATION", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				byte b4 = data[0];
				if (b4 == 0 || b4 > 2)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Invalid version number for DATACLASSIFICATION", base.ObjectID);
					throw SQL.ParsingErrorValue(ParsingErrorState.DataClassificationInvalidVersion, (int)b4);
				}
				if (data.Length != 2)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Unknown token for DATACLASSIFICATION", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				byte b5 = data[1];
				this._parser.DataClassificationVersion = (int)((b5 == 0) ? 0 : b4);
				return;
			}
			case 10:
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for UTF8 support", base.ObjectID);
				if (data.Length < 1)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Unknown value for UTF8 support", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				return;
			case 11:
				SqlClientEventSource.Log.TryAdvancedTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ADV> {0}, Received feature extension acknowledgement for SQLDNSCACHING", base.ObjectID);
				if (data.Length < 1)
				{
					SqlClientEventSource.Log.TryTraceEvent<int>("<sc.SqlInternalConnectionTds.OnFeatureExtAck|ERR> {0}, Unknown token for SQLDNSCACHING", base.ObjectID);
					throw SQL.ParsingError(ParsingErrorState.CorruptedTdsStream);
				}
				if (1 != data[0])
				{
					this.IsSQLDNSCachingSupported = false;
					this._cleanSQLDNSCaching = true;
					return;
				}
				this.IsSQLDNSCachingSupported = true;
				this._cleanSQLDNSCaching = false;
				if (this._routingInfo != null)
				{
					this.IsDNSCachingBeforeRedirectSupported = true;
					return;
				}
				return;
			}
			throw SQL.ParsingErrorFeatureId(ParsingErrorState.UnknownFeatureAck, featureId);
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0004C2A4 File Offset: 0x0004A4A4
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x0004C2B8 File Offset: 0x0004A4B8
		internal bool ThreadHasParserLockForClose
		{
			get
			{
				return this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId;
			}
			set
			{
				if (value)
				{
					this._threadIdOwningParserLock = Thread.CurrentThread.ManagedThreadId;
					return;
				}
				if (this._threadIdOwningParserLock == Thread.CurrentThread.ManagedThreadId)
				{
					this._threadIdOwningParserLock = -1;
				}
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0004C2E7 File Offset: 0x0004A4E7
		internal override bool TryReplaceConnection(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions)
		{
			return base.TryOpenConnectionInternal(outerConnection, connectionFactory, retry, userOptions);
		}

		// Token: 0x04000798 RID: 1944
		internal const int MsalHttpRetryStatusCode = 429;

		// Token: 0x04000799 RID: 1945
		internal const int _maxNumberOfRedirectRoute = 10;

		// Token: 0x0400079A RID: 1946
		private readonly SqlConnectionPoolGroupProviderInfo _poolGroupProviderInfo;

		// Token: 0x0400079B RID: 1947
		private TdsParser _parser;

		// Token: 0x0400079C RID: 1948
		private SqlLoginAck _loginAck;

		// Token: 0x0400079D RID: 1949
		private SqlCredential _credential;

		// Token: 0x0400079E RID: 1950
		private FederatedAuthenticationFeatureExtensionData _fedAuthFeatureExtensionData;

		// Token: 0x0400079F RID: 1951
		private bool _sessionRecoveryRequested;

		// Token: 0x040007A0 RID: 1952
		internal bool _sessionRecoveryAcknowledged;

		// Token: 0x040007A1 RID: 1953
		internal SessionData _currentSessionData;

		// Token: 0x040007A2 RID: 1954
		private SessionData _recoverySessionData;

		// Token: 0x040007A3 RID: 1955
		internal bool _fedAuthRequired;

		// Token: 0x040007A4 RID: 1956
		internal bool _federatedAuthenticationRequested;

		// Token: 0x040007A5 RID: 1957
		internal bool _federatedAuthenticationAcknowledged;

		// Token: 0x040007A6 RID: 1958
		internal bool _federatedAuthenticationInfoRequested;

		// Token: 0x040007A7 RID: 1959
		internal bool _federatedAuthenticationInfoReceived;

		// Token: 0x040007A8 RID: 1960
		private SqlFedAuthToken _fedAuthToken;

		// Token: 0x040007A9 RID: 1961
		internal byte[] _accessTokenInBytes;

		// Token: 0x040007AA RID: 1962
		private readonly ActiveDirectoryAuthenticationTimeoutRetryHelper _activeDirectoryAuthTimeoutRetryHelper;

		// Token: 0x040007AB RID: 1963
		private readonly SqlAuthenticationProviderManager _sqlAuthenticationProviderManager;

		// Token: 0x040007AC RID: 1964
		private ServerCertificateValidationCallback _serverCallback;

		// Token: 0x040007AD RID: 1965
		private ClientCertificateRetrievalCallback _clientCallback;

		// Token: 0x040007AE RID: 1966
		private SqlClientOriginalNetworkAddressInfo _originalNetworkAddressInfo;

		// Token: 0x040007AF RID: 1967
		internal bool _cleanSQLDNSCaching;

		// Token: 0x040007B0 RID: 1968
		private bool _serverSupportsDNSCaching;

		// Token: 0x040007B1 RID: 1969
		private bool _SQLDNSRetryEnabled;

		// Token: 0x040007B2 RID: 1970
		private bool DNSCachingBeforeRedirect;

		// Token: 0x040007B3 RID: 1971
		internal SQLDNSInfo pendingSQLDNSObject;

		// Token: 0x040007B4 RID: 1972
		internal byte _tceVersionSupported;

		// Token: 0x040007B5 RID: 1973
		private DbConnectionPool _dbConnectionPool;

		// Token: 0x040007B6 RID: 1974
		private DbConnectionPoolAuthenticationContext _newDbConnectionPoolAuthenticationContext;

		// Token: 0x040007B7 RID: 1975
		private DbConnectionPoolAuthenticationContextKey _dbConnectionPoolAuthenticationContextKey;

		// Token: 0x040007B8 RID: 1976
		private static readonly TimeSpan _dbAuthenticationContextLockedRefreshTimeSpan = new TimeSpan(0, 45, 0);

		// Token: 0x040007B9 RID: 1977
		private static readonly TimeSpan _dbAuthenticationContextUnLockedRefreshTimeSpan = new TimeSpan(0, 10, 0);

		// Token: 0x040007BA RID: 1978
		private readonly TimeoutTimer _timeout;

		// Token: 0x040007BB RID: 1979
		private static HashSet<int> transientErrors = new HashSet<int>();

		// Token: 0x040007BC RID: 1980
		private bool _fConnectionOpen;

		// Token: 0x040007BD RID: 1981
		private bool _fResetConnection;

		// Token: 0x040007BE RID: 1982
		private string _originalDatabase;

		// Token: 0x040007BF RID: 1983
		private string _currentFailoverPartner;

		// Token: 0x040007C0 RID: 1984
		private string _originalLanguage;

		// Token: 0x040007C1 RID: 1985
		private string _currentLanguage;

		// Token: 0x040007C2 RID: 1986
		private int _currentPacketSize;

		// Token: 0x040007C3 RID: 1987
		private int _asyncCommandCount;

		// Token: 0x040007C4 RID: 1988
		private string _instanceName = string.Empty;

		// Token: 0x040007C5 RID: 1989
		private DbConnectionPoolIdentity _identity;

		// Token: 0x040007C6 RID: 1990
		internal SqlInternalConnectionTds.SyncAsyncLock _parserLock = new SqlInternalConnectionTds.SyncAsyncLock();

		// Token: 0x040007C7 RID: 1991
		private int _threadIdOwningParserLock = -1;

		// Token: 0x040007C8 RID: 1992
		private SqlConnectionTimeoutErrorInternal timeoutErrorInternal;

		// Token: 0x040007C9 RID: 1993
		internal Guid _clientConnectionId = Guid.Empty;

		// Token: 0x040007CA RID: 1994
		private RoutingInfo _routingInfo;

		// Token: 0x040007CB RID: 1995
		private Guid _originalClientConnectionId = Guid.Empty;

		// Token: 0x040007CC RID: 1996
		private string _routingDestination;

		// Token: 0x02000245 RID: 581
		internal class SyncAsyncLock
		{
			// Token: 0x06001EBC RID: 7868 RVA: 0x0007DA70 File Offset: 0x0007BC70
			internal void Wait(bool canReleaseFromAnyThread)
			{
				Monitor.Enter(this.semaphore);
				if (canReleaseFromAnyThread || this.semaphore.CurrentCount == 0)
				{
					this.semaphore.Wait();
					if (canReleaseFromAnyThread)
					{
						Monitor.Exit(this.semaphore);
						return;
					}
					this.semaphore.Release();
				}
			}

			// Token: 0x06001EBD RID: 7869 RVA: 0x0007DAC0 File Offset: 0x0007BCC0
			internal void Wait(bool canReleaseFromAnyThread, int timeout, ref bool lockTaken)
			{
				lockTaken = false;
				bool flag = false;
				try
				{
					Monitor.TryEnter(this.semaphore, timeout, ref flag);
					if (flag)
					{
						if (canReleaseFromAnyThread || this.semaphore.CurrentCount == 0)
						{
							if (this.semaphore.Wait(timeout))
							{
								if (canReleaseFromAnyThread)
								{
									Monitor.Exit(this.semaphore);
									flag = false;
								}
								else
								{
									this.semaphore.Release();
								}
								lockTaken = true;
							}
						}
						else
						{
							lockTaken = true;
						}
					}
				}
				finally
				{
					if (!lockTaken && flag)
					{
						Monitor.Exit(this.semaphore);
					}
				}
			}

			// Token: 0x06001EBE RID: 7870 RVA: 0x0007DB50 File Offset: 0x0007BD50
			internal void Release()
			{
				if (this.semaphore.CurrentCount == 0)
				{
					this.semaphore.Release();
					return;
				}
				Monitor.Exit(this.semaphore);
			}

			// Token: 0x17000A3F RID: 2623
			// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0007DB77 File Offset: 0x0007BD77
			internal bool CanBeReleasedFromAnyThread
			{
				get
				{
					return this.semaphore.CurrentCount == 0;
				}
			}

			// Token: 0x06001EC0 RID: 7872 RVA: 0x0007DB87 File Offset: 0x0007BD87
			internal bool ThreadMayHaveLock()
			{
				return Monitor.IsEntered(this.semaphore) || this.semaphore.CurrentCount == 0;
			}

			// Token: 0x04001674 RID: 5748
			private SemaphoreSlim semaphore = new SemaphoreSlim(1);
		}
	}
}
