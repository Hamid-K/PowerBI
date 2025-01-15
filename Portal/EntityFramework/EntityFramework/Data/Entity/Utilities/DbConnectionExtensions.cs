using System;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000073 RID: 115
	internal static class DbConnectionExtensions
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000F90D File Offset: 0x0000DB0D
		public static string GetProviderInvariantName(this DbConnection connection)
		{
			return DbConfiguration.DependencyResolver.GetService(DbProviderServices.GetProviderFactory(connection)).Name;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000F924 File Offset: 0x0000DB24
		public static DbProviderInfo GetProviderInfo(this DbConnection connection, out DbProviderManifest providerManifest)
		{
			string text = DbConfiguration.DependencyResolver.GetService<IManifestTokenResolver>().ResolveManifestToken(connection);
			DbProviderInfo dbProviderInfo = new DbProviderInfo(connection.GetProviderInvariantName(), text);
			providerManifest = DbProviderServices.GetProviderServices(connection).GetProviderManifest(text);
			return dbProviderInfo;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public static DbProviderFactory GetProviderFactory(this DbConnection connection)
		{
			return DbConfiguration.DependencyResolver.GetService<IDbProviderFactoryResolver>().ResolveProviderFactory(connection);
		}
	}
}
