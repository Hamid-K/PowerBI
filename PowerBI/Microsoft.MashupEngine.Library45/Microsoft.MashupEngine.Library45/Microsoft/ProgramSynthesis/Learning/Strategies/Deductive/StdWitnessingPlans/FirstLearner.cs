using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000739 RID: 1849
	internal class FirstLearner : StdWitnessingPlan<First, ExampleSpec>
	{
		// Token: 0x060027BA RID: 10170 RVA: 0x00070EE0 File Offset: 0x0006F0E0
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessSequence(First rule, ExampleSpec spec)
		{
			return new SubsequenceSpec(spec.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => Seq.Of<object>(new object[] { kvp.Value })));
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00070F3C File Offset: 0x0006F13C
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static ExampleSpec WitnessPredicate(First rule, ExampleSpec spec, ExampleSpec sequenceValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in sequenceValues.Examples)
			{
				State key = keyValuePair.Key;
				IEnumerable<object> enumerable = keyValuePair.Value.ToEnumerable<object>();
				object obj = spec.Examples[key];
				bool flag = false;
				foreach (object obj2 in enumerable)
				{
					State state = key.WithFunctionalInput(obj2, false);
					if (ValueEquality.Comparer.Equals(obj2, obj))
					{
						dictionary[state] = true;
						flag = true;
						break;
					}
					dictionary[state] = false;
				}
				if (!flag)
				{
					return null;
				}
			}
			return new ExampleSpec(dictionary);
		}
	}
}
