using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace Microsoft.AspNet.OData.Routing.Conventions
{
	// Token: 0x02000095 RID: 149
	public interface IODataRoutingConvention
	{
		// Token: 0x06000532 RID: 1330
		string SelectController(ODataPath odataPath, HttpRequestMessage request);

		// Token: 0x06000533 RID: 1331
		string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap);
	}
}
