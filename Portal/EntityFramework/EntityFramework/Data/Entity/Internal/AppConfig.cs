using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020000EC RID: 236
	internal class AppConfig
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x0002E705 File Offset: 0x0002C905
		public AppConfig(Configuration configuration)
			: this(configuration.ConnectionStrings.ConnectionStrings, configuration.AppSettings.Settings, (EntityFrameworkSection)configuration.GetSection("entityFramework"), null)
		{
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x0002E734 File Offset: 0x0002C934
		public AppConfig(ConnectionStringSettingsCollection connectionStrings)
			: this(connectionStrings, null, null, null)
		{
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x0002E740 File Offset: 0x0002C940
		private AppConfig()
			: this(ConfigurationManager.ConnectionStrings, AppConfig.Convert(ConfigurationManager.AppSettings), (EntityFrameworkSection)ConfigurationManager.GetSection("entityFramework"), null)
		{
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x0002E768 File Offset: 0x0002C968
		internal AppConfig(ConnectionStringSettingsCollection connectionStrings, KeyValueConfigurationCollection appSettings, EntityFrameworkSection entityFrameworkSettings, ProviderServicesFactory providerServicesFactory = null)
		{
			this._connectionStrings = connectionStrings;
			this._appSettings = appSettings ?? new KeyValueConfigurationCollection();
			this._entityFrameworkSettings = entityFrameworkSettings ?? new EntityFrameworkSection();
			this._providerServicesFactory = providerServicesFactory ?? new ProviderServicesFactory();
			this._providerServices = new Lazy<IList<NamedDbProviderService>>(() => (from e in this._entityFrameworkSettings.Providers.OfType<ProviderElement>()
				select new NamedDbProviderService(e.InvariantName, this._providerServicesFactory.GetInstance(e.ProviderTypeName, e.InvariantName))).ToList<NamedDbProviderService>());
			if (this._entityFrameworkSettings.DefaultConnectionFactory.ElementInformation.IsPresent)
			{
				this._defaultConnectionFactory = new Lazy<IDbConnectionFactory>(delegate
				{
					DefaultConnectionFactoryElement defaultConnectionFactory = this._entityFrameworkSettings.DefaultConnectionFactory;
					IDbConnectionFactory dbConnectionFactory;
					try
					{
						Type factoryType = defaultConnectionFactory.GetFactoryType();
						object[] typedParameterValues = defaultConnectionFactory.Parameters.GetTypedParameterValues();
						dbConnectionFactory = (IDbConnectionFactory)Activator.CreateInstance(factoryType, typedParameterValues);
					}
					catch (Exception ex)
					{
						throw new InvalidOperationException(Strings.SetConnectionFactoryFromConfigFailed(defaultConnectionFactory.FactoryTypeName), ex);
					}
					return dbConnectionFactory;
				}, true);
				return;
			}
			this._defaultConnectionFactory = this._defaultDefaultConnectionFactory;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x0002E831 File Offset: 0x0002CA31
		public virtual IDbConnectionFactory TryGetDefaultConnectionFactory()
		{
			return this._defaultConnectionFactory.Value;
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0002E83E File Offset: 0x0002CA3E
		public ConnectionStringSettings GetConnectionString(string name)
		{
			return this._connectionStrings[name];
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0002E84C File Offset: 0x0002CA4C
		public static AppConfig DefaultInstance
		{
			get
			{
				return AppConfig._defaultInstance;
			}
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x0002E854 File Offset: 0x0002CA54
		private static KeyValueConfigurationCollection Convert(NameValueCollection collection)
		{
			KeyValueConfigurationCollection keyValueConfigurationCollection = new KeyValueConfigurationCollection();
			foreach (string text in collection.AllKeys)
			{
				keyValueConfigurationCollection.Add(text, ConfigurationManager.AppSettings[text]);
			}
			return keyValueConfigurationCollection;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0002E893 File Offset: 0x0002CA93
		public virtual ContextConfig ContextConfigs
		{
			get
			{
				return new ContextConfig(this._entityFrameworkSettings);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0002E8A0 File Offset: 0x0002CAA0
		public virtual InitializerConfig Initializers
		{
			get
			{
				return new InitializerConfig(this._entityFrameworkSettings, this._appSettings);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0002E8B3 File Offset: 0x0002CAB3
		public virtual string ConfigurationTypeName
		{
			get
			{
				return this._entityFrameworkSettings.ConfigurationTypeName;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		public virtual IList<NamedDbProviderService> DbProviderServices
		{
			get
			{
				return this._providerServices.Value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0002E8CD File Offset: 0x0002CACD
		public virtual IEnumerable<IDbInterceptor> Interceptors
		{
			get
			{
				return this._entityFrameworkSettings.Interceptors.Interceptors;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0002E8DF File Offset: 0x0002CADF
		public virtual QueryCacheConfig QueryCache
		{
			get
			{
				return new QueryCacheConfig(this._entityFrameworkSettings);
			}
		}

		// Token: 0x040008EE RID: 2286
		public const string EFSectionName = "entityFramework";

		// Token: 0x040008EF RID: 2287
		private static readonly AppConfig _defaultInstance = new AppConfig();

		// Token: 0x040008F0 RID: 2288
		private readonly KeyValueConfigurationCollection _appSettings;

		// Token: 0x040008F1 RID: 2289
		private readonly ConnectionStringSettingsCollection _connectionStrings;

		// Token: 0x040008F2 RID: 2290
		private readonly EntityFrameworkSection _entityFrameworkSettings;

		// Token: 0x040008F3 RID: 2291
		private readonly Lazy<IDbConnectionFactory> _defaultConnectionFactory;

		// Token: 0x040008F4 RID: 2292
		private readonly Lazy<IDbConnectionFactory> _defaultDefaultConnectionFactory = new Lazy<IDbConnectionFactory>(() => null, true);

		// Token: 0x040008F5 RID: 2293
		private readonly ProviderServicesFactory _providerServicesFactory;

		// Token: 0x040008F6 RID: 2294
		private readonly Lazy<IList<NamedDbProviderService>> _providerServices;
	}
}
