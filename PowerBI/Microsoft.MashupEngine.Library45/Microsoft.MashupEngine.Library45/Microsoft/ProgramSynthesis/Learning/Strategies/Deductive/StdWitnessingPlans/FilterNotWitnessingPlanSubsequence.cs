using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000732 RID: 1842
	internal class FilterNotWitnessingPlanSubsequence : StdWitnessingPlan<FilterNot, SubsequenceSpec>
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x00070017 File Offset: 0x0006E217
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessSet(FilterNot rule, SubsequenceSpec spec)
		{
			return new SubsequenceSpec(spec.PositiveExamples);
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00070490 File Offset: 0x0006E690
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static ExampleSpec WitnessPredicate(FilterNot rule, SubsequenceSpec outerSpec, ExampleSpec supersetValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				foreach (object obj in outerSpec.PositiveExamples[state])
				{
					if (obj != null)
					{
						State state2 = state.WithFunctionalInput(obj, false);
						object obj2;
						if (dictionary.TryGetValue(state2, out obj2) && !false.Equals(obj2))
						{
							return null;
						}
						dictionary[state2] = false;
					}
				}
				foreach (object obj3 in outerSpec.NegativeExamples[state].Intersect(supersetValues.Examples[state].ToEnumerable<object>()))
				{
					if (obj3 != null)
					{
						State state3 = state.WithFunctionalInput(obj3, false);
						object obj4;
						if (dictionary.TryGetValue(state3, out obj4) && !true.Equals(obj4))
						{
							return null;
						}
						dictionary[state3] = true;
					}
				}
			}
			return new ExampleSpec(dictionary);
		}
	}
}
