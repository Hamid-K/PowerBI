using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200002A RID: 42
	internal sealed class IntermediateDataReduction
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00009643 File Offset: 0x00007843
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000964B File Offset: 0x0000784B
		internal IntermediateReductionAlgorithm Primary { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00009654 File Offset: 0x00007854
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000965C File Offset: 0x0000785C
		internal IntermediateReductionAlgorithm Secondary { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00009665 File Offset: 0x00007865
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000966D File Offset: 0x0000786D
		internal IntermediateReductionAlgorithm Intersection { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00009676 File Offset: 0x00007876
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000967E File Offset: 0x0000787E
		internal List<IntermediateScopedReductionAlgorithm> Scoped { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009687 File Offset: 0x00007887
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000968F File Offset: 0x0000788F
		internal int? DataVolume { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00009698 File Offset: 0x00007898
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x000096A0 File Offset: 0x000078A0
		internal IntermediateDynamicLimits DynamicLimits { get; set; }
	}
}
