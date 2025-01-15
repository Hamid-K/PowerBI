using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000124 RID: 292
	internal class LazyInternalContext : InternalContext
	{
		// Token: 0x06001456 RID: 5206 RVA: 0x00034CC0 File Offset: 0x00032EC0
		public LazyInternalContext(DbContext owner, IInternalConnection internalConnection, DbCompiledModel model, Func<DbContext, IDbModelCacheKey> cacheKeyFactory = null, AttributeProvider attributeProvider = null, Lazy<DbDispatchers> dispatchers = null, ObjectContext objectContext = null)
			: base(owner, dispatchers)
		{
			this._internalConnection = internalConnection;
			this._model = model;
			this._cacheKeyFactory = cacheKeyFactory ?? new Func<DbContext, IDbModelCacheKey>(new DefaultModelCacheKeyFactory().Create);
			this._attributeProvider = attributeProvider ?? new AttributeProvider();
			this._objectContext = objectContext;
			this._createdWithExistingModel = model != null;
			base.LoadContextConfigs();
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00034D3E File Offset: 0x00032F3E
		public override ObjectContext ObjectContext
		{
			get
			{
				base.Initialize();
				return this.ObjectContextInUse;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00034D4C File Offset: 0x00032F4C
		public override DbCompiledModel CodeFirstModel
		{
			get
			{
				this.InitializeContext();
				return this._model;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00034D5A File Offset: 0x00032F5A
		public override DbModel ModelBeingInitialized
		{
			get
			{
				this.InitializeContext();
				return this._modelBeingInitialized;
			}
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00034D68 File Offset: 0x00032F68
		public override ObjectContext GetObjectContextWithoutDatabaseInitialization()
		{
			this.InitializeContext();
			return this.ObjectContextInUse;
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00034D76 File Offset: 0x00032F76
		public virtual ObjectContext ObjectContextInUse
		{
			get
			{
				return base.TempObjectContext ?? this._objectContext;
			}
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00034D88 File Offset: 0x00032F88
		public override int SaveChanges()
		{
			if (this.ObjectContextInUse != null)
			{
				return base.SaveChanges();
			}
			return 0;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00034D9A File Offset: 0x00032F9A
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this.ObjectContextInUse != null)
			{
				return base.SaveChangesAsync(cancellationToken);
			}
			return Task.FromResult<int>(0);
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00034DB9 File Offset: 0x00032FB9
		public override void DisposeContext(bool disposing)
		{
			if (!base.IsDisposed)
			{
				base.DisposeContext(disposing);
				if (disposing)
				{
					if (this._objectContext != null)
					{
						this._objectContext.Dispose();
					}
					this._internalConnection.Dispose();
				}
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00034DEB File Offset: 0x00032FEB
		public override DbConnection Connection
		{
			get
			{
				base.CheckContextNotDisposed();
				if (base.TempObjectContext != null)
				{
					return ((EntityConnection)base.TempObjectContext.Connection).StoreConnection;
				}
				return this._internalConnection.Connection;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001460 RID: 5216 RVA: 0x00034E1C File Offset: 0x0003301C
		public override string OriginalConnectionString
		{
			get
			{
				return this._internalConnection.OriginalConnectionString;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00034E29 File Offset: 0x00033029
		public override DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._internalConnection.ConnectionStringOrigin;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x00034E3C File Offset: 0x0003303C
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00034E44 File Offset: 0x00033044
		public override AppConfig AppConfig
		{
			get
			{
				return base.AppConfig;
			}
			set
			{
				base.AppConfig = value;
				this._internalConnection.AppConfig = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x00034E59 File Offset: 0x00033059
		public override string ConnectionStringName
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._internalConnection.ConnectionStringName;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x00034E6C File Offset: 0x0003306C
		// (set) Token: 0x06001466 RID: 5222 RVA: 0x00034E7A File Offset: 0x0003307A
		public override DbProviderInfo ModelProviderInfo
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._modelProviderInfo;
			}
			set
			{
				base.CheckContextNotDisposed();
				this._modelProviderInfo = value;
				this._internalConnection.ProviderName = this._modelProviderInfo.ProviderInvariantName;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00034E9F File Offset: 0x0003309F
		public override string ProviderName
		{
			get
			{
				return this._internalConnection.ProviderName;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00034EAC File Offset: 0x000330AC
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x00034EBA File Offset: 0x000330BA
		public override Action<DbModelBuilder> OnModelCreating
		{
			get
			{
				base.CheckContextNotDisposed();
				return this._onModelCreating;
			}
			set
			{
				base.CheckContextNotDisposed();
				this._onModelCreating = value;
			}
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00034ECC File Offset: 0x000330CC
		public override void OverrideConnection(IInternalConnection connection)
		{
			connection.AppConfig = this.AppConfig;
			if (connection.ConnectionHasModel != this._internalConnection.ConnectionHasModel)
			{
				throw this._internalConnection.ConnectionHasModel ? Error.LazyInternalContext_CannotReplaceEfConnectionWithDbConnection() : Error.LazyInternalContext_CannotReplaceDbConnectionWithEfConnection();
			}
			this._internalConnection.Dispose();
			this._internalConnection = connection;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00034F24 File Offset: 0x00033124
		protected override void InitializeContext()
		{
			base.CheckContextNotDisposed();
			if (this._objectContext == null)
			{
				if (this._creatingModel)
				{
					throw Error.DbContext_ContextUsedInModelCreating();
				}
				try
				{
					DbContextInfo currentInfo = DbContextInfo.CurrentInfo;
					if (currentInfo != null)
					{
						base.ApplyContextInfo(currentInfo);
					}
					this._creatingModel = true;
					if (this._createdWithExistingModel)
					{
						if (this._internalConnection.ConnectionHasModel)
						{
							throw Error.DbContext_ConnectionHasModel();
						}
						this._objectContext = this._model.CreateObjectContext<ObjectContext>(this._internalConnection.Connection);
					}
					else if (this._internalConnection.ConnectionHasModel)
					{
						this._objectContext = this._internalConnection.CreateObjectContextFromConnectionModel();
					}
					else
					{
						IDbModelCacheKey dbModelCacheKey = this._cacheKeyFactory(base.Owner);
						DbCompiledModel value = LazyInternalContext._cachedModels.GetOrAdd(dbModelCacheKey, (IDbModelCacheKey t) => new RetryLazy<LazyInternalContext, DbCompiledModel>(new Func<LazyInternalContext, DbCompiledModel>(LazyInternalContext.CreateModel))).GetValue(this);
						this._objectContext = value.CreateObjectContext<ObjectContext>(this._internalConnection.Connection);
						this._model = value;
					}
					this._objectContext.ContextOptions.EnsureTransactionsForFunctionsAndCommands = this._initialEnsureTransactionsForFunctionsAndCommands;
					this._objectContext.ContextOptions.LazyLoadingEnabled = this._initialLazyLoadingFlag;
					this._objectContext.ContextOptions.ProxyCreationEnabled = this._initialProxyCreationFlag;
					this._objectContext.ContextOptions.UseCSharpNullComparisonBehavior = !this._useDatabaseNullSemanticsFlag;
					this._objectContext.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions = this._disableFilterOverProjectionSimplificationForCustomFunctions;
					this._objectContext.CommandTimeout = this._commandTimeout;
					this._objectContext.ContextOptions.UseConsistentNullReferenceBehavior = true;
					this._objectContext.InterceptionContext = this._objectContext.InterceptionContext.WithDbContext(base.Owner);
					base.ResetDbSets();
					this._objectContext.InitializeMappingViewCacheFactory(base.Owner);
				}
				finally
				{
					this._creatingModel = false;
				}
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00035118 File Offset: 0x00033318
		public static DbCompiledModel CreateModel(LazyInternalContext internalContext)
		{
			Type type = internalContext.Owner.GetType();
			DbModelStore dbModelStore = null;
			if (!(internalContext.Owner is HistoryContext))
			{
				dbModelStore = DbConfiguration.DependencyResolver.GetService<DbModelStore>();
				if (dbModelStore != null)
				{
					DbCompiledModel dbCompiledModel = dbModelStore.TryLoad(type);
					if (dbCompiledModel != null)
					{
						return dbCompiledModel;
					}
				}
			}
			DbModelBuilder dbModelBuilder = internalContext.CreateModelBuilder();
			DbModel dbModel = ((internalContext._modelProviderInfo == null) ? dbModelBuilder.Build(internalContext._internalConnection.Connection) : dbModelBuilder.Build(internalContext._modelProviderInfo));
			internalContext._modelBeingInitialized = dbModel;
			if (dbModelStore != null)
			{
				dbModelStore.Save(type, dbModel);
			}
			return dbModel.Compile();
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000351A8 File Offset: 0x000333A8
		public DbModelBuilder CreateModelBuilder()
		{
			DbModelBuilderVersionAttribute dbModelBuilderVersionAttribute = this._attributeProvider.GetAttributes(base.Owner.GetType()).OfType<DbModelBuilderVersionAttribute>().FirstOrDefault<DbModelBuilderVersionAttribute>();
			DbModelBuilder dbModelBuilder = new DbModelBuilder((dbModelBuilderVersionAttribute != null) ? dbModelBuilderVersionAttribute.Version : DbModelBuilderVersion.Latest);
			string text = LazyInternalContext.StripInvalidCharacters(base.Owner.GetType().Namespace);
			if (!string.IsNullOrWhiteSpace(text))
			{
				dbModelBuilder.Conventions.Add(new IConvention[]
				{
					new ModelNamespaceConvention(text)
				});
			}
			string text2 = LazyInternalContext.StripInvalidCharacters(base.Owner.GetType().Name);
			if (!string.IsNullOrWhiteSpace(text2))
			{
				dbModelBuilder.Conventions.Add(new IConvention[]
				{
					new ModelContainerConvention(text2)
				});
			}
			new DbSetDiscoveryService(base.Owner).RegisterSets(dbModelBuilder);
			base.Owner.CallOnModelCreating(dbModelBuilder);
			if (this.OnModelCreating != null)
			{
				this.OnModelCreating(dbModelBuilder);
			}
			return dbModelBuilder;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0003528C File Offset: 0x0003348C
		private static string StripInvalidCharacters(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(value.Length);
			bool flag = true;
			foreach (char c in value)
			{
				if (c == '.')
				{
					if (!flag)
					{
						stringBuilder.Append(c);
					}
				}
				else
				{
					switch (char.GetUnicodeCategory(c))
					{
					case UnicodeCategory.UppercaseLetter:
					case UnicodeCategory.LowercaseLetter:
					case UnicodeCategory.TitlecaseLetter:
					case UnicodeCategory.ModifierLetter:
					case UnicodeCategory.OtherLetter:
					case UnicodeCategory.LetterNumber:
						flag = false;
						stringBuilder.Append(c);
						break;
					case UnicodeCategory.NonSpacingMark:
					case UnicodeCategory.SpacingCombiningMark:
					case UnicodeCategory.DecimalDigitNumber:
					case UnicodeCategory.ConnectorPunctuation:
						if (!flag)
						{
							stringBuilder.Append(c);
						}
						break;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0003536C File Offset: 0x0003356C
		public override void MarkDatabaseNotInitialized()
		{
			if (!base.InInitializationAction)
			{
				RetryAction<InternalContext> retryAction;
				LazyInternalContext.InitializedDatabases.TryRemove(Tuple.Create<DbCompiledModel, string>(this._model, this._internalConnection.ConnectionKey), out retryAction);
			}
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000353A4 File Offset: 0x000335A4
		public override void MarkDatabaseInitialized()
		{
			this.InitializeContext();
			this.InitializeDatabaseAction(delegate(InternalContext c)
			{
			});
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000353D1 File Offset: 0x000335D1
		protected override void InitializeDatabase()
		{
			this.InitializeDatabaseAction(delegate(InternalContext c)
			{
				c.PerformDatabaseInitialization();
			});
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000353F8 File Offset: 0x000335F8
		private void InitializeDatabaseAction(Action<InternalContext> action)
		{
			if (!this._inDatabaseInitialization && !base.InitializerDisabled)
			{
				try
				{
					this._inDatabaseInitialization = true;
					LazyInternalContext.InitializedDatabases.GetOrAdd(Tuple.Create<DbCompiledModel, string>(this._model, this._internalConnection.ConnectionKey), (Tuple<DbCompiledModel, string> t) => new RetryAction<InternalContext>(action)).PerformAction(this);
				}
				finally
				{
					this._inDatabaseInitialization = false;
					this._modelBeingInitialized = null;
				}
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0003547C File Offset: 0x0003367C
		public override IDatabaseInitializer<DbContext> DefaultInitializer
		{
			get
			{
				if (this._model == null)
				{
					return null;
				}
				return LazyInternalContext._defaultCodeFirstInitializer;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00035490 File Offset: 0x00033690
		// (set) Token: 0x06001475 RID: 5237 RVA: 0x000354BC File Offset: 0x000336BC
		public override bool EnsureTransactionsForFunctionsAndCommands
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._initialEnsureTransactionsForFunctionsAndCommands;
				}
				return objectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.EnsureTransactionsForFunctionsAndCommands = value;
					return;
				}
				this._initialEnsureTransactionsForFunctionsAndCommands = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x000354E8 File Offset: 0x000336E8
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x00035514 File Offset: 0x00033714
		public override bool LazyLoadingEnabled
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._initialLazyLoadingFlag;
				}
				return objectContextInUse.ContextOptions.LazyLoadingEnabled;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.LazyLoadingEnabled = value;
					return;
				}
				this._initialLazyLoadingFlag = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00035540 File Offset: 0x00033740
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x0003556C File Offset: 0x0003376C
		public override bool ProxyCreationEnabled
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._initialProxyCreationFlag;
				}
				return objectContextInUse.ContextOptions.ProxyCreationEnabled;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.ProxyCreationEnabled = value;
					return;
				}
				this._initialProxyCreationFlag = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00035598 File Offset: 0x00033798
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x000355C4 File Offset: 0x000337C4
		public override bool UseDatabaseNullSemantics
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._useDatabaseNullSemanticsFlag;
				}
				return !objectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.UseCSharpNullComparisonBehavior = !value;
					return;
				}
				this._useDatabaseNullSemanticsFlag = value;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x000355F4 File Offset: 0x000337F4
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x00035620 File Offset: 0x00033820
		public override bool DisableFilterOverProjectionSimplificationForCustomFunctions
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._disableFilterOverProjectionSimplificationForCustomFunctions;
				}
				return !objectContextInUse.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions = !value;
					return;
				}
				this._disableFilterOverProjectionSimplificationForCustomFunctions = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00035650 File Offset: 0x00033850
		// (set) Token: 0x0600147F RID: 5247 RVA: 0x00035674 File Offset: 0x00033874
		public override int? CommandTimeout
		{
			get
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse == null)
				{
					return this._commandTimeout;
				}
				return objectContextInUse.CommandTimeout;
			}
			set
			{
				ObjectContext objectContextInUse = this.ObjectContextInUse;
				if (objectContextInUse != null)
				{
					objectContextInUse.CommandTimeout = value;
					return;
				}
				this._commandTimeout = value;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0003569A File Offset: 0x0003389A
		public override string DefaultSchema
		{
			get
			{
				return this.CodeFirstModel.DefaultSchema;
			}
		}

		// Token: 0x0400098D RID: 2445
		private static readonly CreateDatabaseIfNotExists<DbContext> _defaultCodeFirstInitializer = new CreateDatabaseIfNotExists<DbContext>();

		// Token: 0x0400098E RID: 2446
		private static readonly ConcurrentDictionary<IDbModelCacheKey, RetryLazy<LazyInternalContext, DbCompiledModel>> _cachedModels = new ConcurrentDictionary<IDbModelCacheKey, RetryLazy<LazyInternalContext, DbCompiledModel>>();

		// Token: 0x0400098F RID: 2447
		private static readonly ConcurrentDictionary<Tuple<DbCompiledModel, string>, RetryAction<InternalContext>> InitializedDatabases = new ConcurrentDictionary<Tuple<DbCompiledModel, string>, RetryAction<InternalContext>>();

		// Token: 0x04000990 RID: 2448
		private IInternalConnection _internalConnection;

		// Token: 0x04000991 RID: 2449
		private bool _creatingModel;

		// Token: 0x04000992 RID: 2450
		private ObjectContext _objectContext;

		// Token: 0x04000993 RID: 2451
		private DbCompiledModel _model;

		// Token: 0x04000994 RID: 2452
		private readonly bool _createdWithExistingModel;

		// Token: 0x04000995 RID: 2453
		private bool _initialEnsureTransactionsForFunctionsAndCommands = true;

		// Token: 0x04000996 RID: 2454
		private bool _initialLazyLoadingFlag = true;

		// Token: 0x04000997 RID: 2455
		private bool _initialProxyCreationFlag = true;

		// Token: 0x04000998 RID: 2456
		private bool _useDatabaseNullSemanticsFlag;

		// Token: 0x04000999 RID: 2457
		private int? _commandTimeout;

		// Token: 0x0400099A RID: 2458
		private bool _inDatabaseInitialization;

		// Token: 0x0400099B RID: 2459
		private Action<DbModelBuilder> _onModelCreating;

		// Token: 0x0400099C RID: 2460
		private readonly Func<DbContext, IDbModelCacheKey> _cacheKeyFactory;

		// Token: 0x0400099D RID: 2461
		private readonly AttributeProvider _attributeProvider;

		// Token: 0x0400099E RID: 2462
		private DbModel _modelBeingInitialized;

		// Token: 0x0400099F RID: 2463
		private DbProviderInfo _modelProviderInfo;

		// Token: 0x040009A0 RID: 2464
		private bool _disableFilterOverProjectionSimplificationForCustomFunctions;
	}
}
