using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using Microsoft.AnalysisServices.Azure.Gateway;
using Microsoft.Cloud.ModelCommon.Model;
using Microsoft.Cloud.Platform.Azure.WindowsFabric.Routers;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.Cloud.Platform.CommunicationFramework.Routers;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Modularization;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.CloudBI.DataServiceClusterIsolation;
using Microsoft.CloudBI.FeatureSwitches;
using Microsoft.CloudBI.Routers;

namespace Microsoft.AnalysisServices.Azure.Common.ExploreToDataRouter
{
	// Token: 0x0200014D RID: 333
	[BlockServiceProvider(typeof(IExploreToDataRouterFactory))]
	public class ExploreToDataRouterFactoryBlock : AnalyticsBlockBase, IExploreToDataRouterFactory
	{
		// Token: 0x06001190 RID: 4496 RVA: 0x00047D0A File Offset: 0x00045F0A
		public ExploreToDataRouterFactoryBlock()
			: base(typeof(ExploreToDataRouterFactoryBlock).Name)
		{
			this.cfgLock = new object();
			this.svcTypeToRouterBuilderMap = new Dictionary<BIAzure.ServiceType, IRouterBuilder>();
			this.serviceTypeToAffinitizedRouterBuilderMap = new ConcurrentDictionary<BIAzure.ServiceType, IRouterBuilder>();
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00047D44 File Offset: 0x00045F44
		protected override BlockInitializationStatus OnInitialize()
		{
			BlockInitializationStatus blockInitializationStatus = base.OnInitialize();
			if (blockInitializationStatus != BlockInitializationStatus.Done)
			{
				return blockInitializationStatus;
			}
			object obj = this.cfgLock;
			lock (obj)
			{
				if (this.configurationManager == null)
				{
					this.configurationManager = base.ConfigurationManagerFactory.GetConfigurationManager();
				}
			}
			obj = this.cfgLock;
			lock (obj)
			{
				if (!this.onConfigurationChangeSubscriptionCompleted)
				{
					this.configurationManager.Subscribe(new List<Type> { typeof(ExploreToDataServiceRouterConfiguration) }, new CcsEventHandler(this.OnConfigChange));
					this.onConfigurationChangeSubscriptionCompleted = true;
				}
			}
			obj = this.cfgLock;
			lock (obj)
			{
				if (this.exploreToDataServiceRouterCfg == null)
				{
					return BlockInitializationStatus.PartiallyDone;
				}
			}
			return BlockInitializationStatus.Done;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00047E40 File Offset: 0x00046040
		protected override void OnStop()
		{
			base.OnStop();
			IDictionary<BIAzure.ServiceType, IRouterBuilder> dictionary = this.svcTypeToRouterBuilderMap;
			lock (dictionary)
			{
				this.svcTypeToRouterBuilderMap.Clear();
			}
			this.configurationManager.Unsubscribe(new CcsEventHandler(this.OnConfigChange));
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00047EA4 File Offset: 0x000460A4
		private void OnConfigChange(IConfigurationContainer configurationContainer)
		{
			ExploreToDataServiceRouterConfiguration configuration = configurationContainer.GetConfiguration<ExploreToDataServiceRouterConfiguration>();
			this.ValidateExploreToDataServiceRouterConfiguration(configuration);
			object obj = this.cfgLock;
			lock (obj)
			{
				this.exploreToDataServiceRouterCfg = configuration;
			}
			if (this.IsDataServiceClusterIsolationEnabled())
			{
				string text = null;
				obj = this.cfgLock;
				lock (obj)
				{
					text = this.exploreToDataServiceRouterCfg.DataServiceManagementEndpoint;
				}
				this.clusterManagementService = base.CommunicationServices.GetService<IClusterManagementService>(new ServiceIdentification((-53).ToString()), new SingleUriRouter(new Uri(text, UriKind.Absolute), new Type[]
				{
					typeof(EndpointNotFoundException),
					typeof(CommunicationException)
				}), null);
			}
			else
			{
				this.clusterManagementService = null;
			}
			this.SetRouterConfiguration();
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00047F98 File Offset: 0x00046198
		private void SetRouterConfiguration()
		{
			bool flag = this.IsDataServiceClusterIsolationEnabled();
			TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation("[{0}] SetRouterCfg: dataServiceClusterIsolationEnabled={1}", new object[]
			{
				base.GetType().Name,
				flag
			});
			IDictionary<BIAzure.ServiceType, IRouterBuilder> dictionary = this.svcTypeToRouterBuilderMap;
			lock (dictionary)
			{
				this.svcTypeToRouterBuilderMap.Clear();
				if (flag)
				{
					BIAzure.ServiceType[] array = new BIAzure.ServiceType[4];
					RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.65D8F0C6661536EDC95FEDC6FAA79AD288CB1C45).FieldHandle);
					foreach (BIAzure.ServiceType serviceType in array)
					{
						this.AddExploreClusterRouterBuilder(serviceType);
					}
				}
				else
				{
					this.AddLocalClusterRouterBuilder<SingletonPrimaryRouter>(21, false);
					this.AddLocalClusterRouterBuilder<RoundRobinRandomNodeRouter>(22, false);
					this.AddLocalClusterRouterBuilder<PreferOneDynamicNodeRouter>(22, true);
					this.AddLocalClusterRouterBuilder<SingletonPrimaryRouter>(24, false);
					this.AddLocalClusterRouterBuilder<SingletonPrimaryRouter>(32, false);
				}
			}
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00048074 File Offset: 0x00046274
		private void AddExploreClusterRouterBuilder(BIAzure.ServiceType svcType)
		{
			IRouterBuilder routerBuilder = new ExploreToDataExternalRouterBuilder(base.EventsKitFactory, this.clusterManagementService, svcType);
			this.svcTypeToRouterBuilderMap.Add(svcType, routerBuilder);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000480A4 File Offset: 0x000462A4
		private void AddLocalClusterRouterBuilder<T>(BIAzure.ServiceType svcType, bool useAffinitizedRouter = false) where T : WindowsFabricServiceRouter
		{
			IRouterBuilder routerBuilder = new LocalClusterRouterBuilder<T>(base.WindowsFabricRouterDependencies, base.ServiceModel, svcType);
			if (useAffinitizedRouter)
			{
				this.serviceTypeToAffinitizedRouterBuilderMap.TryAdd(svcType, routerBuilder);
				return;
			}
			this.svcTypeToRouterBuilderMap.Add(svcType, routerBuilder);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000480E4 File Offset: 0x000462E4
		private void ValidateExploreToDataServiceRouterConfiguration(ExploreToDataServiceRouterConfiguration cfg)
		{
			if (!this.IsDataServiceClusterIsolationEnabled())
			{
				return;
			}
			if (cfg == null || string.IsNullOrWhiteSpace(cfg.DataServiceManagementEndpoint))
			{
				throw new ConfigurationException("FeatureSwitch 'DataServiceClusterIsolationEnabled' is set and ExploreToDataServiceRouterConfiguration is null or member DataServiceManagementEndpoint is null or empty String! This is not allowed! Please set correct URI to Data Service Gateway Management endpoint!");
			}
			Uri uri;
			if (!Uri.TryCreate(cfg.DataServiceManagementEndpoint, UriKind.Absolute, out uri))
			{
				throw new ConfigurationException("Can't create Absolute URI from ExploreToDataServiceRouterConfiguration.DataServiceManagementEndpoint value: {0}! Please set correct URI value!".FormatWithInvariantCulture(new object[] { cfg.DataServiceManagementEndpoint }));
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00048144 File Offset: 0x00046344
		public Router GetRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType endpointType)
		{
			return this.GetRouter(serviceType, endpointType, endpointType);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004814F File Offset: 0x0004634F
		public Router GetAffinitizedRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType endpointType)
		{
			return this.GetAffinitizedRouter(serviceType, endpointType, new PreferNodeByRouterKeysHashNodeSelector(), new DeterministicNodeSorter());
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00048164 File Offset: 0x00046364
		public Router GetAffinitizedRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType endpointType, IPreferredNodeSelector preferredNodeSelector, INodeSorter nodeSorter)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IPreferredNodeSelector>(preferredNodeSelector, "preferredNodeSelector");
			TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation(string.Format("[{0}] GetAffinitizedRouter: BlockState={1}; serviceType={2}; ", base.GetType().Name, base.State, serviceType) + string.Format("endpointType={0}, preferredNodeSelector={1}, nodeSorter={2}", endpointType, preferredNodeSelector.GetType().Name, nodeSorter.GetType().Name));
			IRouterBuilder routerBuilder;
			if (!this.serviceTypeToAffinitizedRouterBuilderMap.TryGetValue(serviceType, out routerBuilder))
			{
				throw new ServiceTypeNotInConfigurationExploreToDataRouterException(serviceType.ToString(), string.Join<BIAzure.ServiceType>(", ", this.serviceTypeToAffinitizedRouterBuilderMap.Keys), "serviceTypeToAffinitizedRouterBuilderMap");
			}
			return routerBuilder.BuildAffinitizedRouter(endpointType, endpointType, preferredNodeSelector, nodeSorter);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00048220 File Offset: 0x00046420
		public Router GetRouter(BIAzure.ServiceType serviceType, BIAzure.EndpointType resolvedEndoint, BIAzure.EndpointType targetEndpoint)
		{
			TraceSourceBase<ANCommonTrace>.Tracer.TraceInformation(string.Format("[{0}] GetRouter: BlockState={1}; serviceType={2};", base.GetType().Name, base.State, serviceType) + string.Format(" resolvedEndoint={0}, targetEndpoint={1}", resolvedEndoint, targetEndpoint));
			IDictionary<BIAzure.ServiceType, IRouterBuilder> dictionary = this.svcTypeToRouterBuilderMap;
			IRouterBuilder routerBuilder;
			lock (dictionary)
			{
				if (!this.svcTypeToRouterBuilderMap.TryGetValue(serviceType, out routerBuilder))
				{
					throw new ServiceTypeNotInConfigurationExploreToDataRouterException(serviceType.ToString(), string.Join<BIAzure.ServiceType>(", ", this.svcTypeToRouterBuilderMap.Keys));
				}
			}
			return routerBuilder.Build(resolvedEndoint, targetEndpoint);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000482E8 File Offset: 0x000464E8
		public bool IsDataServiceClusterIsolationEnabled()
		{
			return this.featureSwitchesProvider.FeatureSwitches.DataServiceClusterIsolationEnabled;
		}

		// Token: 0x04000413 RID: 1043
		public const string DATASERVICECLUSTERISOLATION_FEATURESWITCH_NAME = "DataServiceClusterIsolation";

		// Token: 0x04000414 RID: 1044
		[BlockServiceDependency]
		private readonly IFeatureSwitchesProvider featureSwitchesProvider;

		// Token: 0x04000415 RID: 1045
		private IConfigurationManager configurationManager;

		// Token: 0x04000416 RID: 1046
		private readonly object cfgLock;

		// Token: 0x04000417 RID: 1047
		private bool onConfigurationChangeSubscriptionCompleted;

		// Token: 0x04000418 RID: 1048
		private ExploreToDataServiceRouterConfiguration exploreToDataServiceRouterCfg;

		// Token: 0x04000419 RID: 1049
		private IClusterManagementService clusterManagementService;

		// Token: 0x0400041A RID: 1050
		private readonly IDictionary<BIAzure.ServiceType, IRouterBuilder> svcTypeToRouterBuilderMap;

		// Token: 0x0400041B RID: 1051
		private readonly ConcurrentDictionary<BIAzure.ServiceType, IRouterBuilder> serviceTypeToAffinitizedRouterBuilderMap;
	}
}
