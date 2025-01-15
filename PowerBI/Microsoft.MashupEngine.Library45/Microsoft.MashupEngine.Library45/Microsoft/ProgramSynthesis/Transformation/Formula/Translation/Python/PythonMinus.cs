using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001860 RID: 6240
	internal class PythonMinus : FormulaMinus
	{
		// Token: 0x0600CBEE RID: 52206 RVA: 0x002B4835 File Offset: 0x002B2A35
		public PythonMinus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600CBEF RID: 52207 RVA: 0x002B8BD6 File Offset: 0x002B6DD6
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonMinus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
