using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001923 RID: 6435
	internal class ExcelGreaterThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D1F9 RID: 53753 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public ExcelGreaterThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022F8 RID: 8952
		// (get) Token: 0x0600D1FA RID: 53754 RVA: 0x002B8F38 File Offset: 0x002B7138
		public override string Symbol
		{
			get
			{
				return ">=";
			}
		}

		// Token: 0x0600D1FB RID: 53755 RVA: 0x002CC53E File Offset: 0x002CA73E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelGreaterThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
