using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200016E RID: 366
	public class BoolRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x00018FE8 File Offset: 0x000171E8
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
			bool flag;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is bool || bool.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), out flag));
		}
	}
}
