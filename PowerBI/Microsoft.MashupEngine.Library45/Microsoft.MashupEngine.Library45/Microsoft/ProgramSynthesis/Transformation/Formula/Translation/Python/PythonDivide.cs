using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001863 RID: 6243
	internal class PythonDivide : FormulaDivide
	{
		// Token: 0x0600CBF4 RID: 52212 RVA: 0x002B48B3 File Offset: 0x002B2AB3
		public PythonDivide(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600CBF5 RID: 52213 RVA: 0x002B8C33 File Offset: 0x002B6E33
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonDivide(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
