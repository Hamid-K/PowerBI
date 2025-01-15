using System;
using System.Linq;

namespace System.Web.Http.Controllers
{
	// Token: 0x02000105 RID: 261
	public interface IHttpActionSelector
	{
		// Token: 0x060006D6 RID: 1750
		HttpActionDescriptor SelectAction(HttpControllerContext controllerContext);

		// Token: 0x060006D7 RID: 1751
		ILookup<string, HttpActionDescriptor> GetActionMapping(HttpControllerDescriptor controllerDescriptor);
	}
}
