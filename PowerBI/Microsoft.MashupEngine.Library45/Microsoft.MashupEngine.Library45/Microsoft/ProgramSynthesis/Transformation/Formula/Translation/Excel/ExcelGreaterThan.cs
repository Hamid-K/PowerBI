using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001922 RID: 6434
	internal class ExcelGreaterThan : FormulaBinaryOperator
	{
		// Token: 0x0600D1F6 RID: 53750 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public ExcelGreaterThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022F7 RID: 8951
		// (get) Token: 0x0600D1F7 RID: 53751 RVA: 0x002B8F12 File Offset: 0x002B7112
		public override string Symbol
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x0600D1F8 RID: 53752 RVA: 0x002CC51F File Offset: 0x002CA71F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelGreaterThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
