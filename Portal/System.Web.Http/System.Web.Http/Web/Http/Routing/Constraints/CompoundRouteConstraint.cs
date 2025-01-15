using System;
using System.Collections.Generic;
using System.Net.Http;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200016F RID: 367
	public class CompoundRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009B5 RID: 2485 RVA: 0x0001903F File Offset: 0x0001723F
		public CompoundRouteConstraint(IList<IHttpRouteConstraint> constraints)
		{
			if (constraints == null)
			{
				throw Error.ArgumentNull("constraints");
			}
			this.Constraints = constraints;
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0001905C File Offset: 0x0001725C
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x00019064 File Offset: 0x00017264
		public IEnumerable<IHttpRouteConstraint> Constraints { get; private set; }

		// Token: 0x060009B8 RID: 2488 RVA: 0x00019070 File Offset: 0x00017270
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			using (IEnumerator<IHttpRouteConstraint> enumerator = this.Constraints.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Match(request, route, parameterName, values, routeDirection))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}
