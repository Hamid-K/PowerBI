using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000B9 RID: 185
	[ImmutableObject(true)]
	public abstract class PosTagSet : IPosTagSet
	{
		// Token: 0x060003B9 RID: 953
		public abstract bool IsNoun(PosTagKind kind);

		// Token: 0x060003BA RID: 954
		public abstract bool IsVerb(PosTagKind kind);

		// Token: 0x060003BB RID: 955
		public abstract bool IsModal(PosTagKind kind);

		// Token: 0x060003BC RID: 956
		public abstract bool IsAdjective(PosTagKind kind);

		// Token: 0x060003BD RID: 957
		public abstract bool IsAdverb(PosTagKind kind);

		// Token: 0x060003BE RID: 958
		public abstract bool IsPreposition(PosTagKind kind);

		// Token: 0x060003BF RID: 959
		public abstract bool IsNounPlural(PosTagKind kind);

		// Token: 0x060003C0 RID: 960
		public abstract Func<PosTagKind, bool> IsNonNoun();

		// Token: 0x060003C1 RID: 961
		public abstract Func<StemmerSuggestion, bool> StemIsNoun();

		// Token: 0x060003C2 RID: 962
		public abstract Func<StemmerSuggestion, bool> StemIsNounPlural();
	}
}
