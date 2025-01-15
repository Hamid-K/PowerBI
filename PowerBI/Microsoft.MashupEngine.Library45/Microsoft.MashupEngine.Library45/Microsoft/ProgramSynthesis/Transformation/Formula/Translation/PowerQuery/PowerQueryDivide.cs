using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A7 RID: 6311
	internal class PowerQueryDivide : FormulaDivide
	{
		// Token: 0x0600CE08 RID: 52744 RVA: 0x002BF926 File Offset: 0x002BDB26
		public PowerQueryDivide(FormulaExpression left, FormulaExpression right)
			: base(left, right, 18)
		{
		}

		// Token: 0x0600CE09 RID: 52745 RVA: 0x002BF932 File Offset: 0x002BDB32
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryDivide(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
