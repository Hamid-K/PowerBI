using System;
using System.Configuration;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200012D RID: 301
	internal sealed class LocalConfigReaderWriter : LocalConfigReader
	{
		// Token: 0x060008AF RID: 2223 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		internal LocalConfigReaderWriter(string path)
			: base(path, false)
		{
			if (this._dcache == null)
			{
				this._dcache = new DataCacheConfigSection();
				this._cfg.Sections.Add(DataCacheConfigSection.Name, this._dcache);
				this._cfg.Save();
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001EE2E File Offset: 0x0001D02E
		public void EditLogLocation(string newLoc)
		{
			this._dcache.Log.Location = newLoc;
			this.Save();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001EE47 File Offset: 0x0001D047
		public void EditHostParameters(string newServiceName)
		{
			this._dcache.CacheHostName = newServiceName;
			this.Save();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001EE5B File Offset: 0x0001D05B
		public void EditClusterConfigConnectionSettings(ClusterConfigElement elem)
		{
			this._dcache.ClusterConfig = elem;
			this.Save();
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001EE6F File Offset: 0x0001D06F
		public void EditLogLevel(int newLevel)
		{
			this._dcache.Log.Level = newLevel;
			this.Save();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001EE88 File Offset: 0x0001D088
		public void EditPerfmonStatus(bool enabled)
		{
			this._dcache.PerfConfig.IsEnable = enabled;
			this.Save();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001EEA4 File Offset: 0x0001D0A4
		private void Save()
		{
			try
			{
				this._cfg.Save(ConfigurationSaveMode.Minimal);
			}
			catch (ConfigurationErrorsException ex)
			{
				throw new DataCacheException(ex.Message, ex);
			}
		}
	}
}
