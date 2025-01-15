using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D8 RID: 6360
	internal class PowerFxNumberLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600CF82 RID: 53122 RVA: 0x002B482C File Offset: 0x002B2A2C
		public PowerFxNumberLiteral(double value)
			: base(value)
		{
		}

		// Token: 0x0600CF83 RID: 53123 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}
	}
}
