using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200181F RID: 6175
	internal class SqlConcat : FormulaConcat
	{
		// Token: 0x0600CA9C RID: 51868 RVA: 0x002B48DD File Offset: 0x002B2ADD
		public SqlConcat(FormulaExpression left, FormulaExpression right)
			: base(left, right, 6)
		{
		}

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x0600CA9D RID: 51869 RVA: 0x002B3143 File Offset: 0x002B1343
		public override string Symbol
		{
			get
			{
				return "+";
			}
		}

		// Token: 0x0600CA9E RID: 51870 RVA: 0x002B48E8 File Offset: 0x002B2AE8
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlConcat(base.Left.Accept<FormulaExpression>(visitor), base.Right.Accept<FormulaExpression>(visitor));
		}
	}
}
