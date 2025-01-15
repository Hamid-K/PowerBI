using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001865 RID: 6245
	internal class PythonModulo : FormulaBinaryOperator
	{
		// Token: 0x0600CBF9 RID: 52217 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonModulo(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x0600CBFA RID: 52218 RVA: 0x002B8C85 File Offset: 0x002B6E85
		public override string Symbol
		{
			get
			{
				return "%";
			}
		}

		// Token: 0x0600CBFB RID: 52219 RVA: 0x002B8C8C File Offset: 0x002B6E8C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonModulo(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
