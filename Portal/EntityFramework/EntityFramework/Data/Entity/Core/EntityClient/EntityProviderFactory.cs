using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Entity.Core.EntityClient
{
	// Token: 0x020005E0 RID: 1504
	public sealed class EntityProviderFactory : DbProviderFactory, IServiceProvider
	{
		// Token: 0x06004981 RID: 18817 RVA: 0x00104CD9 File Offset: 0x00102ED9
		private EntityProviderFactory()
		{
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x00104CE1 File Offset: 0x00102EE1
		public override DbCommand CreateCommand()
		{
			return new EntityCommand();
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00104CE8 File Offset: 0x00102EE8
		public override DbCommandBuilder CreateCommandBuilder()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x00104CEF File Offset: 0x00102EEF
		public override DbConnection CreateConnection()
		{
			return new EntityConnection();
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x00104CF6 File Offset: 0x00102EF6
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return new EntityConnectionStringBuilder();
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x00104CFD File Offset: 0x00102EFD
		public override DbDataAdapter CreateDataAdapter()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00104D04 File Offset: 0x00102F04
		public override DbParameter CreateParameter()
		{
			return new EntityParameter();
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00104D0B File Offset: 0x00102F0B
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x00104D12 File Offset: 0x00102F12
		object IServiceProvider.GetService(Type serviceType)
		{
			if (!(serviceType == typeof(DbProviderServices)))
			{
				return null;
			}
			return EntityProviderServices.Instance;
		}

		// Token: 0x040019F7 RID: 6647
		public static readonly EntityProviderFactory Instance = new EntityProviderFactory();
	}
}
