using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x020006E3 RID: 1763
	public class DeductiveSynthesis : SynthesisStrategy<Spec>
	{
		// Token: 0x06002646 RID: 9798 RVA: 0x0006ABEB File Offset: 0x00068DEB
		internal DeductiveSynthesis(DeductiveSynthesis.Config config = null)
			: this(null, config)
		{
		}

		// Token: 0x06002647 RID: 9799 RVA: 0x0006ABF5 File Offset: 0x00068DF5
		public DeductiveSynthesis(DomainLearningLogic learningLogic, DeductiveSynthesis.Config config = null)
		{
			StrategyAttribute[] array = new StrategyAttribute[2];
			array[0] = StrategyAttribute.SupportsOrderedTasks;
			base..ctor(array);
			this.LearningLogic = learningLogic;
			this.Configuration = config ?? new DeductiveSynthesis.Config();
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x0006AC34 File Offset: 0x00068E34
		// (set) Token: 0x06002649 RID: 9801 RVA: 0x0006AC3C File Offset: 0x00068E3C
		public DeductiveSynthesis.Config Configuration { get; set; }

		// Token: 0x0600264A RID: 9802 RVA: 0x0006AC48 File Offset: 0x00068E48
		public override void Initialize(SynthesisEngine engine)
		{
			DeductiveSynthesis.WitnessingConfiguration witnessingConfiguration;
			if (DeductiveSynthesis._witnessingConfigurations.Lookup(Record.Create<Grammar, DomainLearningLogic>(engine.Grammar, this.LearningLogic), out witnessingConfiguration))
			{
				this._witnessingPlansByRuleType = witnessingConfiguration.WitnessingPlansByRuleType;
				this._ruleLearners = witnessingConfiguration.RuleLearners;
				return;
			}
			var enumerable = from t in typeof(SynthesisEngine).GetTypeInfo().Assembly.GetTypes()
				let ti = t.GetTypeInfo()
				where ti.IsClass && t.Namespace == typeof(StdWitnessingPlan<, >).Namespace && !ti.IsAbstract && !ti.ContainsGenericParameters && typeof(IWitnessingPlan).IsAssignableFrom(t)
				let plan = Activator.CreateInstance(t) as IWitnessingPlan
				group plan by plan.RuleType into g
				select new
				{
					RuleType = g.Key,
					Plans = g.OrderBy((IWitnessingPlan x) => x.SpecType, MoreSpecificTypeComparer.Instance)
				};
			this._witnessingPlansByRuleType = enumerable.ToDictionary(g => g.RuleType, g => g.Plans.ToList<IWitnessingPlan>());
			this._ruleLearners = new Dictionary<GrammarRule, List<RuleLearner>>();
			if (this.LearningLogic != null)
			{
				foreach (var <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType in from method in this.LearningLogic.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
					from attr in method.GetCustomAttributes<RuleLearnerAttribute>()
					from rule in engine.Grammar.Rules
					where attr.RuleName == rule.Id
					select new
					{
						Rule = rule,
						Learner = (method.IsStatic ? new RuleLearner.Static(method, rule) : new RuleLearner.Instance(method, rule, this.LearningLogic))
					})
				{
					this._ruleLearners.AddOrCreate(<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.Rule, <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType.Learner);
				}
			}
			foreach (var <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType2 in from t in typeof(SynthesisEngine).GetTypeInfo().Assembly.GetTypes()
				let ti = t.GetTypeInfo()
				where ti.IsClass && t.Namespace == typeof(IRuleLearner).Namespace && !ti.IsAbstract
				let args = t.InheritsGeneric(typeof(StdRuleLearner<, >))
				where args != null
				from r in engine.Grammar.Rules.Concat(engine.Grammar.GrammarReferences.Values.SelectMany((Grammar g) => g.Rules))
				where args[0].IsInstanceOfType(r)
				let learner = Activator.CreateInstance(t, new object[] { r }) as RuleLearner
				group learner by r into g
				select new
				{
					Rule = g.Key,
					Learners = g.OrderBy((RuleLearner l) => l.SpecType, MoreSpecificTypeComparer.Instance)
				})
			{
				foreach (RuleLearner ruleLearner in <649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType2.Learners)
				{
					this._ruleLearners.AddOrCreate(<649b79c8-248c-43f5-b831-d641d0649c39><>f__AnonymousType2.Rule, ruleLearner);
				}
			}
			DeductiveSynthesis._witnessingConfigurations.Add(Record.Create<Grammar, DomainLearningLogic>(engine.Grammar, this.LearningLogic), new DeductiveSynthesis.WitnessingConfiguration(this._ruleLearners, this._witnessingPlansByRuleType));
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x0006B138 File Offset: 0x00069338
		private Record<ProgramSet, string>? LearnFromRuleLearners(ILanguage language, SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			List<RuleLearner> list = null;
			GrammarRule grammarRule = null;
			GrammarRule grammarRule2 = language as GrammarRule;
			if (grammarRule2 == null)
			{
				IJoinLanguage joinLanguage = language as IJoinLanguage;
				if (joinLanguage != null)
				{
					this._ruleLearners.TryGetValue(joinLanguage.LanguageRule, out list);
					grammarRule = joinLanguage.LanguageRule;
				}
			}
			else
			{
				this._ruleLearners.TryGetValue(grammarRule2, out list);
				grammarRule = grammarRule2;
			}
			if (list == null)
			{
				return null;
			}
			foreach (RuleLearner ruleLearner in list)
			{
				if (ruleLearner != null && ruleLearner.CanCall(grammarRule, task.Spec))
				{
					string name = ruleLearner.GetType().Name;
					Optional<ProgramSet> optional = ruleLearner.LearnRule(engine, grammarRule, task, cancel);
					if (optional.HasValue)
					{
						ProgramSet programSet = optional.Value;
						if (!ProgramSet.IsNullOrEmpty(programSet))
						{
							programSet = language.Intersect(programSet);
							if (task.RequiresPruning)
							{
								programSet = programSet.Prune(task.PruningRequest, task.FeatureCalculationContext, engine.RandomNumberGenerator, engine.Configuration.LogListener);
							}
						}
						return new Record<ProgramSet, string>?(Record.Create<ProgramSet, string>(programSet, name));
					}
				}
			}
			return null;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x0006B288 File Offset: 0x00069488
		private Optional<ProgramSet> LearnFromAlternatives(IAlternatingLanguage language, SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			DeductiveSynthesis.<>c__DisplayClass17_0 CS$<>8__locals1 = new DeductiveSynthesis.<>c__DisplayClass17_0();
			CS$<>8__locals1.cancel = cancel;
			CS$<>8__locals1.engine = engine;
			CS$<>8__locals1.task = task;
			CS$<>8__locals1.<>4__this = this;
			DomainLearningLogic learningLogic = this.LearningLogic;
			return (((learningLogic != null) ? learningLogic.TacticFor(language.LanguageSymbol) : null) ?? StdTactic.Instance).LearnAlternative(language, new Func<ILanguage, ProgramSet>(CS$<>8__locals1.<LearnFromAlternatives>g__WithAlternative|0));
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x0006B2EC File Offset: 0x000694EC
		public ProgramSet LearnFromDirect(IDirectSetLanguage directSet, LearningTask task, SynthesisEngine engine)
		{
			IEnumerable<ProgramNode> enumerable = directSet.AllElements.Where(new Func<ProgramNode, bool>(task.Spec.CorrectOnAllProvided));
			return ProgramSet.List(directSet.LanguageSymbol, enumerable);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x0006B322 File Offset: 0x00069522
		private ProgramSet PruneIfNeeded(ProgramSet result, LearningTask task, SynthesisEngine engine)
		{
			if (task.RequiresPruning && result != null)
			{
				return result.Prune(task.PruningRequest, task.FeatureCalculationContext, engine.RandomNumberGenerator, engine.Configuration.LogListener);
			}
			return result;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x0006B354 File Offset: 0x00069554
		private Optional<ProgramSet> PruneIfNeeded(Optional<ProgramSet> result, LearningTask task, SynthesisEngine engine)
		{
			return result.Select((ProgramSet ps) => this.PruneIfNeeded(ps, task, engine));
		}

		// Token: 0x06002650 RID: 9808 RVA: 0x0006B390 File Offset: 0x00069590
		public override Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			if (task.Spec.ProvidedInputs.IsEmpty<State>())
			{
				return this.PruneIfNeeded(task.Symbol.TryGetAllPrograms(true, true), task, engine);
			}
			if (task.Symbol.IsVariable)
			{
				Spec specWithNulls = task.Spec.BottomToNull();
				if (specWithNulls.ProvidedInputs.Any(delegate(State input)
				{
					object obj;
					return !input.TryGetValue(task.Symbol, out obj) || !specWithNulls.Valid(input, obj);
				}))
				{
					return this.PruneIfNeeded(ProgramSet.Empty(task.Symbol), task, engine).Some<ProgramSet>();
				}
				return this.PruneIfNeeded(ProgramSet.List(task.Symbol, new ProgramNode[]
				{
					new VariableNode(task.Symbol)
				}), task, engine).Some<ProgramSet>();
			}
			else
			{
				ILanguage language = task.Language;
				IAlternatingLanguage alternatingLanguage = language as IAlternatingLanguage;
				if (alternatingLanguage != null)
				{
					return this.PruneIfNeeded(this.LearnFromAlternatives(alternatingLanguage, engine, task, cancel), task, engine);
				}
				IJoinLanguage joinLanguage = language as IJoinLanguage;
				if (joinLanguage != null)
				{
					return this.PruneIfNeeded(this.LearnFromJoin(joinLanguage, engine, task, cancel), task, engine);
				}
				IDirectSetLanguage directSetLanguage = language as IDirectSetLanguage;
				if (directSetLanguage == null)
				{
					throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Could not handle language of type: \"{0}\" in call to DeductiveSynthesis.Learn()", new object[] { task.Language.GetType() })));
				}
				return this.PruneIfNeeded(this.LearnFromDirect(directSetLanguage, task, engine).Some<ProgramSet>(), task, engine);
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x0006B544 File Offset: 0x00069744
		private Record<Dictionary<int, List<int>>, List<int>>? BuildLearningOrderForPlan(WitnessFunction[] planWitnessFunctions)
		{
			int num = planWitnessFunctions.Length;
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			foreach (int num2 in Enumerable.Range(0, num).AppendItem(-1))
			{
				dictionary[num2] = new List<int>();
			}
			for (int i = 0; i < num; i++)
			{
				if (planWitnessFunctions[i].Prerequisites.Length == 0)
				{
					dictionary[-1].Add(i);
				}
				else
				{
					foreach (WitnessFunction.PrereqInfo prereqInfo in planWitnessFunctions[i].Prerequisites)
					{
						if (Seq.Of<Type>(new Type[]
						{
							planWitnessFunctions[prereqInfo.ParamIndex].ReturnSpecType,
							typeof(ExampleSpec)
						}).Any(new Func<Type, bool>(prereqInfo.SatisfiedBy)))
						{
							dictionary[prereqInfo.ParamIndex].Add(i);
						}
					}
				}
			}
			if (!dictionary.ReachableFrom(-1).IsSupersetOf(Enumerable.Range(0, num).ConvertToHashSet<int>()))
			{
				return null;
			}
			List<int> list = dictionary.TopologicalSort<int, List<int>>();
			if (list != null)
			{
				return new Record<Dictionary<int, List<int>>, List<int>>?(Record.Create<Dictionary<int, List<int>>, List<int>>(dictionary, list));
			}
			return null;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x0006B698 File Offset: 0x00069898
		private Optional<ProgramSet> LearnFromJoin(IJoinLanguage language, SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			Spec spec = task.Spec;
			NonterminalRule rule = language.LanguageRule;
			Record<NonterminalRule, Spec> record = Record.Create<NonterminalRule, Spec>(rule, spec);
			IList<WitnessingPlan> list = null;
			Dictionary<Record<NonterminalRule, Spec>, IList<WitnessingPlan>> applicablePlanCache = this._applicablePlanCache;
			lock (applicablePlanCache)
			{
				list = this._applicablePlanCache.GetOrAdd(record, (Record<NonterminalRule, Spec> _) => this.FindMostApplicablePlans(rule, spec));
			}
			int count = rule.Body.Count;
			bool flag2 = rule.Body.All((Symbol p) => p.IsVariable);
			foreach (WitnessingPlan witnessingPlan in list)
			{
				if (cancel.IsCancellationRequested)
				{
					throw new TaskCanceledException();
				}
				WitnessFunction[] witnessFunctions = witnessingPlan.WitnessFunctions;
				bool flag3 = witnessFunctions.Any((WitnessFunction f) => f.Prerequisites.Length != 0);
				bool flag4;
				if (!flag2)
				{
					flag4 = witnessFunctions.Any((WitnessFunction f) => f.Verify);
				}
				else
				{
					flag4 = true;
				}
				bool flag5 = flag4;
				LogListener logListener = engine.Configuration.LogListener;
				if (logListener != null)
				{
					logListener.EnterEvent(engine.LogEvent.StartedUsePlan(witnessingPlan, flag3, flag5));
				}
				if (flag3)
				{
					Record<Dictionary<int, List<int>>, List<int>>? record2 = this.BuildLearningOrderForPlan(witnessFunctions);
					if (record2 == null)
					{
						LogListener logListener2 = engine.Configuration.LogListener;
						if (logListener2 != null)
						{
							logListener2.ExitEvent();
						}
					}
					else
					{
						Dictionary<int, List<int>> item = record2.Value.Item1;
						List<int> item2 = record2.Value.Item2;
						IEnumerable<ProgramSet> enumerable = this.LearnFromDependencies(witnessingPlan, item2, 0, item, ImmutableDictionary.Create<int, KeyValuePair<Spec, ProgramSet>>(), engine, cancel, language, task);
						LogListener logListener3 = engine.Configuration.LogListener;
						if (logListener3 != null)
						{
							logListener3.ExitEvent();
						}
						ProgramSet programSet = this.PostprocessResult(task, flag5, enumerable.NormalizedUnion(), engine.RandomNumberGenerator, engine.Configuration.LogListener);
						if (!ProgramSet.IsNullOrEmpty(programSet))
						{
							return programSet.Some<ProgramSet>();
						}
					}
				}
				else
				{
					ProgramSet[] array = new ProgramSet[count];
					bool flag6 = true;
					for (int i = 0; i < count; i++)
					{
						if (rule.RecursionLimit[i].OrElse(2147483647) < task.RecursionDepths.GetValueOrDefault(rule.Body[i], 0))
						{
							flag6 = false;
							break;
						}
						LogListener logListener4 = engine.Configuration.LogListener;
						if (logListener4 != null)
						{
							logListener4.EnterEvent(engine.LogEvent.Witness(rule, i, witnessFunctions[i], Array.Empty<Spec>()));
						}
						Spec spec2 = witnessFunctions[i].ConstructWitness(rule, spec.BottomToNull(), Array.Empty<Spec>());
						LogListener logListener5 = engine.Configuration.LogListener;
						if (logListener5 != null)
						{
							logListener5.CurrentEvent.Add("witness", spec2);
						}
						LogListener logListener6 = engine.Configuration.LogListener;
						if (logListener6 != null)
						{
							logListener6.ExitEvent();
						}
						if (spec2 == null)
						{
							flag6 = false;
							break;
						}
						LearningTask learningTask = task.MakeSubtask(language, i, spec2.NullToBottom());
						ProgramSet programSet2 = engine.Learn(learningTask, cancel);
						if (ProgramSet.IsNullOrEmpty(programSet2))
						{
							flag6 = false;
							break;
						}
						array[i] = programSet2;
					}
					LogListener logListener7 = engine.Configuration.LogListener;
					if (logListener7 != null)
					{
						logListener7.ExitEvent();
					}
					if (flag6)
					{
						ProgramSet programSet3 = this.PostprocessResult(task, flag5, ProgramSet.Join(rule, array), engine.RandomNumberGenerator, engine.Configuration.LogListener);
						if (!ProgramSet.IsNullOrEmpty(programSet3))
						{
							return programSet3.Some<ProgramSet>();
						}
					}
				}
			}
			return ProgramSet.Empty(rule.Head).Some<ProgramSet>();
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x0006BAB8 File Offset: 0x00069CB8
		internal IList<WitnessingPlan> FindMostApplicablePlans(GrammarRule rule, Spec spec)
		{
			HashSet<WitnessFunction>[] array = rule.Body.Select((Symbol p, int i) => rule.WitnessFunctionsFor(i, spec, this.LearningLogic).ConvertToHashSet<WitnessFunction>()).ToArray<HashSet<WitnessFunction>>();
			Type type = rule.GetType();
			while (type != null)
			{
				List<IWitnessingPlan> list;
				if (this._witnessingPlansByRuleType.TryGetValue(type, out list))
				{
					foreach (IWitnessingPlan witnessingPlan in list)
					{
						if (witnessingPlan.CanCall(rule, spec))
						{
							for (int l = 0; l < array.Length; l++)
							{
								WitnessFunction witnessFunction = witnessingPlan.PreferredWitnessFunctionFor(rule, l, spec);
								if (witnessFunction != null)
								{
									array[l].Add(witnessFunction);
								}
							}
						}
					}
				}
				type = type.GetTypeInfo().BaseType;
			}
			WitnessingPlan[] array2 = (from c in array.CartesianProduct<WitnessFunction>()
				select new WitnessingPlan(c, null)).ToArray<WitnessingPlan>();
			if (array2.Length == 0)
			{
				return new List<WitnessingPlan>();
			}
			List<WitnessingPlan> list2 = new List<WitnessingPlan> { array2[0] };
			for (int j = 1; j < array2.Length; j++)
			{
				WitnessingPlan witnessingPlan2 = array2[j];
				bool flag = false;
				bool flag2 = false;
				for (int k = 0; k < list2.Count; k++)
				{
					WitnessingPlan.CompareResult compareResult = witnessingPlan2.CompareTo(list2[k]);
					if (compareResult != WitnessingPlan.CompareResult.EquallyPreferred && compareResult != WitnessingPlan.CompareResult.Incomparable)
					{
						if (compareResult == WitnessingPlan.CompareResult.LessPreferred)
						{
							flag = true;
							break;
						}
						if (flag2)
						{
							list2.RemoveAt(k);
						}
						else
						{
							flag2 = true;
							list2[k] = witnessingPlan2;
						}
						flag = true;
					}
				}
				if (!flag)
				{
					list2.Add(witnessingPlan2);
				}
			}
			return list2;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x0006BC98 File Offset: 0x00069E98
		private ProgramSet PostprocessResult(LearningTask<Spec> task, bool needsVerification, ProgramSet result, Random random, LogListener logListener)
		{
			if (ProgramSet.IsNullOrEmpty(result))
			{
				return result;
			}
			if (needsVerification)
			{
				result = result.Filter(task.Spec);
			}
			if (ProgramSet.IsNullOrEmpty(result))
			{
				return result;
			}
			if (task.RequiresPruning)
			{
				result = result.Prune(task.PruningRequest, task.FeatureCalculationContext, random, logListener);
			}
			return result;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x0006BCEC File Offset: 0x00069EEC
		private LearningTask FixupPruningRequestsForParamTask(LearningTask mainTask, LearningTask paramTask, DeductiveSynthesis.ParamDependencyType dependencyType)
		{
			if (dependencyType == DeductiveSynthesis.ParamDependencyType.NoDependants)
			{
				return paramTask;
			}
			if (mainTask.K.HasValue)
			{
				paramTask = paramTask.WithTopKPrograms(this.Configuration.PrereqTopProgramsThreshold(mainTask.K.Value));
			}
			if (mainTask.RandomK.HasValue)
			{
				paramTask = paramTask.WithRandomKPrograms(this.Configuration.PrereqRandomProgramsThreshold(mainTask.RandomK.Value));
			}
			return paramTask;
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x0006BD6C File Offset: 0x00069F6C
		private IEnumerable<ProgramSet> LearnFromDependencies(WitnessingPlan plan, List<int> order, int current, Dictionary<int, List<int>> graph, ImmutableDictionary<int, KeyValuePair<Spec, ProgramSet>> prereqsBindings, SynthesisEngine engine, CancellationToken cancel, IJoinLanguage language, LearningTask<Spec> task)
		{
			NonterminalRule languageRule = language.LanguageRule;
			if (current == order.Count)
			{
				ProgramSet[] array = (from i in Enumerable.Range(0, language.JoinedLanguages.Count<ILanguage>())
					select prereqsBindings[i].Value).ToArray<ProgramSet>();
				ProgramSet programSet = ProgramSet.Join(languageRule, array);
				return Seq.Of<ProgramSet>(new ProgramSet[] { programSet });
			}
			int pi = order[current];
			if (pi == -1)
			{
				return this.LearnFromDependencies(plan, order, current + 1, graph, prereqsBindings, engine, cancel, language, task);
			}
			if (language is GrammarRule && languageRule.RecursionLimit[pi].OrElse(2147483647) < task.RecursionDepths.GetValueOrDefault(languageRule.Body[pi], 0))
			{
				return DeductiveSynthesis.NoPrograms;
			}
			WitnessFunction witnessFunction = plan.WitnessFunctions[pi];
			Spec[] array2 = witnessFunction.Prerequisites.Select((WitnessFunction.PrereqInfo q) => prereqsBindings[q.ParamIndex].Key).ToArray<Spec>();
			LogListener logListener = engine.Configuration.LogListener;
			if (logListener != null)
			{
				logListener.EnterEvent(engine.LogEvent.Witness(languageRule, pi, witnessFunction, array2));
			}
			Spec paramSpec = witnessFunction.ConstructWitness(languageRule, task.Spec.BottomToNull(), array2);
			LogListener logListener2 = engine.Configuration.LogListener;
			if (logListener2 != null)
			{
				logListener2.CurrentEvent.Add("witness", paramSpec);
			}
			LogListener logListener3 = engine.Configuration.LogListener;
			if (logListener3 != null)
			{
				logListener3.ExitEvent();
			}
			if (paramSpec == null)
			{
				return DeductiveSynthesis.NoPrograms;
			}
			bool flag = graph[pi].All((int p) => plan.WitnessFunctions[p].SatisfiedByPrerequisite(pi, paramSpec));
			DeductiveSynthesis.ParamDependencyType paramDependencyType = ((graph[pi].Count == 0) ? DeductiveSynthesis.ParamDependencyType.NoDependants : (flag ? DeductiveSynthesis.ParamDependencyType.UseWitness : DeductiveSynthesis.ParamDependencyType.Cluster));
			LearningTask learningTask = this.FixupPruningRequestsForParamTask(task, task.MakeSubtask(language, pi, paramSpec.NullToBottom()), paramDependencyType);
			ProgramSet programSet2 = engine.Learn(learningTask, cancel);
			if (ProgramSet.IsNullOrEmpty(programSet2))
			{
				return DeductiveSynthesis.NoPrograms;
			}
			switch (paramDependencyType)
			{
			case DeductiveSynthesis.ParamDependencyType.NoDependants:
			case DeductiveSynthesis.ParamDependencyType.UseWitness:
			{
				ImmutableDictionary<int, KeyValuePair<Spec, ProgramSet>> immutableDictionary = prereqsBindings.Add(pi, new KeyValuePair<Spec, ProgramSet>(paramSpec, programSet2));
				return this.LearnFromDependencies(plan, order, current + 1, graph, immutableDictionary, engine, cancel, language, task).Memoize<ProgramSet>();
			}
			case DeductiveSynthesis.ParamDependencyType.Cluster:
			{
				Dictionary<object[], ProgramSet> dictionary = programSet2.ClusterOnInputTuple(task.Spec.ProvidedInputs);
				List<IEnumerable<ProgramSet>> list = new List<IEnumerable<ProgramSet>>(dictionary.Count);
				foreach (KeyValuePair<object[], ProgramSet> keyValuePair in dictionary)
				{
					LogListener logListener4 = engine.Configuration.LogListener;
					if (logListener4 != null)
					{
						logListener4.EnterEvent(engine.LogEvent.Cluster(keyValuePair, list.Count + 1, languageRule, pi));
					}
					LogListener logListener5 = engine.Configuration.LogListener;
					IFeature feature = ((logListener5 != null) ? logListener5.ScoreFeature : null) ?? task.TopProgramsFeature.OrElseDefault<IFeature>();
					if (engine.Configuration.LogListener != null && feature != null && !ProgramSet.IsNullOrEmpty(keyValuePair.Value) && engine.Configuration.LogListener.Includes(LogInfo.Cluster) && engine.Configuration.LogListener.Includes(LogInfo.BestProgram))
					{
						ProgramNode programNode = keyValuePair.Value.TopK(feature, 1, null, null).FirstOrDefault<ProgramNode>();
						engine.Configuration.LogListener.CurrentEvent["clusterBestProgram"] = programNode;
						engine.Configuration.LogListener.CurrentEvent["clusterBestScore"] = ((programNode != null) ? programNode.GetFeatureValue(feature, null) : null);
					}
					ExampleSpec exampleSpec = new ExampleSpec(task.Spec.ProvidedInputs.ZipWith(keyValuePair.Key).ToDictionary<State, object>());
					ImmutableDictionary<int, KeyValuePair<Spec, ProgramSet>> immutableDictionary2 = prereqsBindings.Add(pi, new KeyValuePair<Spec, ProgramSet>(exampleSpec.BottomToNull(), keyValuePair.Value));
					IEnumerable<ProgramSet> enumerable = this.LearnFromDependencies(plan, order, current + 1, graph, immutableDictionary2, engine, cancel, language, task);
					list.Add(enumerable);
					LogListener logListener6 = engine.Configuration.LogListener;
					if (logListener6 != null && logListener6.Includes(LogInfo.Cluster))
					{
						EventNode currentEvent = engine.Configuration.LogListener.CurrentEvent;
						string text = "programCount";
						BigInteger bigInteger;
						if (enumerable == null)
						{
							bigInteger = BigInteger.Zero;
						}
						else
						{
							bigInteger = enumerable.Aggregate(BigInteger.Zero, (BigInteger acc, ProgramSet set) => acc + set.Size);
						}
						currentEvent[text] = bigInteger;
					}
					LogListener logListener7 = engine.Configuration.LogListener;
					if (logListener7 != null && logListener7.Includes(LogInfo.BestProgram) && feature != null)
					{
						ProgramNode programNode2 = null;
						double num = double.NegativeInfinity;
						foreach (ProgramSet programSet3 in enumerable)
						{
							ProgramNode programNode3 = ((programSet3 != null) ? programSet3.TopK(feature, 1, task.FeatureCalculationContext, null).FirstOrDefault<ProgramNode>() : null);
							double? num2 = (double?)((programNode3 != null) ? programNode3.GetFeatureValue(feature, task.FeatureCalculationContext) : null);
							if (num2 != null && num2.Value > num)
							{
								num = num2.Value;
								programNode2 = programNode3;
							}
						}
						if (num > double.NegativeInfinity)
						{
							engine.Configuration.LogListener.CurrentEvent["bestProgram"] = programNode2;
							engine.Configuration.LogListener.CurrentEvent["bestScore"] = num;
							engine.Configuration.LogListener.CurrentEvent["bestOutput"] = task.Spec.ProvidedInputs.Select(new Func<State, object>(programNode2.Invoke)).ToArray<object>();
						}
					}
					LogListener logListener8 = engine.Configuration.LogListener;
					if (logListener8 != null)
					{
						logListener8.ExitEvent();
					}
				}
				ImmutableStack<ProgramSet> immutableStack = DeductiveSynthesis.NoPrograms;
				foreach (IEnumerable<ProgramSet> enumerable2 in list)
				{
					foreach (ProgramSet programSet4 in enumerable2)
					{
						immutableStack = immutableStack.Push(programSet4);
					}
				}
				return immutableStack;
			}
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x04001242 RID: 4674
		private const int Sink = -1;

		// Token: 0x04001243 RID: 4675
		private static readonly ImmutableStack<ProgramSet> NoPrograms = ImmutableStack.Create<ProgramSet>();

		// Token: 0x04001244 RID: 4676
		private readonly Dictionary<Record<NonterminalRule, Spec>, IList<WitnessingPlan>> _applicablePlanCache = new Dictionary<Record<NonterminalRule, Spec>, IList<WitnessingPlan>>();

		// Token: 0x04001245 RID: 4677
		internal readonly Dictionary<GrammarRule, SynthesisEngine> ExternEngineCache = new Dictionary<GrammarRule, SynthesisEngine>();

		// Token: 0x04001246 RID: 4678
		private Dictionary<GrammarRule, List<RuleLearner>> _ruleLearners;

		// Token: 0x04001247 RID: 4679
		private Dictionary<Type, List<IWitnessingPlan>> _witnessingPlansByRuleType;

		// Token: 0x04001248 RID: 4680
		public readonly DomainLearningLogic LearningLogic;

		// Token: 0x0400124A RID: 4682
		private static ConcurrentLruCache<Record<Grammar, DomainLearningLogic>, DeductiveSynthesis.WitnessingConfiguration> _witnessingConfigurations = new ConcurrentLruCache<Record<Grammar, DomainLearningLogic>, DeductiveSynthesis.WitnessingConfiguration>(5, null, null, null);

		// Token: 0x020006E4 RID: 1764
		private struct WitnessingConfiguration
		{
			// Token: 0x06002658 RID: 9816 RVA: 0x0006C496 File Offset: 0x0006A696
			internal WitnessingConfiguration(Dictionary<GrammarRule, List<RuleLearner>> ruleLearners, Dictionary<Type, List<IWitnessingPlan>> witnessingPlansByRuleType)
			{
				this.RuleLearners = ruleLearners;
				this.WitnessingPlansByRuleType = witnessingPlansByRuleType;
			}

			// Token: 0x0400124B RID: 4683
			public readonly Dictionary<Type, List<IWitnessingPlan>> WitnessingPlansByRuleType;

			// Token: 0x0400124C RID: 4684
			public readonly Dictionary<GrammarRule, List<RuleLearner>> RuleLearners;
		}

		// Token: 0x020006E5 RID: 1765
		public class Config
		{
			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06002659 RID: 9817 RVA: 0x0006C4A6 File Offset: 0x0006A6A6
			// (set) Token: 0x0600265A RID: 9818 RVA: 0x0006C4AE File Offset: 0x0006A6AE
			public Func<int, int?> PrereqTopProgramsThreshold { get; set; } = (int k) => null;

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x0600265B RID: 9819 RVA: 0x0006C4B7 File Offset: 0x0006A6B7
			// (set) Token: 0x0600265C RID: 9820 RVA: 0x0006C4BF File Offset: 0x0006A6BF
			public Func<int, int?> PrereqRandomProgramsThreshold { get; set; } = (int k) => null;

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x0600265D RID: 9821 RVA: 0x0006C4C8 File Offset: 0x0006A6C8
			// (set) Token: 0x0600265E RID: 9822 RVA: 0x0006C4D0 File Offset: 0x0006A6D0
			public int RandomSeed { get; set; }
		}

		// Token: 0x020006E7 RID: 1767
		private enum ParamDependencyType
		{
			// Token: 0x04001254 RID: 4692
			NoDependants,
			// Token: 0x04001255 RID: 4693
			Cluster,
			// Token: 0x04001256 RID: 4694
			UseWitness
		}
	}
}
