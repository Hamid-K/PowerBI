using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200000A RID: 10
	public enum FuzzyIndexType
	{
		// Token: 0x04000010 RID: 16
		Default,
		// Token: 0x04000011 RID: 17
		LocalitySensitiveHashing,
		// Token: 0x04000012 RID: 18
		PrefixFiltering,
		// Token: 0x04000013 RID: 19
		NegativeBorder,
		// Token: 0x04000014 RID: 20
		InvertedIndex,
		// Token: 0x04000015 RID: 21
		InvertedIndexWithoutClustering,
		// Token: 0x04000016 RID: 22
		Identity,
		// Token: 0x04000017 RID: 23
		Exact
	}
}
