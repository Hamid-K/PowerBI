using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000736 RID: 1846
	internal class FilterWitnessingPlanExamples : StdWitnessingPlan<Filter, ExampleSpec>
	{
		// Token: 0x060027B1 RID: 10161 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessSet(Filter rule, ExampleSpec spec)
		{
			return new SubsequenceSpec(spec.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => kvp.Value as IEnumerable<object>));
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00070CFC File Offset: 0x0006EEFC
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static ExampleSpec WitnessPredicate(Filter rule, ExampleSpec spec, ExampleSpec supersetValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in supersetValues.Examples)
			{
				State key = keyValuePair.Key;
				IEnumerable<object> enumerable = keyValuePair.Value.ToEnumerable<object>();
				HashSet<object> positives = spec.Examples[key].ToEnumerable<object>().ConvertToHashSet(ValueEquality.Comparer);
				foreach (object obj in positives)
				{
					State state = key.WithFunctionalInput(obj, false);
					object obj2;
					if (dictionary.TryGetValue(state, out obj2) && !true.Equals(obj2))
					{
						return null;
					}
					dictionary[state] = true;
				}
				IEnumerable<object> enumerable2 = enumerable;
				Func<object, bool> func;
				Func<object, bool> <>9__0;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (object e) => !positives.Contains(e));
				}
				foreach (object obj3 in enumerable2.Where(func))
				{
					State state2 = key.WithFunctionalInput(obj3, false);
					dictionary[state2] = false;
				}
			}
			return new ExampleSpec(dictionary);
		}
	}
}
