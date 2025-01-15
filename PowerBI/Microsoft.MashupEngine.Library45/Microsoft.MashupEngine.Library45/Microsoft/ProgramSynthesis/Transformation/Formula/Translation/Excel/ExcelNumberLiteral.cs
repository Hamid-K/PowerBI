using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200191C RID: 6428
	internal class ExcelNumberLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600D1E9 RID: 53737 RVA: 0x002B482C File Offset: 0x002B2A2C
		public ExcelNumberLiteral(double value)
			: base(value)
		{
		}

		// Token: 0x0600D1EA RID: 53738 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}
	}
}
