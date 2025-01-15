using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000083 RID: 131
	internal sealed class DynamicLimitRecommendation
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00006BDE File Offset: 0x00004DDE
		// (set) Token: 0x06000331 RID: 817 RVA: 0x00006BE6 File Offset: 0x00004DE6
		internal Candidate<int> Min { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00006BEF File Offset: 0x00004DEF
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00006BF7 File Offset: 0x00004DF7
		internal Candidate<int> Max { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00006C00 File Offset: 0x00004E00
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00006C08 File Offset: 0x00004E08
		internal bool IsMandatoryConstraint { get; set; }
	}
}
