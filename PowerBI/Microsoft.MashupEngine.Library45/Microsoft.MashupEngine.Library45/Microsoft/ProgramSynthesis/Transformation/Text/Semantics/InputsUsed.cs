using System;
using System.Collections.Immutable;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001C9D RID: 7325
	public class InputsUsed : Feature<IImmutableSet<string>>
	{
		// Token: 0x0600F7A5 RID: 63397 RVA: 0x0034C050 File Offset: 0x0034A250
		public InputsUsed(Grammar grammar)
			: base(grammar, "InputsUsed", false, false, null, Feature<IImmutableSet<string>>.FeatureInfoResolution.Declared)
		{
		}

		// Token: 0x0600F7A6 RID: 63398 RVA: 0x0034C062 File Offset: 0x0034A262
		[FeatureCalculator("IfThenElse")]
		public static IImmutableSet<string> InputsUsed_IfThenElse(IImmutableSet<string> b, IImmutableSet<string> st, IImmutableSet<string> sw)
		{
			return b.Union(st).Union(sw);
		}

		// Token: 0x0600F7A7 RID: 63399 RVA: 0x0034C071 File Offset: 0x0034A271
		[FeatureCalculator("LetPredicate", Method = CalculationMethod.FromChildrenNodes)]
		public IImmutableSet<string> LetPredicateInputsUsed(ProgramNode chooseInput, ProgramNode pred)
		{
			return chooseInput.GetFeatureValue<IImmutableSet<string>>(this, null);
		}

		// Token: 0x0600F7A8 RID: 63400 RVA: 0x0034C07B File Offset: 0x0034A27B
		[FeatureCalculator("SelectInput", Method = CalculationMethod.FromChildrenNodes)]
		public IImmutableSet<string> InputsUsed_ChooseInput(ProgramNode vs, ProgramNode name)
		{
			return name.GetFeatureValue<IImmutableSet<string>>(this, null);
		}

		// Token: 0x0600F7A9 RID: 63401 RVA: 0x0034C085 File Offset: 0x0034A285
		[FeatureCalculator("IndexInputString", Method = CalculationMethod.FromProgramNode)]
		public IImmutableSet<string> InputsUsed_IndexInput(ProgramNode indexInput)
		{
			throw new InvalidOperationException("Cannot compute InputsUsed on IndexInput program.");
		}

		// Token: 0x0600F7AA RID: 63402 RVA: 0x0034C091 File Offset: 0x0034A291
		[FeatureCalculator("name", Method = CalculationMethod.FromLiteral)]
		public static IImmutableSet<string> NameInputsUsed(string name)
		{
			return ImmutableHashSet.Create<string>(name);
		}

		// Token: 0x0600F7AB RID: 63403 RVA: 0x0034C099 File Offset: 0x0034A299
		[FeatureCalculator("Concat")]
		public static IImmutableSet<string> InputsUsed_Concat(IImmutableSet<string> f, IImmutableSet<string> e)
		{
			return f.Union(e);
		}

		// Token: 0x0600F7AC RID: 63404 RVA: 0x0034C0A2 File Offset: 0x0034A2A2
		[FeatureCalculator("ConstStr", Method = CalculationMethod.FromChildrenNodes)]
		public static IImmutableSet<string> InputsUsed_ConstStr(ProgramNode p)
		{
			return ImmutableHashSet<string>.Empty;
		}

		// Token: 0x0600F7AD RID: 63405 RVA: 0x0034C071 File Offset: 0x0034A271
		[FeatureCalculator("LetColumnName", Method = CalculationMethod.FromChildrenNodes)]
		public IImmutableSet<string> ColumnNameInputsUsed(ProgramNode idx, ProgramNode letX)
		{
			return idx.GetFeatureValue<IImmutableSet<string>>(this, null);
		}

		// Token: 0x0600F7AE RID: 63406 RVA: 0x0034C091 File Offset: 0x0034A291
		[FeatureCalculator("idx", Method = CalculationMethod.FromLiteral)]
		public static IImmutableSet<string> IdxInputsUsed(string idx)
		{
			return ImmutableHashSet.Create<string>(idx);
		}
	}
}
