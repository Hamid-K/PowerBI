using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.SemanticQueryEngine;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQL
{
	// Token: 0x0200000C RID: 12
	internal sealed class MsSqlDsvGenerator : SqlDsvGenerator
	{
		// Token: 0x06000090 RID: 144 RVA: 0x000045AE File Offset: 0x000027AE
		internal MsSqlDsvGenerator(IDbConnection connection)
			: base(connection)
		{
			if (!(connection is SqlConnection))
			{
				throw SQEAssert.AssertFalseAndThrow("Connection must be SqlConnection.", Array.Empty<object>());
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000045D0 File Offset: 0x000027D0
		internal static DsvCompareInfo GetCompareInfoStatic(IDbConnection connection)
		{
			using (IDbCommand dbCommand = connection.CreateCommand())
			{
				CultureInfo cultureInfo;
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				if (SqlGetCollationProperties.GetCollationProperties(dbCommand, ref cultureInfo, ref flag, ref flag2, ref flag3, ref flag4))
				{
					return new DsvCompareInfo
					{
						Culture = cultureInfo,
						IgnoreCase = !flag,
						IgnoreNonSpace = !flag2,
						IgnoreKanaType = !flag3,
						IgnoreWidth = !flag4
					};
				}
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004650 File Offset: 0x00002850
		protected override string GetDsvName()
		{
			return this.Connection.Database;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000465D File Offset: 0x0000285D
		protected override DsvCompareInfo GetCompareInfo()
		{
			return MsSqlDsvGenerator.GetCompareInfoStatic(this.Connection);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000466A File Offset: 0x0000286A
		protected override SqlDsvGenerator.DbObjectName CreateDbObjectName(string schemaName, string objectName)
		{
			return new MsSqlDsvGenerator.MsSqlDbObjectName(schemaName, objectName);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004674 File Offset: 0x00002874
		protected override SqlDsvGenerator.DbObjectName[] GetTables()
		{
			DataTable schema = this.Connection.GetSchema("Tables", new string[] { null, null, null, "BASE TABLE" });
			SqlDsvGenerator.DbObjectName[] array = new SqlDsvGenerator.DbObjectName[schema.Rows.Count];
			for (int i = 0; i < schema.Rows.Count; i++)
			{
				DataRow dataRow = schema.Rows[i];
				array[i] = new MsSqlDsvGenerator.MsSqlDbObjectName((string)dataRow["TABLE_SCHEMA"], (string)dataRow["TABLE_NAME"]);
			}
			return array;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000046FD File Offset: 0x000028FD
		protected override IEnumerable<SqlDsvGenerator.ColExtendedInfo> GetColExtInfo()
		{
			using (SqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT\r\n        c.TABLE_SCHEMA, \r\n        c.TABLE_NAME, \r\n        c.COLUMN_NAME, \r\n        c.DATA_TYPE, \r\n        c.CHARACTER_MAXIMUM_LENGTH \r\nFROM INFORMATION_SCHEMA.COLUMNS c\r\n     INNER JOIN INFORMATION_SCHEMA.TABLES t ON c.TABLE_SCHEMA = t.TABLE_SCHEMA AND \r\n                                               c.TABLE_NAME = t.TABLE_NAME AND\r\n                                               t.TABLE_TYPE = 'BASE TABLE'\r\nORDER BY c.TABLE_SCHEMA, c.TABLE_NAME";
				cmd.CommandType = CommandType.Text;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int columnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
					int dataTypeOrdinal = reader.GetOrdinal("DATA_TYPE");
					int lengthOrdinal = reader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
					while (reader.Read())
					{
						string @string = reader.GetString(tableNameOrdinal);
						string string2 = reader.GetString(columnNameOrdinal);
						string text = null;
						if (!reader.IsDBNull(lengthOrdinal))
						{
							try
							{
								text = Convert.ToString(reader.GetValue(lengthOrdinal), CultureInfo.InvariantCulture);
							}
							catch (Exception ex)
							{
								base.TraceWarning("Failed to parse length of column {0} in the table {1} : {2}", new object[]
								{
									string2,
									@string,
									ex.ToString()
								});
							}
						}
						yield return new SqlDsvGenerator.ColExtendedInfo(reader.GetString(tableSchemaOrdinal), @string, string2, reader.GetString(dataTypeOrdinal), text);
					}
				}
				SqlDataReader reader = null;
			}
			SqlCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000470D File Offset: 0x0000290D
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetPKInfo()
		{
			using (SqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT \r\n    TABLE_SCHEMA,\r\n    TABLE_NAME,\r\n    CONSTRAINT_NAME\r\nFROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS \r\nWHERE CONSTRAINT_SCHEMA = TABLE_SCHEMA AND\r\n      CONSTRAINT_TYPE = 'PRIMARY KEY'";
				cmd.CommandType = CommandType.Text;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int constraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.UniqueConstraintInfo(reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal), reader.GetString(constraintNameOrdinal));
					}
				}
				SqlDataReader reader = null;
			}
			SqlCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000471D File Offset: 0x0000291D
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetUCInfo()
		{
			using (SqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT \r\n    TABLE_SCHEMA,\r\n    TABLE_NAME,\r\n    CONSTRAINT_NAME\r\nFROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS \r\nWHERE CONSTRAINT_SCHEMA = TABLE_SCHEMA AND\r\n      CONSTRAINT_TYPE = 'UNIQUE'";
				cmd.CommandType = CommandType.Text;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int constraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.UniqueConstraintInfo(reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal), reader.GetString(constraintNameOrdinal));
					}
				}
				SqlDataReader reader = null;
			}
			SqlCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000472D File Offset: 0x0000292D
		protected override IEnumerable<SqlDsvGenerator.FKConstraintInfo> GetFKInfo()
		{
			using (SqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT\r\n    fkctu.TABLE_SCHEMA        FK_TABLE_SCHEMA,\r\n    fkctu.TABLE_NAME          FK_TABLE_NAME,\r\n    rc.CONSTRAINT_NAME        FK_CONSTRAINT_NAME,\r\n    pkctu.TABLE_SCHEMA        PK_TABLE_SCHEMA,\r\n    pkctu.TABLE_NAME          PK_TABLE_NAME,\r\n    rc.UNIQUE_CONSTRAINT_NAME PK_CONSTRAINT_NAME\r\nFROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc\r\n     INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE fkctu ON rc.CONSTRAINT_SCHEMA =  fkctu.CONSTRAINT_SCHEMA AND\r\n                                                                   rc.CONSTRAINT_NAME = fkctu.CONSTRAINT_NAME AND\r\n                                                                   rc.CONSTRAINT_SCHEMA = fkctu.TABLE_SCHEMA\r\n     INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE pkctu ON rc.UNIQUE_CONSTRAINT_SCHEMA = pkctu.CONSTRAINT_SCHEMA AND\r\n                                                                   rc.UNIQUE_CONSTRAINT_NAME = pkctu.CONSTRAINT_NAME AND\r\n                                                                   rc.UNIQUE_CONSTRAINT_SCHEMA = pkctu.TABLE_SCHEMA\r\nWHERE \r\n      rc.MATCH_OPTION IN ('NONE', 'SIMPLE')";
				cmd.CommandType = CommandType.Text;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int fkTableSchemaOrdinal = reader.GetOrdinal("FK_TABLE_SCHEMA");
					int fkTableNameOrdinal = reader.GetOrdinal("FK_TABLE_NAME");
					int fkConstraintNameOrdinal = reader.GetOrdinal("FK_CONSTRAINT_NAME");
					int pkTableSchemaOrdinal = reader.GetOrdinal("PK_TABLE_SCHEMA");
					int pkTableNameOrdinal = reader.GetOrdinal("PK_TABLE_NAME");
					int pkConstraintNameOrdinal = reader.GetOrdinal("PK_CONSTRAINT_NAME");
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.FKConstraintInfo(reader.GetString(fkTableSchemaOrdinal), reader.GetString(fkTableNameOrdinal), reader.GetString(fkConstraintNameOrdinal), reader.GetString(pkTableSchemaOrdinal), reader.GetString(pkTableNameOrdinal), reader.GetString(pkConstraintNameOrdinal));
					}
				}
				SqlDataReader reader = null;
			}
			SqlCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000473D File Offset: 0x0000293D
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetPrimaryUniqueKeyColumnsInfo()
		{
			using (SqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT \r\n    tc.TABLE_SCHEMA,\r\n    tc.TABLE_NAME,\r\n    tc.CONSTRAINT_NAME,\r\n    kcu.COLUMN_NAME\r\nFROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc\r\n     INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON tc.TABLE_SCHEMA = kcu.TABLE_SCHEMA AND\r\n                                                           tc.TABLE_NAME = kcu.TABLE_NAME AND\r\n                                                           tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME\r\nWHERE tc.CONSTRAINT_SCHEMA = tc.TABLE_SCHEMA AND\r\n      tc.CONSTRAINT_TYPE IN ('PRIMARY KEY', 'UNIQUE')\r\nORDER BY tc.TABLE_SCHEMA, tc.TABLE_NAME, tc.CONSTRAINT_NAME, kcu.ORDINAL_POSITION";
				cmd.CommandType = CommandType.Text;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int constraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
					int columnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.ConstraintColumnInfo(reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal), reader.GetString(constraintNameOrdinal), reader.GetString(columnNameOrdinal));
					}
				}
				SqlDataReader reader = null;
			}
			SqlCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000474D File Offset: 0x0000294D
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetForeignKeyColumnsInfo()
		{
			using (SqlCommand cmd = this.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT\r\n    fkctu.TABLE_SCHEMA,\r\n    fkctu.TABLE_NAME,\r\n    rc.CONSTRAINT_NAME,\r\n    kcu.COLUMN_NAME\r\nFROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc\r\n     INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE fkctu ON rc.CONSTRAINT_SCHEMA =  fkctu.CONSTRAINT_SCHEMA AND\r\n                                                                   rc.CONSTRAINT_NAME = fkctu.CONSTRAINT_NAME AND\r\n                                                                   rc.CONSTRAINT_SCHEMA = fkctu.TABLE_SCHEMA\r\n     INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON fkctu.TABLE_SCHEMA = kcu.TABLE_SCHEMA AND\r\n                                                           fkctu.TABLE_NAME = kcu.TABLE_NAME AND\r\n                                                           rc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME\r\nWHERE \r\n      rc.MATCH_OPTION IN ('NONE', 'SIMPLE')\r\nORDER BY fkctu.TABLE_SCHEMA, fkctu.TABLE_NAME, rc.CONSTRAINT_NAME, kcu.ORDINAL_POSITION";
				cmd.CommandType = CommandType.Text;
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					int tableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
					int tableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
					int constraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
					int columnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.ConstraintColumnInfo(reader.GetString(tableSchemaOrdinal), reader.GetString(tableNameOrdinal), reader.GetString(constraintNameOrdinal), reader.GetString(columnNameOrdinal));
					}
				}
				SqlDataReader reader = null;
			}
			SqlCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000475D File Offset: 0x0000295D
		private new SqlConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return (SqlConnection)base.Connection;
			}
		}

		// Token: 0x020000A2 RID: 162
		private sealed class MsSqlDbObjectName : SqlDsvGenerator.DbObjectName
		{
			// Token: 0x0600062B RID: 1579 RVA: 0x0001682F File Offset: 0x00014A2F
			internal MsSqlDbObjectName(string schemaName, string objectName)
				: base(schemaName, objectName)
			{
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x000194D8 File Offset: 0x000176D8
			public override string ToString()
			{
				if (!string.IsNullOrEmpty(this.SchemaName))
				{
					return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}.{1}", new object[]
					{
						MsSqlBatch.GetDelimitedIdentifierStatic(this.SchemaName),
						MsSqlBatch.GetDelimitedIdentifierStatic(this.ObjectName)
					});
				}
				return MsSqlBatch.GetDelimitedIdentifierStatic(this.ObjectName);
			}
		}
	}
}
