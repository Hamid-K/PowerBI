using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001220 RID: 4640
	public sealed class MatchingLabel : IEquatable<MatchingLabel>
	{
		// Token: 0x06008BBD RID: 35773 RVA: 0x001D45F4 File Offset: 0x001D27F4
		private MatchingLabel(MatchingLabel.MatchType match, IImmutableList<IToken> tokens = null)
		{
			this.Match = match;
			this._tokenSequence = tokens;
		}

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x06008BBE RID: 35774 RVA: 0x001D460A File Offset: 0x001D280A
		public bool IsMatch
		{
			get
			{
				return this.Match != MatchingLabel.MatchType.NoMatch;
			}
		}

		// Token: 0x06008BBF RID: 35775 RVA: 0x001D4618 File Offset: 0x001D2818
		public IReadOnlyList<IToken> GetTokens()
		{
			if (this.Match != MatchingLabel.MatchType.TokenSequenceMatch)
			{
				throw new InvalidOperationException();
			}
			return this._tokenSequence;
		}

		// Token: 0x06008BC0 RID: 35776 RVA: 0x001D462F File Offset: 0x001D282F
		public static MatchingLabel Tokens(IImmutableList<IToken> tokens)
		{
			return new MatchingLabel(MatchingLabel.MatchType.TokenSequenceMatch, tokens);
		}

		// Token: 0x06008BC1 RID: 35777 RVA: 0x001D4638 File Offset: 0x001D2838
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || this.Equals(obj as MatchingLabel));
		}

		// Token: 0x06008BC2 RID: 35778 RVA: 0x001D4654 File Offset: 0x001D2854
		public bool Equals(MatchingLabel other)
		{
			return other != null && (this == other || (this.Match == other.Match && ((this._tokenSequence == null && other._tokenSequence == null) || (this._tokenSequence != null && other._tokenSequence != null && this._tokenSequence.SequenceEqual(other._tokenSequence)))));
		}

		// Token: 0x06008BC3 RID: 35779 RVA: 0x001D46B4 File Offset: 0x001D28B4
		public override int GetHashCode()
		{
			IImmutableList<IToken> tokenSequence = this._tokenSequence;
			return (((tokenSequence != null) ? tokenSequence.OrderDependentHashCode<IToken>() : 0) * 397) ^ this.Match.GetHashCode();
		}

		// Token: 0x06008BC4 RID: 35780 RVA: 0x001D46EE File Offset: 0x001D28EE
		public override string ToString()
		{
			return this.Description();
		}

		// Token: 0x0400391E RID: 14622
		private readonly IImmutableList<IToken> _tokenSequence;

		// Token: 0x0400391F RID: 14623
		public readonly MatchingLabel.MatchType Match;

		// Token: 0x04003920 RID: 14624
		public static readonly MatchingLabel NoMatch = new MatchingLabel(MatchingLabel.MatchType.NoMatch, null);

		// Token: 0x04003921 RID: 14625
		public static readonly MatchingLabel NullMatch = new MatchingLabel(MatchingLabel.MatchType.NullMatch, null);

		// Token: 0x02001221 RID: 4641
		public enum MatchType
		{
			// Token: 0x04003923 RID: 14627
			NullMatch,
			// Token: 0x04003924 RID: 14628
			NoMatch,
			// Token: 0x04003925 RID: 14629
			TokenSequenceMatch
		}
	}
}
