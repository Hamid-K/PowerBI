using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E2 RID: 6370
	internal class PowerFxOr : FormulaBinaryOperator
	{
		// Token: 0x0600CF9D RID: 53149 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxOr(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022D0 RID: 8912
		// (get) Token: 0x0600CF9E RID: 53150 RVA: 0x002C43D4 File Offset: 0x002C25D4
		public override string Symbol
		{
			get
			{
				return "||";
			}
		}

		// Token: 0x0600CF9F RID: 53151 RVA: 0x002C43B5 File Offset: 0x002C25B5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxAnd(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
