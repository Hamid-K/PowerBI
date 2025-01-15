using System;
using System.IO;
using System.Threading.Tasks;

namespace System.Net.Http.Handlers
{
	// Token: 0x02000029 RID: 41
	internal class ProgressContent : HttpContent
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00005A0C File Offset: 0x00003C0C
		public ProgressContent(HttpContent innerContent, ProgressMessageHandler handler, HttpRequestMessage request)
		{
			this._innerContent = innerContent;
			this._handler = handler;
			this._request = request;
			innerContent.Headers.CopyTo(base.Headers);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005A3C File Offset: 0x00003C3C
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			ProgressStream progressStream = new ProgressStream(stream, this._handler, this._request, null);
			return this._innerContent.CopyToAsync(progressStream);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005A6C File Offset: 0x00003C6C
		protected override bool TryComputeLength(out long length)
		{
			long? contentLength = this._innerContent.Headers.ContentLength;
			if (contentLength != null)
			{
				length = contentLength.Value;
				return true;
			}
			length = -1L;
			return false;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005AA3 File Offset: 0x00003CA3
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this._innerContent.Dispose();
			}
		}

		// Token: 0x04000079 RID: 121
		private readonly HttpContent _innerContent;

		// Token: 0x0400007A RID: 122
		private readonly ProgressMessageHandler _handler;

		// Token: 0x0400007B RID: 123
		private readonly HttpRequestMessage _request;
	}
}
