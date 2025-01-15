using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000AF4 RID: 2804
	internal abstract class GoogleAnalyticsConstantExpression : GoogleAnalyticsValueExpression
	{
		// Token: 0x17001853 RID: 6227
		// (get) Token: 0x06004DDB RID: 19931 RVA: 0x00002139 File Offset: 0x00000339
		public override GoogleAnalyticsExpressionKind Kind
		{
			get
			{
				return GoogleAnalyticsExpressionKind.Constant;
			}
		}

		// Token: 0x17001854 RID: 6228
		// (get) Token: 0x06004DDC RID: 19932 RVA: 0x000023C4 File Offset: 0x000005C4
		public override GoogleAnalyticsCubeObjectKind ColumnKind
		{
			get
			{
				return GoogleAnalyticsCubeObjectKind.Constant;
			}
		}
	}
}
