using System;
using System.Net;
using System.Net.Http;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000FB RID: 251
	public class ValueResultConverter<T> : IActionResultConverter
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00010528 File Offset: 0x0000E728
		public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			HttpResponseMessage httpResponseMessage = actionResult as HttpResponseMessage;
			if (httpResponseMessage != null)
			{
				httpResponseMessage.EnsureResponseHasRequest(controllerContext.Request);
				return httpResponseMessage;
			}
			T t = (T)((object)actionResult);
			return controllerContext.Request.CreateResponse(HttpStatusCode.OK, t, controllerContext.Configuration);
		}
	}
}
