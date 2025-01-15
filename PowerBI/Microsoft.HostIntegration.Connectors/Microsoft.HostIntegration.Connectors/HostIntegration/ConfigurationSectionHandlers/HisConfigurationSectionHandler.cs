using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Xml;
using Microsoft.ApplicationServer.Caching;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers
{
	// Token: 0x0200050C RID: 1292
	public class HisConfigurationSectionHandler : ConfigurationSection
	{
		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00096404 File Offset: 0x00094604
		public static bool IsCachingAvailable
		{
			get
			{
				bool flag;
				try
				{
					Assembly assembly = Assembly.Load("Microsoft.ApplicationServer.Caching.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
					Assembly assembly2 = Assembly.Load("Microsoft.ApplicationServer.Caching.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
					if (assembly == null || assembly2 == null)
					{
						flag = false;
					}
					else
					{
						flag = true;
					}
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x00096458 File Offset: 0x00094658
		public static object LoadFromCache(string cacheName, string fileName, string region, string sectionName)
		{
			try
			{
				string text = (string)new DataCacheFactory().GetCache(cacheName).Get(fileName, region);
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
				xmlReaderSettings.XmlResolver = null;
				XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.Load(xmlReader);
				string tempFileName = Path.GetTempFileName();
				File.WriteAllText(tempFileName, "");
				XmlWriter xmlWriter = XmlWriter.Create(tempFileName, new XmlWriterSettings
				{
					Indent = true,
					NewLineOnAttributes = true
				});
				xmlDocument.WriteContentTo(xmlWriter);
				xmlWriter.Close();
				return ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
				{
					ExeConfigFilename = tempFileName
				}, ConfigurationUserLevel.None).Sections[sectionName];
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x04001B7C RID: 7036
		private const string AFCacheClientAssemblyName = "Microsoft.ApplicationServer.Caching.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";

		// Token: 0x04001B7D RID: 7037
		private const string AFCacheCoreAssemblyName = "Microsoft.ApplicationServer.Caching.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
	}
}
