using System;
using System.Configuration;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200012C RID: 300
	internal class LocalConfigReader
	{
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001EC26 File Offset: 0x0001CE26
		public LocalConfigReader(string path, bool throwIfSectionNotFound)
		{
			this.Init(path, throwIfSectionNotFound);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001EC36 File Offset: 0x0001CE36
		public LocalConfigReader(string path)
		{
			this.Init(path, true);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001EC46 File Offset: 0x0001CE46
		public LocalConfigReader()
		{
			this._dcache = (DataCacheConfigSection)ConfigurationManager.GetSection(DataCacheConfigSection.Name);
			if (this._dcache == null)
			{
				ConfigFile.ThrowException(9003, DataCacheSection.Name);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001EC7A File Offset: 0x0001CE7A
		public ClusterConfigElement GetClusterConfigConnectionSettings()
		{
			return this._dcache.ClusterConfig;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001EC87 File Offset: 0x0001CE87
		public string GetLogLocation()
		{
			return this._dcache.Log.Location;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001EC99 File Offset: 0x0001CE99
		public PerformanceMonitorElement GetPerfElement()
		{
			return this._dcache.PerfConfig;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		internal void GetFirstCacheConfig(out string host, out string service, out int masterTimeout)
		{
			if (this.GetClusterConfigConnectionSettings().ConnectionString.Length == 0)
			{
				host = "localhost";
			}
			else
			{
				string text;
				if (!CloudUtility.IsVASDeployment)
				{
					string fqdn;
					host = (fqdn = Utility.GetFQDN());
					text = fqdn;
				}
				else
				{
					string currentEndpointAddress;
					host = (currentEndpointAddress = CloudUtility.GetCurrentEndpointAddress());
					text = currentEndpointAddress;
				}
				host = text;
			}
			service = this._dcache.CacheHostName;
			masterTimeout = this._dcache.Timeout;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001ED09 File Offset: 0x0001CF09
		internal int Timeout
		{
			get
			{
				return this._dcache.Timeout;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x0001ED16 File Offset: 0x0001CF16
		public int LogLevel
		{
			get
			{
				return this._dcache.Log.Level;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001ED28 File Offset: 0x0001CF28
		public int ETWMonitorInterval
		{
			get
			{
				return this._dcache.ETWMonitor.Interval;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001ED3C File Offset: 0x0001CF3C
		private void Init(string path, bool throwIfSectionNotFound)
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
				this._dcache = (DataCacheConfigSection)this._cfg.GetSection(DataCacheConfigSection.Name);
			}
			catch (ConfigurationErrorsException ex2)
			{
				ConfigFile.ThrowException(ex2);
			}
			if (this._dcache == null && throwIfSectionNotFound)
			{
				ConfigFile.ThrowException(9003, DataCacheSection.Name);
			}
		}

		// Token: 0x0400069A RID: 1690
		protected DataCacheConfigSection _dcache;

		// Token: 0x0400069B RID: 1691
		protected Configuration _cfg;
	}
}
