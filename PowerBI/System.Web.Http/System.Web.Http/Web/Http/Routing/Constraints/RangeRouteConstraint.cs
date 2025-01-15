using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200017C RID: 380
	public class RangeRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x00019721 File Offset: 0x00017921
		public RangeRouteConstraint(long min, long max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00019737 File Offset: 0x00017937
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0001973F File Offset: 0x0001793F
		public long Min { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00019748 File Offset: 0x00017948
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x00019750 File Offset: 0x00017950
		public long Max { get; private set; }

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001975C File Offset: 0x0001795C
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
					return num >= this.Min && num <= this.Max;
				}
				if (long.TryParse(Convert.ToString(obj, CultureInfo.InvariantCulture), NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return num >= this.Min && num <= this.Max;
				}
			}
			return false;
		}
	}
}
