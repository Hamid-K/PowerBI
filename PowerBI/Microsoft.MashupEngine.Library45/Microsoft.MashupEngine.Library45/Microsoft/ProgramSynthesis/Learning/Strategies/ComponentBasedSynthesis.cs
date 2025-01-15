using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x020006DA RID: 1754
	public class ComponentBasedSynthesis : SynthesisStrategy<Spec>
	{
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x00069DAA File Offset: 0x00067FAA
		private HashSet<Symbol> IgnoredSymbols { get; }

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00069DB2 File Offset: 0x00067FB2
		private IReadOnlyDictionary<TerminalRule, Func<object, IEnumerable<object>>> InputBasedLiteralGenerators { get; }

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600261B RID: 9755 RVA: 0x00069DBA File Offset: 0x00067FBA
		private Func<Spec, IEnumerable<ProgramNode>> InitialProgramsFunc { get; }

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x00069DC2 File Offset: 0x00067FC2
		private Func<ComponentBasedSynthesis.LearnerState, Spec, bool> IntermediateStateFilter { get; }

		// Token: 0x0600261D RID: 9757 RVA: 0x00069DCA File Offset: 0x00067FCA
		public ComponentBasedSynthesis(IEnumerable<Symbol> ignoredSymbols = null, Func<ComponentBasedSynthesis.LearnerState, Spec, bool> intermediateStateFilter = null, IReadOnlyDictionary<TerminalRule, Func<object, IEnumerable<object>>> inputBasedLiteralGenerators = null, Func<Spec, IEnumerable<ProgramNode>> initialProgramsFunc = null)
			: base(new StrategyAttribute[]
			{
				StrategyAttribute.WithInfiniteResults,
				StrategyAttribute.SupportsOrderedTasks
			})
		{
			this.IgnoredSymbols = ((ignoredSymbols != null) ? ignoredSymbols.ConvertToHashSet<Symbol>() : null);
			this.IntermediateStateFilter = intermediateStateFilter;
			this.InputBasedLiteralGenerators = inputBasedLiteralGenerators;
			this.InitialProgramsFunc = initialProgramsFunc;
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x00069E08 File Offset: 0x00068008
		public override Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			IEnumerable<ProgramNode> enumerable = this.LearnSymbolInternal(engine, task.Symbol, task.Spec, task.TopProgramsFeature, task.FeatureCalculationContext, cancel);
			ProgramSet programSet = ProgramSet.List(task.Symbol, enumerable.Memoize<ProgramNode>());
			if (!task.IsOrdered)
			{
				return programSet.Some<ProgramSet>();
			}
			return programSet.Prune(task.PruningRequest, task.FeatureCalculationContext, engine.RandomNumberGenerator, engine.Configuration.LogListener).Some<ProgramSet>();
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x00069E7F File Offset: 0x0006807F
		private IEnumerable<ProgramNode> LearnSymbolInternal(SynthesisEngine engine, Symbol startSymbol, Spec spec, Optional<IFeature> topProgramsFeature, FeatureCalculationContext featureCalculationContext, CancellationToken cancel)
		{
			ComponentBasedSynthesis.<>c__DisplayClass15_0 CS$<>8__locals1 = new ComponentBasedSynthesis.<>c__DisplayClass15_0();
			CS$<>8__locals1.topProgramsFeature = topProgramsFeature;
			CS$<>8__locals1.featureCalculationContext = featureCalculationContext;
			CS$<>8__locals1.<>4__this = this;
			HashSet<Symbol> ignoredSymbols = this.IgnoredSymbols;
			if (ignoredSymbols != null && ignoredSymbols.Contains(startSymbol))
			{
				yield break;
			}
			CS$<>8__locals1.scoreComparer = CS$<>8__locals1.topProgramsFeature.Select((IFeature _) => Comparer<object>.Default).OrElseDefault<Comparer<object>>();
			CS$<>8__locals1.valueCache = new Dictionary<Symbol, Dictionary<object[], ComponentBasedSynthesis.LearnerState>>();
			int exampleCount = spec.ProvidedInputs.Count;
			State[] inputStates = spec.ProvidedInputs.ToArray<State>();
			TerminalRule inputRule = startSymbol.Grammar.InputRule;
			CS$<>8__locals1.inputSymbol = inputRule.Head;
			object[] array = inputStates.Select((State s) => s[CS$<>8__locals1.inputSymbol]).ToArray<object>();
			List<GrammarRule> components = startSymbol.DependentRules.Distinct<GrammarRule>().Where(delegate(GrammarRule rule)
			{
				HashSet<Symbol> ignoredSymbols4 = CS$<>8__locals1.<>4__this.IgnoredSymbols;
				return ignoredSymbols4 == null || !ignoredSymbols4.Contains(rule.Head);
			}).ToList<GrammarRule>();
			CS$<>8__locals1.changedSymbols = new HashSet<Symbol>();
			int currentGeneration = 0;
			State state3 = State.CreateForLearning(CS$<>8__locals1.inputSymbol, array);
			HashSet<Symbol> ignoredSymbols2 = this.IgnoredSymbols;
			if (ignoredSymbols2 == null || !ignoredSymbols2.Contains(CS$<>8__locals1.inputSymbol))
			{
				ProgramNode programNode = inputRule.BuildASTNode(Array.Empty<ProgramNode>());
				CS$<>8__locals1.valueCache.AddOrCreate(CS$<>8__locals1.inputSymbol, array, new ComponentBasedSynthesis.LearnerState(state3, programNode, currentGeneration, CS$<>8__locals1.<LearnSymbolInternal>g__ComputeScore|0(programNode), array), ObjectArrayEquality.Comparer, null);
				CS$<>8__locals1.changedSymbols.Add(CS$<>8__locals1.inputSymbol);
			}
			Func<Spec, IEnumerable<ProgramNode>> initialProgramsFunc = this.InitialProgramsFunc;
			foreach (ProgramNode programNode2 in (((initialProgramsFunc != null) ? initialProgramsFunc(spec) : null) ?? Enumerable.Empty<ProgramNode>()))
			{
				ComponentBasedSynthesis.LearnerState learnerState = new ComponentBasedSynthesis.LearnerState(state3, programNode2, currentGeneration, CS$<>8__locals1.<LearnSymbolInternal>g__ComputeScore|0(programNode2), inputStates.Select(new Func<State, object>(programNode2.Invoke)).ToArray<object>());
				CS$<>8__locals1.valueCache.AddOrCreate(programNode2.Symbol, learnerState.Values, learnerState, ObjectArrayEquality.Comparer, null);
				CS$<>8__locals1.changedSymbols.Add(programNode2.Symbol);
			}
			foreach (TerminalRule terminalRule in from r in components.OfType<TerminalRule>()
				where !r.IsInput
				select r)
			{
				LiteralGenerator literalGenerator = engine.BindingManager.Generator(terminalRule);
				HashSet<Symbol> ignoredSymbols3 = this.IgnoredSymbols;
				if (ignoredSymbols3 == null || !ignoredSymbols3.Contains(terminalRule.Head))
				{
					CS$<>8__locals1.changedSymbols.Add(terminalRule.Head);
					IEnumerable<object> enumerable = ((literalGenerator != null) ? literalGenerator() : null) ?? Enumerable.Empty<object>();
					Func<object, IEnumerable<object>> func;
					if (this.InputBasedLiteralGenerators != null && this.InputBasedLiteralGenerators.TryGetValue(terminalRule, out func))
					{
						enumerable = enumerable.Union(array.Select(func).Intersect<object>());
					}
					foreach (object obj in enumerable)
					{
						object[] array2 = Enumerable.Repeat<object>(obj, exampleCount).ToArray<object>();
						ProgramNode programNode3 = terminalRule.BuildASTNode(obj);
						CS$<>8__locals1.valueCache.AddOrCreate(terminalRule.Head, array2, new ComponentBasedSynthesis.LearnerState(state3, programNode3, currentGeneration, CS$<>8__locals1.<LearnSymbolInternal>g__ComputeScore|0(programNode3), array2), ObjectArrayEquality.Comparer, null);
					}
				}
			}
			do
			{
				ComponentBasedSynthesis.<>c__DisplayClass15_2 CS$<>8__locals2 = new ComponentBasedSynthesis.<>c__DisplayClass15_2();
				CS$<>8__locals2.CS$<>8__locals2 = CS$<>8__locals1;
				CS$<>8__locals2.previousGeneration = currentGeneration;
				int num = currentGeneration;
				currentGeneration = num + 1;
				List<ComponentBasedSynthesis.LearnerState> list = new List<ComponentBasedSynthesis.LearnerState>();
				foreach (NonterminalRule nonterminalRule in components.OfType<NonterminalRule>())
				{
					IEnumerable<Symbol> body = nonterminalRule.Body;
					Func<Symbol, bool> func2;
					if ((func2 = CS$<>8__locals2.CS$<>8__locals2.<>9__6) == null)
					{
						func2 = (CS$<>8__locals2.CS$<>8__locals2.<>9__6 = (Symbol bodySymbol) => !CS$<>8__locals2.CS$<>8__locals2.changedSymbols.Contains(bodySymbol));
					}
					if (!body.All(func2))
					{
						foreach (IEnumerable<ComponentBasedSynthesis.LearnerState> enumerable2 in nonterminalRule.Body.Select(new Func<Symbol, ComponentBasedSynthesis.LearnerState[]>(CS$<>8__locals2.CS$<>8__locals2.<LearnSymbolInternal>g__ArgumentSource|7)).CartesianProduct<ComponentBasedSynthesis.LearnerState>())
						{
							if (cancel.IsCancellationRequested)
							{
								throw new TaskCanceledException();
							}
							ComponentBasedSynthesis.LearnerState[] array3 = enumerable2.ToArray<ComponentBasedSynthesis.LearnerState>();
							IEnumerable<ComponentBasedSynthesis.LearnerState> enumerable3 = array3;
							Func<ComponentBasedSynthesis.LearnerState, bool> func3;
							if ((func3 = CS$<>8__locals2.<>9__8) == null)
							{
								func3 = (CS$<>8__locals2.<>9__8 = (ComponentBasedSynthesis.LearnerState arg) => arg.Generation < CS$<>8__locals2.previousGeneration);
							}
							if (!enumerable3.All(func3))
							{
								ProgramNode[] array4 = array3.Select((ComponentBasedSynthesis.LearnerState a) => a.Program).ToArray<ProgramNode>();
								object[][] array5 = array3.Select((ComponentBasedSynthesis.LearnerState a) => a.Values).ToArray<object[]>();
								Record<State, object>[] array6 = array3.Select((ComponentBasedSynthesis.LearnerState a) => a.State).ToArray<State>().ZipWith(array5.Cast<object>())
									.ToArray<Record<State, object>>();
								State state2 = nonterminalRule.ValidStateFromArgumentInvocations(array6);
								if (state2 != null)
								{
									object[] array7 = new object[exampleCount];
									int i;
									for (i = 0; i < exampleCount; i = num + 1)
									{
										array7[i] = nonterminalRule.Evaluate(array5.Select((object[] tuple) => tuple[i]).ToArray<object>());
										num = i;
									}
									if (!array7.All((object val) => val == null))
									{
										ProgramNode programNode4 = nonterminalRule.BuildASTNode(array4);
										object obj2 = CS$<>8__locals2.CS$<>8__locals2.<LearnSymbolInternal>g__ComputeScore|0(programNode4);
										Dictionary<object[], ComponentBasedSynthesis.LearnerState> dictionary;
										ComponentBasedSynthesis.LearnerState learnerState2;
										if (CS$<>8__locals2.CS$<>8__locals2.valueCache.TryGetValue(nonterminalRule.Head, out dictionary) && dictionary.TryGetValue(array7, out learnerState2))
										{
											Comparer<object> scoreComparer = CS$<>8__locals2.CS$<>8__locals2.scoreComparer;
											if (scoreComparer == null || scoreComparer.Compare(learnerState2.Score, obj2) >= 0)
											{
												continue;
											}
										}
										list.Add(new ComponentBasedSynthesis.LearnerState(state2, programNode4, currentGeneration, obj2, array7));
									}
								}
							}
						}
					}
				}
				CS$<>8__locals2.CS$<>8__locals2.changedSymbols.Clear();
				using (List<ComponentBasedSynthesis.LearnerState>.Enumerator enumerator6 = list.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						ComponentBasedSynthesis.LearnerState state = enumerator6.Current;
						if (cancel.IsCancellationRequested)
						{
							throw new TaskCanceledException();
						}
						Func<ComponentBasedSynthesis.LearnerState, Spec, bool> intermediateStateFilter = this.IntermediateStateFilter;
						if ((intermediateStateFilter == null || intermediateStateFilter(state, spec)) && CS$<>8__locals2.CS$<>8__locals2.valueCache.AddOrCreate(state.Program.Symbol, state.Values, state, ObjectArrayEquality.Comparer, (CS$<>8__locals2.CS$<>8__locals2.scoreComparer == null) ? null : new Func<ComponentBasedSynthesis.LearnerState, bool>((ComponentBasedSynthesis.LearnerState existing) => CS$<>8__locals2.CS$<>8__locals2.scoreComparer.Compare(state.Score, existing.Score) > 0)))
						{
							CS$<>8__locals2.CS$<>8__locals2.changedSymbols.Add(state.Program.Symbol);
							if (state.Program.Symbol == startSymbol && inputStates.ZipWith(state.Values).All(new Func<Record<State, object>, bool>(spec.Valid)))
							{
								yield return state.Program;
							}
						}
					}
				}
				List<ComponentBasedSynthesis.LearnerState>.Enumerator enumerator6 = default(List<ComponentBasedSynthesis.LearnerState>.Enumerator);
				CS$<>8__locals2 = null;
			}
			while (CS$<>8__locals1.changedSymbols.Any<Symbol>());
			yield break;
			yield break;
		}

		// Token: 0x020006DB RID: 1755
		public class LearnerState
		{
			// Token: 0x06002620 RID: 9760 RVA: 0x00069EBC File Offset: 0x000680BC
			public LearnerState(State state, ProgramNode program, int generation, object score, params object[] values)
			{
				this.State = state;
				this.Values = values;
				this.Program = program;
				this.Generation = generation;
				this.Score = score;
			}

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x06002621 RID: 9761 RVA: 0x00069EE9 File Offset: 0x000680E9
			public State State { get; }

			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x06002622 RID: 9762 RVA: 0x00069EF1 File Offset: 0x000680F1
			public object[] Values { get; }

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x06002623 RID: 9763 RVA: 0x00069EF9 File Offset: 0x000680F9
			public ProgramNode Program { get; }

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06002624 RID: 9764 RVA: 0x00069F01 File Offset: 0x00068101
			public int Generation { get; }

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06002625 RID: 9765 RVA: 0x00069F09 File Offset: 0x00068109
			public object Score { get; }

			// Token: 0x06002626 RID: 9766 RVA: 0x00069F14 File Offset: 0x00068114
			public override string ToString()
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0} => {1}", new object[]
				{
					this.Program,
					this.Values.DumpCollection(ObjectFormatting.Literal, "[", "]", ", ", null)
				}));
			}
		}
	}
}
