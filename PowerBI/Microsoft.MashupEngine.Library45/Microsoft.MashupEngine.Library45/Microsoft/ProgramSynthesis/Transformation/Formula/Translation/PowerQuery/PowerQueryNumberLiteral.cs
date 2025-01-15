using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A0 RID: 6304
	internal class PowerQueryNumberLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600CDF6 RID: 52726 RVA: 0x002B482C File Offset: 0x002B2A2C
		public PowerQueryNumberLiteral(double value)
			: base(value)
		{
		}

		// Token: 0x0600CDF7 RID: 52727 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}
	}
}
