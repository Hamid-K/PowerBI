using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Results
{
	// Token: 0x020000AB RID: 171
	public class RedirectToRouteResult : IHttpActionResult
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		public RedirectToRouteResult(string routeName, IDictionary<string, object> routeValues, UrlHelper urlFactory, HttpRequestMessage request)
			: this(routeName, routeValues, new RedirectToRouteResult.DirectDependencyProvider(urlFactory, request))
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000BDE3 File Offset: 0x00009FE3
		public RedirectToRouteResult(string routeName, IDictionary<string, object> routeValues, ApiController controller)
			: this(routeName, routeValues, new RedirectToRouteResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000BDF3 File Offset: 0x00009FF3
		private RedirectToRouteResult(string routeName, IDictionary<string, object> routeValues, RedirectToRouteResult.IDependencyProvider dependencies)
		{
			if (routeName == null)
			{
				throw new ArgumentNullException("routeName");
			}
			this._routeName = routeName;
			this._routeValues = routeValues;
			this._dependencies = dependencies;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000BE1E File Offset: 0x0000A01E
		public string RouteName
		{
			get
			{
				return this._routeName;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000BE26 File Offset: 0x0000A026
		public IDictionary<string, object> RouteValues
		{
			get
			{
				return this._routeValues;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000BE2E File Offset: 0x0000A02E
		public UrlHelper UrlFactory
		{
			get
			{
				return this._dependencies.UrlFactory;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000BE3B File Offset: 0x0000A03B
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000BE48 File Offset: 0x0000A048
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BE58 File Offset: 0x0000A058
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Found);
			try
			{
				string text = this._dependencies.UrlFactory.Link(this._routeName, this._routeValues);
				if (text == null)
				{
					throw new InvalidOperationException(SRResources.UrlHelper_LinkMustNotReturnNull);
				}
				httpResponseMessage.Headers.Location = new Uri(text);
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x04000100 RID: 256
		private readonly string _routeName;

		// Token: 0x04000101 RID: 257
		private readonly IDictionary<string, object> _routeValues;

		// Token: 0x04000102 RID: 258
		private readonly RedirectToRouteResult.IDependencyProvider _dependencies;

		// Token: 0x020001CC RID: 460
		private interface IDependencyProvider
		{
			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000B0D RID: 2829
			UrlHelper UrlFactory { get; }

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000B0E RID: 2830
			HttpRequestMessage Request { get; }
		}

		// Token: 0x020001CD RID: 461
		private sealed class DirectDependencyProvider : RedirectToRouteResult.IDependencyProvider
		{
			// Token: 0x06000B0F RID: 2831 RVA: 0x0001C78E File Offset: 0x0001A98E
			public DirectDependencyProvider(UrlHelper urlFactory, HttpRequestMessage request)
			{
				if (urlFactory == null)
				{
					throw new ArgumentNullException("urlFactory");
				}
				if (request == null)
				{
					throw new ArgumentNullException("request");
				}
				this._urlFactory = urlFactory;
				this._request = request;
			}

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
			public UrlHelper UrlFactory
			{
				get
				{
					return this._urlFactory;
				}
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
			public HttpRequestMessage Request
			{
				get
				{
					return this._request;
				}
			}

			// Token: 0x04000383 RID: 899
			private readonly UrlHelper _urlFactory;

			// Token: 0x04000384 RID: 900
			private readonly HttpRequestMessage _request;
		}

		// Token: 0x020001CE RID: 462
		private sealed class ApiControllerDependencyProvider : RedirectToRouteResult.IDependencyProvider
		{
			// Token: 0x06000B12 RID: 2834 RVA: 0x0001C7D0 File Offset: 0x0001A9D0
			public ApiControllerDependencyProvider(ApiController controller)
			{
				if (controller == null)
				{
					throw new ArgumentNullException("controller");
				}
				this._controller = controller;
			}

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0001C7ED File Offset: 0x0001A9ED
			public UrlHelper UrlFactory
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.UrlFactory;
				}
			}

			// Token: 0x170002F9 RID: 761
			// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001C800 File Offset: 0x0001AA00
			public HttpRequestMessage Request
			{
				get
				{
					this.EnsureResolved();
					return this._resolvedDependencies.Request;
				}
			}

			// Token: 0x06000B15 RID: 2837 RVA: 0x0001C814 File Offset: 0x0001AA14
			private void EnsureResolved()
			{
				if (this._resolvedDependencies == null)
				{
					HttpRequestMessage request = this._controller.Request;
					if (request == null)
					{
						throw new InvalidOperationException(SRResources.ApiController_RequestMustNotBeNull);
					}
					UrlHelper urlHelper = this._controller.Url ?? new UrlHelper(request);
					this._resolvedDependencies = new RedirectToRouteResult.DirectDependencyProvider(urlHelper, request);
				}
			}

			// Token: 0x04000385 RID: 901
			private readonly ApiController _controller;

			// Token: 0x04000386 RID: 902
			private RedirectToRouteResult.IDependencyProvider _resolvedDependencies;
		}
	}
}
