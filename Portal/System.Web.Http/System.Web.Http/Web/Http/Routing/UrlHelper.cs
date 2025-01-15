using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x0200016C RID: 364
	public class UrlHelper
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public UrlHelper()
		{
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00018DA2 File Offset: 0x00016FA2
		public UrlHelper(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this.Request = request;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00018DBF File Offset: 0x00016FBF
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00018DC7 File Offset: 0x00016FC7
		public HttpRequestMessage Request
		{
			get
			{
				return this._request;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._request = value;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00018DDC File Offset: 0x00016FDC
		public virtual string Content(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw Error.ArgumentNullOrEmpty("path");
			}
			if (this.Request == null)
			{
				throw Error.InvalidOperation(SRResources.RequestIsNull, new object[] { "UrlHelper" });
			}
			if (path.StartsWith("~/", StringComparison.Ordinal))
			{
				HttpRequestContext requestContext = this.Request.GetRequestContext();
				string text;
				if (requestContext != null)
				{
					text = requestContext.VirtualPathRoot;
				}
				else
				{
					HttpConfiguration configuration = this.Request.GetConfiguration();
					if (configuration == null)
					{
						throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
					}
					text = configuration.VirtualPathRoot;
				}
				if (text == null)
				{
					text = "/";
				}
				if (!text.StartsWith("/", StringComparison.Ordinal))
				{
					text = "/" + text;
				}
				if (!text.EndsWith("/", StringComparison.Ordinal))
				{
					text += "/";
				}
				return new Uri(this.Request.RequestUri, text + path.Substring("~/".Length)).AbsoluteUri;
			}
			return new Uri(this.Request.RequestUri, path).AbsoluteUri;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00018EEC File Offset: 0x000170EC
		public virtual string Route(string routeName, object routeValues)
		{
			return this.Route(routeName, new HttpRouteValueDictionary(routeValues));
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00018EFB File Offset: 0x000170FB
		public virtual string Route(string routeName, IDictionary<string, object> routeValues)
		{
			return UrlHelper.GetVirtualPath(this.Request, routeName, routeValues);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00018F0A File Offset: 0x0001710A
		public virtual string Link(string routeName, object routeValues)
		{
			return this.Link(routeName, new HttpRouteValueDictionary(routeValues));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00018F1C File Offset: 0x0001711C
		public virtual string Link(string routeName, IDictionary<string, object> routeValues)
		{
			string text = this.Route(routeName, routeValues);
			if (!string.IsNullOrEmpty(text))
			{
				text = new Uri(this.Request.RequestUri, text).AbsoluteUri;
			}
			return text;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00018F54 File Offset: 0x00017154
		private static string GetVirtualPath(HttpRequestMessage request, string routeName, IDictionary<string, object> routeValues)
		{
			if (routeValues == null)
			{
				routeValues = new HttpRouteValueDictionary();
				routeValues.Add(HttpRoute.HttpRouteKey, true);
			}
			else
			{
				routeValues = new HttpRouteValueDictionary(routeValues);
				if (!routeValues.ContainsKey(HttpRoute.HttpRouteKey))
				{
					routeValues.Add(HttpRoute.HttpRouteKey, true);
				}
			}
			HttpConfiguration configuration = request.GetConfiguration();
			if (configuration == null)
			{
				throw Error.InvalidOperation(SRResources.HttpRequestMessageExtensions_NoConfiguration, new object[0]);
			}
			IHttpVirtualPathData virtualPath = configuration.Routes.GetVirtualPath(request, routeName, routeValues);
			if (virtualPath == null)
			{
				return null;
			}
			return virtualPath.VirtualPath;
		}

		// Token: 0x0400029C RID: 668
		private HttpRequestMessage _request;
	}
}
