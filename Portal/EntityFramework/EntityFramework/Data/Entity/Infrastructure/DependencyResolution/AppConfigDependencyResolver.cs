using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x020002A7 RID: 679
	internal class AppConfigDependencyResolver : IDbDependencyResolver
	{
		// Token: 0x0600218C RID: 8588 RVA: 0x0005DFC8 File Offset: 0x0005C1C8
		public AppConfigDependencyResolver()
		{
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0005DFF4 File Offset: 0x0005C1F4
		public AppConfigDependencyResolver(AppConfig appConfig, InternalConfiguration internalConfiguration, ProviderServicesFactory providerServicesFactory = null)
		{
			this._appConfig = appConfig;
			this._internalConfiguration = internalConfiguration;
			this._providerServicesFactory = providerServicesFactory ?? new ProviderServicesFactory();
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0005E048 File Offset: 0x0005C248
		public virtual object GetService(Type type, object key)
		{
			return this._serviceFactories.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> t) => this.GetServiceFactory(type, key as string))();
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0005E0A0 File Offset: 0x0005C2A0
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return (from f in this._servicesFactories.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> t) => this.GetServicesFactory(type, key))
				select f() into s
				where s != null
				select s).ToList<object>();
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0005E140 File Offset: 0x0005C340
		public virtual IEnumerable<Func<object>> GetServicesFactory(Type type, object key)
		{
			if (type == typeof(IDbInterceptor))
			{
				return this._appConfig.Interceptors.Select((IDbInterceptor i) => () => i).ToList<Func<object>>();
			}
			return new List<Func<object>> { this.GetServiceFactory(type, key as string) };
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0005E1AC File Offset: 0x0005C3AC
		public virtual Func<object> GetServiceFactory(Type type, string name)
		{
			if (!this._providersRegistered)
			{
				Dictionary<string, DbProviderServices> providerFactories = this._providerFactories;
				lock (providerFactories)
				{
					if (!this._providersRegistered)
					{
						this.RegisterDbProviderServices();
						this._providersRegistered = true;
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(name) && type == typeof(DbProviderServices))
			{
				DbProviderServices providerFactory;
				this._providerFactories.TryGetValue(name, out providerFactory);
				return () => providerFactory;
			}
			if (type == typeof(IDbConnectionFactory))
			{
				if (!Database.DefaultConnectionFactoryChanged)
				{
					IDbConnectionFactory dbConnectionFactory = this._appConfig.TryGetDefaultConnectionFactory();
					if (dbConnectionFactory != null)
					{
						Database.DefaultConnectionFactory = dbConnectionFactory;
					}
				}
				return delegate
				{
					if (!Database.DefaultConnectionFactoryChanged)
					{
						return null;
					}
					return Database.SetDefaultConnectionFactory;
				};
			}
			Type type2 = type.TryGetElementType(typeof(IDatabaseInitializer<>));
			if (type2 != null)
			{
				object initializer = this._appConfig.Initializers.TryGetInitializer(type2);
				return () => initializer;
			}
			return () => null;
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0005E2F8 File Offset: 0x0005C4F8
		private void RegisterDbProviderServices()
		{
			IList<NamedDbProviderService> dbProviderServices = this._appConfig.DbProviderServices;
			if (dbProviderServices.All((NamedDbProviderService p) => p.InvariantName != "System.Data.SqlClient"))
			{
				this.RegisterSqlServerProvider();
			}
			dbProviderServices.Each(delegate(NamedDbProviderService p)
			{
				this._providerFactories[p.InvariantName] = p.ProviderServices;
				this._internalConfiguration.AddDefaultResolver(p.ProviderServices);
			});
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0005E350 File Offset: 0x0005C550
		private void RegisterSqlServerProvider()
		{
			string text = string.Format(CultureInfo.InvariantCulture, "System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer, Version={0}, Culture=neutral, PublicKeyToken=b77a5c561934e089", new object[] { new AssemblyName(typeof(DbContext).Assembly().FullName).Version });
			DbProviderServices dbProviderServices = this._providerServicesFactory.TryGetInstance(text);
			if (dbProviderServices != null)
			{
				this._internalConfiguration.SetDefaultProviderServices(dbProviderServices, "System.Data.SqlClient");
			}
		}

		// Token: 0x04000BAC RID: 2988
		private readonly AppConfig _appConfig;

		// Token: 0x04000BAD RID: 2989
		private readonly InternalConfiguration _internalConfiguration;

		// Token: 0x04000BAE RID: 2990
		private readonly ConcurrentDictionary<Tuple<Type, object>, Func<object>> _serviceFactories = new ConcurrentDictionary<Tuple<Type, object>, Func<object>>();

		// Token: 0x04000BAF RID: 2991
		private readonly ConcurrentDictionary<Tuple<Type, object>, IEnumerable<Func<object>>> _servicesFactories = new ConcurrentDictionary<Tuple<Type, object>, IEnumerable<Func<object>>>();

		// Token: 0x04000BB0 RID: 2992
		private readonly Dictionary<string, DbProviderServices> _providerFactories = new Dictionary<string, DbProviderServices>();

		// Token: 0x04000BB1 RID: 2993
		private bool _providersRegistered;

		// Token: 0x04000BB2 RID: 2994
		private readonly ProviderServicesFactory _providerServicesFactory;
	}
}
