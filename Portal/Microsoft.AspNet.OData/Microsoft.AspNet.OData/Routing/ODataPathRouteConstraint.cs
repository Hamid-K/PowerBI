using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Routing.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Routing
{
	// Token: 0x0200006C RID: 108
	public class ODataPathRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
		public virtual bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			if (routeDirection != null)
			{
				return true;
			}
			ODataPath odataPath = null;
			object obj;
			if (values.TryGetValue(ODataRouteConstants.ODataPath, out obj))
			{
				string leftPart = request.RequestUri.GetLeftPart(UriPartial.Path);
				string query = request.RequestUri.Query;
				odataPath = ODataPathRouteConstraint.GetODataPath(obj as string, leftPart, query, () => request.CreateRequestContainer(this.RouteName));
			}
			if (odataPath != null)
			{
				HttpRequestMessageProperties httpRequestMessageProperties = request.ODataProperties();
				httpRequestMessageProperties.Path = odataPath;
				httpRequestMessageProperties.RouteName = this.RouteName;
				if (!values.ContainsKey(ODataRouteConstants.Controller))
				{
					string text = this.SelectControllerName(odataPath, request);
					if (text != null)
					{
						values[ODataRouteConstants.Controller] = text;
					}
				}
				return true;
			}
			request.DeleteRequestContainer(true);
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		protected virtual string SelectControllerName(ODataPath path, HttpRequestMessage request)
		{
			foreach (IODataRoutingConvention iodataRoutingConvention in request.GetRoutingConventions())
			{
				string text = iodataRoutingConvention.SelectController(path, request);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000D634 File Offset: 0x0000B834
		public ODataPathRouteConstraint(string routeName)
		{
			if (routeName == null)
			{
				throw Error.ArgumentNull("routeName");
			}
			this.RouteName = routeName;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000D651 File Offset: 0x0000B851
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000D659 File Offset: 0x0000B859
		public string RouteName { get; private set; }

		// Token: 0x06000423 RID: 1059 RVA: 0x0000D664 File Offset: 0x0000B864
		private static ODataPath GetODataPath(string oDataPathString, string uriPathString, string queryString, Func<IServiceProvider> requestContainerFactory)
		{
			ODataPath odataPath = null;
			try
			{
				string text = uriPathString;
				if (!string.IsNullOrEmpty(oDataPathString))
				{
					text = ODataPathRouteConstraint.RemoveODataPath(text, oDataPathString);
				}
				string text2 = uriPathString.Substring(text.Length);
				if (!string.IsNullOrEmpty(queryString))
				{
					text2 += queryString;
				}
				if (text.EndsWith(ODataPathRouteConstraint._escapedSlash, StringComparison.OrdinalIgnoreCase))
				{
					text = text.Substring(0, text.Length - ODataPathRouteConstraint._escapedSlash.Length);
				}
				IServiceProvider serviceProvider = requestContainerFactory();
				odataPath = ServiceProviderServiceExtensions.GetRequiredService<IODataPathHandler>(serviceProvider).Parse(text, text2, serviceProvider);
			}
			catch (ODataException)
			{
				odataPath = null;
			}
			return odataPath;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		private static string RemoveODataPath(string uriString, string oDataPathString)
		{
			int num = uriString.Length - oDataPathString.Length - 1;
			if (num <= 0)
			{
				throw Error.InvalidOperation(SRResources.RequestUriTooShortForODataPath, new object[] { uriString, oDataPathString });
			}
			string text = uriString.Substring(0, num + 1);
			if (string.Equals(uriString.Substring(num + 1), oDataPathString, StringComparison.Ordinal))
			{
				return text;
			}
			for (;;)
			{
				int num2 = text.LastIndexOf('/', num - 1);
				int num3 = text.LastIndexOf(ODataPathRouteConstraint._escapedSlash, num - 1, StringComparison.OrdinalIgnoreCase);
				if (num2 > num3)
				{
					num = num2;
				}
				else
				{
					if (num3 < 0)
					{
						break;
					}
					num = num3 + 2;
				}
				text = uriString.Substring(0, num + 1);
				if (string.Equals(Uri.UnescapeDataString(uriString.Substring(num + 1)), oDataPathString, StringComparison.Ordinal))
				{
					return text;
				}
				if (num == 0)
				{
					goto Block_6;
				}
			}
			throw Error.InvalidOperation(SRResources.ODataPathNotFound, new object[] { uriString, oDataPathString });
			Block_6:
			throw Error.InvalidOperation(SRResources.ODataPathNotFound, new object[] { uriString, oDataPathString });
		}

		// Token: 0x040000DC RID: 220
		private static readonly string _escapedSlash = Uri.EscapeDataString("/");
	}
}
