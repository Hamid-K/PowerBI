using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Resources;
using System.Data.SqlClient;
using System.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000077 RID: 119
	internal static class DbProviderFactoryExtensions
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public static string GetProviderInvariantName(this DbProviderFactory factory)
		{
			IEnumerable<DataRow> enumerable = DbProviderFactories.GetFactoryClasses().Rows.OfType<DataRow>();
			DataRow dataRow = new ProviderRowFinder().FindRow(factory.GetType(), (DataRow r) => DbProviderFactories.GetFactory(r).GetType() == factory.GetType(), enumerable);
			if (dataRow != null)
			{
				return (string)dataRow[2];
			}
			if (factory.GetType() == typeof(SqlClientFactory))
			{
				return "System.Data.SqlClient";
			}
			throw new NotSupportedException(Strings.ProviderNameNotFound(factory));
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		internal static DbProviderServices GetProviderServices(this DbProviderFactory factory)
		{
			if (factory is EntityProviderFactory)
			{
				return EntityProviderServices.Instance;
			}
			IProviderInvariantName service = DbConfiguration.DependencyResolver.GetService(factory);
			return DbConfiguration.DependencyResolver.GetService(service.Name);
		}
	}
}
