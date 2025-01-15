using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public class DomainBinding : IXmlSerializable
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00011A38 File Offset: 0x0000FC38
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00011A40 File Offset: 0x0000FC40
		public string DomainName { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00011A49 File Offset: 0x0000FC49
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00011A51 File Offset: 0x0000FC51
		public List<Column> Columns { get; set; }

		// Token: 0x060003AC RID: 940 RVA: 0x00011A5A File Offset: 0x0000FC5A
		public DomainBinding()
		{
			this.Columns = new List<Column>();
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011A70 File Offset: 0x0000FC70
		public void Validate(DataTable schemaTable)
		{
			foreach (Column column in this.Columns)
			{
				if (!string.IsNullOrEmpty(column.Name))
				{
					DataRow dataRow = SchemaUtils.FindColumnSchemaRow(schemaTable, column.Name, false);
					if (dataRow == null)
					{
						throw new Exception(string.Format(StringResources.ColumnNotFoundInSchemaTable, column.Name));
					}
					if (column.Ordinal >= 0 && (int)dataRow[SchemaTableColumn.ColumnOrdinal] != column.Ordinal)
					{
						throw new Exception(string.Format("Column ordinal specified for column {0} does not match the record schema.", column.Name));
					}
				}
				if (column.Ordinal >= 0 && schemaTable != null)
				{
					DataRow dataRow2 = SchemaUtils.FindColumnSchemaRow(schemaTable, column.Ordinal);
					if (dataRow2 == null)
					{
						throw new Exception(string.Format("Column ordinal specified for domain binding domain {0} was not found in the record schema.", this.DomainName));
					}
					if (!string.IsNullOrEmpty(column.Name) && !column.Name.Equals(dataRow2[SchemaTableColumn.ColumnName] as string))
					{
						throw new Exception(string.Format("Column ordinal specified for column {0} does not match the record schema.", column.Name));
					}
				}
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011BA0 File Offset: 0x0000FDA0
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:DomainBinding", xmlNamespaceManager);
			if (xmlNode != null)
			{
				XmlNode namedItem;
				if ((namedItem = xmlNode.Attributes.GetNamedItem("domainName")) != null)
				{
					this.DomainName = namedItem.Value;
				}
				foreach (object obj in xmlNode.SelectNodes("//ns:Column", xmlNamespaceManager))
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					Column column = new Column();
					column.ReadXml(new XmlNodeReader(xmlNode2));
					this.Columns.Add(column);
				}
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011C68 File Offset: 0x0000FE68
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("DomainBinding");
			writer.WriteAttributeString("domainName", this.DomainName);
			foreach (Column column in this.Columns)
			{
				column.WriteXml(writer);
			}
			writer.WriteEndElement();
		}
	}
}
