using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000342 RID: 834
	public class ExtensionSettings
	{
		// Token: 0x06001BED RID: 7149 RVA: 0x000711E2 File Offset: 0x0006F3E2
		public ExtensionSettings()
		{
			this.Extension = null;
			this.ParameterValues = null;
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x000711F8 File Offset: 0x0006F3F8
		internal static string ThisToXml(ExtensionSettings settings)
		{
			if (settings == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			ExtensionSettings.WriteToXml(settings, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00071227 File Offset: 0x0006F427
		internal static void WriteToXml(ExtensionSettings settings, XmlTextWriter xml)
		{
			if (settings == null)
			{
				return;
			}
			xml.WriteStartElement("ExtensionSettings");
			xml.WriteElementString("Extension", settings.Extension);
			ParameterValueOrFieldReference.WriteThisArrayToXml(settings.ParameterValues, xml);
			xml.WriteEndElement();
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x0007125C File Offset: 0x0006F45C
		internal static ExtensionSettings XmlToThis(string settings)
		{
			ExtensionSettings extensionSettings = new ExtensionSettings();
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, settings);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ExtensionSettings/Extension");
			if (xmlNode != null)
			{
				extensionSettings.Extension = xmlNode.InnerText;
			}
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("/ExtensionSettings/ParameterValues/ParameterValue");
			extensionSettings.ParameterValues = ParameterValueOrFieldReference.XmlNodesToThisArray(xmlNodeList, false);
			return extensionSettings;
		}

		// Token: 0x04000B5F RID: 2911
		public string Extension;

		// Token: 0x04000B60 RID: 2912
		[XmlArrayItem(typeof(ParameterValue))]
		[XmlArrayItem(typeof(ParameterFieldReference))]
		public ParameterValueOrFieldReference[] ParameterValues;
	}
}
