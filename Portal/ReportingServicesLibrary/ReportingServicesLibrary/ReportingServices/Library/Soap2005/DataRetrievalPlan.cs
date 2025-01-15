using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000316 RID: 790
	public class DataRetrievalPlan
	{
		// Token: 0x06001B37 RID: 6967 RVA: 0x0006EA54 File Offset: 0x0006CC54
		public DataRetrievalPlan()
		{
			this.Item = null;
			this.DataSet = null;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0006EA6C File Offset: 0x0006CC6C
		internal static string ThisToXml(DataRetrievalPlan dataSettings)
		{
			if (dataSettings == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			DataRetrievalPlan.WriteToXml(dataSettings, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0006EA9B File Offset: 0x0006CC9B
		internal static void WriteToXml(DataRetrievalPlan dataSettings, XmlTextWriter xml)
		{
			if (dataSettings == null)
			{
				return;
			}
			xml.WriteStartElement("DataSettings");
			DataSource.WriteToXml(new DataSource
			{
				Item = dataSettings.Item
			}, xml);
			Microsoft.ReportingServices.Library.Soap.DataSetDefinition.WriteToXml(dataSettings.DataSet, xml);
			xml.WriteEndElement();
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x0006EAD8 File Offset: 0x0006CCD8
		internal static DataRetrievalPlan XmlToThis(string xml)
		{
			if (xml == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, xml);
			DataRetrievalPlan dataRetrievalPlan = new DataRetrievalPlan();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/DataSettings/DataSource");
			if (xmlNode != null)
			{
				DataSourceInfo dataSourceInfo = DataSourceInfo.ParseDataSourceNode(xmlNode, false, true, DataProtection.Instance);
				dataRetrievalPlan.Item = DataSource.DataSourceInfoToThis(dataSourceInfo, true, false).Item;
			}
			XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/DataSettings/DataSet");
			if (xmlNode2 != null)
			{
				dataRetrievalPlan.DataSet = Microsoft.ReportingServices.Library.Soap.DataSetDefinition.XmlToThis(xmlNode2.OuterXml);
			}
			return dataRetrievalPlan;
		}

		// Token: 0x04000AA0 RID: 2720
		[XmlElement(typeof(DataSourceDefinition))]
		[XmlElement(typeof(DataSourceReference))]
		[XmlElement(typeof(InvalidDataSourceReference))]
		public DataSourceDefinitionOrReference Item;

		// Token: 0x04000AA1 RID: 2721
		public Microsoft.ReportingServices.Library.Soap.DataSetDefinition DataSet;
	}
}
