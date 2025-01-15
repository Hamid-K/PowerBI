using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200017A RID: 378
	internal sealed class ModelDataSourceResolverFactory : IModelDataSourceResolverFactory
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x00032B20 File Offset: 0x00030D20
		public IModelDataSourceResolver CreateDataSourceResolver(string itemPath, CatalogItemContext itemContext, IRSHostingService service)
		{
			return new DataSourceResolver(itemPath, itemContext, (RSService)service);
		}

		// Token: 0x040005B6 RID: 1462
		public static readonly IModelDataSourceResolverFactory Instance = new ModelDataSourceResolverFactory();
	}
}
