using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000031 RID: 49
	internal sealed class IntermediateBinnedLineSampleLimit : IntermediateReductionAlgorithm
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000984F File Offset: 0x00007A4F
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x00009857 File Offset: 0x00007A57
		internal int? MinPointsPerSeries { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00009860 File Offset: 0x00007A60
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00009868 File Offset: 0x00007A68
		internal int? MaxPointsPerSeries { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00009871 File Offset: 0x00007A71
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00009879 File Offset: 0x00007A79
		internal int? MaxDynamicSeriesCount { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00009882 File Offset: 0x00007A82
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000988A File Offset: 0x00007A8A
		internal int? PrimaryScalarKey { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00009893 File Offset: 0x00007A93
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000989B File Offset: 0x00007A9B
		internal IReadOnlyList<ProjectedDsqExpression> Measures { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000098A4 File Offset: 0x00007AA4
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x000098AC File Offset: 0x00007AAC
		internal string DbCountCalc { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000098B5 File Offset: 0x00007AB5
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000098BD File Offset: 0x00007ABD
		internal string DbPrimaryCalc { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x000098C6 File Offset: 0x00007AC6
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x000098CE File Offset: 0x00007ACE
		internal string DbSecondaryCalc { get; set; }

		// Token: 0x060001E5 RID: 485 RVA: 0x000098D8 File Offset: 0x00007AD8
		internal override bool IsFullySpecified()
		{
			return base.Count != null;
		}
	}
}
