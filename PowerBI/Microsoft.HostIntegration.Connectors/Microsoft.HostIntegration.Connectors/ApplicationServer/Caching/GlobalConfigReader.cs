using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000115 RID: 277
	internal class GlobalConfigReader : MarshalByRefObject, IClusterConfigurationReader
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x0001C7DE File Offset: 0x0001A9DE
		public GlobalConfigReader(string path)
		{
			this.Init(path, true);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001C7EE File Offset: 0x0001A9EE
		public GlobalConfigReader()
		{
			this.Init(null, true);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001C7FE File Offset: 0x0001A9FE
		public GlobalConfigReader(string path, bool throwIfSectionNotFound)
		{
			this.Init(path, throwIfSectionNotFound);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001C80E File Offset: 0x0001AA0E
		public Version GetStoreVersion()
		{
			return VersioningUtility.StringToVersion(this._section.AdvancedConfigs.StoreVersion.ClusterConfigStoreVersion);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001C82C File Offset: 0x0001AA2C
		public List<INamedCacheConfiguration> GetListOfNamedCaches()
		{
			IEnumerator enumerator = this._section.Caches.GetEnumerator();
			List<INamedCacheConfiguration> list = new List<INamedCacheConfiguration>();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				INamedCacheConfiguration namedCacheConfiguration = (INamedCacheConfiguration)obj;
				if (namedCacheConfiguration != null)
				{
					Utility.UpdateExpirationSettings(namedCacheConfiguration);
					if (!Utility.IsValidExpirationSettings(namedCacheConfiguration))
					{
						ConfigFile.ThrowException(17036);
					}
				}
				list.Add(namedCacheConfiguration);
			}
			return list;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0001C889 File Offset: 0x0001AA89
		public int MaxNamedCacheCount
		{
			get
			{
				return this._section.Caches.MaxCount;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0001C89C File Offset: 0x0001AA9C
		public int BasePartitionCount
		{
			get
			{
				int num = this._section.Caches.BasePartitionCount;
				if (num == 2147483647)
				{
					num = ConfigManager.GetPartitionCount(this.GetClusterSize());
				}
				return num;
			}
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		public List<IHostConfiguration> GetListOfHosts()
		{
			IEnumerator enumerator = this._section.Hosts.GetEnumerator();
			List<IHostConfiguration> list = new List<IHostConfiguration>();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				list.Add((IHostConfiguration)obj);
			}
			return list;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001C910 File Offset: 0x0001AB10
		public List<IHostConfiguration> GetListOfNodes(bool seeds)
		{
			IEnumerator enumerator = this._section.Hosts.GetEnumerator();
			List<IHostConfiguration> list = new List<IHostConfiguration>();
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				IHostConfiguration hostConfiguration = (IHostConfiguration)obj;
				if (hostConfiguration.IsQuorumHost == seeds)
				{
					list.Add(hostConfiguration);
				}
			}
			return list;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0001C95C File Offset: 0x0001AB5C
		public List<IDomainLayoutConfiguration> DomainLayout
		{
			get
			{
				if (this._section.Domains == null)
				{
					return null;
				}
				IEnumerator enumerator = this._section.Domains.GetEnumerator();
				List<IDomainLayoutConfiguration> list = new List<IDomainLayoutConfiguration>();
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					DomainLayoutConfiguration domainLayoutConfiguration = (DomainLayoutConfiguration)obj;
					list.Add(domainLayoutConfiguration);
				}
				return list;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001C9AD File Offset: 0x0001ABAD
		public IHostConfiguration GetHostUsingHostAndServiceNames(string hostName, string serviceName)
		{
			return this._section.Hosts.Get(HostCollection.GetHostKey(hostName, serviceName));
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001C9C8 File Offset: 0x0001ABC8
		public IHostConfiguration GetHostUsingHostAndServicePort(string hostName, int servicePortNumber)
		{
			foreach (object obj in this._section.Hosts)
			{
				IHostConfiguration hostConfiguration = (IHostConfiguration)obj;
				if (hostConfiguration.Name.Equals(hostName, StringComparison.OrdinalIgnoreCase) && hostConfiguration.ServicePort == servicePortNumber)
				{
					return hostConfiguration;
				}
			}
			return null;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001CA18 File Offset: 0x0001AC18
		public INamedCacheConfiguration GetNamedCache(string cacheName)
		{
			INamedCacheConfiguration namedCacheConfiguration = this._section.Caches.Get(cacheName);
			if (namedCacheConfiguration != null)
			{
				Utility.UpdateExpirationSettings(namedCacheConfiguration);
				if (!Utility.IsValidExpirationSettings(namedCacheConfiguration))
				{
					ConfigFile.ThrowException(17036);
				}
			}
			return namedCacheConfiguration;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001CA53 File Offset: 0x0001AC53
		public uint MaxRoutingLookupRetries()
		{
			return (uint)this._section.AdvancedConfigs.RoutingLookUpConfig.MaxAttempts;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001CA6A File Offset: 0x0001AC6A
		public uint RoutingLookupWaitInterval()
		{
			return (uint)this._section.AdvancedConfigs.RoutingLookUpConfig.WaitInterval;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001CA81 File Offset: 0x0001AC81
		public string GetClusterSize()
		{
			return this._section.Size;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001CA8E File Offset: 0x0001AC8E
		public CASConfigElement GetCASConfigStoreConnectionSettings()
		{
			return this._section.AdvancedConfigs.CasConfigConnectionSettings;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		public QuotaProperties GetQuotaProperties()
		{
			return this._section.AdvancedConfigs.QuotaProperties;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001CAB2 File Offset: 0x0001ACB2
		public UsageProperties GetUsageProperties()
		{
			return this._section.AdvancedConfigs.UsageProperties;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001CAC4 File Offset: 0x0001ACC4
		public VersionProperties GetVersionProperties()
		{
			long num = 0L;
			return this.GetVersionProperties(ref num);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001CADC File Offset: 0x0001ACDC
		public VersionProperties GetVersionProperties(ref long currentVersion)
		{
			return this._section.AdvancedConfigs.VersionProperties;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x0001CAEE File Offset: 0x0001ACEE
		public AdvancedPropertiesElement AdvancedProperties
		{
			get
			{
				return this._section.AdvancedConfigs;
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001CAFB File Offset: 0x0001ACFB
		public DeploymentSettingsElement GetDeploymentSettings()
		{
			return this._section.DeploymentSettings;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001CB08 File Offset: 0x0001AD08
		public DeploymentSettingsElement GetDeploymentSettings(ref long currentVersion)
		{
			return this.GetDeploymentSettings();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001CB10 File Offset: 0x0001AD10
		public bool TestConnection()
		{
			return this._cfg.HasFile;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x000189CC File Offset: 0x00016BCC
		public virtual ClusterConfigElement SecondaryConfig
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001CB22 File Offset: 0x0001AD22
		private void Init(string path, bool throwIfSectionNotFound)
		{
			if (path == null || path.Trim().Length == 0)
			{
				path = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
			}
			this.LoadConfigurationFromFile(path, throwIfSectionNotFound);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001CB50 File Offset: 0x0001AD50
		protected void LoadConfigurationFromFile(string path, bool throwIfSectionNotFound)
		{
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			exeConfigurationFileMap.ExeConfigFilename = path;
			try
			{
				this._cfg = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
			}
			catch (ConfigurationErrorsException ex)
			{
				ConfigFile.ThrowException(ex);
			}
			if (!File.Exists(path))
			{
				ConfigFile.ThrowException(9004, path);
			}
			try
			{
				this._section = (DataCacheSection)this._cfg.GetSection(DataCacheSection.Name);
			}
			catch (ConfigurationErrorsException ex2)
			{
				ConfigFile.ThrowException(9001, 100, ex2);
			}
			if (this._section == null && throwIfSectionNotFound)
			{
				ConfigFile.ThrowException(9003, DataCacheSection.Name);
			}
			ArrayList arrayList = new ArrayList();
			this.ListErrors(arrayList);
			if (arrayList.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < arrayList.Count; i++)
				{
					stringBuilder.AppendLine(arrayList[i].ToString());
				}
				throw new DataCacheException("CONFIGURATION_MANAGER", 9001, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9001, stringBuilder.ToString()), false);
			}
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001CC68 File Offset: 0x0001AE68
		private void ListErrors(IList errorList)
		{
			if (this._section != null)
			{
				this._section.ListErrorsInSection(errorList);
			}
		}

		// Token: 0x04000650 RID: 1616
		protected Configuration _cfg;

		// Token: 0x04000651 RID: 1617
		protected DataCacheSection _section;
	}
}
