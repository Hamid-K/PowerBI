using System;

namespace System.Web.Http.Routing
{
	// Token: 0x02000144 RID: 324
	public interface IInlineConstraintResolver
	{
		// Token: 0x060008DD RID: 2269
		IHttpRouteConstraint ResolveConstraint(string inlineConstraint);
	}
}
