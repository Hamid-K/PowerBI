using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000038 RID: 56
	internal sealed class IntermediateDynamicLimitEvenDistributionBlock : IntermediateDynamicLimitBlock
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00009AC0 File Offset: 0x00007CC0
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00009AC8 File Offset: 0x00007CC8
		internal IReadOnlyList<IntermediateDynamicLimit> Limits { get; set; }
	}
}
