using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Extensions
{
	// Token: 0x020001C7 RID: 455
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class UrlHelperExtensions
	{
		// Token: 0x06000F1D RID: 3869 RVA: 0x0003E2BE File Offset: 0x0003C4BE
		public static string CreateODataLink(this UrlHelper urlHelper, params ODataPathSegment[] segments)
		{
			return urlHelper.CreateODataLink(segments);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003E2C8 File Offset: 0x0003C4C8
		public static string CreateODataLink(this UrlHelper urlHelper, IList<ODataPathSegment> segments)
		{
			if (urlHelper == null)
			{
				throw Error.ArgumentNull("urlHelper");
			}
			HttpRequestMessage request = urlHelper.Request;
			string routeName = request.ODataProperties().RouteName;
			if (string.IsNullOrEmpty(routeName))
			{
				throw Error.InvalidOperation(SRResources.RequestMustHaveODataRouteName, new object[0]);
			}
			IODataPathHandler pathHandler = request.GetPathHandler();
			return urlHelper.CreateODataLink(routeName, pathHandler, segments);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003E320 File Offset: 0x0003C520
		public static string CreateODataLink(this UrlHelper urlHelper, string routeName, IODataPathHandler pathHandler, IList<ODataPathSegment> segments)
		{
			if (urlHelper == null)
			{
				throw Error.ArgumentNull("urlHelper");
			}
			if (pathHandler == null)
			{
				throw Error.ArgumentNull("pathHandler");
			}
			string text = pathHandler.Link(new Microsoft.AspNet.OData.Routing.ODataPath(segments));
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
			httpRouteValueDictionary.Add(ODataRouteConstants.ODataPath, text);
			return urlHelper.Link(routeName, httpRouteValueDictionary);
		}
	}
}
