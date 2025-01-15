using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A6 RID: 6310
	internal class PowerQueryMultiply : FormulaMultiply
	{
		// Token: 0x0600CE06 RID: 52742 RVA: 0x002BF8FB File Offset: 0x002BDAFB
		public PowerQueryMultiply(FormulaExpression left, FormulaExpression right)
			: base(left, right, 18)
		{
		}

		// Token: 0x0600CE07 RID: 52743 RVA: 0x002BF907 File Offset: 0x002BDB07
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryMultiply(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
