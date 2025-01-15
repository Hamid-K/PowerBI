using System;
using System.Collections.ObjectModel;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000125 RID: 293
	public interface IStemmerToken : IPosTaggerToken, ISpellCorrectedToken, IToken, IUtteranceToken
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005E1 RID: 1505
		ReadOnlyCollection<StemmerSuggestion> Stems { get; }
	}
}
