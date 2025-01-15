using System;
using System.Configuration;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C4 RID: 452
	internal static class MemcacheShimUtility
	{
		// Token: 0x06000EE2 RID: 3810 RVA: 0x00032644 File Offset: 0x00030844
		internal static void ProcessConfiguration(DataCacheClientsSection dataCacheClients, ref DataCacheNamedClientCollection namedCacheClients, ref DataCacheLogSink logSink)
		{
			if (dataCacheClients == null)
			{
				throw new DataCacheException("CONFIGURATION_MANAGER", 8003, "DataCacheClientsSection is missing");
			}
			namedCacheClients = new DataCacheNamedClientCollection();
			foreach (object obj in dataCacheClients.Clients)
			{
				ConfigurationElement configurationElement = (ConfigurationElement)obj;
				namedCacheClients.Add((DataCacheNamedClient)configurationElement);
			}
			logSink = new DataCacheLogSink(TraceUtils.GetLogSinkTypeFromTraceSinkType(dataCacheClients.TraceSettings.SinkType), dataCacheClients.TraceSettings.ClientTraceLevel);
			PropertyInformation propertyInformation = dataCacheClients.ElementInformation.Properties["tracing"];
			if (propertyInformation != null)
			{
				ConfigurationElement configurationElement2 = (ConfigurationElement)propertyInformation.Value;
				if (configurationElement2.ElementInformation.IsPresent)
				{
					logSink.IsConfigEntryPresent = true;
				}
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00032724 File Offset: 0x00030924
		internal static void ThrowMissingNamedConfigException(string cacheName)
		{
			string text = string.Format(CultureInfo.CurrentCulture, "dataCacheClient config with name {0} is missing. No default config ('{1}') specified", new object[] { cacheName, "DefaultShimConfig" });
			throw new DataCacheException("CONFIGURATION_MANAGER", 8003, text);
		}
	}
}
