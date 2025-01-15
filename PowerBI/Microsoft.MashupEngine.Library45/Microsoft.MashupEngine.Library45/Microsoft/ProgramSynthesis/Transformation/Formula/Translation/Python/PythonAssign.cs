using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200186B RID: 6251
	internal class PythonAssign : FormulaBinaryOperator
	{
		// Token: 0x0600CC0F RID: 52239 RVA: 0x002B8E3A File Offset: 0x002B703A
		public PythonAssign(FormulaExpression left, FormulaExpression right, bool compact)
			: base(left, right, 0, compact, false)
		{
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x0600CC10 RID: 52240 RVA: 0x002B8E47 File Offset: 0x002B7047
		public override string Symbol
		{
			get
			{
				return "=";
			}
		}

		// Token: 0x0600CC11 RID: 52241 RVA: 0x002B8E4E File Offset: 0x002B704E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonAssign(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor), base.Compact);
		}
	}
}
