using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D5C RID: 7516
	internal abstract class BooleanRankingFeature : RankingFeature
	{
		// Token: 0x0600FCC2 RID: 64706 RVA: 0x0035EA08 File Offset: 0x0035CC08
		protected BooleanRankingFeature(Grammar grammar, string name, double learnedCoefficient = 0.0, bool accumulateDefinitions = true)
			: base(grammar, name, learnedCoefficient, accumulateDefinitions)
		{
		}

		// Token: 0x0600FCC3 RID: 64707 RVA: 0x0035EA15 File Offset: 0x0035CC15
		protected override double Accumulate(double x, double y)
		{
			return (double)((x > 0.0 || y > 0.0) ? 1 : 0);
		}
	}
}
