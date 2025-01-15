using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000172 RID: 370
	public class DoubleRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x0001918C File Offset: 0x0001738C
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
			double num;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is double || double.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num));
		}
	}
}
