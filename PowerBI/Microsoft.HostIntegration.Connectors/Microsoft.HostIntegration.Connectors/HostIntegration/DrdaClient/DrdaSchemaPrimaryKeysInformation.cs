using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A1C RID: 2588
	internal class DrdaSchemaPrimaryKeysInformation : DrdaSchemaInformation
	{
		// Token: 0x06005157 RID: 20823 RVA: 0x00147886 File Offset: 0x00145A86
		public DrdaSchemaPrimaryKeysInformation()
			: base("PrimaryKeys", DrdaSchemaPrimaryKeysInformation.SchemaRestrictions, 3, DrdaSchemaPrimaryKeysInformation.SchemaResultColumns)
		{
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x001478A0 File Offset: 0x00145AA0
		public override async Task ExecuteAsync(DrdaConnection conn, ISqlStatement statement, DrdaParameterCollection parameters, string options, bool isAsync, CancellationToken cancellationToken)
		{
			bool useSQL = base.CanUseSQL(conn);
			if (!useSQL)
			{
				try
				{
					await statement.ExecuteAsync("CALL SYSIBM.SQLPRIMARYKEYS (?, ?, ?, ?)", base.GetParameters(conn.Requester.HostType, parameters, 0, options), true, false, isAsync, cancellationToken);
				}
				catch (DrdaException)
				{
					useSQL = true;
				}
			}
			if (useSQL)
			{
				await statement.ExecuteAsync(DrdaSchemaPrimaryKeysInformation.PrimaryKeysQuery.GetQuery(conn, parameters), null, true, false, isAsync, cancellationToken);
			}
		}

		// Token: 0x04003FFE RID: 16382
		private static DrdaSchemaRestriction[] SchemaRestrictions = new DrdaSchemaRestriction[]
		{
			new DrdaSchemaRestriction("TABLE_CATALOG", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_SCHEMA", typeof(string), null, 128),
			new DrdaSchemaRestriction("TABLE_NAME", typeof(string), null, 128)
		};

		// Token: 0x04003FFF RID: 16383
		private static DrdaSchemaResultColumn[] SchemaResultColumns = new DrdaSchemaResultColumn[]
		{
			new DrdaSchemaResultColumn("TableCatalog", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableSchema", typeof(string), null, 128),
			new DrdaSchemaResultColumn("TableName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("ColumnName", typeof(string), null, 128),
			new DrdaSchemaResultColumn("KeySequence", typeof(int), null),
			new DrdaSchemaResultColumn("Keyname", typeof(string), null, 128)
		};

		// Token: 0x04004000 RID: 16384
		private static DrdaPrimaryKeysSchemaQuery PrimaryKeysQuery = new DrdaPrimaryKeysSchemaQuery();
	}
}
