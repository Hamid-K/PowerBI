using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200181D RID: 6173
	internal class SqlMultiply : FormulaMultiply
	{
		// Token: 0x0600CA98 RID: 51864 RVA: 0x002B4889 File Offset: 0x002B2A89
		public SqlMultiply(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600CA99 RID: 51865 RVA: 0x002B4894 File Offset: 0x002B2A94
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlMultiply(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
