using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F5C RID: 3932
	internal class AdobeAnalyticsIdentifierExpression : AdobeAnalyticsExpression
	{
		// Token: 0x060067D7 RID: 26583 RVA: 0x0016542D File Offset: 0x0016362D
		public AdobeAnalyticsIdentifierExpression(string identifier)
		{
			this.Identifier = identifier;
		}

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x060067D8 RID: 26584 RVA: 0x00002105 File Offset: 0x00000305
		public override AdobeAnalyticsExpressionKind Kind
		{
			get
			{
				return AdobeAnalyticsExpressionKind.Identifier;
			}
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x060067D9 RID: 26585 RVA: 0x0016543C File Offset: 0x0016363C
		// (set) Token: 0x060067DA RID: 26586 RVA: 0x00165444 File Offset: 0x00163644
		public string Identifier { get; private set; }
	}
}
