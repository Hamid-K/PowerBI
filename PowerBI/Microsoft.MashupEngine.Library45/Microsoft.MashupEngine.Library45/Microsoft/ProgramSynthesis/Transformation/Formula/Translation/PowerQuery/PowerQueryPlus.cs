using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A5 RID: 6309
	internal class PowerQueryPlus : FormulaPlus
	{
		// Token: 0x0600CE04 RID: 52740 RVA: 0x002BF8D0 File Offset: 0x002BDAD0
		public PowerQueryPlus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 16)
		{
		}

		// Token: 0x0600CE05 RID: 52741 RVA: 0x002BF8DC File Offset: 0x002BDADC
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryPlus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
