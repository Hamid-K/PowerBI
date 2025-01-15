using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D59 RID: 7513
	internal class IsConstant : BooleanRankingFeature
	{
		// Token: 0x0600FCB2 RID: 64690 RVA: 0x0035E7A7 File Offset: 0x0035C9A7
		public IsConstant(Grammar grammar)
			: base(grammar, "IsConstant", 0.0, false)
		{
		}

		// Token: 0x0600FCB3 RID: 64691 RVA: 0x00012DE5 File Offset: 0x00010FE5
		[FeatureCalculator("ConstStr", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate_ConstStr(ProgramNode program)
		{
			return 1.0;
		}

		// Token: 0x0600FCB4 RID: 64692 RVA: 0x0001AF59 File Offset: 0x00019159
		[FeatureCalculator("Concat", Method = CalculationMethod.FromProgramNode)]
		public static double Calculate_Concat(ProgramNode program)
		{
			return 0.0;
		}
	}
}
