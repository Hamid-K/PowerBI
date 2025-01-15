using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200186D RID: 6253
	internal class PythonEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CC15 RID: 52245 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x0600CC16 RID: 52246 RVA: 0x002B8E99 File Offset: 0x002B7099
		public override string Symbol
		{
			get
			{
				return "==";
			}
		}

		// Token: 0x0600CC17 RID: 52247 RVA: 0x002B8E7A File Offset: 0x002B707A
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
