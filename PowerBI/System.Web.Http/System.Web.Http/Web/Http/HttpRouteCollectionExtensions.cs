using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http.Batch;
using System.Web.Http.Routing;

namespace System.Web.Http
{
	// Token: 0x0200002F RID: 47
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRouteCollectionExtensions
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00004BFC File Offset: 0x00002DFC
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate)
		{
			return routes.MapHttpRoute(name, routeTemplate, null, null, null);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004C09 File Offset: 0x00002E09
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults)
		{
			return routes.MapHttpRoute(name, routeTemplate, defaults, null, null);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004C16 File Offset: 0x00002E16
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints)
		{
			return routes.MapHttpRoute(name, routeTemplate, defaults, constraints, null);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004C24 File Offset: 0x00002E24
		public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler)
		{
			if (routes == null)
			{
				throw Error.ArgumentNull("routes");
			}
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary(defaults);
			HttpRouteValueDictionary httpRouteValueDictionary2 = new HttpRouteValueDictionary(constraints);
			IHttpRoute httpRoute = routes.CreateRoute(routeTemplate, httpRouteValueDictionary, httpRouteValueDictionary2, null, handler);
			routes.Add(name, httpRoute);
			return httpRoute;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004C64 File Offset: 0x00002E64
		public static IHttpRoute MapHttpBatchRoute(this HttpRouteCollection routes, string routeName, string routeTemplate, HttpBatchHandler batchHandler)
		{
			return routes.MapHttpRoute(routeName, routeTemplate, null, null, batchHandler);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004C71 File Offset: 0x00002E71
		public static IHttpRoute IgnoreRoute(this HttpRouteCollection routes, string routeName, string routeTemplate)
		{
			return routes.IgnoreRoute(routeName, routeTemplate, null);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004C7C File Offset: 0x00002E7C
		public static IHttpRoute IgnoreRoute(this HttpRouteCollection routes, string routeName, string routeTemplate, object constraints)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}
			if (routeName == null)
			{
				throw new ArgumentNullException("routeName");
			}
			if (routeTemplate == null)
			{
				throw new ArgumentNullException("routeTemplate");
			}
			HttpRouteCollectionExtensions.IgnoreHttpRouteInternal ignoreHttpRouteInternal = new HttpRouteCollectionExtensions.IgnoreHttpRouteInternal(routeTemplate, new HttpRouteValueDictionary(constraints), new StopRoutingHandler());
			routes.Add(routeName, ignoreHttpRouteInternal);
			return ignoreHttpRouteInternal;
		}

		// Token: 0x0200019A RID: 410
		private sealed class IgnoreHttpRouteInternal : HttpRoute
		{
			// Token: 0x06000A4C RID: 2636 RVA: 0x0001AC82 File Offset: 0x00018E82
			public IgnoreHttpRouteInternal(string routeTemplate, HttpRouteValueDictionary constraints, HttpMessageHandler handler)
				: base(routeTemplate, null, constraints, null, handler)
			{
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x0000413B File Offset: 0x0000233B
			public override IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
			{
				return null;
			}
		}
	}
}
