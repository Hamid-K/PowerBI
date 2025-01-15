using System;
using System.Threading;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners
{
	// Token: 0x0200074F RID: 1871
	internal abstract class StdRuleLearner<TRule, TSpec> : Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Static where TRule : GrammarRule where TSpec : Spec
	{
		// Token: 0x06002814 RID: 10260 RVA: 0x000719EF File Offset: 0x0006FBEF
		public StdRuleLearner(TRule rule)
			: base(rule)
		{
			base.Learner = MethodReference.WithoutReference<Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Static.RuleLearner>(new Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Static.RuleLearner(this.LearnRule));
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x00071A14 File Offset: 0x0006FC14
		public override bool CanCall(GrammarRule rule, Spec spec)
		{
			return base.CanCall(rule, spec) && rule is TRule && spec is TSpec;
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x00071587 File Offset: 0x0006F787
		public override Type SpecType
		{
			get
			{
				return typeof(TSpec);
			}
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x00071A33 File Offset: 0x0006FC33
		private new Optional<ProgramSet> LearnRule(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel)
		{
			return this.LearnRule(engine, (TRule)((object)rule), (task as LearningTask<TSpec>) ?? new LearningTask<TSpec>(task), cancel);
		}

		// Token: 0x06002818 RID: 10264
		protected abstract Optional<ProgramSet> LearnRule(SynthesisEngine engine, TRule rule, LearningTask<TSpec> task, CancellationToken cancel);
	}
}
