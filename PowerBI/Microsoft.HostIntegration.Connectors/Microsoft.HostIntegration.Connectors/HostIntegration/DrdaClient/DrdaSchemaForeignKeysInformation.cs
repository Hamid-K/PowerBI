using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A1E RID: 2590
	internal class DrdaSchemaForeignKeysInformation : DrdaSchemaInformation
	{
		// Token: 0x0600515C RID: 20828 RVA: 0x00147C3E File Offset: 0x00145E3E
		public DrdaSchemaForeignKeysInformation()
			: base("ForeignKeys", DrdaSchemaForeignKeysInformation.SchemaRestrictions, 6, DrdaSchemaForeignKeysInformation.SchemaResultColumns)
		{
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x00147C58 File Offset: 0x00145E58
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			bool useSQL = base.CanUseSQL(conn);
			if (!useSQL)
			{
				try
				{
					await statement.ExecuteAsync("CALL SYSIBM.SQLFOREIGNKEYS (?, ?, ?, ?, ?, ?, ?)", base.GetParameters(conn.Requester.HostType, parameters, 0, options), true, false, isAsync, cancellationToken);
				}
				catch (DrdaException)
				{
					useSQL = true;
				}
			}
			if (useSQL)
			{
				string query = DrdaSchemaForeignKeysInformation.ForeignKeysQuery.GetQuery(conn, parameters);
				if (query.Length == 0)
				{
					throw new NotSupportedException();
				}
				await statement.ExecuteAsync(query, null, true, false, isAsync, cancellationToken);
			}
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00147CD0 File Offset: 0x00145ED0
		public override object GetRestrictionValue(DrdaConnection conn, int i)
		{
			if ((i == 1 || i == 4) && conn.DrdaConnectionString.DefaultSchema.Length > 0)
			{
				return conn.DrdaConnectionString.DefaultSchema;
			}
			return base.GetRestrictionValue(conn, i);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00147D04 File Offset: 0x00145F04
		public override void GetResultValues(DrdaConnection connection, DrdaSchemaResultSet resultSet, bool[] hasColumns)
		{
			for (int i = 0; i < resultSet.FieldCount; i++)
			{
				object obj = resultSet.QueryResultSet.GetValue(i);
				if ((i == 0 || i == 3) && (obj == null || (obj is string && ((string)obj).ToString().Length == 0)))
				{
					obj = connection.Database;
				}
				resultSet.GetColumn(i).Value = obj;
			}
		}

		// Token: 0x0400400C RID: 16396
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("PK_TABLE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("PK_TABLE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("PK_TABLE_NAME", typeof(string), null, 128),
			new DrdaSchemaRestriction("FK_TABLE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("FK_TABLE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("FK_TABLE_NAME", typeof(string), null, 128)
		};

		// Token: 0x0400400D RID: 16397
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("PKTableCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("PKTableSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("PKTableName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("PKColumnName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("FKTableCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("FKTableSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("FKTableName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("FKColumnName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("KeySequence", typeof(int), null),
			new DrdaSchemaResultColumn("UpdateRule", typeof(int), null),
			new DrdaSchemaResultColumn("DeleteRule", typeof(int), null),
			new DrdaSchemaResultColumn("ForeignKeyName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("PrimaryKeyName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("Deferrability", typeof(int), null)
		};

		// Token: 0x0400400E RID: 16398
		private static DrdaForeignKeysSchemaQuery ForeignKeysQuery = new DrdaForeignKeysSchemaQuery();
	}
}
