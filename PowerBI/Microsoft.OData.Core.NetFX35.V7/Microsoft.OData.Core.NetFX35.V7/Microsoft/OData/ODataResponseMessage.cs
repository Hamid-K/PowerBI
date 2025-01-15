using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000096 RID: 150
	internal sealed class ODataResponseMessage : ODataMessage, IODataResponseMessage
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x0000FE3D File Offset: 0x0000E03D
		internal ODataResponseMessage(IODataResponseMessage responseMessage, bool writing, bool enableMessageStreamDisposal, long maxMessageSize)
			: base(writing, enableMessageStreamDisposal, maxMessageSize)
		{
			this.responseMessage = responseMessage;
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000FE50 File Offset: 0x0000E050
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0000FDC7 File Offset: 0x0000DFC7
		public int StatusCode
		{
			get
			{
				return this.responseMessage.StatusCode;
			}
			set
			{
				throw new ODataException(Strings.ODataMessage_MustNotModifyMessage);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000FE5D File Offset: 0x0000E05D
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.responseMessage.Headers;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x0000FE6A File Offset: 0x0000E06A
		public override string GetHeader(string headerName)
		{
			return this.responseMessage.GetHeader(headerName);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0000FE78 File Offset: 0x0000E078
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.responseMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x0000FE8D File Offset: 0x0000E08D
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.responseMessage.GetStream), false);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.responseMessage as TInterface;
		}

		// Token: 0x040002BA RID: 698
		private readonly IODataResponseMessage responseMessage;
	}
}
