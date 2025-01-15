using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.OData
{
	// Token: 0x02000095 RID: 149
	internal sealed class ODataRequestMessage : ODataMessage, IODataRequestMessage
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0000FDA7 File Offset: 0x0000DFA7
		internal ODataRequestMessage(IODataRequestMessage requestMessage, bool writing, bool enableMessageStreamDisposal, long maxMessageSize)
			: base(writing, enableMessageStreamDisposal, maxMessageSize)
		{
			this.requestMessage = requestMessage;
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000FDBA File Offset: 0x0000DFBA
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000FDC7 File Offset: 0x0000DFC7
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000FDD3 File Offset: 0x0000DFD3
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0000FDC7 File Offset: 0x0000DFC7
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public override IEnumerable<KeyValuePair<string, string>> Headers
		{
			get
			{
				return this.requestMessage.Headers;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0000FDED File Offset: 0x0000DFED
		public override string GetHeader(string headerName)
		{
			return this.requestMessage.GetHeader(headerName);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0000FDFB File Offset: 0x0000DFFB
		public override void SetHeader(string headerName, string headerValue)
		{
			base.VerifyCanSetHeader();
			this.requestMessage.SetHeader(headerName, headerValue);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0000FE10 File Offset: 0x0000E010
		public override Stream GetStream()
		{
			return base.GetStream(new Func<Stream>(this.requestMessage.GetStream), true);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0000FE2B File Offset: 0x0000E02B
		internal override TInterface QueryInterface<TInterface>()
		{
			return this.requestMessage as TInterface;
		}

		// Token: 0x040002B9 RID: 697
		private readonly IODataRequestMessage requestMessage;
	}
}
