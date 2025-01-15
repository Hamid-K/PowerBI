using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D8 RID: 216
	internal interface IClusterConfigurationReader
	{
		// Token: 0x060005C5 RID: 1477
		List<INamedCacheConfiguration> GetListOfNamedCaches();

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005C6 RID: 1478
		int MaxNamedCacheCount { get; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005C7 RID: 1479
		int BasePartitionCount { get; }

		// Token: 0x060005C8 RID: 1480
		Version GetStoreVersion();

		// Token: 0x060005C9 RID: 1481
		List<IHostConfiguration> GetListOfHosts();

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005CA RID: 1482
		List<IDomainLayoutConfiguration> DomainLayout { get; }

		// Token: 0x060005CB RID: 1483
		IHostConfiguration GetHostUsingHostAndServiceNames(string hostName, string serviceName);

		// Token: 0x060005CC RID: 1484
		INamedCacheConfiguration GetNamedCache(string cacheName);

		// Token: 0x060005CD RID: 1485
		uint MaxRoutingLookupRetries();

		// Token: 0x060005CE RID: 1486
		uint RoutingLookupWaitInterval();

		// Token: 0x060005CF RID: 1487
		List<IHostConfiguration> GetListOfNodes(bool seeds);

		// Token: 0x060005D0 RID: 1488
		string GetClusterSize();

		// Token: 0x060005D1 RID: 1489
		CASConfigElement GetCASConfigStoreConnectionSettings();

		// Token: 0x060005D2 RID: 1490
		QuotaProperties GetQuotaProperties();

		// Token: 0x060005D3 RID: 1491
		UsageProperties GetUsageProperties();

		// Token: 0x060005D4 RID: 1492
		VersionProperties GetVersionProperties();

		// Token: 0x060005D5 RID: 1493
		VersionProperties GetVersionProperties(ref long currentVersion);

		// Token: 0x060005D6 RID: 1494
		bool TestConnection();

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060005D7 RID: 1495
		AdvancedPropertiesElement AdvancedProperties { get; }

		// Token: 0x060005D8 RID: 1496
		DeploymentSettingsElement GetDeploymentSettings();

		// Token: 0x060005D9 RID: 1497
		DeploymentSettingsElement GetDeploymentSettings(ref long currentVersion);

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060005DA RID: 1498
		ClusterConfigElement SecondaryConfig { get; }
	}
}
