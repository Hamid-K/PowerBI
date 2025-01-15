using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000E0 RID: 224
	internal class ClusterConfigReader : IClusterConfigurationReader
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00019C28 File Offset: 0x00017E28
		public ClusterConfigReader(ClusterConfigElement clusterConfigReaderSetting)
		{
			this.clusterConfigElement = clusterConfigReaderSetting;
			this.clusterPropertiesReader = ConfigProviderFactory.Instance.CreateClusterPropertiesReaderInstance(this.clusterConfigElement.CloudProvider);
			this.clusterHostConfigurationReader = ConfigProviderFactory.Instance.CreateHostConfigReaderInstance(this.clusterConfigElement.CloudProvider);
			this.clusterPropertiesReader.Open(this.clusterConfigElement.ConnectionString);
			this.clusterHostConfigurationReader.Open(this.clusterConfigElement.ConnectionString, this.clusterPropertiesReader.AdvancedProperties.MemoryPressureMonitorProperties.CacheUserDataSizePerNode);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00019CB9 File Offset: 0x00017EB9
		public List<INamedCacheConfiguration> GetListOfNamedCaches()
		{
			return this.clusterPropertiesReader.GetListOfNamedCaches();
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00019CC6 File Offset: 0x00017EC6
		public int MaxNamedCacheCount
		{
			get
			{
				return this.clusterPropertiesReader.MaxNamedCacheCount;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00019CD3 File Offset: 0x00017ED3
		public int BasePartitionCount
		{
			get
			{
				return this.clusterPropertiesReader.BasePartitionCount;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00019CE0 File Offset: 0x00017EE0
		public Version GetStoreVersion()
		{
			return this.clusterPropertiesReader.GetStoreVersion();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00019CED File Offset: 0x00017EED
		public List<IHostConfiguration> GetListOfHosts()
		{
			return this.clusterHostConfigurationReader.GetListOfHosts();
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00019CFA File Offset: 0x00017EFA
		public List<IDomainLayoutConfiguration> DomainLayout
		{
			get
			{
				return this.clusterHostConfigurationReader.DomainLayout;
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00019D07 File Offset: 0x00017F07
		public IHostConfiguration GetHostUsingHostAndServiceNames(string hostName, string serviceName)
		{
			return this.clusterHostConfigurationReader.GetHostUsingHostAndServiceNames(hostName, serviceName);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00019D16 File Offset: 0x00017F16
		public INamedCacheConfiguration GetNamedCache(string cacheName)
		{
			return this.clusterPropertiesReader.GetNamedCache(cacheName);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00019D24 File Offset: 0x00017F24
		public uint MaxRoutingLookupRetries()
		{
			return this.clusterPropertiesReader.MaxRoutingLookupRetries();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00019D31 File Offset: 0x00017F31
		public uint RoutingLookupWaitInterval()
		{
			return this.clusterPropertiesReader.RoutingLookupWaitInterval();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00019D3E File Offset: 0x00017F3E
		public List<IHostConfiguration> GetListOfNodes(bool seeds)
		{
			return this.clusterHostConfigurationReader.GetListOfNodes(seeds);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00019D4C File Offset: 0x00017F4C
		public string GetClusterSize()
		{
			return this.clusterPropertiesReader.GetClusterSize();
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00019D59 File Offset: 0x00017F59
		public CASConfigElement GetCASConfigStoreConnectionSettings()
		{
			return this.clusterPropertiesReader.GetCASConfigStoreConnectionSettings();
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00019D66 File Offset: 0x00017F66
		public QuotaProperties GetQuotaProperties()
		{
			return this.clusterPropertiesReader.GetQuotaProperties();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00019D73 File Offset: 0x00017F73
		public UsageProperties GetUsageProperties()
		{
			return this.clusterPropertiesReader.GetUsageProperties();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00019D80 File Offset: 0x00017F80
		public VersionProperties GetVersionProperties()
		{
			return this.clusterPropertiesReader.GetVersionProperties();
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00019D8D File Offset: 0x00017F8D
		public VersionProperties GetVersionProperties(ref long currentVersion)
		{
			return this.clusterPropertiesReader.GetVersionProperties(ref currentVersion);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00019D9B File Offset: 0x00017F9B
		public bool TestConnection()
		{
			return this.clusterPropertiesReader.TestConnection() && this.clusterHostConfigurationReader.TestConnection();
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00019DB7 File Offset: 0x00017FB7
		public AdvancedPropertiesElement AdvancedProperties
		{
			get
			{
				return this.clusterPropertiesReader.AdvancedProperties;
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00019DC4 File Offset: 0x00017FC4
		public DeploymentSettingsElement GetDeploymentSettings()
		{
			return this.clusterPropertiesReader.GetDeploymentSettings();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00019DD1 File Offset: 0x00017FD1
		public DeploymentSettingsElement GetDeploymentSettings(ref long currentVersion)
		{
			return this.clusterPropertiesReader.GetDeploymentSettings(ref currentVersion);
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00019DDF File Offset: 0x00017FDF
		public ClusterConfigElement SecondaryConfig
		{
			get
			{
				return this.clusterPropertiesReader.SecondaryConfig;
			}
		}

		// Token: 0x040003E6 RID: 998
		private ClusterConfigElement clusterConfigElement;

		// Token: 0x040003E7 RID: 999
		private IClusterPropertiesReader clusterPropertiesReader;

		// Token: 0x040003E8 RID: 1000
		private IClusterHostConfigurationReader clusterHostConfigurationReader;
	}
}
