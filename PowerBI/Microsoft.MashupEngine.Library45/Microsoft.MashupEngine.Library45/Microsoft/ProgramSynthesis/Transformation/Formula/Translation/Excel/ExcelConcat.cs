using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001926 RID: 6438
	internal class ExcelConcat : FormulaConcat
	{
		// Token: 0x0600D202 RID: 53762 RVA: 0x002B48DD File Offset: 0x002B2ADD
		public ExcelConcat(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x170022FB RID: 8955
		// (get) Token: 0x0600D203 RID: 53763 RVA: 0x002BF95D File Offset: 0x002BDB5D
		public override string Symbol
		{
			get
			{
				return "&";
			}
		}

		// Token: 0x0600D204 RID: 53764 RVA: 0x002CC59B File Offset: 0x002CA79B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new ExcelConcat(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
