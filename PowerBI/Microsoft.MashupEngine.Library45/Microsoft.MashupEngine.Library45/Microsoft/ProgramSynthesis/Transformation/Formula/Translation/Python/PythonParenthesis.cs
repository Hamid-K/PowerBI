using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001854 RID: 6228
	internal class PythonParenthesis : FormulaParenthesis
	{
		// Token: 0x0600CBC4 RID: 52164 RVA: 0x002B47BD File Offset: 0x002B29BD
		public PythonParenthesis(FormulaExpression body)
			: base(body)
		{
		}

		// Token: 0x0600CBC5 RID: 52165 RVA: 0x002B88B8 File Offset: 0x002B6AB8
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonParenthesis(base.Body.Accept<FormulaExpression>(visitor));
		}
	}
}
