using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018AE RID: 6318
	internal class PowerQueryEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CE20 RID: 52768 RVA: 0x002BFCDF File Offset: 0x002BDEDF
		public PowerQueryEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 12, false, false)
		{
		}

		// Token: 0x170022A1 RID: 8865
		// (get) Token: 0x0600CE21 RID: 52769 RVA: 0x002B8E47 File Offset: 0x002B7047
		public override string Symbol
		{
			get
			{
				return "=";
			}
		}

		// Token: 0x0600CE22 RID: 52770 RVA: 0x002BFCED File Offset: 0x002BDEED
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
