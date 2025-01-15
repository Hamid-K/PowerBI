using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x0200006B RID: 107
	internal enum QueryPatternReason
	{
		// Token: 0x040002AC RID: 684
		BinnedLineSampleLimit,
		// Token: 0x040002AD RID: 685
		ComplexSortExpression,
		// Token: 0x040002AE RID: 686
		ExistsFilter,
		// Token: 0x040002AF RID: 687
		ExpressionFeature,
		// Token: 0x040002B0 RID: 688
		FilterContext,
		// Token: 0x040002B1 RID: 689
		InlineDataTransform,
		// Token: 0x040002B2 RID: 690
		InstanceFilter,
		// Token: 0x040002B3 RID: 691
		IsAfter,
		// Token: 0x040002B4 RID: 692
		LeftOuterJoinFunctionNotSupported,
		// Token: 0x040002B5 RID: 693
		LimitWithin,
		// Token: 0x040002B6 RID: 694
		GroupSynchronization,
		// Token: 0x040002B7 RID: 695
		MultiDimensionalModel,
		// Token: 0x040002B8 RID: 696
		NestedDataShape,
		// Token: 0x040002B9 RID: 697
		NestedTotal,
		// Token: 0x040002BA RID: 698
		PeerPrimaryMembers,
		// Token: 0x040002BB RID: 699
		PeerSecondaryMembers,
		// Token: 0x040002BC RID: 700
		OverlappingPointsSampleLimit,
		// Token: 0x040002BD RID: 701
		QueryBatchingFeatureNotSupported,
		// Token: 0x040002BE RID: 702
		ScopedLimit,
		// Token: 0x040002BF RID: 703
		SubstituteWithIndexFunctionNotSupported,
		// Token: 0x040002C0 RID: 704
		SubtotalPosition,
		// Token: 0x040002C1 RID: 705
		SummarizeColumnsFunctionNotSupported,
		// Token: 0x040002C2 RID: 706
		TableVariablesNotSupported,
		// Token: 0x040002C3 RID: 707
		TopNPerLevelLimit,
		// Token: 0x040002C4 RID: 708
		UnsupportedMeasureOutsideInnermostScope,
		// Token: 0x040002C5 RID: 709
		ValueFilterTarget,
		// Token: 0x040002C6 RID: 710
		VisualCalculation,
		// Token: 0x040002C7 RID: 711
		ContextOnlyCalculation,
		// Token: 0x040002C8 RID: 712
		ContextOnlyDataMember
	}
}
