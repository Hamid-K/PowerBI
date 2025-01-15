using System;
using System.Data.Common;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B1 RID: 4273
	internal abstract class DelegatingDbProviderFactory : DbProviderFactory
	{
		// Token: 0x06006FEB RID: 28651 RVA: 0x00181867 File Offset: 0x0017FA67
		public DelegatingDbProviderFactory(DbProviderFactory factory)
		{
			this.factory = factory;
		}

		// Token: 0x17001F82 RID: 8066
		// (get) Token: 0x06006FEC RID: 28652 RVA: 0x00181876 File Offset: 0x0017FA76
		public DbProviderFactory InnerFactory
		{
			get
			{
				return this.factory;
			}
		}

		// Token: 0x17001F83 RID: 8067
		// (get) Token: 0x06006FED RID: 28653 RVA: 0x0018187E File Offset: 0x0017FA7E
		public override bool CanCreateDataSourceEnumerator
		{
			get
			{
				return this.factory.CanCreateDataSourceEnumerator;
			}
		}

		// Token: 0x06006FEE RID: 28654 RVA: 0x0018188B File Offset: 0x0017FA8B
		public override DbCommand CreateCommand()
		{
			return this.factory.CreateCommand();
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x00181898 File Offset: 0x0017FA98
		public override DbConnection CreateConnection()
		{
			return this.factory.CreateConnection();
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x001818A5 File Offset: 0x0017FAA5
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			return this.factory.CreateConnectionStringBuilder();
		}

		// Token: 0x06006FF1 RID: 28657 RVA: 0x001818B2 File Offset: 0x0017FAB2
		public override DbDataAdapter CreateDataAdapter()
		{
			return this.factory.CreateDataAdapter();
		}

		// Token: 0x06006FF2 RID: 28658 RVA: 0x001818BF File Offset: 0x0017FABF
		public override DbDataSourceEnumerator CreateDataSourceEnumerator()
		{
			return this.factory.CreateDataSourceEnumerator();
		}

		// Token: 0x06006FF3 RID: 28659 RVA: 0x001818CC File Offset: 0x0017FACC
		public override DbParameter CreateParameter()
		{
			return this.factory.CreateParameter();
		}

		// Token: 0x06006FF4 RID: 28660 RVA: 0x001818D9 File Offset: 0x0017FAD9
		public override CodeAccessPermission CreatePermission(PermissionState state)
		{
			return this.factory.CreatePermission(state);
		}

		// Token: 0x04003DF4 RID: 15860
		private readonly DbProviderFactory factory;
	}
}
