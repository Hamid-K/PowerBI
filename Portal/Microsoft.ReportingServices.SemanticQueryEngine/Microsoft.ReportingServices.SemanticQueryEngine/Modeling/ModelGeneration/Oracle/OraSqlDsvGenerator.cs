using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.SemanticQueryEngine;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.Oracle
{
	// Token: 0x02000008 RID: 8
	internal sealed class OraSqlDsvGenerator : SqlDsvGenerator
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003C75 File Offset: 0x00001E75
		internal OraSqlDsvGenerator(IDbConnection connection)
			: base(connection)
		{
			if (!(connection is OracleConnection))
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be OracleConnection.", Array.Empty<object>());
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000334E File Offset: 0x0000154E
		internal static DsvCompareInfo GetCompareInfoStatic(IDbConnection connection)
		{
			return null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003C98 File Offset: 0x00001E98
		protected override string GetDsvName()
		{
			using (IDbCommand dbCommand = this.Connection.CreateCommand())
			{
				dbCommand.CommandText = "SELECT USER FROM DUAL";
				this.m_owner = (string)dbCommand.ExecuteScalar();
			}
			return this.m_owner;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003CF0 File Offset: 0x00001EF0
		protected override DsvCompareInfo GetCompareInfo()
		{
			return OraSqlDsvGenerator.GetCompareInfoStatic(this.Connection);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003CFD File Offset: 0x00001EFD
		protected override SqlDsvGenerator.DbObjectName CreateDbObjectName(string schemaName, string objectName)
		{
			return new OraSqlDsvGenerator.OraSqlDbObjectName(schemaName, objectName);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003D08 File Offset: 0x00001F08
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
				if (OraSqlDsvGenerator.SupportedTypesDetector.Match(keyValuePair.Value.DbTypeName ?? "").Success)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(OraSqlBatch.GetDelimitedIdentifierStatic(keyValuePair.Key));
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

		// Token: 0x0600005E RID: 94 RVA: 0x00003E24 File Offset: 0x00002024
		protected override SqlDsvGenerator.DbObjectName[] GetTables()
		{
			DataTable schema = this.Connection.GetSchema("Tables", new string[] { this.m_owner });
			SqlDsvGenerator.DbObjectName[] array = new SqlDsvGenerator.DbObjectName[schema.Rows.Count];
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				DataRow dataRow = schema.Rows[i];
				array[i] = new OraSqlDsvGenerator.OraSqlDbObjectName((string)dataRow["OWNER"], (string)dataRow["TABLE_NAME"]);
			}
			return array;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003EAE File Offset: 0x000020AE
		protected override IEnumerable<SqlDsvGenerator.ColExtendedInfo> GetColExtInfo()
		{
			DataTable schema = this.Connection.GetSchema("Columns", new string[] { this.m_owner });
			foreach (DataRow dataRow in schema.Select(null, "OWNER, TABLE_NAME"))
			{
				string text = (string)dataRow["OWNER"];
				string text2 = (string)dataRow["TABLE_NAME"];
				string text3 = (string)dataRow["COLUMN_NAME"];
				string text4 = (string)dataRow["DATATYPE"];
				string text5 = null;
				try
				{
					text5 = Convert.ToString(dataRow["LENGTH"], CultureInfo.InvariantCulture);
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

		// Token: 0x06000060 RID: 96 RVA: 0x00003EBE File Offset: 0x000020BE
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetPKInfo()
		{
			DataTable schema = this.Connection.GetSchema("PrimaryKeys", new string[] { this.m_owner });
			foreach (object obj in schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				yield return new SqlDsvGenerator.UniqueConstraintInfo((string)dataRow["OWNER"], (string)dataRow["TABLE_NAME"], (string)dataRow["CONSTRAINT_NAME"]);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003ECE File Offset: 0x000020CE
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetUCInfo()
		{
			DataTable schema = this.Connection.GetSchema("UniqueKeys", new string[] { this.m_owner });
			foreach (object obj in schema.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				yield return new SqlDsvGenerator.UniqueConstraintInfo((string)dataRow["OWNER"], (string)dataRow["TABLE_NAME"], (string)dataRow["CONSTRAINT_NAME"]);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003EDE File Offset: 0x000020DE
		protected override IEnumerable<SqlDsvGenerator.FKConstraintInfo> GetFKInfo()
		{
			DataTable schema = this.Connection.GetSchema("ForeignKeys", new string[] { this.m_owner });
			foreach (DataRow dataRow in schema.Select("FOREIGN_KEY_OWNER = PRIMARY_KEY_OWNER"))
			{
				yield return new SqlDsvGenerator.FKConstraintInfo((string)dataRow["FOREIGN_KEY_OWNER"], (string)dataRow["FOREIGN_KEY_TABLE_NAME"], (string)dataRow["FOREIGN_KEY_CONSTRAINT_NAME"], (string)dataRow["PRIMARY_KEY_OWNER"], (string)dataRow["PRIMARY_KEY_TABLE_NAME"], (string)dataRow["PRIMARY_KEY_CONSTRAINT_NAME"]);
			}
			DataRow[] array = null;
			yield break;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003EEE File Offset: 0x000020EE
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetPrimaryUniqueKeyColumnsInfo()
		{
			DataTable schema = this.Connection.GetSchema("IndexColumns", new string[] { this.m_owner, null, this.m_owner });
			foreach (DataRow dataRow in schema.Select("TABLE_OWNER = INDEX_OWNER", "TABLE_OWNER, TABLE_NAME, INDEX_OWNER, INDEX_NAME, COLUMN_POSITION"))
			{
				yield return new SqlDsvGenerator.ConstraintColumnInfo((string)dataRow["TABLE_OWNER"], (string)dataRow["TABLE_NAME"], (string)dataRow["INDEX_NAME"], (string)dataRow["COLUMN_NAME"]);
			}
			DataRow[] array = null;
			yield break;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003EFE File Offset: 0x000020FE
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetForeignKeyColumnsInfo()
		{
			DataTable schema = this.Connection.GetSchema("ForeignKeyColumns", new string[] { this.m_owner });
			foreach (DataRow dataRow in schema.Select(null, "OWNER, TABLE_NAME, CONSTRAINT_NAME, POSITION"))
			{
				yield return new SqlDsvGenerator.ConstraintColumnInfo((string)dataRow["OWNER"], (string)dataRow["TABLE_NAME"], (string)dataRow["CONSTRAINT_NAME"], (string)dataRow["COLUMN_NAME"]);
			}
			DataRow[] array = null;
			yield break;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003F0E File Offset: 0x0000210E
		private new OracleConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return (OracleConnection)base.Connection;
			}
		}

		// Token: 0x04000044 RID: 68
		private string m_owner;

		// Token: 0x04000045 RID: 69
		private static readonly Regex SupportedTypesDetector = new Regex("(^VARCHAR2(?<=\\w)(?!\\w))|(^NVARCHAR2(?<=\\w)(?!\\w))|(^CHAR(?<=\\w)(?!\\w))|(^NCHAR(?<=\\w)(?!\\w))|(^NUMBER(?<=\\w)(?!\\w))|(^FLOAT(?<=\\w)(?!\\w))|(^LONG(?<=\\w)(?!\\w))|(^CLOB(?<=\\w)(?!\\w))|(^NCLOB(?<=\\w)(?!\\w))|(^DATE(?<=\\w)(?!\\w))|(^TIMESTAMP(?<=\\w)(?!\\w)(?!.*WITH))|(^RAW(?<=\\w)(?!\\w))|(^LONG RAW(?<=\\w)(?!\\w))|(^BLOB(?<=\\w)(?!\\w))", RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x02000093 RID: 147
		private sealed class OraSqlDbObjectName : SqlDsvGenerator.DbObjectName
		{
			// Token: 0x060005B0 RID: 1456 RVA: 0x0001682F File Offset: 0x00014A2F
			internal OraSqlDbObjectName(string schemaName, string objectName)
				: base(schemaName, objectName)
			{
			}

			// Token: 0x060005B1 RID: 1457 RVA: 0x000179A0 File Offset: 0x00015BA0
			public override string ToString()
			{
				if (!string.IsNullOrEmpty(this.SchemaName))
				{
					return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}.{1}", new object[]
					{
						OraSqlBatch.GetDelimitedIdentifierStatic(this.SchemaName),
						OraSqlBatch.GetDelimitedIdentifierStatic(this.ObjectName)
					});
				}
				return OraSqlBatch.GetDelimitedIdentifierStatic(this.ObjectName);
			}
		}
	}
}
