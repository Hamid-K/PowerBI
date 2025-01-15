using System;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A8 RID: 168
	public interface IPosTagSet
	{
		// Token: 0x06000365 RID: 869
		bool IsNoun(PosTagKind kind);

		// Token: 0x06000366 RID: 870
		bool IsVerb(PosTagKind kind);

		// Token: 0x06000367 RID: 871
		bool IsModal(PosTagKind kind);

		// Token: 0x06000368 RID: 872
		bool IsAdjective(PosTagKind kind);

		// Token: 0x06000369 RID: 873
		bool IsAdverb(PosTagKind kind);

		// Token: 0x0600036A RID: 874
		bool IsPreposition(PosTagKind kind);

		// Token: 0x0600036B RID: 875
		bool IsNounPlural(PosTagKind kind);

		// Token: 0x0600036C RID: 876
		Func<PosTagKind, bool> IsNonNoun();

		// Token: 0x0600036D RID: 877
		Func<StemmerSuggestion, bool> StemIsNoun();

		// Token: 0x0600036E RID: 878
		Func<StemmerSuggestion, bool> StemIsNounPlural();
	}
}
