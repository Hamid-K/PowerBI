using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000138 RID: 312
	public enum TokenMatchType
	{
		// Token: 0x04000617 RID: 1559
		OriginalValueMatch,
		// Token: 0x04000618 RID: 1560
		SpellCorrectedMatch,
		// Token: 0x04000619 RID: 1561
		StemmedMatch,
		// Token: 0x0400061A RID: 1562
		SynonymMatch,
		// Token: 0x0400061B RID: 1563
		InferredMatch,
		// Token: 0x0400061C RID: 1564
		StemmedSpellCorrectedMatch
	}
}
