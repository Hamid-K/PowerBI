using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x0200014F RID: 335
	internal class LinkGenerationRoute : IHttpRoute
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x00016FF6 File Offset: 0x000151F6
		public LinkGenerationRoute(IHttpRoute innerRoute)
		{
			if (innerRoute == null)
			{
				throw new ArgumentNullException("innerRoute");
			}
			this._innerRoute = innerRoute;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00017013 File Offset: 0x00015213
		public string RouteTemplate
		{
			get
			{
				return this._innerRoute.RouteTemplate;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00017020 File Offset: 0x00015220
		public IDictionary<string, object> Defaults
		{
			get
			{
				return this._innerRoute.Defaults;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0001702D File Offset: 0x0001522D
		public IDictionary<string, object> Constraints
		{
			get
			{
				return this._innerRoute.Constraints;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001703A File Offset: 0x0001523A
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return this._innerRoute.DataTokens;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0000413B File Offset: 0x0000233B
		public HttpMessageHandler Handler
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000413B File Offset: 0x0000233B
		public IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request)
		{
			return null;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00017047 File Offset: 0x00015247
		public IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			return this._innerRoute.GetVirtualPath(request, values);
		}

		// Token: 0x04000276 RID: 630
		private readonly IHttpRoute _innerRoute;
	}
}
