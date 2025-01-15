using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017E0 RID: 6112
	internal interface IFormulaBinaryOperator : IFormulaOperator
	{
		// Token: 0x170021E5 RID: 8677
		// (get) Token: 0x0600C96F RID: 51567
		bool Associative { get; }

		// Token: 0x170021E6 RID: 8678
		// (get) Token: 0x0600C970 RID: 51568
		FormulaExpression Left { get; }

		// Token: 0x170021E7 RID: 8679
		// (get) Token: 0x0600C971 RID: 51569
		FormulaExpression Right { get; }
	}
}
