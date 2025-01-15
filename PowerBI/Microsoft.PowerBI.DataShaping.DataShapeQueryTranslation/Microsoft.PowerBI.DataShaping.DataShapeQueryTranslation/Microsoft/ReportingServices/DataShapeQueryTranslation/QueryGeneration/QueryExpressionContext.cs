using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000085 RID: 133
	internal sealed class QueryExpressionContext
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x000175BB File Offset: 0x000157BB
		internal QueryExpressionContext(QueryExpression queryExpression, QueryExpressionFeatures queryExpressionFeatures, bool calculateInMeasureContext)
		{
			this.m_queryExpression = queryExpression;
			this.m_queryExpressionFeatures = queryExpressionFeatures;
			this.m_calculateInMeasureContext = calculateInMeasureContext;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x000175D8 File Offset: 0x000157D8
		public QueryExpression QueryExpression
		{
			get
			{
				return this.m_queryExpression;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x000175E0 File Offset: 0x000157E0
		public QueryExpressionFeatures QueryExpressionFeatures
		{
			get
			{
				return this.m_queryExpressionFeatures;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x000175E8 File Offset: 0x000157E8
		public bool CalculateAsMeasure
		{
			get
			{
				return this.m_queryExpressionFeatures.HasFlag(QueryExpressionFeatures.QueryMeasure) || this.m_queryExpressionFeatures.HasFlag(QueryExpressionFeatures.ModelMeasure);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001761A File Offset: 0x0001581A
		public bool CalculateInMeasureContext
		{
			get
			{
				return this.m_calculateInMeasureContext;
			}
		}

		// Token: 0x0400031F RID: 799
		private readonly QueryExpression m_queryExpression;

		// Token: 0x04000320 RID: 800
		private readonly QueryExpressionFeatures m_queryExpressionFeatures;

		// Token: 0x04000321 RID: 801
		private readonly bool m_calculateInMeasureContext;
	}
}
