using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000146 RID: 326
	internal sealed class ServiceConfigurationManager : ConfigManager
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x00020C28 File Offset: 0x0001EE28
		static ServiceConfigurationManager()
		{
			DataCacheServerLogManager.ChangeLogLevel(TraceLevel.Verbose);
			VersioningUtility.LoadSupportedClientVersions();
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00020C3C File Offset: 0x0001EE3C
		internal ServiceConfigurationManager(string hostName, string serviceName, string path)
		{
			LocalConfigReader localConfigReader = this.InitializeEssentialParameters(hostName, serviceName, path);
			this.PrepareLocalConfigToGlobalConfigBridge(localConfigReader);
			this.InitializeDataFromGlobalConfig();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00020C9C File Offset: 0x0001EE9C
		internal ServiceConfigurationManager(ClusterConfigElement connectionSettings)
		{
			if (!string.IsNullOrEmpty(connectionSettings.CloudProvider))
			{
				CloudUtility.ProviderType = connectionSettings.CloudProvider;
			}
			else
			{
				CloudUtility.ProviderType = "Microsoft.ApplicationServer.Caching.AzureProvider, Microsoft.ApplicationServer.Caching.CacheAzureProvider, Culture=neutral";
			}
			this._globalConfigConnSettings = connectionSettings;
			if (CloudUtility.IsVASDeployment)
			{
				DataCacheServerLogManager.ChangeLogLevel(TraceLevel.Off);
				if (ServiceConfigurationManager._initialLogLevel != TraceLevel.Off)
				{
					DataCacheServerLogManager.ChangeLogLevel(ServiceConfigurationManager._initialLogLevel);
				}
				this.InitializeEssentialParameters(CloudUtility.GetCurrentEndpointAddress(), "AppFabricCachingService", connectionSettings, CloudUtility.GetLogLocationForCurrentInstance());
			}
			else
			{
				LocalConfigReader localConfigReader = this.InitializeEssentialParameters(null);
				this.PrepareLocalConfigToGlobalConfigBridge(localConfigReader);
			}
			this._globalConfig = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			this.InitializeDataFromGlobalConfig();
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00020D6C File Offset: 0x0001EF6C
		internal ServiceConfigurationManager(string hostName, string serviceName, ClusterConfigElement connectionString, string logLocation)
			: this(hostName, serviceName, connectionString, logLocation, false)
		{
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00020D7C File Offset: 0x0001EF7C
		internal ServiceConfigurationManager(string hostName, string serviceName, ClusterConfigElement connectionString, string logLocation, bool isEmulated)
		{
			this.IsEmulatedEnvironment = isEmulated;
			this.InitializeEssentialParameters(hostName, serviceName, connectionString, logLocation);
			this._globalConfig = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			this.InitializeDataFromGlobalConfig();
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x00020DF0 File Offset: 0x0001EFF0
		internal ServiceConfigurationManager(string path)
		{
			LocalConfigReader localConfigReader = this.InitializeEssentialParameters(path);
			this.PrepareLocalConfigToGlobalConfigBridge(localConfigReader);
			this.InitializeDataFromGlobalConfig();
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00020E4C File Offset: 0x0001F04C
		internal ServiceConfigurationManager()
		{
			LocalConfigReader localConfigReader = this.InitializeEssentialParameters(null);
			this.PrepareLocalConfigToGlobalConfigBridge(localConfigReader);
			this.InitializeDataFromGlobalConfig();
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x00020EA8 File Offset: 0x0001F0A8
		private LocalConfigReader InitializeEssentialParameters(string path)
		{
			path = ConfigManager.ValidatePath(path);
			LocalConfigReader localConfigReader = new LocalConfigReader(path);
			this._sampleInterval = localConfigReader.GetPerfElement().SampleInterval;
			this._perfCounterEnable = localConfigReader.GetPerfElement().IsEnable;
			this._fabricConfigPath = path ?? this.GetFabricConfigurationPath();
			localConfigReader.GetFirstCacheConfig(out this._host, out this._service, out this._timeout);
			this.ExtractOtherLocalConfigParams(localConfigReader);
			return localConfigReader;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00020F18 File Offset: 0x0001F118
		private LocalConfigReader InitializeEssentialParameters(string hostName, string serviceName, string path)
		{
			path = ConfigManager.ValidatePath(path);
			LocalConfigReader localConfigReader = new LocalConfigReader(path);
			this._sampleInterval = localConfigReader.GetPerfElement().SampleInterval;
			this._perfCounterEnable = localConfigReader.GetPerfElement().IsEnable;
			this._fabricConfigPath = path ?? this.GetFabricConfigurationPath();
			this._host = hostName;
			this._service = serviceName;
			this.ExtractOtherLocalConfigParams(localConfigReader);
			return localConfigReader;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x00020F7D File Offset: 0x0001F17D
		private void InitializeEssentialParameters(string hostName, string serviceName, ClusterConfigElement connectionSettings, string logLocation)
		{
			this._host = hostName;
			this._service = serviceName;
			this._globalConfigConnSettings = connectionSettings;
			this._logLocation = ServiceConfigurationManager.RefineLogLocation(logLocation);
			this._logLevel = -1;
			this._fabricConfigPath = this.GetFabricConfigurationPath();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00020FB4 File Offset: 0x0001F1B4
		private string GetFabricConfigurationPath()
		{
			string text = null;
			if (Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this.logSource, "Getting fabric configuration path", new object[0]);
			}
			if (CloudUtility.IsVASDeployment)
			{
				string environmentVariable = Environment.GetEnvironmentVariable("RoleRoot");
				text = Path.Combine(Path.Combine(environmentVariable, "approot"), "Microsoft.Cloud.AzureRoleHost.dll.config");
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(this.logSource, "roleroot={0} fabricConfigPath={1}", new object[] { environmentVariable, text });
				}
			}
			return text;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00021034 File Offset: 0x0001F234
		private void InitMemoryManager()
		{
			this.IsMemoryManagerEnabled = this._advancedProperties.MemoryPressureMonitorProperties.IsMemoryManagerEnabled;
			this.IsSystemMemoryCheckEnabled = this._advancedProperties.MemoryPressureMonitorProperties.IsEnabled;
			this.BufferSize = this._advancedProperties.MemoryPressureMonitorProperties.BufferSize;
			this.MinObjectPoolSize = this._advancedProperties.MemoryPressureMonitorProperties.MinObjectPoolSize;
			this.MaxObjectPoolSize = this._advancedProperties.MemoryPressureMonitorProperties.MaxObjectPoolSize;
			this.AverageCacheItemSizeInBytes = this._advancedProperties.MemoryPressureMonitorProperties.AverageCacheItemSizeInBytes;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000210C5 File Offset: 0x0001F2C5
		private static string RefineLogLocation(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				path = Path.GetTempPath();
			}
			return path;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x000210D7 File Offset: 0x0001F2D7
		private void ExtractOtherLocalConfigParams(LocalConfigReader localConfig)
		{
			this._logLocation = ServiceConfigurationManager.RefineLogLocation(localConfig.GetLogLocation());
			this._logLevel = localConfig.LogLevel;
			this._etwMonitorInterval = localConfig.ETWMonitorInterval;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00021102 File Offset: 0x0001F302
		private void InitializeDataFromGlobalConfig()
		{
			this.InitializeHostsRelatedData();
			this.InitializeOtherGlobalConfigValues();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00021110 File Offset: 0x0001F310
		private void PrepareLocalConfigToGlobalConfigBridge(LocalConfigReader localConfig)
		{
			this._globalConfigConnSettings = localConfig.GetClusterConfigConnectionSettings();
			this._globalConfig = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00021130 File Offset: 0x0001F330
		private void InitializeOtherGlobalConfigValues()
		{
			this._maxRetries = this._globalConfig.MaxRoutingLookupRetries();
			this._configStoreConnectionSettings = this._globalConfig.GetCASConfigStoreConnectionSettings();
			this.InitializeAdvancedProperties(this._globalConfig);
			this.InitializeGlobalConfigVersions(this._globalConfig);
			this.InitializeDeploymentSettings();
			this._tktDir = this._logLocation;
			this._basePartitionCount = this._globalConfig.BasePartitionCount;
			this._maxNamedCacheCount = this._globalConfig.MaxNamedCacheCount;
			this.InitMemoryManager();
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000211B4 File Offset: 0x0001F3B4
		private void InitializeAdvancedProperties(IClusterConfigurationReader globalConfig)
		{
			this._advancedProperties = globalConfig.AdvancedProperties;
			int num = globalConfig.AdvancedProperties.TransportProperties.MaxBufferSize;
			if (num == -1)
			{
				num = 8388608;
			}
			this.BatchSize = Math.Max(1, num - 2048);
			if (CloudUtility.IsVASDeployment)
			{
				VersionProperties versionProperties = this._advancedProperties.VersionProperties;
				ClientVersionInfo.Singleton.EditAllowedVersions(versionProperties.BeginClientVersion, versionProperties.EndClientVersion, VersioningUtility.GetOtherSupportedClientVersions(versionProperties.BeginServerVersion, versionProperties.EndServerVersion));
				this._advancedProperties.DnsDomain = CloudUtility.CloudProvider.GetConfigurationValue("dnsDomain");
				MemoryPressureMonitorProperties memoryPressureMonitorProperties = this._advancedProperties.MemoryPressureMonitorProperties;
				CacheHostConfiguration host = CloudUtility.CloudProvider.GetHost(this._host, this._service);
				if (host.NodeThrottlingInternalHighThreshold != null)
				{
					memoryPressureMonitorProperties.InternalThrottleHighPercent = host.NodeThrottlingInternalHighThreshold.Value;
				}
				if (host.NodeThrottlingInternalLowThreshold != null)
				{
					memoryPressureMonitorProperties.InternalThrottleLowPercent = host.NodeThrottlingInternalLowThreshold.Value;
				}
				if (host.NodeThrottlingExternalHighThreshold != null)
				{
					memoryPressureMonitorProperties.ThrottleHighPercent = host.NodeThrottlingExternalHighThreshold.Value;
				}
				if (host.NodeThrottlingExternalLowThreshold != null)
				{
					memoryPressureMonitorProperties.ThrottleLowPercent = host.NodeThrottlingExternalLowThreshold.Value;
				}
				if (host.CacheDataSizeInPercent != null)
				{
					memoryPressureMonitorProperties.PercentItemInFinalizerQueue = 250 / host.CacheDataSizeInPercent.Value;
				}
				if (host.AverageCacheItemSizeInBytes != null)
				{
					memoryPressureMonitorProperties.AverageCacheItemSizeInBytes = (long)host.AverageCacheItemSizeInBytes.Value;
				}
				if (host.CacheEvictionHWM != null)
				{
					this._advancedProperties.QuotaProperties.CacheEvictionHighWatermarkPercent = host.CacheEvictionHWM.Value;
				}
			}
			if (this._advancedProperties.SecurityProperties.UseAcsForClient && CloudUtility.IsVASDeployment)
			{
				ServerAcsSecurityElement serverAcsSecurityElement = new ServerAcsSecurityElement();
				serverAcsSecurityElement.AcsHostName = CloudUtility.CloudProvider.GetConfigurationValue("acshostname");
				serverAcsSecurityElement.SigningKey = CloudUtility.CloudProvider.GetConfigurationValue("issuerKey");
				this._advancedProperties.SecurityProperties.AcsSecurity = serverAcsSecurityElement;
			}
			if (this._advancedProperties.SecurityProperties.SharedKeyAuth.IsEnabled && CloudUtility.IsVASDeployment)
			{
				string configurationValue = CloudUtility.CloudProvider.GetConfigurationValue("authorizationKey");
				this._advancedProperties.SecurityProperties.SharedKeyAuth.AuthorizationKey = configurationValue;
			}
			if (this._advancedProperties.SecurityProperties.SslEnabled && string.IsNullOrEmpty(this._advancedProperties.SecurityProperties.SslProperties.SslCertIdentity))
			{
				throw new DataCacheException(this.logSource, 9010, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9010), new ConfigurationErrorsException());
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00021487 File Offset: 0x0001F687
		private void InitializeDeploymentSettings()
		{
			this._deploymentSettings = this._globalConfig.GetDeploymentSettings();
			ConfigManager.CurrentClusterDeploymentMode = this._deploymentSettings.DeploymentMode.Value;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x000214AF File Offset: 0x0001F6AF
		private void InitializeGlobalConfigVersions(IClusterConfigurationReader globalConfig)
		{
			this.storeVersion = globalConfig.GetStoreVersion();
			globalConfig.GetVersionProperties(ref this.versionPropertiesId);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x000214CC File Offset: 0x0001F6CC
		private void InitializeThisHostData()
		{
			if (CloudUtility.IsVASDeployment)
			{
				this._thisHost = CloudUtility.GetHost(this._host, this._service);
			}
			else
			{
				this._thisHost = Utility.GetHostUsingHostAndServiceNames(this._host, this._service, this._allHosts);
			}
			ReleaseAssert.IsTrue(this._thisHost != null);
			this._DOM_URIs = new EndpointID[1];
			this._DOM_URIs[0] = new EndpointID(this._thisHost.ServiceURIInternal);
			this._INPUT_URIs = new EndpointID[1];
			this._INPUT_URIs[0] = new EndpointID(this._thisHost.ServiceURI);
			this.restServiceURI = this._thisHost.RestServiceURI;
			if (this.SocketPort != 0)
			{
				this.SocketDOMUris = new EndpointID[1];
				this.SocketDOMUris[0] = new EndpointID(Utility.GetServiceUri(this._thisHost.Name, this.SocketPort, TransportProtocol.NetTcp));
			}
			this.InitializeNamedCaches();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x000215BE File Offset: 0x0001F7BE
		private void InitializeHostsRelatedData()
		{
			this.InitializeHostsData(this._globalConfig);
			this.InitializeThisHostData();
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000215D4 File Offset: 0x0001F7D4
		private void OverrideQuotaPropertiesIfNeeded(INamedCacheConfiguration config)
		{
			if (CloudUtility.IsVASDeployment)
			{
				CacheHostConfiguration host = CloudUtility.CloudProvider.GetHost(this._host, this._service);
				config.Quota.SizeQuota.HighWatermarkInQuotaPercent = ((host.CacheEvictionHWM != null) ? host.CacheEvictionHWM.Value : config.Quota.SizeQuota.HighWatermarkInQuotaPercent);
				config.Quota.SizeQuota.LowWatermarkInQuotaPercent = ((host.CacheEvictionLWM != null) ? host.CacheEvictionLWM.Value : config.Quota.SizeQuota.LowWatermarkInQuotaPercent);
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x00021684 File Offset: 0x0001F884
		private void InitializeNamedCaches()
		{
			List<INamedCacheConfiguration> listOfNamedCaches = this._globalConfig.GetListOfNamedCaches();
			this._namedCaches = new Dictionary<string, INamedCacheConfiguration>(listOfNamedCaches.Count);
			for (int i = 0; i < listOfNamedCaches.Count; i++)
			{
				INamedCacheConfiguration namedCacheConfiguration = listOfNamedCaches[i];
				if (Utility.IsHighAvailabilityBlocked(namedCacheConfiguration.Replicas))
				{
					EventLogProvider.EventWriteServerServiceCrash("AppFabricCachingService.Crash", GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9009));
					throw new DataCacheException("ServiceStart", 9009, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9009));
				}
				this.OverrideQuotaPropertiesIfNeeded(namedCacheConfiguration);
				this._namedCaches[namedCacheConfiguration.Name] = namedCacheConfiguration;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00021726 File Offset: 0x0001F926
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0002172E File Offset: 0x0001F92E
		public bool IsMemoryManagerEnabled { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00021737 File Offset: 0x0001F937
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0002173F File Offset: 0x0001F93F
		public bool IsSystemMemoryCheckEnabled { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00021748 File Offset: 0x0001F948
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x00021750 File Offset: 0x0001F950
		public long BufferSize { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00021759 File Offset: 0x0001F959
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00021761 File Offset: 0x0001F961
		public long MinObjectPoolSize { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002176A File Offset: 0x0001F96A
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00021772 File Offset: 0x0001F972
		public long MaxObjectPoolSize { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0002177B File Offset: 0x0001F97B
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00021783 File Offset: 0x0001F983
		public long AverageCacheItemSizeInBytes { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0002178C File Offset: 0x0001F98C
		internal IClusterConfigurationReader GlobalConfigurationReader
		{
			get
			{
				return this._globalConfig;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00021794 File Offset: 0x0001F994
		public long PerfSampleInterval
		{
			get
			{
				return this._sampleInterval;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0002179C File Offset: 0x0001F99C
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x000217A4 File Offset: 0x0001F9A4
		public bool PerfCountersEnabled
		{
			get
			{
				return this._perfCounterEnable;
			}
			internal set
			{
				this._perfCounterEnable = value;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x000217B0 File Offset: 0x0001F9B0
		public string HostName
		{
			get
			{
				string text;
				if (!this._host.Equals("localhost"))
				{
					text = this._host;
				}
				else
				{
					text = Utility.GetFQDN();
				}
				return text;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000217DF File Offset: 0x0001F9DF
		public string ServiceName
		{
			get
			{
				return this._service;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x000217E7 File Offset: 0x0001F9E7
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000217EF File Offset: 0x0001F9EF
		public ClusterConfigElement ClusterConfigConnectionSettings
		{
			get
			{
				return this._globalConfigConnSettings;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x000217F7 File Offset: 0x0001F9F7
		public IHostConfiguration NodeProperties
		{
			get
			{
				return this._thisHost;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000217FF File Offset: 0x0001F9FF
		public int CASNodeID
		{
			get
			{
				return this._thisHost.NodeId;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0002180C File Offset: 0x0001FA0C
		public string DisplayFriendlyNodeId
		{
			get
			{
				return this._thisHost.DisplayFriendlyNodeId;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00021819 File Offset: 0x0001FA19
		public int ServicePort
		{
			get
			{
				return this._thisHost.ServicePort;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00021826 File Offset: 0x0001FA26
		public int ServicePortInternal
		{
			get
			{
				return this._thisHost.ServicePortInternal;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00021833 File Offset: 0x0001FA33
		public int RestPort
		{
			get
			{
				return this._thisHost.RestPort;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00021840 File Offset: 0x0001FA40
		public int RestSslPort
		{
			get
			{
				return this._thisHost.RestSslPort;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0002184D File Offset: 0x0001FA4D
		public string RestServiceUri
		{
			get
			{
				return this.restServiceURI;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00021855 File Offset: 0x0001FA55
		public string RestSslServiceUri
		{
			get
			{
				return this._thisHost.RestSslServiceURI;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00021862 File Offset: 0x0001FA62
		public int ClusterPort
		{
			get
			{
				return this._thisHost.ClusterPort;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002186F File Offset: 0x0001FA6F
		public int ArbitrationPort
		{
			get
			{
				return this._thisHost.ArbitrationPort;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002187C File Offset: 0x0001FA7C
		public int ReplicationPort
		{
			get
			{
				return this._thisHost.ReplicationPort;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00021889 File Offset: 0x0001FA89
		public CASConfigElement CASConfigConnectionSettings
		{
			get
			{
				return this._configStoreConnectionSettings;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00021891 File Offset: 0x0001FA91
		public string FabricConfigConnectionString
		{
			get
			{
				return this._fabricConfigPath;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00021899 File Offset: 0x0001FA99
		public string TicketDirectory
		{
			get
			{
				return this._tktDir;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x000218A1 File Offset: 0x0001FAA1
		public int MaxRetries
		{
			get
			{
				return (int)this._maxRetries;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000218A9 File Offset: 0x0001FAA9
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x000218B1 File Offset: 0x0001FAB1
		public int BatchSize { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x000218BC File Offset: 0x0001FABC
		public IDomainLayoutConfiguration[] DomainInformation
		{
			get
			{
				if (CloudUtility.IsVASDeployment)
				{
					return new List<IDomainLayoutConfiguration>(CloudUtility.CloudProvider.GetAllDomains()).ToArray();
				}
				if (this._globalConfig == null || this._globalConfig.DomainLayout == null)
				{
					return null;
				}
				return this._globalConfig.DomainLayout.ToArray();
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0002190C File Offset: 0x0001FB0C
		public string[] ServiceURIs
		{
			get
			{
				return this._serverURIs;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00021914 File Offset: 0x0001FB14
		internal EndpointID[] DOMURIs
		{
			get
			{
				return this._DOM_URIs;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0002191C File Offset: 0x0001FB1C
		internal EndpointID[] InputURIs
		{
			get
			{
				return this._INPUT_URIs;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00021924 File Offset: 0x0001FB24
		public int NamedCacheCount
		{
			get
			{
				return this._namedCaches.Count;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x00021931 File Offset: 0x0001FB31
		public string LogLocation
		{
			get
			{
				return this._logLocation;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00021939 File Offset: 0x0001FB39
		public int LogLevel
		{
			get
			{
				return this._logLevel;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x00021941 File Offset: 0x0001FB41
		public int ETWMonitorInterval
		{
			get
			{
				return this._etwMonitorInterval;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00021949 File Offset: 0x0001FB49
		public AdvancedPropertiesElement AdvancedProperties
		{
			get
			{
				return this._advancedProperties;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00021951 File Offset: 0x0001FB51
		public long RetryIntervalOnMemoryExhausation
		{
			get
			{
				return 10000L * (long)this.AdvancedProperties.MemoryPressureMonitorProperties.Interval * 2L;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002196E File Offset: 0x0001FB6E
		public bool IsPublicListenerRequired
		{
			get
			{
				return this.ServicePortInternal != 0;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002197C File Offset: 0x0001FB7C
		public bool IsSeedNode(int nodeId)
		{
			List<IHostConfiguration> seedNodes = this._seedNodes;
			foreach (IHostConfiguration hostConfiguration in seedNodes)
			{
				if (hostConfiguration.NodeId == nodeId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000219DC File Offset: 0x0001FBDC
		internal bool IsThisSeedNode()
		{
			return this._thisHost.IsQuorumHost;
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x000219E9 File Offset: 0x0001FBE9
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x000219F1 File Offset: 0x0001FBF1
		internal bool IsEmulatedEnvironment { get; private set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x000219FA File Offset: 0x0001FBFA
		public int SocketPort
		{
			get
			{
				return this._thisHost.CacheSocketPort;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00021A07 File Offset: 0x0001FC07
		public int DiscoveryPort
		{
			get
			{
				return this._thisHost.CacheDiscoveryPort;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00021A14 File Offset: 0x0001FC14
		public int SslSocketPort
		{
			get
			{
				return this._thisHost.SslSocketPort;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00021A21 File Offset: 0x0001FC21
		public int SslDiscoveryPort
		{
			get
			{
				return this._thisHost.SslDiscoveryPort;
			}
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00021A2E File Offset: 0x0001FC2E
		public List<IHostConfiguration> GetListOfSeedNodes()
		{
			return this._seedNodes;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00021A36 File Offset: 0x0001FC36
		public List<IHostConfiguration> GetListOfHosts()
		{
			return this._allHosts;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00021A3E File Offset: 0x0001FC3E
		public IEnumerable<KeyValuePair<string, INamedCacheConfiguration>> GetListOfNamedCaches()
		{
			return this._namedCaches;
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00021A46 File Offset: 0x0001FC46
		internal int MaxNamedCacheCount
		{
			get
			{
				return this._maxNamedCacheCount;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00021A4E File Offset: 0x0001FC4E
		internal int BasePartitionCount
		{
			get
			{
				return this._basePartitionCount;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00021A56 File Offset: 0x0001FC56
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00021A5D File Offset: 0x0001FC5D
		internal static TraceLevel InitialLogLevel
		{
			get
			{
				return ServiceConfigurationManager._initialLogLevel;
			}
			set
			{
				ServiceConfigurationManager._initialLogLevel = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00021A65 File Offset: 0x0001FC65
		internal Version StoreVersion
		{
			get
			{
				return this.storeVersion;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x00021A6D File Offset: 0x0001FC6D
		internal long VersionPropertiesId
		{
			get
			{
				return this.versionPropertiesId;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00021A75 File Offset: 0x0001FC75
		internal int CacheGatewayCount
		{
			get
			{
				return this._allHosts.Count;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00021A82 File Offset: 0x0001FC82
		internal bool IsExplicitVASMode()
		{
			return this.ServicePort != this.ServicePortInternal;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00021A98 File Offset: 0x0001FC98
		public INamedCacheConfiguration GetNamedCacheProperties(string cacheName)
		{
			this._namedCacheLock.AcquireReaderLock(-1);
			INamedCacheConfiguration namedCacheConfiguration;
			try
			{
				this._namedCaches.TryGetValue(cacheName, out namedCacheConfiguration);
			}
			finally
			{
				this._namedCacheLock.ReleaseReaderLock();
			}
			return namedCacheConfiguration;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00021AE0 File Offset: 0x0001FCE0
		public DeploymentSettingsElement GetDeploymentSettings()
		{
			return this._deploymentSettings;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00021AE8 File Offset: 0x0001FCE8
		internal void AcquireNamedCacheReaderLock()
		{
			this._namedCacheLock.AcquireReaderLock(-1);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00021AF6 File Offset: 0x0001FCF6
		internal void ReleaseNamedCacheReaderLock()
		{
			this._namedCacheLock.ReleaseReaderLock();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x00021B04 File Offset: 0x0001FD04
		public TimeSpan GetDefaultTTL(string cacheName)
		{
			INamedCacheConfiguration namedCacheConfiguration = null;
			if (this._namedCaches.ContainsKey(cacheName))
			{
				namedCacheConfiguration = this._namedCaches[cacheName];
			}
			if (namedCacheConfiguration == null)
			{
				return new TimeSpan(0, 0, 10, 0);
			}
			if (!namedCacheConfiguration.IsExpirable)
			{
				return TimeSpan.MaxValue;
			}
			int num = (int)namedCacheConfiguration.DefaultTTL;
			return new TimeSpan(0, 0, num, 0);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x00021B5C File Offset: 0x0001FD5C
		public bool DeleteNamedCacheConfiguration(string cacheName)
		{
			this._namedCacheLock.AcquireWriterLock(-1);
			try
			{
				if (this._oldCommittedCache != null && this._oldCommittedCache.Name.Equals(cacheName))
				{
					this._oldCommittedCache = null;
				}
				if (!this._namedCaches.ContainsKey(cacheName))
				{
					return false;
				}
				this._namedCaches.Remove(cacheName);
			}
			finally
			{
				this._namedCacheLock.ReleaseWriterLock();
			}
			return true;
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00021BD8 File Offset: 0x0001FDD8
		public bool RevertNamedCacheConfiguration(string cacheName)
		{
			this._namedCacheLock.AcquireWriterLock(-1);
			try
			{
				if (this._oldCommittedCache == null || !this._namedCaches.ContainsKey(cacheName))
				{
					return false;
				}
				this._namedCaches[cacheName] = this._oldCommittedCache;
				this._oldCommittedCache = null;
			}
			finally
			{
				this._namedCacheLock.ReleaseWriterLock();
			}
			return true;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00021C48 File Offset: 0x0001FE48
		public bool CommitNamedCacheConfiguration(string cacheName)
		{
			this._namedCacheLock.AcquireReaderLock(-1);
			try
			{
				if (this._oldCommittedCache == null)
				{
					return false;
				}
				if (this._oldCommittedCache.Name.Equals(cacheName))
				{
					this._oldCommittedCache = null;
				}
			}
			finally
			{
				this._namedCacheLock.ReleaseReaderLock();
			}
			return true;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00021CA8 File Offset: 0x0001FEA8
		public void ReloadNamedCacheConfiguration(string cacheName)
		{
			IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			INamedCacheConfiguration namedCache = reader.GetNamedCache(cacheName);
			INamedCacheConfiguration namedCacheProperties = this.GetNamedCacheProperties(cacheName);
			if (namedCache == null)
			{
				if (namedCacheProperties == null)
				{
					return;
				}
				this._namedCacheLock.AcquireWriterLock(-1);
				try
				{
					this._namedCaches.Remove(cacheName);
					return;
				}
				finally
				{
					this._namedCacheLock.ReleaseWriterLock();
				}
			}
			this.OverrideQuotaPropertiesIfNeeded(namedCache);
			this._namedCacheLock.AcquireWriterLock(-1);
			try
			{
				this._oldCommittedCache = namedCacheProperties;
				this._namedCaches[cacheName] = namedCache;
			}
			finally
			{
				this._namedCacheLock.ReleaseWriterLock();
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				this.TraceCacheConfigChanges(namedCacheProperties, namedCache);
			}
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00021D60 File Offset: 0x0001FF60
		public void GetPossibleNamedCacheChanges(out List<string> existingCaches, out List<string> newCaches, out List<string> deletedCaches)
		{
			IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			List<INamedCacheConfiguration> listOfNamedCaches = reader.GetListOfNamedCaches();
			List<string> list;
			if (listOfNamedCaches == null)
			{
				list = new List<string>();
			}
			else
			{
				list = listOfNamedCaches.Select((INamedCacheConfiguration c) => c.Name).ToList<string>();
			}
			try
			{
				this.AcquireNamedCacheReaderLock();
				ServiceConfigurationManager.GetNamedCacheChanges(new List<string>(this._namedCaches.Keys), list, out existingCaches, out newCaches, out deletedCaches);
			}
			finally
			{
				this.ReleaseNamedCacheReaderLock();
			}
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00021DEC File Offset: 0x0001FFEC
		internal static void GetNamedCacheChanges(List<string> existingCacheConfig, List<string> newCacheConfig, out List<string> existingCaches, out List<string> newCaches, out List<string> deletedCaches)
		{
			IEnumerable<string> enumerable = existingCacheConfig.Intersect(newCacheConfig);
			existingCaches = enumerable.ToList<string>();
			IEnumerable<string> enumerable2 = newCacheConfig.Where((string nc) => !existingCacheConfig.Contains(nc));
			newCaches = enumerable2.ToList<string>();
			IEnumerable<string> enumerable3 = existingCacheConfig.Where((string nc) => !newCacheConfig.Contains(nc));
			deletedCaches = enumerable3.ToList<string>();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00021E68 File Offset: 0x00020068
		private void TraceCacheConfigChanges(INamedCacheConfiguration currentNC, INamedCacheConfiguration latestNC)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (currentNC == null)
			{
				EventLogWriter.WriteInfo(this.logSource, "Loading configuration for first time for cache: {0}", new object[] { latestNC.Name });
				if (latestNC.Quota.SizeQuota != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "SizeInMB: {0}; ", new object[] { latestNC.Quota.SizeQuota.SizeInMB });
				}
				if (latestNC.Quota.ConnectionQuota != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "ConnectionCount: {0}; ", new object[] { latestNC.Quota.ConnectionQuota.ConnectionCount });
				}
				if (latestNC.Quota.TransactionQuota != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "TransactionCount: {0}; ", new object[] { latestNC.Quota.TransactionQuota.TransactionCount });
				}
				if (latestNC.Quota.DataTransferQuota != null)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "DataTransferCount: {0}; ", new object[] { latestNC.Quota.DataTransferQuota.DataTransferCount });
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Cache Enabled: {0}; ", new object[] { latestNC.Enabled });
			}
			else
			{
				EventLogWriter.WriteInfo(this.logSource, "Re-loading configuration for cache: {0}", new object[] { latestNC.Name });
				if (currentNC.Quota.SizeQuota != null && latestNC.Quota.SizeQuota != null && currentNC.Quota.SizeQuota.SizeInMB != latestNC.Quota.SizeQuota.SizeInMB)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "SizeInMB Old: {0}, New: {1}; ", new object[]
					{
						currentNC.Quota.SizeQuota.SizeInMB,
						latestNC.Quota.SizeQuota.SizeInMB
					});
				}
				if (currentNC.Quota.ConnectionQuota != null && latestNC.Quota.ConnectionQuota != null && currentNC.Quota.ConnectionQuota.ConnectionCount != latestNC.Quota.ConnectionQuota.ConnectionCount)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "ConnectionCount Old: {0}, New: {1}; ", new object[]
					{
						currentNC.Quota.ConnectionQuota.ConnectionCount,
						latestNC.Quota.ConnectionQuota.ConnectionCount
					});
				}
				if (currentNC.Quota.TransactionQuota != null && latestNC.Quota.TransactionQuota != null && currentNC.Quota.TransactionQuota.TransactionCount != latestNC.Quota.TransactionQuota.TransactionCount)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "TransactionCount Old: {0}, New: {1}; ", new object[]
					{
						currentNC.Quota.TransactionQuota.TransactionCount,
						latestNC.Quota.TransactionQuota.TransactionCount
					});
				}
				if (currentNC.Quota.DataTransferQuota != null && latestNC.Quota.DataTransferQuota != null && currentNC.Quota.DataTransferQuota.DataTransferCount != latestNC.Quota.DataTransferQuota.DataTransferCount)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "DataTransferCount Old: {0}, New: {1}; ", new object[]
					{
						currentNC.Quota.DataTransferQuota.DataTransferCount,
						latestNC.Quota.DataTransferQuota.DataTransferCount
					});
				}
				if (currentNC.Enabled != latestNC.Enabled)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Cache Enabled Old: {0}, New: {1} ", new object[] { currentNC.Enabled, latestNC.Enabled });
				}
			}
			EventLogWriter.WriteInfo(this.logSource, "Cachename: {0}, Changes are: {1}; ", new object[]
			{
				latestNC.Name,
				stringBuilder.ToString()
			});
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002228C File Offset: 0x0002048C
		public void ReloadAdvancedProperties()
		{
			IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			this.InitializeAdvancedProperties(reader);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000222AC File Offset: 0x000204AC
		public void ReloadHostsProperties()
		{
			IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			this.InitializeHostsData(reader);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000222CC File Offset: 0x000204CC
		public LastPolledVersions GetAllVersions()
		{
			LastPolledVersions lastPolledVersions = default(LastPolledVersions);
			IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			reader.GetVersionProperties(ref lastPolledVersions.VersionPropertiesId);
			return lastPolledVersions;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000222FC File Offset: 0x000204FC
		public LastPolledVersions GetDeploymentVersion()
		{
			LastPolledVersions lastPolledVersions = default(LastPolledVersions);
			IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
			reader.GetDeploymentSettings(ref lastPolledVersions.VersionPropertiesId);
			return lastPolledVersions;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002232C File Offset: 0x0002052C
		public void ReloadVersionProperties(ref LastPolledVersions versions)
		{
			try
			{
				IClusterConfigurationReader reader = ClusterConfigurationFactory.GetReader(this._globalConfigConnSettings);
				VersionProperties versionProperties = reader.GetVersionProperties(ref versions.VersionPropertiesId);
				if (versionProperties != null)
				{
					this._advancedProperties.VersionProperties = versionProperties;
				}
			}
			catch (ConfigStoreException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this.logSource, "ReloadVersionProperties Failed: {0}", new object[] { ex });
				}
			}
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002239C File Offset: 0x0002059C
		public void ReloadDeploymentSettings(ref LastPolledVersions currentVersion)
		{
			DeploymentSettingsElement deploymentSettings = this._globalConfig.GetDeploymentSettings(ref currentVersion.VersionPropertiesId);
			if (deploymentSettings != null)
			{
				this._deploymentSettings = deploymentSettings;
				ConfigManager.CurrentClusterDeploymentMode = this._deploymentSettings.DeploymentMode.Value;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x000223DC File Offset: 0x000205DC
		public List<string> GetCachesToReload()
		{
			List<string> list = new List<string>();
			try
			{
				List<INamedCacheConfiguration> listOfNamedCaches = this._globalConfig.GetListOfNamedCaches();
				foreach (INamedCacheConfiguration namedCacheConfiguration in listOfNamedCaches)
				{
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string>("Velocity.ServiceConfigurationManager", "Current latest cache in DB checked  for reload: {0}", namedCacheConfiguration.ToString());
					}
					if (!this._namedCaches.ContainsKey(namedCacheConfiguration.Name) || this._namedCaches[namedCacheConfiguration.Name].Version != namedCacheConfiguration.Version)
					{
						list.Add(namedCacheConfiguration.Name);
						if (Provider.IsEnabled(TraceLevel.Info))
						{
							EventLogWriter.WriteInfo("Velocity.ServiceConfigurationManager", (!this._namedCaches.ContainsKey(namedCacheConfiguration.Name)) ? "Cache added for reload because of cache not found. Reload Operation - ADDED." : "Cache added for reload because of cache version update. Reload Operation - UPDATED.", new object[0]);
						}
					}
				}
				INamedCacheConfiguration currentNamedCache;
				foreach (INamedCacheConfiguration namedCacheConfiguration2 in this._namedCaches.Values)
				{
					currentNamedCache = namedCacheConfiguration2;
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string>("Velocity.ServiceConfigurationManager", "Current cache in Service Config checked for reload: {0}", currentNamedCache.ToString());
					}
					if (!listOfNamedCaches.Exists((INamedCacheConfiguration nc) => nc.Name.Equals(currentNamedCache.Name, StringComparison.OrdinalIgnoreCase)))
					{
						list.Add(currentNamedCache.Name);
						if (Provider.IsEnabled(TraceLevel.Info))
						{
							EventLogWriter.WriteInfo("Velocity.ServiceConfigurationManager", "Cache added for reload because unknown cache is found. Reload Operation - DELETED: {0}", new object[] { currentNamedCache.ToString() });
						}
					}
				}
			}
			catch (ConfigStoreException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(this.logSource, "GetCachesToReload Failed: {0}", new object[] { ex });
				}
			}
			return list;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00022604 File Offset: 0x00020804
		private void InitializeHostsData(IClusterConfigurationReader globalConfig)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<bool>(this.logSource, "Initialize host data with cloud provider : {0}", CloudUtility.IsVASDeployment);
			}
			if (CloudUtility.IsVASDeployment)
			{
				this._allHosts = CloudUtility.GetRoleInstances();
			}
			else
			{
				this._allHosts = globalConfig.GetListOfHosts();
			}
			string[] array = new string[this._allHosts.Count];
			for (int i = 0; i < this._allHosts.Count; i++)
			{
				array[i] = this._allHosts[i].ServiceURIInternal;
			}
			this._serverURIs = array;
			if (CloudUtility.IsVASDeployment)
			{
				this._seedNodes = new List<IHostConfiguration>();
				using (List<IHostConfiguration>.Enumerator enumerator = this._allHosts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IHostConfiguration hostConfiguration = enumerator.Current;
						if (hostConfiguration.IsQuorumHost)
						{
							this._seedNodes.Add(hostConfiguration);
						}
					}
					return;
				}
			}
			this._seedNodes = globalConfig.GetListOfNodes(true);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0001C75C File Offset: 0x0001A95C
		public static HostConfiguration GetHostDefaults()
		{
			return new HostConfiguration();
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001C6CD File Offset: 0x0001A8CD
		public static NamedCacheConfiguration GetNamedCacheDefault()
		{
			return new NamedCacheConfiguration();
		}

		// Token: 0x040006EF RID: 1775
		private const string LogSource = "Velocity.ServiceConfigurationManager";

		// Token: 0x040006F0 RID: 1776
		private IClusterConfigurationReader _globalConfig;

		// Token: 0x040006F1 RID: 1777
		private ClusterConfigElement _globalConfigConnSettings;

		// Token: 0x040006F2 RID: 1778
		private string _logLocation;

		// Token: 0x040006F3 RID: 1779
		private int _logLevel;

		// Token: 0x040006F4 RID: 1780
		private int _etwMonitorInterval;

		// Token: 0x040006F5 RID: 1781
		private string _fabricConfigPath;

		// Token: 0x040006F6 RID: 1782
		private string _tktDir;

		// Token: 0x040006F7 RID: 1783
		private int _timeout = 15000;

		// Token: 0x040006F8 RID: 1784
		private string _host;

		// Token: 0x040006F9 RID: 1785
		private string _service;

		// Token: 0x040006FA RID: 1786
		private ReaderWriterLock _namedCacheLock = new ReaderWriterLock();

		// Token: 0x040006FB RID: 1787
		private Dictionary<string, INamedCacheConfiguration> _namedCaches;

		// Token: 0x040006FC RID: 1788
		private INamedCacheConfiguration _oldCommittedCache;

		// Token: 0x040006FD RID: 1789
		private IHostConfiguration _thisHost;

		// Token: 0x040006FE RID: 1790
		private CASConfigElement _configStoreConnectionSettings;

		// Token: 0x040006FF RID: 1791
		private uint _maxRetries;

		// Token: 0x04000700 RID: 1792
		private List<IHostConfiguration> _allHosts;

		// Token: 0x04000701 RID: 1793
		private string[] _serverURIs;

		// Token: 0x04000702 RID: 1794
		private EndpointID[] _DOM_URIs;

		// Token: 0x04000703 RID: 1795
		private EndpointID[] _INPUT_URIs;

		// Token: 0x04000704 RID: 1796
		internal EndpointID[] SocketDOMUris;

		// Token: 0x04000705 RID: 1797
		private List<IHostConfiguration> _seedNodes;

		// Token: 0x04000706 RID: 1798
		private AdvancedPropertiesElement _advancedProperties;

		// Token: 0x04000707 RID: 1799
		private DeploymentSettingsElement _deploymentSettings;

		// Token: 0x04000708 RID: 1800
		private long _sampleInterval = 950L;

		// Token: 0x04000709 RID: 1801
		private bool _perfCounterEnable = true;

		// Token: 0x0400070A RID: 1802
		private int _basePartitionCount;

		// Token: 0x0400070B RID: 1803
		private int _maxNamedCacheCount;

		// Token: 0x0400070C RID: 1804
		private static TraceLevel _initialLogLevel = TraceLevel.Warning;

		// Token: 0x0400070D RID: 1805
		private Version storeVersion;

		// Token: 0x0400070E RID: 1806
		private long versionPropertiesId;

		// Token: 0x0400070F RID: 1807
		private string restServiceURI;

		// Token: 0x04000710 RID: 1808
		private string logSource = "Config.ServiceConfigurationManager";
	}
}
