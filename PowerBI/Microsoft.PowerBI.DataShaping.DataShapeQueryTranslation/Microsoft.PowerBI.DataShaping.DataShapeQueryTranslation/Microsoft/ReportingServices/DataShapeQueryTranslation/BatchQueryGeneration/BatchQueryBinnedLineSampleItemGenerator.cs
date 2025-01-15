using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000132 RID: 306
	internal sealed class BatchQueryBinnedLineSampleItemGenerator : IPlanBinnedLineSampleItemVisitor
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x0002DB47 File Offset: 0x0002BD47
		private BatchQueryBinnedLineSampleItemGenerator(GeneratedTable input, CalculationExpressionMap calculationExpressions)
		{
			this.m_input = input;
			this.m_calculationExpressions = calculationExpressions;
			this.m_columns = new QueryItemCollection<QueryExpression>();
			this.m_sortDirections = null;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002DB70 File Offset: 0x0002BD70
		public static IReadOnlyList<QueryExpression> Generate(GeneratedTable input, IReadOnlyList<PlanBinnedLineSampleItem> planBinnedLineSampleItems, CalculationExpressionMap calculationExpressions, out List<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection> sortDirections)
		{
			BatchQueryBinnedLineSampleItemGenerator batchQueryBinnedLineSampleItemGenerator = new BatchQueryBinnedLineSampleItemGenerator(input, calculationExpressions);
			foreach (PlanBinnedLineSampleItem planBinnedLineSampleItem in planBinnedLineSampleItems)
			{
				planBinnedLineSampleItem.Accept(batchQueryBinnedLineSampleItemGenerator);
			}
			sortDirections = batchQueryBinnedLineSampleItemGenerator.m_sortDirections;
			return batchQueryBinnedLineSampleItemGenerator.m_columns;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public static QueryExpression GenerateSingleColumn(GeneratedTable input, PlanBinnedLineSampleItem planBinnedLineSampleItem, CalculationExpressionMap calculationExpressions)
		{
			BatchQueryBinnedLineSampleItemGenerator batchQueryBinnedLineSampleItemGenerator = new BatchQueryBinnedLineSampleItemGenerator(input, calculationExpressions);
			planBinnedLineSampleItem.Accept(batchQueryBinnedLineSampleItemGenerator);
			return batchQueryBinnedLineSampleItemGenerator.m_columns.Single("Expected one column for PlanBinnedLineSampleItem", Array.Empty<string>());
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002DC00 File Offset: 0x0002BE00
		public void Visit(PlanBinnedLineSampleMember item)
		{
			foreach (SortKey sortKey in item.Member.Group.SortKeys)
			{
				Util.AddToLazyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection>(ref this.m_sortDirections, BatchQueryGenerationUtils.TranslateSortDirection(sortKey.SortDirection.Value, false));
				this.AddReferenceExpression(sortKey.Value.ExpressionId.Value);
			}
			foreach (GroupKey groupKey in item.Member.Group.GroupKeys)
			{
				this.AddReferenceExpression(groupKey.Value.ExpressionId.Value);
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002DCEC File Offset: 0x0002BEEC
		public void Visit(PlanBinnedLineSampleCalculation item)
		{
			ReadOnlyCollection<ExpressionId> expressions = this.m_calculationExpressions.GetExpressions(item.Calculation);
			for (int i = 0; i < expressions.Count; i++)
			{
				this.AddReferenceExpression(expressions[i]);
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002DD2C File Offset: 0x0002BF2C
		private void AddReferenceExpression(ExpressionId exprId)
		{
			QueryTableColumn queryTableColumn = this.m_input.ColumnMap[exprId];
			this.m_columns.Add(queryTableColumn.Name, queryTableColumn.QdmReference());
		}

		// Token: 0x040005CB RID: 1483
		private readonly GeneratedTable m_input;

		// Token: 0x040005CC RID: 1484
		private readonly CalculationExpressionMap m_calculationExpressions;

		// Token: 0x040005CD RID: 1485
		private readonly QueryItemCollection<QueryExpression> m_columns;

		// Token: 0x040005CE RID: 1486
		private List<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection> m_sortDirections;
	}
}
