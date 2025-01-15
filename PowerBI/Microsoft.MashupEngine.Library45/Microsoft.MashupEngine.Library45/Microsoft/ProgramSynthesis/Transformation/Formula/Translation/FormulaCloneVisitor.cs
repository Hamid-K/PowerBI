using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017DE RID: 6110
	internal class FormulaCloneVisitor : IFormulaVisitor<FormulaExpression>
	{
		// Token: 0x0600C96C RID: 51564 RVA: 0x002B2458 File Offset: 0x002B0658
		public FormulaExpression Visit(FormulaExpression expression)
		{
			return expression.AcceptClone(this);
		}
	}
}
