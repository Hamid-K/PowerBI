using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration.MSSQLADW
{
	// Token: 0x0200000A RID: 10
	internal sealed class MsSqlAdwDsvGenerator : SqlDsvGenerator
	{
		// Token: 0x0600007C RID: 124 RVA: 0x000042D0 File Offset: 0x000024D0
		internal MsSqlAdwDsvGenerator(IDbConnection connection)
			: base(connection)
		{
			if (MsSqlAdwDsvGenerator.m_ConnectionType == null)
			{
				object staticLock = MsSqlAdwDsvGenerator.StaticLock;
				lock (staticLock)
				{
					MsSqlAdwDsvGenerator.m_ConnectionType = Microsoft.ReportingServices.Common.DataExtensionsHelper.GetDataExtensionConnectionType("SqlDwConnectionWrapper", "GetDwConnectionType");
				}
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004334 File Offset: 0x00002534
		internal static DsvCompareInfo GetCompareInfoStatic(IDbConnection connection)
		{
			return new DsvCompareInfo
			{
				Culture = CultureInfo.GetCultureInfo("en-US"),
				IgnoreCase = true,
				IgnoreKanaType = true,
				IgnoreNonSpace = true,
				IgnoreWidth = true
			};
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004367 File Offset: 0x00002567
		protected override string GetDsvName()
		{
			return base.Connection.Database;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004374 File Offset: 0x00002574
		protected override DsvCompareInfo GetCompareInfo()
		{
			return MsSqlAdwDsvGenerator.GetCompareInfoStatic(base.Connection);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004381 File Offset: 0x00002581
		protected override SqlDsvGenerator.DbObjectName CreateDbObjectName(string schemaName, string objectName)
		{
			return new MsSqlAdwDsvGenerator.MsSqlAdwDbObjectName(schemaName, objectName);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000438C File Offset: 0x0000258C
		protected override SqlDsvGenerator.DbObjectName[] GetTables()
		{
			List<SqlDsvGenerator.DbObjectName> list = new List<SqlDsvGenerator.DbObjectName>();
			using (IDbCommand dbCommand = base.Connection.CreateCommand())
			{
				dbCommand.CommandText = "SELECT TABLE_NAME, TABLE_SCHEMA FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG='" + base.Connection.Database + "'";
				dbCommand.CommandType = CommandType.Text;
				using (IDataReader dataReader = dbCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						list.Add(new MsSqlAdwDsvGenerator.MsSqlAdwDbObjectName((string)dataReader["TABLE_SCHEMA"], (string)dataReader["TABLE_NAME"]));
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004448 File Offset: 0x00002648
		protected override IEnumerable<SqlDsvGenerator.ColExtendedInfo> GetColExtInfo()
		{
			using (IDbCommand cmd = base.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT\r\n        c.TABLE_SCHEMA, \r\n        c.TABLE_NAME, \r\n        c.COLUMN_NAME, \r\n        c.DATA_TYPE, \r\n        c.CHARACTER_MAXIMUM_LENGTH \r\nFROM INFORMATION_SCHEMA.COLUMNS c\r\n     INNER JOIN INFORMATION_SCHEMA.TABLES t ON c.TABLE_SCHEMA = t.TABLE_SCHEMA AND \r\n                                               c.TABLE_NAME = t.TABLE_NAME AND\r\n                                               t.TABLE_TYPE = 'BASE TABLE'\r\nORDER BY c.TABLE_SCHEMA, c.TABLE_NAME";
				cmd.CommandType = CommandType.Text;
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string text = (string)reader["TABLE_NAME"];
						string text2 = (string)reader["COLUMN_NAME"];
						string text3 = null;
						if (reader["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
						{
							try
							{
								text3 = Convert.ToString(reader["CHARACTER_MAXIMUM_LENGTH"], CultureInfo.InvariantCulture);
							}
							catch (Exception ex)
							{
								base.TraceWarning("Failed to parse length of column {0} in the table {1} : {2}", new object[]
								{
									text2,
									text,
									ex.ToString()
								});
							}
						}
						yield return new SqlDsvGenerator.ColExtendedInfo((string)reader["TABLE_SCHEMA"], text, text2, (string)reader["DATA_TYPE"], text3);
					}
				}
				IDataReader reader = null;
			}
			IDbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004458 File Offset: 0x00002658
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetPKInfo()
		{
			using (IDbCommand cmd = base.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT \r\n    TABLE_SCHEMA,\r\n    TABLE_NAME,\r\n    CONSTRAINT_NAME\r\nFROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS \r\nWHERE CONSTRAINT_SCHEMA = TABLE_SCHEMA AND\r\n      CONSTRAINT_TYPE = 'PRIMARY KEY'";
				cmd.CommandType = CommandType.Text;
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.UniqueConstraintInfo((string)reader["TABLE_SCHEMA"], (string)reader["TABLE_NAME"], (string)reader["CONSTRAINT_NAME"]);
					}
				}
				IDataReader reader = null;
			}
			IDbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004468 File Offset: 0x00002668
		protected override IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetUCInfo()
		{
			using (IDbCommand cmd = base.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT \r\n    TABLE_SCHEMA,\r\n    TABLE_NAME,\r\n    CONSTRAINT_NAME\r\nFROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS \r\nWHERE CONSTRAINT_SCHEMA = TABLE_SCHEMA AND\r\n      CONSTRAINT_TYPE = 'UNIQUE'";
				cmd.CommandType = CommandType.Text;
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.UniqueConstraintInfo((string)reader["TABLE_SCHEMA"], (string)reader["TABLE_NAME"], (string)reader["CONSTRAINT_NAME"]);
					}
				}
				IDataReader reader = null;
			}
			IDbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004478 File Offset: 0x00002678
		protected override IEnumerable<SqlDsvGenerator.FKConstraintInfo> GetFKInfo()
		{
			using (IDbCommand cmd = base.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT\r\n    fkctu.TABLE_SCHEMA        FK_TABLE_SCHEMA,\r\n    fkctu.TABLE_NAME          FK_TABLE_NAME,\r\n    rc.CONSTRAINT_NAME        FK_CONSTRAINT_NAME,\r\n    pkctu.TABLE_SCHEMA        PK_TABLE_SCHEMA,\r\n    pkctu.TABLE_NAME          PK_TABLE_NAME,\r\n    rc.UNIQUE_CONSTRAINT_NAME PK_CONSTRAINT_NAME\r\nFROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc\r\n     INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE fkctu ON rc.CONSTRAINT_SCHEMA =  fkctu.CONSTRAINT_SCHEMA AND\r\n                                                                   rc.CONSTRAINT_NAME = fkctu.CONSTRAINT_NAME AND\r\n                                                                   rc.CONSTRAINT_SCHEMA = fkctu.TABLE_SCHEMA\r\n     INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE pkctu ON rc.UNIQUE_CONSTRAINT_SCHEMA = pkctu.CONSTRAINT_SCHEMA AND\r\n                                                                   rc.UNIQUE_CONSTRAINT_NAME = pkctu.CONSTRAINT_NAME AND\r\n                                                                   rc.UNIQUE_CONSTRAINT_SCHEMA = pkctu.TABLE_SCHEMA\r\nWHERE \r\n      rc.MATCH_OPTION IN ('NONE', 'SIMPLE')";
				cmd.CommandType = CommandType.Text;
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.FKConstraintInfo((string)reader["FK_TABLE_SCHEMA"], (string)reader["FK_TABLE_NAME"], (string)reader["FK_CONSTRAINT_NAME"], (string)reader["PK_TABLE_SCHEMA"], (string)reader["PK_TABLE_NAME"], (string)reader["PK_CONSTRAINT_NAME"]);
					}
				}
				IDataReader reader = null;
			}
			IDbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004488 File Offset: 0x00002688
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetPrimaryUniqueKeyColumnsInfo()
		{
			using (IDbCommand cmd = base.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT \r\n    tc.TABLE_SCHEMA,\r\n    tc.TABLE_NAME,\r\n    tc.CONSTRAINT_NAME,\r\n    kcu.COLUMN_NAME,\r\n    kcu.ORDINAL_POSITION\r\nFROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc\r\n     INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON tc.TABLE_SCHEMA = kcu.TABLE_SCHEMA AND\r\n                                                           tc.TABLE_NAME = kcu.TABLE_NAME AND\r\n                                                           tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME\r\nWHERE tc.CONSTRAINT_SCHEMA = tc.TABLE_SCHEMA AND\r\n      tc.CONSTRAINT_TYPE IN ('PRIMARY KEY', 'UNIQUE')\r\nORDER BY tc.TABLE_SCHEMA, tc.TABLE_NAME, tc.CONSTRAINT_NAME, kcu.ORDINAL_POSITION";
				cmd.CommandType = CommandType.Text;
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.ConstraintColumnInfo((string)reader["TABLE_SCHEMA"], (string)reader["TABLE_NAME"], (string)reader["CONSTRAINT_NAME"], (string)reader["COLUMN_NAME"]);
					}
				}
				IDataReader reader = null;
			}
			IDbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004498 File Offset: 0x00002698
		protected override IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetForeignKeyColumnsInfo()
		{
			using (IDbCommand cmd = base.Connection.CreateCommand())
			{
				cmd.CommandText = "SELECT\r\n    fkctu.TABLE_SCHEMA,\r\n    fkctu.TABLE_NAME,\r\n    rc.CONSTRAINT_NAME,\r\n    kcu.COLUMN_NAME,\r\n    kcu.ORDINAL_POSITION\r\nFROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS rc\r\n     INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE fkctu ON rc.CONSTRAINT_SCHEMA =  fkctu.CONSTRAINT_SCHEMA AND\r\n                                                                   rc.CONSTRAINT_NAME = fkctu.CONSTRAINT_NAME AND\r\n                                                                   rc.CONSTRAINT_SCHEMA = fkctu.TABLE_SCHEMA\r\n     INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON fkctu.TABLE_SCHEMA = kcu.TABLE_SCHEMA AND\r\n                                                           fkctu.TABLE_NAME = kcu.TABLE_NAME AND\r\n                                                           rc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME\r\nWHERE \r\n      rc.MATCH_OPTION IN ('NONE', 'SIMPLE')\r\nORDER BY fkctu.TABLE_SCHEMA, fkctu.TABLE_NAME, rc.CONSTRAINT_NAME, kcu.ORDINAL_POSITION";
				cmd.CommandType = CommandType.Text;
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						yield return new SqlDsvGenerator.ConstraintColumnInfo((string)reader["TABLE_SCHEMA"], (string)reader["TABLE_NAME"], (string)reader["CONSTRAINT_NAME"], (string)reader["COLUMN_NAME"]);
					}
				}
				IDataReader reader = null;
			}
			IDbCommand cmd = null;
			yield break;
			yield break;
		}

		// Token: 0x04000046 RID: 70
		private static Type m_ConnectionType = null;

		// Token: 0x04000047 RID: 71
		private static object StaticLock = new object();

		// Token: 0x0200009B RID: 155
		private sealed class MsSqlAdwDbObjectName : SqlDsvGenerator.DbObjectName
		{
			// Token: 0x060005ED RID: 1517 RVA: 0x0001682F File Offset: 0x00014A2F
			internal MsSqlAdwDbObjectName(string schemaName, string objectName)
				: base(schemaName, objectName)
			{
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x00018618 File Offset: 0x00016818
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
