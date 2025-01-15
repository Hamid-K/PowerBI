using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000179 RID: 377
	public class MaxRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00019552 File Offset: 0x00017752
		public MaxRouteConstraint(long max)
		{
			this.Max = max;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00019561 File Offset: 0x00017761
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00019569 File Offset: 0x00017769
		public long Max { get; private set; }

		// Token: 0x060009D7 RID: 2519 RVA: 0x00019574 File Offset: 0x00017774
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
			if (values.TryGetValue(parameterName, out obj) && obj != null)
			{
				long num;
				if (obj is long)
				{
					num = (long)obj;
					return num <= this.Max;
				}
				if (long.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return num <= this.Max;
				}
			}
			return false;
		}
	}
}
