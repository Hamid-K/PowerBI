using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D50 RID: 7504
	internal class NegativeOffsets : RankingFeature
	{
		// Token: 0x0600FC96 RID: 64662 RVA: 0x0035E45A File Offset: 0x0035C65A
		public NegativeOffsets(Grammar grammar)
			: base(grammar, "NegativeOffsets", -4.14222760861917, true)
		{
		}

		// Token: 0x0600FC97 RID: 64663 RVA: 0x0035E472 File Offset: 0x0035C672
		[FeatureCalculator("RegexPosition", Method = CalculationMethod.FromChildrenNodes)]
		[FeatureCalculator("RegexPositionRelative", Method = CalculationMethod.FromChildrenNodes)]
		public static double Calculate_RegexPosition(ProgramNode x, ProgramNode regexPair, LiteralNode k)
		{
			return (int)k.Value < 0;
		}

		// Token: 0x0600FC98 RID: 64664 RVA: 0x0035E483 File Offset: 0x0035C683
		[FeatureCalculator("AbsolutePosition", Method = CalculationMethod.FromChildrenNodes)]
		public static double Calculate_AbsolutePosition(ProgramNode x, LiteralNode k)
		{
			return (int)k.Value < 0;
		}
	}
}
