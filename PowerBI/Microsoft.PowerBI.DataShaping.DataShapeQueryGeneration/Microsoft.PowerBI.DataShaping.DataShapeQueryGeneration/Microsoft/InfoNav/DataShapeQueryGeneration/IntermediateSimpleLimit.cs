using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200002E RID: 46
	internal sealed class IntermediateSimpleLimit : IntermediateReductionAlgorithm
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000976D File Offset: 0x0000796D
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00009775 File Offset: 0x00007975
		internal string Calc { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000977E File Offset: 0x0000797E
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x00009786 File Offset: 0x00007986
		internal IntermediateSimpleLimitKind Kind { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x0000978F File Offset: 0x0000798F
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00009797 File Offset: 0x00007997
		internal bool DisablePreserveKeyPoints { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000097A0 File Offset: 0x000079A0
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x000097A8 File Offset: 0x000079A8
		internal bool? IsStrict { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000097B1 File Offset: 0x000079B1
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x000097B9 File Offset: 0x000079B9
		internal long? Skip { get; set; }

		// Token: 0x060001C9 RID: 457 RVA: 0x000097C4 File Offset: 0x000079C4
		internal override bool IsFullySpecified()
		{
			return base.Count != null;
		}
	}
}
