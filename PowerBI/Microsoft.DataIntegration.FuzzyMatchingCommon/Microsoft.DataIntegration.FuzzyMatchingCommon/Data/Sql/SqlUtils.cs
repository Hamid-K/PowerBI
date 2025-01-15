using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using Microsoft.SqlServer.Server;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data.Sql
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public static class SqlUtils
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0001678E File Offset: 0x0001498E
		public static int MajorVersion(DbConnection cxn)
		{
			return int.Parse(cxn.ServerVersion.Substring(0, cxn.ServerVersion.IndexOf('.')));
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000167AE File Offset: 0x000149AE
		public static bool IsContextConnection(SqlConnection connection)
		{
			return SqlUtils.IsContextConnectionString(connection.ConnectionString);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000167BB File Offset: 0x000149BB
		public static bool IsContextConnectionString(string connectionString)
		{
			connectionString = connectionString.ToLower();
			return connectionString.Contains("context connection = true") || connectionString.Contains("context connection=true");
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000167DF File Offset: 0x000149DF
		public static void AddParameterWithValue(DbCommand cmd, string parameterName, object parameterValue)
		{
			DbParameter dbParameter = cmd.CreateParameter();
			dbParameter.ParameterName = parameterName;
			dbParameter.Value = parameterValue;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000167F4 File Offset: 0x000149F4
		public static SqlCommand CreateSqlCommand(SqlConnection connection)
		{
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandTimeout = connection.ConnectionTimeout;
			return sqlCommand;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00016808 File Offset: 0x00014A08
		public static DbCommand CreateDbCommand(DbConnection connection)
		{
			DbCommand dbCommand = connection.CreateCommand();
			dbCommand.CommandTimeout = connection.ConnectionTimeout;
			return dbCommand;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001681C File Offset: 0x00014A1C
		public static OleDbCommand CreateOleDbCommand(OleDbConnection connection)
		{
			OleDbCommand oleDbCommand = connection.CreateCommand();
			oleDbCommand.CommandTimeout = connection.ConnectionTimeout;
			return oleDbCommand;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00016830 File Offset: 0x00014A30
		public static void GetPrimaryKey(DbConnection cxn, SqlName tableName, out string[] keyColumnNames)
		{
			List<string> list = new List<string>();
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandText = string.Format("select kcu.COLUMN_NAME\r\n                      from {0}.{1}.INFORMATION_SCHEMA.TABLE_CONSTRAINTS as tc\r\n                      join {0}.{1}.INFORMATION_SCHEMA.KEY_COLUMN_USAGE as kcu\r\n                        on kcu.CONSTRAINT_SCHEMA = tc.CONSTRAINT_SCHEMA\r\n                       and kcu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME\r\n                       and kcu.TABLE_SCHEMA = tc.TABLE_SCHEMA\r\n                       and kcu.TABLE_NAME = tc.TABLE_NAME\r\n                     where tc.CONSTRAINT_TYPE = 'PRIMARY KEY'\r\n                       and tc.TABLE_SCHEMA=@schemaName\r\n                       and tc.TABLE_NAME=@tableName\r\n                     order by kcu.ORDINAL_POSITION asc", tableName.GetDelimitedPart(SqlName.Part.Server), tableName.GetDelimitedPart(SqlName.Part.Database));
				SqlUtils.AddParameterWithValue(dbCommand, "@schemaName", tableName.GetExplicitSchema());
				SqlUtils.AddParameterWithValue(dbCommand, "@tableName", tableName.GetPart(SqlName.Part.Table));
				using (IDataReader dataReader = dbCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						list.Add((string)dataReader[0]);
					}
				}
			}
			keyColumnNames = list.ToArray();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000168E8 File Offset: 0x00014AE8
		public static void GetIdentityColumns(DbConnection cxn, SqlName tableName, out string[] columnNames)
		{
			List<string> list = new List<string>();
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandText = string.Format("SELECT COLUMN_NAME\r\n                                    FROM {0}.{1}.INFORMATION_SCHEMA.COLUMNS\r\n                                    WHERE TABLE_SCHEMA=@schemaName AND TABLE_NAME=@tableName\r\n                                    AND COLUMNPROPERTY(OBJECT_ID({2}), COLUMN_NAME, 'IsIdentity') = 1\r\n                                   ", tableName.GetDelimitedPart(SqlName.Part.Server), tableName.GetDelimitedPart(SqlName.Part.Database), SqlName.CreateStringLiteral(tableName.QualifiedName));
				SqlUtils.AddParameterWithValue(dbCommand, "@schemaName", tableName.GetExplicitSchema());
				SqlUtils.AddParameterWithValue(dbCommand, "@tableName", tableName.GetPart(SqlName.Part.Table));
				using (IDataReader dataReader = dbCommand.ExecuteReader())
				{
					while (dataReader.Read())
					{
						list.Add((string)dataReader[0]);
					}
				}
			}
			columnNames = list.ToArray();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000169AC File Offset: 0x00014BAC
		public static bool IsTable(string objectType)
		{
			return objectType.Equals("U") || objectType.Equals("V") || objectType.Equals("S") || objectType.Equals("FT") || objectType.Equals("IF") || objectType.Equals("TF") || objectType.Equals("IT");
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00016A14 File Offset: 0x00014C14
		public static SqlName CreateUniqueIdentifier(DbConnection cxn, SqlName proposedName)
		{
			SqlName sqlName;
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				int num = 0;
				for (;;)
				{
					sqlName = new SqlName(proposedName);
					if (num > 0)
					{
						sqlName.Table = string.Format("{0} ({1})", sqlName.Table, num);
					}
					dbCommand.CommandText = string.Format("select object_id({0})", SqlName.CreateStringLiteral(sqlName.QualifiedName));
					if (DBNull.Value == dbCommand.ExecuteScalar())
					{
						break;
					}
					num++;
				}
			}
			return sqlName;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00016A9C File Offset: 0x00014C9C
		public static bool GetSchemaId(DbConnection cxn, SqlName tableName, out int schemaId)
		{
			schemaId = -1;
			bool flag = false;
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				if (SqlUtils.MajorVersion(cxn) > 8)
				{
					dbCommand.CommandText = string.Format("SELECT schema_id FROM {0}.{1}.sys.schemas WHERE name={2}", SqlName.DelimitElement(tableName.Server), SqlName.DelimitElement(tableName.Database), SqlName.CreateStringLiteral(tableName.GetExplicitSchema()));
					object obj = dbCommand.ExecuteScalar();
					if (obj != null)
					{
						schemaId = (int)obj;
						flag = true;
					}
				}
				else
				{
					dbCommand.CommandText = string.Format("SELECT uid FROM {0}.{1}.dbo.sysusers WHERE name={2}", SqlName.DelimitElement(tableName.Server), SqlName.DelimitElement(tableName.Database), SqlName.CreateStringLiteral(tableName.GetExplicitSchema()));
					object obj2 = dbCommand.ExecuteScalar();
					if (obj2 != null)
					{
						schemaId = (int)((short)obj2);
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00016B68 File Offset: 0x00014D68
		public static bool TryGetObjectIds(DbConnection cxn, SqlName objectName, out int objectId, out int schemaId, out string objectType)
		{
			bool flag = false;
			schemaId = 0;
			objectId = 0;
			objectType = string.Empty;
			if (objectName.IsValid && SqlUtils.GetSchemaId(cxn, objectName, out schemaId))
			{
				using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
				{
					if (objectName.Database.Equals("tempdb"))
					{
						dbCommand.CommandText = string.Format("SELECT object_id, type FROM {0}.{1}.sys.objects WHERE object_id=OBJECT_ID({2}) and schema_id={3}", new object[]
						{
							SqlName.DelimitElement(objectName.Server),
							SqlName.DelimitElement(objectName.Database),
							SqlName.CreateStringLiteral(objectName.QualifiedName),
							schemaId
						});
					}
					else if (SqlUtils.MajorVersion(cxn) > 8)
					{
						dbCommand.CommandText = string.Format("SELECT object_id, type FROM {0}.{1}.sys.objects WHERE name={2} and schema_id={3}", new object[]
						{
							SqlName.DelimitElement(objectName.Server),
							SqlName.DelimitElement(objectName.Database),
							SqlName.CreateStringLiteral(objectName.Table),
							schemaId
						});
					}
					else
					{
						dbCommand.CommandText = string.Format("SELECT object_id, type FROM {0}.dbo.sysobjects WHERE name={2} and uid={3}", new object[]
						{
							SqlName.DelimitElement(objectName.Server),
							SqlName.DelimitElement(objectName.Database),
							SqlName.CreateStringLiteral(objectName.Table),
							schemaId
						});
					}
					using (IDataReader dataReader = dbCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							objectId = (int)dataReader[0];
							objectType = ((string)dataReader[1]).Trim();
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00016D24 File Offset: 0x00014F24
		public static bool TableExists(DbConnection cxn, SqlName tableName)
		{
			int num;
			int num2;
			string text;
			return SqlUtils.TryGetObjectIds(cxn, tableName, out num, out num2, out text) && SqlUtils.IsTable(text);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00016D48 File Offset: 0x00014F48
		public static bool TryDropTable(DbConnection cxn, SqlName tableName)
		{
			if (SqlUtils.TableExists(cxn, tableName))
			{
				using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
				{
					dbCommand.CommandText = string.Format("DROP TABLE {0}", tableName.QualifiedName);
					dbCommand.ExecuteNonQuery();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public static bool TableHasIntegerIdentityPrimaryKey(DbConnection cxn, SqlName tableName, out string keyColumnName, out int keyColumnIndex)
		{
			bool flag = false;
			keyColumnName = string.Empty;
			keyColumnIndex = -1;
			int num;
			int num2;
			string text;
			if (SqlUtils.TryGetObjectIds(cxn, tableName, out num, out num2, out text) && text.Equals("U"))
			{
				using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
				{
					dbCommand.CommandText = string.Format("select t.name, c.name, k.colid \r\n                                        from dbo.sysindexkeys as k, dbo.syscolumns as c, dbo.systypes as t  \r\n                                        where k.id={0} \r\n                                        and k.id=c.id \r\n                                        and k.colid=c.colid \r\n                                        and c.xtype=t.xtype  \r\n                                        and k.indid=1", num);
					int num3 = 0;
					using (IDataReader dataReader = dbCommand.ExecuteReader())
					{
						if (dataReader.Read())
						{
							if (dataReader[0].ToString().CompareTo("int") == 0)
							{
								flag = true;
								keyColumnName = dataReader[1] as string;
								keyColumnIndex = (int)((short)dataReader[2] - 1);
							}
							num3++;
						}
						if (num3 > 1)
						{
							flag = false;
							keyColumnName = string.Empty;
							keyColumnIndex = -1;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00016E9C File Offset: 0x0001509C
		public static bool SchemaExists(DbConnection cxn, string unquotedSchemaName)
		{
			bool flag;
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				if (SqlUtils.MajorVersion(cxn) > 8)
				{
					dbCommand.CommandText = "SELECT COUNT(*) FROM sys.schemas WHERE name = @schemaName";
				}
				else
				{
					dbCommand.CommandText = "SELECT COUNT(*) FROM sysusers WHERE name = @schemaName";
				}
				DbParameter dbParameter = dbCommand.CreateParameter();
				dbParameter.ParameterName = "@schemaName";
				dbParameter.DbType = 16;
				dbParameter.Value = unquotedSchemaName;
				dbParameter.Size = unquotedSchemaName.Length;
				dbCommand.Parameters.Add(dbParameter);
				flag = (int)dbCommand.ExecuteScalar() != 0;
			}
			return flag;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00016F38 File Offset: 0x00015138
		public static void CreateSchema(DbConnection cxn, SqlName schemaName)
		{
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandText = "CREATE SCHEMA " + schemaName.QualifiedName;
				dbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00016F88 File Offset: 0x00015188
		public static void DropSchema(DbConnection cxn, SqlName schemaName)
		{
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandText = "DROP SCHEMA " + schemaName.QualifiedName;
				dbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00016FD8 File Offset: 0x000151D8
		public static void BulkWrite(SqlConnection cxn, SqlName dstTableName, IDataReader srcReader)
		{
			using (SqlCommand sqlCommand = SqlUtils.CreateSqlCommand(cxn))
			{
				sqlCommand.CommandText = "SET XACT_ABORT ON";
				sqlCommand.ExecuteNonQuery();
			}
			using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(cxn, 0, null))
			{
				sqlBulkCopy.BulkCopyTimeout = cxn.ConnectionTimeout;
				sqlBulkCopy.BatchSize = 100000;
				sqlBulkCopy.DestinationTableName = dstTableName.QualifiedName;
				sqlBulkCopy.WriteToServer(srcReader);
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00017068 File Offset: 0x00015268
		public static void BulkWrite(OleDbConnection cxn, SqlName dstTableName, IDataReader srcReader)
		{
			using (OleDbCommand oleDbCommand = cxn.CreateCommand())
			{
				oleDbCommand.CommandText = "select top 0 * from " + dstTableName.QualifiedName;
				using (OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(2))
				{
					DataTable schemaTable = oleDbDataReader.GetSchemaTable();
					using (OleDbCommand oleDbCommand2 = SqlUtils.CreateOleDbCommand(cxn))
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("INSERT INTO " + dstTableName.QualifiedName + " values (");
						bool flag = true;
						foreach (object obj in schemaTable.Rows)
						{
							DataRow dataRow = (DataRow)obj;
							if (flag)
							{
								stringBuilder.Append("?");
								flag = false;
							}
							else
							{
								stringBuilder.Append(", ?");
							}
							long num = ((dataRow[SchemaTableColumn.ColumnSize] is long) ? ((long)dataRow[SchemaTableColumn.ColumnSize]) : ((long)((dataRow[SchemaTableColumn.ColumnSize] is int) ? ((int)dataRow[SchemaTableColumn.ColumnSize]) : 0)));
							oleDbCommand2.Parameters.Add(string.Empty, (OleDbType)dataRow[SchemaTableColumn.ProviderType], (int)num);
						}
						stringBuilder.Append(")");
						oleDbCommand2.CommandText = stringBuilder.ToString();
						oleDbCommand2.Prepare();
						while (srcReader.Read())
						{
							for (int i = 0; i < srcReader.FieldCount; i++)
							{
								object value = srcReader.GetValue(i);
								oleDbCommand2.Parameters[i].Value = ((value == null) ? DBNull.Value : value);
							}
							oleDbCommand2.ExecuteNonQuery();
						}
					}
				}
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000172A0 File Offset: 0x000154A0
		public static void BulkWrite(DbConnection dbCxn, SqlName dstTableName, IDataReader srcReader)
		{
			if (dbCxn is SqlConnection)
			{
				SqlUtils.BulkWrite(dbCxn as SqlConnection, dstTableName, srcReader);
				return;
			}
			if (dbCxn is OleDbConnection)
			{
				SqlUtils.BulkWrite(dbCxn as OleDbConnection, dstTableName, srcReader);
				return;
			}
			throw new ArgumentException("Unable to BulkWrite using connection of type " + dbCxn.GetType().ToString() + ".  Must be SqlConnection or OleDbConnection.");
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000172F8 File Offset: 0x000154F8
		public static void WriteSqlVariant(object o)
		{
			SqlDbType sqlDbType = SqlUtils.ObjectToSqlDbType(o);
			switch (sqlDbType)
			{
			case 0:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 8:
			case 12:
			case 13:
			case 14:
			case 16:
			case 20:
			case 21:
			case 22:
				return;
			default:
				throw new Exception(string.Format("Unsupported data type '{0}'.", sqlDbType.ToString()));
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001738C File Offset: 0x0001558C
		public static SqlDbType ObjectToSqlDbType(object o)
		{
			Type type = o.GetType();
			if (type == typeof(long))
			{
				return 0;
			}
			if (type == typeof(bool))
			{
				return 2;
			}
			if (type == typeof(DateTime))
			{
				return 4;
			}
			if (type == typeof(decimal))
			{
				return 5;
			}
			if (type == typeof(float))
			{
				return 6;
			}
			if (type == typeof(int))
			{
				return 8;
			}
			if (type == typeof(char))
			{
				return 10;
			}
			if (type == typeof(string))
			{
				return 12;
			}
			if (type == typeof(double))
			{
				return 13;
			}
			if (type == typeof(ushort))
			{
				return 16;
			}
			if (type == typeof(DataTable))
			{
				return 30;
			}
			if (type == typeof(byte))
			{
				return 20;
			}
			if (type == typeof(Guid))
			{
				return 14;
			}
			if (type == typeof(byte[]))
			{
				return 21;
			}
			throw new Exception(string.Format("Unsupported data type '{0}'.", type.ToString()));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00017490 File Offset: 0x00015690
		public static SqlDbType DbTypeToSqlDbType(DbType t)
		{
			switch (t)
			{
			case 0:
				return 22;
			case 1:
				return 1;
			case 2:
				return 20;
			case 3:
				return 2;
			case 4:
				return 9;
			case 5:
				return 4;
			case 6:
				return 4;
			case 7:
				return 5;
			case 8:
				return 6;
			case 9:
				return 14;
			case 10:
				return 16;
			case 11:
				return 8;
			case 12:
				return 0;
			case 13:
				return 29;
			case 14:
				return 16;
			case 15:
				return 13;
			case 16:
				return 12;
			case 17:
				return 4;
			case 18:
				return 8;
			case 19:
				return 8;
			case 20:
				return 0;
			case 22:
				return 3;
			case 23:
				return 10;
			case 25:
				return 25;
			}
			throw new ArgumentException("Unexpected DbType encountered when converting to SqlDbType.");
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00017554 File Offset: 0x00015754
		public static string MapSchemaTypeToSqlColumnType(DataRow schemaRow)
		{
			string text = string.Empty;
			int num = (schemaRow.IsNull(SchemaTableColumn.ColumnSize) ? 0 : ((int)schemaRow[SchemaTableColumn.ColumnSize]));
			short num2 = (schemaRow.IsNull(SchemaTableColumn.NumericPrecision) ? 255 : ((short)schemaRow[SchemaTableColumn.NumericPrecision]));
			short num3 = (schemaRow.IsNull(SchemaTableColumn.NumericScale) ? 255 : ((short)schemaRow[SchemaTableColumn.NumericScale]));
			if (!schemaRow.IsNull(SchemaTableColumn.IsLong))
			{
				bool flag = (bool)schemaRow[SchemaTableColumn.IsLong];
			}
			if (schemaRow.Table.Columns.Contains(SchemaTableOptionalColumn.IsRowVersion) && !schemaRow.IsNull(SchemaTableOptionalColumn.IsRowVersion))
			{
				bool flag2 = (bool)schemaRow[SchemaTableOptionalColumn.IsRowVersion];
			}
			string text2 = ((!schemaRow.Table.Columns.Contains(SchemaTableOptionalColumn.ProviderSpecificDataType)) ? string.Empty : (schemaRow.IsNull(SchemaTableOptionalColumn.ProviderSpecificDataType) ? string.Empty : schemaRow[SchemaTableOptionalColumn.ProviderSpecificDataType].ToString()));
			if (text2.Length > 0)
			{
				if (text2.EndsWith("DbType.AnsiString", 4))
				{
					text = string.Format("varchar({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("DbType.AnsiStringFixedLength", 4))
				{
					text = string.Format("char({0})", num);
				}
				else if (text2.EndsWith("DbType.Binary", 4))
				{
					text = string.Format("varbinary({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("DbType.Boolean", 4))
				{
					text = "bit";
				}
				else if (text2.EndsWith("DbType.Byte", 4))
				{
					text = "tinyint";
				}
				else if (text2.EndsWith("DbType.Currency", 4))
				{
					text = "money";
				}
				else if (text2.EndsWith("DbType.Date", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DbType.DateTime", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DbType.Decimal", 4))
				{
					text = string.Format("decimal({0},{1})", num2, num3);
				}
				else if (text2.EndsWith("DbType.Double", 4))
				{
					text = "float";
				}
				else if (text2.EndsWith("DbType.Guid", 4))
				{
					text = "uniqueidentifier";
				}
				else if (text2.EndsWith("DbType.Int16", 4))
				{
					text = "smallint";
				}
				else if (text2.EndsWith("DbType.Int32", 4))
				{
					text = "int";
				}
				else if (text2.EndsWith("DbType.Int64", 4))
				{
					text = "bigint";
				}
				else if (text2.EndsWith("DbType.Object", 4))
				{
					text = "variant";
				}
				else if (text2.EndsWith("DbType.SByte", 4))
				{
					text = "numeric(3,0)";
				}
				else if (text2.EndsWith("DbType.Single", 4))
				{
					text = "real";
				}
				else if (text2.EndsWith("DbType.String", 4))
				{
					text = string.Format("nvarchar({0})", (num <= 0 || num > 4000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("DbType.StringFixedLength", 4))
				{
					text = string.Format("nchar({0})", num);
				}
				else if (text2.EndsWith("DbType.Time", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DbType.UInt16", 4))
				{
					text = "numeric(5,0)";
				}
				else if (text2.EndsWith("DbType.UInt32", 4))
				{
					text = "numeric(10,0)";
				}
				else if (text2.EndsWith("DbType.UInt64", 4))
				{
					text = "numeric(20,0)";
				}
				else if (text2.EndsWith("DbType.Xml", 4))
				{
					text = "xml";
				}
				else if (text2.EndsWith("SqlTypes.BigInt", 4))
				{
					text = "bigint";
				}
				else if (text2.EndsWith("SqlTypes.Binary", 4))
				{
					text = string.Format("varbinary({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("SqlTypes.Bit", 4))
				{
					text = "bit";
				}
				else if (text2.EndsWith("SqlTypes.Char", 4))
				{
					text = string.Format("char({0})", num);
				}
				else if (text2.EndsWith("SqlTypes.DateTime", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("SqlTypes.Decimal", 4))
				{
					text = string.Format("decimal({0},{1})", num2, num3);
				}
				else if (text2.EndsWith("SqlTypes.Float", 4))
				{
					text = "float";
				}
				else if (text2.EndsWith("SqlTypes.Image", 4))
				{
					text = "image";
				}
				else if (text2.EndsWith("SqlTypes.Int", 4))
				{
					text = "int";
				}
				else if (text2.EndsWith("SqlTypes.Money", 4))
				{
					text = "money";
				}
				else if (text2.EndsWith("SqlTypes.NChar", 4))
				{
					text = string.Format("nchar({0})", num);
				}
				else if (text2.EndsWith("SqlTypes.NText", 4))
				{
					text = "ntext";
				}
				else if (text2.EndsWith("SqlTypes.NVarChar", 4))
				{
					text = string.Format("nvarchar({0})", (num <= 0 || num > 4000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("SqlTypes.Real", 4))
				{
					text = "real";
				}
				else if (text2.EndsWith("SqlTypes.SmallDateTime", 4))
				{
					text = "smalldatetime";
				}
				else if (text2.EndsWith("SqlTypes.SmallInt", 4))
				{
					text = "smallint";
				}
				else if (text2.EndsWith("SqlTypes.SmallMoney", 4))
				{
					text = "smallmoney";
				}
				else if (text2.EndsWith("SqlTypes.SqlBoolean", 4))
				{
					text = "bit";
				}
				else if (text2.EndsWith("SqlTypes.SqlByte", 4))
				{
					text = "tinyint";
				}
				else if (text2.EndsWith("SqlTypes.SqlBytes", 4))
				{
					text = string.Format("varbinary({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("SqlTypes.SqlChars", 4))
				{
					string text3 = "nchar";
					string text4 = ((num <= 0 || num > 4000) ? "max" : num.ToString());
					if (schemaRow.Table.Columns.Contains("DataTypeName") && !schemaRow.IsNull("DataTypeName"))
					{
						string text5 = (string)schemaRow["DataTypeName"];
						if (text5.Length > 0)
						{
							text3 = text5;
							text4 = ((num <= 0 || num > 8000) ? "max" : num.ToString());
						}
					}
					text = string.Format("{0}({1})", text3, text4);
				}
				else if (text2.EndsWith("SqlTypes.SqlDateTime", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("SqlTypes.SqlDecimal", 4))
				{
					text = string.Format("decimal({0},{1})", num2, num3);
				}
				else if (text2.EndsWith("SqlTypes.SqlDouble", 4))
				{
					text = "float";
				}
				else if (text2.EndsWith("SqlTypes.SqlGuid", 4))
				{
					text = "uniqueidentifier";
				}
				else if (text2.EndsWith("SqlTypes.SqlInt16", 4))
				{
					text = "smallint";
				}
				else if (text2.EndsWith("SqlTypes.SqlInt32", 4))
				{
					text = "int";
				}
				else if (text2.EndsWith("SqlTypes.SqlInt64", 4))
				{
					text = "bigint";
				}
				else if (text2.EndsWith("SqlTypes.SqlMoney", 4))
				{
					text = "money";
				}
				else if (text2.EndsWith("SqlTypes.SqlSingle", 4))
				{
					text = "real";
				}
				else if (text2.EndsWith("SqlTypes.SqlString", 4))
				{
					string text6 = "nvarchar";
					string text7 = ((num <= 0 || num > 4000) ? "max" : num.ToString());
					if (schemaRow.Table.Columns.Contains("DataTypeName") && !schemaRow.IsNull("DataTypeName"))
					{
						string text8 = (string)schemaRow["DataTypeName"];
						if (text8.Length > 0)
						{
							text6 = text8;
							text7 = ((num <= 0 || num > 8000) ? "max" : num.ToString());
						}
					}
					text = string.Format("{0}({1})", text6, text7);
				}
				else if (text2.EndsWith("SqlTypes.SqlXml", 4))
				{
					text = "xml";
				}
				else if (text2.EndsWith("SqlTypes.Text", 4))
				{
					text = "text";
				}
				else if (text2.EndsWith("SqlTypes.Timestamp", 4))
				{
					text = "timestamp";
				}
				else if (text2.EndsWith("SqlTypes.Tinyint", 4))
				{
					text = "tinyint";
				}
				else if (text2.EndsWith("SqlTypes.UniqueIdentifier", 4))
				{
					text = "uniqueidentifier";
				}
				else if (text2.EndsWith("SqlTypes.VarBinary", 4))
				{
					text = string.Format("varbinary({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("SqlTypes.VarChar", 4))
				{
					text = string.Format("varchar({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("SqlTypes.Variant", 4))
				{
					text = "variant";
				}
				else if (text2.EndsWith("SqlTypes.Xml", 4))
				{
					text = "xml";
				}
				else if (text2.EndsWith("SqlTypes.Udt", 4))
				{
					text = "udt";
				}
				else if (text2.EndsWith("DT_EMPTY", 4))
				{
					text = "bit";
				}
				else if (text2.EndsWith("DT_NULL", 4))
				{
					text = "bit";
				}
				else if (text2.EndsWith("DT_I1", 4))
				{
					text = "numeric(3, 0)";
				}
				else if (text2.EndsWith("DT_I2", 4))
				{
					text = "smallint";
				}
				else if (text2.EndsWith("DT_I4", 4))
				{
					text = "int";
				}
				else if (text2.EndsWith("DT_I8", 4))
				{
					text = "bigint";
				}
				else if (text2.EndsWith("DT_UI1", 4))
				{
					text = "tinyint";
				}
				else if (text2.EndsWith("DT_UI2", 4))
				{
					text = "numeric(5,0)";
				}
				else if (text2.EndsWith("DT_UI4", 4))
				{
					text = "numeric(10,0)";
				}
				else if (text2.EndsWith("DT_UI8", 4))
				{
					text = "numeric(20,0)";
				}
				else if (text2.EndsWith("DT_R4", 4))
				{
					text = "real";
				}
				else if (text2.EndsWith("DT_R8", 4))
				{
					text = "float";
				}
				else if (text2.EndsWith("DT_CY", 4))
				{
					text = "money";
				}
				else if (text2.EndsWith("DT_DATE", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DT_BOOL", 4))
				{
					text = "bit";
				}
				else if (text2.EndsWith("DT_DECIMAL", 4))
				{
					text = string.Format("decimal({0},{1})", num2, num3);
				}
				else if (text2.EndsWith("DT_FILETIME", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DT_GUID", 4))
				{
					text = "uniqueidentifier";
				}
				else if (text2.EndsWith("DT_BYTES", 4))
				{
					text = string.Format("varbinary({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("DT_STR", 4))
				{
					text = string.Format("varchar({0})", (num <= 0 || num > 8000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("DT_WSTR", 4))
				{
					text = string.Format("nvarchar({0})", (num <= 0 || num > 4000) ? "max" : num.ToString());
				}
				else if (text2.EndsWith("DT_NUMERIC", 4))
				{
					text = string.Format("decimal({0},{1})", num2, num3);
				}
				else if (text2.EndsWith("DT_DBDATE", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DT_DBTIME", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DT_DBTIMESTAMP", 4))
				{
					text = "datetime";
				}
				else if (text2.EndsWith("DT_IMAGE", 4))
				{
					text = "image";
				}
				else if (text2.EndsWith("DT_TEXT", 4))
				{
					text = "text";
				}
				else if (text2.EndsWith("DT_NTEXT", 4))
				{
					text = "ntext";
				}
			}
			if (text.Length == 0)
			{
				text = SqlUtils.TypeToSqlTypeString((Type)schemaRow[SchemaTableColumn.DataType], num, (int)num2, (int)num3);
			}
			return text;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000182EC File Offset: 0x000164EC
		public static string GetDefaultSqlIsNullStringLiteral(Type t)
		{
			string text;
			if (typeof(bool) == t)
			{
				text = "''";
			}
			else if (typeof(byte) == t)
			{
				text = "''";
			}
			else if (typeof(byte[]) == t)
			{
				text = "0x0";
			}
			else if (typeof(char) == t)
			{
				text = "''";
			}
			else if (typeof(DateTime) == t)
			{
				text = "''";
			}
			else if (typeof(decimal) == t)
			{
				text = "0";
			}
			else if (typeof(double) == t)
			{
				text = "0";
			}
			else if (typeof(Guid) == t)
			{
				text = "'00000000-0000-0000-0000-000000000000'";
			}
			else if (typeof(short) == t)
			{
				text = "0";
			}
			else if (typeof(int) == t)
			{
				text = "0";
			}
			else if (typeof(long) == t)
			{
				text = "0";
			}
			else if (typeof(object) == t)
			{
				text = "''";
			}
			else if (typeof(sbyte) == t)
			{
				text = "0";
			}
			else if (typeof(float) == t)
			{
				text = "0";
			}
			else if (typeof(string) == t)
			{
				text = "''";
			}
			else if (typeof(ushort) == t)
			{
				text = "0";
			}
			else if (typeof(uint) == t)
			{
				text = "0";
			}
			else if (typeof(ulong) == t)
			{
				text = "0";
			}
			else
			{
				text = "''";
			}
			return text;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000184A8 File Offset: 0x000166A8
		public static void TypeToSqlDbType(Type t, int length, out SqlDbType sqlDbType, out int size)
		{
			sqlDbType = 23;
			size = 0;
			if (typeof(bool) == t)
			{
				sqlDbType = 2;
				return;
			}
			if (typeof(byte) == t)
			{
				sqlDbType = 20;
				return;
			}
			if (typeof(byte[]) == t)
			{
				sqlDbType = 21;
				size = ((length <= 0 || length > 8000) ? (-1) : length);
				return;
			}
			if (typeof(char) == t)
			{
				sqlDbType = 3;
				return;
			}
			if (typeof(DateTime) == t)
			{
				sqlDbType = 4;
				return;
			}
			if (typeof(decimal) == t)
			{
				sqlDbType = 5;
				return;
			}
			if (typeof(double) == t)
			{
				sqlDbType = 6;
				return;
			}
			if (typeof(Guid) == t)
			{
				sqlDbType = 14;
				return;
			}
			if (typeof(short) == t)
			{
				sqlDbType = 16;
				return;
			}
			if (typeof(int) == t)
			{
				sqlDbType = 8;
				return;
			}
			if (typeof(long) == t)
			{
				sqlDbType = 0;
				return;
			}
			if (typeof(object) == t)
			{
				sqlDbType = 23;
				return;
			}
			if (typeof(sbyte) == t)
			{
				sqlDbType = 16;
				return;
			}
			if (typeof(float) == t)
			{
				sqlDbType = 13;
				return;
			}
			if (typeof(string) == t)
			{
				sqlDbType = 12;
				size = ((length <= 0 || length > 4000) ? (-1) : length);
				return;
			}
			if (typeof(ushort) == t)
			{
				sqlDbType = 5;
				return;
			}
			if (typeof(uint) == t)
			{
				sqlDbType = 5;
				return;
			}
			if (typeof(ulong) == t)
			{
				sqlDbType = 5;
				return;
			}
			sqlDbType = 23;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00018620 File Offset: 0x00016820
		public static string TypeToSqlTypeString(Type t, int length, int precision, int scale)
		{
			string text;
			if (typeof(bool) == t)
			{
				text = "bit";
			}
			else if (typeof(byte) == t)
			{
				text = "tinyint";
			}
			else if (typeof(byte[]) == t)
			{
				text = string.Format("varbinary({0})", (length <= 0 || length > 8000) ? "max" : length.ToString());
			}
			else if (typeof(char) == t)
			{
				text = "char";
			}
			else if (typeof(DateTime) == t)
			{
				text = "datetime";
			}
			else if (typeof(decimal) == t)
			{
				text = string.Format("decimal({0},{1})", precision, scale);
			}
			else if (typeof(double) == t)
			{
				text = "float";
			}
			else if (typeof(Guid) == t)
			{
				text = "uniqueidentifier";
			}
			else if (typeof(short) == t)
			{
				text = "smallint";
			}
			else if (typeof(int) == t)
			{
				text = "int";
			}
			else if (typeof(long) == t)
			{
				text = "bigint";
			}
			else if (typeof(object) == t)
			{
				text = "sql_variant";
			}
			else if (typeof(sbyte) == t)
			{
				text = "numeric(3,0)";
			}
			else if (typeof(float) == t)
			{
				text = "real";
			}
			else if (typeof(string) == t)
			{
				text = string.Format("nvarchar({0})", (length <= 0 || length > 4000) ? "max" : length.ToString());
			}
			else if (typeof(ushort) == t)
			{
				text = "numeric(5,0)";
			}
			else if (typeof(uint) == t)
			{
				text = "numeric(10,0)";
			}
			else if (typeof(ulong) == t)
			{
				text = "numeric(20,0)";
			}
			else
			{
				text = "variant";
			}
			return text;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001882A File Offset: 0x00016A2A
		public static string GenerateSqlCreateTableStatementFromSchema(DataTable schemaTable, SqlName targetTableName)
		{
			return SqlUtils.GenerateSqlCreateTableStatementFromSchema(schemaTable, targetTableName, true);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00018834 File Offset: 0x00016A34
		public static string GenerateSqlCreateTableStatementFromSchema(DataTable schemaTable, SqlName targetTableName, bool addKeyConstraints)
		{
			return string.Format("CREATE TABLE {0} ({1})", targetTableName.QualifiedName, SqlUtils.GenerateSqlColumnNameColumnTypeListFromSchema(schemaTable, addKeyConstraints));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00018850 File Offset: 0x00016A50
		public static string GenerateSqlColumnNameColumnTypeListFromSchema(DataTable schemaTable, bool addKeyConstraints)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.AppendFormat("{0} {1}", SqlName.DelimitElement(dataRow[SchemaTableColumn.ColumnName].ToString()), SqlUtils.MapSchemaTypeToSqlColumnType(dataRow));
				if (addKeyConstraints && !dataRow.IsNull(SchemaTableColumn.IsKey) && (bool)dataRow[SchemaTableColumn.IsKey])
				{
					stringBuilder.Append(" primary key");
				}
				if (!dataRow.IsNull(SchemaTableColumn.AllowDBNull) && !(bool)dataRow[SchemaTableColumn.AllowDBNull])
				{
					stringBuilder.Append(" not null");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001894C File Offset: 0x00016B4C
		public static SqlCommand GenerateSqlInsertCommandFromSchema(DataTable schemaTable, SqlName targetTableName, SqlConnection connection)
		{
			SqlCommand sqlCommand = connection.CreateCommand();
			sqlCommand.CommandTimeout = connection.ConnectionTimeout;
			string text = SchemaUtils.CommaSeparatedListOfColumnNames(schemaTable);
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < schemaTable.Rows.Count; i++)
			{
				string text2 = string.Format("@column{0}", i.ToString());
				if (i > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(text2);
				SqlMetaData sqlMetaData = SqlUtils.SqlMetaDataFromSchemaTableRow(schemaTable.Rows[i]);
				sqlCommand.Parameters.Add(text2, sqlMetaData.SqlDbType, (int)sqlMetaData.MaxLength);
			}
			sqlCommand.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", targetTableName.QualifiedName, text, stringBuilder.ToString());
			sqlCommand.Prepare();
			return sqlCommand;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00018A10 File Offset: 0x00016C10
		public static void CreateSqlTableFromDataTable(SqlConnection cxn, DataTable sourceTable, SqlName targetTableName, bool dropTableIfExists)
		{
			using (IDataReader dataReader = sourceTable.CreateDataReader())
			{
				SqlUtils.CreateTableFromSchema(cxn, dataReader.GetSchemaTable(), targetTableName, dropTableIfExists);
				new SqlBulkCopy(cxn, 4, null)
				{
					DestinationTableName = targetTableName.QualifiedName
				}.WriteToServer(dataReader);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00018A68 File Offset: 0x00016C68
		public static void CreateTableFromSchema(DbConnection cxn, DataTable schemaTable, SqlName targetTableName, bool dropTableIfExists)
		{
			SqlUtils.CreateTableFromSchema(cxn, schemaTable, targetTableName, dropTableIfExists, true);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00018A74 File Offset: 0x00016C74
		public static void CreateTableFromSchema(DbConnection cxn, DataTable schemaTable, SqlName targetTableName, bool dropTableIfExists, bool addKeyConstraints)
		{
			if (SqlUtils.TableExists(cxn, targetTableName))
			{
				if (!dropTableIfExists)
				{
					throw new DuplicateNameException("Cannot create new table.  Table " + targetTableName.QualifiedName + " already exists.");
				}
				SqlUtils.TryDropTable(cxn, targetTableName);
			}
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandText = SqlUtils.GenerateSqlCreateTableStatementFromSchema(schemaTable, targetTableName, addKeyConstraints);
				dbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00018AEC File Offset: 0x00016CEC
		public static void AddExtendedProperty(DbConnection cxn, string propertyName, object propertyValue, string typeL0, string nameL0, string typeL1, string nameL1, string typeL2, string nameL2)
		{
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandType = 4;
				dbCommand.CommandText = "sp_addextendedproperty";
				SqlUtils.AddParameterWithValue(dbCommand, "level0type", typeL0);
				SqlUtils.AddParameterWithValue(dbCommand, "level0name", nameL0);
				SqlUtils.AddParameterWithValue(dbCommand, "level1type", typeL1);
				SqlUtils.AddParameterWithValue(dbCommand, "level1name", nameL1);
				SqlUtils.AddParameterWithValue(dbCommand, "level2type", typeL2);
				SqlUtils.AddParameterWithValue(dbCommand, "level2name", nameL2);
				SqlUtils.AddParameterWithValue(dbCommand, "name", propertyName);
				SqlUtils.AddParameterWithValue(dbCommand, "value", propertyValue);
				dbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00018B9C File Offset: 0x00016D9C
		public static void UpdateExtendedProperty(DbConnection cxn, string propertyName, object propertyValue, string typeL0, string nameL0, string typeL1, string nameL1, string typeL2, string nameL2)
		{
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandType = 4;
				dbCommand.CommandText = "sp_updateextendedproperty";
				SqlUtils.AddParameterWithValue(dbCommand, "level0type", typeL0);
				SqlUtils.AddParameterWithValue(dbCommand, "level0name", nameL0);
				SqlUtils.AddParameterWithValue(dbCommand, "level1type", typeL1);
				SqlUtils.AddParameterWithValue(dbCommand, "level1name", nameL1);
				SqlUtils.AddParameterWithValue(dbCommand, "level2type", typeL2);
				SqlUtils.AddParameterWithValue(dbCommand, "level2name", nameL2);
				SqlUtils.AddParameterWithValue(dbCommand, "name", propertyName);
				SqlUtils.AddParameterWithValue(dbCommand, "value", propertyValue);
				dbCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00018C4C File Offset: 0x00016E4C
		public static object GetExtendedProperty(DbConnection cxn, string propertyName, string typeL0, string nameL0, string typeL1, string nameL1, string typeL2, string nameL2)
		{
			object obj;
			using (DbCommand dbCommand = SqlUtils.CreateDbCommand(cxn))
			{
				dbCommand.CommandText = "select value \r\n                                    from fn_listextendedproperty(@name, @level0type, @level0name, @level1type, @level1name, @level2type, @level2name)";
				SqlUtils.AddParameterWithValue(dbCommand, "name", propertyName);
				SqlUtils.AddParameterWithValue(dbCommand, "level0type", typeL0);
				SqlUtils.AddParameterWithValue(dbCommand, "level0name", nameL0);
				SqlUtils.AddParameterWithValue(dbCommand, "level1type", typeL1);
				SqlUtils.AddParameterWithValue(dbCommand, "level1name", nameL1);
				SqlUtils.AddParameterWithValue(dbCommand, "level2type", typeL2);
				SqlUtils.AddParameterWithValue(dbCommand, "level2name", nameL2);
				obj = dbCommand.ExecuteScalar();
			}
			return obj;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00018CE8 File Offset: 0x00016EE8
		public static SqlMetaData[] CreateSqlMetaDataFromSchemaTable(DataTable schemaTable)
		{
			SqlMetaData[] array = new SqlMetaData[schemaTable.Rows.Count];
			int num = 0;
			foreach (object obj in schemaTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				array[num] = SqlUtils.SqlMetaDataFromSchemaTableRow(dataRow);
				num++;
			}
			return array;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00018D60 File Offset: 0x00016F60
		public static SqlMetaData SqlMetaDataFromSchemaTableRow(DataRow r)
		{
			SqlMetaData sqlMetaData = null;
			string text = (string)r[SchemaTableColumn.ColumnName];
			SqlDbType sqlDbType;
			if (r.Table.Columns.Contains("DataTypeName") && r["DataTypeName"].ToString().Length > 0)
			{
				sqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), r["DataTypeName"].ToString(), true);
			}
			else
			{
				if (!r.Table.Columns.Contains(SchemaTableColumn.DataType) || r[SchemaTableColumn.DataType] == null)
				{
					throw new ArgumentException("Unable to convert schema metadata row to SqlMetaData.");
				}
				sqlDbType = SqlUtils.DbTypeToSqlDbType(SchemaUtils.TypeToDbType((Type)r[SchemaTableColumn.DataType]));
			}
			switch (sqlDbType)
			{
			case 0:
			case 2:
			case 4:
			case 6:
			case 8:
			case 9:
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
			case 19:
			case 20:
				sqlMetaData = new SqlMetaData(text, sqlDbType);
				break;
			case 1:
			case 3:
			case 7:
			case 10:
			case 11:
			case 12:
			case 18:
			case 21:
			case 22:
			{
				long num = ((r[SchemaTableColumn.ColumnSize] is long) ? ((long)r[SchemaTableColumn.ColumnSize]) : ((long)((r[SchemaTableColumn.ColumnSize] is int) ? ((int)r[SchemaTableColumn.ColumnSize]) : 0)));
				sqlMetaData = new SqlMetaData(text, sqlDbType, num);
				break;
			}
			case 5:
				sqlMetaData = new SqlMetaData(text, sqlDbType, (byte)r[SchemaTableColumn.NumericPrecision], (byte)r[SchemaTableColumn.NumericScale]);
				break;
			}
			return sqlMetaData;
		}
	}
}
