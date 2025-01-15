using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A1 RID: 6305
	internal class PowerQueryNullLiteral : FormulaExpression
	{
		// Token: 0x0600CDF8 RID: 52728 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}

		// Token: 0x0600CDF9 RID: 52729 RVA: 0x002BF808 File Offset: 0x002BDA08
		protected override string ToCodeString()
		{
			return "null";
		}
	}
}
