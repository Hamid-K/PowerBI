using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CCF RID: 7375
	public class WholeColumnUsed : Feature<bool>
	{
		// Token: 0x0600FA24 RID: 64036 RVA: 0x0035324C File Offset: 0x0035144C
		public WholeColumnUsed(Grammar grammar)
			: base(grammar, "WholeColumnUsed", false, false, null, Feature<bool>.FeatureInfoResolution.Declared)
		{
			this._build = GrammarBuilders.Instance(base.Grammar);
		}

		// Token: 0x0600FA25 RID: 64037 RVA: 0x00353270 File Offset: 0x00351470
		[FeatureCalculator("LetColumnName", Method = CalculationMethod.FromChildrenNodes)]
		public bool ColumnName(ProgramNode _, ProgramNode letOptions)
		{
			return this._build.Node.Cast.letOptions(letOptions).Switch<ProgramNode>(this._build, (LetCell letCell) => letCell.conv.Node, (LetX letX) => letX.conv.Node).GetFeatureValue<bool>(this, null);
		}

		// Token: 0x0600FA26 RID: 64038 RVA: 0x0000A5FD File Offset: 0x000087FD
		[FeatureCalculator("WholeColumn", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("Lookup", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("AsDecimal", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("AsPartialDateTime", Method = CalculationMethod.FromProgramNode)]
		public bool WholeColumn(ProgramNode x)
		{
			return true;
		}

		// Token: 0x0600FA27 RID: 64039 RVA: 0x0000FA11 File Offset: 0x0000DC11
		[FeatureCalculator("SubStr", Method = CalculationMethod.FromProgramNode)]
		public bool NotWholeColumn(ProgramNode x)
		{
			return false;
		}

		// Token: 0x0600FA28 RID: 64040 RVA: 0x003532E6 File Offset: 0x003514E6
		[FeatureCalculator("ToLowercase", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ToUppercase", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ToSimpleTitleCase", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("FormatPartialDateTime", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("FormatNumber", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("FormatNumericRange", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("FormatDateTimeRange", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("LetSharedParsedDateTime", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("LetSharedParsedNumber", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ParsePartialDateTime", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("ParseNumber", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("RoundPartialDateTime", Method = CalculationMethod.FromProgramNode)]
		[FeatureCalculator("RoundNumber", Method = CalculationMethod.FromProgramNode)]
		public bool Child0WholeColumnsUsed(ProgramNode p)
		{
			return p.Children[0].GetFeatureValue<bool>(this, null);
		}

		// Token: 0x04005CAC RID: 23724
		private readonly GrammarBuilders _build;
	}
}
