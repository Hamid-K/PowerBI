using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class SqlRowsetDefinition : RowsetDefinition, IXmlSerializable
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003395 File Offset: 0x00001595
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000339D File Offset: 0x0000159D
		public string ConnectionName { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000033A6 File Offset: 0x000015A6
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000033AE File Offset: 0x000015AE
		public string QueryString { get; set; }

		// Token: 0x06000068 RID: 104 RVA: 0x000033B7 File Offset: 0x000015B7
		public SqlRowsetDefinition()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033BF File Offset: 0x000015BF
		public SqlRowsetDefinition(XmlReader reader)
		{
			this.ReadXml(reader);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000033D0 File Offset: 0x000015D0
		public override IDataReader CreateDataReader(ConnectionManager connectionManager)
		{
			SqlConnection connection = connectionManager.GetConnection(this.ConnectionName);
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandTimeout = connection.ConnectionTimeout;
			sqlCommand.CommandText = this.QueryString;
			return sqlCommand.ExecuteReader();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003410 File Offset: 0x00001610
		public override DataTable GetSchemaTable(ConnectionManager connectionManager)
		{
			SqlConnection connection = connectionManager.GetConnection(this.ConnectionName);
			DataTable schemaTable;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandTimeout = connection.ConnectionTimeout;
				sqlCommand.CommandText = this.QueryString;
				using (IDataReader dataReader = sqlCommand.ExecuteReader(2))
				{
					schemaTable = dataReader.GetSchemaTable();
				}
			}
			return schemaTable;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000348C File Offset: 0x0000168C
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003490 File Offset: 0x00001690
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			if (reader.ReadToFollowing("SqlRowset"))
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
				if (reader.MoveToAttribute("connectionName"))
				{
					this.ConnectionName = reader.Value;
				}
				reader.MoveToElement();
				this.QueryString = reader.ReadString();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000352C File Offset: 0x0000172C
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("SqlRowset");
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
			if (!string.IsNullOrEmpty(this.ConnectionName))
			{
				writer.WriteAttributeString("connectionName", this.ConnectionName);
			}
			writer.WriteString(this.QueryString);
			writer.WriteEndElement();
		}
	}
}
