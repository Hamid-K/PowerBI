using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D44 RID: 7492
	internal class StandardDelimiters : RankingFeature
	{
		// Token: 0x0600FC7B RID: 64635 RVA: 0x0035E1AA File Offset: 0x0035C3AA
		public StandardDelimiters(Grammar grammar)
			: base(grammar, "StandardDelimiters", 11.1546796607919, true)
		{
		}

		// Token: 0x0600FC7C RID: 64636 RVA: 0x0035E1C4 File Offset: 0x0035C3C4
		[FeatureCalculator("Concat", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate(ProgramNode f, ProgramNode e)
		{
			double num;
			if (!this._build.Node.IsRule.ConstStr(f))
			{
				e e2 = this._build.Node.Cast.e(e);
				Concat concat2;
				ConstStr constStr;
				if (e2.Is_Concat(this._build, out concat2) && concat2.f.Is_ConstStr(this._build, out constStr))
				{
					e2 = concat2.e;
					if (!e2.Switch<f>(this._build, (Atom atom) => atom.f, (Concat concat) => concat.f).Is_ConstStr(this._build))
					{
						num = (double)1;
						goto IL_00C3;
					}
				}
			}
			num = (double)0;
			IL_00C3:
			return num;
		}
	}
}
