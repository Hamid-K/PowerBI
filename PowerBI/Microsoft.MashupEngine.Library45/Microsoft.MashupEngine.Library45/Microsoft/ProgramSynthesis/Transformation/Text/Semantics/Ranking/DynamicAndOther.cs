using System;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D4C RID: 7500
	internal class DynamicAndOther : RankingFeature
	{
		// Token: 0x0600FC8D RID: 64653 RVA: 0x0035E3A2 File Offset: 0x0035C5A2
		public DynamicAndOther(Grammar grammar)
			: base(grammar, "DynamicAndOther", -46.872453929388, true)
		{
		}

		// Token: 0x0600FC8E RID: 64654 RVA: 0x0035E3BA File Offset: 0x0035C5BA
		[FeatureCalculator("r", Method = CalculationMethod.FromLiteral)]
		public static double Calculate(RegularExpression r)
		{
			return (double)((r.Tokens.Any((Token t) => t.IsDynamicToken) && r.Tokens.Length > 1) ? 1 : 0);
		}
	}
}
