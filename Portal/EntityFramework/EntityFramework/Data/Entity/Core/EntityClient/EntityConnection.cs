using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005DC RID: 1500
	public class EntityConnection : DbConnection
	{
		// Token: 0x060048DC RID: 18652 RVA: 0x00102FCD File Offset: 0x001011CD
		public EntityConnection()
			: this(string.Empty)
		{
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x00102FDA File Offset: 0x001011DA
		public EntityConnection(string connectionString)
		{
			this._connectionStringLock = new object();
			this._entityConnectionOwnsStoreConnection = true;
			this._associatedContexts = new List<ObjectContext>();
			base..ctor();
			this.ChangeConnectionString(connectionString);
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x00103006 File Offset: 0x00101206
		public EntityConnection(MetadataWorkspace workspace, DbConnection connection)
			: this(Check.NotNull<MetadataWorkspace>(workspace, "workspace"), Check.NotNull<DbConnection>(connection, "connection"), false, false)
		{
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x00103026 File Offset: 0x00101226
		public EntityConnection(MetadataWorkspace workspace, DbConnection connection, bool entityConnectionOwnsStoreConnection)
			: this(Check.NotNull<MetadataWorkspace>(workspace, "workspace"), Check.NotNull<DbConnection>(connection, "connection"), false, entityConnectionOwnsStoreConnection)
		{
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x00103048 File Offset: 0x00101248
		internal EntityConnection(MetadataWorkspace workspace, DbConnection connection, bool skipInitialization, bool entityConnectionOwnsStoreConnection)
		{
			this._connectionStringLock = new object();
			this._entityConnectionOwnsStoreConnection = true;
			this._associatedContexts = new List<ObjectContext>();
			base..ctor();
			if (!skipInitialization)
			{
				if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.CSpace))
				{
					throw new ArgumentException(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("EdmItemCollection"));
				}
				if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.SSpace))
				{
					throw new ArgumentException(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("StoreItemCollection"));
				}
				if (!workspace.IsItemCollectionAlreadyRegistered(DataSpace.CSSpace))
				{
					throw new ArgumentException(Strings.EntityClient_ItemCollectionsNotRegisteredInWorkspace("StorageMappingItemCollection"));
				}
				if (connection.GetProviderFactory() == null)
				{
					throw new ProviderIncompatibleException(Strings.EntityClient_DbConnectionHasNoProvider(connection));
				}
				StoreItemCollection storeItemCollection = (StoreItemCollection)workspace.GetItemCollection(DataSpace.SSpace);
				this._providerFactory = storeItemCollection.ProviderFactory;
				this._initialized = true;
			}
			this._metadataWorkspace = workspace;
			this._storeConnection = connection;
			this._entityConnectionOwnsStoreConnection = entityConnectionOwnsStoreConnection;
			if (this._storeConnection != null)
			{
				this._entityClientConnectionState = DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext);
			}
			this.SubscribeToStoreConnectionStateChangeEvents();
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x0010313F File Offset: 0x0010133F
		private void SubscribeToStoreConnectionStateChangeEvents()
		{
			if (this._storeConnection != null)
			{
				this._storeConnection.StateChange += this.StoreConnectionStateChangeHandler;
			}
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x00103161 File Offset: 0x00101361
		private void UnsubscribeFromStoreConnectionStateChangeEvents()
		{
			if (this._storeConnection != null)
			{
				this._storeConnection.StateChange -= this.StoreConnectionStateChangeHandler;
			}
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x00103184 File Offset: 0x00101384
		internal virtual void StoreConnectionStateChangeHandler(object sender, StateChangeEventArgs stateChange)
		{
			ConnectionState currentState = stateChange.CurrentState;
			if (this._entityClientConnectionState != currentState)
			{
				ConnectionState entityClientConnectionState = this._entityClientConnectionState;
				this._entityClientConnectionState = stateChange.CurrentState;
				this.OnStateChange(new StateChangeEventArgs(entityClientConnectionState, currentState));
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x060048E4 RID: 18660 RVA: 0x001031C4 File Offset: 0x001013C4
		// (set) Token: 0x060048E5 RID: 18661 RVA: 0x00103300 File Offset: 0x00101500
		public override string ConnectionString
		{
			get
			{
				if (this._userConnectionOptions == null)
				{
					return string.Format(CultureInfo.InvariantCulture, "{0}={3}{4};{1}={5};{2}=\"{6}\";", new object[]
					{
						"metadata",
						"provider",
						"provider connection string",
						"reader://",
						this._metadataWorkspace.MetadataWorkspaceId,
						this._storeConnection.GetProviderInvariantName(),
						DbInterception.Dispatch.Connection.GetConnectionString(this._storeConnection, this.InterceptionContext)
					});
				}
				string usersConnectionString = this._userConnectionOptions.UsersConnectionString;
				if (this._userConnectionOptions == this._effectiveConnectionOptions && this._storeConnection != null)
				{
					string text = null;
					try
					{
						text = DbInterception.Dispatch.Connection.GetConnectionString(this._storeConnection, this.InterceptionContext);
					}
					catch (Exception ex)
					{
						if (ex.IsCatchableExceptionType())
						{
							throw new EntityException(Strings.EntityClient_ProviderSpecificError("ConnectionString"), ex);
						}
						throw;
					}
					string text2 = this._userConnectionOptions["provider connection string"];
					if (text != text2 && (!string.IsNullOrEmpty(text) || !string.IsNullOrEmpty(text2)))
					{
						return new EntityConnectionStringBuilder(usersConnectionString)
						{
							ProviderConnectionString = text
						}.ConnectionString;
					}
				}
				return usersConnectionString;
			}
			set
			{
				if (this._initialized)
				{
					throw new InvalidOperationException(Strings.EntityClient_SettingsCannotBeChangedOnOpenConnection);
				}
				this.ChangeConnectionString(value);
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x060048E6 RID: 18662 RVA: 0x0010331C File Offset: 0x0010151C
		internal IEnumerable<ObjectContext> AssociatedContexts
		{
			get
			{
				return this._associatedContexts;
			}
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x00103324 File Offset: 0x00101524
		internal virtual void AssociateContext(ObjectContext context)
		{
			if (this._associatedContexts.Count != 0)
			{
				foreach (ObjectContext objectContext in this._associatedContexts.ToArray())
				{
					if (context == objectContext || objectContext.IsDisposed)
					{
						this._associatedContexts.Remove(objectContext);
					}
				}
			}
			this._associatedContexts.Add(context);
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x060048E8 RID: 18664 RVA: 0x00103381 File Offset: 0x00101581
		internal DbInterceptionContext InterceptionContext
		{
			get
			{
				return DbInterceptionContext.Combine(this.AssociatedContexts.Select((ObjectContext c) => c.InterceptionContext));
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x060048E9 RID: 18665 RVA: 0x001033B4 File Offset: 0x001015B4
		public override int ConnectionTimeout
		{
			get
			{
				if (this._storeConnection == null)
				{
					return 0;
				}
				int connectionTimeout;
				try
				{
					connectionTimeout = DbInterception.Dispatch.Connection.GetConnectionTimeout(this._storeConnection, this.InterceptionContext);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("ConnectionTimeout"), ex);
					}
					throw;
				}
				return connectionTimeout;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x060048EA RID: 18666 RVA: 0x00103418 File Offset: 0x00101618
		public override string Database
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x060048EB RID: 18667 RVA: 0x00103420 File Offset: 0x00101620
		public override ConnectionState State
		{
			get
			{
				ConnectionState? fakeConnectionState = this._fakeConnectionState;
				if (fakeConnectionState == null)
				{
					return this._entityClientConnectionState;
				}
				return fakeConnectionState.GetValueOrDefault();
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x060048EC RID: 18668 RVA: 0x0010344C File Offset: 0x0010164C
		public override string DataSource
		{
			get
			{
				if (this._storeConnection == null)
				{
					return string.Empty;
				}
				string dataSource;
				try
				{
					dataSource = DbInterception.Dispatch.Connection.GetDataSource(this._storeConnection, this.InterceptionContext);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("DataSource"), ex);
					}
					throw;
				}
				return dataSource;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x060048ED RID: 18669 RVA: 0x001034B4 File Offset: 0x001016B4
		public override string ServerVersion
		{
			get
			{
				if (this._storeConnection == null)
				{
					throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
				}
				if (this.State != ConnectionState.Open)
				{
					throw Error.EntityClient_ConnectionNotOpen();
				}
				string serverVersion;
				try
				{
					serverVersion = DbInterception.Dispatch.Connection.GetServerVersion(this._storeConnection, this.InterceptionContext);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("ServerVersion"), ex);
					}
					throw;
				}
				return serverVersion;
			}
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060048EE RID: 18670 RVA: 0x0010352C File Offset: 0x0010172C
		protected override DbProviderFactory DbProviderFactory
		{
			get
			{
				return EntityProviderFactory.Instance;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00103533 File Offset: 0x00101733
		internal virtual DbProviderFactory StoreProviderFactory
		{
			get
			{
				return this._providerFactory;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060048F0 RID: 18672 RVA: 0x0010353B File Offset: 0x0010173B
		public virtual DbConnection StoreConnection
		{
			get
			{
				return this._storeConnection;
			}
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x00103543 File Offset: 0x00101743
		public virtual MetadataWorkspace GetMetadataWorkspace()
		{
			if (this._metadataWorkspace != null)
			{
				return this._metadataWorkspace;
			}
			this._metadataWorkspace = MetadataCache.Instance.GetMetadataWorkspace(this._effectiveConnectionOptions);
			this._initialized = true;
			return this._metadataWorkspace;
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060048F2 RID: 18674 RVA: 0x00103577 File Offset: 0x00101777
		public virtual EntityTransaction CurrentTransaction
		{
			get
			{
				if (this._currentTransaction != null && (DbInterception.Dispatch.Transaction.GetConnection(this._currentTransaction.StoreTransaction, this.InterceptionContext) == null || this.State == ConnectionState.Closed))
				{
					this.ClearCurrentTransaction();
				}
				return this._currentTransaction;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x001035B8 File Offset: 0x001017B8
		internal virtual bool EnlistedInUserTransaction
		{
			get
			{
				bool flag;
				try
				{
					flag = this._enlistedTransaction != null && this._enlistedTransaction.TransactionInformation.Status == TransactionStatus.Active;
				}
				catch (ObjectDisposedException)
				{
					this._enlistedTransaction = null;
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x060048F4 RID: 18676 RVA: 0x0010360C File Offset: 0x0010180C
		public override void Open()
		{
			this._fakeConnectionState = null;
			if (!DbInterception.Dispatch.CancelableEntityConnection.Opening(this, this.InterceptionContext))
			{
				this._fakeConnectionState = new ConnectionState?(ConnectionState.Open);
				return;
			}
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State == ConnectionState.Broken)
			{
				throw Error.EntityClient_CannotOpenBrokenConnection();
			}
			if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				MetadataWorkspace metadataWorkspace = this.GetMetadataWorkspace();
				try
				{
					DbProviderServices.GetExecutionStrategy(this._storeConnection, metadataWorkspace).Execute(delegate
					{
						DbInterception.Dispatch.Connection.Open(this._storeConnection, this.InterceptionContext);
					});
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("Open"), ex);
					}
					throw;
				}
				this.ClearTransactions();
			}
			if (this._storeConnection == null || DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
		}

		// Token: 0x060048F5 RID: 18677 RVA: 0x0010370C File Offset: 0x0010190C
		public override async Task OpenAsync(CancellationToken cancellationToken)
		{
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State == ConnectionState.Broken)
			{
				throw Error.EntityClient_CannotOpenBrokenConnection();
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				MetadataWorkspace metadataWorkspace = this.GetMetadataWorkspace();
				try
				{
					await DbProviderServices.GetExecutionStrategy(this._storeConnection, metadataWorkspace).ExecuteAsync(() => DbInterception.Dispatch.Connection.OpenAsync(this._storeConnection, this.InterceptionContext, cancellationToken), cancellationToken).WithCurrentCulture();
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("Open"), ex);
					}
					throw;
				}
				this.ClearTransactions();
			}
			if (this._storeConnection == null || DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
		}

		// Token: 0x060048F6 RID: 18678 RVA: 0x00103759 File Offset: 0x00101959
		public new virtual EntityCommand CreateCommand()
		{
			return new EntityCommand(null, this);
		}

		// Token: 0x060048F7 RID: 18679 RVA: 0x00103762 File Offset: 0x00101962
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		// Token: 0x060048F8 RID: 18680 RVA: 0x0010376A File Offset: 0x0010196A
		public override void Close()
		{
			this._fakeConnectionState = null;
			if (this._storeConnection == null)
			{
				return;
			}
			this.StoreCloseHelper();
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x00103787 File Offset: 0x00101987
		public override void ChangeDatabase(string databaseName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x0010378E File Offset: 0x0010198E
		public new virtual EntityTransaction BeginTransaction()
		{
			return base.BeginTransaction() as EntityTransaction;
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x0010379B File Offset: 0x0010199B
		public new virtual EntityTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return base.BeginTransaction(isolationLevel) as EntityTransaction;
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x001037AC File Offset: 0x001019AC
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			if (this._fakeConnectionState != null)
			{
				return new EntityTransaction();
			}
			if (this.CurrentTransaction != null)
			{
				throw new InvalidOperationException(Strings.EntityClient_TransactionAlreadyStarted);
			}
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
			BeginTransactionInterceptionContext interceptionContext = new BeginTransactionInterceptionContext(this.InterceptionContext);
			if (isolationLevel != IsolationLevel.Unspecified)
			{
				interceptionContext = interceptionContext.WithIsolationLevel(isolationLevel);
			}
			DbTransaction dbTransaction = null;
			try
			{
				dbTransaction = DbProviderServices.GetExecutionStrategy(this._storeConnection, this.GetMetadataWorkspace()).Execute<DbTransaction>(delegate
				{
					if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) == ConnectionState.Broken)
					{
						DbInterception.Dispatch.Connection.Close(this._storeConnection, interceptionContext);
					}
					if (DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) == ConnectionState.Closed)
					{
						DbInterception.Dispatch.Connection.Open(this._storeConnection, interceptionContext);
					}
					return DbInterception.Dispatch.Connection.BeginTransaction(this._storeConnection, interceptionContext);
				});
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ErrorInBeginningTransaction, ex);
				}
				throw;
			}
			if (dbTransaction == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_ReturnedNullOnProviderMethod("BeginTransaction", this._storeConnection.GetType().Name));
			}
			this._currentTransaction = new EntityTransaction(this, dbTransaction);
			return this._currentTransaction;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x001038B4 File Offset: 0x00101AB4
		internal virtual EntityTransaction UseStoreTransaction(DbTransaction storeTransaction)
		{
			if (storeTransaction == null)
			{
				this.ClearCurrentTransaction();
			}
			else
			{
				if (this.CurrentTransaction != null)
				{
					throw new InvalidOperationException(Strings.DbContext_TransactionAlreadyStarted);
				}
				if (this.EnlistedInUserTransaction)
				{
					throw new InvalidOperationException(Strings.DbContext_TransactionAlreadyEnlistedInUserTransaction);
				}
				DbConnection connection = DbInterception.Dispatch.Transaction.GetConnection(storeTransaction, this.InterceptionContext);
				if (connection == null)
				{
					throw new InvalidOperationException(Strings.DbContext_InvalidTransactionNoConnection);
				}
				if (connection != this.StoreConnection)
				{
					throw new InvalidOperationException(Strings.DbContext_InvalidTransactionForConnection);
				}
				this._currentTransaction = new EntityTransaction(this, storeTransaction);
			}
			return this._currentTransaction;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0010393C File Offset: 0x00101B3C
		public override void EnlistTransaction(Transaction transaction)
		{
			if (this._storeConnection == null)
			{
				throw Error.EntityClient_ConnectionStringNeededBeforeOperation();
			}
			if (this.State != ConnectionState.Open)
			{
				throw Error.EntityClient_ConnectionNotOpen();
			}
			try
			{
				EnlistTransactionInterceptionContext enlistTransactionInterceptionContext = new EnlistTransactionInterceptionContext(this.InterceptionContext);
				enlistTransactionInterceptionContext = enlistTransactionInterceptionContext.WithTransaction(transaction);
				DbInterception.Dispatch.Connection.EnlistTransaction(this._storeConnection, enlistTransactionInterceptionContext);
				if (transaction != null && !this.EnlistedInUserTransaction)
				{
					transaction.TransactionCompleted += this.EnlistedTransactionCompleted;
				}
				this._enlistedTransaction = transaction;
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ProviderSpecificError("EnlistTransaction"), ex);
				}
				throw;
			}
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x001039EC File Offset: 0x00101BEC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ClearTransactions();
				if (this._storeConnection != null)
				{
					if (this._entityConnectionOwnsStoreConnection)
					{
						this.StoreCloseHelper();
					}
					this.UnsubscribeFromStoreConnectionStateChangeEvents();
					if (this._entityConnectionOwnsStoreConnection)
					{
						DbInterception.Dispatch.Connection.Dispose(this._storeConnection, this.InterceptionContext);
					}
					this._storeConnection = null;
				}
				this._entityClientConnectionState = ConnectionState.Closed;
				this.ChangeConnectionString(string.Empty);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00103A61 File Offset: 0x00101C61
		internal virtual void ClearCurrentTransaction()
		{
			this._currentTransaction = null;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x00103A6C File Offset: 0x00101C6C
		private void ChangeConnectionString(string newConnectionString)
		{
			DbConnectionOptions dbConnectionOptions = EntityConnection._emptyConnectionOptions;
			if (!string.IsNullOrEmpty(newConnectionString))
			{
				dbConnectionOptions = new DbConnectionOptions(newConnectionString, EntityConnectionStringBuilder.ValidKeywords);
			}
			DbProviderFactory dbProviderFactory = null;
			DbConnection dbConnection = null;
			DbConnectionOptions dbConnectionOptions2 = dbConnectionOptions;
			if (!dbConnectionOptions.IsEmpty)
			{
				string text = dbConnectionOptions["name"];
				if (!string.IsNullOrEmpty(text))
				{
					if (1 < dbConnectionOptions.Parsetable.Count)
					{
						throw new ArgumentException(Strings.EntityClient_ExtraParametersWithNamedConnection);
					}
					ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
					if (connectionStringSettings == null || connectionStringSettings.ProviderName != "System.Data.EntityClient")
					{
						throw new ArgumentException(Strings.EntityClient_InvalidNamedConnection);
					}
					dbConnectionOptions2 = new DbConnectionOptions(connectionStringSettings.ConnectionString, EntityConnectionStringBuilder.ValidKeywords);
					if (!string.IsNullOrEmpty(dbConnectionOptions2["name"]))
					{
						throw new ArgumentException(Strings.EntityClient_NestedNamedConnection(text));
					}
				}
				EntityConnection.ValidateValueForTheKeyword(dbConnectionOptions2, "metadata");
				string text2 = EntityConnection.ValidateValueForTheKeyword(dbConnectionOptions2, "provider");
				dbProviderFactory = DbConfiguration.DependencyResolver.GetService(text2);
				dbConnection = EntityConnection.GetStoreConnection(dbProviderFactory);
				try
				{
					string text3 = dbConnectionOptions2["provider connection string"];
					if (text3 != null)
					{
						DbInterception.Dispatch.Connection.SetConnectionString(dbConnection, new DbConnectionPropertyInterceptionContext<string>(this.InterceptionContext).WithValue(text3));
					}
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						throw new EntityException(Strings.EntityClient_ProviderSpecificError("ConnectionString"), ex);
					}
					throw;
				}
			}
			object connectionStringLock = this._connectionStringLock;
			lock (connectionStringLock)
			{
				this._providerFactory = dbProviderFactory;
				this._metadataWorkspace = null;
				this.ClearTransactions();
				this.UnsubscribeFromStoreConnectionStateChangeEvents();
				this._storeConnection = dbConnection;
				this.SubscribeToStoreConnectionStateChangeEvents();
				this._userConnectionOptions = dbConnectionOptions;
				this._effectiveConnectionOptions = dbConnectionOptions2;
			}
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00103C28 File Offset: 0x00101E28
		private static string ValidateValueForTheKeyword(DbConnectionOptions effectiveConnectionOptions, string keywordName)
		{
			string text = effectiveConnectionOptions[keywordName];
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException(Strings.EntityClient_ConnectionStringMissingInfo(keywordName));
			}
			return text;
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x00103C61 File Offset: 0x00101E61
		private void ClearTransactions()
		{
			this.ClearCurrentTransaction();
			this.ClearEnlistedTransaction();
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x00103C6F File Offset: 0x00101E6F
		private void ClearEnlistedTransaction()
		{
			if (this.EnlistedInUserTransaction)
			{
				this._enlistedTransaction.TransactionCompleted -= this.EnlistedTransactionCompleted;
			}
			this._enlistedTransaction = null;
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x00103C97 File Offset: 0x00101E97
		private void EnlistedTransactionCompleted(object sender, TransactionEventArgs e)
		{
			e.Transaction.TransactionCompleted -= this.EnlistedTransactionCompleted;
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x00103CB0 File Offset: 0x00101EB0
		private void StoreCloseHelper()
		{
			try
			{
				if (this._storeConnection != null && DbInterception.Dispatch.Connection.GetState(this._storeConnection, this.InterceptionContext) != ConnectionState.Closed)
				{
					DbInterception.Dispatch.Connection.Close(this._storeConnection, this.InterceptionContext);
				}
				this.ClearTransactions();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityException(Strings.EntityClient_ErrorInClosingConnection, ex);
				}
				throw;
			}
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x00103D2C File Offset: 0x00101F2C
		private static DbConnection GetStoreConnection(DbProviderFactory factory)
		{
			DbConnection dbConnection = factory.CreateConnection();
			if (dbConnection == null)
			{
				throw new ProviderIncompatibleException(Strings.EntityClient_ReturnedNullOnProviderMethod("CreateConnection", factory.GetType().Name));
			}
			return dbConnection;
		}

		// Token: 0x040019C9 RID: 6601
		private const string EntityClientProviderName = "System.Data.EntityClient";

		// Token: 0x040019CA RID: 6602
		private const string ProviderInvariantName = "provider";

		// Token: 0x040019CB RID: 6603
		private const string ProviderConnectionString = "provider connection string";

		// Token: 0x040019CC RID: 6604
		private const string ReaderPrefix = "reader://";

		// Token: 0x040019CD RID: 6605
		private readonly object _connectionStringLock;

		// Token: 0x040019CE RID: 6606
		private static readonly DbConnectionOptions _emptyConnectionOptions = new DbConnectionOptions(string.Empty, new string[0]);

		// Token: 0x040019CF RID: 6607
		private DbConnectionOptions _userConnectionOptions;

		// Token: 0x040019D0 RID: 6608
		private DbConnectionOptions _effectiveConnectionOptions;

		// Token: 0x040019D1 RID: 6609
		private ConnectionState _entityClientConnectionState;

		// Token: 0x040019D2 RID: 6610
		private DbProviderFactory _providerFactory;

		// Token: 0x040019D3 RID: 6611
		private DbConnection _storeConnection;

		// Token: 0x040019D4 RID: 6612
		private readonly bool _entityConnectionOwnsStoreConnection;

		// Token: 0x040019D5 RID: 6613
		private MetadataWorkspace _metadataWorkspace;

		// Token: 0x040019D6 RID: 6614
		private EntityTransaction _currentTransaction;

		// Token: 0x040019D7 RID: 6615
		private Transaction _enlistedTransaction;

		// Token: 0x040019D8 RID: 6616
		private bool _initialized;

		// Token: 0x040019D9 RID: 6617
		private ConnectionState? _fakeConnectionState;

		// Token: 0x040019DA RID: 6618
		private readonly List<ObjectContext> _associatedContexts;
	}
}
