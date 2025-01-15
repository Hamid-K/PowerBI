using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.LimitPlanning
{
	// Token: 0x0200023D RID: 573
	internal static class WindowTableBuilder
	{
		// Token: 0x06001399 RID: 5017 RVA: 0x0004C3BC File Offset: 0x0004A5BC
		internal static PlanOperationContext ApplyLegacyPrimaryHierarchySegmentation(PlanOperationContext opContext, PlanDeclarationCollection declarations, TranslationErrorContext errorContext, DataShapeAnnotations annotations, IReadOnlyList<DataMember> dynamicMembers, Limit correspondingLimit, Candidate<int> requestedPrimaryLeafCount, RestartMatchingBehavior? restartMatchingBehavior, Identifier dataShapeId)
		{
			if (dynamicMembers.IsNullOrEmpty<DataMember>())
			{
				return opContext;
			}
			PlanOperation planOperation = opContext.Table;
			bool flag;
			planOperation = WindowTableBuilder.ApplyStartPosition(planOperation, dynamicMembers, restartMatchingBehavior, annotations.SubtotalAnnotations, out flag);
			int? num = BatchDataSetPlanningSegmentationUtils.DetermineEffectiveSegmentSize(requestedPrimaryLeafCount, dynamicMembers, flag, annotations.SubtotalAnnotations);
			if (BatchDataSetPlanningSegmentationUtils.NeedsSegmentationTopN(num, flag, correspondingLimit))
			{
				PlanExpression planExpression = BatchDataSetPlanningUtils.CreateSegmentSizeExpression(num.Value, dataShapeId, errorContext);
				planOperation = planOperation.TopN(planExpression, dynamicMembers.ToSortItems(annotations, true), false);
				planOperation = planOperation.DeclareIfNotDeclared(PlanNames.PrimaryWindowed(dataShapeId), declarations, false, false, null, false);
			}
			return opContext.ReplaceTable(planOperation, null, null, null);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004C44C File Offset: 0x0004A64C
		public static PlanOperation ApplyStartPosition(PlanOperation table, IEnumerable<DataMember> dynamicMembers, RestartMatchingBehavior? restartMatchingBehavior, BatchSubtotalAnnotations subtotalAnnotations, out bool hasStartPosition)
		{
			List<DataMember> startAtMembers = BatchDataSetPlanningSegmentationUtils.GetStartAtMembers(dynamicMembers, subtotalAnnotations);
			hasStartPosition = startAtMembers.Count > 0;
			if (!hasStartPosition)
			{
				return table;
			}
			return table.ApplyStartPosition(startAtMembers, restartMatchingBehavior);
		}
	}
}
