using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D4F RID: 7503
	internal class RegexCount : RankingFeature
	{
		// Token: 0x0600FC94 RID: 64660 RVA: 0x0035E42F File Offset: 0x0035C62F
		public RegexCount(Grammar grammar)
			: base(grammar, "RegexCount", 1.9581727523013, true)
		{
		}

		// Token: 0x0600FC95 RID: 64661 RVA: 0x0035E447 File Offset: 0x0035C647
		[FeatureCalculator("r", Method = CalculationMethod.FromLiteral)]
		public static double Calculate(RegularExpression r)
		{
			return (double)r.Score / 100.0;
		}
	}
}
