using System;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B0C RID: 2828
	internal sealed class GoogleAnalyticsSortOrder : GoogleAnalyticsExpression
	{
		// Token: 0x06004E57 RID: 20055 RVA: 0x00103BE7 File Offset: 0x00101DE7
		public GoogleAnalyticsSortOrder(GoogleAnalyticsIdentifierExpression identifier, bool ascending)
		{
			this.identifier = identifier;
			this.ascending = ascending;
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06004E58 RID: 20056 RVA: 0x00103BFD File Offset: 0x00101DFD
		public bool Ascending
		{
			get
			{
				return this.ascending;
			}
		}

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x0000244F File Offset: 0x0000064F
		public override GoogleAnalyticsExpressionKind Kind
		{
			get
			{
				return GoogleAnalyticsExpressionKind.Sort;
			}
		}

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x00103C05 File Offset: 0x00101E05
		public string Identifier
		{
			get
			{
				return this.identifier.Identifier;
			}
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06004E5B RID: 20059 RVA: 0x00103C12 File Offset: 0x00101E12
		public GoogleAnalyticsIdentifierExpression IdentifierExpression
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x00103C1A File Offset: 0x00101E1A
		public override string QueryString
		{
			get
			{
				if (!this.ascending)
				{
					return "-" + this.identifier.Identifier;
				}
				return this.identifier.Identifier;
			}
		}

		// Token: 0x04002A1F RID: 10783
		private readonly GoogleAnalyticsIdentifierExpression identifier;

		// Token: 0x04002A20 RID: 10784
		private readonly bool ascending;
	}
}
