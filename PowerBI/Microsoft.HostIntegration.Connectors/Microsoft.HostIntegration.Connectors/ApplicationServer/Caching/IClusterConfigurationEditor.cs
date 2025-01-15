using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000DA RID: 218
	internal interface IClusterConfigurationEditor : IClusterConfigurationReader
	{
		// Token: 0x06000607 RID: 1543
		bool TryDeleteDomain(string domainAddress);

		// Token: 0x06000608 RID: 1544
		bool TryAddDomain(IDomainLayoutConfiguration domainConfiguration);

		// Token: 0x06000609 RID: 1545
		bool TryDeleteNamedCache(string name);

		// Token: 0x0600060A RID: 1546
		bool TryAddNamedCache(INamedCacheConfiguration nc);

		// Token: 0x0600060B RID: 1547
		bool EditMaxNamedCacheCount(int value);

		// Token: 0x0600060C RID: 1548
		bool EditBasePartitionCount(int value);

		// Token: 0x0600060D RID: 1549
		bool TryAddHost(IHostConfiguration host);

		// Token: 0x0600060E RID: 1550
		bool TryDeleteHost(string hostName, string serviceName);

		// Token: 0x0600060F RID: 1551
		void EditNamedCacheConfig(INamedCacheConfiguration newValue);

		// Token: 0x06000610 RID: 1552
		void EditHostConfig(IHostConfiguration newValue);

		// Token: 0x06000611 RID: 1553
		void EditDomainConfig(IDomainLayoutConfiguration newConfig);

		// Token: 0x06000612 RID: 1554
		void EditClusterSize(string newValue);

		// Token: 0x06000613 RID: 1555
		void EditCASConfigStoreConnectionSettings(CASConfigElement newConnectionString);

		// Token: 0x06000614 RID: 1556
		void EditMemoryPressureMonitorProperties(MemoryPressureMonitorProperties memPressureMonitorProps);

		// Token: 0x06000615 RID: 1557
		void EditRegionProperties(RegionProperties regionProps);

		// Token: 0x06000616 RID: 1558
		void EditStoreProperties(StoreProperties storeProps);

		// Token: 0x06000617 RID: 1559
		void EditRoutingLookUpElement(RoutingLookUpElement routingLookupProps);

		// Token: 0x06000618 RID: 1560
		void EditRequestRetryElement(RequestRetryElement requestRetryProps);

		// Token: 0x06000619 RID: 1561
		void EditAdvancedProperties(AdvancedPropertiesElement advProps);

		// Token: 0x0600061A RID: 1562
		void EditAdvancedProperties(AdvancedPropertiesElement advProps, AdvancePropertiesChange change);

		// Token: 0x0600061B RID: 1563
		void EditQuotaProperties(QuotaProperties quotaPropertiesProps);

		// Token: 0x0600061C RID: 1564
		void EditUsageProperties(UsageProperties usagePropertiesProps);

		// Token: 0x0600061D RID: 1565
		void EditVersionProperties(VersionProperties versionProperties);

		// Token: 0x0600061E RID: 1566
		void EditDeploymentSettings(DeploymentSettingsElement depProps);

		// Token: 0x0600061F RID: 1567
		void EditDeploymentSettings(DeploymentSettingsElement depProps, DeploymentSettingsChange change);

		// Token: 0x06000620 RID: 1568
		void SetStoreVersion();
	}
}
