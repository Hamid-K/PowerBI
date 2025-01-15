using System;
using System.Data.Common;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200023C RID: 572
	internal class DefaultDbProviderFactoryResolver : IDbProviderFactoryResolver
	{
		// Token: 0x06001E2F RID: 7727 RVA: 0x000544B6 File Offset: 0x000526B6
		public DbProviderFactory ResolveProviderFactory(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			return DbProviderFactories.GetFactory(connection);
		}
	}
}
