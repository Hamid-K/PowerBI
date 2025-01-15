using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200191E RID: 6430
	internal class ExcelMinus : FormulaMinus
	{
		// Token: 0x0600D1EE RID: 53742 RVA: 0x002B4835 File Offset: 0x002B2A35
		public ExcelMinus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600D1EF RID: 53743 RVA: 0x002CC4A3 File Offset: 0x002CA6A3
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelMinus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
