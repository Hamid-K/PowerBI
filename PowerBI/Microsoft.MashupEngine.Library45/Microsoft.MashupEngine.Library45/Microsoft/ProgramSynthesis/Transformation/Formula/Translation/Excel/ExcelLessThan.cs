using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001924 RID: 6436
	internal class ExcelLessThan : FormulaBinaryOperator
	{
		// Token: 0x0600D1FC RID: 53756 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public ExcelLessThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022F9 RID: 8953
		// (get) Token: 0x0600D1FD RID: 53757 RVA: 0x002B8F5E File Offset: 0x002B715E
		public override string Symbol
		{
			get
			{
				return "<";
			}
		}

		// Token: 0x0600D1FE RID: 53758 RVA: 0x002CC55D File Offset: 0x002CA75D
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelLessThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
