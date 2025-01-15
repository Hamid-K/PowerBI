using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000143 RID: 323
	internal sealed class BatchQueryGroupItemGenerator : IPlanGroupByItemVisitor
	{
		// Token: 0x06000BF0 RID: 3056 RVA: 0x000303F1 File Offset: 0x0002E5F1
		private BatchQueryGroupItemGenerator(DataShapeAnnotations annotations, CalculationExpressionMap calculationExpressions, GeneratedTable input)
		{
			this.m_annotations = annotations;
			this.m_calculationExpressions = calculationExpressions;
			this.m_columns = new QueryItemCollection<QueryTableColumn>();
			this.m_input = input;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0003041C File Offset: 0x0002E61C
		public static IEnumerable<QueryTableColumn> Generate(DataShapeAnnotations annotations, CalculationExpressionMap calculationExpressions, GeneratedTable input, ReadOnlyCollection<PlanGroupByItem> planGroupByItems)
		{
			BatchQueryGroupItemGenerator batchQueryGroupItemGenerator = new BatchQueryGroupItemGenerator(annotations, calculationExpressions, input);
			foreach (PlanGroupByItem planGroupByItem in planGroupByItems)
			{
				planGroupByItem.Accept(batchQueryGroupItemGenerator);
			}
			return batchQueryGroupItemGenerator.Columns;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00030474 File Offset: 0x0002E674
		private IEnumerable<QueryTableColumn> Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0003047C File Offset: 0x0002E67C
		public void Visit(PlanGroupByMember item)
		{
			foreach (GroupKey groupKey in item.Member.Group.GroupKeys)
			{
				this.AddReferenceColumn(groupKey.Value.ExpressionId.Value);
			}
			if (item.SubtotalIndicatorColumnName != null)
			{
				this.AddReferenceColumn(item.SubtotalIndicatorColumnName);
			}
			Group group = item.Member.Group;
			if (group.SortKeys != null)
			{
				SortByMeasureInfoCollection sortByMeasureInfos = this.m_annotations.DataMemberAnnotations.GetSortByMeasureInfos(item.Member);
				bool flag = this.m_annotations.DataMemberAnnotations.HasLimits(item.Member);
				for (int i = 0; i < group.SortKeys.Count; i++)
				{
					SortKey sortKey = group.SortKeys[i];
					bool flag2 = sortByMeasureInfos != null && sortByMeasureInfos.ContainsKey(sortKey);
					if ((!item.ExcludeMeasureSortKeys || !flag2) && (sortByMeasureInfos == null || (item.IncludeSortByMeasureKeysAtMeasureScope && sortByMeasureInfos.IsAtMeasureScope) || !flag2 || flag))
					{
						this.AddReferenceColumn(sortKey.Value.ExpressionId.Value);
					}
				}
			}
			Contract.RetailAssert(item.ContextTables.IsNullOrEmpty<PlanOperation>(), "ContextTables on GroupByMembers are not allowed outside of GroupAndJoin");
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000305E0 File Offset: 0x0002E7E0
		public void Visit(PlanGroupByCalculation item)
		{
			ReadOnlyCollection<ExpressionId> expressions = this.m_calculationExpressions.GetExpressions(item.Calculation);
			for (int i = 0; i < expressions.Count; i++)
			{
				this.AddReferenceColumn(expressions[i]);
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0003061D File Offset: 0x0002E81D
		public void Visit(PlanGroupByColumn item)
		{
			this.AddReferenceColumn(item.Name);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0003062C File Offset: 0x0002E82C
		private void AddReferenceColumn(ExpressionId exprId)
		{
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[exprId];
			this.m_columns.Add(queryTableColumn.Name, queryTableColumn.ToReferenceColumn());
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00030664 File Offset: 0x0002E864
		public void Visit(PlanGroupByDataTransformColumn planGroupByDataTransformColumn)
		{
			this.AddReferenceColumn(planGroupByDataTransformColumn.Column.Value.ExpressionId.Value);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00030690 File Offset: 0x0002E890
		private void AddReferenceColumn(string planColumnName)
		{
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[planColumnName];
			this.m_columns.Add(queryTableColumn.Name, queryTableColumn.ToReferenceColumn());
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000306C8 File Offset: 0x0002E8C8
		public void Visit(PlanGroupByGroupKey item)
		{
			this.AddReferenceColumn(item.GroupKey.Value.ExpressionId.Value);
		}

		// Token: 0x04000608 RID: 1544
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x04000609 RID: 1545
		private readonly CalculationExpressionMap m_calculationExpressions;

		// Token: 0x0400060A RID: 1546
		private readonly QueryItemCollection<QueryTableColumn> m_columns;

		// Token: 0x0400060B RID: 1547
		private readonly GeneratedTable m_input;
	}
}
