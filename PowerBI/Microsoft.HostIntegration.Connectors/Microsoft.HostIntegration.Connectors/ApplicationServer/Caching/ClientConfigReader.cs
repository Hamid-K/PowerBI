using System;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000D7 RID: 215
	internal sealed class ClientConfigReader
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00017F24 File Offset: 0x00016124
		public ClientConfigReader(string path)
		{
			this.Init(path);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00017F33 File Offset: 0x00016133
		public ClientConfigReader()
		{
			this.Init(null);
		}

		// Token: 0x17000113 RID: 275
		public DataCacheNamedClient this[string key]
		{
			get
			{
				return this._dcacheClients.Get(key);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00017F50 File Offset: 0x00016150
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00017F58 File Offset: 0x00016158
		public DataCacheLogSink LogSink
		{
			get
			{
				return this._logSink;
			}
			set
			{
				this._logSink = value;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00017F64 File Offset: 0x00016164
		private void Init(string path)
		{
			DataCacheClientSection dataCacheClientSection = null;
			DataCacheClientsSection dataCacheClientsSection = null;
			try
			{
				if (path != null)
				{
					Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
					{
						ExeConfigFilename = path
					}, ConfigurationUserLevel.None);
					dataCacheClientSection = (DataCacheClientSection)configuration.GetSection(DataCacheClientSection.Name);
					dataCacheClientsSection = (DataCacheClientsSection)configuration.GetSection("dataCacheClients");
				}
				else
				{
					dataCacheClientSection = (DataCacheClientSection)ConfigurationManager.GetSection(DataCacheClientSection.Name);
					dataCacheClientsSection = (DataCacheClientsSection)ConfigurationManager.GetSection("dataCacheClients");
				}
			}
			catch (ConfigurationErrorsException ex)
			{
				ConfigFile.ThrowException(8003, ex);
			}
			this.ReadConfiguration(dataCacheClientSection, dataCacheClientsSection);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00017FFC File Offset: 0x000161FC
		private void ReadConfiguration(DataCacheClientSection dcacheClient, DataCacheClientsSection dcacheClients)
		{
			if (dcacheClient != null && dcacheClients != null)
			{
				throw new DataCacheException("CONFIGURATION_MANAGER", 8003, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "ERRCMC0004"));
			}
			if (dcacheClients != null)
			{
				this._dcacheClients = new DataCacheNamedClientCollection();
				foreach (object obj in dcacheClients.Clients)
				{
					ConfigurationElement configurationElement = (ConfigurationElement)obj;
					this._dcacheClients.Add((DataCacheNamedClient)configurationElement);
				}
				if (this._dcacheClients.Get("default") == null)
				{
					this._dcacheClients.Add(new DataCacheNamedClient(new DataCacheClientSection()));
				}
				this.LogSink = new DataCacheLogSink(TraceUtils.GetLogSinkTypeFromTraceSinkType(dcacheClients.TraceSettings.SinkType), dcacheClients.TraceSettings.ClientTraceLevel);
				PropertyInformation propertyInformation = dcacheClients.ElementInformation.Properties["tracing"];
				if (propertyInformation != null)
				{
					ConfigurationElement configurationElement2 = (ConfigurationElement)propertyInformation.Value;
					if (configurationElement2.ElementInformation.IsPresent)
					{
						this.LogSink.IsConfigEntryPresent = true;
					}
				}
			}
			if (dcacheClient != null)
			{
				this._dcacheClients = new DataCacheNamedClientCollection();
				this._dcacheClients.Add(new DataCacheNamedClient(dcacheClient));
				this.LogSink = new DataCacheLogSink(TraceUtils.GetLogSinkTypeFromTraceSinkType(dcacheClient.TraceSettings.SinkType), dcacheClient.TraceSettings.ClientTraceLevel);
			}
			if (dcacheClient == null && dcacheClients == null)
			{
				this._dcacheClients = new DataCacheNamedClientCollection();
				this._dcacheClients.Add(new DataCacheNamedClient(new DataCacheClientSection()));
			}
		}

		// Token: 0x040003D2 RID: 978
		private DataCacheNamedClientCollection _dcacheClients;

		// Token: 0x040003D3 RID: 979
		private DataCacheLogSink _logSink;
	}
}
