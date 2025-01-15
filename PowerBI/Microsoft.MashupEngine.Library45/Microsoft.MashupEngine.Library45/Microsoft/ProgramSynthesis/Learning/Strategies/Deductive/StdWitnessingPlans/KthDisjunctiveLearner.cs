using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x0200073D RID: 1853
	internal class KthDisjunctiveLearner : StdWitnessingPlan<Kth, DisjunctiveExamplesSpec>
	{
		// Token: 0x060027CA RID: 10186 RVA: 0x000711EC File Offset: 0x0006F3EC
		[WitnessFunction(0)]
		internal static DisjunctiveSubsequenceSpec WitnessSequence(Kth rule, DisjunctiveExamplesSpec spec)
		{
			return new DisjunctiveSubsequenceSpec(spec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Value.Select((object v) => Seq.Of<object>(new object[] { v }))));
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x00071247 File Offset: 0x0006F447
		[WitnessFunction(1, DependsOnParameters = new int[] { 0 })]
		internal static DisjunctiveExamplesSpec WitnessK(Kth rule, DisjunctiveExamplesSpec spec, ExampleSpec sequenceValues)
		{
			return KthLearner.WitnessK(rule, spec, sequenceValues);
		}
	}
}
