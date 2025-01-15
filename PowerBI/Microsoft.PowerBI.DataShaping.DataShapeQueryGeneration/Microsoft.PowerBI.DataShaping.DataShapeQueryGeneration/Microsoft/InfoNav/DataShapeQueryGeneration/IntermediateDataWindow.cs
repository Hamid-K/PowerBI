using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000030 RID: 48
	internal sealed class IntermediateDataWindow : IntermediateReductionAlgorithm
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000097E7 File Offset: 0x000079E7
		// (set) Token: 0x060001CC RID: 460 RVA: 0x000097EF File Offset: 0x000079EF
		internal string Calc { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000097F8 File Offset: 0x000079F8
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00009800 File Offset: 0x00007A00
		internal bool IncludeRestartToken { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00009809 File Offset: 0x00007A09
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00009811 File Offset: 0x00007A11
		internal IReadOnlyList<IReadOnlyList<PrimitiveValue>> RestartTokens { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000981A File Offset: 0x00007A1A
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00009822 File Offset: 0x00007A22
		internal RestartMatchingBehavior? RestartMatchingBehavior { get; set; }

		// Token: 0x060001D3 RID: 467 RVA: 0x0000982C File Offset: 0x00007A2C
		internal override bool IsFullySpecified()
		{
			return base.Count != null;
		}
	}
}
