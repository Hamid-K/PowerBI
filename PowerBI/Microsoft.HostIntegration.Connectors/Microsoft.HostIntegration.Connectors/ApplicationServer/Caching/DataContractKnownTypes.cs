using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Fabric.Data;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200039D RID: 925
	internal static class DataContractKnownTypes
	{
		// Token: 0x060020D4 RID: 8404 RVA: 0x00064270 File Offset: 0x00062470
		public static void AddType(Type type)
		{
			Interlocked.Exchange<HashSet<Type>>(ref DataContractKnownTypes._dataContractTypes, new HashSet<Type>(DataContractKnownTypes._dataContractTypes) { type });
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0006429C File Offset: 0x0006249C
		public static void AddTypes(IEnumerable<Type> types)
		{
			HashSet<Type> hashSet = new HashSet<Type>(DataContractKnownTypes._dataContractTypes);
			hashSet.UnionWith(types);
			Interlocked.Exchange<HashSet<Type>>(ref DataContractKnownTypes._dataContractTypes, hashSet);
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x000642C7 File Offset: 0x000624C7
		public static IEnumerable<Type> KnownTypes
		{
			get
			{
				return DataContractKnownTypes._dataContractTypes;
			}
		}

		// Token: 0x04001330 RID: 4912
		private static HashSet<Type> _dataContractTypes = new HashSet<Type>(new Type[]
		{
			typeof(LPM),
			typeof(Exception),
			typeof(CacheRegionStats),
			typeof(NamedCacheStats),
			typeof(DetailedCacheNodeStats),
			typeof(HostCacheStats),
			typeof(CacheRegionStats[]),
			typeof(DetailedNamedCacheStats[]),
			typeof(DetailedNamedCacheStats),
			typeof(RequestBody),
			typeof(BroadcastResult),
			typeof(NamedCachePropertyList),
			typeof(ServerNotificationProperties),
			typeof(DataCacheDeploymentMode),
			typeof(List<KeyValuePair<NamedCacheProperty, object>>),
			typeof(EvictionType),
			typeof(ExpirationType),
			typeof(DataCacheTag),
			typeof(InternalCacheItemVersion),
			typeof(NotificationRequest),
			typeof(List<KeyValuePair<string, object>>),
			typeof(PartitionNotificationLsnRequest),
			typeof(PartitionNotificationRequest),
			typeof(PartitionId),
			typeof(byte[][]),
			typeof(BroadcastResult),
			typeof(ShutdownStatus),
			typeof(string[]),
			typeof(DataCacheException),
			typeof(NamedCacheConfiguration),
			typeof(CASConfigElement),
			typeof(HostConfiguration),
			typeof(NamedCacheDeploymentType),
			typeof(QuotaProperties),
			typeof(ConsistencyType),
			typeof(HostNodeDomainConfigurationElementCollection),
			typeof(HostNodeDomainConfigurationElement),
			typeof(MemcachePortsCollection),
			typeof(PolicyConfig),
			typeof(CacheQuotaThresholds),
			typeof(EvictionConfig),
			typeof(DataTransferQuotaConfig),
			typeof(ExpirationConfig),
			typeof(BackingStoreConfig),
			typeof(ConnectionQuotaConfig),
			typeof(CacheSizeQuotaConfig),
			typeof(ProviderSetting),
			typeof(MemoryPressureMonitorProperties),
			typeof(CacheSizeQuotaConfig),
			typeof(WriteBehindConfig),
			typeof(TransactionQuotaConfig),
			typeof(ReadThroughConfig),
			typeof(HostNodeDomainConfigurationElement[]),
			typeof(RegionProperties),
			typeof(RequestRetryElement),
			typeof(StoreProperties),
			typeof(TransportElement),
			typeof(ServerSecurityProperties),
			typeof(ProviderConfig),
			typeof(RoutingLookUpElement),
			typeof(ServerAcsSecurityElement),
			typeof(List<LocalCacheItem>),
			typeof(AuthorizationElement),
			typeof(SharedKeyAuthorization),
			typeof(SslProperties),
			typeof(UsageProperties),
			typeof(VersionProperties),
			typeof(ProviderSetting),
			typeof(ProviderSettings),
			typeof(DataCacheSecurityMode),
			typeof(DataCacheProtectionLevel),
			typeof(List<ProviderSetting>),
			typeof(QuotaConfig),
			typeof(List<AllowElement>),
			typeof(ResourceElementCollection),
			typeof(DeploymentModeElement),
			typeof(List<ResourceElement>),
			typeof(List<KnownUsageProviderElement>),
			typeof(KeyValuePair<string, int>[]),
			typeof(KnownUsageProviderElementCollection),
			typeof(DomainLayoutCollectionConfiguration),
			typeof(HostNodeDomainConfigurationElement),
			typeof(HostNodeDomainConfigurationElement[]),
			typeof(DomainLayoutConfigurationElementCollection),
			typeof(DomainLayoutConfiguration),
			typeof(DomainLayoutConfiguration[]),
			typeof(HostNodeDomainConfigurationElementCollection),
			typeof(DomainLayoutCollectionConfiguration),
			typeof(Uri),
			typeof(SerializationConfig),
			typeof(EnumeratorState),
			typeof(BaseEnumeratorState),
			typeof(FindEnumeratorState),
			typeof(UnionAllEnumeratorState),
			typeof(FixedDepthEnumeratorState),
			typeof(OmEnumeratorStateForTagsIntersection)
		});
	}
}
