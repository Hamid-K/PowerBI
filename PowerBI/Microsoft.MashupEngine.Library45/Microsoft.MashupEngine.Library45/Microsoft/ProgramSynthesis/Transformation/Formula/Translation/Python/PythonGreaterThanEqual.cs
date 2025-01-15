using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001872 RID: 6258
	internal class PythonGreaterThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CC24 RID: 52260 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonGreaterThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x0600CC25 RID: 52261 RVA: 0x002B8F38 File Offset: 0x002B7138
		public override string Symbol
		{
			get
			{
				return ">=";
			}
		}

		// Token: 0x0600CC26 RID: 52262 RVA: 0x002B8F3F File Offset: 0x002B713F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonGreaterThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
