using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation
{
	// Token: 0x020017E2 RID: 6114
	internal interface IFormulaLiteral<out T>
	{
		// Token: 0x170021E9 RID: 8681
		// (get) Token: 0x0600C973 RID: 51571
		T Value { get; }
	}
}
