using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x02000058 RID: 88
	public class Database
	{
		// Token: 0x06000222 RID: 546 RVA: 0x0000915B File Offset: 0x0000735B
		internal Database(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000916C File Offset: 0x0000736C
		public DbContextTransaction CurrentTransaction
		{
			get
			{
				EntityTransaction currentTransaction = ((EntityConnection)this._internalContext.ObjectContext.Connection).CurrentTransaction;
				if (this._dbContextTransaction == null || this._entityTransaction != currentTransaction)
				{
					this._entityTransaction = currentTransaction;
					if (currentTransaction != null)
					{
						this._dbContextTransaction = new DbContextTransaction(currentTransaction);
					}
					else
					{
						this._dbContextTransaction = null;
					}
				}
				return this._dbContextTransaction;
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000091CA File Offset: 0x000073CA
		public void UseTransaction(DbTransaction transaction)
		{
			this._entityTransaction = ((EntityConnection)this._internalContext.GetObjectContextWithoutDatabaseInitialization().Connection).UseStoreTransaction(transaction);
			this._dbContextTransaction = null;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000091F4 File Offset: 0x000073F4
		public DbContextTransaction BeginTransaction()
		{
			EntityConnection entityConnection = (EntityConnection)this._internalContext.ObjectContext.Connection;
			this._dbContextTransaction = new DbContextTransaction(entityConnection);
			this._entityTransaction = entityConnection.CurrentTransaction;
			return this._dbContextTransaction;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009238 File Offset: 0x00007438
		public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			EntityConnection entityConnection = (EntityConnection)this._internalContext.ObjectContext.Connection;
			this._dbContextTransaction = new DbContextTransaction(entityConnection, isolationLevel);
			this._entityTransaction = entityConnection.CurrentTransaction;
			return this._dbContextTransaction;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000927A File Offset: 0x0000747A
		public DbConnection Connection
		{
			get
			{
				return this._internalContext.Connection;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009287 File Offset: 0x00007487
		public static void SetInitializer<TContext>(IDatabaseInitializer<TContext> strategy) where TContext : DbContext
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
			InternalConfiguration.Instance.RootResolver.DatabaseInitializerResolver.SetInitializer(typeof(TContext), strategy ?? new NullDatabaseInitializer<TContext>());
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000092C5 File Offset: 0x000074C5
		public void Initialize(bool force)
		{
			if (force)
			{
				this._internalContext.MarkDatabaseInitialized();
				this._internalContext.PerformDatabaseInitialization();
				return;
			}
			this._internalContext.Initialize();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000092EC File Offset: 0x000074EC
		public bool CompatibleWithModel(bool throwIfNoMetadata)
		{
			return this.CompatibleWithModel(throwIfNoMetadata, DatabaseExistenceState.Unknown);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000092F6 File Offset: 0x000074F6
		internal bool CompatibleWithModel(bool throwIfNoMetadata, DatabaseExistenceState existenceState)
		{
			return this._internalContext.CompatibleWithModel(throwIfNoMetadata, existenceState);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009305 File Offset: 0x00007505
		public void Create()
		{
			this.Create(DatabaseExistenceState.Unknown);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009310 File Offset: 0x00007510
		internal void Create(DatabaseExistenceState existenceState)
		{
			if (existenceState == DatabaseExistenceState.Unknown)
			{
				if (this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection))))
				{
					DbInterceptionContext dbInterceptionContext = new DbInterceptionContext();
					dbInterceptionContext = dbInterceptionContext.WithDbContext(this._internalContext.Owner);
					throw Error.Database_DatabaseAlreadyExists(DbInterception.Dispatch.Connection.GetDatabase(this._internalContext.Connection, dbInterceptionContext));
				}
				existenceState = DatabaseExistenceState.DoesNotExist;
			}
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				this._internalContext.CreateDatabase(clonedObjectContext.ObjectContext, existenceState);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000093D4 File Offset: 0x000075D4
		public bool CreateIfNotExists()
		{
			if (this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection))))
			{
				return false;
			}
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				this._internalContext.CreateDatabase(clonedObjectContext.ObjectContext, DatabaseExistenceState.DoesNotExist);
			}
			return true;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000945C File Offset: 0x0000765C
		public bool Exists()
		{
			return this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection)));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009498 File Offset: 0x00007698
		public bool Delete()
		{
			if (!this._internalContext.DatabaseOperations.Exists(this._internalContext.Connection, this._internalContext.CommandTimeout, new Lazy<StoreItemCollection>(new Func<StoreItemCollection>(this.CreateStoreItemCollection))))
			{
				return false;
			}
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				this._internalContext.DatabaseOperations.Delete(clonedObjectContext.ObjectContext);
				this._internalContext.MarkDatabaseNotInitialized();
			}
			return true;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009530 File Offset: 0x00007730
		public static bool Exists(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			bool flag;
			using (LazyInternalConnection lazyInternalConnection = new LazyInternalConnection(nameOrConnectionString))
			{
				flag = new DatabaseOperations().Exists(lazyInternalConnection.Connection, null, new Lazy<StoreItemCollection>(() => new StoreItemCollection()));
			}
			return flag;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000095AC File Offset: 0x000077AC
		public static bool Delete(string nameOrConnectionString)
		{
			Check.NotEmpty(nameOrConnectionString, "nameOrConnectionString");
			if (!Database.Exists(nameOrConnectionString))
			{
				return false;
			}
			using (LazyInternalConnection lazyInternalConnection = new LazyInternalConnection(nameOrConnectionString))
			{
				using (ObjectContext objectContext = Database.CreateEmptyObjectContext(lazyInternalConnection.Connection))
				{
					new DatabaseOperations().Delete(objectContext);
				}
			}
			return true;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009620 File Offset: 0x00007820
		public static bool Exists(DbConnection existingConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			return new DatabaseOperations().Exists(existingConnection, null, new Lazy<StoreItemCollection>(() => new StoreItemCollection()));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009674 File Offset: 0x00007874
		public static bool Delete(DbConnection existingConnection)
		{
			Check.NotNull<DbConnection>(existingConnection, "existingConnection");
			if (!Database.Exists(existingConnection))
			{
				return false;
			}
			using (ObjectContext objectContext = Database.CreateEmptyObjectContext(existingConnection))
			{
				new DatabaseOperations().Delete(objectContext);
			}
			return true;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000235 RID: 565 RVA: 0x000096C8 File Offset: 0x000078C8
		// (set) Token: 0x06000236 RID: 566 RVA: 0x000096D4 File Offset: 0x000078D4
		[Obsolete("The default connection factory should be set in the config file or using the DbConfiguration class. (See http://go.microsoft.com/fwlink/?LinkId=260883)")]
		public static IDbConnectionFactory DefaultConnectionFactory
		{
			get
			{
				return DbConfiguration.DependencyResolver.GetService<IDbConnectionFactory>();
			}
			set
			{
				Check.NotNull<IDbConnectionFactory>(value, "value");
				Database._defaultConnectionFactory = new Lazy<IDbConnectionFactory>(() => value, true);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000237 RID: 567 RVA: 0x0000970B File Offset: 0x0000790B
		internal static IDbConnectionFactory SetDefaultConnectionFactory
		{
			get
			{
				return Database._defaultConnectionFactory.Value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00009719 File Offset: 0x00007919
		internal static bool DefaultConnectionFactoryChanged
		{
			get
			{
				return Database._defaultConnectionFactory != Database._defaultDefaultConnectionFactory;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000972C File Offset: 0x0000792C
		internal static void ResetDefaultConnectionFactory()
		{
			Database._defaultConnectionFactory = Database._defaultDefaultConnectionFactory;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000973A File Offset: 0x0000793A
		private static ObjectContext CreateEmptyObjectContext(DbConnection connection)
		{
			return new DbModelBuilder().Build(connection).Compile().CreateObjectContext<ObjectContext>(connection);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009752 File Offset: 0x00007952
		public DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbRawSqlQuery<TElement>(new InternalSqlNonSetQuery(this._internalContext, typeof(TElement), sql, parameters));
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009788 File Offset: 0x00007988
		public DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters)
		{
			Check.NotNull<Type>(elementType, "elementType");
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbRawSqlQuery(new InternalSqlNonSetQuery(this._internalContext, elementType, sql, parameters));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000097C1 File Offset: 0x000079C1
		public int ExecuteSqlCommand(string sql, params object[] parameters)
		{
			return this.ExecuteSqlCommand(this._internalContext.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, sql, parameters);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000097DC File Offset: 0x000079DC
		public int ExecuteSqlCommand(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return this._internalContext.ExecuteSqlCommand(transactionalBehavior, sql, parameters);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009804 File Offset: 0x00007A04
		public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
		{
			return this.ExecuteSqlCommandAsync(sql, CancellationToken.None, parameters);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009813 File Offset: 0x00007A13
		public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, params object[] parameters)
		{
			return this.ExecuteSqlCommandAsync(transactionalBehavior, sql, CancellationToken.None, parameters);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009823 File Offset: 0x00007A23
		public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
		{
			return this.ExecuteSqlCommandAsync(this._internalContext.EnsureTransactionsForFunctionsAndCommands ? TransactionalBehavior.EnsureTransaction : TransactionalBehavior.DoNotEnsureTransaction, sql, cancellationToken, parameters);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000983F File Offset: 0x00007A3F
		public Task<int> ExecuteSqlCommandAsync(TransactionalBehavior transactionalBehavior, string sql, CancellationToken cancellationToken, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			cancellationToken.ThrowIfCancellationRequested();
			return this._internalContext.ExecuteSqlCommandAsync(transactionalBehavior, sql, cancellationToken, parameters);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009871 File Offset: 0x00007A71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009879 File Offset: 0x00007A79
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009882 File Offset: 0x00007A82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000988A File Offset: 0x00007A8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009894 File Offset: 0x00007A94
		private StoreItemCollection CreateStoreItemCollection()
		{
			StoreItemCollection storeItemCollection;
			using (ClonedObjectContext clonedObjectContext = this._internalContext.CreateObjectContextForDdlOps())
			{
				storeItemCollection = (StoreItemCollection)clonedObjectContext.ObjectContext.Connection.GetMetadataWorkspace().GetItemCollection(DataSpace.SSpace);
			}
			return storeItemCollection;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000098EC File Offset: 0x00007AEC
		// (set) Token: 0x06000249 RID: 585 RVA: 0x000098FC File Offset: 0x00007AFC
		public int? CommandTimeout
		{
			get
			{
				return this._internalContext.CommandTimeout;
			}
			set
			{
				if (value != null)
				{
					int? num = value;
					int num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						throw new ArgumentException(Strings.ObjectContext_InvalidCommandTimeout);
					}
				}
				this._internalContext.CommandTimeout = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00009941 File Offset: 0x00007B41
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000994E File Offset: 0x00007B4E
		public Action<string> Log
		{
			get
			{
				return this._internalContext.Log;
			}
			set
			{
				this._internalContext.Log = value;
			}
		}

		// Token: 0x040000A9 RID: 169
		private static readonly Lazy<IDbConnectionFactory> _defaultDefaultConnectionFactory = new Lazy<IDbConnectionFactory>(() => AppConfig.DefaultInstance.TryGetDefaultConnectionFactory() ?? new LocalDbConnectionFactory(), true);

		// Token: 0x040000AA RID: 170
		private static volatile Lazy<IDbConnectionFactory> _defaultConnectionFactory = Database._defaultDefaultConnectionFactory;

		// Token: 0x040000AB RID: 171
		private readonly InternalContext _internalContext;

		// Token: 0x040000AC RID: 172
		private EntityTransaction _entityTransaction;

		// Token: 0x040000AD RID: 173
		private DbContextTransaction _dbContextTransaction;
	}
}
