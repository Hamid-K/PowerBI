using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018E0 RID: 6368
	internal class PowerFxEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CF97 RID: 53143 RVA: 0x002B8C78 File Offset: 0x002B6E78
		public PowerFxEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 0, false, false)
		{
		}

		// Token: 0x170022CE RID: 8910
		// (get) Token: 0x0600CF98 RID: 53144 RVA: 0x002B8E47 File Offset: 0x002B7047
		public override string Symbol
		{
			get
			{
				return "=";
			}
		}

		// Token: 0x0600CF99 RID: 53145 RVA: 0x002C438F File Offset: 0x002C258F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
