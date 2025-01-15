using System;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D54 RID: 7508
	internal class ExtractionRegularExpressions : RegexPositionRankingFeature
	{
		// Token: 0x0600FCA4 RID: 64676 RVA: 0x0035E5EE File Offset: 0x0035C7EE
		public ExtractionRegularExpressions(Grammar grammar)
			: base(grammar, "ExtractionRegularExpressions", -6.61292140806713)
		{
		}

		// Token: 0x0600FCA5 RID: 64677 RVA: 0x0035E605 File Offset: 0x0035C805
		protected override double Calculate(RegularExpression ll, RegularExpression lr, int leftK, RegularExpression rl, RegularExpression rr, int rightK)
		{
			return (double)((leftK == rightK && ll.Tokens.Length == 0 && rr.Tokens.Length == 0 && lr.Equals(rl)) ? 1 : 0);
		}
	}
}
