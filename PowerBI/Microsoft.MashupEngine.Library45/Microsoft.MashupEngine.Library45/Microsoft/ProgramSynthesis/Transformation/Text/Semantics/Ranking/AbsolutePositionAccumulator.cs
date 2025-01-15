using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D4A RID: 7498
	internal class AbsolutePositionAccumulator : RankingFeature
	{
		// Token: 0x0600FC89 RID: 64649 RVA: 0x0035E335 File Offset: 0x0035C535
		public AbsolutePositionAccumulator(Grammar grammar)
			: base(grammar, "AbsolutePositionAccumulator", -12.9362834895906, true)
		{
		}

		// Token: 0x0600FC8A RID: 64650 RVA: 0x0035E34D File Offset: 0x0035C54D
		[FeatureCalculator("AbsolutePosition", Method = CalculationMethod.FromChildrenNodes)]
		[FeatureCalculator("RelativePosition", Method = CalculationMethod.FromChildrenNodes)]
		public static double Calculate(ProgramNode x, LiteralNode k)
		{
			return (double)Math.Abs((int)k.Value);
		}
	}
}
