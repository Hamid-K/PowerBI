using System;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200016D RID: 365
	public class AlphaRouteConstraint : RegexRouteConstraint
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x00018FD9 File Offset: 0x000171D9
		public AlphaRouteConstraint()
			: base("^[a-z]*$")
		{
		}
	}
}
