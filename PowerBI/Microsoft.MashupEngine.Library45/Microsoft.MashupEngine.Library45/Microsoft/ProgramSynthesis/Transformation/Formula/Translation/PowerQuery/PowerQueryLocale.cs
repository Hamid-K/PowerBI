using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B9 RID: 6329
	internal class PowerQueryLocale : FormulaStringLiteral
	{
		// Token: 0x0600CE47 RID: 52807 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public PowerQueryLocale(string value)
			: base(value)
		{
		}

		// Token: 0x0600CE48 RID: 52808 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CE49 RID: 52809 RVA: 0x002BFFC0 File Offset: 0x002BE1C0
		protected override string ToCodeString()
		{
			return "\"" + base.Value + "\"";
		}
	}
}
