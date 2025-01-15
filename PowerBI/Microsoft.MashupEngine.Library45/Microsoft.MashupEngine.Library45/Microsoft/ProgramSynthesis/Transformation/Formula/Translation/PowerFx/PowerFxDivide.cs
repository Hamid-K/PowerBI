using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018DC RID: 6364
	internal class PowerFxDivide : FormulaDivide
	{
		// Token: 0x0600CF8A RID: 53130 RVA: 0x002B48B3 File Offset: 0x002B2AB3
		public PowerFxDivide(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600CF8B RID: 53131 RVA: 0x002C42CC File Offset: 0x002C24CC
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxDivide(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
