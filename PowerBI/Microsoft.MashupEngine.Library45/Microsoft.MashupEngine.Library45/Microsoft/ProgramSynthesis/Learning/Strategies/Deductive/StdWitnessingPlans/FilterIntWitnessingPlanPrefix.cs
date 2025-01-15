using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x0200072A RID: 1834
	internal class FilterIntWitnessingPlanPrefix : StdWitnessingPlan<FilterInt, PrefixSpec>
	{
		// Token: 0x06002790 RID: 10128 RVA: 0x00070017 File Offset: 0x0006E217
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessList(FilterInt rule, PrefixSpec spec)
		{
			return new SubsequenceSpec(spec.PositiveExamples);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x00070024 File Offset: 0x0006E224
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static DisjunctiveExamplesSpec WitnessSlice(FilterInt rule, PrefixSpec spec, ExampleSpec listValues)
		{
			List<Record<int, int>> initIterPairs = FilterIntWitnessingPlan.FindInitIterPairs(spec, listValues, true);
			if (initIterPairs.Count == 0)
			{
				return null;
			}
			return DisjunctiveExamplesSpec.From(spec.ProvidedInputs.ToDictionary((State s) => s, (State s) => initIterPairs.Cast<object>()));
		}
	}
}
