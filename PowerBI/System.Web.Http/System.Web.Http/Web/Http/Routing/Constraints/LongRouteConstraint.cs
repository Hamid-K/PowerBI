using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000177 RID: 375
	public class LongRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00019464 File Offset: 0x00017664
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
			long num;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is long || long.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out num));
		}
	}
}
