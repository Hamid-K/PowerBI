using System;
using System.Data.Common;
using System.Data.Sql;
using System.Security;
using System.Security.Permissions;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000DA RID: 218
	public sealed class SqlClientFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x00033164 File Offset: 0x00031364
		private SqlClientFactory()
		{
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0000EBAD File Offset: 0x0000CDAD
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003316C File Offset: 0x0003136C
		public override DbCommand CreateCommand()
		{
			return new SqlCommand();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x00033173 File Offset: 0x00031373
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new SqlCommandBuilder();
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003317A File Offset: 0x0003137A
		public override DbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00033181 File Offset: 0x00031381
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new SqlConnectionStringBuilder();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00033188 File Offset: 0x00031388
		public override DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003318F File Offset: 0x0003138F
		public override DbParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00033196 File Offset: 0x00031396
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new SqlClientPermission(state);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003319E File Offset: 0x0003139E
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return SqlDataSourceEnumerator.Instance;
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000331A8 File Offset: 0x000313A8
		object IServiceProvider.GetService(Type serviceType)
		{
			object obj = null;
			if (serviceType == GreenMethods.SystemDataCommonDbProviderServices_Type)
			{
				obj = GreenMethods.MicrosoftDataSqlClientSqlProviderServices_Instance();
			}
			return obj;
		}

		// Token: 0x04000697 RID: 1687
		public static readonly SqlClientFactory Instance = new SqlClientFactory();
	}
}
