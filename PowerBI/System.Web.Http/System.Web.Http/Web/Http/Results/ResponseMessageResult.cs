using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Http.Results
{
	// Token: 0x020000BC RID: 188
	public class ResponseMessageResult : IHttpActionResult
	{
		// Token: 0x0600049C RID: 1180 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public ResponseMessageResult(HttpResponseMessage response)
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this._response = response;
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		public HttpResponseMessage Response
		{
			get
			{
				return this._response;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000CD15 File Offset: 0x0000AF15
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this._response);
		}

		// Token: 0x04000128 RID: 296
		private readonly HttpResponseMessage _response;
	}
}
