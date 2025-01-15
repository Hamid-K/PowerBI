using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017EC RID: 6124
	internal abstract class FormulaRegexLiteral : FormulaExpression, IFormulaLiteral<string>
	{
		// Token: 0x0600C9B5 RID: 51637 RVA: 0x002B2B71 File Offset: 0x002B0D71
		protected FormulaRegexLiteral(string value)
		{
			this.Value = value;
			base.Children = new FormulaExpression[0];
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x0600C9B6 RID: 51638 RVA: 0x002B2B8C File Offset: 0x002B0D8C
		public string Value { get; }
	}
}
