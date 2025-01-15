using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017E1 RID: 6113
	internal interface IFormulaUnaryOperator : IFormulaOperator
	{
		// Token: 0x170021E8 RID: 8680
		// (get) Token: 0x0600C972 RID: 51570
		FormulaExpression Subject { get; }
	}
}
