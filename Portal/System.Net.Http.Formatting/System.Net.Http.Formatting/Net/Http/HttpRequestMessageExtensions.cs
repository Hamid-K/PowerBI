using System;
using System.ComponentModel;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000015 RID: 21
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpRequestMessageExtensions
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003027 File Offset: 0x00001227
		public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode statusCode)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return new HttpResponseMessage
			{
				StatusCode = statusCode,
				RequestMessage = request
			};
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000304A File Offset: 0x0000124A
		public static HttpResponseMessage CreateResponse(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return new HttpResponseMessage
			{
				RequestMessage = request
			};
		}
	}
}
