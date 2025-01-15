using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018EB RID: 6379
	internal class PowerFxLocale : FormulaStringLiteral
	{
		// Token: 0x0600CFBD RID: 53181 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public PowerFxLocale(string value)
			: base(value)
		{
		}

		// Token: 0x0600CFBE RID: 53182 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CFBF RID: 53183 RVA: 0x002BFFC0 File Offset: 0x002BE1C0
		protected override string ToCodeString()
		{
			return "\"" + base.Value + "\"";
		}
	}
}
