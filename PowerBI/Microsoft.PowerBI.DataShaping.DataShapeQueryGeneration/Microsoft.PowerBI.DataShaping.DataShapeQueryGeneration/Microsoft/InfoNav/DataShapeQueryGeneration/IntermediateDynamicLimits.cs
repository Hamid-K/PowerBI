using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000035 RID: 53
	internal sealed class IntermediateDynamicLimits
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00009A0F File Offset: 0x00007C0F
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00009A17 File Offset: 0x00007C17
		internal int TargetIntersectionCount { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00009A20 File Offset: 0x00007C20
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00009A28 File Offset: 0x00007C28
		internal bool SuppressIntersectionLimit { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00009A31 File Offset: 0x00007C31
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00009A39 File Offset: 0x00007C39
		internal IntermediateDynamicLimitRange Primary { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00009A42 File Offset: 0x00007C42
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00009A4A File Offset: 0x00007C4A
		internal IntermediateDynamicLimitRange Secondary { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00009A53 File Offset: 0x00007C53
		// (set) Token: 0x06000202 RID: 514 RVA: 0x00009A5B File Offset: 0x00007C5B
		internal IReadOnlyList<IntermediateDynamicLimitBlock> Blocks { get; set; }
	}
}
