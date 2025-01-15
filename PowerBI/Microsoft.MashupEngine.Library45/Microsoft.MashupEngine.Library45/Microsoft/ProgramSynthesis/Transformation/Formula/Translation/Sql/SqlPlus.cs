using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200181C RID: 6172
	internal class SqlPlus : FormulaPlus
	{
		// Token: 0x0600CA96 RID: 51862 RVA: 0x002B485F File Offset: 0x002B2A5F
		public SqlPlus(FormulaExpression left, FormulaExpression right)
			: base(left, right, 4)
		{
		}

		// Token: 0x0600CA97 RID: 51863 RVA: 0x002B486A File Offset: 0x002B2A6A
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlPlus(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
