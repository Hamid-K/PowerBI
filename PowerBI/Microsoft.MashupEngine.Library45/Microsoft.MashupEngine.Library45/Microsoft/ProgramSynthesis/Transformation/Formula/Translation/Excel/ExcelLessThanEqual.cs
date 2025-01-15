using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001925 RID: 6437
	internal class ExcelLessThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600D1FF RID: 53759 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public ExcelLessThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022FA RID: 8954
		// (get) Token: 0x0600D200 RID: 53760 RVA: 0x002B8F84 File Offset: 0x002B7184
		public override string Symbol
		{
			get
			{
				return "<=";
			}
		}

		// Token: 0x0600D201 RID: 53761 RVA: 0x002CC57C File Offset: 0x002CA77C
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelLessThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
