using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000734 RID: 1844
	internal class FilterWitnessingPlanSubsequence : StdWitnessingPlan<Filter, SubsequenceSpec>
	{
		// Token: 0x060027AB RID: 10155 RVA: 0x00070017 File Offset: 0x0006E217
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessSet(Filter rule, SubsequenceSpec spec)
		{
			return new SubsequenceSpec(spec.PositiveExamples);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00070898 File Offset: 0x0006EA98
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static ExampleSpec WitnessPredicate(Filter rule, SubsequenceSpec spec, ExampleSpec supersetValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in spec.ProvidedInputs)
			{
				foreach (object obj in spec.PositiveExamples[state])
				{
					if (obj != null)
					{
						State state2 = state.WithFunctionalInput(obj, false);
						object obj2;
						if (dictionary.TryGetValue(state2, out obj2) && !true.Equals(obj2))
						{
							return null;
						}
						dictionary[state2] = true;
					}
				}
				foreach (object obj3 in spec.NegativeExamples[state].Intersect(supersetValues.Examples[state].ToEnumerable<object>()))
				{
					if (obj3 != null)
					{
						State state3 = state.WithFunctionalInput(obj3, false);
						object obj4;
						if (dictionary.TryGetValue(state3, out obj4) && !false.Equals(obj4))
						{
							return null;
						}
						dictionary[state3] = false;
					}
				}
			}
			return new ExampleSpec(dictionary);
		}
	}
}
