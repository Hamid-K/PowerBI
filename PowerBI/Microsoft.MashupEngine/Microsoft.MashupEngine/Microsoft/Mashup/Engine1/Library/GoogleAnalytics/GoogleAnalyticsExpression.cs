using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B08 RID: 2824
	internal abstract class GoogleAnalyticsExpression
	{
		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06004E47 RID: 20039
		public abstract GoogleAnalyticsExpressionKind Kind { get; }

		// Token: 0x17001886 RID: 6278
		// (get) Token: 0x06004E48 RID: 20040
		public abstract string QueryString { get; }
	}
}
