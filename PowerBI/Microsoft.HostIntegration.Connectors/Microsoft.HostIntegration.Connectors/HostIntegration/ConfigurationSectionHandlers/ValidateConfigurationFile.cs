using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers
{
	// Token: 0x0200050F RID: 1295
	public class ValidateConfigurationFile
	{
		// Token: 0x06002BD6 RID: 11222 RVA: 0x00096B50 File Offset: 0x00094D50
		public void ValidateConfigFile(string appConfigFile, string schemaName)
		{
			this.ValidateConfigFile(appConfigFile, null, schemaName);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00096B5B File Offset: 0x00094D5B
		public void ValidateConfigFile(StreamReader appConfigStream, string schemaName)
		{
			this.ValidateConfigFile(null, appConfigStream, schemaName);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x00096B68 File Offset: 0x00094D68
		private void ValidateConfigFile(string appConfigFile, StreamReader appConfigStream, string schemaName)
		{
			string text = null;
			RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
			string text2 = null;
			try
			{
				RegistryKey registryKey2 = registryKey.OpenSubKey("Software\\Microsoft\\VisualStudio");
				if (registryKey2 != null)
				{
					for (int i = 10; i <= 12; i++)
					{
						string text3 = string.Format("{0}.0", i);
						RegistryKey registryKey3 = registryKey2.OpenSubKey(text3);
						if (registryKey3 != null)
						{
							text2 = (string)registryKey3.GetValue("ShellFolder");
							registryKey3.Close();
							if (!string.IsNullOrEmpty(text2))
							{
								text = ((i == 10) ? "DotNetConfig.xsd" : "DotNetConfig40.xsd");
								break;
							}
						}
					}
					registryKey2.Close();
				}
				if (!string.IsNullOrEmpty(text2))
				{
					if (!text2.EndsWith("\\"))
					{
						text2 += "\\";
					}
					if (File.Exists(text2 + "XML\\Schemas\\" + text))
					{
						XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
						string text4 = this.GetHisSchemaDirectory() + "System\\Schemas\\" + schemaName;
						if (!File.Exists(text4))
						{
							throw new Exception("Config file validation did not occur because schema \"" + schemaName + "\" was not found.");
						}
						XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
						xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
						xmlReaderSettings.XmlResolver = null;
						XmlReader xmlReader = XmlReader.Create(text4, xmlReaderSettings);
						xmlSchemaSet.Add(XmlSchema.Read(xmlReader, new ValidationEventHandler(this.ValidationHandler)));
						xmlReader = XmlReader.Create(text2 + "XML\\Schemas\\" + text, xmlReaderSettings);
						xmlSchemaSet.Add(XmlSchema.Read(xmlReader, new ValidationEventHandler(this.ValidationHandler)));
						xmlSchemaSet.Compile();
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.XmlResolver = null;
						if (string.IsNullOrEmpty(appConfigFile) && appConfigStream == null)
						{
							throw new Exception("Both file name and stream are null");
						}
						if (!string.IsNullOrEmpty(appConfigFile))
						{
							xmlReader = XmlReader.Create(appConfigFile, xmlReaderSettings);
						}
						else
						{
							xmlReader = XmlReader.Create(appConfigStream, xmlReaderSettings);
						}
						xmlDocument.Load(xmlReader);
						xmlDocument.Schemas = xmlSchemaSet;
						xmlDocument.Validate(new ValidationEventHandler(this.ValidationHandler));
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Configuration file validation failed: " + ex.Message);
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x00096D88 File Offset: 0x00094F88
		private void ValidationHandler(object sender, ValidationEventArgs args)
		{
			throw new Exception(args.Message);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x00096D98 File Offset: 0x00094F98
		private string GetHisSchemaDirectory()
		{
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Host Integration Server", "InstallPath", null);
			if (text == null)
			{
				throw new Exception("Config file validation did not occur because the Host Integration Server 10 installation path could not be found.");
			}
			if (!text.EndsWith("\\"))
			{
				text += "\\";
			}
			return text;
		}
	}
}
