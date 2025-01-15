using System;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Conditionals.Predicates
{
	// Token: 0x0200173A RID: 5946
	public interface IPredicateEvaluator
	{
		// Token: 0x0600C58E RID: 50574
		bool Evaluate(IRow row);
	}
}
