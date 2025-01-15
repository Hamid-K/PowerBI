using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x0200015E RID: 350
	public class HttpMethodConstraint : IHttpRouteConstraint
	{
		// Token: 0x0600095F RID: 2399 RVA: 0x00017A62 File Offset: 0x00015C62
		public HttpMethodConstraint(params HttpMethod[] allowedMethods)
		{
			if (allowedMethods == null)
			{
				throw Error.ArgumentNull("allowedMethods");
			}
			this.AllowedMethods = new Collection<HttpMethod>(allowedMethods);
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000960 RID: 2400 RVA: 0x00017A84 File Offset: 0x00015C84
		// (set) Token: 0x06000961 RID: 2401 RVA: 0x00017A8C File Offset: 0x00015C8C
		public Collection<HttpMethod> AllowedMethods { get; private set; }

		// Token: 0x06000962 RID: 2402 RVA: 0x00017A98 File Offset: 0x00015C98
		protected virtual bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (route == null)
			{
				throw Error.ArgumentNull("route");
			}
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			if (routeDirection == HttpRouteDirection.UriResolution)
			{
				return this.AllowedMethods.Contains(request.Method);
			}
			if (routeDirection != HttpRouteDirection.UriGeneration)
			{
				throw Error.InvalidEnumArgument(string.Empty, (int)routeDirection, typeof(HttpRouteDirection));
			}
			HttpMethod httpMethod;
			return !values.TryGetValue(parameterName, out httpMethod) || this.AllowedMethods.Contains(httpMethod);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00017B2C File Offset: 0x00015D2C
		bool IHttpRouteConstraint.Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			return this.Match(request, route, parameterName, values, routeDirection);
		}
	}
}
