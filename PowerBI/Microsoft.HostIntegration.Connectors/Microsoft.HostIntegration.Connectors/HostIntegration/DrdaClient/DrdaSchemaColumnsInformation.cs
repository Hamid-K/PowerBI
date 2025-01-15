using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A15 RID: 2581
	internal class DrdaSchemaColumnsInformation : DrdaSchemaInformation
	{
		// Token: 0x06005145 RID: 20805 RVA: 0x00146302 File Offset: 0x00144502
		public DrdaSchemaColumnsInformation()
			: base("Columns", DrdaSchemaColumnsInformation.SchemaRestrictions, 4, DrdaSchemaColumnsInformation.SchemaResultColumns)
		{
		}

		// Token: 0x06005146 RID: 20806 RVA: 0x0014631C File Offset: 0x0014451C
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			this._dateTimeSubTypeIndex = -1;
			bool useSQL = base.CanUseSQL(conn);
			if (!useSQL)
			{
				try
				{
					await statement.ExecuteAsync("CALL SYSIBM.SQLCOLUMNS (?, ?, ?, ?, ?)", base.GetParameters(conn.Requester.HostType, parameters, 0, options), true, false, isAsync, cancellationToken);
				}
				catch (DrdaException)
				{
					useSQL = true;
				}
			}
			if (useSQL)
			{
				await statement.ExecuteAsync(DrdaSchemaColumnsInformation.ColumnsQuery.GetQuery(conn, parameters), null, true, false, isAsync, cancellationToken);
			}
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00146394 File Offset: 0x00144594
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaClientType drdaClientType = DrdaClientType.VarChar;
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
			object obj = null;
			string text = null;
			for (int i = 0; i < resultSet.FieldCount; i++)
			{
				switch (i)
				{
				case 0:
					obj = queryResultSet.GetValue(i);
					if (obj == null || obj is DBNull || (obj is string && ((string)obj).ToString().Length == 0))
					{
						obj = connection.Database;
					}
					break;
				case 1:
					obj = queryResultSet.GetValue(i);
					break;
				case 2:
					obj = queryResultSet.GetValue(i);
					break;
				case 3:
					obj = queryResultSet.GetValue(i);
					break;
				case 4:
				{
					int num = resultSet.GetColumnMappingIndex(i, "ORDINAL_POSITION", 16);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 5:
				{
					int num = resultSet.GetColumnMappingIndex(i, "TYPE_NAME", 5);
					obj = queryResultSet.GetValue(num);
					text = (string)obj;
					DrdaType drdaType = DrdaSchemaInformation.TypeNameToDrdaType(connection.Requester.Flavor, text);
					drdaType = base.AdjustInformixDatetimeType(drdaType, queryResultSet);
					drdaClientType = DataTypeConverter.ToDrdaClientType(drdaType);
					if (drdaType == DrdaType.Real)
					{
						num = resultSet.GetColumnMappingIndex(7, "PRECISION", -1);
						if (hasColumns[7] && num > -1)
						{
							int precision = this.GetPrecision(queryResultSet, num);
							string serverClass = connection.ServerClass;
							if (serverClass != null && serverClass == "DB2/MVS")
							{
								if (precision > 4)
								{
									drdaType = DrdaType.Double;
									drdaClientType = DrdaClientType.Double;
								}
							}
							else if (precision > 24)
							{
								drdaType = DrdaType.Double;
								drdaClientType = DrdaClientType.Double;
							}
						}
					}
					obj = drdaType;
					break;
				}
				case 6:
				{
					int num = resultSet.GetColumnMappingIndex(7, "PRECISION", -1);
					if (hasColumns[7] && num > 0 && (drdaClientType == DrdaClientType.Real || drdaClientType == DrdaClientType.Double))
					{
						obj = this.GetPrecision(queryResultSet, num);
					}
					else
					{
						num = resultSet.GetColumnMappingIndex(6, "COLUMN_SIZE", 6);
						obj = queryResultSet.GetValue(num);
						obj = DrdaSchemaInformation.ConvertLength(obj, drdaClientType);
					}
					break;
				}
				case 7:
					if (hasColumns[7])
					{
						int num = resultSet.GetColumnMappingIndex(7, "PRECISION", -1);
						if (num > -1)
						{
							obj = queryResultSet.GetValue(num);
						}
						else
						{
							hasColumns[7] = false;
							num = resultSet.GetColumnMappingIndex(6, "COLUMN_SIZE", 6);
							obj = queryResultSet.GetValue(num);
						}
					}
					else
					{
						int num = resultSet.GetColumnMappingIndex(6, "COLUMN_SIZE", 6);
						obj = queryResultSet.GetValue(num);
					}
					obj = DrdaSchemaInformation.ConvertPrecision(obj, drdaClientType);
					break;
				case 8:
				{
					int num = resultSet.GetColumnMappingIndex(8, "DECIMAL_DIGITS", 8);
					obj = queryResultSet.GetValue(num);
					obj = DrdaSchemaInformation.ConvertScale(obj, drdaClientType);
					break;
				}
				case 9:
				{
					int num = resultSet.GetColumnMappingIndex(9, "NULLABLE", 10);
					obj = queryResultSet.GetValue(num);
					obj = DrdaSchemaInformation.ConvertNullable(obj);
					break;
				}
				case 10:
				{
					int num = resultSet.GetColumnMappingIndex(10, "REMARKS", 11);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 11:
					if (hasColumns[11])
					{
						int num = resultSet.GetColumnMappingIndex(11, "CCSID", -2);
						if (num >= 0)
						{
							obj = queryResultSet.GetValue(num);
							if (obj != null && obj != DBNull.Value && (int)obj == 65535)
							{
								if (drdaClientType == DrdaClientType.Char)
								{
									DrdaType drdaType = DataTypeConverter.ToDrdaType(DrdaClientType.Binary);
									resultSet.GetColumn(5).Value = drdaType;
								}
								else if (drdaClientType == DrdaClientType.VarChar)
								{
									DrdaType drdaType = DataTypeConverter.ToDrdaType(DrdaClientType.VarBinary);
									resultSet.GetColumn(5).Value = drdaType;
								}
							}
						}
						else
						{
							hasColumns[11] = false;
							obj = 0;
						}
					}
					else
					{
						obj = 0;
					}
					break;
				case 12:
				{
					int num = resultSet.GetColumnMappingIndex(12, "COLUMN_DEF", 12);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 13:
				{
					obj = DBNull.Value;
					int num = resultSet.GetColumnMappingIndex(13, "IS_AUTOINCREMENT", 22);
					if (queryResultSet.FieldCount > num)
					{
						obj = queryResultSet.GetValue(num);
						obj = DrdaSchemaInformation.ConvertNullable(obj);
					}
					break;
				}
				case 14:
					obj = text;
					break;
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x04003FCE RID: 16334
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("TABLE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_NAME", typeof(string), null, 128),
			new DrdaSchemaRestriction("COLUMN_NAME", typeof(string), null, 30)
		};

		// Token: 0x04003FCF RID: 16335
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("TableCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ColumnName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Ordinal", typeof(int), null),
			new DrdaSchemaResultColumn("DataType", typeof(DrdaType), DrdaType.VarChar),
			new DrdaSchemaResultColumn("Length", typeof(int), 0),
			new DrdaSchemaResultColumn("Precision", typeof(int), 0),
			new DrdaSchemaResultColumn("Scale", typeof(int), 0),
			new DrdaSchemaResultColumn("Nullable", typeof(bool), true),
			new DrdaSchemaResultColumn("Remarks", typeof(string), null, 128),
			new DrdaSchemaResultColumn("CCSID", typeof(int), 0),
			new DrdaSchemaResultColumn("Definition", typeof(string), null, 254),
			new DrdaSchemaResultColumn("IsAutoInc", typeof(bool), false),
			new DrdaSchemaResultColumn("SourceTypeName", typeof(string), null, 128)
		};

		// Token: 0x04003FD0 RID: 16336
		private static DrdaColumnsSchemaQuery ColumnsQuery = new DrdaColumnsSchemaQuery();
	}
}
