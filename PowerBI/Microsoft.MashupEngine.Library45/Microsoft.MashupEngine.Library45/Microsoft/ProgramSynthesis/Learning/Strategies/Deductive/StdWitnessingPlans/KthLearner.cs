using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x0200073B RID: 1851
	internal class KthLearner : StdWitnessingPlan<Kth, ExampleSpec>
	{
		// Token: 0x060027C1 RID: 10177 RVA: 0x00071064 File Offset: 0x0006F264
		[WitnessFunction(0)]
		internal static SubsequenceSpec WitnessSequence(Kth rule, ExampleSpec spec)
		{
			return new SubsequenceSpec(spec.Examples.ToDictionary((KeyValuePair<State, object> kvp) => kvp.Key, (KeyValuePair<State, object> kvp) => Seq.Of<object>(new object[] { kvp.Value })));
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000710C0 File Offset: 0x0006F2C0
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		internal static DisjunctiveExamplesSpec WitnessK(Kth rule, DisjunctiveExamplesSpec spec, ExampleSpec sequenceValues)
		{
			Dictionary<State, List<int>> dictionary = new Dictionary<State, List<int>>();
			foreach (KeyValuePair<State, object> keyValuePair in sequenceValues.Examples)
			{
				State key = keyValuePair.Key;
				object[] array = keyValuePair.Value.ToEnumerable<object>().ToArray<object>();
				HashSet<int> hashSet = new HashSet<int>();
				for (int i = 0; i < array.Length; i++)
				{
					if (spec.Valid(key, array[i]))
					{
						hashSet.Add(i);
						hashSet.Add(i - array.Length);
					}
				}
				dictionary[key] = hashSet.ToList<int>();
			}
			return DisjunctiveExamplesSpec.From(dictionary.ToDictionary((KeyValuePair<State, List<int>> kvp) => kvp.Key, (KeyValuePair<State, List<int>> kvp) => kvp.Value.Cast<object>()));
		}
	}
}
