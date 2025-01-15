using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B5 RID: 6325
	internal class PowerQueryLessThanEqual : FormulaBinaryOperator
	{
		// Token: 0x0600CE35 RID: 52789 RVA: 0x002BFD8A File Offset: 0x002BDF8A
		public PowerQueryLessThanEqual(FormulaExpression left, FormulaExpression right)
			: base(left, right, 14, false, false)
		{
		}

		// Token: 0x170022A8 RID: 8872
		// (get) Token: 0x0600CE36 RID: 52790 RVA: 0x002B8F84 File Offset: 0x002B7184
		public override string Symbol
		{
			get
			{
				return "<=";
			}
		}

		// Token: 0x0600CE37 RID: 52791 RVA: 0x002BFDF5 File Offset: 0x002BDFF5
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryLessThanEqual(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
