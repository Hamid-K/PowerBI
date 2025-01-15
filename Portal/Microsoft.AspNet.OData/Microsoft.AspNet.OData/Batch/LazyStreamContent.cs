using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001CA RID: 458
	internal class LazyStreamContent : HttpContent
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x0003E5A1 File Offset: 0x0003C7A1
		public LazyStreamContent(Func<Stream> getStream)
		{
			this._getStream = getStream;
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0003E5B0 File Offset: 0x0003C7B0
		private StreamContent StreamContent
		{
			get
			{
				if (this._streamContent == null)
				{
					this._streamContent = new StreamContent(this._getStream());
				}
				return this._streamContent;
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0003E5D6 File Offset: 0x0003C7D6
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return this.StreamContent.CopyToAsync(stream, context);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003E5E5 File Offset: 0x0003C7E5
		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}

		// Token: 0x04000432 RID: 1074
		private Func<Stream> _getStream;

		// Token: 0x04000433 RID: 1075
		private StreamContent _streamContent;
	}
}
