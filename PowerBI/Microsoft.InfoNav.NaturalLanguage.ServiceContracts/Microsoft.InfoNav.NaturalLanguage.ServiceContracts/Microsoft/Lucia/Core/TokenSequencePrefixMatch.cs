using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200013C RID: 316
	public sealed class TokenSequencePrefixMatch<T> : TokenSequenceMatch<T>
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0000B0D2 File Offset: 0x000092D2
		public TokenSequencePrefixMatch(T data, TokenSequenceMatchType matchType, IEnumerable<TokenMatch> tokenMatches, IEnumerable<TextSegment> matchedSegments, string sequenceText, IEnumerable<string> matchingValues, double? weight, double score)
			: base(data, matchType, tokenMatches, matchedSegments, sequenceText, weight, score)
		{
			this.MatchingValues = matchingValues.AsReadOnlyList<string>();
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000B0F2 File Offset: 0x000092F2
		public IReadOnlyList<string> MatchingValues { get; }

		// Token: 0x06000634 RID: 1588 RVA: 0x0000B0FA File Offset: 0x000092FA
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000B103 File Offset: 0x00009303
		public override string ToString(bool verbose)
		{
			return StringUtil.FormatInvariant("{0} ({1})", base.ToString(verbose), string.Join("|", this.MatchingValues));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000B126 File Offset: 0x00009326
		public TokenSequencePrefixMatch<T> WithMatchingValues(IEnumerable<string> matchingValues)
		{
			return new TokenSequencePrefixMatch<T>(base.Data, base.MatchType, base.TokenMatches, base.MatchedSegments, base.SequenceText, matchingValues, base.Weight, base.Score);
		}
	}
}
