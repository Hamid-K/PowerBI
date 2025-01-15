using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200013D RID: 317
	[ImmutableObject(true)]
	public sealed class TokenSequenceMatchComparer<T> : IEqualityComparer<TokenSequenceMatch<T>> where T : IEquatable<T>
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0000B158 File Offset: 0x00009358
		public bool Equals(TokenSequenceMatch<T> x, TokenSequenceMatch<T> y)
		{
			if (x.TokenMatches[0].TokenIndex == y.TokenMatches[0].TokenIndex && x.TokenMatches.Last<TokenMatch>().TokenIndex == y.TokenMatches.Last<TokenMatch>().TokenIndex)
			{
				T data = x.Data;
				return data.Equals(y.Data);
			}
			return false;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public int GetHashCode(TokenSequenceMatch<T> obj)
		{
			int hashCode = obj.TokenMatches[0].TokenIndex.GetHashCode();
			int hashCode2 = obj.TokenMatches.Last<TokenMatch>().TokenIndex.GetHashCode();
			T data = obj.Data;
			return Hashing.CombineHash(hashCode, hashCode2, data.GetHashCode());
		}

		// Token: 0x0400062C RID: 1580
		public static readonly TokenSequenceMatchComparer<T> Instance = new TokenSequenceMatchComparer<T>();
	}
}
