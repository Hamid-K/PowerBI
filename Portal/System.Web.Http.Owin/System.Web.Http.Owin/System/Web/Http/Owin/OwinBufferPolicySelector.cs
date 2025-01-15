using System;
using System.Net.Http;
using System.Web.Http.Hosting;

namespace System.Web.Http.Owin
{
	// Token: 0x0200000E RID: 14
	public class OwinBufferPolicySelector : IHostBufferPolicySelector
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public bool UseBufferedInputStream(object hostContext)
		{
			return false;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002BFC File Offset: 0x00000DFC
		public bool UseBufferedOutputStream(HttpResponseMessage response)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			HttpContent content = response.Content;
			if (content == null)
			{
				return false;
			}
			long? contentLength = content.Headers.ContentLength;
			if (contentLength != null && contentLength.Value >= 0L)
			{
				return false;
			}
			bool? transferEncodingChunked = response.Headers.TransferEncodingChunked;
			return (transferEncodingChunked == null || !transferEncodingChunked.Value) && !(content is StreamContent) && !(content is PushStreamContent);
		}
	}
}
