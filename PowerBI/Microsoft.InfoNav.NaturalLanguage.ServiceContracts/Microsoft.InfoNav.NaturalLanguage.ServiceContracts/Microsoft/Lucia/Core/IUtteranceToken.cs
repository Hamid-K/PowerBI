using System;
using System.Collections.ObjectModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000AE RID: 174
	public interface IUtteranceToken
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000386 RID: 902
		int TokenStartIndex { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000387 RID: 903
		int TokenLength { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000388 RID: 904
		string TokenValue { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000389 RID: 905
		ReadOnlyCollection<PosTag> TokenPosTags { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600038A RID: 906
		ReadOnlyCollection<StemmerSuggestion> TokenStems { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600038B RID: 907
		SpellCorrectionSuggestion TokenSpellCorrection { get; }
	}
}
