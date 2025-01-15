using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Hosting;
using System.Web.Http.Properties;
using System.Web.Http.Routing;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x0200007D RID: 125
	public class HttpRoutingDispatcher : HttpMessageHandler
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00009368 File Offset: 0x00007568
		public HttpRoutingDispatcher(HttpConfiguration configuration)
			: this(configuration, new HttpControllerDispatcher(configuration))
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009377 File Offset: 0x00007577
		public HttpRoutingDispatcher(HttpConfiguration configuration, HttpMessageHandler defaultHandler)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			if (defaultHandler == null)
			{
				throw Error.ArgumentNull("defaultHandler");
			}
			this._configuration = configuration;
			this._defaultInvoker = new HttpMessageInvoker(defaultHandler);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000093B0 File Offset: 0x000075B0
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			IHttpRouteData httpRouteData = request.GetRouteData();
			if (httpRouteData == null)
			{
				httpRouteData = this._configuration.Routes.GetRouteData(request);
				if (httpRouteData != null)
				{
					request.SetRouteData(httpRouteData);
				}
			}
			if (httpRouteData == null || (httpRouteData.Route != null && httpRouteData.Route.Handler is StopRoutingHandler))
			{
				request.Properties.Add(HttpPropertyKeys.NoRouteMatched, true);
				return Task.FromResult<HttpResponseMessage>(request.CreateErrorResponse(HttpStatusCode.NotFound, Error.Format(SRResources.ResourceNotFound, new object[] { request.RequestUri }), SRResources.NoRouteData));
			}
			httpRouteData.RemoveOptionalRoutingParameters();
			return ((httpRouteData.Route == null || httpRouteData.Route.Handler == null) ? this._defaultInvoker : new HttpMessageInvoker(httpRouteData.Route.Handler, false)).SendAsync(request, cancellationToken);
		}

		// Token: 0x040000AE RID: 174
		private readonly HttpConfiguration _configuration;

		// Token: 0x040000AF RID: 175
		private readonly HttpMessageInvoker _defaultInvoker;
	}
}
