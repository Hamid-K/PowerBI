using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200003A RID: 58
	internal sealed class IntermediateDynamicLimit
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00009B03 File Offset: 0x00007D03
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00009B0B File Offset: 0x00007D0B
		internal int ScopedReductionIndex { get; set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00009B14 File Offset: 0x00007D14
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00009B1C File Offset: 0x00007D1C
		internal IntermediateDynamicLimitRange Count { get; set; }
	}
}
