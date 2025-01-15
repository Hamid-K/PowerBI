using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D3D RID: 7485
	internal class WholeColumnCount : RankingFeature
	{
		// Token: 0x0600FC69 RID: 64617 RVA: 0x0035E00D File Offset: 0x0035C20D
		public WholeColumnCount(Grammar grammar)
			: base(grammar, "WholeColumnCount", 22.0810140157715, true)
		{
		}

		// Token: 0x0600FC6A RID: 64618 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("WholeColumn", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
