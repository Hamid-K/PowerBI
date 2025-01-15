using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.EnterpriseServices;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using Microsoft.HostIntegration.Drda.Requester;
using Microsoft.HostIntegration.StaticSqlUtil;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009D8 RID: 2520
	public sealed class DrdaConnection : DbConnection, ICloneable
	{
		// Token: 0x14000059 RID: 89
		// (add) Token: 0x06004DB7 RID: 19895 RVA: 0x00138838 File Offset: 0x00136A38
		// (remove) Token: 0x06004DB8 RID: 19896 RVA: 0x00138870 File Offset: 0x00136A70
		internal event EventHandler TransactionStateEventHandler;

		// Token: 0x06004DB9 RID: 19897 RVA: 0x001388A8 File Offset: 0x00136AA8
		public DrdaConnection()
		{
			Trace.ApiEnterTrace("DrdaConnection()");
			this._requester = null;
			this._state = ConnectionState.Closed;
			this._transactionState = TransactionState.AutoCommit;
			this._connectionString = new DrdaConnectionString();
			this._isClosing = false;
			this._serverClass = string.Empty;
			this._serverVersion = string.Empty;
			this.ClientCertificate = null;
			this._exceptionMaker = new Func<string, string, int, int, Exception>(DrdaException.MakeException);
			this._traceContainer = new DrdaClientTraceContainer();
			this._tracePoint = new DrdaClientTracePoint(this._traceContainer, 7);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004DBA RID: 19898 RVA: 0x0013894E File Offset: 0x00136B4E
		public DrdaConnection(string connectionString)
			: this()
		{
			Trace.ApiEnterTrace(this._tracePoint, "DrdaConnection(string)");
			this.ConnectionString = connectionString;
		}

		// Token: 0x06004DBB RID: 19899 RVA: 0x0013896D File Offset: 0x00136B6D
		public DrdaConnection(DrdaConnection connection)
			: this()
		{
			Trace.ApiEnterTrace(this._tracePoint, "DrdaConnection(DrdaConnection)");
			this.ConnectionString = connection.ConnectionString;
			connection._connectionString.CopyInto(this._connectionString);
		}

		// Token: 0x06004DBC RID: 19900 RVA: 0x001389A4 File Offset: 0x00136BA4
		~DrdaConnection()
		{
			this._traceContainer.Release();
			DrdaConnection._objectTypeCount = Interlocked.Decrement(ref DrdaConnection._objectTypeCount);
			this.Dispose(false);
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06004DBD RID: 19901 RVA: 0x001389EC File Offset: 0x00136BEC
		internal static DrdaPermission DrdaPermission
		{
			get
			{
				return DrdaConnection._DrdaPermission;
			}
		}

		// Token: 0x06004DBE RID: 19902 RVA: 0x001389F3 File Offset: 0x00136BF3
		private static DrdaPermission CreateDrdaPermission()
		{
			DrdaPermission drdaPermission = (DrdaPermission)DrdaFactory.Instance.CreatePermission(PermissionState.None);
			drdaPermission.Add(string.Empty, string.Empty, KeyRestrictionBehavior.AllowOnly);
			return drdaPermission;
		}

		// Token: 0x06004DBF RID: 19903 RVA: 0x00138A18 File Offset: 0x00136C18
		public static void ClearPool(DrdaConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException();
			}
			if (connection.Requester == null)
			{
				if (connection._connectionString.IsInitialized && !string.IsNullOrEmpty(connection._connectionString.ConnectionString))
				{
					connection._connectionString.ParseESSOConnectionString();
					RequesterFactory.Instance.ClearPool(connection._connectionString.DrdaConnectInfo);
					return;
				}
			}
			else
			{
				RequesterFactory.Instance.ClearPool(connection.Requester);
			}
		}

		// Token: 0x06004DC0 RID: 19904 RVA: 0x00138A88 File Offset: 0x00136C88
		internal void OnTransactionStateChanged(TransactionState state)
		{
			TransactionStateEventArgs transactionStateEventArgs = new TransactionStateEventArgs();
			transactionStateEventArgs.State = state;
			if (this.TransactionStateEventHandler != null)
			{
				this.TransactionStateEventHandler(this, transactionStateEventArgs);
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06004DC1 RID: 19905 RVA: 0x00138AB7 File Offset: 0x00136CB7
		[DefaultValue("")]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override string DataSource
		{
			get
			{
				return this._connectionString.DataSource;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06004DC2 RID: 19906 RVA: 0x00138AC4 File Offset: 0x00136CC4
		// (set) Token: 0x06004DC3 RID: 19907 RVA: 0x00138AD1 File Offset: 0x00136CD1
		[DefaultValue("")]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override string ConnectionString
		{
			get
			{
				return this._connectionString.ConnectionString;
			}
			set
			{
				if (this.State != ConnectionState.Closed)
				{
					throw DrdaException.ConnectionAlreadyOpen(this.State);
				}
				this._connectionString.ConnectionString = value;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06004DC4 RID: 19908 RVA: 0x00138AF3 File Offset: 0x00136CF3
		[DefaultValue("")]
		[SettingsBindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		public override string Database
		{
			get
			{
				return this._connectionString.Database;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06004DC5 RID: 19909 RVA: 0x00138AF3 File Offset: 0x00136CF3
		// (set) Token: 0x06004DC6 RID: 19910 RVA: 0x00138B00 File Offset: 0x00136D00
		public string InitialCatalog
		{
			get
			{
				return this._connectionString.Database;
			}
			set
			{
				if (this.State != ConnectionState.Closed)
				{
					throw DrdaException.ConnectionAlreadyOpen(this.State);
				}
				this._connectionString.Database = value;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06004DC7 RID: 19911 RVA: 0x00138B22 File Offset: 0x00136D22
		// (set) Token: 0x06004DC8 RID: 19912 RVA: 0x00138B2F File Offset: 0x00136D2F
		public string DatabaseName
		{
			get
			{
				return this._connectionString.DatabaseName;
			}
			set
			{
				if (this.State != ConnectionState.Closed)
				{
					throw DrdaException.ConnectionAlreadyOpen(this.State);
				}
				this._connectionString.DatabaseName = value;
			}
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x00138B51 File Offset: 0x00136D51
		// (set) Token: 0x06004DCA RID: 19914 RVA: 0x00138B60 File Offset: 0x00136D60
		public bool LiteralReplacement
		{
			get
			{
				return this._connectionString.GetBooleanProperty(ConnectionKey.KEY_LITERALREPLACEMENT);
			}
			set
			{
				if (this.State != ConnectionState.Closed)
				{
					throw DrdaException.ConnectionAlreadyOpen(this.State);
				}
				this._connectionString.SetBooleanProperty(ConnectionKey.KEY_LITERALREPLACEMENT, value);
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x00138B84 File Offset: 0x00136D84
		// (set) Token: 0x06004DCC RID: 19916 RVA: 0x00138B91 File Offset: 0x00136D91
		public bool BulkCopySchema
		{
			get
			{
				return this._connectionString.BulkCopySchema;
			}
			set
			{
				this._connectionString.BulkCopySchema = value;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x00138B9F File Offset: 0x00136D9F
		// (set) Token: 0x06004DCE RID: 19918 RVA: 0x00138BAC File Offset: 0x00136DAC
		public string DefaultSchema
		{
			get
			{
				return this._connectionString.DefaultSchema;
			}
			set
			{
				if (this.State != ConnectionState.Closed)
				{
					throw DrdaException.ConnectionAlreadyOpen(this.State);
				}
				this._connectionString.DefaultSchema = value;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004DCF RID: 19919 RVA: 0x00138BCE File Offset: 0x00136DCE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int ConnectionTimeout
		{
			get
			{
				return this._connectionString.ConnectionTimeout;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06004DD0 RID: 19920 RVA: 0x00138BDB File Offset: 0x00136DDB
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ServerVersion
		{
			get
			{
				return this._serverVersion;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x00138BE3 File Offset: 0x00136DE3
		public string ServerClass
		{
			get
			{
				return this._serverClass;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x00138BEB File Offset: 0x00136DEB
		internal DrdaConnectionString DrdaConnectionString
		{
			get
			{
				return this._connectionString;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06004DD3 RID: 19923 RVA: 0x00138BF3 File Offset: 0x00136DF3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06004DD4 RID: 19924 RVA: 0x00138BFB File Offset: 0x00136DFB
		// (set) Token: 0x06004DD5 RID: 19925 RVA: 0x00138C03 File Offset: 0x00136E03
		public X509Certificate ClientCertificate { get; set; }

		// Token: 0x06004DD6 RID: 19926 RVA: 0x00138C0C File Offset: 0x00136E0C
		public override void ChangeDatabase(string value)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			throw DrdaException.NotSupported("ChangeDatabase()");
		}

		// Token: 0x06004DD7 RID: 19927 RVA: 0x00138C23 File Offset: 0x00136E23
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal static Transaction GetCurrentTransaction()
		{
			return global::System.Transactions.Transaction.Current;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x00138C2C File Offset: 0x00136E2C
		public int GetHostVersion(HOST_VERSION_ENUM part)
		{
			if (this.ServerVersion == null)
			{
				return 0;
			}
			string[] array = this.ServerVersion.Split(new char[] { '.' });
			if ((HOST_VERSION_ENUM)array.Length <= part)
			{
				return 0;
			}
			int num;
			int.TryParse(array[(int)part], out num);
			return num;
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x00138C70 File Offset: 0x00136E70
		private async Task InternalOpenAsync(bool isAsync, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			DrdaConnection.DrdaPermission.Demand();
			if (this.State != ConnectionState.Closed)
			{
				throw DrdaException.ConnectionAlreadyOpen(this.State);
			}
			this._connectionString.ParseESSOConnectionString();
			if (!this._connectionString.IsInitialized)
			{
				throw DrdaException.NoConnectionString();
			}
			this.ClearSchemaCache();
			try
			{
				this._requester = RequesterFactory.Instance.GetRequester(this._connectionString.DrdaConnectInfo, this._traceContainer, this._exceptionMaker);
			}
			catch (InvalidOperationException ex)
			{
				Trace.MessageTrace("Pool Max size reached: " + ex.ToString());
				throw DrdaException.NoConnectionInPool();
			}
			this._requester.SetProviderName("DrdaClient");
			await this._requester.ConnectAsync(this.ClientCertificate, isAsync, cancellationToken);
			bool autoCommit = this._connectionString.AutoCommit;
			SqlIsolationLevels sqlIsolationLevels = SqlIsolationLevels.ReadCommitted;
			Transaction currentTransaction = DrdaConnection.GetCurrentTransaction();
			this.CheckTransactionIsolationLevel(currentTransaction, ref autoCommit, ref sqlIsolationLevels, true);
			this._requester[RequesterProperties.TransacctionIsolation] = sqlIsolationLevels;
			this._requester[RequesterProperties.AutoCommit] = autoCommit;
			this._serverClass = this._requester.ServerClass;
			this._serverVersion = this._requester.ServerVersion;
			if (this._connectionString.UnitsOfWork == UnitsOfWorkType.RUW && currentTransaction != null)
			{
				throw DrdaException.NoDistributedTransactions();
			}
			if (this._connectionString.UnitsOfWork == UnitsOfWorkType.DUW && !DrdaConnection.IsSysTxEqualSysEsTransaction())
			{
				Trace.MessageTrace(this._tracePoint, "Auto enlisting in a distributed transaction");
				await this.InternalEnlistTransactionAsync(currentTransaction, -1, isAsync, cancellationToken);
			}
			this._state = ConnectionState.Open;
			this._statementPool = new Queue<ISqlStatement>(10);
			base.OnStateChange(new StateChangeEventArgs(ConnectionState.Closed, ConnectionState.Open));
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x00138CC8 File Offset: 0x00136EC8
		public override void Open()
		{
			this.InternalOpenAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x00138CEE File Offset: 0x00136EEE
		public override Task OpenAsync(CancellationToken cancellationToken)
		{
			return this.InternalOpenAsync(true, cancellationToken);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x00138CF8 File Offset: 0x00136EF8
		private async Task InternalCloseAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this.State != ConnectionState.Closed && !this.IsClosing)
			{
				try
				{
					this._isClosing = true;
					if (TransactionState.LocalStarted == this.TransactionState)
					{
						Trace.MessageTrace(this._tracePoint, "Closing a connection and forcing the active local transaction to rollback.");
						this.Transaction.Rollback();
					}
					await this.ClearStatementPoolAsync(isAsync, cancellationToken);
					await this._requester.Disconnect(isAsync, cancellationToken);
				}
				finally
				{
					this._isClosing = false;
					this._state = ConnectionState.Closed;
					this._requester = null;
					base.OnStateChange(new StateChangeEventArgs(ConnectionState.Open, ConnectionState.Closed));
				}
			}
		}

		// Token: 0x06004DDD RID: 19933 RVA: 0x00138D50 File Offset: 0x00136F50
		public override void Close()
		{
			Trace.ApiEnterTrace(this._tracePoint);
			this.InternalCloseAsync(false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DDE RID: 19934 RVA: 0x00138D81 File Offset: 0x00136F81
		public Task CloseAsync(CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.InternalCloseAsync(true, cancellationToken);
		}

		// Token: 0x06004DDF RID: 19935 RVA: 0x00138D96 File Offset: 0x00136F96
		public override DataTable GetSchema()
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.GetSchema("MetaDataCollections");
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x00138DAE File Offset: 0x00136FAE
		public override DataTable GetSchema(string collectionName)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.GetSchema(collectionName, new string[0]);
		}

		// Token: 0x06004DE1 RID: 19937 RVA: 0x00138DC8 File Offset: 0x00136FC8
		public DataTable GetSchema(string collectionName, string options)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.GetSchema(collectionName, new string[0], options);
		}

		// Token: 0x06004DE2 RID: 19938 RVA: 0x00138DE3 File Offset: 0x00136FE3
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			return this.GetSchema(collectionName, restrictionValues, null);
		}

		// Token: 0x06004DE3 RID: 19939 RVA: 0x00138DF0 File Offset: 0x00136FF0
		public DataTable GetSchema(string collectionName, string[] restrictionValues, string options)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			Trace.MessageTrace(this._tracePoint, "GetSchema with collectionName = {0}", collectionName);
			if (this.State != ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			if (collectionName == null)
			{
				collectionName = "MetaDataCollections";
			}
			if (this._schemaCache == null)
			{
				this._schemaCache = new DrdaSchemaCache();
			}
			DataTable dataTable = this._schemaCache.GetSchema(collectionName, restrictionValues, options);
			if (dataTable == null)
			{
				dataTable = new DataTable();
				if (collectionName.Equals(DrdaSchemaMetaDataCollectionsInformation.SchemaName))
				{
					dataTable = DrdaSchemaMetaDataCollectionsInformation.Execute(DrdaSchemaCommand.SchemaList);
				}
				else if (collectionName.Equals(DrdaSchemaDataSourceInformation.SchemaName))
				{
					dataTable = DrdaSchemaDataSourceInformation.Execute(this);
				}
				else if (collectionName.Equals(DrdaRestrictionInformation.SchemaName))
				{
					dataTable = DrdaRestrictionInformation.Execute(DrdaSchemaCommand.SchemaList);
				}
				else if (collectionName.Equals(DrdaReservedWordsInformation.SchemaName))
				{
					dataTable = DrdaReservedWordsInformation.Execute(this);
				}
				else
				{
					DrdaDataAdapter drdaDataAdapter = new DrdaDataAdapter();
					drdaDataAdapter.SchemaSelectCommand = new DrdaSchemaCommand(collectionName, this, options);
					if (restrictionValues != null)
					{
						for (int i = 0; i < restrictionValues.Length; i++)
						{
							DrdaParameter drdaParameter = new DrdaParameter();
							drdaParameter.Value = restrictionValues[i];
							if (restrictionValues[i] != null)
							{
								drdaParameter.Size = restrictionValues[i].Length;
							}
							drdaParameter.DrdaType = DrdaType.VarChar;
							drdaDataAdapter.SchemaSelectCommand.Parameters.Add(drdaParameter);
						}
					}
					drdaDataAdapter.Fill(dataTable);
					drdaDataAdapter.SchemaSelectCommand.Dispose();
					drdaDataAdapter.SchemaSelectCommand = null;
					drdaDataAdapter.Dispose();
					if (string.Compare(collectionName, "DataTypes", true) == 0)
					{
						Stack<int> stack = new Stack<int>();
						for (int j = 0; j < dataTable.Rows.Count; j++)
						{
							DataRow dataRow = dataTable.Rows[j];
							if (dataRow[0] is string && (string)dataRow[0] == string.Empty && dataRow[1] is int && (int)dataRow[1] == -1)
							{
								stack.Push(j);
							}
						}
						bool flag = stack.Count > 0;
						while (stack.Count > 0)
						{
							dataTable.Rows.RemoveAt(stack.Pop());
						}
						if (flag)
						{
							dataTable.AcceptChanges();
						}
					}
				}
				this._schemaCache.AddSchema(collectionName, restrictionValues, options, dataTable.Copy());
			}
			return dataTable;
		}

		// Token: 0x06004DE4 RID: 19940 RVA: 0x00139029 File Offset: 0x00137229
		public void ClearSchemaCache()
		{
			if (this._schemaCache != null)
			{
				this._schemaCache.Clear();
			}
		}

		// Token: 0x06004DE5 RID: 19941 RVA: 0x0013903E File Offset: 0x0013723E
		protected override DbCommand CreateDbCommand()
		{
			return new DrdaCommand
			{
				Connection = this
			};
		}

		// Token: 0x06004DE6 RID: 19942 RVA: 0x0013904C File Offset: 0x0013724C
		public new DrdaCommand CreateCommand()
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return (DrdaCommand)this.CreateDbCommand();
		}

		// Token: 0x06004DE7 RID: 19943 RVA: 0x00139064 File Offset: 0x00137264
		protected override DbTransaction BeginDbTransaction(global::System.Data.IsolationLevel isolationLevel)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			DrdaConnection.DrdaPermission.Demand();
			if (this._connectionString.UnitsOfWork == UnitsOfWorkType.DUW)
			{
				throw DrdaException.NoLocalTransactions();
			}
			SqlIsolationLevels sqlIsolationLevels;
			if (isolationLevel <= global::System.Data.IsolationLevel.ReadUncommitted)
			{
				if (isolationLevel != global::System.Data.IsolationLevel.Unspecified)
				{
					if (isolationLevel != global::System.Data.IsolationLevel.ReadUncommitted)
					{
						goto IL_0086;
					}
					sqlIsolationLevels = SqlIsolationLevels.ReadUncommitted;
					goto IL_0092;
				}
			}
			else if (isolationLevel != global::System.Data.IsolationLevel.ReadCommitted)
			{
				if (isolationLevel == global::System.Data.IsolationLevel.RepeatableRead)
				{
					sqlIsolationLevels = SqlIsolationLevels.RepeatableRead;
					goto IL_0092;
				}
				if (isolationLevel != global::System.Data.IsolationLevel.Serializable)
				{
					goto IL_0086;
				}
				sqlIsolationLevels = SqlIsolationLevels.Serializable;
				goto IL_0092;
			}
			sqlIsolationLevels = SqlIsolationLevels.ReadCommitted;
			isolationLevel = global::System.Data.IsolationLevel.ReadCommitted;
			goto IL_0092;
			IL_0086:
			throw DrdaException.NotSupported(isolationLevel);
			IL_0092:
			Trace.MessageTrace(this._tracePoint, "Setting isolation level to: {0}", sqlIsolationLevels);
			this._requester[RequesterProperties.TransacctionIsolation] = sqlIsolationLevels;
			this._requester[RequesterProperties.AutoCommit] = false;
			if (this.TransactionState != TransactionState.AutoCommit)
			{
				throw DrdaException.NoParallelTransactions();
			}
			DrdaTransaction drdaTransaction = new DrdaTransaction(this, isolationLevel);
			this.Transaction = drdaTransaction;
			return drdaTransaction;
		}

		// Token: 0x06004DE8 RID: 19944 RVA: 0x0013915C File Offset: 0x0013735C
		internal void CheckExecuteTransactionIsolationLevel()
		{
			if (this._connectionString.UnitsOfWork == UnitsOfWorkType.DUW)
			{
				bool flag = false;
				SqlIsolationLevels sqlIsolationLevels = SqlIsolationLevels.ReadCommitted;
				this.CheckTransactionIsolationLevel(null, ref flag, ref sqlIsolationLevels, false);
				this._requester[RequesterProperties.TransacctionIsolation] = sqlIsolationLevels;
				this._requester[RequesterProperties.AutoCommit] = flag;
			}
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x001391AF File Offset: 0x001373AF
		public new DrdaTransaction BeginTransaction()
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.BeginTransaction(global::System.Data.IsolationLevel.ReadCommitted);
		}

		// Token: 0x06004DEA RID: 19946 RVA: 0x001391C7 File Offset: 0x001373C7
		public new DrdaTransaction BeginTransaction(global::System.Data.IsolationLevel il)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return (DrdaTransaction)base.BeginTransaction(il);
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x001391E0 File Offset: 0x001373E0
		object ICloneable.Clone()
		{
			return new DrdaConnection(this);
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06004DEC RID: 19948 RVA: 0x001391E8 File Offset: 0x001373E8
		internal IRequester Requester
		{
			get
			{
				return this._requester;
			}
		}

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06004DED RID: 19949 RVA: 0x001391F0 File Offset: 0x001373F0
		internal bool IsClosing
		{
			get
			{
				return this._isClosing;
			}
		}

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06004DEE RID: 19950 RVA: 0x001391F8 File Offset: 0x001373F8
		// (set) Token: 0x06004DEF RID: 19951 RVA: 0x00139200 File Offset: 0x00137400
		internal TransactionState TransactionState
		{
			get
			{
				return this._transactionState;
			}
			set
			{
				this._transactionState = value;
			}
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06004DF0 RID: 19952 RVA: 0x0013920C File Offset: 0x0013740C
		// (set) Token: 0x06004DF1 RID: 19953 RVA: 0x00139263 File Offset: 0x00137463
		internal DrdaTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.IsAlive)
				{
					if (((DrdaTransaction)this._transaction.Target).Connection != null)
					{
						return (DrdaTransaction)this._transaction.Target;
					}
					this._transaction.Target = null;
				}
				return null;
			}
			set
			{
				if (this._transaction != null)
				{
					this._transaction.Target = value;
					return;
				}
				this._transaction = new WeakReference(value);
			}
		}

		// Token: 0x06004DF2 RID: 19954 RVA: 0x00139288 File Offset: 0x00137488
		internal ISqlStatement CheckOutStatement()
		{
			ISqlStatement sqlStatement;
			if (this._statementPool.Count == 0)
			{
				sqlStatement = this._requester.CreateStatement();
				Trace.MessageTrace(this._tracePoint, "CheckOutStatement(): Allocating statement: {0}", sqlStatement);
			}
			else
			{
				sqlStatement = this._statementPool.Dequeue();
				Trace.MessageTrace(this._tracePoint, "CheckOutStatement(): Retrieving statement from pool: {0}", sqlStatement);
			}
			return sqlStatement;
		}

		// Token: 0x06004DF3 RID: 19955 RVA: 0x001392E4 File Offset: 0x001374E4
		internal async Task CheckInStatementAsync(ISqlStatement statement, bool isAsync, CancellationToken cancellationToken)
		{
			Trace.MessageTrace(this._tracePoint, "CheckInStatement(): Returning statement to pool: {0}", statement);
			if (statement.Requester != this._requester)
			{
				Trace.MessageTrace(this._tracePoint, "CheckInStatement: statement is not created by us: {0}", statement);
				await statement.CloseAsync(isAsync, cancellationToken);
			}
			else
			{
				statement.Reset();
				this._statementPool.Enqueue(statement);
			}
		}

		// Token: 0x06004DF4 RID: 19956 RVA: 0x00139344 File Offset: 0x00137544
		private async Task ClearStatementPoolAsync(bool isAsync, CancellationToken cancellationToken)
		{
			while (this._statementPool.Count > 0)
			{
				ISqlStatement sqlStatement = this._statementPool.Dequeue();
				Trace.MessageTrace(this._tracePoint, "ClearStatementPool(): Freeing statement: {0}", sqlStatement);
				await sqlStatement.CloseAsync(isAsync, cancellationToken);
			}
		}

		// Token: 0x06004DF5 RID: 19957 RVA: 0x0013939C File Offset: 0x0013759C
		private async Task InternalEnlistTransactionAsync(Transaction transaction, int timeOut, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._connectionString.UnitsOfWork == UnitsOfWorkType.RUW)
			{
				throw DrdaException.NoDistributedTransactions();
			}
			bool flag = true;
			SqlIsolationLevels sqlIsolationLevels = SqlIsolationLevels.ReadCommitted;
			this.CheckTransactionIsolationLevel(transaction, ref flag, ref sqlIsolationLevels, true);
			this._requester[RequesterProperties.TransacctionIsolation] = sqlIsolationLevels;
			this._requester[RequesterProperties.AutoCommit] = flag;
			if (timeOut < 0)
			{
				await this._requester.EnlistAsync(transaction, isAsync, cancellationToken);
			}
			else
			{
				await this._requester.EnlistAsync(transaction, timeOut, isAsync, cancellationToken);
			}
		}

		// Token: 0x06004DF6 RID: 19958 RVA: 0x00139404 File Offset: 0x00137604
		public override void EnlistTransaction(Transaction transaction)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			this.InternalEnlistTransactionAsync(transaction, -1, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DF7 RID: 19959 RVA: 0x00139438 File Offset: 0x00137638
		public void EnlistTransaction(Transaction transaction, int timeOut)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			this.InternalEnlistTransactionAsync(transaction, timeOut, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DF8 RID: 19960 RVA: 0x0013946B File Offset: 0x0013766B
		public Task EnlistTransactionAsync(Transaction transaction, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.InternalEnlistTransactionAsync(transaction, -1, true, cancellationToken);
		}

		// Token: 0x06004DF9 RID: 19961 RVA: 0x00139482 File Offset: 0x00137682
		public Task EnlistTransactionAsync(Transaction transaction, int timeOut, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.InternalEnlistTransactionAsync(transaction, timeOut, true, cancellationToken);
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x0013949C File Offset: 0x0013769C
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			this.EnlistDistributedDtcTransactionAsync(transaction, -1, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x001394D0 File Offset: 0x001376D0
		public void EnlistDistributedTransaction(ITransaction transaction, int timeOut)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			this.EnlistDistributedDtcTransactionAsync(transaction, timeOut, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DFC RID: 19964 RVA: 0x00139503 File Offset: 0x00137703
		public Task EnlistDistributedTransactionAsync(ITransaction transaction, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.EnlistDistributedDtcTransactionAsync(transaction, -1, true, cancellationToken);
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x0013951A File Offset: 0x0013771A
		public Task EnlistDistributedTransactionAsync(ITransaction transaction, int timeOut, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(this._tracePoint);
			return this.EnlistDistributedDtcTransactionAsync(transaction, timeOut, true, cancellationToken);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x00139534 File Offset: 0x00137734
		private async Task EnlistDistributedDtcTransactionAsync(ITransaction transaction, int timeOut, bool isAsync, CancellationToken cancellationToken)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(DrdaConnection.DrdaPermission);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Demand();
			Transaction transaction2 = null;
			if (transaction != null)
			{
				transaction2 = TransactionInterop.GetTransactionFromDtcTransaction((IDtcTransaction)transaction);
			}
			await this.InternalEnlistTransactionAsync(transaction2, timeOut, isAsync, cancellationToken);
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x0013959C File Offset: 0x0013779C
		internal void CheckTransactionIsolationLevel(Transaction transaction, ref bool autoCommit, ref SqlIsolationLevels isoLevel, bool allowException)
		{
			if (this._connectionString.UnitsOfWork == UnitsOfWorkType.DUW)
			{
				autoCommit = false;
				if (transaction == null)
				{
					transaction = global::System.Transactions.Transaction.Current;
				}
				if (transaction != null)
				{
					switch (transaction.IsolationLevel)
					{
					case global::System.Transactions.IsolationLevel.Serializable:
						if (this.ServerClass == "DB2/MVS")
						{
							isoLevel = SqlIsolationLevels.ReadCommitted;
							return;
						}
						isoLevel = SqlIsolationLevels.Serializable;
						break;
					case global::System.Transactions.IsolationLevel.RepeatableRead:
						isoLevel = SqlIsolationLevels.RepeatableRead;
						return;
					case global::System.Transactions.IsolationLevel.ReadCommitted:
					case global::System.Transactions.IsolationLevel.Unspecified:
						isoLevel = SqlIsolationLevels.ReadCommitted;
						return;
					case global::System.Transactions.IsolationLevel.ReadUncommitted:
						isoLevel = SqlIsolationLevels.ReadUncommitted;
						return;
					case global::System.Transactions.IsolationLevel.Snapshot:
					case global::System.Transactions.IsolationLevel.Chaos:
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x00139638 File Offset: 0x00137838
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static bool IsSysTxEqualSysEsTransaction()
		{
			bool flag = false;
			if (!ContextUtil.IsInTransaction && null == global::System.Transactions.Transaction.Current)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x00139660 File Offset: 0x00137860
		internal async Task CommitAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this.State != ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			if (this._transactionState == TransactionState.LocalStarted)
			{
				await this._requester.CommitAsync(isAsync, cancellationToken);
				this._requester[RequesterProperties.TransacctionIsolation] = SqlIsolationLevels.ReadCommitted;
				this._requester[RequesterProperties.AutoCommit] = true;
				this._transactionState = TransactionState.AutoCommit;
				this.OnTransactionStateChanged(TransactionState.Commit);
				if (this._transaction != null)
				{
					this._transaction.Target = null;
				}
				return;
			}
			throw DrdaException.AlreadyEndTransaction();
		}

		// Token: 0x06004E02 RID: 19970 RVA: 0x001396B8 File Offset: 0x001378B8
		internal async Task RollbackAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this.State != ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			if (this._transactionState == TransactionState.LocalStarted)
			{
				await this._requester.RollbackAsync(isAsync, cancellationToken);
				this._requester[RequesterProperties.TransacctionIsolation] = SqlIsolationLevels.ReadCommitted;
				this._requester[RequesterProperties.AutoCommit] = true;
				this._transactionState = TransactionState.AutoCommit;
				this.OnTransactionStateChanged(TransactionState.RollBack);
				if (this._transaction != null)
				{
					this._transaction.Target = null;
				}
				return;
			}
			throw DrdaException.AlreadyEndTransaction();
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x00139710 File Offset: 0x00137910
		internal async Task BindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.BindPackageAsync(package, options, progress, isAsync, cancellationToken);
		}

		// Token: 0x06004E04 RID: 19972 RVA: 0x00139780 File Offset: 0x00137980
		internal async Task DropPackageAsync(Package package, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.DropPackageAsync(package, progress, isAsync, cancellationToken);
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x001397E8 File Offset: 0x001379E8
		internal async Task CopyPackageAsync(Package package, Options options, string targetRdbName, string targetCollectionId, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.CopyPackageAsync(package, options, targetRdbName, targetCollectionId, progress, isAsync, cancellationToken);
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x0013986C File Offset: 0x00137A6C
		internal async Task RebindPackageAsync(Package package, Options options, IProgress<string> progress, bool isAsync, CancellationToken cancellationToken)
		{
			await this._requester.RebindPackageAsync(package, options, progress, isAsync, cancellationToken);
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x001398DC File Offset: 0x00137ADC
		public void SetupHostPackages(IProgress<string> progress)
		{
			this._requester.SetupHostPackagesAsync(true, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004E08 RID: 19976 RVA: 0x0013990C File Offset: 0x00137B0C
		public async Task SetupHostPackagesAsync(IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this._requester.SetupHostPackagesAsync(true, progress, true, cancellationToken);
		}

		// Token: 0x06004E09 RID: 19977 RVA: 0x00139964 File Offset: 0x00137B64
		public void SetupHostPackages(bool releaseCommit, IProgress<string> progress)
		{
			this._requester.SetupHostPackagesAsync(releaseCommit, progress, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x00139994 File Offset: 0x00137B94
		public async Task SetupHostPackagesAsync(bool releaseCommit, IProgress<string> progress, CancellationToken cancellationToken)
		{
			await this._requester.SetupHostPackagesAsync(releaseCommit, progress, true, cancellationToken);
		}

		// Token: 0x06004E0B RID: 19979 RVA: 0x001399F1 File Offset: 0x00137BF1
		public void SetCustomPackageData(XmlReader packageData)
		{
			this._requester.SetCustomPackageData(packageData);
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x001399FF File Offset: 0x00137BFF
		protected override void Dispose(bool disposing)
		{
			if (this._state != ConnectionState.Closed)
			{
				this.Close();
			}
			this.ConnectionString = string.Empty;
			this._traceContainer.Release();
			DrdaConnection._objectTypeCount = Interlocked.Decrement(ref DrdaConnection._objectTypeCount);
			base.Dispose(disposing);
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x00139A3B File Offset: 0x00137C3B
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return DrdaFactory.Instance;
			}
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06004E0E RID: 19982 RVA: 0x00139A42 File Offset: 0x00137C42
		internal DrdaClientTracePoint TracePoint
		{
			get
			{
				return this._tracePoint;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x00139A4A File Offset: 0x00137C4A
		internal DrdaClientTraceContainer TraceContainer
		{
			get
			{
				return this._traceContainer;
			}
		}

		// Token: 0x04003DBA RID: 15802
		private DrdaConnectionString _connectionString;

		// Token: 0x04003DBB RID: 15803
		private ConnectionState _state;

		// Token: 0x04003DBC RID: 15804
		private WeakReference _transaction;

		// Token: 0x04003DBD RID: 15805
		private TransactionState _transactionState;

		// Token: 0x04003DBE RID: 15806
		private IRequester _requester;

		// Token: 0x04003DBF RID: 15807
		private Queue<ISqlStatement> _statementPool;

		// Token: 0x04003DC0 RID: 15808
		private bool _isClosing;

		// Token: 0x04003DC1 RID: 15809
		private DrdaSchemaCache _schemaCache;

		// Token: 0x04003DC2 RID: 15810
		private Func<string, string, int, int, Exception> _exceptionMaker;

		// Token: 0x04003DC3 RID: 15811
		private static int _objectTypeCount;

		// Token: 0x04003DC4 RID: 15812
		internal readonly int _objectID = Interlocked.Increment(ref DrdaConnection._objectTypeCount);

		// Token: 0x04003DC5 RID: 15813
		private string _serverClass;

		// Token: 0x04003DC6 RID: 15814
		private string _serverVersion;

		// Token: 0x04003DC7 RID: 15815
		private DrdaClientTracePoint _tracePoint;

		// Token: 0x04003DC8 RID: 15816
		private DrdaClientTraceContainer _traceContainer;

		// Token: 0x04003DCA RID: 15818
		private static DrdaPermission _DrdaPermission = DrdaConnection.CreateDrdaPermission();
	}
}
