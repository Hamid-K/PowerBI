using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000039 RID: 57
	internal sealed class IntermediateDynamicLimitPrimarySecondaryBlock : IntermediateDynamicLimitBlock
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009AD9 File Offset: 0x00007CD9
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00009AE1 File Offset: 0x00007CE1
		internal IntermediateDynamicLimit Primary { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009AEA File Offset: 0x00007CEA
		// (set) Token: 0x06000214 RID: 532 RVA: 0x00009AF2 File Offset: 0x00007CF2
		internal IntermediateDynamicLimit Secondary { get; set; }
	}
}
