using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E4 RID: 6372
	internal class PowerFxGreaterThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CFA3 RID: 53155 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxGreaterThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x0600CFA4 RID: 53156 RVA: 0x002B8F38 File Offset: 0x002B7138
		public override string Symbol
		{
			get
			{
				return ">=";
			}
		}

		// Token: 0x0600CFA5 RID: 53157 RVA: 0x002C43FA File Offset: 0x002C25FA
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxGreaterThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
