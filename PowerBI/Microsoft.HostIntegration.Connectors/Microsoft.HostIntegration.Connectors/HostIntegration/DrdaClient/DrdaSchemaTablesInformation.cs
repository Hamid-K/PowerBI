using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A11 RID: 2577
	internal class DrdaSchemaTablesInformation : DrdaSchemaInformation
	{
		// Token: 0x06005139 RID: 20793 RVA: 0x00145C7D File Offset: 0x00143E7D
		public DrdaSchemaTablesInformation()
			: base("Tables", DrdaSchemaTablesInformation.SchemaRestrictions, 3, DrdaSchemaTablesInformation.SchemaResultColumns)
		{
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x00145C98 File Offset: 0x00143E98
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			bool useSQL = base.CanUseSQL(conn);
			if (!useSQL)
			{
				try
				{
					await statement.ExecuteAsync("CALL SYSIBM.SQLTABLES (?, ?, ?, ?, ?)", base.GetParameters(conn.Requester.HostType, parameters, 0, options), true, false, isAsync, cancellationToken);
				}
				catch (DrdaException)
				{
					useSQL = true;
				}
			}
			if (useSQL)
			{
				await statement.ExecuteAsync(DrdaSchemaTablesInformation.TablesQuery.GetQuery(conn, parameters), null, true, false, isAsync, cancellationToken);
			}
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x00145D10 File Offset: 0x00143F10
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
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
				case 2:
				case 4:
					obj = queryResultSet.GetValue(i);
					break;
				case 3:
					obj = queryResultSet.GetValue(i);
					obj = DrdaSchemaInformation.ConvertTableType(obj);
					break;
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x04003FB7 RID: 16311
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("TABLE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_NAME", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_TYPE", typeof(string), null, 128)
		};

		// Token: 0x04003FB8 RID: 16312
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("TableCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableType", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Remarks", typeof(string), null, 128)
		};

		// Token: 0x04003FB9 RID: 16313
		private static DrdaTablesSchemaQuery TablesQuery = new DrdaTablesSchemaQuery();
	}
}
