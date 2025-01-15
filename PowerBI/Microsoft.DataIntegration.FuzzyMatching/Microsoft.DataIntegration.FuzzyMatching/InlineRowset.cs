using System;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000010 RID: 16
	[Serializable]
	public class InlineRowset : RowsetDefinition, IXmlSerializable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EAC File Offset: 0x000010AC
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002EB4 File Offset: 0x000010B4
		public string RidColumnType { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002EBD File Offset: 0x000010BD
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002EC8 File Offset: 0x000010C8
		public DataTable DataTable
		{
			get
			{
				return this.m_rows;
			}
			set
			{
				this.m_rows = value;
				if (this.RidColumnName != null && this.DataTable != null && this.DataTable.Columns.Contains(this.RidColumnName))
				{
					this.DataTable.Constraints.Add("keyConstraint", this.DataTable.Columns[this.RidColumnName], true);
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F31 File Offset: 0x00001131
		public InlineRowset()
		{
			this.DataTable = new DataTable();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002F44 File Offset: 0x00001144
		public InlineRowset(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002F54 File Offset: 0x00001154
		public InlineRowset(DataTable schemaTable)
		{
			this.DataTable = new DataTable();
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				Type type;
				if (dataRow[SchemaTableColumn.DataType] is Type)
				{
					type = dataRow[SchemaTableColumn.DataType] as Type;
				}
				else
				{
					type = Type.GetType(dataRow[SchemaTableColumn.DataType].ToString());
				}
				this.DataTable.Columns.Add((string)dataRow[SchemaTableColumn.ColumnName], type);
			}
		}

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003014 File Offset: 0x00001214
		public override string RidColumnName
		{
			set
			{
				this.m_ridColumnName = value;
				if (this.DataTable != null && this.RidColumnName != null && this.DataTable.Columns.Contains(this.RidColumnName))
				{
					this.DataTable.Constraints.Add("keyConstraint", this.DataTable.Columns[this.RidColumnName], true);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000307D File Offset: 0x0000127D
		public override IDataReader CreateDataReader(ConnectionManager connectionManager)
		{
			return this.DataTable.CreateDataReader();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000308A File Offset: 0x0000128A
		public override DataTable GetSchemaTable(ConnectionManager connectionManager)
		{
			return this.DataTable.CreateDataReader().GetSchemaTable();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000309C File Offset: 0x0000129C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000030A0 File Offset: 0x000012A0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("InlineRowset"))
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
				if (reader.MoveToAttribute("ridColumnType"))
				{
					this.RidColumnType = reader.Value;
				}
				reader.MoveToElement();
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(reader);
				string text = ((base.Name != null) ? base.Name : "InlineRowset");
				XmlNode xmlNode = xmlDocument.CreateNode(1, text, null);
				while (xmlDocument.FirstChild.ChildNodes.Count > 0)
				{
					xmlNode.AppendChild(xmlDocument.FirstChild.ChildNodes.get_ItemOf(0));
				}
				XmlNodeReader xmlNodeReader = new XmlNodeReader(xmlNode);
				DataSet dataSet = new DataSet();
				if (!string.IsNullOrEmpty(this.RidColumnType))
				{
					dataSet.Tables.Add(new DataTable("Row"));
					Type type = Type.GetType(this.RidColumnType);
					if (type == typeof(string))
					{
						dataSet.Tables[0].Columns.Add(new DataColumn
						{
							ColumnName = this.RidColumnName,
							DataType = type,
							MaxLength = 4000
						});
					}
					else
					{
						dataSet.Tables[0].Columns.Add(new DataColumn
						{
							ColumnName = this.RidColumnName,
							DataType = type
						});
					}
					dataSet.ReadXml(xmlNodeReader, 3);
				}
				else
				{
					dataSet.ReadXml(xmlNodeReader, 6);
				}
				this.DataTable = dataSet.Tables[0];
				this.DataTable.TableName = text;
				this.UpdateColumnLengths(this.DataTable);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003288 File Offset: 0x00001488
		private void UpdateColumnLengths(DataTable table)
		{
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.DataType == typeof(string) && dataColumn.MaxLength == -1)
				{
					dataColumn.MaxLength = 4000;
				}
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003300 File Offset: 0x00001500
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("InlineRowset");
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
			if (!string.IsNullOrEmpty(this.RidColumnType))
			{
				writer.WriteAttributeString("ridColumnType", this.RidColumnType);
			}
			throw new NotImplementedException();
		}

		// Token: 0x04000022 RID: 34
		protected DataTable m_rows;

		// Token: 0x0200011E RID: 286
		private class MyResolver : XmlResolver
		{
			// Token: 0x06000BC4 RID: 3012 RVA: 0x00033873 File Offset: 0x00031A73
			[PermissionSet(7, Name = "FullTrust")]
			public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000BC5 RID: 3013 RVA: 0x0003387A File Offset: 0x00031A7A
			[PermissionSet(7, Name = "FullTrust")]
			public override Uri ResolveUri(Uri baseUri, string relativeUri)
			{
				return base.ResolveUri(baseUri, relativeUri);
			}

			// Token: 0x17000246 RID: 582
			// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x00033884 File Offset: 0x00031A84
			public override ICredentials Credentials
			{
				set
				{
					throw new NotImplementedException();
				}
			}
		}
	}
}
