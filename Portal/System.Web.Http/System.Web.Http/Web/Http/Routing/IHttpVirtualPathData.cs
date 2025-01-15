using System;

namespace System.Web.Http.Routing
{
	// Token: 0x0200015B RID: 347
	public interface IHttpVirtualPathData
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000953 RID: 2387
		IHttpRoute Route { get; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000954 RID: 2388
		// (set) Token: 0x06000955 RID: 2389
		string VirtualPath { get; set; }
	}
}
