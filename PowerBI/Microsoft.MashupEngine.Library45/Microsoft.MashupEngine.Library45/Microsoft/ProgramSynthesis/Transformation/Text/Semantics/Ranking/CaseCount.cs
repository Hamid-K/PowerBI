using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D3C RID: 7484
	internal class CaseCount : RankingFeature
	{
		// Token: 0x0600FC67 RID: 64615 RVA: 0x0035DFF5 File Offset: 0x0035C1F5
		public CaseCount(Grammar grammar)
			: base(grammar, "CaseCount", -19.8144012644136, true)
		{
		}

		// Token: 0x0600FC68 RID: 64616 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("ToLowercase", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ToUppercase", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ToSimpleTitleCase", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate(ProgramNode program)
		{
			return 1.0;
		}
	}
}
