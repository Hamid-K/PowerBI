using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x02000178 RID: 376
	public class MaxLengthRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x000194C1 File Offset: 0x000176C1
		public MaxLengthRouteConstraint(int maxLength)
		{
			if (maxLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxLength", maxLength, 0);
			}
			this.MaxLength = maxLength;
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000194EB File Offset: 0x000176EB
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x000194F3 File Offset: 0x000176F3
		public int MaxLength { get; private set; }

		// Token: 0x060009D3 RID: 2515 RVA: 0x000194FC File Offset: 0x000176FC
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
			return values.TryGetValue(parameterName, out obj) && obj != null && Convert.ToString(obj, CultureInfo.InvariantCulture).Length <= this.MaxLength;
		}
	}
}
