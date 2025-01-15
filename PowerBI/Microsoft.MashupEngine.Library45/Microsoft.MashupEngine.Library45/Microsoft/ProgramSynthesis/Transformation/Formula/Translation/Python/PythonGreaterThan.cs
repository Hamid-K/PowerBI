using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001871 RID: 6257
	internal class PythonGreaterThan : FormulaBinaryOperator
	{
		// Token: 0x0600CC21 RID: 52257 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonGreaterThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x0600CC22 RID: 52258 RVA: 0x002B8F12 File Offset: 0x002B7112
		public override string Symbol
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x0600CC23 RID: 52259 RVA: 0x002B8F19 File Offset: 0x002B7119
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonGreaterThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
