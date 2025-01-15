using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D43 RID: 7491
	internal class SuccessiveSubstringCount : RankingFeature
	{
		// Token: 0x0600FC77 RID: 64631 RVA: 0x0035E0E2 File Offset: 0x0035C2E2
		public SuccessiveSubstringCount(Grammar grammar)
			: base(grammar, "SuccessiveSubstringCount", 7.57724220529858, true)
		{
		}

		// Token: 0x0600FC78 RID: 64632 RVA: 0x0035E0FC File Offset: 0x0035C2FC
		[FeatureCalculator("Concat", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate(ProgramNode f, ProgramNode e)
		{
			return (double)((this._build.Node.IsRule.LetColumnName(f) && this._build.Node.Cast.e(e).Switch<bool>(this._build, (Atom atom) => atom.f.Is_LetColumnName(this._build), (Concat concat) => concat.f.Is_LetColumnName(this._build))) ? 1 : 0);
		}
	}
}
