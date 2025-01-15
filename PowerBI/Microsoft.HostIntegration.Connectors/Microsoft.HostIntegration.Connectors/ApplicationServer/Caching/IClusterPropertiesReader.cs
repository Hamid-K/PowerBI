using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200011E RID: 286
	internal interface IClusterPropertiesReader
	{
		// Token: 0x06000835 RID: 2101
		void Open(string readerParams);

		// Token: 0x06000836 RID: 2102
		void Close();

		// Token: 0x06000837 RID: 2103
		List<INamedCacheConfiguration> GetListOfNamedCaches();

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000838 RID: 2104
		int MaxNamedCacheCount { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000839 RID: 2105
		int BasePartitionCount { get; }

		// Token: 0x0600083A RID: 2106
		string GetClusterSize();

		// Token: 0x0600083B RID: 2107
		Version GetStoreVersion();

		// Token: 0x0600083C RID: 2108
		INamedCacheConfiguration GetNamedCache(string cacheName);

		// Token: 0x0600083D RID: 2109
		uint MaxRoutingLookupRetries();

		// Token: 0x0600083E RID: 2110
		uint RoutingLookupWaitInterval();

		// Token: 0x0600083F RID: 2111
		CASConfigElement GetCASConfigStoreConnectionSettings();

		// Token: 0x06000840 RID: 2112
		QuotaProperties GetQuotaProperties();

		// Token: 0x06000841 RID: 2113
		UsageProperties GetUsageProperties();

		// Token: 0x06000842 RID: 2114
		VersionProperties GetVersionProperties();

		// Token: 0x06000843 RID: 2115
		VersionProperties GetVersionProperties(ref long currentVersion);

		// Token: 0x06000844 RID: 2116
		bool TestConnection();

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000845 RID: 2117
		AdvancedPropertiesElement AdvancedProperties { get; }

		// Token: 0x06000846 RID: 2118
		DeploymentSettingsElement GetDeploymentSettings();

		// Token: 0x06000847 RID: 2119
		DeploymentSettingsElement GetDeploymentSettings(ref long currentVersion);

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000848 RID: 2120
		ClusterConfigElement SecondaryConfig { get; }
	}
}
