using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000F3 RID: 243
	[Serializable]
	public class TokenSequenceComparer : IEqualityComparer<TokenSequence>
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0002CC65 File Offset: 0x0002AE65
		public bool Equals(TokenSequence x, TokenSequence y)
		{
			return x.Equals(y);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0002CC6F File Offset: 0x0002AE6F
		public int GetHashCode(TokenSequence x)
		{
			return x.GetHashCode();
		}

		// Token: 0x040003BE RID: 958
		public static readonly TokenSequenceComparer Instance = new TokenSequenceComparer();
	}
}
