using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200018F RID: 399
	public sealed class ODataPayloadKindDetectionResult
	{
		// Token: 0x06000F07 RID: 3847 RVA: 0x000347FB File Offset: 0x000329FB
		internal ODataPayloadKindDetectionResult(ODataPayloadKind payloadKind, ODataFormat format)
		{
			this.payloadKind = payloadKind;
			this.format = format;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00034811 File Offset: 0x00032A11
		public ODataPayloadKind PayloadKind
		{
			get
			{
				return this.payloadKind;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00034819 File Offset: 0x00032A19
		public ODataFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x04000688 RID: 1672
		private readonly ODataPayloadKind payloadKind;

		// Token: 0x04000689 RID: 1673
		private readonly ODataFormat format;
	}
}
