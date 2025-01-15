using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001918 RID: 6424
	internal class ExcelStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600D1D9 RID: 53721 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public ExcelStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600D1DA RID: 53722 RVA: 0x002CC364 File Offset: 0x002CA564
		protected ExcelStringLiteral()
		{
		}

		// Token: 0x0600D1DB RID: 53723 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D1DC RID: 53724 RVA: 0x002CC36C File Offset: 0x002CA56C
		protected override string ToCodeString()
		{
			if (base.Value != null)
			{
				return "\"" + base.Value.Replace("\"", "\"\"") + "\"";
			}
			return null;
		}
	}
}
