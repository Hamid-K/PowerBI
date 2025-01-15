using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001921 RID: 6433
	internal class ExcelDivide : FormulaDivide
	{
		// Token: 0x0600D1F4 RID: 53748 RVA: 0x002B48B3 File Offset: 0x002B2AB3
		public ExcelDivide(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600D1F5 RID: 53749 RVA: 0x002CC500 File Offset: 0x002CA700
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelDivide(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
