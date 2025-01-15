using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001870 RID: 6256
	internal class PythonOr : FormulaBinaryOperator
	{
		// Token: 0x0600CC1E RID: 52254 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PythonOr(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x0600CC1F RID: 52255 RVA: 0x002B8EEC File Offset: 0x002B70EC
		public override string Symbol
		{
			get
			{
				return "or";
			}
		}

		// Token: 0x0600CC20 RID: 52256 RVA: 0x002B8EF3 File Offset: 0x002B70F3
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PythonOr(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
