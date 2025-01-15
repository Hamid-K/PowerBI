using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200035C RID: 860
	internal sealed class DataSourceDefinition2Collection : Dictionary<string, DataSourceReferenceOrDefinition>, IXmlSerializable
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x000730FC File Offset: 0x000712FC
		public DataSourceDefinition2Collection(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00073105 File Offset: 0x00071305
		public DataSourceDefinition2Collection()
		{
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x00073110 File Offset: 0x00071310
		internal static DataSourceDefinition2Collection ReadFromString(string dataSourceDefinition)
		{
			DataSourceDefinition2Collection dataSourceDefinition2Collection = new DataSourceDefinition2Collection();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.ProhibitDtd = true;
			xmlReaderSettings.XmlResolver = null;
			using (StringReader stringReader = new StringReader(dataSourceDefinition))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					xmlReader.Read();
					dataSourceDefinition2Collection.ReadXml(xmlReader);
				}
			}
			return dataSourceDefinition2Collection;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0000289C File Offset: 0x00000A9C
		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x00073190 File Offset: 0x00071390
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (!reader.IsEmptyElement && reader.NodeType == XmlNodeType.Element && reader.LocalName == "DataSource")
				{
					reader.Read();
					string text = reader.ReadElementContentAsString();
					DataSourceReferenceOrDefinition dataSourceReferenceOrDefinition = new DataSourceReferenceOrDefinition();
					dataSourceReferenceOrDefinition.ReadXml(reader);
					base.Add(text, dataSourceReferenceOrDefinition);
				}
			}
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000731F0 File Offset: 0x000713F0
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("ArrayOfDataSource", "http://schemas.microsoft.com/sqlserver/reporting/2012/01/datasource");
			foreach (KeyValuePair<string, DataSourceReferenceOrDefinition> keyValuePair in this)
			{
				writer.WriteStartElement("DataSource");
				writer.WriteElementString("Name", keyValuePair.Key);
				keyValuePair.Value.WriteXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BC8 RID: 3016
		internal const string NAMESPACE2012 = "http://schemas.microsoft.com/sqlserver/reporting/2012/01/datasource";

		// Token: 0x04000BC9 RID: 3017
		internal const string DATASOURCES = "ArrayOfDataSource";

		// Token: 0x04000BCA RID: 3018
		internal const string DATASOURCE = "DataSource";

		// Token: 0x04000BCB RID: 3019
		internal const string NAME = "Name";
	}
}
