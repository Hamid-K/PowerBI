using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F5B RID: 3931
	internal abstract class AdobeAnalyticsExpression
	{
		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x060067D3 RID: 26579
		public abstract AdobeAnalyticsExpressionKind Kind { get; }

		// Token: 0x060067D4 RID: 26580 RVA: 0x001653E0 File Offset: 0x001635E0
		public bool TryGetConstant(out string constant)
		{
			AdobeAnalyticsConstantExpression adobeAnalyticsConstantExpression = this as AdobeAnalyticsConstantExpression;
			if (adobeAnalyticsConstantExpression != null)
			{
				constant = adobeAnalyticsConstantExpression.Value;
				return true;
			}
			constant = null;
			return false;
		}

		// Token: 0x060067D5 RID: 26581 RVA: 0x00165408 File Offset: 0x00163608
		public bool TryGetIdentifier(out string identifier)
		{
			AdobeAnalyticsIdentifierExpression adobeAnalyticsIdentifierExpression = this as AdobeAnalyticsIdentifierExpression;
			if (adobeAnalyticsIdentifierExpression != null)
			{
				identifier = adobeAnalyticsIdentifierExpression.Identifier;
				return true;
			}
			identifier = null;
			return false;
		}
	}
}
