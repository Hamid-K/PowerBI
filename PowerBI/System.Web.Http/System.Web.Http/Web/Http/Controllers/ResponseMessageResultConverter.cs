using System;
using System.Net.Http;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000FA RID: 250
	public class ResponseMessageResultConverter : IActionResultConverter
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x000104E4 File Offset: 0x0000E6E4
		public HttpResponseMessage Convert(HttpControllerContext controllerContext, object actionResult)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			HttpResponseMessage httpResponseMessage = (HttpResponseMessage)actionResult;
			if (httpResponseMessage == null)
			{
				throw Error.InvalidOperation(SRResources.ResponseMessageResultConverter_NullHttpResponseMessage, new object[0]);
			}
			httpResponseMessage.EnsureResponseHasRequest(controllerContext.Request);
			return httpResponseMessage;
		}
	}
}
