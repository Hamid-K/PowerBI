using System;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B6B RID: 2923
	internal class RepresentativeValueWithSimilarity
	{
		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x060050C7 RID: 20679 RVA: 0x0010E8D6 File Offset: 0x0010CAD6
		public string Value { get; }

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x0010E8DE File Offset: 0x0010CADE
		public string RepresentativeValue { get; }

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x060050C9 RID: 20681 RVA: 0x0010E8E6 File Offset: 0x0010CAE6
		public double Similarity { get; }

		// Token: 0x060050CA RID: 20682 RVA: 0x0010E8EE File Offset: 0x0010CAEE
		public RepresentativeValueWithSimilarity(string value, string representativeValue, double similarity)
		{
			this.Value = value;
			this.RepresentativeValue = representativeValue;
			this.Similarity = similarity;
		}
	}
}
