using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200010D RID: 269
	public class HttpControllerContext
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x000118E8 File Offset: 0x0000FAE8
		public HttpControllerContext(HttpRequestContext requestContext, HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, IHttpController controller)
		{
			if (requestContext == null)
			{
				throw Error.ArgumentNull("requestContext");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			if (controller == null)
			{
				throw Error.ArgumentNull("controller");
			}
			this._requestContext = requestContext;
			this._request = request;
			this._controllerDescriptor = controllerDescriptor;
			this._controller = controller;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00011954 File Offset: 0x0000FB54
		public HttpControllerContext(HttpConfiguration configuration, IHttpRouteData routeData, HttpRequestMessage request)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (routeData == null)
			{
				throw Error.ArgumentNull("routeData");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			this._requestContext = new HttpRequestContext
			{
				Configuration = configuration,
				RouteData = routeData
			};
			this._request = request;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000119B1 File Offset: 0x0000FBB1
		public HttpControllerContext()
		{
			this._requestContext = new HttpRequestContext();
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x000119C4 File Offset: 0x0000FBC4
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x000119D1 File Offset: 0x0000FBD1
		public HttpConfiguration Configuration
		{
			get
			{
				return this._requestContext.Configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._requestContext.Configuration = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x000119E8 File Offset: 0x0000FBE8
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x000119F0 File Offset: 0x0000FBF0
		public HttpControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerDescriptor = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00011A02 File Offset: 0x0000FC02
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00011A0A File Offset: 0x0000FC0A
		public IHttpController Controller
		{
			get
			{
				return this._controller;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controller = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00011A1C File Offset: 0x0000FC1C
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00011A24 File Offset: 0x0000FC24
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

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00011A36 File Offset: 0x0000FC36
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00011A3E File Offset: 0x0000FC3E
		public HttpRequestContext RequestContext
		{
			get
			{
				return this._requestContext;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._requestContext = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x00011A50 File Offset: 0x0000FC50
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x00011A5D File Offset: 0x0000FC5D
		public IHttpRouteData RouteData
		{
			get
			{
				return this._requestContext.RouteData;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._requestContext.RouteData = value;
			}
		}

		// Token: 0x040001CD RID: 461
		private HttpRequestContext _requestContext;

		// Token: 0x040001CE RID: 462
		private HttpRequestMessage _request;

		// Token: 0x040001CF RID: 463
		private HttpControllerDescriptor _controllerDescriptor;

		// Token: 0x040001D0 RID: 464
		private IHttpController _controller;
	}
}
