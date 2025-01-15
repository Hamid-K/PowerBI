using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerQuery
{
	// Token: 0x020018B1 RID: 6321
	internal class PowerQueryOr : FormulaBinaryOperator
	{
		// Token: 0x0600CE29 RID: 52777 RVA: 0x002BFD5E File Offset: 0x002BDF5E
		public PowerQueryOr(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4, false, false)
		{
		}

		// Token: 0x170022A4 RID: 8868
		// (get) Token: 0x0600CE2A RID: 52778 RVA: 0x002B8EEC File Offset: 0x002B70EC
		public override string Symbol
		{
			get
			{
				return "or";
			}
		}

		// Token: 0x0600CE2B RID: 52779 RVA: 0x002BFD6B File Offset: 0x002BDF6B
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerQueryOr(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
