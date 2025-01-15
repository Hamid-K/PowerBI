using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics.Extensions
{
	// Token: 0x02001ED7 RID: 7895
	public static class SubsequenceSpecExtensions
	{
		// Token: 0x06010A7F RID: 68223 RVA: 0x003958C8 File Offset: 0x00393AC8
		private static SubsequenceSpec MergeWith(this SubsequenceSpec spec1, SubsequenceSpec spec2)
		{
			if (!spec1.NegativeExamples.Any((KeyValuePair<State, HashSet<object>> e) => e.Value.Any<object>()))
			{
				if (!spec2.NegativeExamples.Any((KeyValuePair<State, HashSet<object>> e) => e.Value.Any<object>()))
				{
					return new SubsequenceSpec(spec1.PositiveExamples.Concat(spec2.PositiveExamples).GroupBy((KeyValuePair<State, IEnumerable<object>> kv) => kv.Key, (State key, IEnumerable<KeyValuePair<State, IEnumerable<object>>> stateExampleKvp) => new KeyValuePair<State, IEnumerable<object>>(key, stateExampleKvp.SelectMany((KeyValuePair<State, IEnumerable<object>> ex) => ex.Value))));
				}
			}
			throw new Exception("Specs should not contain negative examples");
		}

		// Token: 0x06010A80 RID: 68224 RVA: 0x00395991 File Offset: 0x00393B91
		public static SubsequenceSpec Merge(this IEnumerable<SubsequenceSpec> specs)
		{
			Func<SubsequenceSpec, SubsequenceSpec, SubsequenceSpec> func;
			if ((func = SubsequenceSpecExtensions.<>O.<0>__MergeWith) == null)
			{
				func = (SubsequenceSpecExtensions.<>O.<0>__MergeWith = new Func<SubsequenceSpec, SubsequenceSpec, SubsequenceSpec>(SubsequenceSpecExtensions.MergeWith));
			}
			return specs.Aggregate(func);
		}

		// Token: 0x02001ED8 RID: 7896
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400638E RID: 25486
			public static Func<SubsequenceSpec, SubsequenceSpec, SubsequenceSpec> <0>__MergeWith;
		}
	}
}
