using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Contracts
{
	// Token: 0x02001755 RID: 5973
	public interface IConditionalBranch
	{
		// Token: 0x170021B7 RID: 8631
		// (get) Token: 0x0600C640 RID: 50752
		Predicate Predicate { get; }

		// Token: 0x170021B8 RID: 8632
		// (get) Token: 0x0600C641 RID: 50753
		IProgram Program { get; }

		// Token: 0x0600C642 RID: 50754
		string ToString();
	}
}
