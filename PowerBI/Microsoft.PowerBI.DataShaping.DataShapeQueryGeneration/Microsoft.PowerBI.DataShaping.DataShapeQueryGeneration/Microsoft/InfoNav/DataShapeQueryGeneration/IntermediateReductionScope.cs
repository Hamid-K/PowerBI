using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200002D RID: 45
	internal sealed class IntermediateReductionScope
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00009727 File Offset: 0x00007927
		// (set) Token: 0x060001BA RID: 442 RVA: 0x0000972F File Offset: 0x0000792F
		internal List<int> Primary { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00009738 File Offset: 0x00007938
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00009740 File Offset: 0x00007940
		internal List<int> Secondary { get; set; }

		// Token: 0x060001BD RID: 445 RVA: 0x00009749 File Offset: 0x00007949
		internal bool IsEmpty()
		{
			return this.Primary.IsNullOrEmpty<int>() && this.Secondary.IsNullOrEmpty<int>();
		}
	}
}
