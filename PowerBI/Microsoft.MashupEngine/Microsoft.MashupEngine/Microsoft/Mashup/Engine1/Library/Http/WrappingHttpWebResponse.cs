using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000AAD RID: 2733
	internal class WrappingHttpWebResponse : MashupHttpWebResponse
	{
		// Token: 0x06004C93 RID: 19603 RVA: 0x000FC70B File Offset: 0x000FA90B
		public WrappingHttpWebResponse(HttpWebResponse response)
		{
			this.response = response;
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x000FC71A File Offset: 0x000FA91A
		public override string CharacterSet
		{
			get
			{
				return this.response.CharacterSet;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06004C95 RID: 19605 RVA: 0x000FC727 File Offset: 0x000FA927
		public override string ContentEncoding
		{
			get
			{
				return this.response.ContentEncoding;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x000FC734 File Offset: 0x000FA934
		public override long ContentLength
		{
			get
			{
				return this.response.ContentLength;
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06004C97 RID: 19607 RVA: 0x000FC741 File Offset: 0x000FA941
		public override string ContentType
		{
			get
			{
				return this.response.ContentType;
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x000FC74E File Offset: 0x000FA94E
		// (set) Token: 0x06004C99 RID: 19609 RVA: 0x000FC75B File Offset: 0x000FA95B
		public override CookieCollection Cookies
		{
			get
			{
				return this.response.Cookies;
			}
			set
			{
				this.response.Cookies = value;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x000FC769 File Offset: 0x000FA969
		public override WebHeaderCollection Headers
		{
			get
			{
				return this.response.Headers;
			}
		}

		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06004C9B RID: 19611 RVA: 0x000FC776 File Offset: 0x000FA976
		public override bool IsMutuallyAuthenticated
		{
			get
			{
				return this.response.IsMutuallyAuthenticated;
			}
		}

		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x000FC783 File Offset: 0x000FA983
		public override DateTime LastModified
		{
			get
			{
				return this.response.LastModified;
			}
		}

		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06004C9D RID: 19613 RVA: 0x000FC790 File Offset: 0x000FA990
		public override string Method
		{
			get
			{
				return this.response.Method;
			}
		}

		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x000FC79D File Offset: 0x000FA99D
		public override Version ProtocolVersion
		{
			get
			{
				return this.response.ProtocolVersion;
			}
		}

		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06004C9F RID: 19615 RVA: 0x000FC7AA File Offset: 0x000FA9AA
		public override Uri ResponseUri
		{
			get
			{
				return this.response.ResponseUri;
			}
		}

		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x000FC7B7 File Offset: 0x000FA9B7
		public override string Server
		{
			get
			{
				return this.response.Server;
			}
		}

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x000FC7C4 File Offset: 0x000FA9C4
		public override HttpStatusCode StatusCode
		{
			get
			{
				return this.response.StatusCode;
			}
		}

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x000FC7D1 File Offset: 0x000FA9D1
		public override string StatusDescription
		{
			get
			{
				return this.response.StatusDescription;
			}
		}

		// Token: 0x06004CA3 RID: 19619 RVA: 0x000FC7DE File Offset: 0x000FA9DE
		public override void Close()
		{
			this.response.Close();
		}

		// Token: 0x06004CA4 RID: 19620 RVA: 0x000FC7EB File Offset: 0x000FA9EB
		public override string GetResponseHeader(string headerName)
		{
			return this.response.GetResponseHeader(headerName);
		}

		// Token: 0x06004CA5 RID: 19621 RVA: 0x000FC7F9 File Offset: 0x000FA9F9
		public override Stream GetResponseStream()
		{
			if (this.buffered != null)
			{
				this.buffered.Position = 0L;
				return this.buffered.NonDisposable();
			}
			return this.response.GetResponseStream();
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x000FC827 File Offset: 0x000FAA27
		public override void Buffer()
		{
			if (this.buffered == null)
			{
				this.buffered = new MemoryStream();
				this.response.GetResponseStream().CopyTo(this.buffered);
			}
		}

		// Token: 0x0400287A RID: 10362
		private readonly HttpWebResponse response;

		// Token: 0x0400287B RID: 10363
		private MemoryStream buffered;
	}
}
