using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002AF RID: 687
	internal class DbConfigurationManager
	{
		// Token: 0x060021B4 RID: 8628 RVA: 0x0005E8C8 File Offset: 0x0005CAC8
		public DbConfigurationManager(DbConfigurationLoader loader, DbConfigurationFinder finder)
		{
			this._loader = loader;
			this._finder = finder;
			this._configuration = new Lazy<InternalConfiguration>(delegate
			{
				DbConfiguration dbConfiguration = this._newConfiguration ?? this._newConfigurationType.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadDbConfigurationType), null);
				dbConfiguration.InternalConfiguration.Lock();
				return dbConfiguration.InternalConfiguration;
			});
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x0005E952 File Offset: 0x0005CB52
		public static DbConfigurationManager Instance
		{
			get
			{
				return DbConfigurationManager._configManager;
			}
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0005E959 File Offset: 0x0005CB59
		public virtual void AddLoadedHandler(EventHandler<DbConfigurationLoadedEventArgs> handler)
		{
			if (this.ConfigurationSet)
			{
				throw new InvalidOperationException(Strings.AddHandlerToInUseConfiguration);
			}
			this._loadedHandler = (EventHandler<DbConfigurationLoadedEventArgs>)Delegate.Combine(this._loadedHandler, handler);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0005E985 File Offset: 0x0005CB85
		public virtual void RemoveLoadedHandler(EventHandler<DbConfigurationLoadedEventArgs> handler)
		{
			this._loadedHandler = (EventHandler<DbConfigurationLoadedEventArgs>)Delegate.Remove(this._loadedHandler, handler);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0005E9A0 File Offset: 0x0005CBA0
		public virtual void OnLoaded(InternalConfiguration configuration)
		{
			DbConfigurationLoadedEventArgs dbConfigurationLoadedEventArgs = new DbConfigurationLoadedEventArgs(configuration);
			EventHandler<DbConfigurationLoadedEventArgs> loadedHandler = this._loadedHandler;
			if (loadedHandler != null)
			{
				loadedHandler(configuration.Owner, dbConfigurationLoadedEventArgs);
			}
			configuration.DispatchLoadedInterceptors(dbConfigurationLoadedEventArgs);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0005E9D4 File Offset: 0x0005CBD4
		public virtual InternalConfiguration GetConfiguration()
		{
			if (this._configurationOverrides.IsValueCreated)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._configurationOverrides.Value.Count != 0)
					{
						return this._configurationOverrides.Value.Last<Tuple<AppConfig, InternalConfiguration>>().Item2;
					}
				}
			}
			return this._configuration.Value;
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0005EA54 File Offset: 0x0005CC54
		public virtual void SetConfigurationType(Type configurationType)
		{
			this._newConfigurationType = configurationType;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0005EA60 File Offset: 0x0005CC60
		public virtual void SetConfiguration(InternalConfiguration configuration)
		{
			Type type = this._loader.TryLoadFromConfig(AppConfig.DefaultInstance);
			if (type != null)
			{
				configuration = type.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadDbConfigurationType), null).InternalConfiguration;
			}
			this._newConfiguration = configuration.Owner;
			if (!(this._configuration.Value.Owner.GetType() != configuration.Owner.GetType()))
			{
				return;
			}
			if (this._configuration.Value.Owner.GetType() == typeof(DbConfiguration))
			{
				throw new InvalidOperationException(Strings.DefaultConfigurationUsedBeforeSet(configuration.Owner.GetType().Name));
			}
			throw new InvalidOperationException(Strings.ConfigurationSetTwice(configuration.Owner.GetType().Name, this._configuration.Value.Owner.GetType().Name));
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0005EB4C File Offset: 0x0005CD4C
		public virtual void EnsureLoadedForContext(Type contextType)
		{
			this.EnsureLoadedForAssembly(contextType.Assembly(), contextType);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0005EB5C File Offset: 0x0005CD5C
		public virtual void EnsureLoadedForAssembly(Assembly assemblyHint, Type contextTypeHint)
		{
			if (contextTypeHint == typeof(DbContext) || this._knownAssemblies.ContainsKey(assemblyHint))
			{
				return;
			}
			if (this._configurationOverrides.IsValueCreated)
			{
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._configurationOverrides.Value.Count != 0)
					{
						return;
					}
				}
			}
			if (!this.ConfigurationSet)
			{
				Type type = this._loader.TryLoadFromConfig(AppConfig.DefaultInstance) ?? this._finder.TryFindConfigurationType(assemblyHint, this._finder.TryFindContextType(assemblyHint, contextTypeHint, null), null);
				if (type != null)
				{
					this.SetConfigurationType(type);
				}
			}
			else if (!assemblyHint.IsDynamic && !this._loader.AppConfigContainsDbConfigurationType(AppConfig.DefaultInstance))
			{
				contextTypeHint = this._finder.TryFindContextType(assemblyHint, contextTypeHint, null);
				Type type2 = this._finder.TryFindConfigurationType(assemblyHint, contextTypeHint, null);
				if (type2 != null)
				{
					if (this._configuration.Value.Owner.GetType() == typeof(DbConfiguration))
					{
						throw new InvalidOperationException(Strings.ConfigurationNotDiscovered(type2.Name));
					}
					if (contextTypeHint != null && type2 != this._configuration.Value.Owner.GetType())
					{
						throw new InvalidOperationException(Strings.SetConfigurationNotDiscovered(this._configuration.Value.Owner.GetType().Name, contextTypeHint.Name));
					}
				}
			}
			this._knownAssemblies.TryAdd(assemblyHint, null);
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0005ED0C File Offset: 0x0005CF0C
		private bool ConfigurationSet
		{
			get
			{
				return this._configuration.IsValueCreated;
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0005ED1C File Offset: 0x0005CF1C
		public virtual bool PushConfiguration(AppConfig config, Type contextType)
		{
			if (config == AppConfig.DefaultInstance && (contextType == typeof(DbContext) || this._knownAssemblies.ContainsKey(contextType.Assembly())))
			{
				return false;
			}
			Type type;
			if ((type = this._loader.TryLoadFromConfig(config)) == null)
			{
				type = this._finder.TryFindConfigurationType(contextType, null) ?? typeof(DbConfiguration);
			}
			InternalConfiguration internalConfiguration = type.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadDbConfigurationType), null).InternalConfiguration;
			internalConfiguration.SwitchInRootResolver(this._configuration.Value.RootResolver);
			internalConfiguration.AddAppConfigResolver(new AppConfigDependencyResolver(config, internalConfiguration, null));
			object @lock = this._lock;
			lock (@lock)
			{
				this._configurationOverrides.Value.Add(Tuple.Create<AppConfig, InternalConfiguration>(config, internalConfiguration));
			}
			internalConfiguration.Lock();
			return true;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0005EE0C File Offset: 0x0005D00C
		public virtual void PopConfiguration(AppConfig config)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Tuple<AppConfig, InternalConfiguration> tuple = this._configurationOverrides.Value.FirstOrDefault((Tuple<AppConfig, InternalConfiguration> c) => c.Item1 == config);
				if (tuple != null)
				{
					this._configurationOverrides.Value.Remove(tuple);
				}
			}
		}

		// Token: 0x04000BBA RID: 3002
		private static readonly DbConfigurationManager _configManager = new DbConfigurationManager(new DbConfigurationLoader(), new DbConfigurationFinder());

		// Token: 0x04000BBB RID: 3003
		private EventHandler<DbConfigurationLoadedEventArgs> _loadedHandler;

		// Token: 0x04000BBC RID: 3004
		private readonly DbConfigurationLoader _loader;

		// Token: 0x04000BBD RID: 3005
		private readonly DbConfigurationFinder _finder;

		// Token: 0x04000BBE RID: 3006
		private readonly Lazy<InternalConfiguration> _configuration;

		// Token: 0x04000BBF RID: 3007
		private volatile DbConfiguration _newConfiguration;

		// Token: 0x04000BC0 RID: 3008
		private volatile Type _newConfigurationType = typeof(DbConfiguration);

		// Token: 0x04000BC1 RID: 3009
		private readonly object _lock = new object();

		// Token: 0x04000BC2 RID: 3010
		private readonly ConcurrentDictionary<Assembly, object> _knownAssemblies = new ConcurrentDictionary<Assembly, object>();

		// Token: 0x04000BC3 RID: 3011
		private readonly Lazy<List<Tuple<AppConfig, InternalConfiguration>>> _configurationOverrides = new Lazy<List<Tuple<AppConfig, InternalConfiguration>>>(() => new List<Tuple<AppConfig, InternalConfiguration>>());
	}
}
