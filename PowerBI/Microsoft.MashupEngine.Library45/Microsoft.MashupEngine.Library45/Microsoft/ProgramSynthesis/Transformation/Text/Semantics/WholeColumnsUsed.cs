using System;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CCD RID: 7373
	public class WholeColumnsUsed : Feature<IImmutableSet<string>>
	{
		// Token: 0x0600FA1B RID: 64027 RVA: 0x00353137 File Offset: 0x00351337
		public WholeColumnsUsed(Grammar grammar, WholeColumnUsed wholeColumnUsed, InputsUsed inputsUsed)
			: base(grammar, "WholeColumnsUsed", false, false, null, Feature<IImmutableSet<string>>.FeatureInfoResolution.Declared)
		{
			this._wholeColumnUsed = wholeColumnUsed;
			this._inputsUsed = inputsUsed;
			this._build = GrammarBuilders.Instance(base.Grammar);
		}

		// Token: 0x0600FA1C RID: 64028 RVA: 0x00353168 File Offset: 0x00351368
		[FeatureCalculator("IfThenElse")]
		public static IImmutableSet<string> WholeColumnsUsed_IfThenElse(IImmutableSet<string> b, IImmutableSet<string> st, IImmutableSet<string> sw)
		{
			return st.Union(sw);
		}

		// Token: 0x0600FA1D RID: 64029 RVA: 0x0034C0A2 File Offset: 0x0034A2A2
		[FeatureCalculator("LetPredicate", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ConstStr", Method = CalculationMethod.FromProgramNode)]
		public IImmutableSet<string> LetPredicateWholeColumnsUsed(ProgramNode program)
		{
			return ImmutableHashSet<string>.Empty;
		}

		// Token: 0x0600FA1E RID: 64030 RVA: 0x0034C099 File Offset: 0x0034A299
		[FeatureCalculator("Concat")]
		public static IImmutableSet<string> WholeColumnsUsed_Concat(IImmutableSet<string> f, IImmutableSet<string> e)
		{
			return f.Union(e);
		}

		// Token: 0x0600FA1F RID: 64031 RVA: 0x00353174 File Offset: 0x00351374
		[FeatureCalculator("LetColumnName", Method = CalculationMethod.FromChildrenNodes)]
		public IImmutableSet<string> ColumnNameWholeColumnsUsed(ProgramNode idx, ProgramNode letOptionsNode)
		{
			if (!this._build.Node.Cast.letOptions(letOptionsNode).Switch<ProgramNode>(this._build, (LetCell letCell) => letCell.conv.Node, (LetX letX) => letX.conv.Node).GetFeatureValue<bool>(this._wholeColumnUsed, null))
			{
				return ImmutableHashSet<string>.Empty;
			}
			return idx.GetFeatureValue<IImmutableSet<string>>(this._inputsUsed, null);
		}

		// Token: 0x04005CA6 RID: 23718
		private readonly WholeColumnUsed _wholeColumnUsed;

		// Token: 0x04005CA7 RID: 23719
		private readonly InputsUsed _inputsUsed;

		// Token: 0x04005CA8 RID: 23720
		private readonly GrammarBuilders _build;
	}
}
