using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Ranking
{
	// Token: 0x02001D57 RID: 7511
	internal class PrefixOrSuffix : BooleanRankingFeature
	{
		// Token: 0x0600FCAA RID: 64682 RVA: 0x0035E65E File Offset: 0x0035C85E
		public PrefixOrSuffix(Grammar grammar)
			: base(grammar, "PrefixOrSuffix", 20.0352740175828, true)
		{
		}

		// Token: 0x0600FCAB RID: 64683 RVA: 0x0035E678 File Offset: 0x0035C878
		[FeatureCalculator("PosPair", Method = CalculationMethod.FromChildrenNodes)]
		public double Calculate(ProgramNode l, ProgramNode r)
		{
			return (double)((this.IsPrefixPosition(this._build.Node.Cast.pos(l)) || this.IsSuffixPosition(this._build.Node.Cast.pos(r))) ? 1 : 0);
		}

		// Token: 0x0600FCAC RID: 64684 RVA: 0x0035E6C6 File Offset: 0x0035C8C6
		private bool IsPrefixPosition(pos child)
		{
			return this.IsPosition(child, 0, 1);
		}

		// Token: 0x0600FCAD RID: 64685 RVA: 0x0035E6D1 File Offset: 0x0035C8D1
		private bool IsSuffixPosition(pos child)
		{
			return this.IsPosition(child, -1, -1);
		}

		// Token: 0x0600FCAE RID: 64686 RVA: 0x0035E6DC File Offset: 0x0035C8DC
		private bool IsPosition(pos child, int relativePositionIndex, int regexRelativePositionIndex)
		{
			RelativePosition relativePosition;
			if (child.Is_RelativePosition(this._build, out relativePosition))
			{
				return relativePosition.k.Value == relativePositionIndex;
			}
			RegexPositionRelative regexPositionRelative = child.Cast_RegexPositionRelative(this._build);
			if (regexPositionRelative.k.Value != regexRelativePositionIndex)
			{
				return false;
			}
			RegexPair regexPair = regexPositionRelative.regexPair.Cast_RegexPair();
			if (regexPair.r1.Value.Count > 0)
			{
				return false;
			}
			RegularExpression value = regexPair.r2.Value;
			return value.Count == 1 && value.Tokens[0].Name == "Line Separator";
		}
	}
}
