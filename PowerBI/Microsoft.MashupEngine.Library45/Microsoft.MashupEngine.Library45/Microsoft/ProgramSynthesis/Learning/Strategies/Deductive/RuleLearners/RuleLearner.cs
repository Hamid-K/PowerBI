using System;
using System.Reflection;
using System.Threading;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners
{
	// Token: 0x02000749 RID: 1865
	internal abstract class RuleLearner : IRuleLearner
	{
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060027F7 RID: 10231 RVA: 0x00071813 File Offset: 0x0006FA13
		private GrammarRule Rule { get; }

		// Token: 0x060027F8 RID: 10232 RVA: 0x0007181C File Offset: 0x0006FA1C
		public Optional<ProgramSet> LearnRule(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel)
		{
			if (!this.CanCall(rule, task.Spec))
			{
				throw new ArgumentException("The rule/spec should be of the supported types");
			}
			MethodInfo methodInfo = typeof(LearningTask).GetMethod("Cast").MakeGenericMethod(new Type[] { this.SpecType });
			return this.Learn(engine, rule, (LearningTask)methodInfo.Invoke(task, new object[0]), cancel);
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060027F9 RID: 10233 RVA: 0x00071888 File Offset: 0x0006FA88
		public virtual Type SpecType
		{
			get
			{
				Type type;
				if ((type = this._specType) == null)
				{
					type = (this._specType = this.OriginalLearner.GetParameters()[RuleLearner.RuleSpecIndex].ParameterType.GenericTypeArguments[0]);
				}
				return type;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060027FA RID: 10234
		public abstract MethodInfo OriginalLearner { get; }

		// Token: 0x060027FB RID: 10235 RVA: 0x000718C5 File Offset: 0x0006FAC5
		public virtual bool CanCall(GrammarRule rule, Spec spec)
		{
			return this.Rule.Equals(rule) && this.SpecType.IsInstanceOfType(spec);
		}

		// Token: 0x060027FC RID: 10236
		public abstract Optional<ProgramSet> Learn(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel);

		// Token: 0x060027FD RID: 10237 RVA: 0x000718E3 File Offset: 0x0006FAE3
		protected RuleLearner(GrammarRule rule)
		{
			this.Rule = rule;
		}

		// Token: 0x04001377 RID: 4983
		private static readonly int RuleSpecIndex = Array.FindIndex<ParameterInfo>(typeof(RuleLearner.Static.RuleLearner).GetMethod("Invoke").GetParameters(), (ParameterInfo p) => typeof(LearningTask).IsAssignableFrom(p.ParameterType));

		// Token: 0x04001378 RID: 4984
		private Type _specType;

		// Token: 0x0200074A RID: 1866
		public class Static : Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner
		{
			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x060027FF RID: 10239 RVA: 0x00071927 File Offset: 0x0006FB27
			// (set) Token: 0x06002800 RID: 10240 RVA: 0x0007192F File Offset: 0x0006FB2F
			protected MethodReference<Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Static.RuleLearner> Learner { get; set; }

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x06002801 RID: 10241 RVA: 0x00071938 File Offset: 0x0006FB38
			public override MethodInfo OriginalLearner
			{
				get
				{
					return this.Learner;
				}
			}

			// Token: 0x06002802 RID: 10242 RVA: 0x00071945 File Offset: 0x0006FB45
			public Static(MethodInfo method, GrammarRule rule)
				: base(rule)
			{
				this.Learner = MethodReference.Create<Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Static.RuleLearner>(method, false);
			}

			// Token: 0x06002803 RID: 10243 RVA: 0x0007195B File Offset: 0x0006FB5B
			protected Static(GrammarRule rule)
				: base(rule)
			{
			}

			// Token: 0x06002804 RID: 10244 RVA: 0x00071964 File Offset: 0x0006FB64
			public override Optional<ProgramSet> Learn(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel)
			{
				return this.Learner.Invoke(engine, rule, task, cancel);
			}

			// Token: 0x0200074B RID: 1867
			// (Invoke) Token: 0x06002806 RID: 10246
			public delegate Optional<ProgramSet> RuleLearner(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel);
		}

		// Token: 0x0200074C RID: 1868
		public sealed class Instance : Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner
		{
			// Token: 0x170006F3 RID: 1779
			// (get) Token: 0x06002809 RID: 10249 RVA: 0x0007197B File Offset: 0x0006FB7B
			public DomainLearningLogic Logic { get; }

			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x0600280A RID: 10250 RVA: 0x00071983 File Offset: 0x0006FB83
			public override MethodInfo OriginalLearner { get; }

			// Token: 0x0600280B RID: 10251 RVA: 0x0007198B File Offset: 0x0006FB8B
			public Instance(MethodInfo method, GrammarRule rule, DomainLearningLogic logic)
				: base(rule)
			{
				this.Logic = logic;
				this.OriginalLearner = method;
				this._learner = MethodReference.Create<Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Instance.RuleLearner>(method, true);
			}

			// Token: 0x0600280C RID: 10252 RVA: 0x000719AF File Offset: 0x0006FBAF
			public override Optional<ProgramSet> Learn(SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel)
			{
				return this._learner.Invoke(this.Logic, engine, rule, task, cancel);
			}

			// Token: 0x0400137A RID: 4986
			private readonly MethodReference<Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners.RuleLearner.Instance.RuleLearner> _learner;

			// Token: 0x0200074D RID: 1869
			// (Invoke) Token: 0x0600280E RID: 10254
			private delegate Optional<ProgramSet> RuleLearner(DomainLearningLogic logic, SynthesisEngine engine, GrammarRule rule, LearningTask task, CancellationToken cancel);
		}
	}
}
