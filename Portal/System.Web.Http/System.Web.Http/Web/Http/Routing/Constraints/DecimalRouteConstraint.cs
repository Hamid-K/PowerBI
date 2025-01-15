using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000171 RID: 369
	public class DecimalRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x0001912C File Offset: 0x0001732C
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
			decimal num;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is decimal || decimal.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out num));
		}
	}
}
