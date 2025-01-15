using System;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D53 RID: 7507
	internal class DelimiterRegularExpressions : RegexPositionRankingFeature
	{
		// Token: 0x0600FCA2 RID: 64674 RVA: 0x0035E5A7 File Offset: 0x0035C7A7
		public DelimiterRegularExpressions(Grammar grammar)
			: base(grammar, "DelimiterRegularExpressions", -12.7556759347035)
		{
		}

		// Token: 0x0600FCA3 RID: 64675 RVA: 0x0035E5BE File Offset: 0x0035C7BE
		protected override double Calculate(RegularExpression ll, RegularExpression lr, int leftK, RegularExpression rl, RegularExpression rr, int rightK)
		{
			return (double)(((leftK == rightK - 1 || rightK == -1) && lr.Tokens.Length == 0 && rr.Tokens.Length == 0 && object.Equals(ll, rl)) ? 1 : 0);
		}
	}
}
