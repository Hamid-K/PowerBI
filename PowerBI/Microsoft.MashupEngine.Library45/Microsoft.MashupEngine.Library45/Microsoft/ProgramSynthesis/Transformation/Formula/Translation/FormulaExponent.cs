using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F7 RID: 6135
	internal abstract class FormulaExponent : FormulaBinaryOperator
	{
		// Token: 0x0600C9DD RID: 51677 RVA: 0x002B3122 File Offset: 0x002B1322
		protected FormulaExponent(FormulaExpression left, FormulaExpression right, int precedence = 2)
			: base(left, right, precedence, false, false)
		{
		}
	}
}
