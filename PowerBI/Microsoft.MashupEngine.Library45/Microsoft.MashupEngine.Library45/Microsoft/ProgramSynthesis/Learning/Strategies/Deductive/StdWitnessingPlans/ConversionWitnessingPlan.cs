using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000727 RID: 1831
	internal class ConversionWitnessingPlan : StdWitnessingPlan<ConversionRule, Spec>
	{
		// Token: 0x06002789 RID: 10121 RVA: 0x0006FF80 File Offset: 0x0006E180
		[WitnessFunction(0)]
		internal static Spec Witness(ConversionRule rule, Spec spec)
		{
			if (!rule.IsTrivial)
			{
				return spec.TransformInputs((State s) => rule.Substitutions.Aggregate(s, (State acc, KeyValuePair<Symbol, Symbol> sub) => acc.Substitute(sub.Value, sub.Key)));
			}
			return spec;
		}
	}
}
