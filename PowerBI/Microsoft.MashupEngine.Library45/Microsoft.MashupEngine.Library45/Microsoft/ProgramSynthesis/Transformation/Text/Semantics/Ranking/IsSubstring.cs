using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D58 RID: 7512
	internal class IsSubstring : BooleanRankingFeature
	{
		// Token: 0x0600FCAF RID: 64687 RVA: 0x0035E78F File Offset: 0x0035C98F
		public IsSubstring(Grammar grammar)
			: base(grammar, "IsSubstring", 12.484242131462, false)
		{
		}

		// Token: 0x0600FCB0 RID: 64688 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("SubStr", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate_SubStr(ProgramNode program)
		{
			return 1.0;
		}

		// Token: 0x0600FCB1 RID: 64689 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Concat", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate_Concat(ProgramNode program)
		{
			return 0.0;
		}
	}
}
