using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000032 RID: 50
	internal sealed class IntermediateOverlappingPointsSampleLimit : IntermediateReductionAlgorithm
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000098FB File Offset: 0x00007AFB
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00009903 File Offset: 0x00007B03
		internal IntermediatePlotAxisBinding X { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000990C File Offset: 0x00007B0C
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00009914 File Offset: 0x00007B14
		internal IntermediatePlotAxisBinding Y { get; set; }

		// Token: 0x060001EB RID: 491 RVA: 0x00009920 File Offset: 0x00007B20
		internal override bool IsFullySpecified()
		{
			return base.Count != null;
		}
	}
}
