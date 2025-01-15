using System;
using System.Data.Common;
using System.Data.Entity.SqlServer.Resources;
using System.Data.SqlClient;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000011 RID: 17
	internal class SqlProviderUtilities
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000651C File Offset: 0x0000471C
		internal static SqlConnection GetRequiredSqlConnection(DbConnection connection)
		{
			SqlConnection sqlConnection = connection as SqlConnection;
			if (sqlConnection == null)
			{
				throw new ArgumentException(Strings.Mapping_Provider_WrongConnectionType(typeof(SqlConnection)));
			}
			return sqlConnection;
		}
	}
}
