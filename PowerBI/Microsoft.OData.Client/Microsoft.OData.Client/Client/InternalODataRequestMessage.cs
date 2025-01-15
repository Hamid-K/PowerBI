using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Microsoft.OData.Client
{
	// Token: 0x02000052 RID: 82
	internal class InternalODataRequestMessage : DataServiceClientRequestMessage
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000A04A File Offset: 0x0000824A
		internal InternalODataRequestMessage(IODataRequestMessage requestMessage, bool allowGetStream)
			: base(requestMessage.Method)
		{
			this.requestMessage = requestMessage;
			this.allowGetStream = allowGetStream;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A066 File Offset: 0x00008266
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.HeaderCollection.AsEnumerable();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000A073 File Offset: 0x00008273
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00006FEF File Offset: 0x000051EF
		public override Uri Url
		{
			get
			{
				return this.requestMessage.Url;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000A080 File Offset: 0x00008280
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00006FEF File Offset: 0x000051EF
		public override string Method
		{
			get
			{
				return this.requestMessage.Method;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000A08D File Offset: 0x0000828D
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000A08D File Offset: 0x0000828D
		public override ICredentials Credentials
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000A08D File Offset: 0x0000828D
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000A08D File Offset: 0x0000828D
		public override int Timeout
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00006FEF File Offset: 0x000051EF
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00006FEF File Offset: 0x000051EF
		public override bool SendChunked
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000A094 File Offset: 0x00008294
		private HeaderCollection HeaderCollection
		{
			get
			{
				if (this.headers == null)
				{
					this.headers = new HeaderCollection(this.requestMessage.Headers);
				}
				return this.headers;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000A0BA File Offset: 0x000082BA
		public override string GetHeader(string headerName)
		{
			return this.HeaderCollection.GetHeader(headerName);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public override void SetHeader(string headerName, string headerValue)
		{
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000A0D7 File Offset: 0x000082D7
		public override Stream GetStream()
		{
			if (!this.allowGetStream)
			{
				throw new NotImplementedException();
			}
			if (this.cachedRequestStream == null)
			{
				this.cachedRequestStream = this.requestMessage.GetStream();
			}
			return this.cachedRequestStream;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00006FEF File Offset: 0x000051EF
		public override void Abort()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006FEF File Offset: 0x000051EF
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00006FEF File Offset: 0x000051EF
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00006FEF File Offset: 0x000051EF
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00006FEF File Offset: 0x000051EF
		public override IODataResponseMessage EndGetResponse(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00006FEF File Offset: 0x000051EF
		public override IODataResponseMessage GetResponse()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000DC RID: 220
		private readonly IODataRequestMessage requestMessage;

		// Token: 0x040000DD RID: 221
		private readonly bool allowGetStream;

		// Token: 0x040000DE RID: 222
		private Stream cachedRequestStream;

		// Token: 0x040000DF RID: 223
		private HeaderCollection headers;
	}
}
