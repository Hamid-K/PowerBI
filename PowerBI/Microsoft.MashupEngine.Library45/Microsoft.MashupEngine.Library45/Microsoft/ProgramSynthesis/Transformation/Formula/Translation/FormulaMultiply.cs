using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F4 RID: 6132
	internal abstract class FormulaMultiply : FormulaBinaryOperator
	{
		// Token: 0x0600C9D5 RID: 51669 RVA: 0x002B3136 File Offset: 0x002B1336
		protected FormulaMultiply(FormulaExpression left, FormulaExpression right, int precedence = 6)
			: base(left, right, precedence, false, true)
		{
		}

		// Token: 0x17002205 RID: 8709
		// (get) Token: 0x0600C9D6 RID: 51670 RVA: 0x0001B368 File Offset: 0x00019568
		public override string Symbol
		{
			get
			{
				return "*";
			}
		}
	}
}
