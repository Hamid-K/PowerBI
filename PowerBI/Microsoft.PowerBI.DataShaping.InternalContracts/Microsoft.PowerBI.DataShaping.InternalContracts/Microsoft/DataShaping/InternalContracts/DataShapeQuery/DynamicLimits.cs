using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000082 RID: 130
	internal sealed class DynamicLimits : IDataBoundItem
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00006B81 File Offset: 0x00004D81
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00006B89 File Offset: 0x00004D89
		internal Candidate<int> TargetIntersectionCount { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00006B92 File Offset: 0x00004D92
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00006B9A File Offset: 0x00004D9A
		internal Expression IntersectionLimit { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00006BA3 File Offset: 0x00004DA3
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00006BAB File Offset: 0x00004DAB
		internal DynamicLimitRecommendation Primary { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00006BB4 File Offset: 0x00004DB4
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00006BBC File Offset: 0x00004DBC
		internal DynamicLimitRecommendation Secondary { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00006BC5 File Offset: 0x00004DC5
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00006BCD File Offset: 0x00004DCD
		internal List<DynamicLimitBlock> Blocks { get; set; }
	}
}
