using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000157 RID: 343
	public interface IHttpRouteInfoProvider
	{
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000941 RID: 2369
		string Name { get; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000942 RID: 2370
		string Template { get; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000943 RID: 2371
		int Order { get; }
	}
}
