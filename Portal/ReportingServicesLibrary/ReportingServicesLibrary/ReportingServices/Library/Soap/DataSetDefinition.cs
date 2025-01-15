using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000344 RID: 836
	public class DataSetDefinition
	{
		// Token: 0x06001BF2 RID: 7154 RVA: 0x00071304 File Offset: 0x0006F504
		public DataSetDefinition()
		{
			this.Fields = null;
			this.Query = null;
			this.CaseSensitivity = SensitivityEnum.Auto;
			this.CaseSensitivitySpecified = false;
			this.Collation = null;
			this.AccentSensitivity = SensitivityEnum.Auto;
			this.AccentSensitivitySpecified = false;
			this.KanatypeSensitivity = SensitivityEnum.Auto;
			this.KanatypeSensitivitySpecified = false;
			this.WidthSensitivity = SensitivityEnum.Auto;
			this.WidthSensitivitySpecified = false;
			this.Name = null;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0007136C File Offset: 0x0006F56C
		internal static string ThisToXml(DataSetDefinition dataSet)
		{
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			DataSetDefinition.WriteToXml(dataSet, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00071398 File Offset: 0x0006F598
		internal static void WriteToXml(DataSetDefinition dataSet, XmlTextWriter xml)
		{
			if (dataSet == null)
			{
				return;
			}
			xml.WriteStartElement("DataSet");
			Field.WriteArrayToXml(dataSet.Fields, xml);
			QueryDefinition.WriteToXml(dataSet.Query, xml);
			if (dataSet.CaseSensitivitySpecified)
			{
				xml.WriteElementString("CaseSensitivity", dataSet.CaseSensitivity.ToString());
			}
			if (dataSet.Collation != null)
			{
				xml.WriteElementString("Collation", dataSet.Collation);
			}
			if (dataSet.AccentSensitivitySpecified)
			{
				xml.WriteElementString("AccentSensitivity", dataSet.AccentSensitivity.ToString());
			}
			if (dataSet.KanatypeSensitivitySpecified)
			{
				xml.WriteElementString("KanatypeSensitivity", dataSet.KanatypeSensitivity.ToString());
			}
			if (dataSet.WidthSensitivitySpecified)
			{
				xml.WriteElementString("WidthSensitivity", dataSet.WidthSensitivity.ToString());
			}
			if (dataSet.Name != null)
			{
				xml.WriteElementString("Name", dataSet.Name);
			}
			xml.WriteEndElement();
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00071494 File Offset: 0x0006F694
		internal static DataSetDefinition XmlToThis(string xml)
		{
			if (xml == null)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlUtil.SafeOpenXmlDocumentString(xmlDocument, xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/DataSet");
			if (xmlNode == null)
			{
				return null;
			}
			DataSetDefinition dataSetDefinition = new DataSetDefinition();
			XmlNodeList xmlNodeList = xmlNode.SelectNodes("Fields/Field");
			dataSetDefinition.Fields = Field.XmlNodesToThisArray(xmlNodeList);
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("Query");
			dataSetDefinition.Query = QueryDefinition.XmlNodeToThis(xmlNode2);
			XmlNode xmlNode3 = xmlNode.SelectSingleNode("CaseSensitivity");
			if (xmlNode3 != null)
			{
				dataSetDefinition.CaseSensitivity = (SensitivityEnum)Enum.Parse(typeof(SensitivityEnum), xmlNode3.InnerText);
				dataSetDefinition.CaseSensitivitySpecified = true;
			}
			XmlNode xmlNode4 = xmlNode.SelectSingleNode("Collation");
			if (xmlNode4 != null)
			{
				dataSetDefinition.Collation = xmlNode4.InnerText;
			}
			XmlNode xmlNode5 = xmlNode.SelectSingleNode("AccentSensitivity");
			if (xmlNode5 != null)
			{
				dataSetDefinition.AccentSensitivity = (SensitivityEnum)Enum.Parse(typeof(SensitivityEnum), xmlNode5.InnerText);
				dataSetDefinition.AccentSensitivitySpecified = true;
			}
			XmlNode xmlNode6 = xmlNode.SelectSingleNode("KanatypeSensitivity");
			if (xmlNode6 != null)
			{
				dataSetDefinition.KanatypeSensitivity = (SensitivityEnum)Enum.Parse(typeof(SensitivityEnum), xmlNode6.InnerText);
				dataSetDefinition.KanatypeSensitivitySpecified = true;
			}
			XmlNode xmlNode7 = xmlNode.SelectSingleNode("WidthSensitivity");
			if (xmlNode7 != null)
			{
				dataSetDefinition.WidthSensitivity = (SensitivityEnum)Enum.Parse(typeof(SensitivityEnum), xmlNode7.InnerText);
				dataSetDefinition.WidthSensitivitySpecified = true;
			}
			XmlNode xmlNode8 = xmlNode.SelectSingleNode("Name");
			if (xmlNode8 != null)
			{
				dataSetDefinition.Name = xmlNode8.InnerText;
			}
			return dataSetDefinition;
		}

		// Token: 0x04000B6B RID: 2923
		public Field[] Fields;

		// Token: 0x04000B6C RID: 2924
		public QueryDefinition Query;

		// Token: 0x04000B6D RID: 2925
		public SensitivityEnum CaseSensitivity;

		// Token: 0x04000B6E RID: 2926
		[XmlIgnore]
		public bool CaseSensitivitySpecified;

		// Token: 0x04000B6F RID: 2927
		public string Collation;

		// Token: 0x04000B70 RID: 2928
		public SensitivityEnum AccentSensitivity;

		// Token: 0x04000B71 RID: 2929
		[XmlIgnore]
		public bool AccentSensitivitySpecified;

		// Token: 0x04000B72 RID: 2930
		public SensitivityEnum KanatypeSensitivity;

		// Token: 0x04000B73 RID: 2931
		[XmlIgnore]
		public bool KanatypeSensitivitySpecified;

		// Token: 0x04000B74 RID: 2932
		public SensitivityEnum WidthSensitivity;

		// Token: 0x04000B75 RID: 2933
		[XmlIgnore]
		public bool WidthSensitivitySpecified;

		// Token: 0x04000B76 RID: 2934
		public string Name;
	}
}
