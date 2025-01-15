using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x0200015C RID: 348
	public interface IHttpRouteData
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000956 RID: 2390
		IHttpRoute Route { get; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000957 RID: 2391
		IDictionary<string, object> Values { get; }
	}
}
