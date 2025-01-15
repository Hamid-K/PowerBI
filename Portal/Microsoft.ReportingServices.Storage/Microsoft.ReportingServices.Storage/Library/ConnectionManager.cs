using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000010 RID: 16
	internal class ConnectionManager : IRSConnectionManager
	{
		// Token: 0x06000097 RID: 151 RVA: 0x0000533D File Offset: 0x0000353D
		public static ConnectionManager Create(ConnectionTransactionType transactionType, IsolationLevel defaultIsolationLevel)
		{
			return new MultithreadedConnectionManager(transactionType, defaultIsolationLevel);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00005346 File Offset: 0x00003546
		public static int SqlCommandTimeoutSeconds
		{
			get
			{
				return Globals.Configuration.DBQueryTimeout;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005352 File Offset: 0x00003552
		public static int SqlCleanupTimeoutSeconds
		{
			get
			{
				return Globals.Configuration.DBCleanupTimeout;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000535E File Offset: 0x0000355E
		public static ConnectionTransactionType DefaultTransactionType
		{
			get
			{
				return ConnectionTransactionType.Explicit;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00005361 File Offset: 0x00003561
		public static IsolationLevel DefaultIsolationLevel
		{
			get
			{
				return IsolationLevel.ReadCommitted;
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005368 File Offset: 0x00003568
		private static void ChangeConfigHandler(object source, EventArgs args)
		{
			object configAccessSync = ConnectionManager._configAccessSync;
			lock (configAccessSync)
			{
				ConnectionManager._StaticConfig = null;
				ConnectionManager._edition = SqlServerSkuType.None;
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000053B0 File Offset: 0x000035B0
		public ConnectionManager()
		{
			this._config = ConnectionManager._StaticConfig;
			if (this._config == null)
			{
				object configAccessSync = ConnectionManager._configAccessSync;
				lock (configAccessSync)
				{
					this._config = ConnectionManager._StaticConfig;
					if (this._config == null)
					{
						if (!ConnectionManager._ChangeHandlerSet)
						{
							ConnectionManager._ChangeHandlerSet = true;
						}
						this._config = new ConnectionConfig();
						ConnectionManager._StaticConfig = this._config;
					}
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000545C File Offset: 0x0000365C
		public ConnectionManager(ConnectionTransactionType transactionType, IsolationLevel defaultIsolationLevel)
			: this()
		{
			this._transactionType = transactionType;
			this._defaultIsolationLevel = defaultIsolationLevel;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005472 File Offset: 0x00003672
		public IsolationLevel GetIsolationLevel()
		{
			return this._defaultIsolationLevel;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000547A File Offset: 0x0000367A
		public ConnectionTransactionType GetTransactionType()
		{
			return this.ConnectionTransactionType;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005482 File Offset: 0x00003682
		public static ConnectionManager CreateBatchConnection()
		{
			return new ConnectionManager
			{
				_isBatchScopeTransaction = true
			};
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00005490 File Offset: 0x00003690
		public static void Init()
		{
			if (!SymmetricKeyEncryption.IsInitialized)
			{
				ConnectionManager connectionManager = null;
				try
				{
					connectionManager = new ConnectionManager();
					connectionManager.WillDisconnectStorage();
					connectionManager.VerifyConnection(true);
				}
				finally
				{
					if (connectionManager != null)
					{
						connectionManager.DisconnectStorage();
					}
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000054D8 File Offset: 0x000036D8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public SqlConnection Connection
		{
			get
			{
				this.VerifyConnection(true);
				return this._connection;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000054E7 File Offset: 0x000036E7
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public SqlConnection UnverifiedConnection
		{
			get
			{
				if (this._connection == null)
				{
					this.PerformVerificationChecks();
					if (this.ConnectionString == null)
					{
						throw new InternalCatalogException("Storage does not contain a connnection string");
					}
					this.OpenConnection();
				}
				return this._connection;
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005518 File Offset: 0x00003718
		public void ChangeDatabase(string database)
		{
			RSTrace.CatalogTrace.Assert(!string.IsNullOrEmpty(database), "!String.IsNullOrEmpty(database)");
			try
			{
				if (!string.Equals(this.Connection.Database, database, StringComparison.OrdinalIgnoreCase))
				{
					this.Connection.ChangeDatabase(database);
				}
			}
			catch (Exception ex)
			{
				Storage.WrapAndThrowKnownExceptionTypes(ex);
				throw;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005578 File Offset: 0x00003778
		public SqlConnection GetUnverifiedConnection()
		{
			this.OpenConnection();
			return this._connection;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005586 File Offset: 0x00003786
		public bool IsSupportedEditionForSharePoint()
		{
			this.EnsureCorrectEdition();
			return Sku.IsFeatureEnabled(Globals.Configuration.InstanceID, RestrictedFeatures.Sharepoint);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000559F File Offset: 0x0000379F
		public SqlTransaction Transaction
		{
			get
			{
				return this._Transaction;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000055A7 File Offset: 0x000037A7
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000055AF File Offset: 0x000037AF
		public ConnectionTransactionType ConnectionTransactionType
		{
			get
			{
				return this._transactionType;
			}
			set
			{
				this._transactionType = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000055B8 File Offset: 0x000037B8
		public bool IsBatchScoped
		{
			get
			{
				return this._isBatchScopeTransaction;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000055C0 File Offset: 0x000037C0
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000055C8 File Offset: 0x000037C8
		public bool SingleCommitEnabled
		{
			get
			{
				return this._singleCommit;
			}
			set
			{
				if (value)
				{
					RSTrace.CatalogTrace.Assert(this.ConnectionTransactionType == ConnectionTransactionType.Explicit, "ConnectionTransactionType");
				}
				this._singleCommit = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000055EC File Offset: 0x000037EC
		public static string ReportingServicesVersionNumber
		{
			get
			{
				if (ConnectionManager._rsBuildNumber == null)
				{
					ConnectionManager._rsBuildNumber = UpgradeScripts.ServerProductVersion;
				}
				return ConnectionManager._rsBuildNumber;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005604 File Offset: 0x00003804
		public virtual IDisposable EnterThreadSafeContext()
		{
			return null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00005607 File Offset: 0x00003807
		public void WillDisconnectStorage()
		{
			this._willDisconnectStorage = true;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00005610 File Offset: 0x00003810
		public void DisconnectStorage()
		{
			try
			{
				using (this.EnterThreadSafeContext())
				{
					if (this._connection != null)
					{
						try
						{
							this._connection.Close();
						}
						catch (Exception ex)
						{
							Storage.WrapAndThrowKnownExceptionTypes(ex);
							throw;
						}
						if (this._connectionWasOpened)
						{
							this._connectionWasOpened = false;
						}
						this._connection = null;
					}
				}
			}
			catch (Exception ex2)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Connection close failed. Exception thrown: {0}", new object[] { ex2.ToString() });
				throw;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000056B0 File Offset: 0x000038B0
		public void VerifyConnection(bool initializeEncryption = true)
		{
			if (this._connection != null)
			{
				return;
			}
			this.PerformVerificationChecks();
			if (this.ConnectionString == null)
			{
				throw new InternalCatalogException("Storage does not contain a connnection string");
			}
			this.OpenConnection();
			this.EnsureCorrectEdition();
			if (initializeEncryption)
			{
				this.InitEncryption();
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000056E9 File Offset: 0x000038E9
		public void VerifyConnectionAndDbVersion()
		{
			if (this._connection != null)
			{
				return;
			}
			this.PerformVerificationChecks();
			if (this.ConnectionString == null)
			{
				throw new InternalCatalogException("Storage does not contain a connnection string");
			}
			this.OpenConnection();
			this.EnsureCorrectEdition();
			this.InitEncryption();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00005720 File Offset: 0x00003920
		private void InitEncryption()
		{
			if (SymmetricKeyEncryption.IsInitialized)
			{
				return;
			}
			using (new UpgradeMutex())
			{
				RSEventLog.Current.ResetWriteOnceEvent(Event.CouldNotCommunicateToCatalog);
				RevertImpersonationContext.Run(delegate
				{
					SymmetricKeyEncryption.AcquireWriterLock();
					try
					{
						if (!SymmetricKeyEncryption.IsInitialized)
						{
							if (RSTrace.CryptoTrace.TraceInfo)
							{
								RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Initializing crypto as user: {0}", new object[] { UserUtil.GetWindowsIdentityName() });
							}
							KeyStorage keyStorage = new KeyStorage(this);
							this.BeginTransaction(IsolationLevel.ReadCommitted);
							if (RSTrace.CryptoTrace.TraceInfo)
							{
								RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Exporting public key");
							}
							byte[] publicKey = SymmetricKeyEncryption.GetPublicKey();
							this.GetSymmetricKey(keyStorage, Globals.Configuration.InstallationID, publicKey);
							RSEventLog.Current.ResetWriteOnceEvent(Event.NotActivated);
							RSEventLog.Current.ResetWriteOnceEvent(Event.IsDisabled);
							this.CommitTransaction();
						}
					}
					catch
					{
						this.DisconnectStorage();
						throw;
					}
					finally
					{
						SymmetricKeyEncryption.ReleaseWriterLock();
					}
				});
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00005774 File Offset: 0x00003974
		private void GetSymmetricKey(KeyStorage storage, Guid installationID, byte[] publicKey)
		{
			byte[] array;
			int num = storage.AnnouncePublicKeyOrGetSymmetricKey(installationID, publicKey, out array);
			if (array != null)
			{
				storage.ValidateScaleoutEdition();
				this.SetNewSymmetricKey(installationID, array, publicKey, storage);
				return;
			}
			if (num == 0)
			{
				if (RSTrace.CryptoTrace.TraceInfo)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Info, "NT Service self activating");
				}
				byte[] array2 = SymmetricKeyEncryption.CreateSymmetricKey();
				if (array2 != null)
				{
					storage.SetKeysForInstallation(installationID, array2, publicKey);
				}
				else
				{
					RSTrace.CryptoTrace.Assert(false, "NT Service is self activating but it already has a symmetric key");
				}
				RSEventLog.Current.WriteInformation(Event.ActivationSuccessful, Array.Empty<object>());
				return;
			}
			if (RSTrace.CryptoTrace.TraceError)
			{
				RSTrace.CryptoTrace.Trace(TraceLevel.Info, "NT Service not activated: can be added to scale out group with config tool");
			}
			this.CommitTransaction();
			throw new CannotValidateEncryptedDataException();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000581C File Offset: 0x00003A1C
		private void SetNewSymmetricKey(Guid installationID, byte[] symmetricKey, byte[] publicKey, KeyStorage storage)
		{
			try
			{
				if (RSTrace.CryptoTrace.TraceInfo)
				{
					RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Importing existing encryption key");
				}
				SymmetricKeyEncryption.SetSymmetricKeyFromPublicKeyEncryptedBlob(symmetricKey);
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2146893819)
				{
					try
					{
						if (RSTrace.CryptoTrace.TraceInfo)
						{
							RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Importing existing encryption key using 1024 bit key");
						}
						byte[] array = SymmetricKeyEncryption.ReencryptSymmetricKey(symmetricKey, publicKey);
						if (array == null)
						{
							RSTrace.CryptoTrace.Assert(false, "NT Service is self activating using 1024 bit key but it already has a symmetric key");
						}
						if (RSTrace.CryptoTrace.TraceInfo)
						{
							RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Updating symmetric key in Keys table");
						}
						storage.SetKeysForInstallation(installationID, array, publicKey);
						this.CommitTransaction();
						if (RSTrace.CryptoTrace.TraceInfo)
						{
							RSTrace.CryptoTrace.Trace(TraceLevel.Info, "Keys table updated. Now importing symmetric key");
						}
						SymmetricKeyEncryption.SetPublicKeyEncryptedSymmetricKey(array);
						goto IL_00CE;
					}
					catch (COMException ex2)
					{
						throw new CannotValidateEncryptedDataException(ex2);
					}
					goto IL_00C7;
					IL_00CE:
					return;
				}
				IL_00C7:
				throw new CannotValidateEncryptedDataException(ex);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00005918 File Offset: 0x00003B18
		public void EnsureDBCmptLevel()
		{
			try
			{
				using (IDisposable disposable = this.EnterThreadSafeContext())
				{
					using (InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand(string.Format(CultureInfo.InvariantCulture, this.dbcmptScriptsTemplate, this.EscapeAndBracketDBName(this.Connection.Database), this.EscapeAndBracketDBName(this.Connection.Database + "TempDB")), this._connection), disposable))
					{
						instrumentedSqlCommand.CommandType = CommandType.Text;
						instrumentedSqlCommand.CommandTimeout = ConnectionManager.SqlCommandTimeoutSeconds;
						instrumentedSqlCommand.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Failed set DB compatibility level: ", new object[] { ex.ToString() });
				throw;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000059F8 File Offset: 0x00003BF8
		public string EscapeAndBracketDBName(string dbName)
		{
			return "[" + dbName.Replace("]", "]]") + "]";
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00005A1C File Offset: 0x00003C1C
		public void BeginTransaction(IsolationLevel isoLevel)
		{
			using (this.EnterThreadSafeContext())
			{
				if (this._transactionType != ConnectionTransactionType.AutoCommit)
				{
					RSTrace.CatalogTrace.Assert(this._Transaction == null, "Transaction is already open");
					try
					{
						this._Transaction = this.Connection.BeginTransaction(isoLevel);
						if (RSTrace.CatalogTrace.TraceVerbose)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Transaction begin.");
						}
					}
					catch (Exception ex)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Transaction begin failed. Exception thrown: {0}", new object[] { ex.ToString() });
						Storage.WrapAndThrowKnownExceptionTypes(ex);
						throw;
					}
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005AD4 File Offset: 0x00003CD4
		public void BeginTransaction()
		{
			this.BeginTransaction(this._defaultIsolationLevel);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public void CommitTransaction()
		{
			using (this.EnterThreadSafeContext())
			{
				this.CheckForSingleCommit();
				if (this._transactionType != ConnectionTransactionType.AutoCommit)
				{
					if (this._Transaction != null)
					{
						try
						{
							using (new MeasureSql())
							{
								try
								{
									this._Transaction.Commit();
								}
								catch (Exception ex)
								{
									Storage.WrapAndThrowKnownExceptionTypes(ex);
									throw;
								}
							}
							if (RSTrace.CatalogTrace.TraceVerbose)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Transaction commit.");
							}
						}
						catch (Exception ex2)
						{
							RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Transaction commit failed. Exception thrown: {0}", new object[] { ex2.ToString() });
							throw;
						}
						finally
						{
							try
							{
								this._Transaction.Dispose();
							}
							catch (Exception ex3)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Transaction dispose failed. Exception thrown: {0}", new object[] { ex3.ToString() });
							}
							finally
							{
								this._Transaction = null;
								this._performedCommitOrRollback = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00005C24 File Offset: 0x00003E24
		public void AbortTransaction()
		{
			using (this.EnterThreadSafeContext())
			{
				this.CheckForSingleCommit();
				if (this._transactionType != ConnectionTransactionType.AutoCommit)
				{
					if (this._Transaction != null)
					{
						try
						{
							if (this._Transaction.Connection != null && this._Transaction.Connection.State != ConnectionState.Closed && this._Transaction.Connection.State != ConnectionState.Broken)
							{
								using (new MeasureSql())
								{
									try
									{
										this._Transaction.Rollback();
										this._performedCommitOrRollback = true;
									}
									catch (Exception ex)
									{
										Storage.WrapAndThrowKnownExceptionTypes(ex);
										throw;
									}
									goto IL_00A2;
								}
							}
							if (RSTrace.CatalogTrace.TraceWarning)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Warning, "Transaction rollback was not executed connection is invalid");
							}
							IL_00A2:
							if (RSTrace.CatalogTrace.TraceVerbose)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Verbose, "Transaction rollback.");
							}
							this._Transaction = null;
						}
						catch (ReportServerStorageException ex2)
						{
							if (!ex2.IsSqlException || ex2.SqlErrorNumber != Native.SqlTransactionAbortedCode)
							{
								RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Transaction rollback failed. Exception thrown: {0}", new object[] { ex2.ToString() });
								throw;
							}
							this._Transaction = null;
						}
					}
				}
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005DA8 File Offset: 0x00003FA8
		protected virtual void PerformVerificationChecks()
		{
			if (!this._willDisconnectStorage)
			{
				throw new InternalCatalogException("Connection access outside guarded area.");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005DBD File Offset: 0x00003FBD
		private string ConnectionString
		{
			get
			{
				return this._config.ConnectionString.ToString();
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00005DCF File Offset: 0x00003FCF
		private bool ShouldImpersonateCatUser
		{
			get
			{
				return Globals.Configuration.ConnectionAuth == RSBaseConfiguration.CatalogConnectionAuth.Windows && Globals.Configuration.ConnectionType == RSBaseConfiguration.CatalogConnectionType.Impersonate;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005DEC File Offset: 0x00003FEC
		private void OpenConnection()
		{
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = null;
				try
				{
					WindowsIdentity current = WindowsIdentity.GetCurrent(true);
					if (current != null && current.Token != IntPtr.Zero)
					{
						windowsImpersonationContext = WindowsIdentity.Impersonate(IntPtr.Zero);
					}
					if (this.ShouldImpersonateCatUser)
					{
						WindowsImpersonationContext windowsImpersonationContext2 = this._config.ImpersonateCatUser();
						if (windowsImpersonationContext == null)
						{
							windowsImpersonationContext = windowsImpersonationContext2;
						}
					}
					this._connection = new SqlConnection(this.ConnectionString);
					this._connection.StateChange += this.ConnectionStateChange;
					try
					{
						using (new MeasureSql())
						{
							this._connection.Open();
						}
						this._connectionWasOpened = true;
					}
					catch (InvalidOperationException ex)
					{
						throw new ReportServerDatabaseUnavailableException(ex);
					}
					catch (SqlException ex2)
					{
						if (ex2.Number == 4060)
						{
							throw new ReportServerDatabaseLogonFailedException(ex2);
						}
						throw new ReportServerDatabaseUnavailableException(ex2);
					}
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005F00 File Offset: 0x00004100
		private void ConnectionStateChange(object sender, StateChangeEventArgs e)
		{
			if (this._connection != null && this._connection.State == ConnectionState.Closed)
			{
				this.DisconnectStorage();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005F1D File Offset: 0x0000411D
		private void EnsureCorrectEdition()
		{
			if (ConnectionManager._edition != SqlServerSkuType.None)
			{
				return;
			}
			ConnectionManager._edition = Sku.EnsureCorrectEdition(this._connection, this.ConnectionString, true);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005F3E File Offset: 0x0000413E
		private void CheckForSingleCommit()
		{
			if (this.SingleCommitEnabled && this._performedCommitOrRollback)
			{
				throw new InternalCatalogException("Multiple commit/rollback operations performed on SingleCommit connection manager.");
			}
		}

		// Token: 0x04000082 RID: 130
		private const int LoginFailedErrorNumber = 4060;

		// Token: 0x04000083 RID: 131
		private static SqlServerSkuType _edition = SqlServerSkuType.None;

		// Token: 0x04000084 RID: 132
		private static bool _ChangeHandlerSet = false;

		// Token: 0x04000085 RID: 133
		private static ConnectionConfig _StaticConfig = null;

		// Token: 0x04000086 RID: 134
		private static object _configAccessSync = new object();

		// Token: 0x04000087 RID: 135
		private readonly ConnectionConfig _config;

		// Token: 0x04000088 RID: 136
		private IsolationLevel _defaultIsolationLevel = ConnectionManager.DefaultIsolationLevel;

		// Token: 0x04000089 RID: 137
		private ConnectionTransactionType _transactionType = ConnectionManager.DefaultTransactionType;

		// Token: 0x0400008A RID: 138
		private SqlConnection _connection;

		// Token: 0x0400008B RID: 139
		private bool _connectionWasOpened;

		// Token: 0x0400008C RID: 140
		private SqlTransaction _Transaction;

		// Token: 0x0400008D RID: 141
		private bool _willDisconnectStorage;

		// Token: 0x0400008E RID: 142
		private bool _singleCommit;

		// Token: 0x0400008F RID: 143
		private bool _performedCommitOrRollback;

		// Token: 0x04000090 RID: 144
		private static string _rsBuildNumber;

		// Token: 0x04000091 RID: 145
		private bool _isBatchScopeTransaction;

		// Token: 0x04000092 RID: 146
		private string dbcmptScriptsTemplate = "\r\nDECLARE @currVer nvarchar(128), @currMajorVer nvarchar(32), @idx int, @currMajorVerInt tinyint, @cmpt_level tinyint, @altersql nvarchar(128) ;\r\nSELECT @currVer = CONVERT(nvarchar(128), ServerProperty('ProductVersion')) ;\r\nSET @idx = CHARINDEX('.', @currVer, 0) ;\r\nSET @currMajorVer = SUBSTRING(@currVer, 1, @idx-1) ;\r\nSET @currMajorVerInt = CONVERT(tinyint, @currMajorVer) ;\r\n\r\nDECLARE @dbname sysname\r\nIF @currMajorVerInt =  10\r\nBEGIN\t\t\r\n    SELECT @dbname=DB_NAME()\r\n    SELECT @cmpt_level = compatibility_level FROM\r\n                        master.sys.databases WHERE name = @dbname;\r\n    IF @cmpt_level <> 100\r\n    BEGIN\r\n    SELECT @altersql = 'ALTER DATABASE ' + @dbname +\r\n        ' SET COMPATIBILITY_LEVEL = 100' \r\n    EXEC (@altersql) \r\n    END   \t\r\nEND\r\n\r\nUSE {1}\r\n\r\nIF @currMajorVerInt =  10\r\nBEGIN\t    \r\n    SELECT @dbname=DB_NAME()\r\n    SELECT @cmpt_level = compatibility_level FROM\r\n                         master.sys.databases WHERE name = @dbname;\r\n    IF @cmpt_level <> 100\r\n    BEGIN\t\r\n        SELECT @altersql = 'ALTER DATABASE ' + @dbname +\r\n        ' SET COMPATIBILITY_LEVEL = 100' \r\n    EXEC (@altersql)\r\n    END\r\nEND\r\n\r\nUSE {0}\r\n";
	}
}
