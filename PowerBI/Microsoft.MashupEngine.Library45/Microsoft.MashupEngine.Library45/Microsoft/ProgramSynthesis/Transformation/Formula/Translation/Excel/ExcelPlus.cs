using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200191F RID: 6431
	internal class ExcelPlus : FormulaPlus
	{
		// Token: 0x0600D1F0 RID: 53744 RVA: 0x002B485F File Offset: 0x002B2A5F
		public ExcelPlus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600D1F1 RID: 53745 RVA: 0x002CC4C2 File Offset: 0x002CA6C2
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelPlus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
