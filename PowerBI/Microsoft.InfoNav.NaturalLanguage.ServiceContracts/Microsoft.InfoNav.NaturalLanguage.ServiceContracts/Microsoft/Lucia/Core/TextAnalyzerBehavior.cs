using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200012D RID: 301
	[Flags]
	public enum TextAnalyzerBehavior
	{
		// Token: 0x040005E6 RID: 1510
		Tokenize = 1,
		// Token: 0x040005E7 RID: 1511
		SpellCorrect = 2,
		// Token: 0x040005E8 RID: 1512
		PosTag = 4,
		// Token: 0x040005E9 RID: 1513
		Stem = 8,
		// Token: 0x040005EA RID: 1514
		TokenizePosTag = 5,
		// Token: 0x040005EB RID: 1515
		TokenizePosTagStem = 13,
		// Token: 0x040005EC RID: 1516
		All = 15
	}
}
