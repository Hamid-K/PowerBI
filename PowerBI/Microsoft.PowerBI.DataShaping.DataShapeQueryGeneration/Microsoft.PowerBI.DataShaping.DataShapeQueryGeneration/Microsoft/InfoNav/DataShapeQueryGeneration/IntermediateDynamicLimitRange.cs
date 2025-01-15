using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000036 RID: 54
	internal sealed class IntermediateDynamicLimitRange
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00009A6C File Offset: 0x00007C6C
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00009A74 File Offset: 0x00007C74
		internal int Min { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009A7D File Offset: 0x00007C7D
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00009A85 File Offset: 0x00007C85
		internal int Max { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00009A8E File Offset: 0x00007C8E
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00009A96 File Offset: 0x00007C96
		internal bool IsMandatoryConstraint { get; set; }
	}
}
