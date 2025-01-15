using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200017A RID: 378
	public class MinLengthRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x000195F1 File Offset: 0x000177F1
		public MinLengthRouteConstraint(int minLength)
		{
			if (minLength < 0)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("minLength", minLength, 0);
			}
			this.MinLength = minLength;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0001961B File Offset: 0x0001781B
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x00019623 File Offset: 0x00017823
		public int MinLength { get; private set; }

		// Token: 0x060009DB RID: 2523 RVA: 0x0001962C File Offset: 0x0001782C
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
			return values.TryGetValue(parameterName, out obj) && obj != null && Convert.ToString(obj, CultureInfo.InvariantCulture).Length >= this.MinLength;
		}
	}
}
