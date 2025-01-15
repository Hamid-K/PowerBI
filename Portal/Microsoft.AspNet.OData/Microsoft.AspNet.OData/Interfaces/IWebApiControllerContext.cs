using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Routing.Conventions;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x0200005D RID: 93
	internal interface IWebApiControllerContext
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000292 RID: 658
		SelectControllerResult ControllerResult { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000293 RID: 659
		IWebApiRequestMessage Request { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000294 RID: 660
		IDictionary<string, object> RouteData { get; }
	}
}
