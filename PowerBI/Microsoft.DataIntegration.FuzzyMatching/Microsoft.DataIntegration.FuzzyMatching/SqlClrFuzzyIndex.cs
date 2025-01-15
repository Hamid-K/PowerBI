using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public class SqlClrFuzzyIndex : IXmlSerializable, IName, IDropTables
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000088B1 File Offset: 0x00006AB1
		// (set) Token: 0x060001DF RID: 479 RVA: 0x000088B9 File Offset: 0x00006AB9
		public string Name { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000088C2 File Offset: 0x00006AC2
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x000088CA File Offset: 0x00006ACA
		public string Description { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000088D3 File Offset: 0x00006AD3
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x000088DB File Offset: 0x00006ADB
		public string DomainManagerName { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000088E4 File Offset: 0x00006AE4
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000088EC File Offset: 0x00006AEC
		public string RowsetName { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000088F5 File Offset: 0x00006AF5
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000088FD File Offset: 0x00006AFD
		public LookupCollection Lookups { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008906 File Offset: 0x00006B06
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000890E File Offset: 0x00006B0E
		public RecordBinding RecordBinding { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008917 File Offset: 0x00006B17
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000891F File Offset: 0x00006B1F
		public JoinSide JoinSide { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008928 File Offset: 0x00006B28
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00008930 File Offset: 0x00006B30
		internal string SchemaName { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008939 File Offset: 0x00006B39
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00008941 File Offset: 0x00006B41
		public Type RidType { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000894A File Offset: 0x00006B4A
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00008952 File Offset: 0x00006B52
		public SqlName RecordTableName { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x0000895B File Offset: 0x00006B5B
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00008963 File Offset: 0x00006B63
		public SqlName RecordContextTableName { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000896C File Offset: 0x00006B6C
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00008974 File Offset: 0x00006B74
		public SqlName SignatureTableName { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000897D File Offset: 0x00006B7D
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00008985 File Offset: 0x00006B85
		public List<Property> Properties { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000898E File Offset: 0x00006B8E
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00008996 File Offset: 0x00006B96
		public string CreationConnectionName { get; set; }

		// Token: 0x060001FA RID: 506 RVA: 0x0000899F File Offset: 0x00006B9F
		public SqlClrFuzzyIndex()
		{
			this.RecordTableName = SqlName.Empty;
			this.RecordContextTableName = SqlName.Empty;
			this.SignatureTableName = SqlName.Empty;
			this.Lookups = new LookupCollection();
			this.Properties = new List<Property>();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000089DE File Offset: 0x00006BDE
		public SqlClrFuzzyIndex(XmlReader reader)
			: this()
		{
			this.ReadXml(reader);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000089ED File Offset: 0x00006BED
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000089F0 File Offset: 0x00006BF0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			reader = FuzzyLookupXmlBuilder.CreateValidatingXmlReader(reader);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(reader);
			XmlNamespaceManager xmlNamespaceManager = xmlDocument.CreateFL3XmlNamespaceManager();
			XmlNode xmlNode = xmlDocument.SelectSingleNode("/ns:FuzzyIndex", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.Name = xmlNode.Attributes.GetNamedItemOrDefault("name", null);
				this.Description = xmlNode.Attributes.GetNamedItemOrDefault("description", null);
				XmlNode xmlNode2;
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("domainManagerName")) != null)
				{
					this.DomainManagerName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("rowsetName")) != null)
				{
					this.RowsetName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("joinSide")) != null)
				{
					this.JoinSide = (JoinSide)Enum.Parse(typeof(JoinSide), xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("ridType")) != null)
				{
					this.RidType = Type.GetType(xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("creationConnectionName")) != null)
				{
					this.CreationConnectionName = xmlNode2.Value;
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("recordTableName")) != null)
				{
					this.RecordTableName = new SqlName(xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("recordContextTableName")) != null)
				{
					this.RecordContextTableName = new SqlName(xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.Attributes.GetNamedItem("signatureTableName")) != null)
				{
					this.SignatureTableName = new SqlName(xmlNode2.Value);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:RecordBinding", xmlNamespaceManager)) != null)
				{
					this.RecordBinding = new RecordBinding();
					this.RecordBinding.ReadXml(new XmlNodeReader(xmlNode2));
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Properties", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Property>(new XmlNodeReader(xmlNode2), "Properties", this.Properties);
				}
				if ((xmlNode2 = xmlNode.SelectSingleNode("/*/ns:Lookups", xmlNamespaceManager)) != null)
				{
					CollectionSerialization.ReadXml<Lookup>(new XmlNodeReader(xmlNode2), "Lookups", this.Lookups);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008BF8 File Offset: 0x00006DF8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("FuzzyIndex");
			if (!string.IsNullOrEmpty(this.Name))
			{
				writer.WriteAttributeString("name", this.Name);
			}
			if (!string.IsNullOrEmpty(this.Description))
			{
				writer.WriteAttributeString("description", this.Description);
			}
			if (!string.IsNullOrEmpty(this.DomainManagerName))
			{
				writer.WriteAttributeString("domainManagerName", this.DomainManagerName);
			}
			if (!string.IsNullOrEmpty(this.RowsetName))
			{
				writer.WriteAttributeString("rowsetName", this.RowsetName);
			}
			if (this.RidType != null)
			{
				writer.WriteAttributeString("ridType", this.RidType.ToString());
			}
			if (!string.IsNullOrEmpty(this.CreationConnectionName))
			{
				writer.WriteAttributeString("creationConnectionName", this.CreationConnectionName);
			}
			if (this.JoinSide != JoinSide.Undefined)
			{
				writer.WriteAttributeString("joinSide", this.JoinSide.ToString());
			}
			if (!SqlName.IsNullOrEmpty(this.RecordTableName))
			{
				writer.WriteAttributeString("recordTableName", this.RecordTableName.QualifiedName);
			}
			if (!SqlName.IsNullOrEmpty(this.RecordContextTableName))
			{
				writer.WriteAttributeString("recordContextTableName", this.RecordContextTableName.QualifiedName);
			}
			if (!SqlName.IsNullOrEmpty(this.SignatureTableName))
			{
				writer.WriteAttributeString("signatureTableName", this.SignatureTableName.QualifiedName);
			}
			if (this.RecordBinding != null)
			{
				this.RecordBinding.WriteXml(writer);
			}
			CollectionSerialization.WriteXml<Property>(writer, "Properties", this.Properties);
			CollectionSerialization.WriteXml<Lookup>(writer, "Lookups", this.Lookups);
			writer.WriteEndElement();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008D90 File Offset: 0x00006F90
		public bool TryDropTables(ConnectionManager connectionManager)
		{
			bool flag = false;
			ConnectionManager connectionManager2 = null;
			if (connectionManager == null)
			{
				connectionManager2 = new ConnectionManager();
				connectionManager = connectionManager2;
			}
			try
			{
				SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
				if (connection.State == 1)
				{
					using (connection.CreateCommand())
					{
						if (this.RecordTableName != null)
						{
							flag = SqlUtils.TryDropTable(connection, this.RecordTableName);
						}
						if (this.RecordContextTableName != null)
						{
							flag = SqlUtils.TryDropTable(connection, this.RecordContextTableName) && flag;
						}
						if (this.SignatureTableName != null)
						{
							flag = SqlUtils.TryDropTable(connection, this.SignatureTableName) && flag;
						}
					}
				}
			}
			finally
			{
				if (connectionManager2 != null)
				{
					connectionManager2.Dispose();
				}
			}
			return flag;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008E44 File Offset: 0x00007044
		public static string CreateJoinQuery(string comparerName, SqlClrFuzzyIndex leftIndex, SqlClrFuzzyIndex rightIndex, string schemaName, double threshold, int maxComparisons, bool filterIdentityMatches, bool generateXml = false)
		{
			return string.Format(string.Concat(new string[]
			{
				"\r\n                    declare @comparerHandle int = {6}.GetObjectHandle({0});\r\n                    declare @domainManagerHandle int = {6}.GetObjectHandle({9});\r\n\r\n                    with __FL3_S as \r\n                    (\r\n\t                    select {4}.RID as LRID, {5}.RID as RRID, COUNT(distinct {4}.Signature / 0x0000000100000000) as Cnt\r\n\t                    from {4} with (nolock)\r\n                        join {5} with (nolock) \r\n\t                    on {4}.Signature={5}.Signature\r\n\t                    group by {4}.RID, {5}.RID\r\n                    ),\r\n                    __FL3_S2 as \r\n                    (\r\n\t                    select LRID, RRID, row_number() over (partition by LRID order by Cnt DESC, RRID ASC) RN\r\n\t                    from __FL3_S\r\n                    ),\r\n                    __FL3_PAIRS as \r\n                    (\r\n                        select LRID, RRID \r\n                        from __FL3_S2\r\n                        where RN <= {8} OR LRID=RRID\r\n                    )\r\n                    select LRID, RRID\r\n                        , Similarity ",
				generateXml ? ", SimilarityXml " : "",
				"\r\n                    from \r\n                    (\r\n                        select LRID, RRID\r\n                            , {6}.RecordContextSimilarity(@comparerHandle, @domainManagerHandle, {1}.RecordContext, {2}.RecordContext, {3}) as Similarity",
				generateXml ? ", {6}.RecordContextSimilarityXml(@comparerHandle, @domainManagerHandle, {1}.RecordContext, {2}.RecordContext, {3}) as SimilarityXml" : "",
				"\r\n                        from __FL3_PAIRS\r\n                        inner loop join {1} on LRID={1}.RID\r\n                        inner loop join {2} on RRID={2}.RID\r\n                    ) as a\r\n                    where Similarity is not null {7}\r\n                "
			}), new object[]
			{
				SqlName.CreateStringLiteral(comparerName),
				leftIndex.RecordContextTableName.QualifiedName,
				rightIndex.RecordContextTableName.QualifiedName,
				threshold,
				leftIndex.SignatureTableName.QualifiedName,
				rightIndex.SignatureTableName.QualifiedName,
				SqlName.DelimitElement(schemaName),
				filterIdentityMatches ? "and LRID <> RRID" : "",
				maxComparisons,
				SqlName.CreateStringLiteral(rightIndex.DomainManagerName)
			});
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008F28 File Offset: 0x00007128
		private bool IsIntegerColumn(DataTable schemaTable, string columnName)
		{
			int ordinal = SchemaUtils.GetOrdinal(schemaTable, columnName, true);
			return (Type)schemaTable.Rows[ordinal][SchemaTableColumn.DataType] == typeof(int);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008F68 File Offset: 0x00007168
		private void WriteInlineRowsetToSql(ConnectionManager connectionManager, InlineRowset rowset, SqlName tableName, bool overwriteExisting, bool appendRidColumn, out SqlRowsetDefinition sqlRowset)
		{
			DataTable dataTable = rowset.DataTable;
			SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
			if (overwriteExisting)
			{
				SqlUtils.TryDropTable(connection, tableName);
			}
			DataTable schemaTable = rowset.GetSchemaTable(connectionManager);
			string text = rowset.RidColumnName;
			if (appendRidColumn)
			{
				text = SchemaUtils.GenerateUniqueColumnName(schemaTable, "RID");
				schemaTable.Columns.Add(text, typeof(int));
			}
			int num = 1;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				SqlUtils.CreateTableFromSchema(connection, schemaTable, tableName, overwriteExisting);
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				for (int i = 0; i < schemaTable.Rows.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(", ");
						stringBuilder2.Append(", ");
					}
					string text2 = (string)schemaTable.Rows[i][SchemaTableColumn.ColumnName];
					stringBuilder.Append(SqlName.DelimitElement(text2));
					string text3 = "@" + i.ToString();
					stringBuilder2.Append(text3);
					DataRow dataRow = schemaTable.Rows[i];
					SqlDbType sqlDbType;
					int num2;
					SqlUtils.TypeToSqlDbType((Type)dataRow[SchemaTableColumn.DataType], (int)dataRow[SchemaTableColumn.ColumnSize], out sqlDbType, out num2);
					sqlCommand.Parameters.Add(text3, sqlDbType, num2);
				}
				sqlCommand.CommandText = string.Format("insert into {0} ({1}) values ({2})", tableName.QualifiedName, stringBuilder, stringBuilder2);
				sqlCommand.Prepare();
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow2 = (DataRow)obj;
					for (int j = 0; j < schemaTable.Rows.Count; j++)
					{
						if (appendRidColumn && j == schemaTable.Rows.Count)
						{
							sqlCommand.Parameters[1].Value = num++;
						}
						else
						{
							sqlCommand.Parameters[j].Value = dataRow2[j];
						}
					}
					sqlCommand.ExecuteNonQuery();
				}
			}
			sqlRowset = new SqlRowsetDefinition();
			sqlRowset.RidColumnName = text;
			sqlRowset.Name = rowset.Name;
			sqlRowset.QueryString = string.Format("select * from {0}", tableName.QualifiedName);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009210 File Offset: 0x00007410
		public int Remove(ConnectionManager connectionManager, IDomainManager domainManager, ITokenIdProvider tokenIdProvider, IRowsetDefinition rowsetDefinition)
		{
			int num = 0;
			if (string.IsNullOrEmpty(rowsetDefinition.RidColumnName))
			{
				DataTable schemaTable = rowsetDefinition.GetSchemaTable(connectionManager);
				if (schemaTable.Rows.Count != 1)
				{
					throw new Exception("Must define the RidColumnName attribute on the Rowset passed to RemoveRecords.");
				}
				rowsetDefinition.RidColumnName = (string)schemaTable.Rows[0][SchemaTableColumn.ColumnName];
			}
			SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandTimeout = 0;
				SqlName sqlName = null;
				if (rowsetDefinition is InlineRowset)
				{
					sqlName = new SqlName("#RecordsToDelete");
					SqlRowsetDefinition sqlRowsetDefinition;
					this.WriteInlineRowsetToSql(connectionManager, rowsetDefinition as InlineRowset, sqlName, true, false, out sqlRowsetDefinition);
					rowsetDefinition = sqlRowsetDefinition;
				}
				if (!(rowsetDefinition is SqlRowsetDefinition))
				{
					throw new Exception("Unexpected RowsetDefinition type.");
				}
				SqlRowsetDefinition sqlRowsetDefinition2 = rowsetDefinition as SqlRowsetDefinition;
				sqlCommand.CommandText = string.Format("\r\n                        with R as ({0})\r\n                        delete from {1}\r\n                        where RID in (select {2} from R)", sqlRowsetDefinition2.QueryString, this.RecordContextTableName.QualifiedName, sqlRowsetDefinition2.RidColumnName);
				num = sqlCommand.ExecuteNonQuery();
				sqlCommand.CommandText = string.Format("\r\n                        with R as ({0})\r\n                        delete from {1} \r\n                        where RID in (select {2} from R)", sqlRowsetDefinition2.QueryString, this.SignatureTableName.QualifiedName, sqlRowsetDefinition2.RidColumnName);
				sqlCommand.ExecuteNonQuery();
				if (sqlName != null)
				{
					SqlUtils.TryDropTable(connection, sqlName);
				}
			}
			return num;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009368 File Offset: 0x00007568
		public int Append(ConnectionManager connectionManager, IRowsetDefinition rowsetDefinition)
		{
			int num = 0;
			if (string.IsNullOrEmpty(rowsetDefinition.RidColumnName))
			{
				DataTable schemaTable = rowsetDefinition.GetSchemaTable(connectionManager);
				if (schemaTable.Rows.Count != 1)
				{
					throw new Exception("Must define the RidColumnName attribute on the Rowset passed to AddRecords.");
				}
				rowsetDefinition.RidColumnName = (string)schemaTable.Rows[0][SchemaTableColumn.ColumnName];
			}
			SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
			SqlName sqlName = null;
			try
			{
				if (rowsetDefinition is InlineRowset)
				{
					sqlName = new SqlName("#RecordsToAdd");
					SqlRowsetDefinition sqlRowsetDefinition;
					this.WriteInlineRowsetToSql(connectionManager, rowsetDefinition as InlineRowset, sqlName, true, false, out sqlRowsetDefinition);
					rowsetDefinition = sqlRowsetDefinition;
				}
				if (!(rowsetDefinition is SqlRowsetDefinition))
				{
					throw new Exception("Unexpected RowsetDefinition type.");
				}
				SqlRowsetDefinition sqlRowsetDefinition2 = rowsetDefinition as SqlRowsetDefinition;
				if (this.RecordBinding.Schema == null)
				{
					this.RecordBinding.Schema = sqlRowsetDefinition2.GetSchemaTable(connectionManager);
				}
				num = this.AppendRecordContextTable(connection, this.RecordContextTableName, sqlRowsetDefinition2);
				this.AppendSignatureTable(connection, this.SignatureTableName, this.RecordContextTableName, sqlRowsetDefinition2);
			}
			finally
			{
				if (sqlName != null)
				{
					SqlUtils.TryDropTable(connection, sqlName);
				}
			}
			return num;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00009484 File Offset: 0x00007684
		public int Create(ConnectionManager connectionManager, IDomainManager domainManager, IRowsetDefinition rowset, Type ridType)
		{
			int num = -1;
			if (this.RecordContextTableName == null || this.SignatureTableName == null)
			{
				throw new Exception("Must set RecordContextTableName and SignatureTableName.");
			}
			SqlConnection connection = connectionManager.GetConnection(ConnectionManager.ContextConnectionName);
			string name = this.Name;
			this.Name = Guid.NewGuid().ToString();
			int num3;
			try
			{
				int num2 = SqlClr.ObjectManager.CreateReference(this.Name, this);
				bool commitTemporaryObjectsToTempDb = SqlClr.ObjectManager.CommitTemporaryObjectsToTempDb;
				if (commitTemporaryObjectsToTempDb)
				{
					SqlClr.ObjectManager.Commit(num2, null, connectionManager, ConnectionManager.ContextConnectionName, true);
				}
				try
				{
					if (rowset is InlineRowset)
					{
						if (this.RecordTableName == null)
						{
							throw new Exception("Must set RecordTableName.");
						}
						SqlRowsetDefinition sqlRowsetDefinition;
						this.WriteInlineRowsetToSql(connectionManager, rowset as InlineRowset, this.RecordTableName, false, false, out sqlRowsetDefinition);
						rowset = sqlRowsetDefinition;
					}
					if (!(rowset is SqlRowsetDefinition))
					{
						if (rowset == null)
						{
							string text;
							if (ridType == typeof(int))
							{
								text = "int";
							}
							else if (ridType == typeof(long))
							{
								text = "bigint";
							}
							else
							{
								if (ridType != typeof(Guid))
								{
									throw new Exception(string.Format("{0} is an unsupported rid type.  Ensure RidType is specified in the RecordBinding.", ridType.ToString()));
								}
								text = "uniqueidentifier";
							}
							using (SqlCommand sqlCommand = connection.CreateCommand())
							{
								sqlCommand.CommandText = string.Format("CREATE TABLE {0} (RID {1} not null, RecordContext varbinary({2}))", this.RecordContextTableName.QualifiedName, text, 8000);
								sqlCommand.ExecuteNonQuery();
								sqlCommand.CommandText = string.Format("ALTER TABLE {0} ADD CONSTRAINT [{1}] PRIMARY KEY ({2})", this.RecordContextTableName.QualifiedName, Guid.NewGuid().ToString(), "RID");
								sqlCommand.ExecuteNonQuery();
								goto IL_01E6;
							}
						}
						throw new NotImplementedException("Unexpected RowsetDefinition type.");
					}
					if (this.RecordBinding.Schema == null)
					{
						this.RecordBinding.Schema = rowset.GetSchemaTable(connectionManager);
					}
					num = this.CreateRecordContextTable(connection, this.RecordContextTableName, rowset as SqlRowsetDefinition);
					IL_01E6:
					this.CreateSignatureTable(connection, this.SignatureTableName, this.RecordContextTableName);
				}
				catch (Exception ex)
				{
					this.TryDropTables(connectionManager);
					throw ex;
				}
				finally
				{
					SqlClr.ObjectManager.Drop(this.Name, connectionManager, false, commitTemporaryObjectsToTempDb);
				}
				num3 = num;
			}
			finally
			{
				this.Name = name;
			}
			return num3;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009720 File Offset: 0x00007920
		public int CreateSignatureTable(SqlConnection connection, SqlName signatureTableName, SqlName recordContextTableName)
		{
			int num = 0;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandText = string.Format("  declare @signatureGeneratorHandle int = {3}.GetObjectHandle({2});\r\n\r\n                        select ISNULL(sig.Signature, 0) as Signature, RID\r\n                        into {0}\r\n                        from {1} with (nolock)\r\n                        cross apply \r\n                        {3}.Signatures(@signatureGeneratorHandle, RecordContext) as sig", new object[]
				{
					signatureTableName.QualifiedName,
					recordContextTableName.QualifiedName,
					SqlName.CreateStringLiteral(this.Name),
					SqlName.DelimitElement(this.SchemaName)
				});
				num = sqlCommand.ExecuteNonQuery();
				sqlCommand.CommandText = string.Format("ALTER TABLE {0} ADD CONSTRAINT [{1}] PRIMARY KEY ({2})", signatureTableName.QualifiedName, Guid.NewGuid().ToString(), "Signature, RID");
				sqlCommand.ExecuteNonQuery();
				sqlCommand.CommandText = string.Format("create index rid on {0}(RID)", signatureTableName.QualifiedName);
				sqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000097F0 File Offset: 0x000079F0
		public void AppendSignatureTable(SqlConnection connection, SqlName signatureTableName, SqlName recordContextTableName, SqlRowsetDefinition recordsToAddRowset)
		{
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				sqlCommand.CommandText = string.Format("  declare @signatureGeneratorHandle int = {3}.GetObjectHandle({2});\r\n\r\n                        with R as ({4})\r\n                        insert into {0} (Signature, RID)\r\n                        select sig.*, RID\r\n                        from {1} with (nolock)\r\n                        cross apply \r\n                        {3}.Signatures(@signatureGeneratorHandle, RecordContext) as sig\r\n                        where RID in (select {5} from R with (nolock))\r\n                        ", new object[]
				{
					signatureTableName.QualifiedName,
					recordContextTableName.QualifiedName,
					SqlName.CreateStringLiteral(this.Name),
					SqlName.DelimitElement(this.SchemaName),
					recordsToAddRowset.QueryString,
					SqlName.DelimitElement(recordsToAddRowset.RidColumnName)
				});
				sqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00009884 File Offset: 0x00007A84
		public int CreateRecordContextTable(SqlConnection connection, SqlName recordContextTableName, SqlRowsetDefinition rowset)
		{
			int num = 0;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				string text = SchemaUtils.CommaSeparatedListOfColumnNames(this.RecordBinding.Schema);
				int count = this.RecordBinding.Schema.Rows.Count;
				sqlCommand.CommandText = string.Format("  declare @domainManagerHandle int = {6}.GetObjectHandle({8});\r\n                        declare @recordBindingHandle int = {6}.GetObjectHandle({2});\r\n\r\n                        with R as ({0}),\r\n                        RC as \r\n                        (\r\n                            select ISNULL({1}, {7}) AS RID\r\n                                 , {6}.RecordContext(@domainManagerHandle, @recordBindingHandle, {6}.Record::Create{3}({4})) as RecordContext\r\n                            from R with (nolock)\r\n                        )\r\n                        select RID, RecordContext\r\n                        into {5}\r\n                        from RC with (nolock)\r\n                        order by len(RecordContext)\r\n                    ", new object[]
				{
					rowset.QueryString,
					SqlName.DelimitElement(rowset.RidColumnName),
					SqlName.CreateStringLiteral(this.Name),
					count,
					text,
					recordContextTableName.QualifiedName,
					SqlName.DelimitElement(this.SchemaName),
					SqlUtils.GetDefaultSqlIsNullStringLiteral((Type)SchemaUtils.GetRow(this.RecordBinding.Schema, rowset.RidColumnName)[SchemaTableColumn.DataType]),
					SqlName.CreateStringLiteral(this.DomainManagerName)
				});
				num = sqlCommand.ExecuteNonQuery();
				sqlCommand.CommandText = string.Format("ALTER TABLE {0} ADD CONSTRAINT [{1}] PRIMARY KEY ({2})", recordContextTableName.QualifiedName, Guid.NewGuid().ToString(), "RID");
				sqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000099C4 File Offset: 0x00007BC4
		public int AppendRecordContextTable(SqlConnection connection, SqlName recordContextTableName, SqlRowsetDefinition rowset)
		{
			int num = 0;
			using (SqlCommand sqlCommand = connection.CreateCommand())
			{
				string text = SchemaUtils.CommaSeparatedListOfColumnNames(this.RecordBinding.Schema);
				int count = this.RecordBinding.Schema.Rows.Count;
				sqlCommand.CommandText = string.Format("  declare @domainManagerHandle int = {7}.GetObjectHandle({8});\r\n                        declare @recordBindingHandle int = {7}.GetObjectHandle({2});\r\n\r\n                        with R as ({0})\r\n                        insert into {5} (RID, RecordContext) \r\n                        select {1} {6}\r\n                             , {7}.RecordContext(@domainManagerHandle, @recordBindingHandle, {7}.Record::Create{3}({4})) as RecordContext\r\n                        from R with (nolock)\r\n                    ", new object[]
				{
					rowset.QueryString,
					SqlName.DelimitElement(rowset.RidColumnName),
					SqlName.CreateStringLiteral(this.Name),
					count,
					text,
					recordContextTableName.QualifiedName,
					rowset.RidColumnName.Equals("RID") ? "" : "as RID",
					SqlName.DelimitElement(this.SchemaName),
					SqlName.CreateStringLiteral(this.DomainManagerName)
				});
				num = sqlCommand.ExecuteNonQuery();
			}
			return num;
		}

		// Token: 0x04000088 RID: 136
		private const int MaxColumns = 16;

		// Token: 0x04000089 RID: 137
		private const int RecordContextMaxLength = 8000;
	}
}
