using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000166 RID: 358
	public interface IEntityTermFilter<T>
	{
		// Token: 0x06000709 RID: 1801
		IEnumerable<TokenSequenceMatch<T>> FilterNonCompletionMatches(IEnumerable<TokenSequenceMatch<T>> matches);

		// Token: 0x0600070A RID: 1802
		IEnumerable<TokenSequencePrefixMatch<T>> FilterCompletionMatches(IEnumerable<TokenSequencePrefixMatch<T>> matches);
	}
}
