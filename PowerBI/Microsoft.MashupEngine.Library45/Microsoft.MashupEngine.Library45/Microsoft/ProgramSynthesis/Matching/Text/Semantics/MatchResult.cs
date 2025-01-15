using System;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Matching.Text.Semantics
{
	// Token: 0x02001233 RID: 4659
	internal struct MatchResult
	{
		// Token: 0x06008C5D RID: 35933 RVA: 0x001D6E2C File Offset: 0x001D502C
		private MatchResult(IToken leftRemaining, IToken rightRemaining, Optional<bool> isDisjoint)
		{
			this.LeftRemaining = leftRemaining;
			this.RightRemaining = rightRemaining;
			this.IsDisjoint = isDisjoint;
		}

		// Token: 0x06008C5E RID: 35934 RVA: 0x001D6E43 File Offset: 0x001D5043
		internal static MatchResult MatchedWithLeftRemaining(IToken leftRemaining)
		{
			return new MatchResult(leftRemaining, null, false.Some<bool>());
		}

		// Token: 0x06008C5F RID: 35935 RVA: 0x001D6E52 File Offset: 0x001D5052
		internal static MatchResult MatchedWithNoneRemaining()
		{
			return new MatchResult(null, null, false.Some<bool>());
		}

		// Token: 0x06008C60 RID: 35936 RVA: 0x001D6E61 File Offset: 0x001D5061
		internal static MatchResult MatchedWithRightRemaining(IToken rightRemaining)
		{
			return new MatchResult(null, rightRemaining, false.Some<bool>());
		}

		// Token: 0x06008C61 RID: 35937 RVA: 0x001D6E70 File Offset: 0x001D5070
		internal static MatchResult Unknown(IToken leftRemaining, IToken rightRemaining)
		{
			return new MatchResult(leftRemaining, rightRemaining, Optional<bool>.Nothing);
		}

		// Token: 0x06008C62 RID: 35938 RVA: 0x001D6E7E File Offset: 0x001D507E
		internal static MatchResult DidNotMatch()
		{
			return new MatchResult(null, null, true.Some<bool>());
		}

		// Token: 0x06008C63 RID: 35939 RVA: 0x001D6E8D File Offset: 0x001D508D
		internal MatchResult Invert()
		{
			return new MatchResult(this.RightRemaining, this.LeftRemaining, this.IsDisjoint);
		}

		// Token: 0x04003966 RID: 14694
		internal readonly IToken LeftRemaining;

		// Token: 0x04003967 RID: 14695
		internal readonly IToken RightRemaining;

		// Token: 0x04003968 RID: 14696
		internal readonly Optional<bool> IsDisjoint;
	}
}
