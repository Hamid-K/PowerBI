using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction
{
	// Token: 0x020001A2 RID: 418
	public interface ITokenizer<out TToken, in TSequenceable, in TSequence> where TToken : IToken<TSequenceable, TSequence> where TSequence : IEnumerable<TSequenceable>
	{
		// Token: 0x06000922 RID: 2338
		IEnumerable<TToken> Tokenize(TSequence sequence);

		// Token: 0x06000923 RID: 2339
		IEnumerable<IEnumerable<TToken>> Tokenize(IEnumerable<TSequence> sequences);

		// Token: 0x06000924 RID: 2340
		TToken TokenAtStartOrDefault(TSequence sequence);
	}
}
