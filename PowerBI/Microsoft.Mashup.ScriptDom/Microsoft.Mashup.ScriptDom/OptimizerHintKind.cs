using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000089 RID: 137
	[Serializable]
	internal enum OptimizerHintKind
	{
		// Token: 0x04000355 RID: 853
		Unspecified,
		// Token: 0x04000356 RID: 854
		HashGroup,
		// Token: 0x04000357 RID: 855
		OrderGroup,
		// Token: 0x04000358 RID: 856
		MergeJoin,
		// Token: 0x04000359 RID: 857
		HashJoin,
		// Token: 0x0400035A RID: 858
		LoopJoin,
		// Token: 0x0400035B RID: 859
		ConcatUnion,
		// Token: 0x0400035C RID: 860
		HashUnion,
		// Token: 0x0400035D RID: 861
		MergeUnion,
		// Token: 0x0400035E RID: 862
		KeepUnion,
		// Token: 0x0400035F RID: 863
		ForceOrder,
		// Token: 0x04000360 RID: 864
		RobustPlan,
		// Token: 0x04000361 RID: 865
		KeepPlan,
		// Token: 0x04000362 RID: 866
		KeepFixedPlan,
		// Token: 0x04000363 RID: 867
		ExpandViews,
		// Token: 0x04000364 RID: 868
		AlterColumnPlan,
		// Token: 0x04000365 RID: 869
		ShrinkDBPlan,
		// Token: 0x04000366 RID: 870
		BypassOptimizerQueue,
		// Token: 0x04000367 RID: 871
		UsePlan,
		// Token: 0x04000368 RID: 872
		ParameterizationSimple,
		// Token: 0x04000369 RID: 873
		ParameterizationForced,
		// Token: 0x0400036A RID: 874
		OptimizeCorrelatedUnionAll,
		// Token: 0x0400036B RID: 875
		Recompile,
		// Token: 0x0400036C RID: 876
		Fast,
		// Token: 0x0400036D RID: 877
		CheckConstraintsPlan,
		// Token: 0x0400036E RID: 878
		MaxRecursion,
		// Token: 0x0400036F RID: 879
		MaxDop,
		// Token: 0x04000370 RID: 880
		QueryTraceOn,
		// Token: 0x04000371 RID: 881
		CardinalityTunerLimit,
		// Token: 0x04000372 RID: 882
		TableHints,
		// Token: 0x04000373 RID: 883
		OptimizeFor,
		// Token: 0x04000374 RID: 884
		IgnoreNonClusteredColumnStoreIndex
	}
}
