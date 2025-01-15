using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200186E RID: 6254
	internal class PythonNotEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CC18 RID: 52248 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonNotEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x0600CC19 RID: 52249 RVA: 0x002B8EA0 File Offset: 0x002B70A0
		public override string Symbol
		{
			get
			{
				return "!=";
			}
		}

		// Token: 0x0600CC1A RID: 52250 RVA: 0x002B8EA7 File Offset: 0x002B70A7
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonNotEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
