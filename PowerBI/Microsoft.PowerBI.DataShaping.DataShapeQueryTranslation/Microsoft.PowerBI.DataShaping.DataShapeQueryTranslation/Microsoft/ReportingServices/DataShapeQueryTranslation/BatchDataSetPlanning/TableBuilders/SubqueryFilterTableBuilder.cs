using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001E2 RID: 482
	internal static class SubqueryFilterTableBuilder
	{
		// Token: 0x060010A8 RID: 4264 RVA: 0x00045840 File Offset: 0x00043A40
		internal static PlanOperation ProjectAndApplySubqueryFilter(IValueFilterPlanningContext plannerContext, DataShape containingDataShape, FilterCondition subqueryFilter, PlanOperation table)
		{
			if (subqueryFilter == null)
			{
				return table;
			}
			List<PlanProjectItem> list = new List<PlanProjectItem> { PlanPreserveAllColumnsProjectItem.Instance };
			SubqueryFilterTableBuilder.AddFilterProjections(plannerContext.OutputExpressionTable, plannerContext.ErrorContext, plannerContext.ScopeTree, containingDataShape, subqueryFilter, list);
			PlanOperation planOperation = table.Project(list, false);
			return SubqueryFilterTableBuilder.ApplySubqueryFilter(subqueryFilter, planOperation);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00045890 File Offset: 0x00043A90
		internal static void AddFilterProjections(ExpressionTable expressionTable, TranslationErrorContext errorContext, ScopeTree scopeTree, DataShape containingDataShape, FilterCondition subqueryFilter, List<PlanProjectItem> projectItems)
		{
			if (subqueryFilter == null)
			{
				return;
			}
			foreach (FilterExpressionInfo filterExpressionInfo in FilterExpressionCollector.CollectSubqueryReferenceExpressions(subqueryFilter, containingDataShape.Id, expressionTable, errorContext, containingDataShape, scopeTree))
			{
				foreach (Calculation calculation in filterExpressionInfo.ReferencedCalculations)
				{
					projectItems.Add(new PlanMapColumnIdentityProjectItem(calculation.Value.ExpressionId.Value, new ExpressionId[] { filterExpressionInfo.Expression.ExpressionId.Value }));
				}
			}
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00045964 File Offset: 0x00043B64
		internal static PlanOperation ApplySubqueryFilter(FilterCondition subqueryFilter, PlanOperation table)
		{
			if (subqueryFilter == null)
			{
				return table;
			}
			return table.FilterBy(subqueryFilter);
		}
	}
}
