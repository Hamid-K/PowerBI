using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200002B RID: 43
	internal abstract class IntermediateReductionAlgorithm
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000096B1 File Offset: 0x000078B1
		// (set) Token: 0x060001AB RID: 427 RVA: 0x000096B9 File Offset: 0x000078B9
		internal string Id { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000096C2 File Offset: 0x000078C2
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000096CA File Offset: 0x000078CA
		internal int? Count { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000096D3 File Offset: 0x000078D3
		// (set) Token: 0x060001AF RID: 431 RVA: 0x000096DB File Offset: 0x000078DB
		internal int? WarningCount { get; set; }

		// Token: 0x060001B0 RID: 432
		internal abstract bool IsFullySpecified();
	}
}
