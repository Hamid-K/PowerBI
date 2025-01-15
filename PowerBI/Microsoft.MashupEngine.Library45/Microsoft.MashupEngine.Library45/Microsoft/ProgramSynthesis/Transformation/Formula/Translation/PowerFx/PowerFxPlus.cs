using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018DA RID: 6362
	internal class PowerFxPlus : FormulaPlus
	{
		// Token: 0x0600CF86 RID: 53126 RVA: 0x002B485F File Offset: 0x002B2A5F
		public PowerFxPlus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600CF87 RID: 53127 RVA: 0x002C428E File Offset: 0x002C248E
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxPlus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
