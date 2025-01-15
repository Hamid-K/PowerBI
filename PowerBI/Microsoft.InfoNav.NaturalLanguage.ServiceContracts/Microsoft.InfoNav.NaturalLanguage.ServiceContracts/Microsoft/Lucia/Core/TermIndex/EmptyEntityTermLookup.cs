using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000168 RID: 360
	public sealed class EmptyEntityTermLookup<T> : IEntityTermLookup<T>, IDisposable
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0000C272 File Offset: 0x0000A472
		private EmptyEntityTermLookup()
		{
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000C27A File Offset: 0x0000A47A
		public void Dispose()
		{
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000C27C File Offset: 0x0000A47C
		public IEnumerable<TokenSequenceMatch<T>> FindTokenSequenceMatchesForNonCompletion(IEnumerable<ITokenSequenceSearchDefinition> searchDefinitions, SearchSettings searchSettings, CancellationToken cancellationToken)
		{
			return Util.EmptyReadOnlyCollection<TokenSequenceMatch<T>>();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000C283 File Offset: 0x0000A483
		public IEnumerable<TokenSequencePrefixMatch<T>> FindTokenSequenceMatchesForCompletion(ITokenSequenceSearchDefinition searchDefinition, SearchSettings searchSettings, CancellationToken cancellationToken, int targetMatchingInstances)
		{
			return Util.EmptyReadOnlyCollection<TokenSequencePrefixMatch<T>>();
		}

		// Token: 0x040006AB RID: 1707
		public static readonly EmptyEntityTermLookup<T> Instance = new EmptyEntityTermLookup<T>();
	}
}
