using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F5D RID: 3933
	internal class AdobeAnalyticsConstantExpression : AdobeAnalyticsExpression
	{
		// Token: 0x060067DB RID: 26587 RVA: 0x0016544D File Offset: 0x0016364D
		public AdobeAnalyticsConstantExpression(string value)
		{
			this.Value = value;
		}

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x060067DC RID: 26588 RVA: 0x00002139 File Offset: 0x00000339
		public override AdobeAnalyticsExpressionKind Kind
		{
			get
			{
				return AdobeAnalyticsExpressionKind.Constant;
			}
		}

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x060067DD RID: 26589 RVA: 0x0016545C File Offset: 0x0016365C
		// (set) Token: 0x060067DE RID: 26590 RVA: 0x00165464 File Offset: 0x00163664
		public string Value { get; private set; }
	}
}
