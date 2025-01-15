using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000251 RID: 593
	internal sealed class FilterAnnotationAnalyzer : FilterExpressionVisitor
	{
		// Token: 0x06001487 RID: 5255 RVA: 0x0004E8B9 File Offset: 0x0004CAB9
		private FilterAnnotationAnalyzer(ExpressionTable expressionTable, VisitDataShapeDelegate visitDataShape)
			: base(visitDataShape)
		{
			this.m_expressionTable = expressionTable;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0004E8CC File Offset: 0x0004CACC
		public static FilterAnnotationAnalyzerResult Analyze(Filter filter, ExpressionTable expressionTable, VisitDataShapeDelegate visitDataShape)
		{
			FilterAnnotationAnalyzer filterAnnotationAnalyzer = new FilterAnnotationAnalyzer(expressionTable, visitDataShape);
			filterAnnotationAnalyzer.Visit(filter);
			return new FilterAnnotationAnalyzerResult(filterAnnotationAnalyzer.m_contextDataShape, filterAnnotationAnalyzer.m_isApplyFilter, filterAnnotationAnalyzer.m_isScopeFilter);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004E900 File Offset: 0x0004CB00
		internal override FilterCondition Visit(ContextFilterCondition condition)
		{
			base.Visit(condition);
			this.m_contextDataShape = condition.DataShape;
			return condition;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004E917 File Offset: 0x0004CB17
		internal override FilterCondition Visit(ApplyFilterCondition condition)
		{
			base.Visit(condition);
			this.m_isApplyFilter = true;
			return condition;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0004E929 File Offset: 0x0004CB29
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			if (MeasureAnalyzer.IsMeasure(this.m_expressionTable.GetNode(expression)))
			{
				this.m_isScopeFilter = true;
			}
		}

		// Token: 0x0400091E RID: 2334
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x0400091F RID: 2335
		private DataShape m_contextDataShape;

		// Token: 0x04000920 RID: 2336
		private bool m_isApplyFilter;

		// Token: 0x04000921 RID: 2337
		private bool m_isScopeFilter;
	}
}
