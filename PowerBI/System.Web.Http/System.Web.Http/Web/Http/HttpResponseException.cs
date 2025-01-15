using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Properties;

namespace System.Web.Http
{
	// Token: 0x02000026 RID: 38
	public class HttpResponseException : Exception
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004224 File Offset: 0x00002424
		public HttpResponseException(HttpStatusCode statusCode)
			: this(new HttpResponseMessage(statusCode))
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004232 File Offset: 0x00002432
		public HttpResponseException(HttpResponseMessage response)
			: base(SRResources.HttpResponseExceptionMessage)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			this.Response = response;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004254 File Offset: 0x00002454
		// (set) Token: 0x060000DA RID: 218 RVA: 0x0000425C File Offset: 0x0000245C
		public HttpResponseMessage Response { get; private set; }
	}
}
