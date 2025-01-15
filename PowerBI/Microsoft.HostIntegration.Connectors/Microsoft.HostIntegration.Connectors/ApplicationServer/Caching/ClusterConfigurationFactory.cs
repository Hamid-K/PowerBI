using System;
using System.Configuration;
using System.IO;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000DF RID: 223
	internal class ClusterConfigurationFactory
	{
		// Token: 0x0600064F RID: 1615 RVA: 0x000197FC File Offset: 0x000179FC
		public static IClusterConfigurationReader GetReader(ClusterConfigElement cs)
		{
			if (cs.Provider == "xml")
			{
				return ClusterConfigurationFactory.GetConfigXMLReader(cs.ConnectionString);
			}
			if (cs.Provider == "cscfg")
			{
				return new ClusterConfigReader(cs);
			}
			return ClusterConfigurationFactory.GetConfigDictionaryReader(cs);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0001983B File Offset: 0x00017A3B
		public static IClusterConfigurationEditor GetEditor(ClusterConfigElement cs)
		{
			return ClusterConfigurationFactory.GetEditor(cs, 3);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019844 File Offset: 0x00017A44
		public static IClusterConfigurationEditor GetEditor(ClusterConfigElement cs, int retries)
		{
			if (cs.Provider == "xml")
			{
				return ClusterConfigurationFactory.GetConfigXMLEditor(cs.ConnectionString, retries);
			}
			if (cs.Provider == "cscfg")
			{
				throw new NotImplementedException("Editors for CSCFG provider not implemented yet");
			}
			return ClusterConfigurationFactory.GetConfigDictionaryEditor(cs);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00019893 File Offset: 0x00017A93
		public static IClusterConfigurationReader GetConfigXMLReader(string xmlFilePath)
		{
			return new GlobalConfigReader(xmlFilePath);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001989B File Offset: 0x00017A9B
		public static IClusterConfigurationReader GetConfigDictionaryReader(ClusterConfigElement cs)
		{
			return new ClusterConfigDictionaryReader(cs);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000198A3 File Offset: 0x00017AA3
		public static IClusterConfigurationEditor GetConfigXMLEditor(string xmlFilePath, int retries)
		{
			return new GlobalConfigReaderWriter(xmlFilePath, retries);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000198AC File Offset: 0x00017AAC
		public static IClusterConfigurationEditor GetConfigDictionaryEditor(ClusterConfigElement cs)
		{
			return new ClusterConfigDictionaryReaderWriter(cs);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x000198B4 File Offset: 0x00017AB4
		public static bool TryExportToXml(IClusterConfigurationReader reader, out string xml)
		{
			ConfigChange configChange = default(ConfigChange);
			configChange.ChangeAll(true);
			return ClusterConfigurationFactory.TryExportToXml(reader, out xml, configChange);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x000198DC File Offset: 0x00017ADC
		public static bool TryExportToXml(IClusterConfigurationReader reader, out string xml, ConfigChange change)
		{
			xml = ClusterConfigurationFactory.CreateEmptyXmlConfiguration();
			IClusterConfigurationEditor configXMLEditor = ClusterConfigurationFactory.GetConfigXMLEditor(xml, 3);
			return ClusterConfigurationFactory.TransferConfigurations(reader, configXMLEditor, change);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00019904 File Offset: 0x00017B04
		public static bool TryImportFromXml(string xmlConfigFilePath, IClusterConfigurationEditor editor, ConfigChange change)
		{
			IClusterConfigurationReader configXMLReader = ClusterConfigurationFactory.GetConfigXMLReader(xmlConfigFilePath);
			return ClusterConfigurationFactory.TransferConfigurations(configXMLReader, editor, change);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00019920 File Offset: 0x00017B20
		private static string CreateEmptyXmlConfiguration()
		{
			string text = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			ExeConfigurationFileMap exeConfigurationFileMap = new ExeConfigurationFileMap();
			exeConfigurationFileMap.ExeConfigFilename = text;
			text = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(exeConfigurationFileMap, ConfigurationUserLevel.None);
			configuration.SaveAs(text, ConfigurationSaveMode.Minimal);
			return text;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00019988 File Offset: 0x00017B88
		public static ConfigChange GetChange(IClusterConfigurationReader reader1, IClusterConfigurationReader reader2)
		{
			ConfigChange configChange = default(ConfigChange);
			AdvancedPropertiesElement advancedProperties = reader1.AdvancedProperties;
			AdvancedPropertiesElement advancedProperties2 = reader2.AdvancedProperties;
			configChange.ChangeAdvanceProperties = advancedProperties.ComputeDifferences(advancedProperties2);
			return configChange;
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000199BC File Offset: 0x00017BBC
		private static bool TransferConfigurations(IClusterConfigurationReader reader, IClusterConfigurationEditor editor, ConfigChange change)
		{
			bool flag = true;
			if (change.ChangeCluster)
			{
				editor.EditClusterSize(reader.GetClusterSize());
			}
			if ((change.ChangeCluster || change.ChangeHost) && reader.DomainLayout != null)
			{
				foreach (IDomainLayoutConfiguration domainLayoutConfiguration in reader.DomainLayout)
				{
					editor.EditDomainConfig(domainLayoutConfiguration);
				}
			}
			if (change.ChangeHost)
			{
				foreach (IHostConfiguration hostConfiguration in editor.GetListOfHosts())
				{
					flag = flag && editor.TryDeleteHost(hostConfiguration.Name, hostConfiguration.ServiceName);
				}
				foreach (IHostConfiguration hostConfiguration2 in reader.GetListOfHosts())
				{
					flag = flag && editor.TryAddHost(hostConfiguration2);
				}
			}
			if (change.ChangeCache.MaxNamedCacheCountChange)
			{
				editor.EditMaxNamedCacheCount(reader.MaxNamedCacheCount);
			}
			if (change.ChangeCache.BasePartitionCountChange)
			{
				editor.EditBasePartitionCount(reader.BasePartitionCount);
			}
			if (change.ChangeCache.CachePropertiesChanged)
			{
				foreach (INamedCacheConfiguration namedCacheConfiguration in editor.GetListOfNamedCaches())
				{
					flag = flag && editor.TryDeleteNamedCache(namedCacheConfiguration.Name);
				}
				foreach (INamedCacheConfiguration namedCacheConfiguration2 in reader.GetListOfNamedCaches())
				{
					flag = flag && editor.TryAddNamedCache(namedCacheConfiguration2);
				}
			}
			if (change.ChangeAdvanceProperties.Changed)
			{
				editor.EditAdvancedProperties(reader.AdvancedProperties, change.ChangeAdvanceProperties);
			}
			if (change.ChangeDeploymentSettings.Changed)
			{
				editor.EditDeploymentSettings(reader.GetDeploymentSettings(), change.ChangeDeploymentSettings);
			}
			return flag;
		}
	}
}
