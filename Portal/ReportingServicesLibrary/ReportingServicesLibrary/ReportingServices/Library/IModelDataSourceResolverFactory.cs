using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000179 RID: 377
	internal interface IModelDataSourceResolverFactory
	{
		// Token: 0x06000DDB RID: 3547
		IModelDataSourceResolver CreateDataSourceResolver(string itemPath, CatalogItemContext itemContext, IRSHostingService service);
	}
}
