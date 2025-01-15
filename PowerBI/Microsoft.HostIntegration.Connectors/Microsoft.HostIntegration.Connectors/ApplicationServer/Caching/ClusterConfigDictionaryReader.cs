using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D9 RID: 217
	internal class ClusterConfigDictionaryReader : MarshalByRefObject, IClusterConfigurationReader
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x00018190 File Offset: 0x00016390
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x00018197 File Offset: 0x00016397
		private static Dictionary<string, Dictionary<string, Type>> ConfigStoreEntryInformation { get; set; } = new Dictionary<string, Dictionary<string, Type>>();

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001819F File Offset: 0x0001639F
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x000181A6 File Offset: 0x000163A6
		private static Dictionary<string, Type> ConfigStoreListEntryInformation { get; set; } = new Dictionary<string, Type>();

		// Token: 0x060005DF RID: 1503 RVA: 0x000181B0 File Offset: 0x000163B0
		static ClusterConfigDictionaryReader()
		{
			ClusterConfigDictionaryReader.RegisterList(CacheCollection.Name, typeof(INamedCacheConfiguration));
			ClusterConfigDictionaryReader.RegisterList("domain", typeof(IDomainLayoutConfiguration));
			ClusterConfigDictionaryReader.RegisterList(HostCollection.Name, typeof(IHostConfiguration));
			ClusterConfigDictionaryReader.RegisterList(HostCollection.Name, typeof(IHostConfiguration));
			ClusterConfigDictionaryReader.RegisterEntry("cacheAttributes", "maxCount", typeof(int));
			ClusterConfigDictionaryReader.RegisterEntry("cacheAttributes", "partitionCount", typeof(int));
			ClusterConfigDictionaryReader.RegisterEntry("dataCache", "size", typeof(string));
			ClusterConfigDictionaryReader.RegisterEntry(DeploymentSettingsElement.Name, "deploymentMode", typeof(DeploymentModeElement));
			ClusterConfigDictionaryReader.RegisterEntry(DeploymentSettingsElement.Name, "gracefulShutdown", typeof(GracefulShutdownElement));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "regionProperties", typeof(RegionProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "storeProperties", typeof(StoreProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "partitionStoreConnectionSettings", typeof(CASConfigElement));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "requestRetry", typeof(RequestRetryElement));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "routingLookupRetry", typeof(RoutingLookUpElement));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "memoryPressureMonitor", typeof(MemoryPressureMonitorProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "securityProperties", typeof(ServerSecurityProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "transportProperties", typeof(TransportElement));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "quotaProperties", typeof(QuotaProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "usageProperties", typeof(UsageProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "versionProperties", typeof(VersionProperties));
			ClusterConfigDictionaryReader.RegisterEntry("advancedProperties", "storeVersionProperties", typeof(StoreVersionProperties));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x000183CC File Offset: 0x000165CC
		internal static void RegisterEntry(string type, string key, Type valueType)
		{
			Dictionary<string, Type> dictionary = null;
			ClusterConfigDictionaryReader.ConfigStoreEntryInformation.TryGetValue(type, out dictionary);
			if (dictionary == null)
			{
				dictionary = new Dictionary<string, Type>();
				ClusterConfigDictionaryReader.ConfigStoreEntryInformation.Add(type, dictionary);
			}
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, valueType);
				return;
			}
			dictionary[key] = valueType;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00018418 File Offset: 0x00016618
		internal static void RegisterList(string type, Type valueType)
		{
			if (!ClusterConfigDictionaryReader.ConfigStoreListEntryInformation.ContainsKey(type))
			{
				ClusterConfigDictionaryReader.ConfigStoreListEntryInformation.Add(type, valueType);
				return;
			}
			ClusterConfigDictionaryReader.ConfigStoreListEntryInformation[type] = valueType;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00018440 File Offset: 0x00016640
		public ClusterConfigDictionaryReader(ClusterConfigElement cs)
		{
			this._dbConfig = CustomProviderFactory.CreateInstance(cs.Provider);
			this._dbConfig.Open(cs.ConnectionString);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001846C File Offset: 0x0001666C
		public Version GetStoreVersion()
		{
			Version version = this._dbConfig.RetrieveStoreVersion();
			SerializationContext serializationContext = new SerializationContext(version, ClientVersionInfo.Invalid);
			StoreVersionProperties config = this.GetConfig<StoreVersionProperties>(AdvancedPropertiesElement.Name, "storeVersionProperties", serializationContext);
			if (ConfigManager.IsStoreVersionHigherThan2000(version) && config != null)
			{
				version = VersioningUtility.StringToVersion(config.ClusterConfigStoreVersion);
			}
			return version;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000184BC File Offset: 0x000166BC
		public List<INamedCacheConfiguration> GetListOfNamedCaches()
		{
			List<INamedCacheConfiguration> list = new List<INamedCacheConfiguration>();
			ICollection<ConfigStoreEntry> entries = this.GetEntries(CacheCollection.Name);
			SerializationContext serializationContext = this.GetSerializationContext();
			foreach (ConfigStoreEntry configStoreEntry in entries)
			{
				INamedCacheConfiguration namedCacheConfiguration = (INamedCacheConfiguration)SerializationUtility.DeserializeFromByteArray(configStoreEntry.Value, serializationContext);
				if (namedCacheConfiguration != null)
				{
					namedCacheConfiguration.Version = configStoreEntry.Version;
					Utility.UpdateExpirationSettings(namedCacheConfiguration);
					if (!Utility.IsValidExpirationSettings(namedCacheConfiguration))
					{
						int num = 17036;
						throw new DataCacheException("CONFIGURATION_MANAGER", num, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, num));
					}
				}
				list.Add(namedCacheConfiguration);
			}
			return list;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00018578 File Offset: 0x00016778
		private SerializationContext GetSerializationContext()
		{
			Version storeVersion = this.GetStoreVersion();
			return new SerializationContext(storeVersion, ClientVersionInfo.Invalid);
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00018598 File Offset: 0x00016798
		public int MaxNamedCacheCount
		{
			get
			{
				int num = this.GetConfig<int>("cacheAttributes", "maxCount");
				if (num == 0)
				{
					num = 128;
				}
				return num;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x000185C0 File Offset: 0x000167C0
		public int BasePartitionCount
		{
			get
			{
				int num = this.GetConfig<int>("cacheAttributes", "partitionCount");
				if (num == 0)
				{
					num = ConfigManager.GetPartitionCount(this.GetClusterSize());
				}
				return num;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000185F0 File Offset: 0x000167F0
		public List<IDomainLayoutConfiguration> DomainLayout
		{
			get
			{
				return this.GetConfigs<IDomainLayoutConfiguration>("domain");
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001860A File Offset: 0x0001680A
		public List<IHostConfiguration> GetListOfHosts()
		{
			return this.GetConfigs<IHostConfiguration>(HostCollection.Name);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018618 File Offset: 0x00016818
		public List<IHostConfiguration> GetListOfNodes(bool quorumHost)
		{
			List<IHostConfiguration> listOfHosts = this.GetListOfHosts();
			if (listOfHosts == null)
			{
				return null;
			}
			if (quorumHost)
			{
				return listOfHosts.FindAll(new Predicate<IHostConfiguration>(ClusterConfigDictionaryReader.IsQuorumHostPredicate));
			}
			return listOfHosts.FindAll(new Predicate<IHostConfiguration>(ClusterConfigDictionaryReader.NotQuorumHostPredicate));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00018659 File Offset: 0x00016859
		public IHostConfiguration GetHostUsingHostAndServiceNames(string hostName, string serviceName)
		{
			return this.GetConfig<IHostConfiguration>(HostCollection.Name, HostCollection.GetHostKey(hostName, serviceName));
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00018670 File Offset: 0x00016870
		public INamedCacheConfiguration GetNamedCache(string cacheName)
		{
			long num = 0L;
			INamedCacheConfiguration config = this.GetConfig<INamedCacheConfiguration>(CacheCollection.Name, cacheName, ref num);
			if (config != null)
			{
				config.Version = num;
				Utility.UpdateExpirationSettings(config);
				if (!Utility.IsValidExpirationSettings(config))
				{
					int num2 = 17036;
					throw new DataCacheException("CONFIGURATION_MANAGER", num2, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, num2));
				}
			}
			return config;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000186C8 File Offset: 0x000168C8
		public uint RoutingLookupWaitInterval()
		{
			RoutingLookUpElement routingLookUpElement = this.GetConfig<RoutingLookUpElement>(AdvancedPropertiesElement.Name, "routingLookupRetry");
			if (routingLookUpElement == null)
			{
				routingLookUpElement = new RoutingLookUpElement();
			}
			return (uint)routingLookUpElement.WaitInterval;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000186F8 File Offset: 0x000168F8
		public uint MaxRoutingLookupRetries()
		{
			RoutingLookUpElement routingLookUpElement = this.GetConfig<RoutingLookUpElement>(AdvancedPropertiesElement.Name, "routingLookupRetry");
			if (routingLookUpElement == null)
			{
				routingLookUpElement = new RoutingLookUpElement();
			}
			return (uint)routingLookUpElement.MaxAttempts;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00018725 File Offset: 0x00016925
		public string GetClusterSize()
		{
			return this.GetConfig<string>("dataCache", "size");
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00018737 File Offset: 0x00016937
		public CASConfigElement GetCASConfigStoreConnectionSettings()
		{
			return this.GetConfig<CASConfigElement>(AdvancedPropertiesElement.Name, "partitionStoreConnectionSettings");
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00018749 File Offset: 0x00016949
		public QuotaProperties GetQuotaProperties()
		{
			return this.GetConfig<QuotaProperties>(AdvancedPropertiesElement.Name, "quotaProperties");
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001875B File Offset: 0x0001695B
		public UsageProperties GetUsageProperties()
		{
			return this.GetConfig<UsageProperties>(AdvancedPropertiesElement.Name, "usageProperties");
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00018770 File Offset: 0x00016970
		public VersionProperties GetVersionProperties()
		{
			long num = 0L;
			return this.GetVersionProperties(ref num);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00018788 File Offset: 0x00016988
		public VersionProperties GetVersionProperties(ref long currentVersion)
		{
			return this.GetConfig<VersionProperties>(AdvancedPropertiesElement.Name, "versionProperties", ref currentVersion);
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001879B File Offset: 0x0001699B
		public AdvancedPropertiesElement AdvancedProperties
		{
			get
			{
				return this.GetAdvancedProperties();
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000187A4 File Offset: 0x000169A4
		private AdvancedPropertiesElement GetAdvancedProperties()
		{
			return new AdvancedPropertiesElement
			{
				RegionProperties = (this.GetConfig<RegionProperties>(AdvancedPropertiesElement.Name, "regionProperties") ?? new RegionProperties()),
				StoreProperties = (this.GetConfig<StoreProperties>(AdvancedPropertiesElement.Name, "storeProperties") ?? new StoreProperties()),
				CasConfigConnectionSettings = (this.GetConfig<CASConfigElement>(AdvancedPropertiesElement.Name, "partitionStoreConnectionSettings") ?? new CASConfigElement()),
				RequestRetryElement = (this.GetConfig<RequestRetryElement>(AdvancedPropertiesElement.Name, "requestRetry") ?? new RequestRetryElement()),
				RoutingLookUpConfig = (this.GetConfig<RoutingLookUpElement>(AdvancedPropertiesElement.Name, "routingLookupRetry") ?? new RoutingLookUpElement()),
				MemoryPressureMonitorProperties = (this.GetConfig<MemoryPressureMonitorProperties>(AdvancedPropertiesElement.Name, "memoryPressureMonitor") ?? new MemoryPressureMonitorProperties()),
				SecurityProperties = (this.GetConfig<ServerSecurityProperties>(AdvancedPropertiesElement.Name, "securityProperties") ?? new ServerSecurityProperties()),
				TransportProperties = (this.GetConfig<TransportElement>(AdvancedPropertiesElement.Name, "transportProperties") ?? new TransportElement()),
				QuotaProperties = (this.GetConfig<QuotaProperties>(AdvancedPropertiesElement.Name, "quotaProperties") ?? new QuotaProperties()),
				UsageProperties = (this.GetConfig<UsageProperties>(AdvancedPropertiesElement.Name, "usageProperties") ?? new UsageProperties()),
				VersionProperties = (this.GetConfig<VersionProperties>(AdvancedPropertiesElement.Name, "versionProperties") ?? new VersionProperties())
			};
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00018910 File Offset: 0x00016B10
		public DeploymentSettingsElement GetDeploymentSettings()
		{
			return new DeploymentSettingsElement
			{
				DeploymentMode = (this.GetConfig<DeploymentModeElement>(DeploymentSettingsElement.Name, "deploymentMode") ?? new DeploymentModeElement()),
				GracefulShutdown = (this.GetConfig<GracefulShutdownElement>(DeploymentSettingsElement.Name, "gracefulShutdown") ?? new GracefulShutdownElement())
			};
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00018964 File Offset: 0x00016B64
		public DeploymentSettingsElement GetDeploymentSettings(ref long currentVersion)
		{
			DeploymentModeElement config = this.GetConfig<DeploymentModeElement>(DeploymentSettingsElement.Name, "deploymentMode", ref currentVersion);
			if (config == null)
			{
				return null;
			}
			return new DeploymentSettingsElement
			{
				DeploymentMode = config
			};
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00018998 File Offset: 0x00016B98
		public bool TestConnection()
		{
			bool flag;
			try
			{
				this._dbConfig.TestConnection();
				flag = true;
			}
			catch (ConfigStoreException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000189CC File Offset: 0x00016BCC
		public ClusterConfigElement SecondaryConfig
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000189CF File Offset: 0x00016BCF
		private T GetConfig<T>(string type, string key)
		{
			return this.GetConfig<T>(type, key, null);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000189DC File Offset: 0x00016BDC
		private T GetConfig<T>(string type, string key, SerializationContext context)
		{
			Type registeredType = ClusterConfigDictionaryReader.GetRegisteredType(type, key);
			if (typeof(T) != registeredType)
			{
				throw new InvalidOperationException("invalid type cast");
			}
			object obj = this._dbConfig.BeginTransaction();
			byte[] value = this._dbConfig.GetValue(obj, type, key);
			this._dbConfig.EndTransaction(obj, false);
			return this.GetValue<T>(value, context);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00018A40 File Offset: 0x00016C40
		private T GetConfig<T>(string type, string key, ref long currentVersion)
		{
			Type registeredType = ClusterConfigDictionaryReader.GetRegisteredType(type, key);
			if (typeof(T) != registeredType)
			{
				throw new InvalidOperationException("invalid type cast");
			}
			object obj = this._dbConfig.BeginTransaction();
			byte[] array = null;
			ConfigStoreEntry entry = this._dbConfig.GetEntry(obj, type, key);
			if (entry != null && entry.Version > currentVersion)
			{
				array = entry.Value;
				currentVersion = entry.Version;
			}
			this._dbConfig.EndTransaction(obj, false);
			return this.GetValue<T>(array);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00018ABF File Offset: 0x00016CBF
		private T GetValue<T>(byte[] data)
		{
			return this.GetValue<T>(data, null);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00018ACC File Offset: 0x00016CCC
		private T GetValue<T>(byte[] data, SerializationContext context)
		{
			if (data == null)
			{
				return default(T);
			}
			if (context == null)
			{
				context = this.GetSerializationContext();
			}
			return (T)((object)SerializationUtility.DeserializeFromByteArray(data, context));
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00018B00 File Offset: 0x00016D00
		private static Type GetRegisteredType(string type, string key)
		{
			Type type2 = null;
			if (key == null)
			{
				ClusterConfigDictionaryReader.ConfigStoreListEntryInformation.TryGetValue(type, out type2);
			}
			else
			{
				Dictionary<string, Type> dictionary = null;
				if (ClusterConfigDictionaryReader.ConfigStoreEntryInformation.TryGetValue(type, out dictionary))
				{
					dictionary.TryGetValue(key, out type2);
				}
				if (null == type2)
				{
					ClusterConfigDictionaryReader.ConfigStoreListEntryInformation.TryGetValue(type, out type2);
				}
			}
			return type2;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00018B58 File Offset: 0x00016D58
		public ICollection<ConfigStoreEntry> GetEntries(string type)
		{
			object obj = this._dbConfig.BeginTransaction();
			ICollection<ConfigStoreEntry> entries = this._dbConfig.GetEntries(obj, type);
			this._dbConfig.EndTransaction(obj, false);
			return entries;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00018B90 File Offset: 0x00016D90
		internal static object Deserialize(string key, string type, byte[] value, Version version)
		{
			Type registeredType = ClusterConfigDictionaryReader.GetRegisteredType(type, key);
			if (null == registeredType)
			{
				return registeredType;
			}
			object obj = ClusterConfigDictionaryReader.Deserialize(value, version);
			if (obj == null)
			{
				return obj;
			}
			if (obj.GetType() != registeredType)
			{
				throw new InvalidCastException("value does not match with registered type");
			}
			return obj;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00018BD8 File Offset: 0x00016DD8
		internal static object Deserialize(byte[] value, Version version)
		{
			SerializationContext serializationContext = new SerializationContext(version, ClientVersionInfo.Invalid);
			return SerializationUtility.DeserializeFromByteArray(value, serializationContext);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00018BF8 File Offset: 0x00016DF8
		private List<T> GetConfigs<T>(string type) where T : class
		{
			Type registeredType = ClusterConfigDictionaryReader.GetRegisteredType(type, null);
			if (typeof(T) != registeredType)
			{
				throw new InvalidOperationException("invalid type cast");
			}
			object obj = this._dbConfig.BeginTransaction();
			ICollection<ConfigStoreEntry> entries = this._dbConfig.GetEntries(obj, type);
			this._dbConfig.EndTransaction(obj, false);
			List<T> list = new List<T>();
			SerializationContext serializationContext = this.GetSerializationContext();
			foreach (ConfigStoreEntry configStoreEntry in entries)
			{
				list.Add((T)((object)SerializationUtility.DeserializeFromByteArray(configStoreEntry.Value, serializationContext)));
			}
			return list;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018CB4 File Offset: 0x00016EB4
		private static bool IsQuorumHostPredicate(IHostConfiguration h)
		{
			return h != null && h.IsQuorumHost;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00018CC1 File Offset: 0x00016EC1
		private static bool NotQuorumHostPredicate(IHostConfiguration h)
		{
			return h != null && !h.IsQuorumHost;
		}

		// Token: 0x040003D4 RID: 980
		protected ICustomProvider _dbConfig;
	}
}
