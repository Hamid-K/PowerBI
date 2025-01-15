using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200181B RID: 6171
	internal class SqlMinus : FormulaMinus
	{
		// Token: 0x0600CA94 RID: 51860 RVA: 0x002B4835 File Offset: 0x002B2A35
		public SqlMinus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600CA95 RID: 51861 RVA: 0x002B4840 File Offset: 0x002B2A40
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlMinus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
