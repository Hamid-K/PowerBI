using System;
using System.Net;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200001F RID: 31
	public class WebApiException : WebException
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x0000406D File Offset: 0x0000226D
		public WebApiException(string message, HttpStatusCode status)
			: base(message)
		{
			this._status = status;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000407D File Offset: 0x0000227D
		public WebApiException(string message, HttpStatusCode status, Exception ex)
			: base(message, ex)
		{
			this._status = status;
		}

		// Token: 0x0400007D RID: 125
		private readonly HttpStatusCode _status;
	}
}
