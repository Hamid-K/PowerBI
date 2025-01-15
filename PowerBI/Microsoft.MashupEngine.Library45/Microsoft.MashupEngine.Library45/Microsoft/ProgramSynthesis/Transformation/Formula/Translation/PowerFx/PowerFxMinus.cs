using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D9 RID: 6361
	internal class PowerFxMinus : FormulaMinus
	{
		// Token: 0x0600CF84 RID: 53124 RVA: 0x002B4835 File Offset: 0x002B2A35
		public PowerFxMinus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600CF85 RID: 53125 RVA: 0x002C426F File Offset: 0x002C246F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxMinus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
