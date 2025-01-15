using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A1A RID: 2586
	internal class DrdaSchemaProcedureParametersInformation : DrdaSchemaInformation
	{
		// Token: 0x06005151 RID: 20817 RVA: 0x00147032 File Offset: 0x00145232
		public DrdaSchemaProcedureParametersInformation()
			: base("ProcedureParameters", DrdaSchemaProcedureParametersInformation.SchemaRestrictions, 4, DrdaSchemaProcedureParametersInformation.SchemaResultColumns)
		{
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x0014704C File Offset: 0x0014524C
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			this._dateTimeSubTypeIndex = -1;
			bool useSQL = base.CanUseSQL(conn);
			if (!useSQL)
			{
				try
				{
					await statement.ExecuteAsync("CALL SYSIBM.SQLPROCEDURECOLS (?, ?, ?, ?, ?)", base.GetParameters(conn.Requester.HostType, parameters, 0, options), true, false, isAsync, cancellationToken);
				}
				catch (DrdaException)
				{
					useSQL = true;
				}
			}
			if (useSQL)
			{
				await statement.ExecuteAsync(DrdaSchemaProcedureParametersInformation.ProcedureParametersQuery.GetQuery(conn, parameters), null, true, false, isAsync, cancellationToken);
			}
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x001470C4 File Offset: 0x001452C4
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaClientType drdaClientType = DrdaClientType.VarChar;
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
			string text = string.Empty;
			object obj = null;
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
					int num = resultSet.GetColumnMappingIndex(4, "ORDINAL_POSITION", 17);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 5:
				{
					int num = resultSet.GetColumnMappingIndex(5, "COLUMN_TYPE", 5);
					obj = queryResultSet.GetValue(num);
					obj = DrdaSchemaInformation.ConvertParameterDirection(obj);
					break;
				}
				case 6:
				{
					int num = resultSet.GetColumnMappingIndex(6, "TYPE_NAME", 6);
					obj = queryResultSet.GetValue(num);
					text = (string)obj;
					DrdaType drdaType = DrdaSchemaInformation.TypeNameToDrdaType(connection.Requester.Flavor, text);
					drdaType = base.AdjustInformixDatetimeType(drdaType, queryResultSet);
					drdaClientType = DataTypeConverter.ToDrdaClientType(drdaType);
					obj = drdaType;
					break;
				}
				case 7:
				{
					int num = resultSet.GetColumnMappingIndex(8, "PRECISION", -1);
					if (hasColumns[7] && num > -1 && (drdaClientType == DrdaClientType.Real || drdaClientType == DrdaClientType.Double || drdaClientType == DrdaClientType.Decimal || drdaClientType == DrdaClientType.Numeric))
					{
						obj = this.GetPrecision(queryResultSet, num);
					}
					else
					{
						num = resultSet.GetColumnMappingIndex(7, "COLUMN_SIZE", 7);
						obj = queryResultSet.GetValue(num);
						obj = DrdaSchemaInformation.ConvertLength(obj, drdaClientType);
					}
					break;
				}
				case 8:
					if (hasColumns[8])
					{
						int num = resultSet.GetColumnMappingIndex(8, "PRECISION", -1);
						if (num > -1)
						{
							obj = queryResultSet.GetValue(num);
						}
						else
						{
							hasColumns[8] = false;
							num = resultSet.GetColumnMappingIndex(7, "COLUMN_SIZE", 7);
							obj = queryResultSet.GetValue(num);
						}
					}
					else
					{
						int num = resultSet.GetColumnMappingIndex(7, "COLUMN_SIZE", 7);
						obj = queryResultSet.GetValue(num);
					}
					obj = DrdaSchemaInformation.ConvertPrecision(obj, drdaClientType);
					break;
				case 9:
				{
					int num = resultSet.GetColumnMappingIndex(9, "DECIMAL_DIGITS", 9);
					obj = queryResultSet.GetValue(num);
					obj = DrdaSchemaInformation.ConvertScale(obj, drdaClientType);
					break;
				}
				case 10:
				{
					int num = resultSet.GetColumnMappingIndex(10, "NULLABLE", 10);
					obj = queryResultSet.GetValue(num);
					obj = DrdaSchemaInformation.ConvertNullable(obj);
					break;
				}
				case 11:
					if (hasColumns[11])
					{
						int num = resultSet.GetColumnMappingIndex(11, "CCSID", -1);
						if (num > -1)
						{
							obj = queryResultSet.GetValue(num);
							if ((int)obj == 65535)
							{
								if (drdaClientType == DrdaClientType.Char)
								{
									DrdaType drdaType = DataTypeConverter.ToDrdaType(DrdaClientType.Binary);
									resultSet.GetColumn(6).Value = drdaType;
								}
								if (drdaClientType == DrdaClientType.VarChar)
								{
									DrdaType drdaType = DataTypeConverter.ToDrdaType(DrdaClientType.VarBinary);
									resultSet.GetColumn(6).Value = drdaType;
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
					int num = resultSet.GetColumnMappingIndex(12, "REMARKS", 12);
					obj = queryResultSet.GetValue(num);
					break;
				}
				case 13:
					obj = text;
					break;
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x04003FF0 RID: 16368
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("PROCEDURE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("PROCEDURE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("PROCEDURE_NAME", typeof(string), null, 128),
			new DrdaSchemaRestriction("PARAMETER_NAME", typeof(string), null, 128)
		};

		// Token: 0x04003FF1 RID: 16369
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("ProcedureCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ProcedureSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ProcedureName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ParameterName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Ordinal", typeof(int), null),
			new DrdaSchemaResultColumn("Direction", typeof(DrdaParameterDirection), DrdaParameterDirection.Input),
			new DrdaSchemaResultColumn("DataType", typeof(DrdaType), DrdaType.VarChar),
			new DrdaSchemaResultColumn("Length", typeof(int), 0),
			new DrdaSchemaResultColumn("Precision", typeof(int), 0),
			new DrdaSchemaResultColumn("Scale", typeof(int), 0),
			new DrdaSchemaResultColumn("Nullable", typeof(bool), true),
			new DrdaSchemaResultColumn("CCSID", typeof(int), 0),
			new DrdaSchemaResultColumn("Remarks", typeof(string), null, 128),
			new DrdaSchemaResultColumn("SourceTypeName", typeof(string), null, 128)
		};

		// Token: 0x04003FF2 RID: 16370
		private static DrdaProcedureParametersSchemaQuery ProcedureParametersQuery = new DrdaProcedureParametersSchemaQuery();
	}
}
