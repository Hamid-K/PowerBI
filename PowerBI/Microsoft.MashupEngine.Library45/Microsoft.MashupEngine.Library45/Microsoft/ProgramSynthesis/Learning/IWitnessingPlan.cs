using System;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006BB RID: 1723
	internal interface IWitnessingPlan
	{
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002546 RID: 9542
		Type RuleType { get; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002547 RID: 9543
		Type SpecType { get; }

		// Token: 0x06002548 RID: 9544
		bool CanCall(GrammarRule rule, Spec spec);

		// Token: 0x06002549 RID: 9545
		WitnessFunction PreferredWitnessFunctionFor(GrammarRule rule, int parameter, Spec spec);
	}
}
