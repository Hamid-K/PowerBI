using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Client
{
	// Token: 0x020000C6 RID: 198
	public sealed class DataServiceStreamResponse : IDisposable
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0001BF8D File Offset: 0x0001A18D
		internal DataServiceStreamResponse(IODataResponseMessage response)
		{
			this.responseMessage = response;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001BF9C File Offset: 0x0001A19C
		public string ContentType
		{
			get
			{
				this.CheckDisposed();
				return this.responseMessage.GetHeader("Content-Type");
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001BFB4 File Offset: 0x0001A1B4
		public string ContentDisposition
		{
			get
			{
				this.CheckDisposed();
				return this.responseMessage.GetHeader("Content-Disposition");
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001BFCC File Offset: 0x0001A1CC
		public Dictionary<string, string> Headers
		{
			get
			{
				this.CheckDisposed();
				if (this.headers == null)
				{
					this.headers = (Dictionary<string, string>)new HeaderCollection(this.responseMessage).UnderlyingDictionary;
				}
				return this.headers;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0001BFFD File Offset: 0x0001A1FD
		public Stream Stream
		{
			get
			{
				this.CheckDisposed();
				if (this.responseStream == null)
				{
					this.responseStream = this.responseMessage.GetStream();
				}
				return this.responseStream;
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001C024 File Offset: 0x0001A224
		public void Dispose()
		{
			WebUtil.DisposeMessage(this.responseMessage);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001C031 File Offset: 0x0001A231
		private void CheckDisposed()
		{
			if (this.responseMessage == null)
			{
				Error.ThrowObjectDisposed(base.GetType());
			}
		}

		// Token: 0x040002D8 RID: 728
		private IODataResponseMessage responseMessage;

		// Token: 0x040002D9 RID: 729
		private Dictionary<string, string> headers;

		// Token: 0x040002DA RID: 730
		private Stream responseStream;
	}
}
