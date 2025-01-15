using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity
{
	// Token: 0x02000059 RID: 89
	public class DbConfiguration
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00009985 File Offset: 0x00007B85
		protected internal DbConfiguration()
			: this(new InternalConfiguration(null, null, null, null, null))
		{
			this._internalConfiguration.Owner = this;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000099A3 File Offset: 0x00007BA3
		internal DbConfiguration(InternalConfiguration internalConfiguration)
		{
			this._internalConfiguration = internalConfiguration;
			this._internalConfiguration.Owner = this;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000099BE File Offset: 0x00007BBE
		public static void SetConfiguration(DbConfiguration configuration)
		{
			Check.NotNull<DbConfiguration>(configuration, "configuration");
			InternalConfiguration.Instance = configuration.InternalConfiguration;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000099D7 File Offset: 0x00007BD7
		public static void LoadConfiguration(Type contextType)
		{
			Check.NotNull<Type>(contextType, "contextType");
			if (!typeof(DbContext).IsAssignableFrom(contextType))
			{
				throw new ArgumentException(Strings.BadContextTypeForDiscovery(contextType.Name));
			}
			DbConfigurationManager.Instance.EnsureLoadedForContext(contextType);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009A13 File Offset: 0x00007C13
		public static void LoadConfiguration(Assembly assemblyHint)
		{
			Check.NotNull<Assembly>(assemblyHint, "assemblyHint");
			DbConfigurationManager.Instance.EnsureLoadedForAssembly(assemblyHint, null);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000252 RID: 594 RVA: 0x00009A2D File Offset: 0x00007C2D
		// (remove) Token: 0x06000253 RID: 595 RVA: 0x00009A46 File Offset: 0x00007C46
		public static event EventHandler<DbConfigurationLoadedEventArgs> Loaded
		{
			add
			{
				Check.NotNull<EventHandler<DbConfigurationLoadedEventArgs>>(value, "value");
				DbConfigurationManager.Instance.AddLoadedHandler(value);
			}
			remove
			{
				Check.NotNull<EventHandler<DbConfigurationLoadedEventArgs>>(value, "value");
				DbConfigurationManager.Instance.RemoveLoadedHandler(value);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009A5F File Offset: 0x00007C5F
		protected internal void AddDependencyResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDependencyResolver");
			this._internalConfiguration.AddDependencyResolver(resolver, false);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009A8A File Offset: 0x00007C8A
		protected internal void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			Check.NotNull<IDbDependencyResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("AddDefaultResolver");
			this._internalConfiguration.AddDefaultResolver(resolver);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00009AB4 File Offset: 0x00007CB4
		public static IDbDependencyResolver DependencyResolver
		{
			get
			{
				return InternalConfiguration.Instance.DependencyResolver;
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009AC0 File Offset: 0x00007CC0
		protected internal void SetProviderServices(string providerInvariantName, DbProviderServices provider)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<DbProviderServices>(provider, "provider");
			this._internalConfiguration.CheckNotLocked("SetProviderServices");
			this._internalConfiguration.RegisterSingleton<DbProviderServices>(provider, providerInvariantName);
			this.AddDefaultResolver(provider);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009B00 File Offset: 0x00007D00
		protected internal void SetProviderFactory(string providerInvariantName, DbProviderFactory providerFactory)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<DbProviderFactory>(providerFactory, "providerFactory");
			this._internalConfiguration.CheckNotLocked("SetProviderFactory");
			this._internalConfiguration.RegisterSingleton<DbProviderFactory>(providerFactory, providerInvariantName);
			this._internalConfiguration.AddDependencyResolver(new InvariantNameResolver(providerFactory, providerInvariantName), false);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009B55 File Offset: 0x00007D55
		protected internal void SetExecutionStrategy(string providerInvariantName, Func<IDbExecutionStrategy> getExecutionStrategy)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<IDbExecutionStrategy>>(getExecutionStrategy, "getExecutionStrategy");
			this._internalConfiguration.CheckNotLocked("SetExecutionStrategy");
			this._internalConfiguration.AddDependencyResolver(new ExecutionStrategyResolver<IDbExecutionStrategy>(providerInvariantName, null, getExecutionStrategy), false);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009B94 File Offset: 0x00007D94
		protected internal void SetExecutionStrategy(string providerInvariantName, Func<IDbExecutionStrategy> getExecutionStrategy, string serverName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotEmpty(serverName, "serverName");
			Check.NotNull<Func<IDbExecutionStrategy>>(getExecutionStrategy, "getExecutionStrategy");
			this._internalConfiguration.CheckNotLocked("SetExecutionStrategy");
			this._internalConfiguration.AddDependencyResolver(new ExecutionStrategyResolver<IDbExecutionStrategy>(providerInvariantName, serverName, getExecutionStrategy), false);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009BE9 File Offset: 0x00007DE9
		protected internal void SetDefaultTransactionHandler(Func<TransactionHandler> transactionHandlerFactory)
		{
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			this._internalConfiguration.CheckNotLocked("SetTransactionHandler");
			this._internalConfiguration.AddDependencyResolver(new TransactionHandlerResolver(transactionHandlerFactory, null, null), false);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009C1B File Offset: 0x00007E1B
		protected internal void SetTransactionHandler(string providerInvariantName, Func<TransactionHandler> transactionHandlerFactory)
		{
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			this._internalConfiguration.CheckNotLocked("SetTransactionHandler");
			this._internalConfiguration.AddDependencyResolver(new TransactionHandlerResolver(transactionHandlerFactory, providerInvariantName, null), false);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009C5C File Offset: 0x00007E5C
		protected internal void SetTransactionHandler(string providerInvariantName, Func<TransactionHandler> transactionHandlerFactory, string serverName)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<TransactionHandler>>(transactionHandlerFactory, "transactionHandlerFactory");
			Check.NotEmpty(serverName, "serverName");
			this._internalConfiguration.CheckNotLocked("SetTransactionHandler");
			this._internalConfiguration.AddDependencyResolver(new TransactionHandlerResolver(transactionHandlerFactory, providerInvariantName, serverName), false);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009CB1 File Offset: 0x00007EB1
		protected internal void SetDefaultConnectionFactory(IDbConnectionFactory connectionFactory)
		{
			Check.NotNull<IDbConnectionFactory>(connectionFactory, "connectionFactory");
			this._internalConfiguration.CheckNotLocked("SetDefaultConnectionFactory");
			this._internalConfiguration.RegisterSingleton<IDbConnectionFactory>(connectionFactory);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009CDB File Offset: 0x00007EDB
		protected internal void SetPluralizationService(IPluralizationService pluralizationService)
		{
			Check.NotNull<IPluralizationService>(pluralizationService, "pluralizationService");
			this._internalConfiguration.CheckNotLocked("SetPluralizationService");
			this._internalConfiguration.RegisterSingleton<IPluralizationService>(pluralizationService);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009D05 File Offset: 0x00007F05
		protected internal void SetDatabaseInitializer<TContext>(IDatabaseInitializer<TContext> initializer) where TContext : DbContext
		{
			this._internalConfiguration.CheckNotLocked("SetDatabaseInitializer");
			this._internalConfiguration.RegisterSingleton<IDatabaseInitializer<TContext>>(initializer ?? new NullDatabaseInitializer<TContext>());
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009D2C File Offset: 0x00007F2C
		protected internal void SetMigrationSqlGenerator(string providerInvariantName, Func<MigrationSqlGenerator> sqlGenerator)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<MigrationSqlGenerator>>(sqlGenerator, "sqlGenerator");
			this._internalConfiguration.CheckNotLocked("SetMigrationSqlGenerator");
			this._internalConfiguration.RegisterSingleton<Func<MigrationSqlGenerator>>(sqlGenerator, providerInvariantName);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009D63 File Offset: 0x00007F63
		protected internal void SetManifestTokenResolver(IManifestTokenResolver resolver)
		{
			Check.NotNull<IManifestTokenResolver>(resolver, "resolver");
			this._internalConfiguration.CheckNotLocked("SetManifestTokenResolver");
			this._internalConfiguration.RegisterSingleton<IManifestTokenResolver>(resolver);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009D8D File Offset: 0x00007F8D
		protected internal void SetMetadataAnnotationSerializer(string annotationName, Func<IMetadataAnnotationSerializer> serializerFactory)
		{
			Check.NotEmpty(annotationName, "annotationName");
			Check.NotNull<Func<IMetadataAnnotationSerializer>>(serializerFactory, "serializerFactory");
			this._internalConfiguration.CheckNotLocked("SetMetadataAnnotationSerializer");
			this._internalConfiguration.RegisterSingleton<Func<IMetadataAnnotationSerializer>>(serializerFactory, annotationName);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009DC4 File Offset: 0x00007FC4
		protected internal void SetProviderFactoryResolver(IDbProviderFactoryResolver providerFactoryResolver)
		{
			Check.NotNull<IDbProviderFactoryResolver>(providerFactoryResolver, "providerFactoryResolver");
			this._internalConfiguration.CheckNotLocked("SetProviderFactoryResolver");
			this._internalConfiguration.RegisterSingleton<IDbProviderFactoryResolver>(providerFactoryResolver);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009DEE File Offset: 0x00007FEE
		protected internal void SetModelCacheKey(Func<DbContext, IDbModelCacheKey> keyFactory)
		{
			Check.NotNull<Func<DbContext, IDbModelCacheKey>>(keyFactory, "keyFactory");
			this._internalConfiguration.CheckNotLocked("SetModelCacheKey");
			this._internalConfiguration.RegisterSingleton<Func<DbContext, IDbModelCacheKey>>(keyFactory);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009E18 File Offset: 0x00008018
		protected internal void SetDefaultHistoryContext(Func<DbConnection, string, HistoryContext> factory)
		{
			Check.NotNull<Func<DbConnection, string, HistoryContext>>(factory, "factory");
			this._internalConfiguration.CheckNotLocked("SetDefaultHistoryContext");
			this._internalConfiguration.RegisterSingleton<Func<DbConnection, string, HistoryContext>>(factory);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009E42 File Offset: 0x00008042
		protected internal void SetHistoryContext(string providerInvariantName, Func<DbConnection, string, HistoryContext> factory)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<Func<DbConnection, string, HistoryContext>>(factory, "factory");
			this._internalConfiguration.CheckNotLocked("SetHistoryContext");
			this._internalConfiguration.RegisterSingleton<Func<DbConnection, string, HistoryContext>>(factory, providerInvariantName);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009E79 File Offset: 0x00008079
		protected internal void SetDefaultSpatialServices(DbSpatialServices spatialProvider)
		{
			Check.NotNull<DbSpatialServices>(spatialProvider, "spatialProvider");
			this._internalConfiguration.CheckNotLocked("SetDefaultSpatialServices");
			this._internalConfiguration.RegisterSingleton<DbSpatialServices>(spatialProvider);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009EA3 File Offset: 0x000080A3
		protected internal void SetSpatialServices(DbProviderInfo key, DbSpatialServices spatialProvider)
		{
			Check.NotNull<DbProviderInfo>(key, "key");
			Check.NotNull<DbSpatialServices>(spatialProvider, "spatialProvider");
			this._internalConfiguration.CheckNotLocked("SetSpatialServices");
			this._internalConfiguration.RegisterSingleton<DbSpatialServices>(spatialProvider, key);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009EDA File Offset: 0x000080DA
		protected internal void SetSpatialServices(string providerInvariantName, DbSpatialServices spatialProvider)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<DbSpatialServices>(spatialProvider, "spatialProvider");
			this._internalConfiguration.CheckNotLocked("SetSpatialServices");
			this.RegisterSpatialServices(providerInvariantName, spatialProvider);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009F0C File Offset: 0x0000810C
		private void RegisterSpatialServices(string providerInvariantName, DbSpatialServices spatialProvider)
		{
			this._internalConfiguration.RegisterSingleton<DbSpatialServices>(spatialProvider, delegate(object k)
			{
				DbProviderInfo dbProviderInfo = k as DbProviderInfo;
				return dbProviderInfo != null && dbProviderInfo.ProviderInvariantName == providerInvariantName;
			});
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009F3E File Offset: 0x0000813E
		protected internal void SetDatabaseLogFormatter(Func<DbContext, Action<string>, DatabaseLogFormatter> logFormatterFactory)
		{
			Check.NotNull<Func<DbContext, Action<string>, DatabaseLogFormatter>>(logFormatterFactory, "logFormatterFactory");
			this._internalConfiguration.CheckNotLocked("SetDatabaseLogFormatter");
			this._internalConfiguration.RegisterSingleton<Func<DbContext, Action<string>, DatabaseLogFormatter>>(logFormatterFactory);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009F68 File Offset: 0x00008168
		protected internal void AddInterceptor(IDbInterceptor interceptor)
		{
			Check.NotNull<IDbInterceptor>(interceptor, "interceptor");
			this._internalConfiguration.CheckNotLocked("AddInterceptor");
			this._internalConfiguration.RegisterSingleton<IDbInterceptor>(interceptor);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009F94 File Offset: 0x00008194
		protected internal void SetContextFactory(Type contextType, Func<DbContext> factory)
		{
			Check.NotNull<Type>(contextType, "contextType");
			Check.NotNull<Func<DbContext>>(factory, "factory");
			if (!typeof(DbContext).IsAssignableFrom(contextType))
			{
				throw new ArgumentException(Strings.ContextFactoryContextType(contextType.FullName));
			}
			this._internalConfiguration.CheckNotLocked("SetContextFactory");
			this._internalConfiguration.RegisterSingleton<Func<DbContext>>(factory, contextType);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009FF9 File Offset: 0x000081F9
		protected internal void SetContextFactory<TContext>(Func<TContext> factory) where TContext : DbContext
		{
			Check.NotNull<Func<TContext>>(factory, "factory");
			this.SetContextFactory(typeof(TContext), factory);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A018 File Offset: 0x00008218
		protected internal void SetModelStore(DbModelStore modelStore)
		{
			Check.NotNull<DbModelStore>(modelStore, "modelStore");
			this._internalConfiguration.CheckNotLocked("SetModelStore");
			this._internalConfiguration.RegisterSingleton<DbModelStore>(modelStore);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A042 File Offset: 0x00008242
		protected internal void SetTableExistenceChecker(string providerInvariantName, TableExistenceChecker tableExistenceChecker)
		{
			Check.NotEmpty(providerInvariantName, "providerInvariantName");
			Check.NotNull<TableExistenceChecker>(tableExistenceChecker, "tableExistenceChecker");
			this._internalConfiguration.CheckNotLocked("SetTableExistenceChecker");
			this._internalConfiguration.RegisterSingleton<TableExistenceChecker>(tableExistenceChecker, providerInvariantName);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000A079 File Offset: 0x00008279
		internal virtual InternalConfiguration InternalConfiguration
		{
			get
			{
				return this._internalConfiguration;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000A081 File Offset: 0x00008281
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000A089 File Offset: 0x00008289
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A092 File Offset: 0x00008292
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A09A File Offset: 0x0000829A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A0A2 File Offset: 0x000082A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x040000AE RID: 174
		private readonly InternalConfiguration _internalConfiguration;
	}
}
