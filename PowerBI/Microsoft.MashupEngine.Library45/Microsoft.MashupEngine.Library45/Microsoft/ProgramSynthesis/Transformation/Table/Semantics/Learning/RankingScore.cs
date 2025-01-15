using System;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B30 RID: 6960
	public class RankingScore : Feature<double>
	{
		// Token: 0x0600E4A4 RID: 58532 RVA: 0x000BFE5F File Offset: 0x000BE05F
		public RankingScore(Grammar grammar)
			: base(grammar, "Score", false, false, null, Feature<double>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x0600E4A5 RID: 58533 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("TTableProgram", Method = CalculationMethod.FromProgramNode, SupportsLearningInfo = true)]
		public double FullProgramScore(LearningInfo info)
		{
			return 0.0;
		}
	}
}
