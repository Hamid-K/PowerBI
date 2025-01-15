using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000173 RID: 371
	public class FloatRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009BF RID: 2495 RVA: 0x000191F0 File Offset: 0x000173F0
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
			float num;
			return values.TryGetValue(parameterName, out obj) && obj != null && (obj is float || float.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num));
		}
	}
}
