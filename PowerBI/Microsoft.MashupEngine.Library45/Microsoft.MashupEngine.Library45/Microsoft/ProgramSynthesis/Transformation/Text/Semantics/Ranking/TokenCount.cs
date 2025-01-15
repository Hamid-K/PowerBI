using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D4E RID: 7502
	internal class TokenCount : RankingFeature
	{
		// Token: 0x0600FC92 RID: 64658 RVA: 0x0035E40C File Offset: 0x0035C60C
		public TokenCount(Grammar grammar)
			: base(grammar, "TokenCount", -17.5559661961156, true)
		{
		}

		// Token: 0x0600FC93 RID: 64659 RVA: 0x0035E424 File Offset: 0x0035C624
		[FeatureCalculator("r", Method = CalculationMethod.FromLiteral)]
		public static double Calculate(RegularExpression r)
		{
			return (double)r.Tokens.Length;
		}
	}
}
