using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009BB RID: 2491
	public sealed class DrdaFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06004D02 RID: 19714 RVA: 0x00134C2A File Offset: 0x00132E2A
		public override DbCommand CreateCommand()
		{
			return new DrdaCommand();
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x00134C31 File Offset: 0x00132E31
		public override DbCommandBuilder CreateCommandBuilder()
		{
			return new DrdaCommandBuilder();
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x00134C38 File Offset: 0x00132E38
		public override DbConnection CreateConnection()
		{
			return new DrdaConnection();
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x00134C3F File Offset: 0x00132E3F
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new DrdaConnectionStringBuilder();
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x00134C46 File Offset: 0x00132E46
		public override DbDataAdapter CreateDataAdapter()
		{
			return new DrdaDataAdapter();
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x00134C4D File Offset: 0x00132E4D
		public override DbParameter CreateParameter()
		{
			return new DrdaParameter();
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x00134C54 File Offset: 0x00132E54
		object IServiceProvider.GetService(Type serviceType)
		{
			object obj = null;
			if (serviceType == EntityClientUtils.SystemDataCommonDbProviderServicesType)
			{
				obj = EntityClientUtils.ProviderServicesInstance();
			}
			return obj;
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x00134C77 File Offset: 0x00132E77
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return new DrdaPermission(state);
		}

		// Token: 0x04003D06 RID: 15622
		public static readonly DrdaFactory Instance = new DrdaFactory();
	}
}
