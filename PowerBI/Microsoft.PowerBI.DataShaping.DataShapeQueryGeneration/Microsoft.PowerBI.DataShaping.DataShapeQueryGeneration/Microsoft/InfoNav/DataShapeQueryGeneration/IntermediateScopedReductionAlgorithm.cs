using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200002C RID: 44
	internal sealed class IntermediateScopedReductionAlgorithm
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000096EC File Offset: 0x000078EC
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x000096F4 File Offset: 0x000078F4
		internal IntermediateReductionAlgorithm Algorithm { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x000096FD File Offset: 0x000078FD
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00009705 File Offset: 0x00007905
		internal IntermediateReductionScope Scope { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0000970E File Offset: 0x0000790E
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x00009716 File Offset: 0x00007916
		internal int? TelemetryId { get; set; }
	}
}
