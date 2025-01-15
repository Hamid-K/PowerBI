using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200186C RID: 6252
	internal class PythonIs : FormulaBinaryOperator
	{
		// Token: 0x0600CC12 RID: 52242 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonIs(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x0600CC13 RID: 52243 RVA: 0x002B8E73 File Offset: 0x002B7073
		public override string Symbol
		{
			get
			{
				return "is";
			}
		}

		// Token: 0x0600CC14 RID: 52244 RVA: 0x002B8E7A File Offset: 0x002B707A
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
