using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000075 RID: 117
	[Flags]
	public enum AllowedExpressionContentFlags
	{
		// Token: 0x040002B1 RID: 689
		None = 0,
		// Token: 0x040002B2 RID: 690
		ModelReferences = 2,
		// Token: 0x040002B3 RID: 691
		SubqueryReferences = 4,
		// Token: 0x040002B4 RID: 692
		GroupByAggregates = 8,
		// Token: 0x040002B5 RID: 693
		MedianAggregate = 16,
		// Token: 0x040002B6 RID: 694
		Percentile = 32,
		// Token: 0x040002B7 RID: 695
		SingleValueAggregate = 64,
		// Token: 0x040002B8 RID: 696
		FilteredEval = 128,
		// Token: 0x040002B9 RID: 697
		ScopedEval = 256,
		// Token: 0x040002BA RID: 698
		TransformColumn = 512,
		// Token: 0x040002BB RID: 699
		VisualCalculations = 1024
	}
}
