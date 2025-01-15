using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B0 RID: 6320
	internal class PowerQueryAnd : FormulaBinaryOperator
	{
		// Token: 0x0600CE26 RID: 52774 RVA: 0x002BFD32 File Offset: 0x002BDF32
		public PowerQueryAnd(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6, false, false)
		{
		}

		// Token: 0x170022A3 RID: 8867
		// (get) Token: 0x0600CE27 RID: 52775 RVA: 0x002B8EC6 File Offset: 0x002B70C6
		public override string Symbol
		{
			get
			{
				return "and";
			}
		}

		// Token: 0x0600CE28 RID: 52776 RVA: 0x002BFD3F File Offset: 0x002BDF3F
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryAnd(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
