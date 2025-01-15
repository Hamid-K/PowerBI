using System;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations
{
	// Token: 0x020001E8 RID: 488
	internal interface IPlanOperationVisitor<TResult>
	{
		// Token: 0x060010DB RID: 4315
		TResult Visit(PlanOperationAddMissingItems operation);

		// Token: 0x060010DC RID: 4316
		TResult Visit(PlanOperationAddJoinIndex operation);

		// Token: 0x060010DD RID: 4317
		TResult Visit(PlanOperationApplyStartPosition operation);

		// Token: 0x060010DE RID: 4318
		TResult Visit(PlanOperationBinnedLineSample operation);

		// Token: 0x060010DF RID: 4319
		TResult Visit(PlanOperationCalculateTableInFilterContext operation);

		// Token: 0x060010E0 RID: 4320
		TResult Visit(PlanOperationClearDefaultContext operation);

		// Token: 0x060010E1 RID: 4321
		TResult Visit(PlanOperationCreateFilterContextTable operation);

		// Token: 0x060010E2 RID: 4322
		TResult Visit(PlanOperationCrossJoin operation);

		// Token: 0x060010E3 RID: 4323
		TResult Visit(PlanOperationFullOuterCrossJoin operation);

		// Token: 0x060010E4 RID: 4324
		TResult Visit(PlanOperationDataTransform operation);

		// Token: 0x060010E5 RID: 4325
		TResult Visit(PlanOperationDeclarationReference operation);

		// Token: 0x060010E6 RID: 4326
		TResult Visit(PlanOperationDistinctRows operation);

		// Token: 0x060010E7 RID: 4327
		TResult Visit(PlanOperationEnsureUniqueUnqualifiedNames operation);

		// Token: 0x060010E8 RID: 4328
		TResult Visit(PlanOperationFilterBy operation);

		// Token: 0x060010E9 RID: 4329
		TResult Visit(PlanOperationAddMissingItemsCompatPattern operation);

		// Token: 0x060010EA RID: 4330
		TResult Visit(PlanOperationGroupAndJoin operation);

		// Token: 0x060010EB RID: 4331
		TResult Visit(PlanOperationGroupBy operation);

		// Token: 0x060010EC RID: 4332
		TResult Visit(PlanOperationInnerJoin operation);

		// Token: 0x060010ED RID: 4333
		TResult Visit(PlanOperationLeftOuterJoin operation);

		// Token: 0x060010EE RID: 4334
		TResult Visit(PlanOperationOverlappingPointsSample operation);

		// Token: 0x060010EF RID: 4335
		TResult Visit(PlanOperationTopNPerLevelSample operation);

		// Token: 0x060010F0 RID: 4336
		TResult Visit(PlanOperationProject operation);

		// Token: 0x060010F1 RID: 4337
		TResult Visit(PlanOperationSample operation);

		// Token: 0x060010F2 RID: 4338
		TResult Visit(PlanOperationSingleRow operation);

		// Token: 0x060010F3 RID: 4339
		TResult Visit(PlanOperationSortBy operation);

		// Token: 0x060010F4 RID: 4340
		TResult Visit(PlanOperationSubstituteWithIndex operation);

		// Token: 0x060010F5 RID: 4341
		TResult Visit(PlanOperationTableScan operation);

		// Token: 0x060010F6 RID: 4342
		TResult Visit(PlanOperationTopN operation);

		// Token: 0x060010F7 RID: 4343
		TResult Visit(PlanOperationTopNSkip operation);

		// Token: 0x060010F8 RID: 4344
		TResult Visit(PlanOperationUnion operation);

		// Token: 0x060010F9 RID: 4345
		TResult Visit(PlanOperationVisualCalculationReferenceableTable planOperationVisualCalculationReferenceableTable);
	}
}
