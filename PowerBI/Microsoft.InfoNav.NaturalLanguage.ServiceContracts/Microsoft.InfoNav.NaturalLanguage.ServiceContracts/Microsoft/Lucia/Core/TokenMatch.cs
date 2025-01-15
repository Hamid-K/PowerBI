using System;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200013A RID: 314
	public sealed class TokenMatch : IEquatable<TokenMatch>
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public TokenMatch(int tokenIndex, TokenMatchType matchType)
		{
			this.TokenIndex = tokenIndex;
			this.MatchType = matchType;
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000AD92 File Offset: 0x00008F92
		public int TokenIndex { get; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000AD9A File Offset: 0x00008F9A
		public TokenMatchType MatchType { get; }

		// Token: 0x06000623 RID: 1571 RVA: 0x0000ADA2 File Offset: 0x00008FA2
		public bool Equals(TokenMatch other)
		{
			return other != null && this.TokenIndex == other.TokenIndex && this.MatchType == other.MatchType;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TokenMatch);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.TokenIndex.GetHashCode(), this.MatchType.GetHashCode());
		}

		// Token: 0x04000621 RID: 1569
		internal static readonly Func<TokenMatch, int> TokenIndexSelector = (TokenMatch tokenMatch) => tokenMatch.TokenIndex;
	}
}
