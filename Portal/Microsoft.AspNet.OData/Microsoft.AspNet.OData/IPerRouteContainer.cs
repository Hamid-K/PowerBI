using System;
using Microsoft.OData;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200001A RID: 26
	public interface IPerRouteContainer
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AC RID: 172
		// (set) Token: 0x060000AD RID: 173
		Func<IContainerBuilder> BuilderFactory { get; set; }

		// Token: 0x060000AE RID: 174
		IServiceProvider CreateODataRootContainer(string routeName, Action<IContainerBuilder> configureAction);

		// Token: 0x060000AF RID: 175
		bool HasODataRootContainer(string routeName);

		// Token: 0x060000B0 RID: 176
		IServiceProvider GetODataRootContainer(string routeName);
	}
}
