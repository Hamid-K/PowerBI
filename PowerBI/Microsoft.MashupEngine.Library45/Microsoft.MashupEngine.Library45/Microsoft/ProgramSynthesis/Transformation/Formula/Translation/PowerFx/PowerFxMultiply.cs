using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018DB RID: 6363
	internal class PowerFxMultiply : FormulaMultiply
	{
		// Token: 0x0600CF88 RID: 53128 RVA: 0x002B4889 File Offset: 0x002B2A89
		public PowerFxMultiply(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600CF89 RID: 53129 RVA: 0x002C42AD File Offset: 0x002C24AD
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxMultiply(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
