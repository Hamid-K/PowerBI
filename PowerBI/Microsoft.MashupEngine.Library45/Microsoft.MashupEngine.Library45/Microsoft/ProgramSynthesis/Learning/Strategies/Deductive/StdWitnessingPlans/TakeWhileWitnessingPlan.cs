using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000745 RID: 1861
	internal class TakeWhileWitnessingPlan : StdWitnessingPlan<TakeWhile, SubsequenceSpec>
	{
		// Token: 0x060027ED RID: 10221 RVA: 0x0000E945 File Offset: 0x0000CB45
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessSet(TakeWhile rule, SubsequenceSpec spec)
		{
			return spec;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x00071684 File Offset: 0x0006F884
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static ExampleSpec WitnessPredicate(TakeWhile rule, SubsequenceSpec spec, ExampleSpec sequenceValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in sequenceValues.Examples)
			{
				State key = keyValuePair.Key;
				IEnumerable<object> enumerable = keyValuePair.Value.ToEnumerable<object>();
				HashSet<object> hashSet = new HashSet<object>(spec.PositiveExamples[key]);
				foreach (object obj in enumerable)
				{
					if (hashSet.Count == 0)
					{
						break;
					}
					if (hashSet.Contains(obj))
					{
						hashSet.Remove(obj);
					}
					State state = key.WithFunctionalInput(obj, false);
					dictionary[state] = true;
				}
				if (hashSet.Count > 0)
				{
					return null;
				}
			}
			return new ExampleSpec(dictionary);
		}
	}
}
