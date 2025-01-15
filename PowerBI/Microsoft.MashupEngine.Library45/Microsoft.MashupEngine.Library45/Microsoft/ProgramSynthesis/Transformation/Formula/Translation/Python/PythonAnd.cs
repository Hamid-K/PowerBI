using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200186F RID: 6255
	internal class PythonAnd : FormulaBinaryOperator
	{
		// Token: 0x0600CC1B RID: 52251 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonAnd(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x0600CC1C RID: 52252 RVA: 0x002B8EC6 File Offset: 0x002B70C6
		public override string Symbol
		{
			get
			{
				return "and";
			}
		}

		// Token: 0x0600CC1D RID: 52253 RVA: 0x002B8ECD File Offset: 0x002B70CD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonAnd(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
