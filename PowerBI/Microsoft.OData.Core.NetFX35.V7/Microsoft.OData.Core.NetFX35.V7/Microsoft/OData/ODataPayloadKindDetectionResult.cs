using System;

namespace Microsoft.OData
{
	// Token: 0x02000085 RID: 133
	public sealed class ODataPayloadKindDetectionResult
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000E49E File Offset: 0x0000C69E
		internal ODataPayloadKindDetectionResult(ODataPayloadKind payloadKind, ODataFormat format)
		{
			this.payloadKind = payloadKind;
			this.format = format;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public ODataPayloadKind PayloadKind
		{
			get
			{
				return this.payloadKind;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x04000277 RID: 631
		private readonly ODataPayloadKind payloadKind;

		// Token: 0x04000278 RID: 632
		private readonly ODataFormat format;
	}
}
