using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F63 RID: 3939
	internal sealed class AdobeAnalyticsMeasure : AdobeAnalyticsCubeObject
	{
		// Token: 0x060067F9 RID: 26617 RVA: 0x001653CC File Offset: 0x001635CC
		private AdobeAnalyticsMeasure(string name, string id)
			: base(name, id)
		{
		}

		// Token: 0x17001E13 RID: 7699
		// (get) Token: 0x060067FA RID: 26618 RVA: 0x00165BF6 File Offset: 0x00163DF6
		// (set) Token: 0x060067FB RID: 26619 RVA: 0x00165BFE File Offset: 0x00163DFE
		public string Type { get; private set; }

		// Token: 0x17001E14 RID: 7700
		// (get) Token: 0x060067FC RID: 26620 RVA: 0x00165C07 File Offset: 0x00163E07
		// (set) Token: 0x060067FD RID: 26621 RVA: 0x00165C0F File Offset: 0x00163E0F
		public int Decimals { get; private set; }

		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x060067FE RID: 26622 RVA: 0x00165C18 File Offset: 0x00163E18
		// (set) Token: 0x060067FF RID: 26623 RVA: 0x00165C20 File Offset: 0x00163E20
		public string Formula { get; private set; }

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06006800 RID: 26624 RVA: 0x00002105 File Offset: 0x00000305
		public override AdobeAnalyticsCubeObjectKind Kind
		{
			get
			{
				return AdobeAnalyticsCubeObjectKind.Measure;
			}
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x00165C29 File Offset: 0x00163E29
		public static AdobeAnalyticsMeasure New(string name, string id)
		{
			return new AdobeAnalyticsMeasure(name, id);
		}
	}
}
