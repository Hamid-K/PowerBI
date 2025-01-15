using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001DA RID: 474
	internal sealed class QueryExpressionMeasureAnalyzer : DefaultQueryExpressionVisitor
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x000190F4 File Offset: 0x000172F4
		private QueryExpressionMeasureAnalyzer()
		{
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000190FC File Offset: 0x000172FC
		internal static bool IsMeasure(QueryExpressionContainer expression)
		{
			QueryExpressionMeasureAnalyzer queryExpressionMeasureAnalyzer = new QueryExpressionMeasureAnalyzer();
			queryExpressionMeasureAnalyzer.VisitExpression(expression);
			return queryExpressionMeasureAnalyzer._isMeasure;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0001910F File Offset: 0x0001730F
		protected internal override void Visit(QueryMeasureExpression expression)
		{
			this._isMeasure = true;
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00019118 File Offset: 0x00017318
		protected internal override void Visit(QueryAggregationExpression expression)
		{
			this._isMeasure = true;
		}

		// Token: 0x040006A6 RID: 1702
		private bool _isMeasure;
	}
}
