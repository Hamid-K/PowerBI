using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.PowerFx
{
	// Token: 0x020018D2 RID: 6354
	internal class PowerFxParenthesis : FormulaParenthesis
	{
		// Token: 0x0600CF6E RID: 53102 RVA: 0x002B47BD File Offset: 0x002B29BD
		public PowerFxParenthesis(FormulaExpression body)
			: base(body)
		{
		}

		// Token: 0x0600CF6F RID: 53103 RVA: 0x002C4010 File Offset: 0x002C2210
		public override FormulaExpression AcceptClone(IFormulaVisitor<FormulaExpression> visitor)
		{
			return new PowerFxParenthesis(base.Body.Accept<FormulaExpression>(visitor));
		}
	}
}
