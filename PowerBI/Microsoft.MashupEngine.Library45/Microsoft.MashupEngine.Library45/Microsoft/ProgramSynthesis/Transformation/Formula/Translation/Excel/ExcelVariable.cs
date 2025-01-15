using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Excel
{
	// Token: 0x02001927 RID: 6439
	internal class ExcelVariable : FormulaVariable
	{
		// Token: 0x0600D205 RID: 53765 RVA: 0x002BF983 File Offset: 0x002BDB83
		public ExcelVariable(string name)
			: base(name)
		{
		}

		// Token: 0x0600D206 RID: 53766 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D207 RID: 53767 RVA: 0x002CC5BA File Offset: 0x002CA7BA
		public override FormulaExpression CloneWith(string name)
		{
			return new ExcelVariable(name);
		}
	}
}
