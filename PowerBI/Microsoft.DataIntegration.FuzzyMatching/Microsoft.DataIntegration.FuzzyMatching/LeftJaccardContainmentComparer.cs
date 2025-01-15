using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000B9 RID: 185
	[Serializable]
	public class LeftJaccardContainmentComparer : FuzzyComparer
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001F505 File Offset: 0x0001D705
		public LeftJaccardContainmentComparer()
			: base(FuzzyComparisonType.LeftJaccardContainment)
		{
		}
	}
}
