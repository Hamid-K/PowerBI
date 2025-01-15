using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E6 RID: 6374
	internal class PowerFxLessThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CFA9 RID: 53161 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxLessThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x0600CFAA RID: 53162 RVA: 0x002B8F84 File Offset: 0x002B7184
		public override string Symbol
		{
			get
			{
				return "<=";
			}
		}

		// Token: 0x0600CFAB RID: 53163 RVA: 0x002C4438 File Offset: 0x002C2638
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxLessThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
