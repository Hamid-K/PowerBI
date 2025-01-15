using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F3 RID: 6131
	internal abstract class FormulaPlus : FormulaBinaryOperator
	{
		// Token: 0x0600C9D3 RID: 51667 RVA: 0x002B3136 File Offset: 0x002B1336
		protected FormulaPlus(FormulaExpression left, FormulaExpression right, int precedence = 4)
			: base(left, right, precedence, false, true)
		{
		}

		// Token: 0x17002204 RID: 8708
		// (get) Token: 0x0600C9D4 RID: 51668 RVA: 0x002B3143 File Offset: 0x002B1343
		public override string Symbol
		{
			get
			{
				return "+";
			}
		}
	}
}
