using System;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001222 RID: 4642
	public static class Semantics
	{
		// Token: 0x06008BC6 RID: 35782 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public static bool NoMatch()
		{
			return false;
		}

		// Token: 0x06008BC7 RID: 35783 RVA: 0x00010FAF File Offset: 0x0000F1AF
		public static bool Disjunction(bool match, bool disjunctiveMatch)
		{
			return match || disjunctiveMatch;
		}

		// Token: 0x06008BC8 RID: 35784 RVA: 0x001D4710 File Offset: 0x001D2910
		[LazySemantics]
		public static bool IsNull(SuffixRegion sRegion)
		{
			return sRegion.Source == null;
		}

		// Token: 0x06008BC9 RID: 35785 RVA: 0x001D471B File Offset: 0x001D291B
		[LazySemantics]
		public static bool EndOf(SuffixRegion sRegion)
		{
			return sRegion != null && sRegion.Source != null && sRegion.Length == 0;
		}

		// Token: 0x06008BCA RID: 35786 RVA: 0x001D4733 File Offset: 0x001D2933
		public static SuffixRegion SuffixAfterTokenMatch(SuffixRegion sRegion, IToken token)
		{
			if (sRegion == null)
			{
				return null;
			}
			return sRegion.UnmatchedSuffix(token);
		}
	}
}
