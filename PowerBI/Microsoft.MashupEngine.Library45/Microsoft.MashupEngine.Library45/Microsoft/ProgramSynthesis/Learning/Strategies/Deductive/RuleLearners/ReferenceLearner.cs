using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners
{
	// Token: 0x02000755 RID: 1877
	internal class ReferenceLearner : StdRuleLearner<ConversionRule, Spec>
	{
		// Token: 0x06002824 RID: 10276 RVA: 0x00071D2F File Offset: 0x0006FF2F
		public ReferenceLearner(ConversionRule rule)
			: base(rule)
		{
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x00071D38 File Offset: 0x0006FF38
		protected override Optional<ProgramSet> LearnRule(SynthesisEngine engine, ConversionRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			if (rule.Body[0].Grammar == rule.Grammar)
			{
				return Optional<ProgramSet>.Nothing;
			}
			Func<State, State> <>9__0;
			Func<State, State> <>9__1;
			foreach (DeductiveSynthesis deductiveSynthesis in engine.Configuration.Strategies.OfType<DeductiveSynthesis>())
			{
				DomainLearningLogic learningLogic = deductiveSynthesis.LearningLogic;
				SynthesisEngine synthesisEngine;
				if (!deductiveSynthesis.ExternEngineCache.TryGetValue(rule, out synthesisEngine))
				{
					DomainLearningLogic externLearningLogicFor = learningLogic.GetExternLearningLogicFor(rule);
					if (externLearningLogicFor == null)
					{
						continue;
					}
					SynthesisEngine.Config config = engine.Configuration.Clone();
					for (int i = 0; i < config.Strategies.Length; i++)
					{
						DeductiveSynthesis deductiveSynthesis2 = config.Strategies[i] as DeductiveSynthesis;
						if (deductiveSynthesis2 != null)
						{
							config.Strategies[i] = new DeductiveSynthesis(externLearningLogicFor, deductiveSynthesis2.Configuration);
						}
					}
					synthesisEngine = new SynthesisEngine(externLearningLogicFor.Grammar, config, null);
					deductiveSynthesis.ExternEngineCache[rule] = synthesisEngine;
				}
				InductiveConstraint inductiveConstraint = task.Spec as InductiveConstraint;
				Spec spec2;
				if (inductiveConstraint == null)
				{
					Spec spec = task.Spec;
					Func<State, State> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (State s) => rule.ApplySubstitutions(s, false));
					}
					spec2 = spec.TransformInputs(func);
				}
				else
				{
					spec2 = inductiveConstraint.TransformInputs(rule);
				}
				Spec spec3 = spec2;
				LearningTask learningTask = task.Clone(rule.Body[0], spec3);
				LearningTask learningTask2 = learningTask;
				IEnumerable<State> additionalInputs = task.AdditionalInputs;
				Func<State, State> func2;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (State s) => rule.ApplySubstitutions(s, false));
				}
				learningTask2.AdditionalInputs = additionalInputs.Select(func2).ToList<State>();
				if (learningTask.IsOrdered)
				{
					IFeature externFeature = task.TopProgramsFeature.Value.GetExternFeature(rule, 0);
					learningTask = learningTask.WithTopKRequest(task.K.Value, externFeature, task.TopProgramsFeatureOptions.OrElseDefault<IFeatureOptions>());
				}
				ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
				return ProgramSet.Join(rule, new ProgramSet[] { programSet }).Some<ProgramSet>();
			}
			return ProgramSet.Empty(task.Symbol).Some<ProgramSet>();
		}
	}
}
