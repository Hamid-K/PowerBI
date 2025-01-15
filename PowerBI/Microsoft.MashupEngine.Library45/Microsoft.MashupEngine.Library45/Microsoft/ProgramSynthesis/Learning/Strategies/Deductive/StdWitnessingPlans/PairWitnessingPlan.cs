using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x0200073F RID: 1855
	internal class PairWitnessingPlan : StdWitnessingPlan<Pair, DisjunctiveExamplesSpec>
	{
		// Token: 0x060027D2 RID: 10194 RVA: 0x000712A4 File Offset: 0x0006F4A4
		[WitnessFunction(0)]
		internal static DisjunctiveExamplesSpec WitnessLeft(Pair rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in spec.ProvidedInputs)
			{
				dictionary[state] = spec.DisjunctiveExamples[state].Select((object o) => o.GetRecordItem(0)).ConvertToHashSet(ValueEquality.Comparer);
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x00071338 File Offset: 0x0006F538
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		internal static DisjunctiveExamplesSpec WitnessRight(Pair rule, DisjunctiveExamplesSpec outerSpec, ExampleSpec leftValues)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				object left = leftValues.Examples[state];
				dictionary[state] = (from o in outerSpec.DisjunctiveExamples[state]
					where ValueEquality.Comparer.Equals(o.GetRecordItem(0), left)
					select o.GetRecordItem(1)).ToArray<object>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}
	}
}
