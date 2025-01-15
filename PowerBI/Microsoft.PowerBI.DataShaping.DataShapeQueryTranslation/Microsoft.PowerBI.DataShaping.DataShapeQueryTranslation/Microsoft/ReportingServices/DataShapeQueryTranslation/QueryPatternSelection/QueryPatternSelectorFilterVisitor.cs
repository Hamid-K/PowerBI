using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x02000072 RID: 114
	internal sealed class QueryPatternSelectorFilterVisitor : FilterExpressionVisitor
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00014D6D File Offset: 0x00012F6D
		internal QueryPatternSelectorFilterVisitor(QueryPatternSelectionContext context, QueryPatternReasonCollection reasons)
			: base(null)
		{
			this.m_context = context;
			this.m_reasons = reasons;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00014D84 File Offset: 0x00012F84
		public void Analyze(Filter filter)
		{
			base.Visit(filter);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00014D8E File Offset: 0x00012F8E
		internal override FilterCondition Visit(ExistsFilterCondition condition)
		{
			this.m_reasons.RegisterBatchPatternOnlyReason(QueryPatternReason.ExistsFilter);
			return condition;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00014DA0 File Offset: 0x00012FA0
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			ExpressionNode expressionNode = this.m_context.ExpressionTable.GetNode(expression);
			ExpressionNode expressionNode2;
			if (ExpressionAnalysisUtils.IsFilterByRollupScopeMeasureExpression(expressionNode, out expressionNode2))
			{
				expressionNode = expressionNode2;
			}
			bool flag2;
			bool flag = QueryPatternSelectorExpressionVisitor.IsValidForBatchQueryPattern(this.m_context, expressionNode, false, false, false, null, out flag2);
			this.m_reasons.CheckBatchPrerequisite(flag, QueryPatternReason.ExpressionFeature);
		}

		// Token: 0x040002E5 RID: 741
		private readonly QueryPatternSelectionContext m_context;

		// Token: 0x040002E6 RID: 742
		private readonly QueryPatternReasonCollection m_reasons;
	}
}
