using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000486 RID: 1158
	internal interface ISapBwLegacyServiceFactory
	{
		// Token: 0x0600268E RID: 9870
		ISapBwService CreateTestService(IEngineHost host, IResource resource, SapBwOptions options, SapBwOlapDataSourceLocation location, SapBwRouterString routerString, IDbProviderFactoryService factoryService);
	}
}
