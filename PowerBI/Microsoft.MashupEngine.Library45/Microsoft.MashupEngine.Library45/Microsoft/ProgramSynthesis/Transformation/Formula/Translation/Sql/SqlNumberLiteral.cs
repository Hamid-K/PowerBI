using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x0200181A RID: 6170
	internal class SqlNumberLiteral : FormulaNumberLiteral
	{
		// Token: 0x0600CA92 RID: 51858 RVA: 0x002B482C File Offset: 0x002B2A2C
		public SqlNumberLiteral(double value)
			: base(value)
		{
		}

		// Token: 0x0600CA93 RID: 51859 RVA: 0x00004FAE File Offset: 0x000031AE
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return this;
		}
	}
}
