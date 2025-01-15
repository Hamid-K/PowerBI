using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x0200191D RID: 6429
	internal class ExcelDateTimeLiteral : FormulaDateTimeLiteral
	{
		// Token: 0x0600D1EB RID: 53739 RVA: 0x002B8A01 File Offset: 0x002B6C01
		public ExcelDateTimeLiteral(DateTime value)
			: base(value)
		{
		}

		// Token: 0x0600D1EC RID: 53740 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D1ED RID: 53741 RVA: 0x002CC450 File Offset: 0x002CA650
		protected override string ToCodeString()
		{
			if (!(base.Value == base.Value.Date))
			{
				return string.Format("DateValue(\"{0:s}\")", base.Value);
			}
			return string.Format("DateValue(\"{0:yyyy-MM-dd}\")", base.Value);
		}
	}
}
