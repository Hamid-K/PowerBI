using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000167 RID: 359
	public interface IEntityTermLookup<T> : IDisposable
	{
		// Token: 0x0600070B RID: 1803
		IEnumerable<TokenSequenceMatch<T>> FindTokenSequenceMatchesForNonCompletion(IEnumerable<ITokenSequenceSearchDefinition> searchDefinitions, SearchSettings searchSettings, CancellationToken cancellationToken);

		// Token: 0x0600070C RID: 1804
		IEnumerable<TokenSequencePrefixMatch<T>> FindTokenSequenceMatchesForCompletion(ITokenSequenceSearchDefinition searchDefinition, SearchSettings searchSettings, CancellationToken cancellationToken, int targetMatchingInstances = 10);
	}
}
