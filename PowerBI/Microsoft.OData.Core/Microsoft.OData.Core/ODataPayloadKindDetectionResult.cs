using System;

namespace Microsoft.OData
{
	// Token: 0x020000AA RID: 170
	public sealed class ODataPayloadKindDetectionResult
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x00011BB6 File Offset: 0x0000FDB6
		internal ODataPayloadKindDetectionResult(ODataPayloadKind payloadKind, ODataFormat format)
		{
			this.payloadKind = payloadKind;
			this.format = format;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public ODataPayloadKind PayloadKind
		{
			get
			{
				return this.payloadKind;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040002DD RID: 733
		private readonly ODataPayloadKind payloadKind;

		// Token: 0x040002DE RID: 734
		private readonly ODataFormat format;
	}
}
