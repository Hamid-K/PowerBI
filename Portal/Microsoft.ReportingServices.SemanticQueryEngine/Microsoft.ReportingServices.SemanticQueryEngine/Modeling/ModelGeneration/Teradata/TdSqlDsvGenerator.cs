using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.SemanticQueryEngine;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.Teradata
{
	// Token: 0x02000006 RID: 6
	internal sealed class TdSqlDsvGenerator : SqlDsvGenerator
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000331E File Offset: 0x0000151E
		internal TdSqlDsvGenerator(IDbConnection connection)
			: base(connection)
		{
			if (!(this.Connection.GetType() == TdSqlDsvGenerator.GetTdConnectionType()))
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be TdConnection.", Array.Empty<object>());
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000334E File Offset: 0x0000154E
		internal static DsvCompareInfo GetCompareInfoStatic(IDbConnection connection)
		{
			return null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003354 File Offset: 0x00001554
		internal static Type GetTdConnectionType()
		{
			if (TdSqlDsvGenerator.m_ConnectionType == null)
			{
				object staticLock = TdSqlDsvGenerator.StaticLock;
				lock (staticLock)
				{
					TdSqlDsvGenerator.m_ConnectionType = Microsoft.ReportingServices.Common.DataExtensionsHelper.GetDataExtensionConnectionType("TeradataConnectionWrapper", "GetTdConnectionType");
					if (TdSqlDsvGenerator.m_ConnectionType == null)
					{
						throw SQEAssert.AssertFalseAndThrow("Couldn't get the TdConnectionType.", Array.Empty<object>());
					}
				}
			}
			return TdSqlDsvGenerator.m_ConnectionType;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000033D0 File Offset: 0x000015D0
		protected override string GetDsvName()
		{
			this.m_owner = this.Connection.Database;
			return this.m_owner;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000033E9 File Offset: 0x000015E9
		protected override DsvCompareInfo GetCompareInfo()
		{
			return TdSqlDsvGenerator.GetCompareInfoStatic(this.Connection);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000033F6 File Offset: 0x000015F6
		protected override SqlDsvGenerator.DbObjectName CreateDbObjectName(string schemaName, string objectName)
		{
			return new TdSqlDsvGenerator.TdSqlDbObjectName(schemaName, objectName);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003400 File Offset: 0x00001600
		protected override string GetTableColumnsSelectList(SqlDsvGenerator.DbObjectName tableName)
		{
			Dictionary<string, SqlDsvGenerator.ColExtSchema> dictionary;
			if (!base.ColumnsExtendedSchema.TryGetValue(tableName, out dictionary))
			{
				return base.GetTableColumnsSelectList(tableName);
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, SqlDsvGenerator.ColExtSchema> keyValuePair in dictionary)
			{
				if (TdSqlDsvGenerator.SupportedTypesDetector.Match(keyValuePair.Value.DbTypeName ?? "").Success)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(TdSqlBatch.GetDelimitedIdentifierStatic(keyValuePair.Key));
				}
				else
				{
					base.TraceWarning("Column {0} of the table {1} has unsupported data type {2}. The column will be ignored.", new object[]
					{
						keyValuePair.Key,
						tableName.ToString(),
						keyValuePair.Value.DbTypeName
					});
				}
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			base.TraceWarning("Table {0} has no columns of supported data types. The table will be ignored.", new object[] { tableName.ToString() });
			return null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000351C File Offset: 0x0000171C
		protected override SqlDsvGenerator.DbObjectName[] GetTables()
		{
			DataTable schema = this.Connection.GetSchema("Tables", new string[] { this.m_owner });
			SqlDsvGenerator.DbObjectName[] array = new SqlDsvGenerator.DbObjectName[schema.Rows.Count];
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				DataRow dataRow = schema.Rows[i];
				array[i] = new TdSqlDsvGenerator.TdSqlDbObjectName(dataRow["TABLE_SCHEMA"].ToString(), dataRow["TABLE_NAME"].ToString());
			}
			return array;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000035A6 File Offset: 0x000017A6
		protected override IEnumerable<SqlDsvGenerator.ColExtendedInfo> GetColExtInfo()
		{
			DataTable schema = this.Connection.GetSchema("Columns", new string[] { this.m_owner });
			foreach (DataRow dataRow in schema.Select(null, "TABLE_SCHEMA, TABLE_NAME"))
			{
				string text = dataRow["TABLE_SCHEMA"].ToString();
				string text2 = dataRow["TABLE_NAME"].ToString();
				string text3 = dataRow["COLUMN_NAME"].ToString();
				string text4 = dataRow["COLUMN_TYPE"].ToString();
				string text5 = null;
				try
				{
					text5 = Convert.ToString(dataRow["CHARACTER_MAXIMUM_LENGTH"], CultureInfo.InvariantCulture);
				}
				catch (Exception ex)
				{
					base.TraceWarning("Failed to parse length of column {0} in the table {1} : {2}", new object[]
					{
						text3,
						text2,
						ex.ToString()
					});
				}
				yield return new SqlDsvGenerator.ColExtendedInfo(text, text2, text3, text4, text5);
			}
			DataRow[] array = null;
			yield break;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000035B6 File Offset: 0x000017B6
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetPKInfo()
		{
			using (DbCommand cmd = this.Connection.CreateCommand())
			{
				string text = "LOCK VIEW DBC.IndicesX FOR ACCESS Select DISTINCT CAST('' as VarChar(30)) as \"TABLE_CATALOG\", Trim(Trailing FROM DatabaseName) as \"TABLE_SCHEMA\", Trim(Trailing from TableName)  as \"TABLE_NAME\", Trim(Trailing from IndexName) as \"INDEX_NAME\" FROM DBC.IndicesX WHERE IndexType IN ('K', 'P') AND UniqueFlag='Y' AND TABLE_SCHEMA = '{0}' ORDER BY CAST(TABLE_SCHEMA AS CASESPECIFIC), CAST(TABLE_NAME AS CASESPECIFIC)";
				cmd.CommandText = string.Format(text, this.EscapeQuotation(this.m_owner));
				using (DbDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int constraintNameOrdinal = reader.GetOrdinal("INDEX_NAME");
					while (reader.Read())
					{
						string text2 = (reader.IsDBNull(constraintNameOrdinal) ? string.Format("pk_{0}_{1}", reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal)) : reader.GetString(constraintNameOrdinal));
						yield return new SqlDsvGenerator.UniqueConstraintInfo(reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal), text2);
					}
				}
				DbDataReader reader = null;
			}
			DbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000035C6 File Offset: 0x000017C6
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetUCInfo()
		{
			yield break;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000035CF File Offset: 0x000017CF
		protected override IEnumerable<SqlDsvGenerator.FKConstraintInfo> GetFKInfo()
		{
			using (DbCommand cmd = this.Connection.CreateCommand())
			{
				string text = "LOCK VIEW DBC.ALL_RI_PARENTS FOR ACCESS LOCK VIEW DBC.INDICES FOR ACCESS SELECT DISTINCT \tCAST('' as VarChar(30)) as \"PK_TABLE_CATALOG\", \tTrim(Trailing from a.ParentDB) AS \"PK_TABLE_SCHEMA\", \tTrim(Trailing from a.ParentTable) AS \"PK_TABLE_NAME\", \tCAST('' as VarChar(30)) as \"FK_TABLE_CATALOG\", \tTrim(Trailing FROM a.ChildDB) AS \"FK_TABLE_SCHEMA\", \tTrim(Trailing from a.ChildTable) AS \"FK_TABLE_NAME\", \tTrim(Trailing from a.IndexName) AS \"FK_INDEX_NAME\", \tTrim(Trailing from b.IndexName) AS \"PK_INDEX_NAME\" FROM \tDBC.ALL_RI_PARENTS a, \tDBC.INDICES b WHERE \tUPPER(Trim(Trailing from a.ParentDB)) = UPPER(Trim(Trailing from b.DatabaseName)) \tAND UPPER(Trim(Trailing from a.ParentTable)) = UPPER(Trim(Trailing from b.TableName)) \tAND UPPER(Trim(Trailing from a.ParentKeyColumn)) = UPPER(Trim(Trailing from b.ColumnName)) \tAND b.IndexType IN ('K', 'P')  AND b.UniqueFlag = 'Y'  AND PK_TABLE_SCHEMA = FK_TABLE_SCHEMA  AND PK_TABLE_SCHEMA= '{0}'  ORDER BY \tCAST(FK_TABLE_SCHEMA AS CASESPECIFIC),  CAST(FK_TABLE_NAME AS CASESPECIFIC)";
				cmd.CommandText = string.Format(text, this.EscapeQuotation(this.m_owner));
				using (DbDataReader reader = cmd.ExecuteReader())
				{
					int fkTableSchemaOrdinal = reader.GetOrdinal("FK_TABLE_SCHEMA");
					int fkTableNameOrdinal = reader.GetOrdinal("FK_TABLE_NAME");
					int fkConstraintNameOrdinal = reader.GetOrdinal("FK_INDEX_NAME");
					int pkTableSchemaOrdinal = reader.GetOrdinal("PK_TABLE_SCHEMA");
					int pkTableNameOrdinal = reader.GetOrdinal("PK_TABLE_NAME");
					int pkConstraintNameOrdinal = reader.GetOrdinal("PK_INDEX_NAME");
					while (reader.Read())
					{
						string text2 = (reader.IsDBNull(pkConstraintNameOrdinal) ? string.Format("pk_{0}_{1}", reader.GetString(pkTableSchemaOrdinal), reader.GetString(pkTableNameOrdinal)) : reader.GetString(pkConstraintNameOrdinal));
						string text3 = (reader.IsDBNull(fkConstraintNameOrdinal) ? string.Format("fk_{0}_{1}_{2}", reader.GetString(fkTableSchemaOrdinal), reader.GetString(fkTableNameOrdinal), reader.GetString(pkTableNameOrdinal)) : reader.GetString(fkConstraintNameOrdinal));
						yield return new SqlDsvGenerator.FKConstraintInfo(reader.GetString(fkTableSchemaOrdinal), reader.GetString(fkTableNameOrdinal), text3, reader.GetString(pkTableSchemaOrdinal), reader.GetString(pkTableNameOrdinal), text2);
					}
				}
				DbDataReader reader = null;
			}
			DbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000035DF File Offset: 0x000017DF
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetPrimaryUniqueKeyColumnsInfo()
		{
			using (DbCommand cmd = this.Connection.CreateCommand())
			{
				string text = "LOCK VIEW DBC.IndicesX FOR ACCESS Select CAST('' as VarChar(30)) as \"TABLE_CATALOG\", Trim(Trailing FROM DatabaseName) as \"TABLE_SCHEMA\", Trim(Trailing from TableName)  as \"TABLE_NAME\", Trim(Trailing from ColumnName) as \"COLUMN_NAME\", ColumnPosition as \"ORDINAL_POSITION\", Trim(Trailing from IndexName) as \"INDEX_NAME\" FROM DBC.IndicesX WHERE IndexType IN ('K', 'P') AND UniqueFlag = 'Y' AND TABLE_SCHEMA = '{0}' ORDER BY TABLE_SCHEMA, TABLE_NAME, INDEX_NAME, ORDINAL_POSITION ";
				cmd.CommandText = string.Format(text, this.EscapeQuotation(this.m_owner));
				using (DbDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int constraintNameOrdinal = reader.GetOrdinal("INDEX_NAME");
					int columnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
					while (reader.Read())
					{
						string text2 = (reader.IsDBNull(constraintNameOrdinal) ? string.Format("pk_{0}_{1}", reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal)) : reader.GetString(constraintNameOrdinal));
						yield return new SqlDsvGenerator.ConstraintColumnInfo(reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal), text2, reader.GetString(columnNameOrdinal));
					}
				}
				DbDataReader reader = null;
			}
			DbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000035EF File Offset: 0x000017EF
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetForeignKeyColumnsInfo()
		{
			using (DbCommand cmd = this.Connection.CreateCommand())
			{
				string text = "LOCK VIEW DBC.ALL_RI_PARENTS FOR ACCESS LOCK VIEW DBC.INDICES FOR ACCESS SELECT \tCAST('' as VarChar(30)) as \"PK_TABLE_CATALOG\", \tTrim(Trailing from a.ParentDB) AS \"PK_TABLE_SCHEMA\", \tTrim(Trailing from a.ParentTable) AS \"PK_TABLE_NAME\", \tTrim(Trailing from a.ParentKeyColumn) AS \"PK_COLUMN_NAME\", \tCAST('' as VarChar(30)) as \"FK_TABLE_CATALOG\", \tTrim(Trailing FROM a.ChildDB) AS \"FK_TABLE_SCHEMA\", \tTrim(Trailing from a.ChildTable) AS \"FK_TABLE_NAME\", \tTrim(Trailing from a.ChildKeyColumn) AS \"FK_COLUMN_NAME\", \tb.ColumnPosition AS \"ORDINAL_POSITION\", \tTrim(Trailing from a.IndexName) AS \"FK_INDEX_NAME\", \tTrim(Trailing from b.IndexName) AS \"PK_INDEX_NAME\" FROM \tDBC.ALL_RI_PARENTS a, \tDBC.INDICES b WHERE \tUPPER(Trim(Trailing from a.ParentDB)) = UPPER(Trim(Trailing from b.DatabaseName)) \tAND UPPER(Trim(Trailing from a.ParentTable)) = UPPER(Trim(Trailing from b.TableName)) \tAND UPPER(Trim(Trailing from a.ParentKeyColumn)) = UPPER(Trim(Trailing from b.ColumnName)) \tAND b.IndexType IN ('K', 'P') \tAND b.UniqueFlag = 'Y'  AND PK_TABLE_SCHEMA = FK_TABLE_SCHEMA  AND PK_TABLE_SCHEMA= '{0}'  ORDER BY FK_TABLE_SCHEMA, FK_TABLE_NAME, FK_INDEX_NAME, ORDINAL_POSITION";
				cmd.CommandText = string.Format(text, this.EscapeQuotation(this.m_owner));
				using (DbDataReader reader = cmd.ExecuteReader())
				{
					int fkTableSchemaOrdinal = reader.GetOrdinal("FK_TABLE_SCHEMA");
					int fkTableNameOrdinal = reader.GetOrdinal("FK_TABLE_NAME");
					int fkConstraintNameOrdinal = reader.GetOrdinal("FK_INDEX_NAME");
					int fkColumnNameOrdinal = reader.GetOrdinal("FK_COLUMN_NAME");
					int pkTableNameOrdinal = reader.GetOrdinal("PK_TABLE_NAME");
					while (reader.Read())
					{
						string text2 = (reader.IsDBNull(fkConstraintNameOrdinal) ? string.Format("fk_{0}_{1}_{2}", reader.GetString(fkTableSchemaOrdinal), reader.GetString(fkTableNameOrdinal), reader.GetString(pkTableNameOrdinal)) : reader.GetString(fkConstraintNameOrdinal));
						yield return new SqlDsvGenerator.ConstraintColumnInfo(reader.GetString(fkTableSchemaOrdinal), reader.GetString(fkTableNameOrdinal), text2, reader.GetString(fkColumnNameOrdinal));
					}
				}
				DbDataReader reader = null;
			}
			DbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000035FF File Offset: 0x000017FF
		private string EscapeQuotation(string str)
		{
			return TdSqlDsvGenerator.encoder.GetString(TdSqlDsvGenerator.encoder.GetBytes(str)).Replace("\\", "\\\\").Replace("'", "''");
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003634 File Offset: 0x00001834
		private new DbConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return (DbConnection)base.Connection;
			}
		}

		// Token: 0x0400003F RID: 63
		private string m_owner;

		// Token: 0x04000040 RID: 64
		private static Type m_ConnectionType = null;

		// Token: 0x04000041 RID: 65
		private static object StaticLock = new object();

		// Token: 0x04000042 RID: 66
		private static readonly Regex SupportedTypesDetector = new Regex("(^BIGINT(?<=\\w)(?!\\w))|(^BLOB(?<=\\w)(?!\\w))(^BYTE(?<=\\w)(?!\\w))|(^BYTEINT(?<=\\w)(?!\\w))|(^CHAR(?<=\\w)(?!\\w))|(^CLOB(?<=\\w)(?!\\w))|(^DATE(?<=\\w)(?!\\w))|(^DECIMAL(?<=\\w)(?!\\w))|(^DOUBLE(?<=\\w)(?!\\w))|(^FLOAT(?<=\\w)(?!\\w))|(^GRAPHIC(?<=\\w)(?!\\w))|(^INTEGER(?<=\\w)(?!\\w))|(^INTERVAL DAY(?<=\\w)(?!\\w))|(^INTERVAL DAY TO HOUR(?<=\\w)(?!\\w))|(^INTERVAL DAY TO MINUTE(?<=\\w)(?!\\w))|(^INTERVAL DAY TO SECOND(?<=\\w)(?!\\w))|(^INTERVAL HOUR(?<=\\w)(?!\\w))|(^INTERVAL HOUR TO MINUTE(?<=\\w)(?!\\w))|(^INTERVAL HOUR TO SECOND(?<=\\w)(?!\\w))|(^INTERVAL MINUTE(?<=\\w)(?!\\w))|(^INTERVAL MINUTE TO SECOND(?<=\\w)(?!\\w))|(^INTERVAL MONTH(?<=\\w)(?!\\w))|(^INTERVAL SECOND(?<=\\w)(?!\\w))|(^INTERVAL YEAR(?<=\\w)(?!\\w))|(^INTERVAL YEAR TO MONTH(?<=\\w)(?!\\w))|(^LONG VARCHAR(?<=\\w)(?!\\w))|(^LONG VARGRAPHIC(?<=\\w)(?!\\w))|(^NUMERIC(?<=\\w)(?!\\w))|(^REAL(?<=\\w)(?!\\w))|(^SMALLINT(?<=\\w)(?!\\w))|(^TIME(?<=\\w)(?!\\w))|(^TIMESTAMP(?<=\\w)(?!\\w)(?!.*WITH))|(^VARBYTE(?<=\\w)(?!\\w))|(^VARCHAR(?<=\\w)(?!\\w))|(^VARGRAPHIC(?<=\\w)(?!\\w))|(^UDT(?<=\\w)(?!\\w))|", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x04000043 RID: 67
		private static Encoding encoder = Encoding.Default;

		// Token: 0x0200008B RID: 139
		private sealed class TdSqlDbObjectName : SqlDsvGenerator.DbObjectName
		{
			// Token: 0x0600056E RID: 1390 RVA: 0x0001682F File Offset: 0x00014A2F
			internal TdSqlDbObjectName(string schemaName, string objectName)
				: base(schemaName, objectName)
			{
			}

			// Token: 0x0600056F RID: 1391 RVA: 0x0001683C File Offset: 0x00014A3C
			public override string ToString()
			{
				if (!string.IsNullOrEmpty(this.SchemaName))
				{
					return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}.{1}", new object[]
					{
						TdSqlBatch.GetDelimitedIdentifierStatic(this.SchemaName),
						TdSqlBatch.GetDelimitedIdentifierStatic(this.ObjectName)
					});
				}
				return TdSqlBatch.GetDelimitedIdentifierStatic(this.ObjectName);
			}
		}
	}
}
