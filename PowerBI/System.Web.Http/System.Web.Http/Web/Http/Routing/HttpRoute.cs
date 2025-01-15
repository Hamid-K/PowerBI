using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http.Properties;

namespace System.Web.Http.Routing
{
	// Token: 0x02000160 RID: 352
	public class HttpRoute : IHttpRoute
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0001867E File Offset: 0x0001687E
		public HttpRoute()
			: this(null, null, null, null, null, null)
		{
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001868C File Offset: 0x0001688C
		public HttpRoute(string routeTemplate)
			: this(routeTemplate, null, null, null, null, null)
		{
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0001869A File Offset: 0x0001689A
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults)
			: this(routeTemplate, defaults, null, null, null, null)
		{
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000186A8 File Offset: 0x000168A8
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints)
			: this(routeTemplate, defaults, constraints, null, null, null)
		{
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000186B6 File Offset: 0x000168B6
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens)
			: this(routeTemplate, defaults, constraints, dataTokens, null, null)
		{
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x000186C5 File Offset: 0x000168C5
		public HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler)
			: this(routeTemplate, defaults, constraints, dataTokens, handler, null)
		{
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000186D8 File Offset: 0x000168D8
		internal HttpRoute(string routeTemplate, HttpRouteValueDictionary defaults, HttpRouteValueDictionary constraints, HttpRouteValueDictionary dataTokens, HttpMessageHandler handler, HttpParsedRoute parsedRoute)
		{
			this._routeTemplate = ((routeTemplate == null) ? string.Empty : routeTemplate);
			this._defaults = defaults ?? new HttpRouteValueDictionary();
			this._constraints = constraints ?? new HttpRouteValueDictionary();
			this._dataTokens = dataTokens ?? new HttpRouteValueDictionary();
			this.Handler = handler;
			if (parsedRoute == null)
			{
				this.ParsedRoute = RouteParser.Parse(routeTemplate);
				return;
			}
			this.ParsedRoute = parsedRoute;
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0001874E File Offset: 0x0001694E
		public IDictionary<string, object> Defaults
		{
			get
			{
				return this._defaults;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00018756 File Offset: 0x00016956
		public IDictionary<string, object> Constraints
		{
			get
			{
				return this._constraints;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001875E File Offset: 0x0001695E
		public IDictionary<string, object> DataTokens
		{
			get
			{
				return this._dataTokens;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00018766 File Offset: 0x00016966
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0001876E File Offset: 0x0001696E
		public HttpMessageHandler Handler { get; private set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00018777 File Offset: 0x00016977
		public string RouteTemplate
		{
			get
			{
				return this._routeTemplate;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0001877F File Offset: 0x0001697F
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00018787 File Offset: 0x00016987
		internal HttpParsedRoute ParsedRoute { get; private set; }

		// Token: 0x06000982 RID: 2434 RVA: 0x00018790 File Offset: 0x00016990
		public virtual IHttpRouteData GetRouteData(string virtualPathRoot, HttpRequestMessage request)
		{
			if (virtualPathRoot == null)
			{
				throw Error.ArgumentNull("virtualPathRoot");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			RoutingContext orCreateRoutingContext = HttpRoute.GetOrCreateRoutingContext(virtualPathRoot, request);
			if (!orCreateRoutingContext.IsValid)
			{
				return null;
			}
			HttpRouteValueDictionary httpRouteValueDictionary = this.ParsedRoute.Match(orCreateRoutingContext, this._defaults);
			if (httpRouteValueDictionary == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(request, httpRouteValueDictionary, HttpRouteDirection.UriResolution))
			{
				return null;
			}
			return new HttpRouteData(this, httpRouteValueDictionary);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000187F8 File Offset: 0x000169F8
		private static RoutingContext GetOrCreateRoutingContext(string virtualPathRoot, HttpRequestMessage request)
		{
			RoutingContext routingContext;
			if (!request.Properties.TryGetValue("MS_RoutingContext", out routingContext))
			{
				routingContext = HttpRoute.CreateRoutingContext(virtualPathRoot, request);
				request.Properties["MS_RoutingContext"] = routingContext;
			}
			return routingContext;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00018834 File Offset: 0x00016A34
		private static RoutingContext CreateRoutingContext(string virtualPathRoot, HttpRequestMessage request)
		{
			string text = "/" + request.RequestUri.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			if (!text.StartsWith(virtualPathRoot, StringComparison.Ordinal) && !text.StartsWith(virtualPathRoot, StringComparison.OrdinalIgnoreCase))
			{
				return RoutingContext.Invalid();
			}
			int length = virtualPathRoot.Length;
			string text2;
			if (text.Length > length && text[length] == '/')
			{
				text2 = text.Substring(length + 1);
			}
			else
			{
				text2 = text.Substring(length);
			}
			return RoutingContext.Valid(RouteParser.SplitUriToPathSegmentStrings(text2));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000188B0 File Offset: 0x00016AB0
		public virtual IHttpVirtualPathData GetVirtualPath(HttpRequestMessage request, IDictionary<string, object> values)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (values != null && !values.Keys.Contains(HttpRoute.HttpRouteKey, StringComparer.OrdinalIgnoreCase))
			{
				return null;
			}
			IDictionary<string, object> routeDictionaryWithoutHttpRouteKey = HttpRoute.GetRouteDictionaryWithoutHttpRouteKey(values);
			IHttpRouteData routeData = request.GetRouteData();
			IDictionary<string, object> dictionary = ((routeData == null) ? null : routeData.Values);
			BoundRouteTemplate boundRouteTemplate = this.ParsedRoute.Bind(dictionary, routeDictionaryWithoutHttpRouteKey, this._defaults, this._constraints);
			if (boundRouteTemplate == null)
			{
				return null;
			}
			if (!this.ProcessConstraints(request, boundRouteTemplate.Values, HttpRouteDirection.UriGeneration))
			{
				return null;
			}
			return new HttpVirtualPathData(this, boundRouteTemplate.BoundTemplate);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00018940 File Offset: 0x00016B40
		private static IDictionary<string, object> GetRouteDictionaryWithoutHttpRouteKey(IDictionary<string, object> routeValues)
		{
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
			if (routeValues != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in routeValues)
				{
					if (!string.Equals(keyValuePair.Key, HttpRoute.HttpRouteKey, StringComparison.OrdinalIgnoreCase))
					{
						httpRouteValueDictionary.Add(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return httpRouteValueDictionary;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000189B4 File Offset: 0x00016BB4
		protected virtual bool ProcessConstraint(HttpRequestMessage request, object constraint, string parameterName, HttpRouteValueDictionary values, HttpRouteDirection routeDirection)
		{
			IHttpRouteConstraint httpRouteConstraint = constraint as IHttpRouteConstraint;
			if (httpRouteConstraint != null)
			{
				return httpRouteConstraint.Match(request, this, parameterName, values, routeDirection);
			}
			string text = constraint as string;
			if (text == null)
			{
				throw Error.InvalidOperation(SRResources.Route_ValidationMustBeStringOrCustomConstraint, new object[]
				{
					parameterName,
					this.RouteTemplate,
					typeof(IHttpRouteConstraint).Name
				});
			}
			object obj;
			values.TryGetValue(parameterName, out obj);
			string text2 = Convert.ToString(obj, CultureInfo.InvariantCulture);
			string text3 = "^(" + text + ")$";
			return Regex.IsMatch(text2, text3, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00018A48 File Offset: 0x00016C48
		private bool ProcessConstraints(HttpRequestMessage request, HttpRouteValueDictionary values, HttpRouteDirection routeDirection)
		{
			if (this.Constraints != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.Constraints)
				{
					if (!this.ProcessConstraint(request, keyValuePair.Value, keyValuePair.Key, values, routeDirection))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00018AB8 File Offset: 0x00016CB8
		internal static void ValidateConstraint(string routeTemplate, string name, object constraint)
		{
			if (constraint is IHttpRouteConstraint)
			{
				return;
			}
			if (constraint is string)
			{
				return;
			}
			throw HttpRoute.CreateInvalidConstraintTypeException(routeTemplate, name);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00018AD3 File Offset: 0x00016CD3
		private static Exception CreateInvalidConstraintTypeException(string routeTemplate, string name)
		{
			return Error.InvalidOperation(SRResources.Route_ValidationMustBeStringOrCustomConstraint, new object[]
			{
				name,
				routeTemplate,
				typeof(IHttpRouteConstraint).FullName
			});
		}

		// Token: 0x04000289 RID: 649
		public static readonly string HttpRouteKey = "httproute";

		// Token: 0x0400028A RID: 650
		internal const string RoutingContextKey = "MS_RoutingContext";

		// Token: 0x0400028B RID: 651
		private string _routeTemplate;

		// Token: 0x0400028C RID: 652
		private HttpRouteValueDictionary _defaults;

		// Token: 0x0400028D RID: 653
		private HttpRouteValueDictionary _constraints;

		// Token: 0x0400028E RID: 654
		private HttpRouteValueDictionary _dataTokens;
	}
}
