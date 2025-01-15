using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D52 RID: 7506
	internal abstract class RegexPositionRankingFeature : RankingFeature
	{
		// Token: 0x0600FC9F RID: 64671 RVA: 0x0035E03D File Offset: 0x0035C23D
		protected RegexPositionRankingFeature(Grammar grammar, string name, double learnedCoefficient = 0.0)
			: base(grammar, name, learnedCoefficient, true)
		{
		}

		// Token: 0x0600FCA0 RID: 64672 RVA: 0x0035E4D4 File Offset: 0x0035C6D4
		[FeatureCalculator("PosPair", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate(ProgramNode left, ProgramNode right)
		{
			RegexPositionRelative regexPositionRelative;
			RegexPositionRelative regexPositionRelative2;
			if (!this._build.Node.IsRule.RegexPositionRelative(left, out regexPositionRelative) || !this._build.Node.IsRule.RegexPositionRelative(right, out regexPositionRelative2))
			{
				return 0.0;
			}
			RegexPair regexPair = regexPositionRelative.regexPair.Cast_RegexPair();
			RegexPair regexPair2 = regexPositionRelative2.regexPair.Cast_RegexPair();
			return this.Calculate(regexPair.r1.Value, regexPair.r2.Value, regexPositionRelative.k.Value, regexPair2.r1.Value, regexPair2.r2.Value, regexPositionRelative2.k.Value);
		}

		// Token: 0x0600FCA1 RID: 64673
		protected abstract double Calculate(RegularExpression ll, RegularExpression lr, int leftK, RegularExpression rl, RegularExpression rr, int rightK);
	}
}
