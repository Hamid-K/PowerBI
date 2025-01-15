using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001920 RID: 6432
	internal class ExcelMultiply : FormulaMultiply
	{
		// Token: 0x0600D1F2 RID: 53746 RVA: 0x002B4889 File Offset: 0x002B2A89
		public ExcelMultiply(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600D1F3 RID: 53747 RVA: 0x002CC4E1 File Offset: 0x002CA6E1
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelMultiply(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
