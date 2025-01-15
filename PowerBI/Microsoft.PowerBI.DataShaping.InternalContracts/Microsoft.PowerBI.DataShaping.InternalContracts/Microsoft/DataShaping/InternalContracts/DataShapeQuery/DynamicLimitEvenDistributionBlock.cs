using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000085 RID: 133
	internal sealed class DynamicLimitEvenDistributionBlock : DynamicLimitBlock
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00006C32 File Offset: 0x00004E32
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00006C3A File Offset: 0x00004E3A
		internal List<DynamicLimit> Limits { get; set; }
	}
}
