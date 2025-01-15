using System;
using System.Collections;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000112 RID: 274
	internal class DataCacheSection : ConfigurationSection
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0001C605 File Offset: 0x0001A805
		public static string Name
		{
			get
			{
				return "dataCache";
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001C60C File Offset: 0x0001A80C
		internal void ListErrorsInSection(IList errorList)
		{
			base.ListErrors(errorList);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001C615 File Offset: 0x0001A815
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001C627 File Offset: 0x0001A827
		[ConfigurationProperty("size", DefaultValue = "", IsRequired = true)]
		public string Size
		{
			get
			{
				return (string)base["size"];
			}
			set
			{
				base["size"] = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001C635 File Offset: 0x0001A835
		// (set) Token: 0x06000753 RID: 1875 RVA: 0x0001C647 File Offset: 0x0001A847
		[ConfigurationCollection(typeof(CacheCollection), AddItemName = "cache")]
		[ConfigurationProperty("caches", IsDefaultCollection = false, IsRequired = false)]
		public CacheCollection Caches
		{
			get
			{
				return (CacheCollection)base["caches"];
			}
			set
			{
				base["caches"] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x0001C655 File Offset: 0x0001A855
		// (set) Token: 0x06000755 RID: 1877 RVA: 0x0001C54B File Offset: 0x0001A74B
		[ConfigurationCollection(typeof(DomainLayoutConfigurationElementCollection), AddItemName = "domain")]
		[ConfigurationProperty("domains", IsRequired = false)]
		public DomainLayoutConfigurationElementCollection Domains
		{
			get
			{
				return (DomainLayoutConfigurationElementCollection)base["domains"];
			}
			set
			{
				base["domains"] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x0001C667 File Offset: 0x0001A867
		// (set) Token: 0x06000757 RID: 1879 RVA: 0x00017A96 File Offset: 0x00015C96
		[ConfigurationProperty("hosts", IsDefaultCollection = false, IsRequired = false)]
		[ConfigurationCollection(typeof(HostCollection), AddItemName = "host")]
		public HostCollection Hosts
		{
			get
			{
				return (HostCollection)base["hosts"];
			}
			set
			{
				base["hosts"] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001C679 File Offset: 0x0001A879
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0001C68B File Offset: 0x0001A88B
		[ConfigurationProperty("advancedProperties", IsRequired = false)]
		public AdvancedPropertiesElement AdvancedConfigs
		{
			get
			{
				return (AdvancedPropertiesElement)base["advancedProperties"];
			}
			set
			{
				base["advancedProperties"] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001C699 File Offset: 0x0001A899
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0001C6AB File Offset: 0x0001A8AB
		[ConfigurationProperty("deploymentSettings", IsRequired = false)]
		public DeploymentSettingsElement DeploymentSettings
		{
			get
			{
				return (DeploymentSettingsElement)base["deploymentSettings"];
			}
			set
			{
				base["deploymentSettings"] = value;
			}
		}

		// Token: 0x04000644 RID: 1604
		internal const string DCACHE = "dataCache";

		// Token: 0x04000645 RID: 1605
		internal const string SIZE = "size";

		// Token: 0x04000646 RID: 1606
		internal const string CACHES = "caches";

		// Token: 0x04000647 RID: 1607
		internal const string HOSTS = "hosts";

		// Token: 0x04000648 RID: 1608
		internal const string CACHE = "cache";

		// Token: 0x04000649 RID: 1609
		internal const string HOST = "host";

		// Token: 0x0400064A RID: 1610
		internal const string ADVANCED = "advancedProperties";

		// Token: 0x0400064B RID: 1611
		internal const string DEPLOYMENT = "deploymentSettings";
	}
}
