using System;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000014 RID: 20
	[Serializable]
	public class CsvFileRowset : RowsetDefinition, IXmlSerializable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003AD4 File Offset: 0x00001CD4
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003ADC File Offset: 0x00001CDC
		public string FileName { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003AE5 File Offset: 0x00001CE5
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003AED File Offset: 0x00001CED
		public string ColumnNames { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003AF6 File Offset: 0x00001CF6
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003AFE File Offset: 0x00001CFE
		private string SelectedColumnNames { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003B07 File Offset: 0x00001D07
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003B0F File Offset: 0x00001D0F
		public char Delimiter { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003B18 File Offset: 0x00001D18
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003B20 File Offset: 0x00001D20
		public bool FirstRowContainsHeader { get; set; }

		// Token: 0x0600008C RID: 140 RVA: 0x00003B29 File Offset: 0x00001D29
		public CsvFileRowset()
		{
			this.Delimiter = ',';
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003B39 File Offset: 0x00001D39
		public CsvFileRowset(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003B48 File Offset: 0x00001D48
		public override IDataReader CreateDataReader(ConnectionManager connectionManager)
		{
			return new CsvFileDataReader(this.FileName, this.GetSchemaTable(null), this.Delimiter, this.FirstRowContainsHeader);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003B68 File Offset: 0x00001D68
		public override DataTable GetSchemaTable(ConnectionManager connectionManager)
		{
			DataTable dataTable = SchemaUtils.CreateSchemaTable(this.FileName);
			if (!string.IsNullOrEmpty(this.ColumnNames))
			{
				int num = 0;
				foreach (string text in this.ColumnNames.Split(new char[] { ',' }))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow[SchemaTableColumn.ColumnName] = text;
					dataRow[SchemaTableColumn.ColumnOrdinal] = num++;
					dataRow[SchemaTableColumn.DataType] = typeof(string);
					dataTable.Rows.Add(dataRow);
				}
			}
			else if (this.FirstRowContainsHeader)
			{
				dataTable = CsvFileDataReader.LoadSchemaTable(this.FileName, this.Delimiter);
			}
			return dataTable;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C26 File Offset: 0x00001E26
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003C2C File Offset: 0x00001E2C
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("CsvFileRowset"))
			{
				if (reader.MoveToAttribute("name"))
				{
					base.Name = reader.Value;
				}
				if (reader.MoveToAttribute("description"))
				{
					base.Description = reader.Value;
				}
				if (reader.MoveToAttribute("ridColumnName"))
				{
					this.RidColumnName = reader.Value;
				}
				if (reader.MoveToAttribute("fileName"))
				{
					this.FileName = reader.Value;
				}
				if (reader.MoveToAttribute("columnNames"))
				{
					this.ColumnNames = reader.Value;
				}
				if (reader.MoveToAttribute("delimiter"))
				{
					if (reader.Value.Length != 1)
					{
						throw new Exception("Exactly one delimiter character may be specified.");
					}
					this.Delimiter = reader.Value.get_Chars(0);
				}
				if (reader.MoveToAttribute("firstRowContainsHeader"))
				{
					this.FirstRowContainsHeader = bool.Parse(reader.Value);
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003D24 File Offset: 0x00001F24
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("CsvFileRowset");
			if (!string.IsNullOrEmpty(base.Name))
			{
				writer.WriteAttributeString("name", base.Name);
			}
			if (!string.IsNullOrEmpty(base.Description))
			{
				writer.WriteAttributeString("description", base.Description);
			}
			if (!string.IsNullOrEmpty(this.RidColumnName))
			{
				writer.WriteAttributeString("ridColumnName", this.RidColumnName);
			}
			if (!string.IsNullOrEmpty(this.FileName))
			{
				writer.WriteAttributeString("fileName", this.FileName);
			}
			if (!string.IsNullOrEmpty(this.ColumnNames))
			{
				writer.WriteAttributeString("columnNames", this.ColumnNames);
			}
			writer.WriteAttributeString("delimiter", this.Delimiter.ToString());
			writer.WriteEndElement();
		}
	}
}
