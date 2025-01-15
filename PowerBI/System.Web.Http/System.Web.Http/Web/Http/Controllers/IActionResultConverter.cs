using System;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000F6 RID: 246
	public interface IActionResultConverter
	{
		// Token: 0x06000667 RID: 1639
		HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult);
	}
}
