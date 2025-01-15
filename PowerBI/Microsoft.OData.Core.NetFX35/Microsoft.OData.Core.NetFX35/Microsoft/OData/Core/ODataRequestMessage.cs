using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200019B RID: 411
	internal sealed class ODataRequestMessage : ODataMessage, IODataRequestMessage
	{
		// Token: 0x06000F65 RID: 3941 RVA: 0x00035860 File Offset: 0x00033A60
		internal ODataRequestMessage(IODataRequestMessage requestMessage, bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
			: base(writing, disableMessageStreamDisposal, maxMessageSize)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x00035873 File Offset: 0x00033A73
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x00035880 File Offset: 0x00033A80
		public Uri Url
		{
			get
			{
				return this.requestMessage.Url;
			}
			set
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003588C File Offset: 0x00033A8C
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x00035899 File Offset: 0x00033A99
		public string Method
		{
			get
			{
				return this.requestMessage.Method;
			}
			set
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x000358A5 File Offset: 0x00033AA5
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.requestMessage.Headers;
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000358B2 File Offset: 0x00033AB2
		public override string GetHeader(string headerName)
		{
			return this.requestMessage.GetHeader(headerName);
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x000358C0 File Offset: 0x00033AC0
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x000358D5 File Offset: 0x00033AD5
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.requestMessage.GetStream), true);
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x000358F0 File Offset: 0x00033AF0
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.requestMessage as TInterface;
		}

		// Token: 0x040006C5 RID: 1733
		private readonly IODataRequestMessage requestMessage;
	}
}
