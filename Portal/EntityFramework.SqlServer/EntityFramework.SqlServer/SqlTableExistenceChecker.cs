using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Text;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000016 RID: 22
	internal class SqlTableExistenceChecker : TableExistenceChecker
	{
		// Token: 0x0600022D RID: 557 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		public override bool AnyModelTableExistsInDatabase(ObjectContext context, DbConnection connection, IEnumerable<EntitySet> modelTables, string edmMetadataContextTableName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (EntitySet entitySet in modelTables)
			{
				stringBuilder.Append("'");
				stringBuilder.Append((string)entitySet.MetadataProperties["Schema"].Value);
				stringBuilder.Append(".");
				stringBuilder.Append(this.GetTableName(entitySet));
				stringBuilder.Append("',");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			bool flag2;
			using (DbCommand command = connection.CreateCommand())
			{
				DbCommand command2 = command;
				string[] array = new string[5];
				array[0] = "\r\nSELECT Count(*)\r\nFROM INFORMATION_SCHEMA.TABLES AS t\r\nWHERE t.TABLE_SCHEMA + '.' + t.TABLE_NAME IN (";
				int num = 1;
				StringBuilder stringBuilder2 = stringBuilder;
				array[num] = ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
				array[2] = ")\r\n    OR t.TABLE_NAME = '";
				array[3] = edmMetadataContextTableName;
				array[4] = "'";
				command2.CommandText = string.Concat(array);
				bool flag = true;
				if (DbInterception.Dispatch.Connection.GetState(connection, context.InterceptionContext) == ConnectionState.Open)
				{
					flag = false;
					EntityTransaction currentTransaction = ((EntityConnection)context.Connection).CurrentTransaction;
					if (currentTransaction != null)
					{
						command.Transaction = currentTransaction.StoreTransaction;
					}
				}
				IDbExecutionStrategy executionStrategy = DbProviderServices.GetExecutionStrategy(connection);
				try
				{
					flag2 = executionStrategy.Execute<bool>(delegate
					{
						if (DbInterception.Dispatch.Connection.GetState(connection, context.InterceptionContext) == ConnectionState.Broken)
						{
							DbInterception.Dispatch.Connection.Close(connection, context.InterceptionContext);
						}
						if (DbInterception.Dispatch.Connection.GetState(connection, context.InterceptionContext) == ConnectionState.Closed)
						{
							DbInterception.Dispatch.Connection.Open(connection, context.InterceptionContext);
						}
						return (int)DbInterception.Dispatch.Command.Scalar(command, new DbCommandInterceptionContext(context.InterceptionContext)) > 0;
					});
				}
				finally
				{
					if (flag && DbInterception.Dispatch.Connection.GetState(connection, context.InterceptionContext) != ConnectionState.Closed)
					{
						DbInterception.Dispatch.Connection.Close(connection, context.InterceptionContext);
					}
				}
			}
			return flag2;
		}
	}
}
