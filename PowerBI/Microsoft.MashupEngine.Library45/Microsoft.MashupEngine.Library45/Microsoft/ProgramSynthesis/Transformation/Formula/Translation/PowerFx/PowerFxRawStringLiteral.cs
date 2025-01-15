using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018EA RID: 6378
	internal class PowerFxRawStringLiteral : FormulaRawStringLiteral
	{
		// Token: 0x0600CFBA RID: 53178 RVA: 0x002C4539 File Offset: 0x002C2739
		public PowerFxRawStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600CFBB RID: 53179 RVA: 0x002C4542 File Offset: 0x002C2742
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxRawStringLiteral(base.Value);
		}

		// Token: 0x0600CFBC RID: 53180 RVA: 0x002BFFC0 File Offset: 0x002BE1C0
		protected override string ToCodeString()
		{
			return "\"" + base.Value + "\"";
		}
	}
}
