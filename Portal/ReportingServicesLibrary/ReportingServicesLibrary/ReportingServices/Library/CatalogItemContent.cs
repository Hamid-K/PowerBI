using System;
using System.IO;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001B RID: 27
	internal sealed class CatalogItemContent
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00004F39 File Offset: 0x00003139
		public CatalogItemContent(byte[] content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this._arrayContent = content;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004F56 File Offset: 0x00003156
		public CatalogItemContent(Stream content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this._streamContent = content;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004F73 File Offset: 0x00003173
		public byte[] Content
		{
			get
			{
				return this._arrayContent;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004F7B File Offset: 0x0000317B
		public Stream ContentStream
		{
			get
			{
				return this._streamContent;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004F83 File Offset: 0x00003183
		public bool IsStreamContent
		{
			get
			{
				return this._streamContent != null;
			}
		}

		// Token: 0x040000A7 RID: 167
		private readonly byte[] _arrayContent;

		// Token: 0x040000A8 RID: 168
		private readonly Stream _streamContent;
	}
}
