using System;
using System.Collections.Generic;
using System.Web.Http.Controllers;

namespace System.Web.Http.Routing
{
	// Token: 0x02000143 RID: 323
	public interface IDirectRouteProvider
	{
		// Token: 0x060008DC RID: 2268
		IReadOnlyList<RouteEntry> GetDirectRoutes(HttpControllerDescriptor controllerDescriptor, IReadOnlyList<HttpActionDescriptor> actionDescriptors, IInlineConstraintResolver constraintResolver);
	}
}
