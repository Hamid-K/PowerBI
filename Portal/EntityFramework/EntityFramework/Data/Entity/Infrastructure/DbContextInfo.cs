using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000225 RID: 549
	public class DbContextInfo
	{
		// Token: 0x06001CB4 RID: 7348 RVA: 0x00051FC4 File Offset: 0x000501C4
		public DbContextInfo(Type contextType)
			: this(contextType, null)
		{
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00051FCE File Offset: 0x000501CE
		internal DbContextInfo(Type contextType, Func<IDbDependencyResolver> resolver)
			: this(Check.NotNull<Type>(contextType, "contextType"), null, AppConfig.DefaultInstance, null, resolver)
		{
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00051FE9 File Offset: 0x000501E9
		public DbContextInfo(Type contextType, DbConnectionInfo connectionInfo)
			: this(Check.NotNull<Type>(contextType, "contextType"), null, AppConfig.DefaultInstance, Check.NotNull<DbConnectionInfo>(connectionInfo, "connectionInfo"), null)
		{
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0005200E File Offset: 0x0005020E
		[Obsolete("The application configuration can contain multiple settings that affect the connection used by a DbContext. To ensure all configuration is taken into account, use a DbContextInfo constructor that accepts System.Configuration.Configuration")]
		public DbContextInfo(Type contextType, ConnectionStringSettingsCollection connectionStringSettings)
			: this(Check.NotNull<Type>(contextType, "contextType"), null, new AppConfig(Check.NotNull<ConnectionStringSettingsCollection>(connectionStringSettings, "connectionStringSettings")), null, null)
		{
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00052034 File Offset: 0x00050234
		public DbContextInfo(Type contextType, Configuration config)
			: this(Check.NotNull<Type>(contextType, "contextType"), null, new AppConfig(Check.NotNull<Configuration>(config, "config")), null, null)
		{
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0005205A File Offset: 0x0005025A
		public DbContextInfo(Type contextType, Configuration config, DbConnectionInfo connectionInfo)
			: this(Check.NotNull<Type>(contextType, "contextType"), null, new AppConfig(Check.NotNull<Configuration>(config, "config")), Check.NotNull<DbConnectionInfo>(connectionInfo, "connectionInfo"), null)
		{
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0005208A File Offset: 0x0005028A
		public DbContextInfo(Type contextType, DbProviderInfo modelProviderInfo)
			: this(Check.NotNull<Type>(contextType, "contextType"), Check.NotNull<DbProviderInfo>(modelProviderInfo, "modelProviderInfo"), AppConfig.DefaultInstance, null, null)
		{
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x000520AF File Offset: 0x000502AF
		public DbContextInfo(Type contextType, Configuration config, DbProviderInfo modelProviderInfo)
			: this(Check.NotNull<Type>(contextType, "contextType"), Check.NotNull<DbProviderInfo>(modelProviderInfo, "modelProviderInfo"), new AppConfig(Check.NotNull<Configuration>(config, "config")), null, null)
		{
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x000520E0 File Offset: 0x000502E0
		internal DbContextInfo(DbContext context, Func<IDbDependencyResolver> resolver = null)
		{
			this._resolver = () => DbConfiguration.DependencyResolver;
			base..ctor();
			Check.NotNull<DbContext>(context, "context");
			Func<IDbDependencyResolver> func = resolver;
			if (resolver == null && (func = DbContextInfo.<>c.<>9__21_0) == null)
			{
				func = (DbContextInfo.<>c.<>9__21_0 = () => DbConfiguration.DependencyResolver);
			}
			this._resolver = func;
			this._contextType = context.GetType();
			this._appConfig = AppConfig.DefaultInstance;
			InternalContext internalContext = context.InternalContext;
			this._connectionProviderName = internalContext.ProviderName;
			this._connectionInfo = new DbConnectionInfo(internalContext.OriginalConnectionString, this._connectionProviderName);
			this._connectionString = internalContext.OriginalConnectionString;
			this._connectionStringName = internalContext.ConnectionStringName;
			this._connectionStringOrigin = internalContext.ConnectionStringOrigin;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x000521B4 File Offset: 0x000503B4
		private DbContextInfo(Type contextType, DbProviderInfo modelProviderInfo, AppConfig config, DbConnectionInfo connectionInfo, Func<IDbDependencyResolver> resolver = null)
		{
			this._resolver = () => DbConfiguration.DependencyResolver;
			base..ctor();
			if (!typeof(DbContext).IsAssignableFrom(contextType))
			{
				throw new ArgumentOutOfRangeException("contextType");
			}
			Func<IDbDependencyResolver> func = resolver;
			if (resolver == null && (func = DbContextInfo.<>c.<>9__22_0) == null)
			{
				func = (DbContextInfo.<>c.<>9__22_0 = () => DbConfiguration.DependencyResolver);
			}
			this._resolver = func;
			this._contextType = contextType;
			this._modelProviderInfo = modelProviderInfo;
			this._appConfig = config;
			this._connectionInfo = connectionInfo;
			this._activator = this.CreateActivator();
			if (this._activator != null)
			{
				DbContext dbContext = this.CreateInstance();
				if (dbContext != null)
				{
					this._isConstructible = true;
					using (dbContext)
					{
						this._connectionString = DbInterception.Dispatch.Connection.GetConnectionString(dbContext.InternalContext.Connection, new DbInterceptionContext().WithDbContext(dbContext));
						this._connectionStringName = dbContext.InternalContext.ConnectionStringName;
						this._connectionProviderName = dbContext.InternalContext.ProviderName;
						this._connectionStringOrigin = dbContext.InternalContext.ConnectionStringOrigin;
					}
				}
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000522F4 File Offset: 0x000504F4
		public virtual Type ContextType
		{
			get
			{
				return this._contextType;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x000522FC File Offset: 0x000504FC
		public virtual bool IsConstructible
		{
			get
			{
				return this._isConstructible;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00052304 File Offset: 0x00050504
		public virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0005230C File Offset: 0x0005050C
		public virtual string ConnectionStringName
		{
			get
			{
				return this._connectionStringName;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00052314 File Offset: 0x00050514
		public virtual string ConnectionProviderName
		{
			get
			{
				return this._connectionProviderName;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x0005231C File Offset: 0x0005051C
		public virtual DbConnectionStringOrigin ConnectionStringOrigin
		{
			get
			{
				return this._connectionStringOrigin;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00052324 File Offset: 0x00050524
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0005232C File Offset: 0x0005052C
		public virtual Action<DbModelBuilder> OnModelCreating
		{
			get
			{
				return this._onModelCreating;
			}
			set
			{
				this._onModelCreating = value;
			}
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00052338 File Offset: 0x00050538
		public virtual DbContext CreateInstance()
		{
			bool flag = DbConfigurationManager.Instance.PushConfiguration(this._appConfig, this._contextType);
			DbContextInfo.CurrentInfo = this;
			DbContext dbContext = null;
			DbContext dbContext2;
			try
			{
				try
				{
					dbContext = ((this._activator == null) ? null : this._activator());
				}
				catch (TargetInvocationException ex)
				{
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
					throw ex.InnerException;
				}
				if (dbContext == null)
				{
					dbContext2 = null;
				}
				else
				{
					dbContext.InternalContext.OnDisposing += delegate(object _, EventArgs __)
					{
						DbContextInfo.CurrentInfo = null;
					};
					if (flag)
					{
						dbContext.InternalContext.OnDisposing += delegate(object _, EventArgs __)
						{
							DbConfigurationManager.Instance.PopConfiguration(this._appConfig);
						};
					}
					dbContext.InternalContext.ApplyContextInfo(this);
					dbContext2 = dbContext;
				}
			}
			catch (Exception)
			{
				if (dbContext != null)
				{
					dbContext.Dispose();
				}
				throw;
			}
			finally
			{
				if (dbContext == null)
				{
					DbContextInfo.CurrentInfo = null;
					if (flag)
					{
						DbConfigurationManager.Instance.PopConfiguration(this._appConfig);
					}
				}
			}
			return dbContext2;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00052440 File Offset: 0x00050640
		internal void ConfigureContext(DbContext context)
		{
			if (this._modelProviderInfo != null)
			{
				context.InternalContext.ModelProviderInfo = this._modelProviderInfo;
			}
			context.InternalContext.AppConfig = this._appConfig;
			if (this._connectionInfo != null)
			{
				context.InternalContext.OverrideConnection(new LazyInternalConnection(context, this._connectionInfo));
			}
			else if (this._modelProviderInfo != null && this._appConfig == AppConfig.DefaultInstance)
			{
				context.InternalContext.OverrideConnection(new EagerInternalConnection(context, this._resolver().GetService(this._modelProviderInfo.ProviderInvariantName).CreateConnection(), true));
			}
			if (this._onModelCreating != null)
			{
				context.InternalContext.OnModelCreating = this._onModelCreating;
			}
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000524F8 File Offset: 0x000506F8
		private Func<DbContext> CreateActivator()
		{
			if (this._contextType.GetPublicConstructor(new Type[0]) != null)
			{
				return () => (DbContext)Activator.CreateInstance(this._contextType);
			}
			Func<DbContext> service = this._resolver().GetService(this._contextType);
			if (service != null)
			{
				return service;
			}
			Type type = (from t in this._contextType.Assembly().GetAccessibleTypes()
				where t.IsClass() && typeof(IDbContextFactory<>).MakeGenericType(new Type[] { this._contextType }).IsAssignableFrom(t)
				select t).FirstOrDefault<Type>();
			if (type == null)
			{
				return null;
			}
			if (type.GetPublicConstructor(new Type[0]) == null)
			{
				throw Error.DbContextServices_MissingDefaultCtor(type);
			}
			return new Func<DbContext>(((IDbContextFactory<DbContext>)Activator.CreateInstance(type)).Create);
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x000525AB File Offset: 0x000507AB
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x000525B2 File Offset: 0x000507B2
		internal static DbContextInfo CurrentInfo
		{
			get
			{
				return DbContextInfo._currentInfo;
			}
			set
			{
				DbContextInfo._currentInfo = value;
			}
		}

		// Token: 0x04000B03 RID: 2819
		[ThreadStatic]
		private static DbContextInfo _currentInfo;

		// Token: 0x04000B04 RID: 2820
		private readonly Type _contextType;

		// Token: 0x04000B05 RID: 2821
		private readonly DbProviderInfo _modelProviderInfo;

		// Token: 0x04000B06 RID: 2822
		private readonly DbConnectionInfo _connectionInfo;

		// Token: 0x04000B07 RID: 2823
		private readonly AppConfig _appConfig;

		// Token: 0x04000B08 RID: 2824
		private readonly Func<DbContext> _activator;

		// Token: 0x04000B09 RID: 2825
		private readonly string _connectionString;

		// Token: 0x04000B0A RID: 2826
		private readonly string _connectionProviderName;

		// Token: 0x04000B0B RID: 2827
		private readonly bool _isConstructible;

		// Token: 0x04000B0C RID: 2828
		private readonly DbConnectionStringOrigin _connectionStringOrigin;

		// Token: 0x04000B0D RID: 2829
		private readonly string _connectionStringName;

		// Token: 0x04000B0E RID: 2830
		private readonly Func<IDbDependencyResolver> _resolver;

		// Token: 0x04000B0F RID: 2831
		private Action<DbModelBuilder> _onModelCreating;
	}
}
