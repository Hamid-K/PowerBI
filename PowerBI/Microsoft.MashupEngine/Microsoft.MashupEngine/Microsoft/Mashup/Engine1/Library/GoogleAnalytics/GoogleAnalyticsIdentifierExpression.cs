using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B0B RID: 2827
	internal sealed class GoogleAnalyticsIdentifierExpression : GoogleAnalyticsValueExpression
	{
		// Token: 0x06004E50 RID: 20048 RVA: 0x00103BA5 File Offset: 0x00101DA5
		public GoogleAnalyticsIdentifierExpression(string identifier, GoogleAnalyticsDataType dataType, GoogleAnalyticsCubeObjectKind columnKind)
		{
			this.identifier = identifier;
			this.dataType = dataType;
			this.columnKind = columnKind;
		}

		// Token: 0x1700188A RID: 6282
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x00103BC2 File Offset: 0x00101DC2
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700188B RID: 6283
		// (get) Token: 0x06004E52 RID: 20050 RVA: 0x000023C4 File Offset: 0x000005C4
		public override GoogleAnalyticsExpressionKind Kind
		{
			get
			{
				return GoogleAnalyticsExpressionKind.Identifier;
			}
		}

		// Token: 0x1700188C RID: 6284
		// (get) Token: 0x06004E53 RID: 20051 RVA: 0x00103BC2 File Offset: 0x00101DC2
		public override string QueryString
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06004E54 RID: 20052 RVA: 0x00103BCA File Offset: 0x00101DCA
		public override GoogleAnalyticsDataType DataType
		{
			get
			{
				return this.dataType;
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06004E55 RID: 20053 RVA: 0x00103BD2 File Offset: 0x00101DD2
		public override GoogleAnalyticsCubeObjectKind ColumnKind
		{
			get
			{
				return this.columnKind;
			}
		}

		// Token: 0x06004E56 RID: 20054 RVA: 0x00103BDA File Offset: 0x00101DDA
		protected override Value GenerateV2Filter()
		{
			return TextValue.New(this.identifier);
		}

		// Token: 0x04002A1C RID: 10780
		private readonly string identifier;

		// Token: 0x04002A1D RID: 10781
		private readonly GoogleAnalyticsDataType dataType;

		// Token: 0x04002A1E RID: 10782
		private readonly GoogleAnalyticsCubeObjectKind columnKind;
	}
}
