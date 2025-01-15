using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000174 RID: 372
	public class GuidRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x00019254 File Offset: 0x00017454
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			object obj;
			Guid guid;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is Guid || Guid.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), out guid));
		}
	}
}
