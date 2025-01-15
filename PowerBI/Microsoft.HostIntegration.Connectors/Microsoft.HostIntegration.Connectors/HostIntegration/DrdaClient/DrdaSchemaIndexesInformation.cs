using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A26 RID: 2598
	internal class DrdaSchemaIndexesInformation : DrdaSchemaInformation
	{
		// Token: 0x06005176 RID: 20854 RVA: 0x0014BABB File Offset: 0x00149CBB
		public DrdaSchemaIndexesInformation()
			: base("Indexes", DrdaSchemaIndexesInformation.SchemaRestrictions, 5, DrdaSchemaIndexesInformation.SchemaResultColumns)
		{
		}

		// Token: 0x06005177 RID: 20855 RVA: 0x0014BAD4 File Offset: 0x00149CD4
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			List<ISqlParameter> parameters2 = base.GetParameters(conn.Requester.HostType, parameters, 0, options);
			await statement.ExecuteAsync("CALL SYSIBM.SQLSTATISTICS (?, ?, ?, ?, ?, ?)", parameters2, true, false, isAsync, cancellationToken);
		}

		// Token: 0x06005178 RID: 20856 RVA: 0x0014BB4C File Offset: 0x00149D4C
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
			object obj = null;
			int num = 0;
			for (int i = 0; i < resultSet.FieldCount; i++)
			{
				bool flag = false;
				switch (i)
				{
				case 0:
					obj = queryResultSet.GetValue(i);
					if (obj == null || obj is DBNull || (obj is string && ((string)obj).ToString().Length == 0))
					{
						obj = connection.Database;
					}
					flag = true;
					break;
				case 1:
					obj = queryResultSet.GetValue(i);
					flag = true;
					break;
				case 2:
					obj = queryResultSet.GetValue(i);
					flag = true;
					break;
				case 3:
					num = resultSet.GetColumnMappingIndex(3, "NON_UNIQUE", -1);
					if (num == -1)
					{
						num = resultSet.GetColumnMappingIndex(3, "UNIQUE", -2);
					}
					obj = DBNull.Value;
					if (num < queryResultSet.FieldCount && num >= 0)
					{
						obj = queryResultSet.GetValue(num);
						if (obj != null && obj != DBNull.Value)
						{
							short num2 = (short)obj;
							if (num == 3)
							{
								obj = num2 != 1;
							}
							else
							{
								obj = num2 == -1;
							}
						}
						else
						{
							obj = DBNull.Value;
						}
					}
					flag = true;
					break;
				case 4:
					num = resultSet.GetColumnMappingIndex(i, "INDEX_SCHEMA", -2);
					break;
				case 5:
					num = resultSet.GetColumnMappingIndex(i, "INDEX_NAME", -2);
					break;
				case 6:
					num = resultSet.GetColumnMappingIndex(i, "TYPE", -2);
					break;
				case 7:
					num = resultSet.GetColumnMappingIndex(i, "ORDINAL_POSITION", -2);
					break;
				case 8:
					num = resultSet.GetColumnMappingIndex(i, "COLUMN_NAME", -2);
					break;
				case 9:
					num = resultSet.GetColumnMappingIndex(i, "ASC_OR_DESC", -2);
					obj = DBNull.Value;
					if (num < queryResultSet.FieldCount && num >= 0)
					{
						obj = queryResultSet.GetValue(num);
						if (obj != null && obj != DBNull.Value)
						{
							obj = string.Compare((string)obj, "A", StringComparison.InvariantCultureIgnoreCase) == 0;
						}
						else
						{
							obj = DBNull.Value;
						}
					}
					else
					{
						obj = DBNull.Value;
					}
					flag = true;
					break;
				case 10:
					num = resultSet.GetColumnMappingIndex(i, "COLLATION", -2);
					break;
				case 11:
					num = resultSet.GetColumnMappingIndex(i, "CARDINALITY", -2);
					break;
				case 12:
					num = resultSet.GetColumnMappingIndex(i, "PAGES", -2);
					break;
				case 13:
					num = resultSet.GetColumnMappingIndex(i, "FILTER_CONDITION", -2);
					break;
				}
				if (!flag)
				{
					if (num < queryResultSet.FieldCount && num >= 0)
					{
						obj = queryResultSet.GetValue(num);
					}
					else
					{
						obj = DBNull.Value;
					}
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x0400402B RID: 16427
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("TABLE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_NAME", typeof(string), null, 128),
			new DrdaSchemaRestriction("UNIQUE", typeof(short), null),
			new DrdaSchemaRestriction("RESERVED", typeof(short), null)
		};

		// Token: 0x0400402C RID: 16428
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("TableCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Unique", typeof(bool), null),
			new DrdaSchemaResultColumn("IndexSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("IndexName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Type", typeof(short), null),
			new DrdaSchemaResultColumn("Ordinal", typeof(int), null),
			new DrdaSchemaResultColumn("ColumnName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("IsAsc", typeof(bool), null),
			new DrdaSchemaResultColumn("Collation", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Cardinality", typeof(long), null),
			new DrdaSchemaResultColumn("Pages", typeof(int), null),
			new DrdaSchemaResultColumn("FilterCondition", typeof(string), null, 128)
		};
	}
}
