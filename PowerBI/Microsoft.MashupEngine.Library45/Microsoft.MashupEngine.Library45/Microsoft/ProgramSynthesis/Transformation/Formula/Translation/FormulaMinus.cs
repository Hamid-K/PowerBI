using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017F2 RID: 6130
	internal abstract class FormulaMinus : FormulaBinaryOperator
	{
		// Token: 0x0600C9D1 RID: 51665 RVA: 0x002B3122 File Offset: 0x002B1322
		protected FormulaMinus(FormulaExpression left, FormulaExpression right, int precedence = 4)
			: base(left, right, precedence, false, false)
		{
		}

		// Token: 0x17002203 RID: 8707
		// (get) Token: 0x0600C9D2 RID: 51666 RVA: 0x002B312F File Offset: 0x002B132F
		public override string Symbol
		{
			get
			{
				return "-";
			}
		}
	}
}
