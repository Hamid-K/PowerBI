using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x02001946 RID: 6470
	internal class CSharpParenthesis : FormulaParenthesis
	{
		// Token: 0x0600D38A RID: 54154 RVA: 0x002B47BD File Offset: 0x002B29BD
		public CSharpParenthesis(FormulaExpression body)
			: base(body)
		{
		}

		// Token: 0x0600D38B RID: 54155 RVA: 0x002D1955 File Offset: 0x002CFB55
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new CSharpParenthesis(base.Body.Accept<FormulaExpression>(visitor));
		}
	}
}
