using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BE RID: 446
	public sealed class ODataPayloadKindDetectionResult
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x0002E8A2 File Offset: 0x0002CAA2
		internal ODataPayloadKindDetectionResult(ODataPayloadKind payloadKind, ODataFormat format)
		{
			this.payloadKind = payloadKind;
			this.format = format;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0002E8B8 File Offset: 0x0002CAB8
		public ODataPayloadKind PayloadKind
		{
			get
			{
				return this.payloadKind;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0002E8C0 File Offset: 0x0002CAC0
		public ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x040004A9 RID: 1193
		private readonly ODataPayloadKind payloadKind;

		// Token: 0x040004AA RID: 1194
		private readonly ODataFormat format;
	}
}
