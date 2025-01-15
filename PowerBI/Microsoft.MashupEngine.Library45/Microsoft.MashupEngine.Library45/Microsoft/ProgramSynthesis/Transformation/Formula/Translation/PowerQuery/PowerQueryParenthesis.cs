using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x0200189C RID: 6300
	internal class PowerQueryParenthesis : FormulaParenthesis
	{
		// Token: 0x0600CDE8 RID: 52712 RVA: 0x002B47BD File Offset: 0x002B29BD
		public PowerQueryParenthesis(FormulaExpression body)
			: base(body)
		{
		}

		// Token: 0x0600CDE9 RID: 52713 RVA: 0x002BF744 File Offset: 0x002BD944
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryParenthesis(base.Body.Accept<FormulaExpression>(visitor));
		}
	}
}
