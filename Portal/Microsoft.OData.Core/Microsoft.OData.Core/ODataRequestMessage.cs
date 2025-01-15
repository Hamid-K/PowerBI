using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x020000B7 RID: 183
	internal sealed class ODataRequestMessage : ODataMessage, IODataRequestMessageAsync, IODataRequestMessage
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x0001372C File Offset: 0x0001192C
		internal ODataRequestMessage(IODataRequestMessage requestMessage, bool writing, bool enableMessageStreamDisposal, long maxMessageSize)
			: base(writing, enableMessageStreamDisposal, maxMessageSize)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0001373F File Offset: 0x0001193F
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0001374C File Offset: 0x0001194C
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

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00013758 File Offset: 0x00011958
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0001374C File Offset: 0x0001194C
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

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00013765 File Offset: 0x00011965
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.requestMessage.Headers;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00013772 File Offset: 0x00011972
		public override string GetHeader(string headerName)
		{
			return this.requestMessage.GetHeader(headerName);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00013780 File Offset: 0x00011980
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00013795 File Offset: 0x00011995
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.requestMessage.GetStream), true);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000137B0 File Offset: 0x000119B0
		public override Task<Stream> GetStreamAsync()
		{
			IODataRequestMessageAsync iodataRequestMessageAsync = this.requestMessage as IODataRequestMessageAsync;
			if (iodataRequestMessageAsync == null)
			{
				throw new ODataException(Strings.ODataRequestMessage_AsyncNotAvailable);
			}
			return base.GetStreamAsync(new Func<Task<Stream>>(iodataRequestMessageAsync.GetStreamAsync), true);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000137EB File Offset: 0x000119EB
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.requestMessage as TInterface;
		}

		// Token: 0x04000321 RID: 801
		private readonly IODataRequestMessage requestMessage;
	}
}
