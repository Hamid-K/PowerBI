using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000B8 RID: 184
	internal sealed class ODataResponseMessage : ODataMessage, IODataResponseMessageAsync, IODataResponseMessage
	{
		// Token: 0x0600083A RID: 2106 RVA: 0x000137FD File Offset: 0x000119FD
		internal ODataResponseMessage(IODataResponseMessage responseMessage, bool writing, bool enableMessageStreamDisposal, long maxMessageSize)
			: base(writing, enableMessageStreamDisposal, maxMessageSize)
		{
			this.responseMessage = responseMessage;
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00013810 File Offset: 0x00011A10
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001374C File Offset: 0x0001194C
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

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x0001381D File Offset: 0x00011A1D
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.responseMessage.Headers;
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001382A File Offset: 0x00011A2A
		public override string GetHeader(string headerName)
		{
			return this.responseMessage.GetHeader(headerName);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00013838 File Offset: 0x00011A38
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.responseMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001384D File Offset: 0x00011A4D
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.responseMessage.GetStream), false);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00013868 File Offset: 0x00011A68
		public override Task<Stream> GetStreamAsync()
		{
			IODataResponseMessageAsync iodataResponseMessageAsync = this.responseMessage as IODataResponseMessageAsync;
			if (iodataResponseMessageAsync == null)
			{
				throw new ODataException(Strings.ODataResponseMessage_AsyncNotAvailable);
			}
			return base.GetStreamAsync(new Func<Task<Stream>>(iodataResponseMessageAsync.GetStreamAsync), false);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000138A3 File Offset: 0x00011AA3
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.responseMessage as TInterface;
		}

		// Token: 0x04000322 RID: 802
		private readonly IODataResponseMessage responseMessage;
	}
}
