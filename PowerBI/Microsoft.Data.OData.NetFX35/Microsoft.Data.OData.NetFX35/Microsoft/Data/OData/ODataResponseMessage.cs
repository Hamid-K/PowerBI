using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028A RID: 650
	internal sealed class ODataResponseMessage : ODataMessage, IODataResponseMessage
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x0004C2E4 File Offset: 0x0004A4E4
		internal ODataResponseMessage(IODataResponseMessage responseMessage, bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
			: base(writing, disableMessageStreamDisposal, maxMessageSize)
		{
			this.responseMessage = responseMessage;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0004C2F7 File Offset: 0x0004A4F7
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x0004C304 File Offset: 0x0004A504
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

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0004C310 File Offset: 0x0004A510
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.responseMessage.Headers;
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0004C31D File Offset: 0x0004A51D
		public override string GetHeader(string headerName)
		{
			return this.responseMessage.GetHeader(headerName);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0004C32B File Offset: 0x0004A52B
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.responseMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0004C340 File Offset: 0x0004A540
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.responseMessage.GetStream), false);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0004C35B File Offset: 0x0004A55B
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.responseMessage as TInterface;
		}

		// Token: 0x04000857 RID: 2135
		private readonly IODataResponseMessage responseMessage;
	}
}
