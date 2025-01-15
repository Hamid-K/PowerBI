using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.ConfigurationClasses.Communication;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E2 RID: 1250
	[BlockServiceProvider(typeof(IRouterFactory))]
	public class RouterFactory : Block, IRouterFactory
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00086970 File Offset: 0x00084B70
		protected IEventsKitFactory EventsKitFactory
		{
			get
			{
				return this.m_eventsKitFactory;
			}
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00086978 File Offset: 0x00084B78
		public RouterFactory(string name)
			: base(name)
		{
			this.m_routersConfigurations = new Dictionary<Type, RouterFactory.RouterDetails>();
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008698C File Offset: 0x00084B8C
		public RouterFactory()
			: this(typeof(RouterFactory).Name)
		{
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000869A4 File Offset: 0x00084BA4
		protected override BlockInitializationStatus OnInitialize()
		{
			if (base.OnInitialize() == BlockInitializationStatus.PartiallyDone)
			{
				return BlockInitializationStatus.PartiallyDone;
			}
			this.m_configurationManager = this.m_configurationManagerFactory.GetConfigurationManager();
			this.RegisterRouter(typeof(UrisRoundRobinRouter), new Type[] { typeof(UrisRouterConfiguration) }, new Func<Type, IConfigurationContainer, string, IRouter>(this.CreateRoundRobinRouter));
			this.RegisterRouter(typeof(BroadcastToPredefinedUrisRouter), new Type[] { typeof(UrisRouterConfiguration) }, new Func<Type, IConfigurationContainer, string, IRouter>(this.CreateRoundRobinRouter));
			this.RegisterRouters();
			this.m_configurationManager.Subscribe(new Collection<Type>(this.m_routersConfigurations.Values.SelectMany((RouterFactory.RouterDetails types) => types.Configurations).Distinct<Type>().ToArray<Type>()), new CcsEventHandler(this.ConfigurationChangeHandler));
			return BlockInitializationStatus.Done;
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x00086A88 File Offset: 0x00084C88
		private IRouter CreateRoundRobinRouter(Type router, IConfigurationContainer configs, string id)
		{
			UrisRouterSectionConfiguration urisRouterSectionConfiguration = configs.GetConfiguration<UrisRouterConfiguration>().Routers.Where((UrisRouterSectionConfiguration s) => object.Equals(s.Id, id)).FirstOrDefault<UrisRouterSectionConfiguration>();
			IEnumerable<Uri> enumerable = urisRouterSectionConfiguration.Uris.Select((string uri) => new Uri(uri));
			if (router == typeof(UrisRoundRobinRouter))
			{
				IEnumerable<Type> enumerable2 = urisRouterSectionConfiguration.RetryToDifferentEndPointExceptions.Select((TypeIdentifier typeIdentifier) => CommunicationUtilities.GetKnownType(typeIdentifier));
				IEnumerable<Type> enumerable3 = urisRouterSectionConfiguration.RetryToSameEndPointExceptions.Select((TypeIdentifier typeIdentifier) => CommunicationUtilities.GetKnownType(typeIdentifier));
				return new UrisRoundRobinRouter(enumerable, enumerable2, enumerable3);
			}
			if (router == typeof(BroadcastToPredefinedUrisRouter))
			{
				return new BroadcastToPredefinedUrisRouter(enumerable);
			}
			throw new CommunicationFrameworkRouterException("Unknown router type '{0}'.".FormatWithInvariantCulture(new object[] { router }));
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x00086B94 File Offset: 0x00084D94
		protected override void OnStop()
		{
			this.m_configurationManager.Unsubscribe(new CcsEventHandler(this.ConfigurationChangeHandler));
			base.OnShutdown();
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x00086BB4 File Offset: 0x00084DB4
		public Router Create(RouterIdentifier routerIdentifier)
		{
			RouterFactory.RouterDetails routerDetails = null;
			if (!this.m_routersConfigurations.TryGetValue(routerIdentifier.RouterType, out routerDetails))
			{
				throw new CommunicationFrameworkRouterException("Requested router '{0}' of type '{1}' was not registered.".FormatWithInvariantCulture(new object[] { routerIdentifier.Id, routerIdentifier.RouterType }));
			}
			IRouter router = routerDetails.Handler(routerDetails.Type, this.m_configurationContainer, routerIdentifier.Id);
			ExtendedDiagnostics.EnsureNotNull<IRouter>(router, "router");
			return router as Router;
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x00086C30 File Offset: 0x00084E30
		protected void RegisterRouter([NotNull] Type router, [NotNull] IEnumerable<Type> configurations, [NotNull] Func<Type, IConfigurationContainer, string, IRouter> creator)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(router, "router");
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(configurations, "configs");
			ExtendedDiagnostics.EnsureArgumentNotNull<Func<Type, IConfigurationContainer, string, IRouter>>(creator, "creator");
			IEnumerable<Type> enumerable = configurations.Distinct<Type>();
			RouterFactory.RouterDetails routerDetails = null;
			if (!this.m_routersConfigurations.TryGetValue(router, out routerDetails))
			{
				this.m_routersConfigurations.Add(router, new RouterFactory.RouterDetails(router, enumerable, creator));
				return;
			}
			if (!routerDetails.Configurations.Equivalent(enumerable))
			{
				throw new CommunicationFrameworkRouterException("Re-registering router ({0}) with different configuration types is not supported!".FormatWithInvariantCulture(new object[] { routerDetails.Type }));
			}
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void RegisterRouters()
		{
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x00086CB9 File Offset: 0x00084EB9
		private void ConfigurationChangeHandler(IConfigurationContainer configGroup)
		{
			this.m_configurationContainer = configGroup;
		}

		// Token: 0x04000D70 RID: 3440
		[BlockServiceDependency]
		private IConfigurationManagerFactory m_configurationManagerFactory;

		// Token: 0x04000D71 RID: 3441
		[BlockServiceDependency]
		private readonly IEventsKitFactory m_eventsKitFactory;

		// Token: 0x04000D72 RID: 3442
		private IConfigurationManager m_configurationManager;

		// Token: 0x04000D73 RID: 3443
		private Dictionary<Type, RouterFactory.RouterDetails> m_routersConfigurations;

		// Token: 0x04000D74 RID: 3444
		private IConfigurationContainer m_configurationContainer;

		// Token: 0x02000843 RID: 2115
		private class RouterDetails
		{
			// Token: 0x17000783 RID: 1923
			// (get) Token: 0x06003317 RID: 13079 RVA: 0x000AAE65 File Offset: 0x000A9065
			// (set) Token: 0x06003318 RID: 13080 RVA: 0x000AAE6D File Offset: 0x000A906D
			public Type Type { get; private set; }

			// Token: 0x17000784 RID: 1924
			// (get) Token: 0x06003319 RID: 13081 RVA: 0x000AAE76 File Offset: 0x000A9076
			// (set) Token: 0x0600331A RID: 13082 RVA: 0x000AAE7E File Offset: 0x000A907E
			public IEnumerable<Type> Configurations { get; private set; }

			// Token: 0x17000785 RID: 1925
			// (get) Token: 0x0600331B RID: 13083 RVA: 0x000AAE87 File Offset: 0x000A9087
			// (set) Token: 0x0600331C RID: 13084 RVA: 0x000AAE8F File Offset: 0x000A908F
			public Func<Type, IConfigurationContainer, string, IRouter> Handler { get; private set; }

			// Token: 0x0600331D RID: 13085 RVA: 0x000AAE98 File Offset: 0x000A9098
			public RouterDetails([NotNull] Type type, [NotNull] IEnumerable<Type> configs, [NotNull] Func<Type, IConfigurationContainer, string, IRouter> handler)
			{
				ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
				ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<Type>>(configs, "configs");
				ExtendedDiagnostics.EnsureArgumentNotNull<Func<Type, IConfigurationContainer, string, IRouter>>(handler, "handler");
				this.Type = type;
				this.Configurations = configs;
				this.Handler = handler;
			}
		}
	}
}
