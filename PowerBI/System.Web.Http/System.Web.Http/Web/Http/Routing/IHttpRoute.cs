using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x0200015D RID: 349
	public interface IHttpRoute
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000958 RID: 2392
		string RouteTemplate { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000959 RID: 2393
		IDictionary<string, object> Defaults { get; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600095A RID: 2394
		IDictionary<string, object> Constraints { get; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600095B RID: 2395
		IDictionary<string, object> DataTokens { get; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600095C RID: 2396
		HttpMessageHandler Handler { get; }

		// Token: 0x0600095D RID: 2397
		IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request);

		// Token: 0x0600095E RID: 2398
		IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values);
	}
}
