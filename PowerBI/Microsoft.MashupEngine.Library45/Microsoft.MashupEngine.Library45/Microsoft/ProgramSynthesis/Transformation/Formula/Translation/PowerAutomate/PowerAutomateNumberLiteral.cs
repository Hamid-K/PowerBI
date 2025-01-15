using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerAutomate
{
	// Token: 0x02001905 RID: 6405
	internal class PowerAutomateNumberLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600D0E9 RID: 53481 RVA: 0x002B482C File Offset: 0x002B2A2C
		public PowerAutomateNumberLiteral(double value)
			: base(value)
		{
		}

		// Token: 0x0600D0EA RID: 53482 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}
	}
}
