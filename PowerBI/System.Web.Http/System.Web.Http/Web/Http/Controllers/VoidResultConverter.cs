using System;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000FC RID: 252
	public class VoidResultConverter : IActionResultConverter
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00010579 File Offset: 0x0000E779
		public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			return controllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
		}
	}
}
