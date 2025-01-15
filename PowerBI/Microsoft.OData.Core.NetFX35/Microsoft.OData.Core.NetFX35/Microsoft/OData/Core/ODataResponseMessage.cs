using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData.Core
{
	// Token: 0x0200019C RID: 412
	internal sealed class ODataResponseMessage : ODataMessage, IODataResponseMessage
	{
		// Token: 0x06000F6F RID: 3951 RVA: 0x00035902 File Offset: 0x00033B02
		internal ODataResponseMessage(IODataResponseMessage responseMessage, bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
			: base(writing, disableMessageStreamDisposal, maxMessageSize)
		{
			this.responseMessage = responseMessage;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00035915 File Offset: 0x00033B15
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x00035922 File Offset: 0x00033B22
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003592E File Offset: 0x00033B2E
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.responseMessage.Headers;
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003593B File Offset: 0x00033B3B
		public override string GetHeader(string headerName)
		{
			return this.responseMessage.GetHeader(headerName);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00035949 File Offset: 0x00033B49
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.responseMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003595E File Offset: 0x00033B5E
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.responseMessage.GetStream), false);
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00035979 File Offset: 0x00033B79
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.responseMessage as TInterface;
		}

		// Token: 0x040006C6 RID: 1734
		private readonly IODataResponseMessage responseMessage;
	}
}
