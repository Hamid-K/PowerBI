using System;
using System.IO;
using System.Xml;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding
{
	// Token: 0x0200052B RID: 1323
	public class EncodingConfigfurationUtilities
	{
		// Token: 0x06002CBD RID: 11453 RVA: 0x00097AAC File Offset: 0x00095CAC
		public void WriteConfigurationToXmlFile(ref EncodingConfigurationSectionHandler encodingConfig, string fileName)
		{
			try
			{
				string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?><configuration><configSections><section name=\"hostIntegration.encoding\" type=\"Microsoft.HostIntegration.ConfigurationSectionHandlers.Encoding.ConfigurationSectionHandler, Microsoft.HostIntegration.ConfigurationSectionHandlers, Version=10.0.1000.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\" /></configSections><hostIntegration.encoding xmlns=\"http://schemas.microsoft.com/his/Encoding/2013\">";
				text += "<codePages>";
				foreach (object obj in encodingConfig.CodePages)
				{
					CodePage codePage = (CodePage)obj;
					text = string.Concat(new string[]
					{
						text,
						"<codePage name=\"",
						codePage.Name,
						"\" number=\"",
						codePage.Number.ToString(),
						"\" description=\"",
						codePage.Description,
						"\" nlsCodePage=\"",
						codePage.NlsCodePage.ToString(),
						"\" >"
					});
					if (codePage.UnicodeToEbcdicConversions.Count > 0)
					{
						text += "<unicodeToEbcdicConversions>";
						foreach (object obj2 in codePage.UnicodeToEbcdicConversions)
						{
							UnicodeToEbcdicConversion unicodeToEbcdicConversion = (UnicodeToEbcdicConversion)obj2;
							text = string.Concat(new string[]
							{
								text,
								"<unicodeToEbcdicConversion to=\"",
								unicodeToEbcdicConversion.To,
								"\" from=\"",
								unicodeToEbcdicConversion.From,
								"\" reversible=\"",
								unicodeToEbcdicConversion.Reversible.ToString().ToLowerInvariant(),
								"\" />"
							});
						}
						text += "</unicodeToEbcdicConversions>";
					}
					if (codePage.EbcdicToUnicodeConversions.Count > 0)
					{
						text += "<ebcdicToUnicodeConversions>";
						foreach (object obj3 in codePage.UnicodeToEbcdicConversions)
						{
							UnicodeToEbcdicConversion unicodeToEbcdicConversion2 = (UnicodeToEbcdicConversion)obj3;
							text = string.Concat(new string[]
							{
								text,
								"<ebcdicToUnicodeConversion to=\"",
								unicodeToEbcdicConversion2.To,
								"\" from=\"",
								unicodeToEbcdicConversion2.From,
								"\" reversible=\"",
								unicodeToEbcdicConversion2.Reversible.ToString().ToLowerInvariant(),
								"\" />"
							});
						}
						text += "</ebcdicToUnicodeConversions>";
					}
					text += "</codePage>";
				}
				text += "</codePages></hostIntegration.encoding></configuration>";
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
				xmlReaderSettings.XmlResolver = null;
				XmlReader xmlReader = XmlReader.Create(new StringReader(text), xmlReaderSettings);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				xmlDocument.Load(xmlReader);
				xmlDocument.Save(fileName);
				new ValidateConfigurationFile().ValidateConfigFile(fileName, "HostIntegrationEncodingConfiguration.xsd");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
