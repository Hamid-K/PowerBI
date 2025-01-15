using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000076 RID: 118
	public static class AllowedExpressionContent
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x00012508 File Offset: 0x00010708
		internal static AllowedExpressionContentFlags CombineFlags(params AllowedExpressionContentFlags[] flags)
		{
			AllowedExpressionContentFlags combinedFlags = AllowedExpressionContentFlags.None;
			Array.ForEach<AllowedExpressionContentFlags>(flags, delegate(AllowedExpressionContentFlags flag)
			{
				combinedFlags |= flag;
			});
			return combinedFlags;
		}

		// Token: 0x040002BC RID: 700
		public static AllowedExpressionContentFlags TopLevelSelectWithSubqueryRegrouping = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.ModelReferences,
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates,
			AllowedExpressionContentFlags.MedianAggregate,
			AllowedExpressionContentFlags.Percentile,
			AllowedExpressionContentFlags.VisualCalculations
		});

		// Token: 0x040002BD RID: 701
		public static AllowedExpressionContentFlags TopLevelSelectWithSubqueryRegroupingWithSingleValue = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContent.TopLevelSelectWithSubqueryRegrouping,
			AllowedExpressionContentFlags.SingleValueAggregate
		});

		// Token: 0x040002BE RID: 702
		public static AllowedExpressionContentFlags SubquerySelect = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.ModelReferences,
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates,
			AllowedExpressionContentFlags.MedianAggregate,
			AllowedExpressionContentFlags.Percentile,
			AllowedExpressionContentFlags.FilteredEval,
			AllowedExpressionContentFlags.SingleValueAggregate,
			AllowedExpressionContentFlags.ScopedEval
		});

		// Token: 0x040002BF RID: 703
		public static AllowedExpressionContentFlags TopLevelQuerySelect = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContent.SubquerySelect,
			AllowedExpressionContentFlags.VisualCalculations
		});

		// Token: 0x040002C0 RID: 704
		public static AllowedExpressionContentFlags SelectSubqueryReferences = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates
		});

		// Token: 0x040002C1 RID: 705
		public static AllowedExpressionContentFlags SelectSubqueryReferencesWithSingleValue = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContent.SelectSubqueryReferences,
			AllowedExpressionContentFlags.SingleValueAggregate
		});

		// Token: 0x040002C2 RID: 706
		public static AllowedExpressionContentFlags Transform = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.ModelReferences,
			AllowedExpressionContentFlags.GroupByAggregates,
			AllowedExpressionContentFlags.MedianAggregate,
			AllowedExpressionContentFlags.Percentile,
			AllowedExpressionContentFlags.ScopedEval,
			AllowedExpressionContentFlags.SubqueryReferences
		});

		// Token: 0x040002C3 RID: 707
		public static AllowedExpressionContentFlags TransformSubqueryReferences = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.TransformColumn
		});

		// Token: 0x040002C4 RID: 708
		public static AllowedExpressionContentFlags WhereExpression = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.ModelReferences,
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates,
			AllowedExpressionContentFlags.MedianAggregate,
			AllowedExpressionContentFlags.Percentile,
			AllowedExpressionContentFlags.ScopedEval
		});

		// Token: 0x040002C5 RID: 709
		public static AllowedExpressionContentFlags WhereExpressionSubqueryRegrouping = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates
		});

		// Token: 0x040002C6 RID: 710
		public static AllowedExpressionContentFlags OrderBy = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.ModelReferences,
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates,
			AllowedExpressionContentFlags.MedianAggregate,
			AllowedExpressionContentFlags.Percentile,
			AllowedExpressionContentFlags.ScopedEval
		});

		// Token: 0x040002C7 RID: 711
		public static AllowedExpressionContentFlags OrderByExpressionSubqueryReferences = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[]
		{
			AllowedExpressionContentFlags.SubqueryReferences,
			AllowedExpressionContentFlags.GroupByAggregates
		});

		// Token: 0x040002C8 RID: 712
		public static AllowedExpressionContentFlags ExpansionState = AllowedExpressionContent.CombineFlags(new AllowedExpressionContentFlags[] { AllowedExpressionContentFlags.ModelReferences });
	}
}
