using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Learning.Strategies
{
	// Token: 0x020006F2 RID: 1778
	public class DomainGuidedCBS : SynthesisStrategy<WithInputTopSpec>
	{
		// Token: 0x060026A3 RID: 9891 RVA: 0x0006CD3A File Offset: 0x0006AF3A
		public DomainGuidedCBS(DomainGuidedCBSLearningLogic learningLogic, DomainGuidedCBS.Config config = null)
			: base(new StrategyAttribute[] { StrategyAttribute.SupportsOrderedTasks })
		{
			this.LearningLogic = learningLogic;
			this.Configuration = config ?? new DomainGuidedCBS.Config();
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x0006CD63 File Offset: 0x0006AF63
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x0006CD6B File Offset: 0x0006AF6B
		public DomainGuidedCBSLearningLogic LearningLogic { get; set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x0006CD74 File Offset: 0x0006AF74
		// (set) Token: 0x060026A7 RID: 9895 RVA: 0x0006CD7C File Offset: 0x0006AF7C
		public DomainGuidedCBS.Config Configuration { get; set; }

		// Token: 0x060026A8 RID: 9896 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanCall(WithInputTopSpec spec)
		{
			return true;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x0006CD88 File Offset: 0x0006AF88
		public override Optional<ProgramSet> Learn(SynthesisEngine engine, LearningTask<WithInputTopSpec> task, CancellationToken cancel)
		{
			if (!this.Configuration.ValidStartSymbols.Contains(task.Symbol))
			{
				return Optional<ProgramSet>.Nothing;
			}
			object obj;
			return ProgramSet.List(task.Symbol, this.LearnSymbolInternal(task.Symbol, task.Spec, cancel, out obj).Memoize<ProgramNode>()).Some<ProgramSet>();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0006CDE0 File Offset: 0x0006AFE0
		private IEnumerable<ProgramNode> LearnSymbolInternal(Symbol startSymbol, WithInputTopSpec spec, CancellationToken cancel, out object executionResult)
		{
			DomainGuidedCBS.<>c__DisplayClass11_0 CS$<>8__locals1 = new DomainGuidedCBS.<>c__DisplayClass11_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.valueCache = new Dictionary<Symbol, Dictionary<object[], LearnerState>>();
			int num = spec.ProvidedInputs.Count<State>();
			IEnumerable<State> enumerable = spec.ProvidedInputs.ToArray<State>();
			CS$<>8__locals1.inputRule = startSymbol.Grammar.InputRule;
			object[] array = enumerable.Select((State s) => s[CS$<>8__locals1.inputRule.Head]).ToArray<object>();
			List<GrammarRule> list = startSymbol.DependentRules.Distinct<GrammarRule>().ToList<GrammarRule>();
			State state = State.CreateForLearning(CS$<>8__locals1.inputRule.Head, array);
			CS$<>8__locals1.valueCache.AddOrCreate(CS$<>8__locals1.inputRule.Head, array, new LearnerState(state, CS$<>8__locals1.inputRule.BuildASTNode(Array.Empty<ProgramNode>()), true, array), ObjectArrayEquality.Comparer, null);
			foreach (TerminalRule terminalRule in from r in list.OfType<TerminalRule>()
				where !r.IsInput
				select r)
			{
				if (this.Configuration.TerminalGenerators.ContainsKey(terminalRule.Head.Name))
				{
					foreach (object obj in this.Configuration.TerminalGenerators[terminalRule.Head.Name])
					{
						object[] array2 = Enumerable.Repeat<object>(obj, num).ToArray<object>();
						CS$<>8__locals1.valueCache.AddOrCreate(terminalRule.Head, array2, new LearnerState(state, terminalRule.BuildASTNode(obj), true, array2), ObjectArrayEquality.Comparer, null);
					}
				}
			}
			Dictionary<GrammarRule, NonterminalRule> ruleLiftingFunctionsMap = this.GetRuleLiftingFunctionsMap(list, startSymbol.Grammar.Rules);
			int num2 = CS$<>8__locals1.valueCache.Sum((KeyValuePair<Symbol, Dictionary<object[], LearnerState>> kvp) => kvp.Value.Count);
			int num3 = 0;
			Stopwatch stopwatch = new Stopwatch();
			for (;;)
			{
				stopwatch.Restart();
				List<LearnerState> list2 = new List<LearnerState>();
				using (IEnumerator<NonterminalRule> enumerator3 = list.OfType<NonterminalRule>().GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						DomainGuidedCBS.<>c__DisplayClass11_1 CS$<>8__locals2 = new DomainGuidedCBS.<>c__DisplayClass11_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.rule = enumerator3.Current;
						if (ruleLiftingFunctionsMap.ContainsKey(CS$<>8__locals2.rule))
						{
							this.ApplyRuleLiftingFunction(CS$<>8__locals2.rule, ruleLiftingFunctionsMap[CS$<>8__locals2.rule], CS$<>8__locals2.CS$<>8__locals1.valueCache, list2, cancel);
						}
						else
						{
							if (num3 > 0)
							{
								if (CS$<>8__locals2.rule.Body.All((Symbol s) => s.IsInput || s.IsTerminal))
								{
									continue;
								}
							}
							Dictionary<Symbol, List<LearnerState>> allArgStates = CS$<>8__locals2.rule.Body.Distinct<Symbol>().ToDictionary((Symbol p) => p, new Func<Symbol, List<LearnerState>>(CS$<>8__locals2.<LearnSymbolInternal>g__ArgumentSource|5));
							Dictionary<Symbol, List<LearnerState>> oldArgStates = CS$<>8__locals2.rule.Body.Distinct<Symbol>().ToDictionary((Symbol p) => p, (Symbol p) => allArgStates[p].Where((LearnerState l) => !l.IsNew).ToList<LearnerState>());
							Dictionary<Symbol, List<LearnerState>> newArgStates = CS$<>8__locals2.rule.Body.Distinct<Symbol>().ToDictionary((Symbol p) => p, (Symbol p) => allArgStates[p].Where((LearnerState l) => l.IsNew).ToList<LearnerState>());
							int j;
							foreach (IEnumerable<LearnerState> enumerable2 in CS$<>8__locals2.rule.Body.SelectMany((Symbol s, int i) => CS$<>8__locals2.rule.Body.Select(delegate(Symbol p, int j)
							{
								if (j < j)
								{
									return allArgStates[p];
								}
								if (j != j)
								{
									return oldArgStates[p];
								}
								return newArgStates[p];
							}).CartesianProduct<LearnerState>()))
							{
								if (cancel.IsCancellationRequested)
								{
									throw new TaskCanceledException();
								}
								LearnerState[] array3 = enumerable2.ToArray<LearnerState>();
								ProgramNode[] array4 = array3.Select((LearnerState a) => a.Program).ToArray<ProgramNode>();
								object[][] array5 = array3.Select((LearnerState a) => a.Record).ToArray<object[]>();
								object[] array6 = new object[num];
								int i;
								for (i = 0; i < num; i = j + 1)
								{
									try
									{
										array6[i] = CS$<>8__locals2.rule.Evaluate(array5.Select((object[] tuple) => tuple[i]).ToArray<object>());
									}
									catch (Exception ex)
									{
										throw new Exception("Rule application error", ex);
									}
									j = i;
								}
								Dictionary<object[], LearnerState> dictionary;
								if (!CS$<>8__locals2.CS$<>8__locals1.valueCache.TryGetValue(CS$<>8__locals2.rule.Head, out dictionary) || !dictionary.ContainsKey(array6))
								{
									bool flag;
									if (this.RuleUsedInRanking(CS$<>8__locals2.rule))
									{
										flag = array3.All((LearnerState ls) => ls.UsedInRanking);
									}
									else
									{
										flag = false;
									}
									bool flag2 = flag;
									list2.Add(new LearnerState(array3.Select((LearnerState a) => a.State).First<State>(), CS$<>8__locals2.rule.BuildASTNode(array4), flag2, array6));
								}
							}
						}
					}
				}
				foreach (KeyValuePair<Symbol, Dictionary<object[], LearnerState>> keyValuePair in CS$<>8__locals1.valueCache)
				{
					foreach (LearnerState learnerState in keyValuePair.Value.Values)
					{
						learnerState.IsNew = false;
					}
				}
				foreach (LearnerState learnerState2 in list2)
				{
					if (cancel.IsCancellationRequested)
					{
						throw new TaskCanceledException();
					}
					Symbol symbol = learnerState2.Program.Symbol;
					object[] record = learnerState2.Record;
					Dictionary<object[], LearnerState> dictionary2;
					LearnerState learnerState3;
					if (!CS$<>8__locals1.valueCache.TryGetValue(symbol, out dictionary2) || !dictionary2.TryGetValue(record, out learnerState3) || (learnerState2.UsedInRanking && !learnerState3.UsedInRanking))
					{
						CS$<>8__locals1.valueCache.AddOrCreate(learnerState2.Program.Symbol, learnerState2.Record, learnerState2, ObjectArrayEquality.Comparer, null);
					}
				}
				num3++;
				int num4 = CS$<>8__locals1.valueCache.Sum((KeyValuePair<Symbol, Dictionary<object[], LearnerState>> kvp) => kvp.Value.Count);
				if (num3 == this.Configuration.MaxIterations || num4 > this.Configuration.MaxNumStates || num4 == num2 || stopwatch.ElapsedMilliseconds > (long)this.Configuration.Timeout)
				{
					break;
				}
				num2 = num4;
			}
			this.LearningLogic.LearnedPrograms = CS$<>8__locals1.valueCache;
			IEnumerable<IEnumerable<LearnerState>> topRankedStateSet = this.GetTopRankedStateSet(startSymbol, CS$<>8__locals1.valueCache);
			executionResult = topRankedStateSet.Select((IEnumerable<LearnerState> s) => s.Select((LearnerState v) => v.Record).ToArray<object[]>()).ToArray<object[][]>();
			IEnumerable<LearnerState> enumerable3 = topRankedStateSet.FirstOrDefault<IEnumerable<LearnerState>>();
			if (enumerable3 != null)
			{
				return enumerable3.Select((LearnerState s) => s.Program);
			}
			return Enumerable.Empty<ProgramNode>();
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0006D6B0 File Offset: 0x0006B8B0
		private bool RuleUsedInRanking(NonterminalRule rule)
		{
			OperatorRule operatorRule = rule as OperatorRule;
			return operatorRule == null || !this.Configuration.NonRankingRules.Contains(operatorRule.Id);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0006D6E4 File Offset: 0x0006B8E4
		private bool IsAcceptableState(NonterminalRule rule, LearnerState learnerState)
		{
			OperatorRule operatorRule = rule as OperatorRule;
			if (operatorRule == null)
			{
				return true;
			}
			OperatorRule operatorRule2 = learnerState.Program.GrammarRule as OperatorRule;
			return operatorRule2 == null || !this.Configuration.IgnoreRuleApplications.ContainsKey(operatorRule.Id) || !this.Configuration.IgnoreRuleApplications[operatorRule.Id].Contains(operatorRule2.Id);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0006D754 File Offset: 0x0006B954
		private Dictionary<GrammarRule, NonterminalRule> GetRuleLiftingFunctionsMap(IEnumerable<GrammarRule> dependentRules, IEnumerable<GrammarRule> allRules)
		{
			Dictionary<GrammarRule, NonterminalRule> dictionary = new Dictionary<GrammarRule, NonterminalRule>();
			Dictionary<string, GrammarRule> dictionary2 = new Dictionary<string, GrammarRule>();
			foreach (BlackBoxRule blackBoxRule in dependentRules.OfType<BlackBoxRule>())
			{
				dictionary2[blackBoxRule.Name] = blackBoxRule;
			}
			foreach (BlackBoxRule blackBoxRule2 in allRules.OfType<BlackBoxRule>())
			{
				string name = blackBoxRule2.Name;
				if (name.Length > 4 && name.Substring(0, 4) == "GEN_")
				{
					string text = name.Substring(4, name.Length - 4);
					GrammarRule grammarRule;
					if (dictionary2.TryGetValue(text, out grammarRule))
					{
						dictionary[grammarRule] = blackBoxRule2;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0006D83C File Offset: 0x0006BA3C
		private IEnumerable<IEnumerable<LearnerState>> GetTopRankedStateSet(Symbol startSymbol, Dictionary<Symbol, Dictionary<object[], LearnerState>> valueCache)
		{
			Dictionary<object[], LearnerState> dictionary;
			object[][] array;
			if (!valueCache.TryGetValue(startSymbol, out dictionary))
			{
				array = new object[0][];
			}
			else
			{
				array = (from kvp in dictionary
					where kvp.Value.UsedInRanking
					select kvp.Key).ToArray<object[]>();
			}
			object[][] array2 = array;
			object[][] array3;
			if (!valueCache.TryGetValue(startSymbol, out dictionary))
			{
				array3 = new object[0][];
			}
			else
			{
				array3 = (from kvp in dictionary
					where !kvp.Value.UsedInRanking
					select kvp.Key).ToArray<object[]>();
			}
			object[][] array4 = array3;
			object[][] array5;
			if (!valueCache.TryGetValue(startSymbol.Grammar.InputSymbol, out dictionary))
			{
				array5 = new object[0][];
			}
			else
			{
				array5 = dictionary.Select((KeyValuePair<object[], LearnerState> kvp) => kvp.Key).ToArray<object[]>();
			}
			object[][] array6 = array5;
			Func<object[], LearnerState> <>9__6;
			return this.LearningLogic.Ranker(array2, array4, array6).Select(delegate(object[][] s)
			{
				Func<object[], LearnerState> func;
				if ((func = <>9__6) == null)
				{
					func = (<>9__6 = (object[] v) => valueCache[startSymbol][v]);
				}
				return s.Select(func);
			});
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0006D9A8 File Offset: 0x0006BBA8
		private void ApplyRuleLiftingFunction(NonterminalRule rule, NonterminalRule generator, Dictionary<Symbol, Dictionary<object[], LearnerState>> valueCache, List<LearnerState> newStates, CancellationToken cancel)
		{
			DomainGuidedCBS.<>c__DisplayClass16_0 CS$<>8__locals1 = new DomainGuidedCBS.<>c__DisplayClass16_0();
			CS$<>8__locals1.valueCache = valueCache;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.rule = rule;
			Symbol[] array = CS$<>8__locals1.rule.Body.ToArray<Symbol>();
			int num = array.Length;
			int i;
			int i2;
			for (i = 0; i < num; i = i2 + 1)
			{
				if (cancel.IsCancellationRequested)
				{
					throw new TaskCanceledException();
				}
				object[][][] array2 = array.Select((Symbol s, int k) => CS$<>8__locals1.<ApplyRuleLiftingFunction>g__GetLiftingFunctionArguments|0(s, (k < i) ? null : new bool?(k == i))).ToArray<object[][]>();
				if (!array2.Any((object[][] a) => a.Length == 0))
				{
					object[] array3 = array2;
					foreach (Record<object[], object[][]> record in ((List<Record<object[], object[][]>>)generator.Evaluate(array3)))
					{
						object[] item = record.Item1;
						object[][] item2 = record.Item2;
						Dictionary<object[], LearnerState> dictionary;
						if (!CS$<>8__locals1.valueCache.TryGetValue(CS$<>8__locals1.rule.Head, out dictionary) || !dictionary.ContainsKey(item))
						{
							ProgramNode[] array4 = new ProgramNode[array.Length];
							bool flag = this.RuleUsedInRanking(CS$<>8__locals1.rule);
							State state = null;
							for (int j = 0; j < item2.Length; j++)
							{
								object[] array5 = item2[j];
								Symbol symbol = array[j];
								LearnerState learnerState = CS$<>8__locals1.valueCache[symbol][array5];
								array4[j] = learnerState.Program;
								flag &= learnerState.UsedInRanking;
								state = learnerState.State;
							}
							newStates.Add(new LearnerState(state, CS$<>8__locals1.rule.BuildASTNode(array4), flag, item));
						}
					}
				}
				i2 = i;
			}
		}

		// Token: 0x020006F3 RID: 1779
		public class Config : StrategyConfig
		{
			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x060026B0 RID: 9904 RVA: 0x0006DBA0 File Offset: 0x0006BDA0
			// (set) Token: 0x060026B1 RID: 9905 RVA: 0x0006DBA8 File Offset: 0x0006BDA8
			public IDictionary<string, IEnumerable<object>> TerminalGenerators { get; set; }

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x060026B2 RID: 9906 RVA: 0x0006DBB1 File Offset: 0x0006BDB1
			// (set) Token: 0x060026B3 RID: 9907 RVA: 0x0006DBB9 File Offset: 0x0006BDB9
			public Dictionary<string, HashSet<string>> IgnoreRuleApplications { get; set; }

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0006DBC2 File Offset: 0x0006BDC2
			// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0006DBCA File Offset: 0x0006BDCA
			public HashSet<string> NonRankingRules { get; set; }

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0006DBD3 File Offset: 0x0006BDD3
			// (set) Token: 0x060026B7 RID: 9911 RVA: 0x0006DBDB File Offset: 0x0006BDDB
			public int MaxIterations { get; set; }

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0006DBE4 File Offset: 0x0006BDE4
			// (set) Token: 0x060026B9 RID: 9913 RVA: 0x0006DBEC File Offset: 0x0006BDEC
			public int MaxNumStates { get; set; } = 30000;

			// Token: 0x170006D5 RID: 1749
			// (get) Token: 0x060026BA RID: 9914 RVA: 0x0006DBF5 File Offset: 0x0006BDF5
			// (set) Token: 0x060026BB RID: 9915 RVA: 0x0006DBFD File Offset: 0x0006BDFD
			public int Timeout { get; set; } = 10000;

			// Token: 0x04001294 RID: 4756
			public HashSet<Symbol> ValidStartSymbols;
		}
	}
}
