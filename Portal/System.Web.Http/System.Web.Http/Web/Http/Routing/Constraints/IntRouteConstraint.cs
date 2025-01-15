using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000175 RID: 373
	public class IntRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x000192AC File Offset: 0x000174AC
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
			int num;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is int || int.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out num));
		}
	}
}
