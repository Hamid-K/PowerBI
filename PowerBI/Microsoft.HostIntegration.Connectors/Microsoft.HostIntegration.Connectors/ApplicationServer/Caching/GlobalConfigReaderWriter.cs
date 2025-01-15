using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000116 RID: 278
	internal sealed class GlobalConfigReaderWriter : GlobalConfigReader, IClusterConfigurationEditor, IClusterConfigurationReader
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x0001CC7E File Offset: 0x0001AE7E
		public GlobalConfigReaderWriter(string path, int retries)
			: base(path, false)
		{
			this._retryCount = retries;
			this.Init();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001CC95 File Offset: 0x0001AE95
		public GlobalConfigReaderWriter()
			: base(null, false)
		{
			this._retryCount = 3;
			this.Init();
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public bool TryDeleteDomain(string domainAddress)
		{
			if (this._section.Domains.Delete(domainAddress))
			{
				this.Save();
				return true;
			}
			return false;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001CCCC File Offset: 0x0001AECC
		public bool TryAddDomain(IDomainLayoutConfiguration domainConfiguration)
		{
			if (domainConfiguration == null)
			{
				throw new ArgumentNullException("domainConfiguration");
			}
			DomainLayoutConfiguration domainLayoutConfiguration = new DomainLayoutConfiguration(domainConfiguration);
			this._section.Domains.Add(domainLayoutConfiguration);
			this.Save();
			return true;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001CD06 File Offset: 0x0001AF06
		public bool TryDeleteNamedCache(string name)
		{
			if (this._section.Caches.Delete(name))
			{
				this.Save();
				return true;
			}
			return false;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001CD24 File Offset: 0x0001AF24
		public bool TryAddNamedCache(INamedCacheConfiguration nc)
		{
			if (nc.Quota.Enabled && !base.AdvancedProperties.QuotaProperties.CacheSizeQuotaEnabled)
			{
				AdvancedPropertiesElement advancedPropertiesElement = (AdvancedPropertiesElement)SerializationUtility.Clone(base.AdvancedProperties);
				advancedPropertiesElement.QuotaProperties.CacheSizeQuotaEnabled = true;
				AdvancePropertiesChange advancePropertiesChange = default(AdvancePropertiesChange);
				advancePropertiesChange[AdvanceChanges.QuotaPropertiesChange] = true;
				this.EditAdvancedProperties(base.AdvancedProperties, advancePropertiesChange);
			}
			if (this._section.Caches.Add(nc))
			{
				this.Save();
				return true;
			}
			return false;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001CDAB File Offset: 0x0001AFAB
		public bool EditMaxNamedCacheCount(int value)
		{
			this._section.Caches.MaxCount = value;
			this.Save();
			return true;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001CDC5 File Offset: 0x0001AFC5
		public bool EditBasePartitionCount(int value)
		{
			this._section.Caches.BasePartitionCount = value;
			this.Save();
			return true;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001CDDF File Offset: 0x0001AFDF
		public bool TryDeleteHost(string hostName, string serviceName)
		{
			if (this._section.Hosts.Delete(HostCollection.GetHostKey(hostName, serviceName)))
			{
				this.Save();
				return true;
			}
			return false;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001CE03 File Offset: 0x0001B003
		public bool TryAddHost(IHostConfiguration host)
		{
			if (this._section.Hosts.Add(host))
			{
				this.Save();
				return true;
			}
			return false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001CE21 File Offset: 0x0001B021
		public void EditNamedCacheConfig(INamedCacheConfiguration newConfig)
		{
			this._section.Caches.Delete(newConfig.Name);
			this._section.Caches.Add(newConfig);
			this.Save();
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001CE52 File Offset: 0x0001B052
		public void EditHostConfig(IHostConfiguration newValue)
		{
			this._section.Hosts.Delete(HostCollection.GetHostKey(newValue.Name, newValue.ServiceName));
			this._section.Hosts.Add(newValue);
			this.Save();
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001CE90 File Offset: 0x0001B090
		public void EditDomainConfig(IDomainLayoutConfiguration newConfig)
		{
			if (newConfig == null || null == newConfig.DomainAddress)
			{
				throw new ArgumentNullException("newConfig");
			}
			this._section.Domains.Delete(newConfig.DomainAddress.ToString());
			DomainLayoutConfiguration domainLayoutConfiguration = new DomainLayoutConfiguration(newConfig);
			this._section.Domains.Add(domainLayoutConfiguration);
			this.Save();
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001CEF3 File Offset: 0x0001B0F3
		public void EditClusterSize(string newValue)
		{
			this._section.Size = newValue;
			this.Save();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001CF07 File Offset: 0x0001B107
		public void EditCASConfigStoreConnectionSettings(CASConfigElement newConnectionSettings)
		{
			this._section.AdvancedConfigs.CasConfigConnectionSettings = newConnectionSettings;
			this.Save();
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001CF20 File Offset: 0x0001B120
		public void EditMemoryPressureMonitorProperties(MemoryPressureMonitorProperties memPressureMonitorProps)
		{
			this._section.AdvancedConfigs.MemoryPressureMonitorProperties = memPressureMonitorProps;
			this.Save();
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001CF39 File Offset: 0x0001B139
		public void EditSecurityProperties(ServerSecurityProperties securityProps)
		{
			this._section.AdvancedConfigs.SecurityProperties = securityProps;
			this.Save();
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001CF52 File Offset: 0x0001B152
		public void EditRegionProperties(RegionProperties regionProps)
		{
			this._section.AdvancedConfigs.RegionProperties = regionProps;
			this.Save();
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001CF6B File Offset: 0x0001B16B
		public void EditStoreProperties(StoreProperties storeProps)
		{
			this._section.AdvancedConfigs.StoreProperties = storeProps;
			this.Save();
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001CF84 File Offset: 0x0001B184
		public void EditRoutingLookUpElement(RoutingLookUpElement routingLookupProps)
		{
			this._section.AdvancedConfigs.RoutingLookUpConfig = routingLookupProps;
			this.Save();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001CF9D File Offset: 0x0001B19D
		public void EditRequestRetryElement(RequestRetryElement requestRetryProps)
		{
			this._section.AdvancedConfigs.RequestRetryElement = requestRetryProps;
			this.Save();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001CFB6 File Offset: 0x0001B1B6
		public void EditQuotaProperties(QuotaProperties quotaProperties)
		{
			this._section.AdvancedConfigs.QuotaProperties = quotaProperties;
			this.Save();
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001CFCF File Offset: 0x0001B1CF
		public void EditUsageProperties(UsageProperties usageProperties)
		{
			this._section.AdvancedConfigs.UsageProperties = usageProperties;
			this.Save();
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		public void EditVersionProperties(VersionProperties versionProperties)
		{
			this._section.AdvancedConfigs.VersionProperties = versionProperties;
			this.Save();
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001D004 File Offset: 0x0001B204
		public void SetStoreVersion()
		{
			StoreVersionProperties storeVersionProperties = new StoreVersionProperties();
			storeVersionProperties.ClusterConfigStoreVersion = "3.0.0.0";
			this._section.AdvancedConfigs.StoreVersion = storeVersionProperties;
			this.Save();
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001D03C File Offset: 0x0001B23C
		public void EditAdvancedProperties(AdvancedPropertiesElement advProps)
		{
			AdvancePropertiesChange advancePropertiesChange = default(AdvancePropertiesChange);
			advancePropertiesChange[AdvanceChanges.ChangeAll] = true;
			this.EditAdvancedProperties(advProps, advancePropertiesChange);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001D068 File Offset: 0x0001B268
		public void EditAdvancedProperties(AdvancedPropertiesElement advProps, AdvancePropertiesChange change)
		{
			AdvancedPropertiesElement advancedPropertiesElement = (AdvancedPropertiesElement)SerializationUtility.Clone(this._section.AdvancedConfigs);
			if (change[AdvanceChanges.CASConfigChange])
			{
				advancedPropertiesElement.CasConfigConnectionSettings = advProps.CasConfigConnectionSettings;
			}
			if (change[AdvanceChanges.MemoryPressureMonitorChange])
			{
				advancedPropertiesElement.MemoryPressureMonitorProperties = advProps.MemoryPressureMonitorProperties;
			}
			if (change[AdvanceChanges.RegionPropertiesChange])
			{
				advancedPropertiesElement.RegionProperties = advProps.RegionProperties;
			}
			if (change[AdvanceChanges.StorePropertiesChange])
			{
				advancedPropertiesElement.StoreProperties = advProps.StoreProperties;
			}
			if (change[AdvanceChanges.RequestRetryChange])
			{
				advancedPropertiesElement.RequestRetryElement = advProps.RequestRetryElement;
			}
			if (change[AdvanceChanges.RoutingLookupChange])
			{
				advancedPropertiesElement.RoutingLookUpConfig = advProps.RoutingLookUpConfig;
			}
			if (change[AdvanceChanges.SecurityPropertiesChange])
			{
				advancedPropertiesElement.SecurityProperties = advProps.SecurityProperties;
			}
			if (change[AdvanceChanges.TransportPropertiesChange])
			{
				advancedPropertiesElement.TransportProperties = advProps.TransportProperties;
			}
			if (change[AdvanceChanges.QuotaPropertiesChange])
			{
				advancedPropertiesElement.QuotaProperties = advProps.QuotaProperties;
			}
			if (change[AdvanceChanges.UsagePropertiesChange])
			{
				advancedPropertiesElement.UsageProperties = advProps.UsageProperties;
			}
			if (change[AdvanceChanges.VersionPropertiesChange])
			{
				advancedPropertiesElement.VersionProperties = advProps.VersionProperties;
			}
			this._section.AdvancedConfigs = advancedPropertiesElement;
			this.Save();
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
		public void EditDeploymentSettings(DeploymentSettingsElement deplProps)
		{
			DeploymentSettingsChange deploymentSettingsChange = default(DeploymentSettingsChange);
			deploymentSettingsChange[DeploymentChanges.ChangeAll] = true;
			this.EditDeploymentSettings(deplProps, deploymentSettingsChange);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		public void EditDeploymentSettings(DeploymentSettingsElement deplProps, DeploymentSettingsChange change)
		{
			DeploymentSettingsElement deploymentSettingsElement = (DeploymentSettingsElement)SerializationUtility.Clone(this._section.DeploymentSettings);
			if (change[DeploymentChanges.DeploymentModeChange])
			{
				deploymentSettingsElement.DeploymentMode = deplProps.DeploymentMode;
			}
			if (change[DeploymentChanges.GracefulShutdownModeChange])
			{
				deploymentSettingsElement.GracefulShutdown = deplProps.GracefulShutdown;
			}
			this._section.DeploymentSettings = deploymentSettingsElement;
			this.Save();
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001D230 File Offset: 0x0001B430
		private void Save()
		{
			bool flag = true;
			int num = 0;
			while (flag)
			{
				flag = false;
				num++;
				try
				{
					string filePath = this._cfg.FilePath;
					this._cfg.Save(ConfigurationSaveMode.Minimal);
				}
				catch (Exception ex)
				{
					if (!this.IsExpected(ex, num, out flag))
					{
						throw;
					}
					if (!flag)
					{
						if (ex is UnauthorizedAccessException)
						{
							ConfigFile.ThrowException(9002, ex);
						}
						else
						{
							ConfigFile.ThrowException(ex);
						}
					}
					else
					{
						Thread.Sleep(5000);
					}
				}
			}
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
		internal bool IsExpected(Exception e, int retries, out bool retryPossible)
		{
			bool flag = false;
			retryPossible = false;
			if (GlobalConfigReaderWriter.CanRetryFor(e))
			{
				if (retries <= this._retryCount)
				{
					retryPossible = true;
				}
				flag = true;
			}
			if (e is PathTooLongException || e is NotSupportedException)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001D2EF File Offset: 0x0001B4EF
		private static bool CanRetryFor(Exception e)
		{
			return e is ConfigurationErrorsException || e is IOException || e is UnauthorizedAccessException || e is DirectoryNotFoundException;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001D314 File Offset: 0x0001B514
		private void Init()
		{
			if (this._section == null)
			{
				this._section = new DataCacheSection();
				try
				{
					this._cfg.Sections.Add(DataCacheSection.Name, this._section);
					this.Save();
				}
				catch (ConfigurationErrorsException ex)
				{
					ConfigFile.ThrowException(ex);
				}
				catch (ArgumentException ex2)
				{
					ConfigFile.ThrowException(ex2);
				}
			}
		}

		// Token: 0x04000652 RID: 1618
		private int _retryCount;
	}
}
