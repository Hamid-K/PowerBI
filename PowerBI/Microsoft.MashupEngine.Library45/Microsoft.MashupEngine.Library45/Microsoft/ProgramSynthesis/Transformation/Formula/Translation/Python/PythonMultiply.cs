using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001862 RID: 6242
	internal class PythonMultiply : FormulaMultiply
	{
		// Token: 0x0600CBF2 RID: 52210 RVA: 0x002B4889 File Offset: 0x002B2A89
		public PythonMultiply(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600CBF3 RID: 52211 RVA: 0x002B8C14 File Offset: 0x002B6E14
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonMultiply(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
