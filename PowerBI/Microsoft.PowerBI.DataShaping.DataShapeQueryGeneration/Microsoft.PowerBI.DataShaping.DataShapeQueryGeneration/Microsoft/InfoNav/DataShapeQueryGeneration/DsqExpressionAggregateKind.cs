using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000077 RID: 119
	internal enum DsqExpressionAggregateKind
	{
		// Token: 0x040002CA RID: 714
		None,
		// Token: 0x040002CB RID: 715
		Subtotal,
		// Token: 0x040002CC RID: 716
		Min,
		// Token: 0x040002CD RID: 717
		Max,
		// Token: 0x040002CE RID: 718
		Count,
		// Token: 0x040002CF RID: 719
		Percentile,
		// Token: 0x040002D0 RID: 720
		Average,
		// Token: 0x040002D1 RID: 721
		Median
	}
}
