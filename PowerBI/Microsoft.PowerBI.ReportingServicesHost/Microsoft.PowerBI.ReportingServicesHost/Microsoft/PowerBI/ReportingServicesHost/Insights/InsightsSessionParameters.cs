using System;
using Microsoft.PowerBI.Data.ModelSchemaAnalysis;

namespace Microsoft.PowerBI.ReportingServicesHost.Insights
{
	// Token: 0x02000069 RID: 105
	public sealed class InsightsSessionParameters
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00006588 File Offset: 0x00004788
		public InsightsSessionParameters(bool directQueryContent = false, IMeasureExpressionProvider expressionProvider = null, InsightsSessionDataSourceType dataSourceType = InsightsSessionDataSourceType.Other)
		{
			this.ModelHasDirectQueryContent = directQueryContent;
			this.ExpressionProvider = expressionProvider;
			this.DataSourceType = dataSourceType;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000065A5 File Offset: 0x000047A5
		public bool ModelHasDirectQueryContent { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000065AD File Offset: 0x000047AD
		public IMeasureExpressionProvider ExpressionProvider { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000065B5 File Offset: 0x000047B5
		public InsightsSessionDataSourceType DataSourceType { get; }
	}
}
