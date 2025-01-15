using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200017B RID: 379
	public class MinRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x00019682 File Offset: 0x00017882
		public MinRouteConstraint(long min)
		{
			this.Min = min;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00019691 File Offset: 0x00017891
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00019699 File Offset: 0x00017899
		public long Min { get; private set; }

		// Token: 0x060009DF RID: 2527 RVA: 0x000196A4 File Offset: 0x000178A4
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
					return num >= this.Min;
				}
				if (long.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return num >= this.Min;
				}
			}
			return false;
		}
	}
}
