using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction
{
	// Token: 0x020001A4 RID: 420
	public abstract class Tokenizer<TToken, TSequenceable, TSequence> : ITokenizer<TToken, TSequenceable, TSequence> where TToken : IToken<TSequenceable, TSequence> where TSequence : IEnumerable<TSequenceable>
	{
		// Token: 0x06000932 RID: 2354
		public abstract IEnumerable<TToken> Tokenize(TSequence sequence);

		// Token: 0x06000933 RID: 2355 RVA: 0x0001B532 File Offset: 0x00019732
		public IEnumerable<IEnumerable<TToken>> Tokenize(IEnumerable<TSequence> sequences)
		{
			return sequences.Select(new Func<TSequence, IEnumerable<TToken>>(this.Tokenize));
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0001B547 File Offset: 0x00019747
		public virtual TToken TokenAtStartOrDefault(TSequence sequence)
		{
			return (from first in this.Tokenize(sequence).MaybeFirst<TToken>()
				where first.StartInSequence == 0
				select first).OrElseDefault<TToken>();
		}
	}
}
