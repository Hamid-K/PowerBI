using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B2 RID: 6322
	internal class PowerQueryGreaterThan : FormulaBinaryOperator
	{
		// Token: 0x0600CE2C RID: 52780 RVA: 0x002BFD8A File Offset: 0x002BDF8A
		public PowerQueryGreaterThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 14, false, false)
		{
		}

		// Token: 0x170022A5 RID: 8869
		// (get) Token: 0x0600CE2D RID: 52781 RVA: 0x002B8F12 File Offset: 0x002B7112
		public override string Symbol
		{
			get
			{
				return ">";
			}
		}

		// Token: 0x0600CE2E RID: 52782 RVA: 0x002BFD98 File Offset: 0x002BDF98
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryGreaterThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
