using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000DB RID: 219
	internal class ClusterConfigDictionaryReaderWriter : ClusterConfigDictionaryReader, IClusterConfigurationEditor, IClusterConfigurationReader
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00018CD1 File Offset: 0x00016ED1
		public ClusterConfigDictionaryReaderWriter(ClusterConfigElement cs)
			: base(cs)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00018CDA File Offset: 0x00016EDA
		public bool TryDeleteNamedCache(string name)
		{
			return this.DeleteConfig("caches", name);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00018CE8 File Offset: 0x00016EE8
		public bool TryAddNamedCache(INamedCacheConfiguration nc)
		{
			return this.AddConfig("caches", nc.Name, nc);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00018CFC File Offset: 0x00016EFC
		public bool TryDeleteDomain(string domainAddress)
		{
			return this.DeleteConfig("domain", domainAddress);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00018D0A File Offset: 0x00016F0A
		public bool TryAddDomain(IDomainLayoutConfiguration domainConfiguration)
		{
			if (domainConfiguration == null || null == domainConfiguration.DomainAddress)
			{
				throw new ArgumentNullException("domainConfiguration");
			}
			return this.AddConfig("domain", domainConfiguration.DomainAddress.ToString(), domainConfiguration);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00018D3F File Offset: 0x00016F3F
		public bool EditMaxNamedCacheCount(int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return this.SetConfig("cacheAttributes", "maxCount", value);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00018D66 File Offset: 0x00016F66
		public bool EditBasePartitionCount(int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			return this.SetConfig("cacheAttributes", "partitionCount", value);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00018D8D File Offset: 0x00016F8D
		public bool TryDeleteHost(string hostName, string serviceName)
		{
			return this.DeleteConfig("hosts", HostCollection.GetHostKey(hostName, serviceName));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00018DA1 File Offset: 0x00016FA1
		public bool TryAddHost(IHostConfiguration host)
		{
			return this.AddConfig("hosts", HostCollection.GetHostKey(host.Name, host.ServiceName), host);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00018DC0 File Offset: 0x00016FC0
		public void EditNamedCacheConfig(INamedCacheConfiguration newConfig)
		{
			this.SetConfig("caches", newConfig.Name, newConfig);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00018DD5 File Offset: 0x00016FD5
		public void EditHostConfig(IHostConfiguration newValue)
		{
			this.SetConfig("hosts", HostCollection.GetHostKey(newValue.Name, newValue.ServiceName), newValue);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00018DF5 File Offset: 0x00016FF5
		public void EditDomainConfig(IDomainLayoutConfiguration newConfig)
		{
			if (newConfig == null || null == newConfig.DomainAddress)
			{
				throw new ArgumentNullException("newConfig");
			}
			this.SetConfig("domain", newConfig.DomainAddress.ToString(), newConfig);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00018E2B File Offset: 0x0001702B
		public void EditClusterSize(string newValue)
		{
			this.SetConfig("dataCache", "size", newValue);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00018E3F File Offset: 0x0001703F
		public void EditCASConfigStoreConnectionSettings(CASConfigElement newConnectionSettings)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "partitionStoreConnectionSettings", newConnectionSettings);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00018E53 File Offset: 0x00017053
		public void EditMemoryPressureMonitorProperties(MemoryPressureMonitorProperties memPressureMonitorProps)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "memoryPressureMonitor", memPressureMonitorProps);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00018E67 File Offset: 0x00017067
		public void EditSecurityProperties(ServerSecurityProperties securityProps)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "securityProperties", securityProps);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00018E7B File Offset: 0x0001707B
		public void EditRegionProperties(RegionProperties regionProps)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "regionProperties", regionProps);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00018E8F File Offset: 0x0001708F
		public void EditStoreProperties(StoreProperties storeProps)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "storeProperties", storeProps);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00018EA3 File Offset: 0x000170A3
		public void EditRoutingLookUpElement(RoutingLookUpElement routingLookupProps)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "routingLookupRetry", routingLookupProps);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00018EB7 File Offset: 0x000170B7
		public void EditRequestRetryElement(RequestRetryElement requestRetryProps)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "requestRetry", requestRetryProps);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00018ECB File Offset: 0x000170CB
		public void EditQuotaProperties(QuotaProperties quotaProperties)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "quotaProperties", quotaProperties);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00018EDF File Offset: 0x000170DF
		public void EditVersionProperties(VersionProperties versionProperties)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "versionProperties", versionProperties);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00018EF4 File Offset: 0x000170F4
		public void SetStoreVersion()
		{
			StoreVersionProperties storeVersionProperties = new StoreVersionProperties();
			storeVersionProperties.ClusterConfigStoreVersion = "3.0.0.0";
			this.SetConfig(AdvancedPropertiesElement.Name, "storeVersionProperties", storeVersionProperties);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00018F24 File Offset: 0x00017124
		public void EditAdvancedProperties(AdvancedPropertiesElement advProps)
		{
			AdvancePropertiesChange advancePropertiesChange = default(AdvancePropertiesChange);
			advancePropertiesChange[AdvanceChanges.ChangeAll] = true;
			this.EditAdvancedProperties(advProps, advancePropertiesChange);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00018F50 File Offset: 0x00017150
		public void EditAdvancedProperties(AdvancedPropertiesElement advProps, AdvancePropertiesChange change)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			if (change[AdvanceChanges.CASConfigChange])
			{
				list.Add(new KeyValuePair<string, object>("partitionStoreConnectionSettings", advProps.CasConfigConnectionSettings));
			}
			if (change[AdvanceChanges.MemoryPressureMonitorChange])
			{
				list.Add(new KeyValuePair<string, object>("memoryPressureMonitor", advProps.MemoryPressureMonitorProperties));
			}
			if (change[AdvanceChanges.RegionPropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("regionProperties", advProps.RegionProperties));
			}
			if (change[AdvanceChanges.StorePropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("storeProperties", advProps.StoreProperties));
			}
			if (change[AdvanceChanges.RequestRetryChange])
			{
				list.Add(new KeyValuePair<string, object>("requestRetry", advProps.RequestRetryElement));
			}
			if (change[AdvanceChanges.RoutingLookupChange])
			{
				list.Add(new KeyValuePair<string, object>("routingLookupRetry", advProps.RoutingLookUpConfig));
			}
			if (change[AdvanceChanges.SecurityPropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("securityProperties", advProps.SecurityProperties));
			}
			if (change[AdvanceChanges.TransportPropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("transportProperties", advProps.TransportProperties));
			}
			if (change[AdvanceChanges.QuotaPropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("quotaProperties", advProps.QuotaProperties));
			}
			if (change[AdvanceChanges.UsagePropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("usageProperties", advProps.UsageProperties));
			}
			if (change[AdvanceChanges.VersionPropertiesChange])
			{
				list.Add(new KeyValuePair<string, object>("versionProperties", advProps.VersionProperties));
			}
			this.SetConfigs(AdvancedPropertiesElement.Name, list);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000190E2 File Offset: 0x000172E2
		public void EditUsageProperties(UsageProperties usageProperties)
		{
			this.SetConfig(AdvancedPropertiesElement.Name, "usageProperties", usageProperties);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000190F8 File Offset: 0x000172F8
		public void EditDeploymentSettings(DeploymentSettingsElement deplProps)
		{
			DeploymentSettingsChange deploymentSettingsChange = default(DeploymentSettingsChange);
			deploymentSettingsChange[DeploymentChanges.ChangeAll] = true;
			this.EditDeploymentSettings(deplProps, deploymentSettingsChange);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00019120 File Offset: 0x00017320
		public void EditDeploymentSettings(DeploymentSettingsElement deplProps, DeploymentSettingsChange change)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			if (change[DeploymentChanges.DeploymentModeChange])
			{
				list.Add(new KeyValuePair<string, object>("deploymentMode", deplProps.DeploymentMode));
			}
			if (change[DeploymentChanges.GracefulShutdownModeChange])
			{
				list.Add(new KeyValuePair<string, object>("gracefulShutdown", deplProps.GracefulShutdown));
			}
			this.SetConfigs(DeploymentSettingsElement.Name, list);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00019180 File Offset: 0x00017380
		private bool SetConfig(string type, string key, object value)
		{
			object obj = this._dbConfig.BeginTransaction();
			Version storeVersion = base.GetStoreVersion();
			this._dbConfig.Delete(obj, type, key, 0L);
			SerializationContext serializationContext = new SerializationContext(storeVersion, ClientVersionInfo.Invalid);
			bool flag = this._dbConfig.Insert(obj, type, key, SerializationUtility.SerializeToByteArray(value, serializationContext), DateTime.UtcNow.Ticks);
			this._dbConfig.EndTransaction(obj, false);
			return flag;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000191F0 File Offset: 0x000173F0
		private void SetConfigs(string type, List<KeyValuePair<string, object>> kvs)
		{
			object obj = this._dbConfig.BeginTransaction();
			Version storeVersion = base.GetStoreVersion();
			bool flag = true;
			SerializationContext serializationContext = new SerializationContext(storeVersion, ClientVersionInfo.Invalid);
			foreach (KeyValuePair<string, object> keyValuePair in kvs)
			{
				this._dbConfig.Delete(obj, type, keyValuePair.Key, 0L);
				flag &= this._dbConfig.Insert(obj, type, keyValuePair.Key, SerializationUtility.SerializeToByteArray(keyValuePair.Value, serializationContext), DateTime.UtcNow.Ticks);
			}
			this._dbConfig.EndTransaction(obj, !flag);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000192B4 File Offset: 0x000174B4
		private bool AddConfig(string type, string key, object value)
		{
			object obj = this._dbConfig.BeginTransaction();
			Version storeVersion = base.GetStoreVersion();
			SerializationContext serializationContext = new SerializationContext(storeVersion, ClientVersionInfo.Invalid);
			bool flag = this._dbConfig.Insert(obj, type, key, SerializationUtility.SerializeToByteArray(value, serializationContext), DateTime.UtcNow.Ticks);
			this._dbConfig.EndTransaction(obj, false);
			return flag;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00019314 File Offset: 0x00017514
		private bool DeleteConfig(string type, string key)
		{
			object obj = this._dbConfig.BeginTransaction();
			bool flag = this._dbConfig.Delete(obj, type, key, 0L);
			this._dbConfig.EndTransaction(obj, false);
			return flag;
		}
	}
}
