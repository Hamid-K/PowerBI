using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018A4 RID: 6308
	internal class PowerQueryMinus : FormulaMinus
	{
		// Token: 0x0600CE02 RID: 52738 RVA: 0x002BF8A5 File Offset: 0x002BDAA5
		public PowerQueryMinus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 16)
		{
		}

		// Token: 0x0600CE03 RID: 52739 RVA: 0x002BF8B1 File Offset: 0x002BDAB1
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryMinus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
