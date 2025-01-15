using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing.Conventions;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200006B RID: 107
	public class ODataActionSelector : IHttpActionSelector
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0000D35C File Offset: 0x0000B55C
		public ODataActionSelector(IHttpActionSelector innerSelector)
		{
			if (innerSelector == null)
			{
				throw Error.ArgumentNull("innerSelector");
			}
			this._innerSelector = innerSelector;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000D379 File Offset: 0x0000B579
		public ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor)
		{
			return this._innerSelector.GetActionMapping(controllerDescriptor);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D388 File Offset: 0x0000B588
		public HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			HttpRequestMessage request = controllerContext.Request;
			ODataPath path = request.ODataProperties().Path;
			IEnumerable<IODataRoutingConvention> routingConventions = request.GetRoutingConventions();
			IHttpRouteData routeData = controllerContext.RouteData;
			if (path == null || routingConventions == null || routeData.Values.ContainsKey(ODataRouteConstants.Action))
			{
				return this._innerSelector.SelectAction(controllerContext);
			}
			ILookup<string, HttpActionDescriptor> actionMapping = this._innerSelector.GetActionMapping(controllerContext.ControllerDescriptor);
			foreach (IODataRoutingConvention iodataRoutingConvention in routingConventions)
			{
				string text = iodataRoutingConvention.SelectAction(path, controllerContext, actionMapping);
				if (text != null)
				{
					routeData.Values[ODataRouteConstants.Action] = text;
					return this._innerSelector.SelectAction(controllerContext);
				}
			}
			throw new HttpResponseException(ODataActionSelector.CreateErrorResponse(request, HttpStatusCode.NotFound, Error.Format(SRResources.NoMatchingResource, new object[] { controllerContext.Request.RequestUri }), Error.Format(SRResources.NoRoutingHandlerToSelectAction, new object[] { path.PathTemplate })));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		private static HttpResponseMessage CreateErrorResponse(HttpRequestMessage request, HttpStatusCode statusCode, string message, string messageDetail)
		{
			HttpError httpError = new HttpError(message);
			if (HttpRequestMessageExtensions.ShouldIncludeErrorDetail(request))
			{
				httpError.Add("MessageDetail", messageDetail);
			}
			return HttpRequestMessageExtensions.CreateErrorResponse(request, statusCode, httpError);
		}

		// Token: 0x040000DA RID: 218
		private const string MessageDetailKey = "MessageDetail";

		// Token: 0x040000DB RID: 219
		private readonly IHttpActionSelector _innerSelector;
	}
}
