using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing
{
	// Token: 0x02000165 RID: 357
	public interface IHttpRouteConstraint
	{
		// Token: 0x06000998 RID: 2456
		bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection);
	}
}
