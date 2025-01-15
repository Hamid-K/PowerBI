using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000AF RID: 175
	internal sealed class FilterAnalyzer : FilterExpressionVisitor
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x0001DC57 File Offset: 0x0001BE57
		private FilterAnalyzer(ExpressionTable expressionTable)
			: base(null)
		{
			this.m_expressionTable = expressionTable;
			this.m_isScopeFilter = false;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001DC6E File Offset: 0x0001BE6E
		public static bool IsScopeFilter(Filter filter, ExpressionTable expressionTable)
		{
			FilterAnalyzer filterAnalyzer = new FilterAnalyzer(expressionTable);
			filterAnalyzer.Visit(filter.Condition);
			return filterAnalyzer.m_isScopeFilter;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001DC88 File Offset: 0x0001BE88
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			if (this.m_isScopeFilter)
			{
				return;
			}
			if (MeasureAnalyzer.IsMeasure(this.m_expressionTable.GetNode(expression)))
			{
				this.m_isScopeFilter = true;
			}
		}

		// Token: 0x040003D2 RID: 978
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040003D3 RID: 979
		private bool m_isScopeFilter;
	}
}
