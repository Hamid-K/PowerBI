using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x0200072A RID: 1834
	internal static class HttpResponseHandler
	{
		// Token: 0x06003693 RID: 13971 RVA: 0x000AE0B8 File Offset: 0x000AC2B8
		public static HttpResponseData GetResponseStream(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, string contentType, bool throwOnBadRequest, bool catch404, IEngineHost host, ODataSettingsBase settings, ODataUserSettings userSettings, ODataServerVersion serverVersion, bool handleExceptions = true)
		{
			return new ODataRequest(resource, serviceUri, uri, headers, credentials, contentType, throwOnBadRequest, catch404, host, settings, userSettings, serverVersion, handleExceptions).GetResponseStream();
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000AE0E8 File Offset: 0x000AC2E8
		public static bool IsBadRequestError(WebException e)
		{
			if (e.Status == WebExceptionStatus.ProtocolError)
			{
				MashupHttpWebResponse mashupHttpWebResponse = e.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null)
				{
					HttpStatusCode statusCode = mashupHttpWebResponse.StatusCode;
					return statusCode == HttpStatusCode.BadRequest || statusCode - HttpStatusCode.InternalServerError <= 1;
				}
			}
			return false;
		}
	}
}
