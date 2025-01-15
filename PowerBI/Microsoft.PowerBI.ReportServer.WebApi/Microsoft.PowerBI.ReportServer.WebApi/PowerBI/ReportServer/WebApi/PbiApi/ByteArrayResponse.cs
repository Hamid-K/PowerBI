using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000020 RID: 32
	public class ByteArrayResponse : IHttpActionResult
	{
		// Token: 0x0600008A RID: 138 RVA: 0x00003430 File Offset: 0x00001630
		public ByteArrayResponse(byte[] byteArray, MediaTypeHeaderValue contentType)
		{
			this._content = byteArray;
			this._contentType = contentType;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003446 File Offset: 0x00001646
		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = new ByteArrayContent(this._content),
				Content = 
				{
					Headers = 
					{
						ContentType = this._contentType
					}
				}
			});
		}

		// Token: 0x04000061 RID: 97
		private readonly byte[] _content;

		// Token: 0x04000062 RID: 98
		private readonly MediaTypeHeaderValue _contentType;
	}
}
