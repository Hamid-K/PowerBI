using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Sql
{
	// Token: 0x02001818 RID: 6168
	internal class SqlParenthesis : FormulaParenthesis
	{
		// Token: 0x0600CA8B RID: 51851 RVA: 0x002B47BD File Offset: 0x002B29BD
		public SqlParenthesis(FormulaExpression body)
			: base(body)
		{
		}

		// Token: 0x0600CA8C RID: 51852 RVA: 0x002B47C6 File Offset: 0x002B29C6
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new SqlParenthesis(base.Body.Accept<FormulaExpression>(visitor));
		}
	}
}
