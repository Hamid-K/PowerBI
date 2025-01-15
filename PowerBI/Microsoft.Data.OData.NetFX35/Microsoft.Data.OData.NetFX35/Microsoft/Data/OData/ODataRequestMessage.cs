using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028B RID: 651
	internal sealed class ODataRequestMessage : ODataMessage, IODataRequestMessage
	{
		// Token: 0x060014B2 RID: 5298 RVA: 0x0004C36D File Offset: 0x0004A56D
		internal ODataRequestMessage(IODataRequestMessage requestMessage, bool writing, bool disableMessageStreamDisposal, long maxMessageSize)
			: base(writing, disableMessageStreamDisposal, maxMessageSize)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0004C380 File Offset: 0x0004A580
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0004C38D File Offset: 0x0004A58D
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0004C399 File Offset: 0x0004A599
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x0004C3A6 File Offset: 0x0004A5A6
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

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.requestMessage.Headers;
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0004C3BF File Offset: 0x0004A5BF
		public override string GetHeader(string headerName)
		{
			return this.requestMessage.GetHeader(headerName);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0004C3CD File Offset: 0x0004A5CD
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0004C3E2 File Offset: 0x0004A5E2
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.requestMessage.GetStream), true);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0004C3FD File Offset: 0x0004A5FD
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.requestMessage as TInterface;
		}

		// Token: 0x04000858 RID: 2136
		private readonly IODataRequestMessage requestMessage;
	}
}
