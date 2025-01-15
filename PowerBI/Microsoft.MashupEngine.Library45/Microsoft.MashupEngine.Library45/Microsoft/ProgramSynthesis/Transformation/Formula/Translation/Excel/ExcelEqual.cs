using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200192D RID: 6445
	internal class ExcelEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D21C RID: 53788 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public ExcelEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x0600D21D RID: 53789 RVA: 0x002B8E47 File Offset: 0x002B7047
		public override string Symbol
		{
			get
			{
				return "=";
			}
		}

		// Token: 0x0600D21E RID: 53790 RVA: 0x002CC771 File Offset: 0x002CA971
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
