using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D4B RID: 7499
	internal class RegularExpressionOffsets : RankingFeature
	{
		// Token: 0x0600FC8B RID: 64651 RVA: 0x0035E360 File Offset: 0x0035C560
		public RegularExpressionOffsets(Grammar grammar)
			: base(grammar, "RegularExpressionOffsets", -5.43167633378707, true)
		{
		}

		// Token: 0x0600FC8C RID: 64652 RVA: 0x0035E378 File Offset: 0x0035C578
		[FeatureCalculator("RegexPosition", Method = CalculationMethod.FromChildrenNodes)]
		[FeatureCalculator("RegexPositionRelative", Method = CalculationMethod.FromChildrenNodes)]
		public static double Calculate(ProgramNode x, ProgramNode regexPair, LiteralNode k)
		{
			int num = (int)k.Value;
			return (double)((num > 0) ? ((num - 1) * (num - 1)) : (num * num));
		}
	}
}
