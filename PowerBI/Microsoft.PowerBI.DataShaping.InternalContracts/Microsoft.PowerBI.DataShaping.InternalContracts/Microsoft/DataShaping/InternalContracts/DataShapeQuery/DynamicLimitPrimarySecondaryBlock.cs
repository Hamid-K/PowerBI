using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000086 RID: 134
	internal sealed class DynamicLimitPrimarySecondaryBlock : DynamicLimitBlock
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00006C4B File Offset: 0x00004E4B
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00006C53 File Offset: 0x00004E53
		internal DynamicLimit Primary { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00006C5C File Offset: 0x00004E5C
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00006C64 File Offset: 0x00004E64
		internal DynamicLimit Secondary { get; set; }
	}
}
