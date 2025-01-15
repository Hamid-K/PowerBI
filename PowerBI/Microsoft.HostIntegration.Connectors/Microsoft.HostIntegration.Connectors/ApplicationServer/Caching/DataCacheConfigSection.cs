using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000127 RID: 295
	internal class DataCacheConfigSection : ConfigurationSection
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0001E928 File Offset: 0x0001CB28
		internal static string Name
		{
			get
			{
				return "dataCacheConfig";
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001E92F File Offset: 0x0001CB2F
		internal DataCacheConfigSection()
		{
			this.CacheHostName = "";
			this.Log = new LogConfigElement();
			this.ETWMonitor = new ETWMonitorConfigElement();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001DB97 File Offset: 0x0001BD97
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0001DBA9 File Offset: 0x0001BDA9
		[ConfigurationProperty("cacheHostName", DefaultValue = "", IsRequired = true)]
		internal string CacheHostName
		{
			get
			{
				return (string)base["cacheHostName"];
			}
			set
			{
				base["cacheHostName"] = value;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x00019FBE File Offset: 0x000181BE
		[ConfigurationProperty("timeout", DefaultValue = 15000, IsRequired = false)]
		internal int Timeout
		{
			get
			{
				return (int)base["timeout"];
			}
			set
			{
				base["timeout"] = value;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001E958 File Offset: 0x0001CB58
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0001E96A File Offset: 0x0001CB6A
		[ConfigurationProperty("log", IsRequired = false)]
		internal LogConfigElement Log
		{
			get
			{
				return (LogConfigElement)base["log"];
			}
			set
			{
				base["log"] = value;
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0001E978 File Offset: 0x0001CB78
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0001E98A File Offset: 0x0001CB8A
		[ConfigurationProperty("clusterConfig", IsRequired = true)]
		internal ClusterConfigElement ClusterConfig
		{
			get
			{
				return (ClusterConfigElement)base["clusterConfig"];
			}
			set
			{
				base["clusterConfig"] = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001E998 File Offset: 0x0001CB98
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0001E9AA File Offset: 0x0001CBAA
		[ConfigurationProperty("performanceMonitor", IsRequired = false)]
		internal PerformanceMonitorElement PerfConfig
		{
			get
			{
				return (PerformanceMonitorElement)base["performanceMonitor"];
			}
			set
			{
				base["performanceMonitor"] = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001E9B8 File Offset: 0x0001CBB8
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0001E9CA File Offset: 0x0001CBCA
		[ConfigurationProperty("etwMonitor", IsRequired = false)]
		internal ETWMonitorConfigElement ETWMonitor
		{
			get
			{
				return (ETWMonitorConfigElement)base["etwMonitor"];
			}
			set
			{
				base["etwMonitor"] = value;
			}
		}

		// Token: 0x04000682 RID: 1666
		internal const string DCACHE = "dataCacheConfig";

		// Token: 0x04000683 RID: 1667
		internal const string CLUSTER_NAME = "clusterName";

		// Token: 0x04000684 RID: 1668
		internal const string SERVICE_NAME = "cacheHostName";

		// Token: 0x04000685 RID: 1669
		internal const string TIMEOUT = "timeout";

		// Token: 0x04000686 RID: 1670
		internal const string LOGGING = "log";

		// Token: 0x04000687 RID: 1671
		internal const string CONFIG = "clusterConfig";

		// Token: 0x04000688 RID: 1672
		internal const string PERFORMANCEMONITOR = "performanceMonitor";

		// Token: 0x04000689 RID: 1673
		internal const string ETWMONITOR = "etwMonitor";
	}
}
