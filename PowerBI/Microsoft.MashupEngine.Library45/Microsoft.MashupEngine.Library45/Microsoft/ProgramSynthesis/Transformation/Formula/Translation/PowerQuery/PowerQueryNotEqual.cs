using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018AF RID: 6319
	internal class PowerQueryNotEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CE23 RID: 52771 RVA: 0x002BFCDF File Offset: 0x002BDEDF
		public PowerQueryNotEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 12, false, false)
		{
		}

		// Token: 0x170022A2 RID: 8866
		// (get) Token: 0x0600CE24 RID: 52772 RVA: 0x002BFD0C File Offset: 0x002BDF0C
		public override string Symbol
		{
			get
			{
				return "<>";
			}
		}

		// Token: 0x0600CE25 RID: 52773 RVA: 0x002BFD13 File Offset: 0x002BDF13
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryNotEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
