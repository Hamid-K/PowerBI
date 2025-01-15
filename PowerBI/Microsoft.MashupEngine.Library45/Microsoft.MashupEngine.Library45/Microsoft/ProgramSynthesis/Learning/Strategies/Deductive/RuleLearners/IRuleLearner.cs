using System;
using System.Threading;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners
{
	// Token: 0x02000747 RID: 1863
	public interface IRuleLearner
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060027F2 RID: 10226
		Type SpecType { get; }

		// Token: 0x060027F3 RID: 10227
		Optional<ProgramSet> LearnRule(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel);

		// Token: 0x060027F4 RID: 10228
		bool CanCall(GrammarRule rule, Spec spec);
	}
}
