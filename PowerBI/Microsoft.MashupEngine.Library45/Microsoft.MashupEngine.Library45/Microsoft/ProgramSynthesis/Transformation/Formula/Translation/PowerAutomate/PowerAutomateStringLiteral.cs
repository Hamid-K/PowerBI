using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001901 RID: 6401
	internal class PowerAutomateStringLiteral : FormulaStringLiteral
	{
		// Token: 0x0600D0D9 RID: 53465 RVA: 0x002B2B68 File Offset: 0x002B0D68
		public PowerAutomateStringLiteral(string value)
			: base(value)
		{
		}

		// Token: 0x0600D0DA RID: 53466 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600D0DB RID: 53467 RVA: 0x002B477E File Offset: 0x002B297E
		protected override string ToCodeString()
		{
			if (base.Value != null)
			{
				return "'" + base.Value.Replace("'", "''").Replace("\n", "\\n") + "'";
			}
			return null;
		}
	}
}
