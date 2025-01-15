using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A17 RID: 2583
	internal class DrdaSchemaProceduresInformation : DrdaSchemaInformation
	{
		// Token: 0x0600514B RID: 20811 RVA: 0x00146BEA File Offset: 0x00144DEA
		public DrdaSchemaProceduresInformation()
			: base("Procedures", DrdaSchemaProceduresInformation.SchemaRestrictions, 3, DrdaSchemaProceduresInformation.SchemaResultColumns)
		{
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00146C04 File Offset: 0x00144E04
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			bool useSQL = base.CanUseSQL(conn);
			if (!useSQL)
			{
				try
				{
					await statement.ExecuteAsync("CALL SYSIBM.SQLPROCEDURES (?, ?, ?, ?)", base.GetParameters(conn.Requester.HostType, parameters, 0, options), true, false, isAsync, cancellationToken);
				}
				catch (DrdaException)
				{
					useSQL = true;
				}
			}
			if (useSQL)
			{
				await statement.ExecuteAsync(DrdaSchemaProceduresInformation.ProceduresQuery.GetQuery(conn, parameters), null, true, false, isAsync, cancellationToken);
			}
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00146C7C File Offset: 0x00144E7C
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
			object obj = null;
			for (int i = 0; i < resultSet.FieldCount; i++)
			{
				if (i != 0)
				{
					if (i - 1 <= 5)
					{
						obj = queryResultSet.GetValue(i);
					}
				}
				else
				{
					obj = queryResultSet.GetValue(i);
					if (obj == null || obj is DBNull || (obj is string && ((string)obj).ToString().Length == 0))
					{
						obj = connection.Database;
					}
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x04003FDC RID: 16348
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("PROCEDURE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("PROCEDURE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("PROCEDURE_NAME", typeof(string), null, 128)
		};

		// Token: 0x04003FDD RID: 16349
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("ProcedureCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ProcedureSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ProcedureName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("NumberInputParams", typeof(int), null),
			new DrdaSchemaResultColumn("NumberOutputParams", typeof(int), null),
			new DrdaSchemaResultColumn("NumberResultSets", typeof(int), null),
			new DrdaSchemaResultColumn("Remarks", typeof(string), null, 128)
		};

		// Token: 0x04003FDE RID: 16350
		private static DrdaProceduresSchemaQuery ProceduresQuery = new DrdaProceduresSchemaQuery();
	}
}
