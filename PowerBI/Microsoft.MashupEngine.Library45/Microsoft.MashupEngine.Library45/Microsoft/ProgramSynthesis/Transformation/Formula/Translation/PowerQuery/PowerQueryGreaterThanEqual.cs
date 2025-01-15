using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B3 RID: 6323
	internal class PowerQueryGreaterThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CE2F RID: 52783 RVA: 0x002BFD8A File Offset: 0x002BDF8A
		public PowerQueryGreaterThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 14, false, false)
		{
		}

		// Token: 0x170022A6 RID: 8870
		// (get) Token: 0x0600CE30 RID: 52784 RVA: 0x002B8F38 File Offset: 0x002B7138
		public override string Symbol
		{
			get
			{
				return ">=";
			}
		}

		// Token: 0x0600CE31 RID: 52785 RVA: 0x002BFDB7 File Offset: 0x002BDFB7
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryGreaterThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
