using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200181E RID: 6174
	internal class SqlDivide : FormulaDivide
	{
		// Token: 0x0600CA9A RID: 51866 RVA: 0x002B48B3 File Offset: 0x002B2AB3
		public SqlDivide(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x0600CA9B RID: 51867 RVA: 0x002B48BE File Offset: 0x002B2ABE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlDivide(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
