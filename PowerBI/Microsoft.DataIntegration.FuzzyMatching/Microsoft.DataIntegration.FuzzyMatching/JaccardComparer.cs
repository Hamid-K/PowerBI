using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	public class JaccardComparer : FuzzyComparer
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x0001F4FC File Offset: 0x0001D6FC
		public JaccardComparer()
			: base(FuzzyComparisonType.Jaccard)
		{
		}
	}
}
