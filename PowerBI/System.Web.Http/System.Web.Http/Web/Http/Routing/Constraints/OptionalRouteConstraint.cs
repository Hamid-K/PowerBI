using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200017E RID: 382
	public class OptionalRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x00019873 File Offset: 0x00017A73
		public OptionalRouteConstraint(IHttpRouteConstraint innerConstraint)
		{
			if (innerConstraint == null)
			{
				throw Error.ArgumentNull("innerConstraint");
			}
			this.InnerConstraint = innerConstraint;
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00019890 File Offset: 0x00017A90
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00019898 File Offset: 0x00017A98
		public IHttpRouteConstraint InnerConstraint { get; private set; }

		// Token: 0x060009ED RID: 2541 RVA: 0x000198A4 File Offset: 0x00017AA4
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
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
			RouteParameter optional = RouteParameter.Optional;
			object obj;
			object obj2;
			return (route.Defaults.TryGetValue(parameterName, out obj) && obj == optional && values.TryGetValue(parameterName, out obj2) && obj2 == optional) || this.InnerConstraint.Match(request, route, parameterName, values, routeDirection);
		}
	}
}
