using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002BC RID: 700
	internal class RootDependencyResolver : IDbDependencyResolver
	{
		// Token: 0x06002202 RID: 8706 RVA: 0x0005F8A7 File Offset: 0x0005DAA7
		public RootDependencyResolver()
			: this(new DefaultProviderServicesResolver(), new DatabaseInitializerResolver())
		{
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0005F8BC File Offset: 0x0005DABC
		public RootDependencyResolver(DefaultProviderServicesResolver defaultProviderServicesResolver, DatabaseInitializerResolver databaseInitializerResolver)
		{
			this._databaseInitializerResolver = databaseInitializerResolver;
			this._resolvers.Add(new TransactionContextInitializerResolver());
			this._resolvers.Add(this._databaseInitializerResolver);
			this._resolvers.Add(new DefaultExecutionStrategyResolver());
			this._resolvers.Add(new CachingDependencyResolver(defaultProviderServicesResolver));
			this._resolvers.Add(new CachingDependencyResolver(new DefaultProviderFactoryResolver()));
			this._resolvers.Add(new CachingDependencyResolver(new DefaultInvariantNameResolver()));
			this._resolvers.Add(new SingletonDependencyResolver<IDbConnectionFactory>(new LocalDbConnectionFactory()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<DbContext, IDbModelCacheKey>>(new Func<DbContext, IDbModelCacheKey>(new DefaultModelCacheKeyFactory().Create)));
			this._resolvers.Add(new SingletonDependencyResolver<IManifestTokenResolver>(new DefaultManifestTokenResolver()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<DbConnection, string, HistoryContext>>(HistoryContext.DefaultFactory));
			this._resolvers.Add(new SingletonDependencyResolver<IPluralizationService>(new EnglishPluralizationService()));
			this._resolvers.Add(new SingletonDependencyResolver<AttributeProvider>(new AttributeProvider()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<DbContext, Action<string>, DatabaseLogFormatter>>((DbContext c, Action<string> w) => new DatabaseLogFormatter(c, w)));
			this._resolvers.Add(new SingletonDependencyResolver<Func<TransactionHandler>>(() => new DefaultTransactionHandler(), (object k) => k is ExecutionStrategyKey));
			this._resolvers.Add(new SingletonDependencyResolver<IDbProviderFactoryResolver>(new DefaultDbProviderFactoryResolver()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<IMetadataAnnotationSerializer>>(() => new ClrTypeAnnotationSerializer(), "ClrType"));
			this._resolvers.Add(new SingletonDependencyResolver<Func<IMetadataAnnotationSerializer>>(() => new IndexAnnotationSerializer(), "Index"));
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x0005FAE6 File Offset: 0x0005DCE6
		public DatabaseInitializerResolver DatabaseInitializerResolver
		{
			get
			{
				return this._databaseInitializerResolver;
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0005FAEE File Offset: 0x0005DCEE
		public virtual object GetService(Type type, object key)
		{
			object obj;
			if ((obj = this._defaultResolvers.GetService(type, key)) == null)
			{
				obj = this._defaultProviderResolvers.GetService(type, key) ?? this._resolvers.GetService(type, key);
			}
			return obj;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x0005FB1F File Offset: 0x0005DD1F
		public virtual void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			this._defaultResolvers.Add(resolver);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x0005FB2D File Offset: 0x0005DD2D
		public virtual void SetDefaultProviderServices(DbProviderServices provider, string invariantName)
		{
			this._defaultProviderResolvers.Add(new SingletonDependencyResolver<DbProviderServices>(provider, invariantName));
			this._defaultProviderResolvers.Add(provider);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x0005FB4D File Offset: 0x0005DD4D
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this._defaultResolvers.GetServices(type, key).Concat(this._resolvers.GetServices(type, key));
		}

		// Token: 0x04000BD2 RID: 3026
		private readonly ResolverChain _defaultProviderResolvers = new ResolverChain();

		// Token: 0x04000BD3 RID: 3027
		private readonly ResolverChain _defaultResolvers = new ResolverChain();

		// Token: 0x04000BD4 RID: 3028
		private readonly ResolverChain _resolvers = new ResolverChain();

		// Token: 0x04000BD5 RID: 3029
		private readonly DatabaseInitializerResolver _databaseInitializerResolver;
	}
}
