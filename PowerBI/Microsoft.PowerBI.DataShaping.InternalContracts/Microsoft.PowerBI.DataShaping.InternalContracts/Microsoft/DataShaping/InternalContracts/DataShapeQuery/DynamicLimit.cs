using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000087 RID: 135
	internal sealed class DynamicLimit
	{
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00006C75 File Offset: 0x00004E75
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00006C7D File Offset: 0x00004E7D
		internal Expression LimitRef { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00006C86 File Offset: 0x00004E86
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00006C8E File Offset: 0x00004E8E
		internal DynamicLimitRecommendation Count { get; set; }
	}
}
