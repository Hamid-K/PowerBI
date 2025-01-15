using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000170 RID: 368
	public class DateTimeRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x000190CC File Offset: 0x000172CC
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
			DateTime dateTime;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is DateTime || DateTime.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime));
		}
	}
}
