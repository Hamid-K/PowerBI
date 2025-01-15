using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x02001805 RID: 6149
	public interface IFormulaVisitor<out T>
	{
		// Token: 0x0600CA34 RID: 51764
		T Visit(FormulaExpression expression);
	}
}
