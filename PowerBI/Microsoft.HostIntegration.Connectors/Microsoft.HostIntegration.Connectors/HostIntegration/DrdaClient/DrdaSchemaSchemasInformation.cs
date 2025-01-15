using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A13 RID: 2579
	internal class DrdaSchemaSchemasInformation : DrdaSchemaInformation
	{
		// Token: 0x0600513F RID: 20799 RVA: 0x001460E2 File Offset: 0x001442E2
		public DrdaSchemaSchemasInformation()
			: base("Schemas", DrdaSchemaSchemasInformation.SchemaRestrictions, 0, DrdaSchemaSchemasInformation.SchemaResultColumns)
		{
		}

		// Token: 0x06005140 RID: 20800 RVA: 0x001460FC File Offset: 0x001442FC
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			if (conn == null || conn.State != global::System.Data.ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			string text = "SELECT SCHEMANAME FROM SYSCAT.SCHEMATA ORDER BY SCHEMANAME";
			if (conn.Requester.Flavor == DrdaFlavor.Informix)
			{
				text = "select distinct owner from informix.systables union select distinct owner from informix.sysprocedures ORDER BY owner";
			}
			else
			{
				string serverClass = conn.ServerClass;
				if (serverClass != null)
				{
					if (!(serverClass == "DB2/400"))
					{
						if (serverClass == "DB2/MVS")
						{
							text = "WITH SCHEMAS_CTE( CREATOR ) AS (SELECT DISTINCT CREATOR FROM SYSIBM.SYSTABLES UNION SELECT DISTINCT CREATOR FROM SYSIBM.SYSINDEXES UNION SELECT DISTINCT SCHEMA AS CREATOR FROM SYSIBM.SYSROUTINES UNION SELECT DISTINCT CREATOR FROM SYSIBM.SYSSYNONYMS UNION SELECT DISTINCT JARSCHEMA AS CREATOR FROM SYSIBM.SYSJAROBJECTS UNION SELECT DISTINCT OWNER AS CREATOR FROM SYSIBM.SYSPACKAGE UNION SELECT DISTINCT SCHEMA AS CREATOR FROM SYSIBM.SYSSEQUENCES) SELECT * FROM SCHEMAS_CTE ORDER BY CREATOR FOR READ ONLY";
						}
					}
					else
					{
						text = "SELECT DISTINCT TABLE_SCHEMA SCHEMA FROM QSYS2.SYSTABLES UNION SELECT DISTINCT ROUTINE_SCHEMA SCHEMA FROM QSYS2.SYSROUTINES ORDER BY SCHEMA";
					}
				}
			}
			await statement.ExecuteAsync(text, null, true, false, isAsync, cancellationToken);
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x0014615C File Offset: 0x0014435C
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			DrdaResultSet queryResultSet = resultSet.QueryResultSet;
			resultSet.GetColumn(0).Value = queryResultSet.GetValue(0);
		}

		// Token: 0x04003FC5 RID: 16325
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[0];

		// Token: 0x04003FC6 RID: 16326
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("Schema", typeof(string), null, 128)
		};
	}
}
