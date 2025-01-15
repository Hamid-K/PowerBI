using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B4 RID: 6324
	internal class PowerQueryLessThan : FormulaBinaryOperator
	{
		// Token: 0x0600CE32 RID: 52786 RVA: 0x002BFD8A File Offset: 0x002BDF8A
		public PowerQueryLessThan(FormulaExpression left, FormulaExpression right)
			: base(left, right, 14, false, false)
		{
		}

		// Token: 0x170022A7 RID: 8871
		// (get) Token: 0x0600CE33 RID: 52787 RVA: 0x002B8F5E File Offset: 0x002B715E
		public override string Symbol
		{
			get
			{
				return "<";
			}
		}

		// Token: 0x0600CE34 RID: 52788 RVA: 0x002BFDD6 File Offset: 0x002BDFD6
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryLessThan(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
