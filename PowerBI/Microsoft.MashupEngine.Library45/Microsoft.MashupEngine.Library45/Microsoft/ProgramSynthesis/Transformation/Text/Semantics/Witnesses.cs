using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals;
using Microsoft.ProgramSynthesis.Conditionals.Learning;
using Microsoft.ProgramSynthesis.Conditionals.Learning.Specifications;
using Microsoft.ProgramSynthesis.Conditionals.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.DslLibrary.Dates;
using Microsoft.ProgramSynthesis.DslLibrary.Numbers;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.CustomExtraction;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Dates;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Visitors;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CD4 RID: 7380
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x0600FA30 RID: 64048 RVA: 0x00353358 File Offset: 0x00351558
		public Witnesses(Grammar grammar, Feature<double> rankingScore, IReadOnlyList<string> learningColumns = null, Witnesses.Options options = null)
			: base(grammar)
		{
			this._learningColumns = learningColumns;
			this._options = options ?? new Witnesses.Options();
			this._build = GrammarBuilders.Instance(grammar);
			this._inputsUsed = new InputsUsed(grammar);
			this._rankingScore = rankingScore;
			this._branchCount = BranchCount.Instance(grammar);
			this.AcceptableNoConcatNodeVisitorInstance = new Witnesses.AcceptableNoConcatNodeVisitor(this._build);
			this.AcceptableNoConcatSetVistorInstance = new Witnesses.AcceptableNoConcatSetVistor(this._build, this.AcceptableNoConcatNodeVisitorInstance);
		}

		// Token: 0x170029E1 RID: 10721
		// (get) Token: 0x0600FA31 RID: 64049 RVA: 0x00353400 File Offset: 0x00351600
		internal IEnumerable<string> ColumnNames
		{
			get
			{
				IEnumerable<string> learningColumns = this._learningColumns;
				IEnumerable<string> enumerable;
				if ((enumerable = learningColumns) == null)
				{
					enumerable = from i in Enumerable.Range(0, 256)
						select i.ToString(CultureInfo.InvariantCulture);
				}
				return enumerable;
			}
		}

		// Token: 0x0600FA32 RID: 64050 RVA: 0x00353448 File Offset: 0x00351648
		private ProgramSet FilterConstantPrograms(ProgramSet res)
		{
			if (!this._options.ForbidConstantProgram || (res == null || res.IsEmpty))
			{
				return res;
			}
			res = res.AcceptVisitor<Witnesses.ConstantProgramSetPartition>(new Witnesses.ConstantProgramSetFilter(this._build, this._inputsUsed)).NonConstant;
			return res;
		}

		// Token: 0x0600FA33 RID: 64051 RVA: 0x00353494 File Offset: 0x00351694
		private ProgramSetBuilder<st> FilterTopLevelProgramSet(ProgramSetBuilder<st> set)
		{
			return this._build.Set.Cast.st(this.FilterConstantPrograms((set != null) ? set.Set : null));
		}

		// Token: 0x0600FA34 RID: 64052 RVA: 0x003534C0 File Offset: 0x003516C0
		[RuleLearner("SingleBranch")]
		internal Optional<ProgramSet> LearnSingleBranch(SynthesisEngine engine, ConversionRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			bool isOutsideTopLevel = this._isOutsideTopLevel;
			this._isOutsideTopLevel = false;
			Optional<ProgramSet> optional;
			try
			{
				LearningTask learningTask = task.MakeSubtask(rule, 0, task.Spec);
				ProgramSetBuilder<st> programSetBuilder = this._build.Set.Cast.st(engine.Learn(learningTask, cancel));
				if (isOutsideTopLevel)
				{
					BigInteger bigInteger = ((programSetBuilder != null) ? programSetBuilder.Set.Size : 0);
					programSetBuilder = this.FilterTopLevelProgramSet(programSetBuilder);
					bool flag = programSetBuilder == null || programSetBuilder.Set.IsEmpty;
					int num = task.PruningRequest.K.GetValueOrDefault();
					while (bigInteger > BigInteger.Zero && flag && num > 0 && num < 10)
					{
						num = 2 * num;
						LearningTask learningTask2 = learningTask.WithTopKPrograms(new int?(num));
						programSetBuilder = this._build.Set.Cast.st(engine.Learn(learningTask2, cancel));
						BigInteger bigInteger2 = ((programSetBuilder != null) ? programSetBuilder.Set.Size : 0);
						programSetBuilder = this.FilterTopLevelProgramSet(programSetBuilder);
						if (bigInteger2 <= bigInteger)
						{
							break;
						}
						bigInteger = bigInteger2;
						flag = programSetBuilder == null || programSetBuilder.Set.IsEmpty;
					}
				}
				ProgramSetBuilder<@switch> programSetBuilder2 = this._build.Set.Join.SingleBranch(programSetBuilder);
				optional = ((programSetBuilder2 != null) ? programSetBuilder2.Set : null).Some<ProgramSet>();
			}
			finally
			{
				this._isOutsideTopLevel = isOutsideTopLevel;
			}
			return optional;
		}

		// Token: 0x0600FA35 RID: 64053 RVA: 0x00353640 File Offset: 0x00351840
		private static int? IdentifyConditionalClusterLimit(int exampleCount)
		{
			if (exampleCount < 30)
			{
				return null;
			}
			if (exampleCount < 50)
			{
				return new int?(2);
			}
			return new int?(1);
		}

		// Token: 0x0600FA36 RID: 64054 RVA: 0x00353670 File Offset: 0x00351870
		[RuleLearner("IfThenElse")]
		internal Optional<ProgramSet> LearnIfThenElse(SynthesisEngine engine, BlackBoxRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			if ((this._options.AllowedTransformations & TransformationKind.IfThenElse) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			int num;
			if (this._options.MaxBranchCount != null && task.RecursionDepths.TryGetValue(this._build.Symbol.ite, out num) && num >= this._options.MaxBranchCount.Value)
			{
				return OptionalUtils.Some((T)null);
			}
			bool isOutsideTopLevel = this._isOutsideTopLevel;
			this._isOutsideTopLevel = false;
			Optional<ProgramSet> optional;
			try
			{
				DisjunctiveExamplesSpec disjunctiveExamplesSpec = task.Spec as DisjunctiveExamplesSpec;
				if (disjunctiveExamplesSpec == null)
				{
					optional = OptionalUtils.Some((T)null);
				}
				else if (disjunctiveExamplesSpec.DisjunctiveExamples.Count < 2)
				{
					optional = OptionalUtils.Some((T)null);
				}
				else
				{
					LearningConfiguration learningConfiguration = task.Configuration as LearningConfiguration;
					if (learningConfiguration != null && learningConfiguration.ConditionalBranchLimit != null)
					{
						int? num2 = learningConfiguration.ConditionalBranchLimit;
						int num3 = 2;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							return OptionalUtils.Some((T)null);
						}
					}
					List<State> list = new List<State>();
					List<State> list2 = new List<State>();
					foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in disjunctiveExamplesSpec.DisjunctiveExamples)
					{
						if (keyValuePair.Value.OfType<ValueSubstring>().Any((ValueSubstring v) => Witnesses.NaValues.Contains((v != null) ? v.Value : null)))
						{
							list2.Add(keyValuePair.Key);
						}
						else
						{
							list.Add(keyValuePair.Key);
						}
					}
					ProgramSetBuilder<b> programSetBuilder = null;
					List<BisectSpec> list3 = new List<BisectSpec>();
					if (list.Count == 0)
					{
						if (disjunctiveExamplesSpec.DisjunctiveExamples.Values.SelectMany((IEnumerable<object> example) => example.OfType<ValueSubstring>()).Distinct<ValueSubstring>().Count<ValueSubstring>() == 1)
						{
							return OptionalUtils.Some((T)null);
						}
						list3.Add(new BisectSpec(list2, new State[0]));
					}
					else
					{
						list3.Add(new BisectSpec(list, list2));
						if (list2.Count > 0)
						{
							list3.Add(new BisectSpec(list.Concat(list2).ToList<State>(), new State[0]));
						}
					}
					foreach (BisectSpec bisectSpec in list3)
					{
						LearningTask learningTask = task.Clone(this._build.Symbol.b, bisectSpec);
						if (learningTask.K.HasValue)
						{
							learningTask = learningTask.WithTopKPrograms(new int?(learningTask.K.Value + 100));
						}
						programSetBuilder = this._build.Set.Cast.b(engine.Learn(learningTask, cancel));
						if (!ProgramSetBuilder.IsNullOrEmpty<b>(programSetBuilder))
						{
							break;
						}
					}
					if (ProgramSetBuilder.IsNullOrEmpty<b>(programSetBuilder))
					{
						optional = OptionalUtils.Some((T)null);
					}
					else
					{
						ProgramSet programSet = ProgramSet.Empty(this._build.Symbol.ite);
						IReadOnlyList<State> readOnlyList = task.ProvidedInputs.Distinct<State>().ToList<State>();
						IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<b>>> enumerable = from cluster in programSetBuilder.ClusterOnInputTuple(readOnlyList)
							orderby cluster.Key.Count((Optional<bool> e) => e.HasValue && e.Value) descending
							select cluster;
						LearningTask learningTask2 = task;
						if (learningConfiguration == null || learningConfiguration.ConditionalClusterLimit == null)
						{
							LearningTask learningTask3 = learningTask2;
							int? num4 = ((learningConfiguration != null) ? learningConfiguration.ConditionalBranchLimit : null);
							int? num2 = this._options.ConditionalClusterLimit;
							learningTask2 = learningTask3.WithConfiguration(new LearningConfiguration(num4, (num2 != null) ? num2 : Witnesses.IdentifyConditionalClusterLimit(disjunctiveExamplesSpec.DisjunctiveExamples.Count)));
						}
						int num5 = 0;
						foreach (KeyValuePair<Optional<bool>[], ProgramSetBuilder<b>> keyValuePair2 in enumerable)
						{
							learningConfiguration = learningTask2.Configuration as LearningConfiguration;
							if (cancel.IsCancellationRequested)
							{
								break;
							}
							if (learningConfiguration != null && learningConfiguration.ConditionalBranchLimit != null)
							{
								int num6 = num5;
								int? num2 = learningConfiguration.ConditionalClusterLimit;
								if ((num6 >= num2.GetValueOrDefault()) & (num2 != null))
								{
									break;
								}
							}
							ProgramSetBuilder<ite> programSetBuilder2 = this.LearnCluster(keyValuePair2.Key, keyValuePair2.Value, engine, learningTask2, disjunctiveExamplesSpec, readOnlyList, cancel);
							if (!ProgramSetBuilder.IsNullOrEmpty<ite>(programSetBuilder2))
							{
								num5++;
								if (learningTask2.K.HasValue && learningTask2.PruningRequest.RandomK == null)
								{
									PrunedProgramSet prunedProgramSet = new ProgramSet[] { programSet, programSetBuilder2.Set }.NormalizedUnion().Prune(learningTask2.PruningRequest, learningTask2.FeatureCalculationContext, new Random(this._options.RandomSeed), this._options.GetLogListenerIfEnabled(null));
									learningTask2 = learningTask2.WithConfiguration(new LearningConfiguration(new int?(prunedProgramSet.TopPrograms.Max((ProgramNode node) => node.AcceptVisitor<int>(this._branchCount))), (learningConfiguration != null) ? learningConfiguration.ConditionalClusterLimit : null));
									programSet = prunedProgramSet;
								}
								else if (learningTask2.PruningRequest.RandomK != null)
								{
									programSet = new ProgramSet[] { programSet, programSetBuilder2.Set }.NormalizedUnion();
									learningTask2 = learningTask2.WithConfiguration(new LearningConfiguration(new int?(programSet.TopK(this._rankingScore, learningTask2.K.Value, learningTask2.FeatureCalculationContext, null).Max((ProgramNode node) => node.AcceptVisitor<int>(this._branchCount))), (learningConfiguration != null) ? learningConfiguration.ConditionalClusterLimit : null));
								}
								else
								{
									programSet = new ProgramSet[] { programSet, programSetBuilder2.Set }.NormalizedUnion();
								}
							}
						}
						if (learningTask2.PruningRequest.RandomK != null)
						{
							programSet = programSet.Prune(learningTask2.PruningRequest, learningTask2.FeatureCalculationContext, new Random(this._options.RandomSeed), this._options.GetLogListenerIfEnabled(null));
						}
						optional = programSet.Some<ProgramSet>();
					}
				}
			}
			finally
			{
				this._isOutsideTopLevel = isOutsideTopLevel;
			}
			return optional;
		}

		// Token: 0x0600FA37 RID: 64055 RVA: 0x00353D28 File Offset: 0x00351F28
		[RuleLearner("LetPredicate")]
		internal Optional<ProgramSet> LearnPredicate(SynthesisEngine engine, LetRule rule, LearningTask<BisectSpec> task, CancellationToken cancel)
		{
			if (this._learningColumns == null)
			{
				return OptionalUtils.Some((T)null);
			}
			List<ProgramSetBuilder<b>> list = new List<ProgramSetBuilder<b>>();
			using (IEnumerator<string> enumerator = this._learningColumns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string columnName = enumerator.Current;
					List<State> list2 = task.Spec.Positives.Select((State state) => state.Bind(this._build.Symbol.cs, Semantics.ChooseInput((IRow)state[this._build.Symbol.vs], columnName))).ToList<State>();
					List<State> list3 = new List<State>();
					foreach (State state3 in task.Spec.LikelyNegatives)
					{
						ValueSubstring valueSubstring = Semantics.ChooseInput((IRow)state3[this._build.Symbol.vs], columnName);
						State state2 = state3.Bind(this._build.Symbol.cs, valueSubstring);
						if (Semantics.IsNullOrWhiteSpace(valueSubstring))
						{
							list2.Add(state2);
						}
						else
						{
							list3.Add(state2);
						}
					}
					LearningTask learningTask = task.Clone(this._build.Symbol.pred, new BisectSpec(list2, list3));
					learningTask.AdditionalInputs = task.AdditionalInputs.Select((State state) => state.Bind(this._build.Symbol.cs, Semantics.ChooseInput((IRow)state[this._build.Symbol.vs], columnName))).ToList<State>();
					ProgramSetBuilder<pred> programSetBuilder = this._build.Set.Cast.pred(engine.Learn(learningTask, cancel));
					if (!ProgramSetBuilder.IsNullOrEmpty<pred>(programSetBuilder))
					{
						y y = this._build.Node.Rule.SelectInput(this._build.Node.Variable.vs, this._build.Node.Rule.name(columnName));
						ProgramSetBuilder<y> programSetBuilder2 = ProgramSetBuilder.List<y>(new y[] { y });
						list.Add(this._build.Set.Join.LetPredicate(programSetBuilder2, programSetBuilder));
					}
				}
			}
			ProgramSetBuilder<b> programSetBuilder3 = list.NormalizedUnion<b>();
			return ((programSetBuilder3 != null) ? programSetBuilder3.Set : null).Some<ProgramSet>();
		}

		// Token: 0x170029E2 RID: 10722
		// (get) Token: 0x0600FA38 RID: 64056 RVA: 0x00353F78 File Offset: 0x00352178
		[ExternLearningLogicMapping("Predicate")]
		public DomainLearningLogic ExternWitnessFunction { get; } = new Witnesses(Language.Grammar, new Witnesses.Options
		{
			ClusterCount = new int?(2)
		});

		// Token: 0x0600FA39 RID: 64057 RVA: 0x00353F80 File Offset: 0x00352180
		private ProgramSetBuilder<ite> LearnCluster(IReadOnlyList<Optional<bool>> clusterResult, ProgramSetBuilder<b> predProgramSet, SynthesisEngine engine, LearningTask task, DisjunctiveExamplesSpec examples, IReadOnlyList<State> allInputs, CancellationToken cancel)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			HashSet<State> hashSet = new HashSet<State>();
			Dictionary<State, IEnumerable<object>> dictionary2 = new Dictionary<State, IEnumerable<object>>();
			HashSet<State> hashSet2 = new HashSet<State>();
			for (int i = 0; i < clusterResult.Count; i++)
			{
				State state3 = allInputs[i];
				IEnumerable<object> enumerable;
				if (clusterResult[i].Value)
				{
					if (examples.DisjunctiveExamples.TryGetValue(state3, out enumerable))
					{
						dictionary[state3] = enumerable;
					}
					else
					{
						hashSet.Add(state3);
					}
				}
				else if (examples.DisjunctiveExamples.TryGetValue(state3, out enumerable))
				{
					dictionary2[state3] = enumerable;
				}
				else
				{
					hashSet2.Add(state3);
				}
			}
			if (dictionary.Count == 0 || dictionary.Count == examples.DisjunctiveExamples.Count)
			{
				return ProgramSetBuilder.Empty<ite>(this._build.Symbol.ite);
			}
			LearningTask learningTask = task.Clone(this._build.Symbol.st, new DisjunctiveExamplesSpec(dictionary));
			HashSet<IRow> hashSet3 = (from state in learningTask.FeatureCalculationContext.MaterializeSpecInputs()
				select state[this._build.Symbol.vs] as IRow).ConvertToHashSet<IRow>();
			List<State> list = new List<State>();
			foreach (State state2 in hashSet)
			{
				IRow row = state2[this._build.Symbol.vs] as IRow;
				if (!hashSet3.Contains(row))
				{
					hashSet3.Add(row);
					list.Add(state2);
				}
			}
			learningTask.AdditionalInputs = list.RandomlySampleToList(this._options.RandomSeed, this._options.AdditionalInputsSampleSize);
			ProgramSet programSet = engine.Learn(learningTask, cancel);
			if (ProgramSet.IsNullOrEmpty(programSet))
			{
				return ProgramSetBuilder.Empty<ite>(this._build.Symbol.ite);
			}
			if (cancel.IsCancellationRequested)
			{
				return ProgramSetBuilder.Empty<ite>(this._build.Symbol.ite);
			}
			ProgramSetBuilder<st> programSetBuilder = this._build.Set.Cast.st(programSet);
			LearningTask learningTask2 = task.Clone(this._build.Symbol.st, new DisjunctiveExamplesSpec(dictionary2));
			learningTask2.AdditionalInputs = hashSet2;
			LearningConfiguration learningConfiguration = task.Configuration as LearningConfiguration;
			if (learningConfiguration != null && learningConfiguration.ConditionalBranchLimit != null)
			{
				learningTask2 = learningTask2.WithConfiguration(new LearningConfiguration(learningConfiguration.ConditionalBranchLimit - 1, learningConfiguration.ConditionalClusterLimit));
			}
			ProgramSetBuilder<@switch> programSetBuilder2 = this._build.Set.Join.SingleBranch(this._build.Set.Cast.st(engine.Learn(learningTask2, cancel)));
			if (ProgramSetBuilder.IsNullOrEmpty<@switch>(programSetBuilder2))
			{
				LearningTask learningTask3 = learningTask2.Clone(this._build.Symbol.ite, null);
				programSetBuilder2 = this._build.Set.UnnamedConversion.switch_ite(this._build.Set.Cast.ite(engine.Learn(learningTask3, cancel)));
			}
			if (ProgramSetBuilder.IsNullOrEmpty<@switch>(programSetBuilder2))
			{
				return ProgramSetBuilder.Empty<ite>(this._build.Symbol.ite);
			}
			ProgramSetBuilder<ite> programSetBuilder3 = this._build.Set.Join.IfThenElse(predProgramSet, programSetBuilder, programSetBuilder2);
			if (task.RequiresPruning)
			{
				programSetBuilder3 = programSetBuilder3.Prune(task, engine.RandomNumberGenerator, engine.Configuration.LogListener);
			}
			return programSetBuilder3;
		}

		// Token: 0x0600FA3A RID: 64058 RVA: 0x00354320 File Offset: 0x00352520
		[RuleLearner("Transformation")]
		private Optional<ProgramSet> LearnNoConcatFirst(SynthesisEngine engine, ConversionRule transformationRule, LearningTask<Spec> task, CancellationToken cancel)
		{
			if (!task.RequiresPruning)
			{
				return Optional<ProgramSet>.Nothing;
			}
			if ((this._options.ConcatLocation & ConcatLocation.Nowhere) == (ConcatLocation)0)
			{
				return Optional<ProgramSet>.Nothing;
			}
			LearningTask learningTask = ((task.K.HasValue && task.K.Value == 1) ? task.WithTopKPrograms(new int?(2)) : task);
			ProgramSet programSet = engine.Learn(learningTask.Clone(this._build.Symbol.f, null), cancel);
			if (!ProgramSet.IsNullOrEmpty(programSet) && programSet.AcceptVisitor<bool>(this.AcceptableNoConcatSetVistorInstance) && (task.K.Value <= 2 || programSet.RealizedPrograms.Take(task.K.Value).Count<ProgramNode>() >= task.K.Value))
			{
				return this._build.Set.Join.Transformation(this._build.Set.Join.Atom(this._build.Set.Cast.f(programSet))).Set.Some<ProgramSet>();
			}
			return Optional<ProgramSet>.Nothing;
		}

		// Token: 0x170029E3 RID: 10723
		// (get) Token: 0x0600FA3B RID: 64059 RVA: 0x0035444C File Offset: 0x0035264C
		private Witnesses.AcceptableNoConcatNodeVisitor AcceptableNoConcatNodeVisitorInstance { get; }

		// Token: 0x170029E4 RID: 10724
		// (get) Token: 0x0600FA3C RID: 64060 RVA: 0x00354454 File Offset: 0x00352654
		private Witnesses.AcceptableNoConcatSetVistor AcceptableNoConcatSetVistorInstance { get; }

		// Token: 0x0600FA3D RID: 64061 RVA: 0x0035445C File Offset: 0x0035265C
		private IReadOnlyList<uint> ComputeConcatLocations(IRow inputRow, ValueSubstring ss, ConcatLocation concatLocation)
		{
			if (concatLocation == ConcatLocation.Anywhere)
			{
				throw new ArgumentException("ComputeConcatLocations should not be used for the ConcatLocation Anywhere");
			}
			if (concatLocation == ConcatLocation.AtTokenBoundaries)
			{
				StringLearningCache cache = ss.Cache;
				cache.InitializeStaticTokens(null);
				return cache.MatchStartPositions.Union(cache.MatchEndPositions).ToList<uint>();
			}
			if (concatLocation != ConcatLocation.AtTokenBoundariesExceptInputs)
			{
				throw new NotImplementedException("Unknown ConcatLocation or multiple non-Nowhere ConcatLocations: " + this._options.ConcatLocation.ToString());
			}
			HashSet<uint> avoidSplitPos = new HashSet<uint>();
			foreach (string text in inputRow.ColumnNames)
			{
				ValueSubstring valueSubstring = Semantics.ChooseInput(inputRow, text);
				if (!(valueSubstring == null) && valueSubstring.Length > 1U)
				{
					int num = 0;
					while ((long)num < (long)((ulong)(ss.End - valueSubstring.Length)))
					{
						int num2 = ss.Source.IndexOf(valueSubstring.Value, num, StringComparison.Ordinal);
						if (num2 < 0)
						{
							break;
						}
						num = (int)((long)num2 + (long)((ulong)valueSubstring.Length));
						for (uint num3 = 1U; num3 < valueSubstring.Length; num3 += 1U)
						{
							avoidSplitPos.Add(num3 + (uint)num2);
						}
					}
				}
			}
			StringLearningCache cache2 = ss.Cache;
			cache2.InitializeStaticTokens(null);
			return (from splitPos in cache2.MatchStartPositions.Union(cache2.MatchEndPositions)
				where !avoidSplitPos.Contains(splitPos)
				select splitPos).ToList<uint>();
		}

		// Token: 0x0600FA3E RID: 64062 RVA: 0x003545E8 File Offset: 0x003527E8
		[WitnessFunction("Concat", 0)]
		internal DisjunctiveExamplesSpec WitnessConcatf(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			if (!this._options.AllowConcat || !this._options.OutputType.Equals(UnknownType.Instance))
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			using (IEnumerator<State> enumerator = spec.ProvidedInputs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					State state = enumerator.Current;
					List<ValueSubstring> list = new List<ValueSubstring>();
					using (IEnumerator<ValueSubstring> enumerator2 = spec.DisjunctiveExamples[state].OfType<ValueSubstring>().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ValueSubstring outputSR = enumerator2.Current;
							if (!(outputSR == null) && outputSR.Length > 1U)
							{
								ConcatLocation concatLocation = this._options.ConcatLocation & ~ConcatLocation.Nowhere;
								if (concatLocation == ConcatLocation.Anywhere)
								{
									for (uint num = outputSR.Length - 1U; num > 0U; num -= 1U)
									{
										uint num2 = outputSR.Start + num;
										list.Add(outputSR.Slice(outputSR.Start, new uint?(num2)));
									}
								}
								else
								{
									list.AddRange(from splitPos in outputSR.Cache.GetSpecializedCache<UnboundedCache<ConcatLocation, IReadOnlyList<uint>>>((StringLearningCache _) => new ConcurrentUnboundedCache<ConcatLocation, IReadOnlyList<uint>>()).GetOrAdd(concatLocation, (ConcatLocation cl) => this.ComputeConcatLocations((IRow)state[this.Grammar.InputSymbol], outputSR, cl))
										where splitPos > outputSR.Start && splitPos <= outputSR.End
										select outputSR.Slice(outputSR.Start, new uint?(splitPos)));
								}
							}
						}
					}
					List<StringPrefixSet> list2 = new List<StringPrefixSet>();
					foreach (StringPrefixSet stringPrefixSet in spec.DisjunctiveExamples[state].OfType<StringPrefixSet>())
					{
						list.AddRange(stringPrefixSet.AllPrefixesOfPrefix);
						list2.Add(stringPrefixSet);
					}
					dictionary[state] = list.Concat(list2.Cast<object>()).ToList<object>();
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA3F RID: 64063 RVA: 0x0035488C File Offset: 0x00352A8C
		[WitnessFunction("Concat", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessSuffix(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec prefixes)
		{
			if (!this._options.AllowConcat || !this._options.OutputType.Equals(UnknownType.Instance))
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in prefixes.Examples)
			{
				State key = keyValuePair.Key;
				if (keyValuePair.Value is StringPrefixSet)
				{
					if (keyValuePair.Value.Equals(StringPrefixSet.AllStrings) && spec.DisjunctiveExamples[key].Contains(StringPrefixSet.AllStrings))
					{
						dictionary[key] = new StringPrefixSet[] { StringPrefixSet.AllStrings };
						break;
					}
					return null;
				}
				else
				{
					ValueSubstring valueSubstring = (ValueSubstring)keyValuePair.Value;
					if (valueSubstring == null || valueSubstring.Length == 0U)
					{
						return null;
					}
					List<ValueSubstring> list = new List<ValueSubstring>();
					foreach (ValueSubstring valueSubstring2 in spec.DisjunctiveExamples[key].OfType<ValueSubstring>())
					{
						if (valueSubstring2.StartsWith(valueSubstring) && valueSubstring2.Length > valueSubstring.Length)
						{
							list.Add(valueSubstring2.Slice(valueSubstring2.Start + valueSubstring.Length, null));
						}
					}
					List<StringPrefixSet> list2 = new List<StringPrefixSet>();
					foreach (StringPrefixSet stringPrefixSet in spec.DisjunctiveExamples[key].OfType<StringPrefixSet>())
					{
						StringPrefixSet stringPrefixSet2 = stringPrefixSet.PrefixSetAfter(valueSubstring);
						list2.Add(stringPrefixSet2);
					}
					dictionary[key] = list.Concat(list2.Cast<object>()).ToList<object>();
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA40 RID: 64064 RVA: 0x00354AC4 File Offset: 0x00352CC4
		[WitnessFunction("ConstStr", "s")]
		internal DisjunctiveExamplesSpec WitnessS(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			if ((this._options.AllowedTransformations & TransformationKind.Constant) == TransformationKind.Unknown)
			{
				return null;
			}
			if (!this._options.OutputType.Equals(UnknownType.Instance))
			{
				return null;
			}
			HashSet<string> consts = null;
			List<StringPrefixSet> list = null;
			bool flag = true;
			foreach (IEnumerable<object> enumerable in spec.DisjunctiveExamples.Values)
			{
				IEnumerable<string> enumerable2 = enumerable.Where((object o) => !(o is StringPrefixSet)).Cast<ValueSubstring>().Select(delegate(ValueSubstring o)
				{
					if (o == null)
					{
						return null;
					}
					return o.Value;
				});
				List<StringPrefixSet> outputPrefixSets = enumerable.OfType<StringPrefixSet>().ToList<StringPrefixSet>();
				if (!outputPrefixSets.Any<StringPrefixSet>())
				{
					flag = false;
				}
				if (consts == null)
				{
					consts = enumerable2.ConvertToHashSet<string>();
				}
				else if (outputPrefixSets.Any<StringPrefixSet>())
				{
					IEnumerable<string> enumerable3 = consts.Where((string c) => outputPrefixSets.Any((StringPrefixSet prefix) => prefix.Contains(c)));
					consts.IntersectWith(enumerable2.Concat(enumerable3));
				}
				else
				{
					consts.IntersectWith(enumerable2);
				}
				if (list == null)
				{
					list = outputPrefixSets;
				}
				else
				{
					List<StringPrefixSet> oldPrefixSets = list;
					list = outputPrefixSets.Where((StringPrefixSet outPs) => oldPrefixSets.Any((StringPrefixSet ps) => ps.Contains(outPs))).ToList<StringPrefixSet>();
				}
				if (consts.Count == 0 && list.Count == 0)
				{
					return null;
				}
			}
			if (flag)
			{
				consts.AddRange(list.Select((StringPrefixSet ps) => ps.Prefix.Value));
			}
			return DisjunctiveExamplesSpec.From(spec.ProvidedInputs.ToDictionary((State s) => s, (State s) => consts.Cast<object>()));
		}

		// Token: 0x0600FA41 RID: 64065 RVA: 0x00354D04 File Offset: 0x00352F04
		private static IEnumerable<ValueSubstring> GetCaseInsensitiveSubStrings(ValueSubstring xSR, IEnumerable<ValueSubstring> outputs)
		{
			if (!(xSR == null))
			{
				List<ValueSubstring> list = new List<ValueSubstring>();
				foreach (ValueSubstring valueSubstring in outputs)
				{
					if (!(valueSubstring == null))
					{
						string value = valueSubstring.Value;
						Optional<uint> optional = xSR.IndexOfRelative(value, 0U, StringComparison.OrdinalIgnoreCase);
						while (optional.HasValue && optional.Value <= xSR.Length)
						{
							list.Add(xSR.SliceRelative(optional.Value, new uint?(optional.Value + valueSubstring.Length)));
							optional = xSR.IndexOfRelative(value, optional.Value + 1U, StringComparison.OrdinalIgnoreCase);
						}
					}
				}
				return list;
			}
			if (!outputs.Any((ValueSubstring substringSR) => substringSR == null))
			{
				return new ValueSubstring[0];
			}
			return new ValueSubstring[1];
		}

		// Token: 0x0600FA42 RID: 64066 RVA: 0x00354DF8 File Offset: 0x00352FF8
		private IEnumerable<ValueSubstring> GetCaseInsensitiveSubStringsForPrefix(ValueSubstring xSr, List<StringPrefixSet> prefixSetOutputs)
		{
			if (xSr == null)
			{
				return new ValueSubstring[0];
			}
			List<ValueSubstring> list = new List<ValueSubstring>();
			foreach (StringPrefixSet stringPrefixSet in prefixSetOutputs)
			{
				string value = stringPrefixSet.Prefix.Value;
				Optional<uint> optional = xSr.IndexOfRelative(value, 0U, StringComparison.OrdinalIgnoreCase);
				while (optional.HasValue)
				{
					list.AddRange(xSr.GetAllSubstringsStartingAtRelative(optional.Value, stringPrefixSet.Prefix.Length));
					optional = xSr.IndexOfRelative(value, optional.Value + 1U, StringComparison.OrdinalIgnoreCase);
				}
			}
			return list;
		}

		// Token: 0x0600FA43 RID: 64067 RVA: 0x00002188 File Offset: 0x00000388
		[WitnessFunction("ToLowercase", 0)]
		[WitnessFunction("ToUppercase", 0)]
		[WitnessFunction("ToSimpleTitleCase", 0)]
		internal DisjunctiveExamplesSpec WitnessToCaseOutputNotNullSS(GrammarRule rule, OutputNotNullSpec spec)
		{
			return null;
		}

		// Token: 0x0600FA44 RID: 64068 RVA: 0x00354EAC File Offset: 0x003530AC
		private DisjunctiveExamplesSpec WitnessCaseConversionSS(GrammarRule rule, DisjunctiveExamplesSpec spec, Func<ValueSubstring, ValueSubstring> caseConversionFunction)
		{
			if ((this._options.AllowedTransformations & TransformationKind.CaseTransformation) == TransformationKind.Unknown)
			{
				return null;
			}
			if (!this._options.OutputType.Equals(UnknownType.Instance))
			{
				return null;
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			Func<ValueSubstring, bool> <>9__1;
			Func<StringPrefixSet, bool> <>9__3;
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				State key = keyValuePair.Key;
				object obj;
				if (!key.TryGetValue(this._build.Symbol.x, out obj))
				{
					return null;
				}
				ValueSubstring valueSubstring = (ValueSubstring)obj;
				IEnumerable<object> value = keyValuePair.Value;
				IEnumerable<ValueSubstring> enumerable = value.Where((object o) => !(o is StringPrefixSet)).Cast<ValueSubstring>();
				Func<ValueSubstring, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(ValueSubstring o)
					{
						string text = ((o != null) ? o.Value : null);
						ValueSubstring valueSubstring2 = caseConversionFunction(o);
						return text == ((valueSubstring2 != null) ? valueSubstring2.Value : null);
					});
				}
				List<ValueSubstring> list = enumerable.Where(func).ToList<ValueSubstring>();
				List<ValueSubstring> list2 = new List<ValueSubstring>();
				if (list.Any(delegate(ValueSubstring o)
				{
					bool? flag;
					if (o == null)
					{
						flag = null;
					}
					else
					{
						string value2 = o.Value;
						if (value2 == null)
						{
							flag = null;
						}
						else
						{
							Func<char, bool> func3;
							if ((func3 = Witnesses.<>O.<0>__IsLetter) == null)
							{
								func3 = (Witnesses.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
							}
							flag = new bool?(value2.Any(func3));
						}
					}
					return flag ?? true;
				}))
				{
					list2.AddRange(Witnesses.GetCaseInsensitiveSubStrings(valueSubstring, list));
				}
				IEnumerable<StringPrefixSet> enumerable2 = value.OfType<StringPrefixSet>();
				Func<StringPrefixSet, bool> func2;
				if ((func2 = <>9__3) == null)
				{
					func2 = (<>9__3 = (StringPrefixSet o) => o.Prefix.Value == caseConversionFunction(o.Prefix).Value);
				}
				List<StringPrefixSet> list3 = enumerable2.Where(func2).ToList<StringPrefixSet>();
				if (list3.Any(delegate(StringPrefixSet o)
				{
					IEnumerable<char> value3 = o.Prefix.Value;
					Func<char, bool> func4;
					if ((func4 = Witnesses.<>O.<0>__IsLetter) == null)
					{
						func4 = (Witnesses.<>O.<0>__IsLetter = new Func<char, bool>(char.IsLetter));
					}
					return value3.Any(func4);
				}))
				{
					list2.AddRange(this.GetCaseInsensitiveSubStringsForPrefix(valueSubstring, list3));
				}
				if (!list2.Any<ValueSubstring>())
				{
					return null;
				}
				dictionary[key] = list2;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA45 RID: 64069 RVA: 0x003550A8 File Offset: 0x003532A8
		[WitnessFunction("ToLowercase", 0)]
		internal DisjunctiveExamplesSpec WitnessToLowercaseSS(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<ValueSubstring, ValueSubstring> func;
			if ((func = Witnesses.<>O.<1>__ToLowercase) == null)
			{
				func = (Witnesses.<>O.<1>__ToLowercase = new Func<ValueSubstring, ValueSubstring>(Semantics.ToLowercase));
			}
			return this.WitnessCaseConversionSS(rule, spec, func);
		}

		// Token: 0x0600FA46 RID: 64070 RVA: 0x003550CD File Offset: 0x003532CD
		[WitnessFunction("ToUppercase", 0)]
		internal DisjunctiveExamplesSpec WitnessToUppercaseSS(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<ValueSubstring, ValueSubstring> func;
			if ((func = Witnesses.<>O.<2>__ToUppercase) == null)
			{
				func = (Witnesses.<>O.<2>__ToUppercase = new Func<ValueSubstring, ValueSubstring>(Semantics.ToUppercase));
			}
			return this.WitnessCaseConversionSS(rule, spec, func);
		}

		// Token: 0x0600FA47 RID: 64071 RVA: 0x003550F2 File Offset: 0x003532F2
		[WitnessFunction("ToSimpleTitleCase", 0)]
		internal DisjunctiveExamplesSpec WitnessToSimpleTitleCaseSS(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Func<ValueSubstring, ValueSubstring> func;
			if ((func = Witnesses.<>O.<3>__ToSimpleTitleCase) == null)
			{
				func = (Witnesses.<>O.<3>__ToSimpleTitleCase = new Func<ValueSubstring, ValueSubstring>(Semantics.ToSimpleTitleCase));
			}
			return this.WitnessCaseConversionSS(rule, spec, func);
		}

		// Token: 0x0600FA48 RID: 64072 RVA: 0x00355118 File Offset: 0x00353318
		private IEnumerable<IGrouping<DateTimeFormat, Witnesses.MatchWithState>> GetOutputMatchesByFormat(DisjunctiveExamplesSpec spec)
		{
			HashSet<State> inputStates = spec.DisjunctiveExamples.Keys.ConvertToHashSet<State>();
			return from matchWithState in spec.DisjunctiveExamples.SelectMany(delegate(KeyValuePair<State, IEnumerable<object>> kv)
				{
					Func<DateTimeFormatMatch, Witnesses.MatchWithState> <>9__5;
					return kv.Value.Cast<ValueSubstring>().SelectMany(delegate(ValueSubstring output)
					{
						IEnumerable<DateTimeFormatMatch> enumerable = DateFormatCache.AllFormatMatchesFor(output, DateFormatCache.ParseMode.FullLength, HeuristicsMode.AllowMostFormats);
						Func<DateTimeFormatMatch, Witnesses.MatchWithState> func;
						if ((func = <>9__5) == null)
						{
							func = (<>9__5 = (DateTimeFormatMatch outputDtMatch) => new Witnesses.MatchWithState(kv.Key, outputDtMatch));
						}
						return enumerable.Select(func);
					});
				})
				where this.IsReasonableMatchWithStateHeuristic(matchWithState)
				select matchWithState into o
				group o by o.Match.DateTimeFormat into @group
				where inputStates.SetEquals(@group.Select((Witnesses.MatchWithState o) => o.InputState))
				select @group;
		}

		// Token: 0x0600FA49 RID: 64073 RVA: 0x003551B8 File Offset: 0x003533B8
		private static bool IsReasonableMatchOnInputHeuristic(DateTimeFormatMatch match)
		{
			if (match.DateTimeFormat.FormatParts[0].IsNumeric)
			{
				Optional<char> optional = match.Region.MaybePreviousChar();
				if (optional.HasValue && char.IsDigit(optional.Value))
				{
					return false;
				}
			}
			uint end = match.Region.End;
			if (match.DateTimeFormat.FormatParts.Last<DateTimeFormatPart>().IsNumeric)
			{
				Optional<char> optional2 = match.Region.MaybeNextChar();
				if (optional2.HasValue && char.IsDigit(optional2.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600FA4A RID: 64074 RVA: 0x0035524C File Offset: 0x0035344C
		private bool IsReasonableMatchWithStateHeuristic(Witnesses.MatchWithState matchWithState)
		{
			DateTimeFormatMatch match = matchWithState.Match;
			int start = (int)match.Region.Start;
			int end = (int)match.Region.End;
			if (match.DateTimeFormat.FormatParts[0].IsNumeric)
			{
				if ((from c in match.Region.MaybePreviousChar()
					select char.IsDigit(c)).OrElse(false))
				{
					string text = match.Region.Source.Substring(start - 1, end - start + 1);
					object obj;
					if (matchWithState.InputState.TryGetValue(this._build.Symbol.x, out obj))
					{
						ValueSubstring valueSubstring = obj as ValueSubstring;
						if (valueSubstring != null && valueSubstring.Source.Contains(text))
						{
							return false;
						}
					}
				}
			}
			if (match.DateTimeFormat.FormatParts.Last<DateTimeFormatPart>().IsNumeric)
			{
				if ((from c in match.Region.MaybeNextChar()
					select char.IsDigit(c)).OrElse(false))
				{
					string text2 = match.Region.Source.Substring(start, (int)((ulong)match.Region.End - (ulong)((long)start) + 1UL));
					object obj2;
					if (matchWithState.InputState.TryGetValue(this._build.Symbol.x, out obj2))
					{
						ValueSubstring valueSubstring2 = obj2 as ValueSubstring;
						if (valueSubstring2 != null && valueSubstring2.Source.Contains(text2))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600FA4B RID: 64075 RVA: 0x003553DC File Offset: 0x003535DC
		private State BindColumnNameIfMissing(State state, string columnName)
		{
			object obj;
			if (!state.TryGetValue(this._build.Symbol.columnName, out obj))
			{
				return state.Bind(this._build.Symbol.columnName, columnName).Bind(this._build.Symbol.x, Semantics.ChooseInput((IRow)state[base.Grammar.InputSymbol], columnName)).Bind(this._build.Symbol.cell, Semantics.LookupInput((IRow)state[base.Grammar.InputSymbol], columnName));
			}
			return state;
		}

		// Token: 0x0600FA4C RID: 64076 RVA: 0x00355480 File Offset: 0x00353680
		private IImmutableList<State> GetExampleInputStatesIfAny(LearningTask task)
		{
			if (task.Spec is OutputNotNullSpec && task.Spec.ProvidedInputs.Count<State>() == 1 && task.AdditionalInputs.First<State>()[base.Grammar.InputSymbol].Equals(task.Spec.ProvidedInputs.Single<State>()[base.Grammar.InputSymbol]))
			{
				return null;
			}
			ImmutableList<State> immutableList = task.Spec.ProvidedInputs.Distinct<State>().ToImmutableList<State>();
			if (immutableList.Count == 0)
			{
				return null;
			}
			return immutableList;
		}

		// Token: 0x170029E5 RID: 10725
		// (get) Token: 0x0600FA4D RID: 64077 RVA: 0x00355512 File Offset: 0x00353712
		private DateTimeFormat OutputDateTimeFormat
		{
			get
			{
				FormattedPartialDateTimeType formattedPartialDateTimeType = this._options.OutputType as FormattedPartialDateTimeType;
				if (formattedPartialDateTimeType == null)
				{
					return null;
				}
				return formattedPartialDateTimeType.Format;
			}
		}

		// Token: 0x170029E6 RID: 10726
		// (get) Token: 0x0600FA4E RID: 64078 RVA: 0x00355530 File Offset: 0x00353730
		private DateTimePartSet? OutputDateTimeFormatParts
		{
			get
			{
				DateTimeFormat outputDateTimeFormat = this.OutputDateTimeFormat;
				if (outputDateTimeFormat != null)
				{
					return new DateTimePartSet?(outputDateTimeFormat.MatchedParts);
				}
				PartialDateTimeType partialDateTimeType = this._options.OutputType as PartialDateTimeType;
				if (partialDateTimeType == null && this._options.OutputType is IType<PartialDateTimeType>)
				{
					string text = "Unknown PartialDateTime type: ";
					IType outputType = this._options.OutputType;
					throw new NotImplementedException(text + ((outputType != null) ? outputType.ToString() : null));
				}
				if (partialDateTimeType == null)
				{
					return null;
				}
				return new DateTimePartSet?(partialDateTimeType.Parts);
			}
		}

		// Token: 0x0600FA4F RID: 64079 RVA: 0x003555B8 File Offset: 0x003537B8
		private string GetColumnName(Spec spec)
		{
			object obj;
			if (spec.ProvidedInputs.First<State>().TryGetValue(this._build.Symbol.columnName, out obj))
			{
				return (string)obj;
			}
			throw new ArgumentException("GetColumnName() requires a spec with at least one input which is below the LetColumnName rule.", "spec");
		}

		// Token: 0x0600FA50 RID: 64080 RVA: 0x003555FF File Offset: 0x003537FF
		private string GetColumnName(LearningTask task)
		{
			return this.GetColumnName(task.Spec);
		}

		// Token: 0x0600FA51 RID: 64081 RVA: 0x00355610 File Offset: 0x00353810
		private IEnumerable<Witnesses.OutputDateTimeFormatInfo> GetPossibleOutputFormatInfosFromConstraints(Spec spec)
		{
			DateTimeFormat outputFormat = this.OutputDateTimeFormat;
			DateTimePartSet? outputFormatParts = this.OutputDateTimeFormatParts;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec = spec as DisjunctiveExamplesSpec;
			if (disjunctiveExamplesSpec == null || !disjunctiveExamplesSpec.DisjunctiveExamples.Any<KeyValuePair<State, IEnumerable<object>>>())
			{
				return Seq.Of<Witnesses.OutputDateTimeFormatInfo>(new Witnesses.OutputDateTimeFormatInfo[]
				{
					new Witnesses.OutputDateTimeFormatInfo(outputFormat, outputFormatParts, outputFormat != null)
				});
			}
			return from g in this.GetOutputMatchesByFormat(disjunctiveExamplesSpec).Where(delegate(IGrouping<DateTimeFormat, Witnesses.MatchWithState> g)
				{
					if (outputFormat == null)
					{
						return outputFormatParts == null || g.Key.MatchedParts.Contains(outputFormatParts.Value);
					}
					return object.Equals(outputFormat, g.Key);
				})
				select new Witnesses.OutputDateTimeFormatInfo(g, outputFormat != null);
		}

		// Token: 0x0600FA52 RID: 64082 RVA: 0x003556B0 File Offset: 0x003538B0
		private static DateTimeFormat ExtractSubDateTimeFormat(DateTimeFormat format, DateTimePartSet outputParts)
		{
			List<DateTimeFormatPart> list = format.FormatParts.SkipWhile((DateTimeFormatPart p) => !p.MatchedPart.HasValue || !outputParts.Contains(p.MatchedPart.Value)).TakeWhile((DateTimeFormatPart p) => !p.MatchedPart.HasValue || outputParts.Contains(p.MatchedPart.Value)).ToList<DateTimeFormatPart>();
			while (list.Count > 0 && !list.Last<DateTimeFormatPart>().MatchedPart.HasValue)
			{
				list.RemoveAt(list.Count - 1);
			}
			DateTimeFormat dateTimeFormat = new DateTimeFormat(list);
			if (!dateTimeFormat.MatchedParts.Equals(outputParts))
			{
				return format;
			}
			return dateTimeFormat;
		}

		// Token: 0x0600FA53 RID: 64083 RVA: 0x00355748 File Offset: 0x00353948
		private static DateTimeFormat ChooseOutputDateTimeFormat(IReadOnlyList<DateTimeFormat> inputFormats, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			DateTimePartSet? dateTimePartSet = outputFormatInfo.Parts;
			IEnumerable<DateTimeFormat> enumerable;
			if (dateTimePartSet != null)
			{
				dateTimePartSet = outputFormatInfo.Parts;
				if (dateTimePartSet.Value.Any())
				{
					IReadOnlyList<DateTimeFormat> inputFormats2 = inputFormats;
					enumerable = ((inputFormats2 != null) ? inputFormats2.Select((DateTimeFormat format) => Witnesses.ExtractSubDateTimeFormat(format, outputFormatInfo.Parts.Value)) : null);
					goto IL_006E;
				}
			}
			enumerable = inputFormats;
			IL_006E:
			DateTimeFormat dateTimeFormat;
			if ((dateTimeFormat = ((enumerable != null) ? enumerable.FirstOrDefault((DateTimeFormat proposedOutputFormat) => inputFormats.All((DateTimeFormat otherFormat) => otherFormat.MatchedParts.CanExplain(proposedOutputFormat.MatchedParts))) : null)) == null)
			{
				dateTimePartSet = outputFormatInfo.Parts;
				if (dateTimePartSet == null)
				{
					return null;
				}
				dateTimeFormat = dateTimePartSet.GetValueOrDefault().GetDefaultDateTimeFormat();
			}
			return dateTimeFormat;
		}

		// Token: 0x0600FA54 RID: 64084 RVA: 0x00355802 File Offset: 0x00353A02
		private ProgramSetBuilder<outputDtFormat> BuildOutputDtFormatProgramSet(DateTimeFormat format)
		{
			return ProgramSetBuilder.List<outputDtFormat>(new outputDtFormat[] { this._build.Node.Rule.outputDtFormat(format) });
		}

		// Token: 0x0600FA55 RID: 64085 RVA: 0x0035582C File Offset: 0x00353A2C
		private IEnumerable<ProgramSetBuilder<conv>> LearnReformatDateForParseWithoutExamples(JoinProgramSetBuilder<datetime> conversionParseDateSet, Witnesses.OutputDateTimeFormatInfo outputFormatInfo, State anyState)
		{
			JoinProgramSet joinProgramSet = (JoinProgramSet)conversionParseDateSet.Set.ParameterSpaces[0];
			if (joinProgramSet.Rule == this._build.UnnamedConversion.inputDateTime_parsedDateTime)
			{
				JoinProgramSet parseDateSet = (JoinProgramSet)joinProgramSet.ParameterSpaces[0];
				return (from c in this._build.Set.Cast.inputDtFormats(parseDateSet.ParameterSpaces[1]).ClusterOnInput(anyState)
					group c by Witnesses.ChooseOutputDateTimeFormat(c.Key.Value, outputFormatInfo) into g
					where g.Key != null
					select g).Select(delegate(IGrouping<DateTimeFormat, KeyValuePair<Optional<DateTimeFormat[]>, ProgramSetBuilder<inputDtFormats>>> g)
				{
					ProgramSetBuilder<inputDtFormats> programSetBuilder = g.Select((KeyValuePair<Optional<DateTimeFormat[]>, ProgramSetBuilder<inputDtFormats>> cluster) => cluster.Value).NormalizedUnion<inputDtFormats>();
					return this._build.Set.Join.FormatPartialDateTime(this._build.Set.UnnamedConversion.datetime_inputDateTime(this._build.Set.UnnamedConversion.inputDateTime_parsedDateTime(this._build.Set.Join.ParsePartialDateTime(this._build.Set.Cast.SS(parseDateSet.ParameterSpaces[0]), programSetBuilder))), this.BuildOutputDtFormatProgramSet(g.Key));
				});
			}
			if (joinProgramSet.Rule != this._build.Rule.AsPartialDateTime)
			{
				string text = "Unexpected datetime rule: ";
				NonterminalRule rule = joinProgramSet.Rule;
				throw new NotImplementedException(text + ((rule != null) ? rule.ToString() : null));
			}
			DateTimeFormat dateTimeFormat = Witnesses.ChooseOutputDateTimeFormat(null, outputFormatInfo);
			if (dateTimeFormat == null)
			{
				return Enumerable.Empty<ProgramSetBuilder<conv>>();
			}
			return Seq.Of<ProgramSetBuilder<conv>>(new ProgramSetBuilder<conv>[] { this._build.Set.Join.FormatPartialDateTime(this._build.Set.UnnamedConversion.datetime_inputDateTime(this._build.Set.Join.AsPartialDateTime(ProgramSetBuilder.List<cell>(new cell[] { this._build.Node.Variable.cell }))), this.BuildOutputDtFormatProgramSet(dateTimeFormat)) });
		}

		// Token: 0x0600FA56 RID: 64086 RVA: 0x003559E0 File Offset: 0x00353BE0
		private IEnumerable<ProgramSetBuilder<conv>> LearnReformatDateForOutputFormatInfo(SynthesisEngine engine, BlackBoxRule formatDateRule, LearningTask<Spec> task, CancellationToken cancel, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			cancel.ThrowIfCancellationRequested();
			State anyState = task.ProvidedInputs.FirstOrDefault<State>();
			if (anyState == null)
			{
				throw new NotImplementedException("Learning reformat date with no inputs?");
			}
			LearningTask learningTask = task.MakeSubtask(formatDateRule, 0, null).MakeSubtask(this._build.UnnamedConversion.inputDateTime_parsedDateTime, 0, null).MakeSubtask(this._build.Rule.ParsePartialDateTime, 0, task.Spec);
			IEnumerable<JoinProgramSetBuilder<datetime>> enumerable = this.LearnInputDateForOutputFormatInfo(engine, learningTask, cancel, outputFormatInfo).SelectMany((JoinProgramSetBuilder<inputDateTime> join) => this.ApplyPossibleRoundingSpecs(join, task, outputFormatInfo));
			if (outputFormatInfo.Format == null)
			{
				return enumerable.SelectMany((JoinProgramSetBuilder<datetime> conversionParseDateSet) => this.LearnReformatDateForParseWithoutExamples(conversionParseDateSet, outputFormatInfo, anyState));
			}
			return Seq.Of<ProgramSetBuilder<conv>>(new ProgramSetBuilder<conv>[] { this._build.Set.Join.FormatPartialDateTime(enumerable.NormalizedUnion<datetime>(), this.BuildOutputDtFormatProgramSet(outputFormatInfo.Format)) });
		}

		// Token: 0x0600FA57 RID: 64087 RVA: 0x00355AFC File Offset: 0x00353CFC
		private IEnumerable<JoinProgramSetBuilder<datetime>> ApplyPossibleRoundingSpecs(JoinProgramSetBuilder<inputDateTime> res, LearningTask task, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			if (outputFormatInfo.OutputDtMatchByState == null)
			{
				yield return this._build.Set.ExplicitUnnamedConversion.datetime_inputDateTime(res);
				yield break;
			}
			string columnName = this.GetColumnName(task);
			State[] inputsForIdx = task.Spec.ProvidedInputs.Select((State state) => this.BindColumnNameIfMissing(state, columnName)).ToArray<State>();
			Func<State, IReadOnlyCollection<DateTimeFormatMatch>> <>9__2;
			Func<DateTimeRoundingSpec, dtRoundingSpec> <>9__7;
			foreach (KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<inputDateTime>> parseCluster in res.ClusterOnInputTuple(inputsForIdx))
			{
				Optional<PartialDateTime>[] key = parseCluster.Key;
				if (!key.Any((Optional<PartialDateTime> value) => !value.HasValue))
				{
					IEnumerable<State> enumerable = inputsForIdx;
					Func<State, IReadOnlyCollection<DateTimeFormatMatch>> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (State input) => outputFormatInfo.OutputDtMatchByState[input]);
					}
					IEnumerable<IReadOnlyCollection<DateTimeFormatMatch>> enumerable2 = enumerable.Select(func);
					IReadOnlyList<DateTimeRoundingSpec> roundingSpecs = key.Select((Optional<PartialDateTime> value) => value.Value).ZipWith(enumerable2).AggregateSeedFunc((Record<PartialDateTime, IReadOnlyCollection<DateTimeFormatMatch>> record) => record.Item2.Select((DateTimeFormatMatch output) => Witnesses.GetPossibleExplanations(record.Item1, output.PartialDateTime)).Union<DateTimeRoundingSpec>(), (IEnumerable<DateTimeRoundingSpec> accum, Record<PartialDateTime, IReadOnlyCollection<DateTimeFormatMatch>> record) => accum.Where((DateTimeRoundingSpec roundingSpec) => record.Item2.Any((DateTimeFormatMatch output) => Witnesses.Explains(record.Item1, output.PartialDateTime, roundingSpec))))
						.ToList<DateTimeRoundingSpec>();
					if (roundingSpecs.Contains(null))
					{
						yield return this._build.Set.ExplicitUnnamedConversion.datetime_inputDateTime(parseCluster.Value);
						if (roundingSpecs.Count == 1)
						{
							continue;
						}
					}
					Symbol dtRoundingSpec = this._build.Symbol.dtRoundingSpec;
					IEnumerable<DateTimeRoundingSpec> enumerable3 = roundingSpecs.Where((DateTimeRoundingSpec roundingSpec) => roundingSpec != null);
					Func<DateTimeRoundingSpec, dtRoundingSpec> func2;
					if ((func2 = <>9__7) == null)
					{
						func2 = (<>9__7 = (DateTimeRoundingSpec roundingSpec) => this._build.Node.Rule.dtRoundingSpec(roundingSpec));
					}
					ProgramSetBuilder<dtRoundingSpec> programSetBuilder = ProgramSetBuilder.List<dtRoundingSpec>(dtRoundingSpec, enumerable3.Select(func2).ToArray<dtRoundingSpec>());
					yield return this._build.Set.ExplicitJoin.RoundPartialDateTime(parseCluster.Value, programSetBuilder);
					roundingSpecs = null;
					parseCluster = default(KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<inputDateTime>>);
				}
			}
			IEnumerator<KeyValuePair<Optional<PartialDateTime>[], ProgramSetBuilder<inputDateTime>>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600FA58 RID: 64088 RVA: 0x00355B24 File Offset: 0x00353D24
		private ProgramSetBuilder<conv> LearnDateRangeForOutputFormatInfo(SynthesisEngine engine, BlackBoxRule formatDateTimeRangeRule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			cancel.ThrowIfCancellationRequested();
			LearningTask learningTask = task.MakeSubtask(formatDateTimeRangeRule, 0, null).MakeSubtask(this._build.UnnamedConversion.inputDateTime_parsedDateTime, 0, null).MakeSubtask(this._build.Rule.ParsePartialDateTime, 0, task.Spec);
			IEnumerable<JoinProgramSetBuilder<inputDateTime>> enumerable = this.LearnInputDateForOutputFormatInfo(engine, learningTask, cancel, outputFormatInfo);
			return this._build.Set.Join.FormatDateTimeRange(enumerable.NormalizedUnion<inputDateTime>(), this.BuildOutputDtFormatProgramSet(outputFormatInfo.Format), ProgramSetBuilder.List<s>(new s[] { this._build.Node.Rule.s(outputFormatInfo.RangeFormat.Separator) }), ProgramSetBuilder.List<dtRoundingSpec>(new dtRoundingSpec[] { this._build.Node.Rule.dtRoundingSpec(outputFormatInfo.RangeFormat.LowerRoundingSpec) }), ProgramSetBuilder.List<dtRoundingSpec>(new dtRoundingSpec[] { this._build.Node.Rule.dtRoundingSpec(outputFormatInfo.RangeFormat.UpperRoundingSpec) }));
		}

		// Token: 0x0600FA59 RID: 64089 RVA: 0x00355C44 File Offset: 0x00353E44
		private DisjunctiveExamplesSpec RemovePrefixExamplesFromSpec(DisjunctiveExamplesSpec spec, out List<State> additionalInputs, out PrefixOutputSpec prefixOutputSpec)
		{
			HashSet<State> statesWithPrefixSetExamples = (from kv in spec.DisjunctiveExamples
				where kv.Value.Any((object o) => o is StringPrefixSet)
				select kv.Key).ConvertToHashSet<State>();
			prefixOutputSpec = null;
			additionalInputs = statesWithPrefixSetExamples.ToList<State>();
			if (!statesWithPrefixSetExamples.Any<State>())
			{
				return spec;
			}
			prefixOutputSpec = new PrefixOutputSpec(statesWithPrefixSetExamples.ToDictionary((State state) => state, (State state) => spec.DisjunctiveExamples[state].OfType<StringPrefixSet>()));
			return DisjunctiveExamplesSpec.From(spec.DisjunctiveExamples.Where((KeyValuePair<State, IEnumerable<object>> kv) => !statesWithPrefixSetExamples.Contains(kv.Key)).ToDictionary((KeyValuePair<State, IEnumerable<object>> kv) => kv.Key, (KeyValuePair<State, IEnumerable<object>> kv) => kv.Value));
		}

		// Token: 0x0600FA5A RID: 64090 RVA: 0x00355D84 File Offset: 0x00353F84
		private LearningTask<TSpec> RemovePrefixExamplesFromTask<TSpec>(LearningTask<TSpec> task, out PrefixOutputSpec prefixOutputSpec) where TSpec : Spec
		{
			prefixOutputSpec = null;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec = task.Spec as DisjunctiveExamplesSpec;
			if (disjunctiveExamplesSpec == null)
			{
				return task;
			}
			List<State> list;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec2 = this.RemovePrefixExamplesFromSpec(disjunctiveExamplesSpec, out list, out prefixOutputSpec);
			LearningTask<TSpec> learningTask = task.Clone(null, disjunctiveExamplesSpec2).Cast<TSpec>();
			learningTask.AdditionalInputs = learningTask.AdditionalInputs.Concat(list).ToList<State>();
			return learningTask;
		}

		// Token: 0x0600FA5B RID: 64091 RVA: 0x00355DE0 File Offset: 0x00353FE0
		[RuleLearner("FormatPartialDateTime")]
		private Optional<ProgramSet> LearnReformatDate(SynthesisEngine engine, BlackBoxRule formatDateRule, LearningTask<Spec> task, CancellationToken cancel)
		{
			PrefixOutputSpec prefixOutputSpec;
			LearningTask<Spec> taskWithoutPrefixSets = this.RemovePrefixExamplesFromTask<Spec>(task, out prefixOutputSpec);
			if ((this._options.AllowedTransformations & TransformationKind.FormatDateTime) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if ((this._options.AllowedTransformations & TransformationKind.ParseDateTime) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if (!object.Equals(this._options.OutputType, UnknownType.Instance) && !(this._options.OutputType is IType<PartialDateTime>))
			{
				return OptionalUtils.Some((T)null);
			}
			if (!task.ProvidedInputs.Any<State>())
			{
				return OptionalUtils.Some((T)null);
			}
			ProgramSetBuilder<conv> programSetBuilder = this.GetPossibleOutputFormatInfosFromConstraints(taskWithoutPrefixSets.Spec).SelectMany((Witnesses.OutputDateTimeFormatInfo outputFormatInfo) => this.LearnReformatDateForOutputFormatInfo(engine, formatDateRule, taskWithoutPrefixSets, cancel, outputFormatInfo)).NormalizedUnion<conv>();
			ProgramSet programSet = ((programSetBuilder != null) ? programSetBuilder.Set : null);
			if (prefixOutputSpec == null)
			{
				return programSet.Some<ProgramSet>();
			}
			return ((programSet != null) ? programSet.Filter(prefixOutputSpec) : null).Some<ProgramSet>();
		}

		// Token: 0x0600FA5C RID: 64092 RVA: 0x00355EEC File Offset: 0x003540EC
		private ProgramSetBuilder<SS> LearnSubstringForParseDate(SynthesisEngine engine, LearningTask task, CancellationToken cancel, string columnName, IReadOnlyDictionary<State, IImmutableList<DateTimeFormatMatch>> inputMatchesByInput)
		{
			Symbol inputSymbol = base.Grammar.InputSymbol;
			Dictionary<State, IEnumerable<object>> dictionary = inputMatchesByInput.Where((KeyValuePair<State, IImmutableList<DateTimeFormatMatch>> kv) => kv.Value.Count > 0).ToDictionary((KeyValuePair<State, IImmutableList<DateTimeFormatMatch>> kv) => kv.Key, (KeyValuePair<State, IImmutableList<DateTimeFormatMatch>> kv) => kv.Value.Select((DateTimeFormatMatch inputMatch) => ValueSubstring.Create(inputMatch.Region.Source, new uint?(inputMatch.Region.Start), new uint?(inputMatch.Region.End), null, null)).ConvertToHashSet<object>());
			ProgramSetBuilder<SS> programSetBuilder = null;
			if (dictionary.Count != 0)
			{
				DisjunctiveExamplesSpec disjunctiveExamplesSpec = DisjunctiveExamplesSpec.From(dictionary);
				LearningTask learningTask = task.Clone(null, disjunctiveExamplesSpec);
				if (task.K.HasValue)
				{
					learningTask = learningTask.WithTopKPrograms(new int?(task.K.Value + 10));
				}
				programSetBuilder = this._build.Set.Cast.SS(engine.Learn(learningTask, cancel));
			}
			int num = inputMatchesByInput.Count(delegate(KeyValuePair<State, IImmutableList<DateTimeFormatMatch>> kv)
			{
				IEnumerable<string> enumerable = kv.Value.Select((DateTimeFormatMatch m) => m.Region.Value);
				ValueSubstring valueSubstring = Semantics.ChooseInput((IRow)kv.Key[inputSymbol], columnName);
				return enumerable.Contains((valueSubstring != null) ? valueSubstring.Value : null);
			});
			if (num > 0 && num >= inputMatchesByInput.Count / 2)
			{
				SS ss = this._build.Node.Rule.WholeColumn(this._build.Node.Variable.x);
				if (!programSetBuilder.Set.Contains(ss.Node))
				{
					programSetBuilder = new ProgramSetBuilder<SS>[]
					{
						programSetBuilder,
						ProgramSetBuilder.List<SS>(new SS[] { ss })
					}.NormalizedUnion<SS>();
				}
			}
			return programSetBuilder;
		}

		// Token: 0x0600FA5D RID: 64093 RVA: 0x0035607B File Offset: 0x0035427B
		private IEnumerable<JoinProgramSetBuilder<inputDateTime>> LearnInputDateForOutputFormatInfo(SynthesisEngine engine, LearningTask task, CancellationToken cancel, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			Witnesses.<>c__DisplayClass71_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass71_0();
			CS$<>8__locals1.outputFormatInfo = outputFormatInfo;
			CS$<>8__locals1.<>4__this = this;
			Spec spec = task.Spec;
			State state3;
			if (spec == null)
			{
				state3 = null;
			}
			else
			{
				IReadOnlyList<State> providedInputs = spec.ProvidedInputs;
				state3 = ((providedInputs != null) ? providedInputs.FirstOrDefault<State>() : null);
			}
			State state2 = state3;
			if (state2 == null)
			{
				throw new NotImplementedException("Unable to learn date parsing from no input at all.");
			}
			object obj;
			if (!state2.TryGetValue(this._build.Symbol.x, out obj))
			{
				if (!state2.TryGetValue(this._build.Symbol.cell, out obj))
				{
					throw new NotImplementedException("State has neither x nor cell set?");
				}
				if (CS$<>8__locals1.outputFormatInfo.OutputDtMatchByState != null)
				{
					foreach (KeyValuePair<State, IReadOnlyCollection<DateTimeFormatMatch>> keyValuePair in CS$<>8__locals1.outputFormatInfo.OutputDtMatchByState)
					{
						PartialDateTime inputDate2 = Semantics.AsPartialDateTime(keyValuePair.Key[this._build.Symbol.cell]);
						if (inputDate2 == null || keyValuePair.Value.Any((DateTimeFormatMatch outputDate) => !Witnesses.ExplainsAllowingRounding(inputDate2, outputDate.PartialDateTime)))
						{
							yield break;
						}
					}
				}
				if (CS$<>8__locals1.outputFormatInfo.OutputDtRangeByState != null)
				{
					foreach (KeyValuePair<State, IReadOnlyCollection<Witnesses.DateTimeRangeMatch>> keyValuePair2 in CS$<>8__locals1.outputFormatInfo.OutputDtRangeByState)
					{
						PartialDateTime inputDate = Semantics.AsPartialDateTime(keyValuePair2.Key[this._build.Symbol.cell]);
						if (inputDate == null || keyValuePair2.Value.Any((Witnesses.DateTimeRangeMatch outputRange) => !Witnesses.IsBetween(inputDate, outputRange.Lower, outputRange.Upper, CS$<>8__locals1.outputFormatInfo.Parts)))
						{
							yield break;
						}
					}
				}
				yield return this._build.Set.ExplicitJoin.AsPartialDateTime(ProgramSetBuilder.List<cell>(new cell[] { this._build.Node.Variable.cell }));
				yield break;
			}
			else
			{
				IImmutableList<State> exampleInputStatesIfAny = this.GetExampleInputStatesIfAny(task);
				CS$<>8__locals1.columnName = this.GetColumnName(task);
				IReadOnlyList<State> readOnlyList;
				IReadOnlyList<State> readOnlyList2;
				if ((readOnlyList = task.AdditionalInputs as IReadOnlyList<State>) == null)
				{
					IEnumerable<State> additionalInputs = task.AdditionalInputs;
					readOnlyList2 = ((additionalInputs != null) ? additionalInputs.ToList<State>() : null);
					readOnlyList = readOnlyList2 ?? new State[0];
				}
				IReadOnlyList<State> readOnlyList3 = (from state in readOnlyList.RandomlySample(this._options.RandomSeed, this._options.AdditionalInputsSampleSize)
					select CS$<>8__locals1.<>4__this.BindColumnNameIfMissing(state, CS$<>8__locals1.columnName)).ToList<State>();
				Witnesses.<>c__DisplayClass71_0 CS$<>8__locals4 = CS$<>8__locals1;
				readOnlyList2 = exampleInputStatesIfAny;
				CS$<>8__locals4.inputMatchesByInput = (readOnlyList2 ?? readOnlyList3).ToDictionary((State state) => state, (State state) => CS$<>8__locals1.<>4__this.GetInputMatches(state, CS$<>8__locals1.outputFormatInfo));
				ProgramSetBuilder<SS> programSetBuilder = this.LearnSubstringForParseDate(engine, task, cancel, CS$<>8__locals1.columnName, CS$<>8__locals1.inputMatchesByInput);
				if (ProgramSetBuilder.IsNullOrEmpty<SS>(programSetBuilder))
				{
					yield break;
				}
				IEnumerable<State> enumerable = exampleInputStatesIfAny;
				IEnumerable<State> enumerable2 = (enumerable ?? Enumerable.Empty<State>()).Concat(readOnlyList3);
				State[] inputsForIdx = enumerable2.Select((State state) => CS$<>8__locals1.<>4__this.BindColumnNameIfMissing(state, CS$<>8__locals1.columnName)).ToArray<State>();
				IReadOnlyList<IImmutableList<DateTimeFormatMatch>> inputMatchesByIdx = (from input in inputsForIdx.TakeWhile((State input) => CS$<>8__locals1.inputMatchesByInput.ContainsKey(input))
					select CS$<>8__locals1.inputMatchesByInput[input]).ToList<IImmutableList<DateTimeFormatMatch>>();
				foreach (KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<SS>> keyValuePair3 in programSetBuilder.ClusterOnInputTuple(inputsForIdx))
				{
					Optional<ValueSubstring>[] key = keyValuePair3.Key;
					if ((double)key.Count((Optional<ValueSubstring> v) => v.HasValue) / (double)key.Length >= 0.5)
					{
						IEnumerable<IEnumerable<DateTimeFormatMatch>> enumerable3 = inputMatchesByIdx.Zip(key, (IImmutableList<DateTimeFormatMatch> inputMatches, Optional<ValueSubstring> ss) => inputMatches.Where(delegate(DateTimeFormatMatch im)
						{
							string value = im.Region.Value;
							ValueSubstring valueSubstring = ss.OrElseDefault<ValueSubstring>();
							return value == ((valueSubstring != null) ? valueSubstring.Value : null);
						}));
						IEnumerable<DateTimeFormat[]> enumerable4 = this.SelectInputDateTimeFormats_AllCombinations(task, enumerable3, key, inputsForIdx, CS$<>8__locals1.outputFormatInfo, task.K);
						if (enumerable4 != null)
						{
							Symbol inputDtFormats = this._build.Symbol.inputDtFormats;
							IEnumerable<DateTimeFormat[]> enumerable5 = enumerable4;
							Func<DateTimeFormat[], inputDtFormats> func;
							if ((func = CS$<>8__locals1.<>9__10) == null)
							{
								func = (CS$<>8__locals1.<>9__10 = (DateTimeFormat[] values) => CS$<>8__locals1.<>4__this._build.Node.Rule.inputDtFormats(values));
							}
							ProgramSetBuilder<inputDtFormats> programSetBuilder2 = ProgramSetBuilder.List<inputDtFormats>(inputDtFormats, enumerable5.Select(func).ToArray<inputDtFormats>());
							JoinProgramSetBuilder<parsedDateTime> joinProgramSetBuilder = this._build.Set.ExplicitJoin.ParsePartialDateTime(keyValuePair3.Value, programSetBuilder2);
							if (!joinProgramSetBuilder.Set.IsEmpty)
							{
								yield return this._build.Set.ExplicitUnnamedConversion.inputDateTime_parsedDateTime(joinProgramSetBuilder);
							}
						}
					}
				}
				IEnumerator<KeyValuePair<Optional<ValueSubstring>[], ProgramSetBuilder<SS>>> enumerator3 = null;
				yield break;
			}
			yield break;
		}

		// Token: 0x0600FA5E RID: 64094 RVA: 0x003560A8 File Offset: 0x003542A8
		private static bool IsValidInputDateTimeFormat(DateTimeFormat format, ValueSubstring inputString, Optional<IReadOnlyCollection<DateTimeFormatMatch>> outputDtMatches)
		{
			return DateFormatCache.Parse(format, inputString).Select(delegate(DateTimeFormatMatch inputMatch)
			{
				Func<DateTimeFormatMatch, bool> <>9__2;
				return outputDtMatches.Select(delegate(IReadOnlyCollection<DateTimeFormatMatch> outputMatches)
				{
					Func<DateTimeFormatMatch, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (DateTimeFormatMatch outputMatch) => Witnesses.GetPossibleExplanations(inputMatch.PartialDateTime, outputMatch.PartialDateTime).Any<DateTimeRoundingSpec>());
					}
					return outputMatches.Any(func);
				}).OrElse(true);
			}).OrElseDefault<bool>();
		}

		// Token: 0x0600FA5F RID: 64095 RVA: 0x003560DF File Offset: 0x003542DF
		private static bool Explains(PartialDateTime input, PartialDateTime output, DateTimeRoundingSpec roundingSpec)
		{
			if (roundingSpec != null)
			{
				input = Semantics.RoundPartialDateTime(input, roundingSpec);
				if (input == null)
				{
					return false;
				}
			}
			return input.Explains(output);
		}

		// Token: 0x0600FA60 RID: 64096 RVA: 0x003560FF File Offset: 0x003542FF
		private static IEnumerable<DateTimeRoundingSpec> GetPossibleExplanations(PartialDateTime input, PartialDateTime output)
		{
			if (input.Explains(output))
			{
				yield return null;
			}
			DateTimePart? smallestStandardDateTimePart = Semantics.GetSmallestStandardDateTimePart(output);
			if (smallestStandardDateTimePart == null)
			{
				yield break;
			}
			for (DateTimePart part = smallestStandardDateTimePart.Value; part != DateTimePart.Day; part = Semantics.GetNextLargerPart(part))
			{
				int foundAmount = output.Get(part).Value;
				foreach (DateTimeRoundingSpec dateTimeRoundingSpec in Witnesses.GetDateTimeRoundingSpecs(part))
				{
					if ((long)foundAmount % (long)((ulong)dateTimeRoundingSpec.Delta) == 0L && Witnesses.Explains(input, output, dateTimeRoundingSpec))
					{
						yield return dateTimeRoundingSpec;
					}
				}
				IEnumerator<DateTimeRoundingSpec> enumerator = null;
				if (foundAmount != 0)
				{
					break;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0600FA61 RID: 64097 RVA: 0x00356116 File Offset: 0x00354316
		private static bool ExplainsAllowingRounding(PartialDateTime input, PartialDateTime output)
		{
			return Witnesses.GetPossibleExplanations(input, output).Any<DateTimeRoundingSpec>();
		}

		// Token: 0x0600FA62 RID: 64098 RVA: 0x00356124 File Offset: 0x00354324
		private static IReadOnlyList<DateTimeRoundingSpec> PrebuildDateTimeRoundingSpecs(IEnumerable<int> amounts, DateTimePart part, IEnumerable<RoundingMode> modes)
		{
			return modes.SelectMany((RoundingMode mode) => amounts.Select((int amount) => new DateTimeRoundingSpec(PartialDateTime.Empty, (uint)amount, part, mode, null, 0U))).ToList<DateTimeRoundingSpec>();
		}

		// Token: 0x0600FA63 RID: 64099 RVA: 0x0035615C File Offset: 0x0035435C
		private static IEnumerable<DateTimeRoundingSpec> GetDateTimeRoundingSpecs(DateTimePart part)
		{
			switch (part)
			{
			case DateTimePart.Year:
				return Witnesses.YearRoundings;
			case DateTimePart.Hour:
				return Witnesses.HourRoundings;
			case DateTimePart.Minute:
				return Witnesses.MinuteRoundings;
			case DateTimePart.Second:
				return Witnesses.SecondRoundings;
			}
			return Witnesses.EmptyRoundings;
		}

		// Token: 0x0600FA64 RID: 64100 RVA: 0x0035619C File Offset: 0x0035439C
		private IImmutableList<DateTimeFormatMatch> GetInputMatches(State state, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			return (from inputMatch in DateFormatCache.AllFormatMatchesFor((ValueSubstring)state[this._build.Symbol.x], DateFormatCache.ParseMode.Partial, HeuristicsMode.AllowMostFormats)
				where Witnesses.IsReasonableMatchOnInputHeuristic(inputMatch)
				where Witnesses.IsValidInputMatch(inputMatch, state, outputFormatInfo)
				select inputMatch).ToImmutableList<DateTimeFormatMatch>();
		}

		// Token: 0x0600FA65 RID: 64101 RVA: 0x00356220 File Offset: 0x00354420
		private static bool IsValidInputMatch(DateTimeFormatMatch inputMatch, State state, Witnesses.OutputDateTimeFormatInfo outputFormatInfo)
		{
			MultiValueDictionary<State, DateTimeFormatMatch> outputDtMatchByState = outputFormatInfo.OutputDtMatchByState;
			Optional<IReadOnlyCollection<DateTimeFormatMatch>> optional = ((outputDtMatchByState != null) ? outputDtMatchByState.MaybeGet(state) : Optional<IReadOnlyCollection<DateTimeFormatMatch>>.Nothing);
			if (!outputFormatInfo.AllowNumericInputFormats && inputMatch.DateTimeFormat.IsNumeric && optional.HasValue)
			{
				if (optional.Value.Select((DateTimeFormatMatch outputDtMatch) => outputDtMatch.Region).Any((StringRegion outputMatch) => inputMatch.Region.Value.Contains(outputMatch.Value)))
				{
					return false;
				}
			}
			if (optional.HasValue)
			{
				return optional.Value.Any((DateTimeFormatMatch outputDtMatch) => Witnesses.ExplainsAllowingRounding(inputMatch.PartialDateTime, outputDtMatch.PartialDateTime));
			}
			MultiValueDictionary<State, Witnesses.DateTimeRangeMatch> outputDtRangeByState = outputFormatInfo.OutputDtRangeByState;
			Optional<IReadOnlyCollection<Witnesses.DateTimeRangeMatch>> optional2 = ((outputDtRangeByState != null) ? outputDtRangeByState.MaybeGet(state) : Optional<IReadOnlyCollection<Witnesses.DateTimeRangeMatch>>.Nothing);
			if (optional2.HasValue)
			{
				return optional2.Value.Any((Witnesses.DateTimeRangeMatch match) => Witnesses.IsBetween(inputMatch.PartialDateTime, match.Lower, match.Upper, outputFormatInfo.Parts));
			}
			return outputFormatInfo.Parts == null || inputMatch.DateTimeFormat.MatchedParts.CanExplain(outputFormatInfo.Parts.Value);
		}

		// Token: 0x0600FA66 RID: 64102 RVA: 0x00356368 File Offset: 0x00354568
		private static bool IsBetween(PartialDateTime candidate, PartialDateTime lower, PartialDateTime upper, DateTimePartSet? parts)
		{
			if (!candidate.Parts.Intersect(DateTimePartSet.TimeParts).Any())
			{
				return false;
			}
			long totalMilliseconds = Semantics.GetTotalMilliseconds(candidate);
			long totalMilliseconds2 = Semantics.GetTotalMilliseconds(lower);
			long totalMilliseconds3 = Semantics.GetTotalMilliseconds(upper);
			if (totalMilliseconds2 > totalMilliseconds3)
			{
				return totalMilliseconds2 <= totalMilliseconds || totalMilliseconds < totalMilliseconds3;
			}
			return totalMilliseconds2 <= totalMilliseconds && totalMilliseconds < totalMilliseconds3;
		}

		// Token: 0x0600FA67 RID: 64103 RVA: 0x003563C4 File Offset: 0x003545C4
		private IEnumerable<DateTimeFormat[]> SelectInputDateTimeFormats_AllCombinations(LearningTask task, IEnumerable<IEnumerable<DateTimeFormatMatch>> inputMatchesForCluster, IReadOnlyList<Optional<ValueSubstring>> substringValues, IReadOnlyList<State> inputsForIdx, Witnesses.OutputDateTimeFormatInfo outputFormatInfo, Optional<int> k)
		{
			DateTimePartSet? parts = outputFormatInfo.Parts;
			bool flag = parts == null || !parts.Value.Any();
			MultiValueDictionary<State, DateTimeFormatMatch> outputDtMatchByState = outputFormatInfo.OutputDtMatchByState;
			int numExamples = ((task.Spec is DisjunctiveExamplesSpec) ? task.Spec.ProvidedInputs.Count<State>() : 0);
			List<HashSet<DateTimeFormat>> formatsForSubstringProgram = new List<HashSet<DateTimeFormat>>();
			using (IEnumerator<HashSet<DateTimeFormat>> enumerator = inputMatchesForCluster.Select((IEnumerable<DateTimeFormatMatch> inputMatches) => inputMatches.Select((DateTimeFormatMatch inputMatch) => inputMatch.DateTimeFormat).ConvertToHashSet<DateTimeFormat>()).Take(task.Spec.ProvidedInputs.Count<State>()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					HashSet<DateTimeFormat> formatSet = enumerator.Current;
					if (!formatsForSubstringProgram.Any((HashSet<DateTimeFormat> l) => l.Intersect(formatSet).Any<DateTimeFormat>()))
					{
						formatsForSubstringProgram.Add(formatSet);
					}
				}
			}
			if (formatsForSubstringProgram.Any((HashSet<DateTimeFormat> f) => f.Count == 0))
			{
				return null;
			}
			if (this._options.ParseSingleDateFormat && formatsForSubstringProgram.Count > 1)
			{
				return null;
			}
			List<DateTimeFormat> formatsForExamples = ((task.Spec is DisjunctiveExamplesSpec) ? formatsForSubstringProgram.Union<DateTimeFormat>() : Enumerable.Empty<DateTimeFormat>()).ToList<DateTimeFormat>();
			List<DateTimePart> explainableParts = null;
			if (flag)
			{
				explainableParts = DateTimePartList.AllDateTime.Where(delegate(DateTimePart part)
				{
					Func<DateTimeFormat, bool> <>9__16;
					return formatsForSubstringProgram.All(delegate(HashSet<DateTimeFormat> l)
					{
						Func<DateTimeFormat, bool> func2;
						if ((func2 = <>9__16) == null)
						{
							func2 = (<>9__16 = (DateTimeFormat f) => f.MatchedParts.CanExplain(part));
						}
						return l.Any(func2);
					});
				}).ToList<DateTimePart>();
			}
			int num = Math.Max(10, Math.Min(50, substringValues.Count / 10));
			Predicate<DateTimeFormat> <>9__21;
			Predicate<DateTimeFormat> <>9__23;
			int i;
			for (i = 0; i < substringValues.Count; i++)
			{
				if (substringValues[i].HasValue)
				{
					ValueSubstring computedInputString = substringValues[i].Value;
					State state = inputsForIdx[i];
					Optional<IReadOnlyCollection<DateTimeFormatMatch>> outputDtMatches = ((outputDtMatchByState != null) ? outputDtMatchByState.MaybeGet(state) : Optional<IReadOnlyCollection<DateTimeFormatMatch>>.Nothing);
					Func<DateTimeFormat, bool> isValidFormatForComputedInputString = (DateTimeFormat format) => Witnesses.IsValidInputDateTimeFormat(format, computedInputString, outputDtMatches);
					if (!formatsForSubstringProgram.Any((HashSet<DateTimeFormat> formats) => formats.All(isValidFormatForComputedInputString)))
					{
						HashSet<DateTimeFormat> hashSet = formatsForSubstringProgram.FirstOrDefault((HashSet<DateTimeFormat> formats) => formats.Any(isValidFormatForComputedInputString));
						if (hashSet != null)
						{
							hashSet.RemoveWhere((DateTimeFormat format) => !isValidFormatForComputedInputString(format));
						}
						else
						{
							HashSet<DateTimeFormat> newInputFormats = Witnesses.GetDateTimeFormatsForInput(parts, computedInputString, outputDtMatches);
							if (newInputFormats.Any<DateTimeFormat>())
							{
								if (flag)
								{
									HashSet<DateTimeFormat> newInputFormats3 = newInputFormats;
									Predicate<DateTimeFormat> predicate;
									if ((predicate = <>9__21) == null)
									{
										predicate = (<>9__21 = (DateTimeFormat f) => explainableParts.All((DateTimePart part) => !f.MatchedParts.CanExplain(part)));
									}
									newInputFormats3.RemoveWhere(predicate);
									explainableParts.RemoveAll((DateTimePart part) => newInputFormats.All((DateTimeFormat f) => !f.MatchedParts.CanExplain(part)));
									HashSet<DateTimeFormat> newInputFormats2 = newInputFormats;
									Predicate<DateTimeFormat> predicate2;
									if ((predicate2 = <>9__23) == null)
									{
										predicate2 = (<>9__23 = (DateTimeFormat f) => !f.MatchedParts.CanExplain(explainableParts));
									}
									newInputFormats2.RemoveWhere(predicate2);
								}
								formatsForSubstringProgram.Add(newInputFormats);
							}
							else if (--num <= 0)
							{
								return null;
							}
						}
					}
				}
			}
			if (flag)
			{
				IEnumerable<DateTimePartSet> matchedParts = formatsForSubstringProgram.SelectMany((HashSet<DateTimeFormat> l) => l.Select((DateTimeFormat f) => f.MatchedParts)).Distinct<DateTimePartSet>();
				explainableParts.RemoveAll((DateTimePart part) => !matchedParts.All((DateTimePartSet mp) => mp.CanExplain(part)));
				if (!explainableParts.Any<DateTimePart>())
				{
					return null;
				}
			}
			IEnumerable<HashSet<DateTimeFormat>> formatsForSubstringProgram2 = formatsForSubstringProgram;
			Func<HashSet<DateTimeFormat>, IReadOnlyList<IReadOnlyList<DateTimeFormat>>> func;
			if ((func = Witnesses.<>O.<4>__GroupParsingFormats) == null)
			{
				func = (Witnesses.<>O.<4>__GroupParsingFormats = new Func<HashSet<DateTimeFormat>, IReadOnlyList<IReadOnlyList<DateTimeFormat>>>(DateTimeFormatUtil.GroupParsingFormats));
			}
			List<IReadOnlyList<IReadOnlyList<DateTimeFormat>>> list = formatsForSubstringProgram2.Select(func).ToList<IReadOnlyList<IReadOnlyList<DateTimeFormat>>>();
			int num2 = list.Count((IReadOnlyList<IReadOnlyList<DateTimeFormat>> l) => l.Count > 1);
			Optional<int> numberOfFormatsPerList;
			if (num2 <= 2)
			{
				numberOfFormatsPerList = Optional<int>.Nothing;
			}
			else
			{
				int num3 = Math.Min(k.OrElse(1000), 1000);
				numberOfFormatsPerList = Math.Max(1, (int)Math.Ceiling(Math.Pow((double)num3, 1.0 / (double)num2))).Some<int>();
			}
			Func<DateTimeFormat, double> <>9__30;
			Func<IReadOnlyList<DateTimeFormat>, double> scoreGroup = delegate(IReadOnlyList<DateTimeFormat> group)
			{
				Func<DateTimeFormat, double> func3;
				if ((func3 = <>9__30) == null)
				{
					func3 = (<>9__30 = (DateTimeFormat i) => RankingSubfeatureCalculator._InputDtFormatScore(i, this._options.AvoidImperialDateTimeFormat));
				}
				return group.Select(func3).Max();
			};
			Func<IReadOnlyList<DateTimeFormat>, IEnumerable<DateTimeFormat>> <>9__31;
			IGrouping<int, IReadOnlyList<IReadOnlyList<DateTimeFormat>>> grouping = (from g in (from g in list.Select(delegate(IReadOnlyList<IReadOnlyList<DateTimeFormat>> g)
					{
						if (!numberOfFormatsPerList.HasValue)
						{
							return g.TopK(scoreGroup, g.Count, null, null);
						}
						return g.TopK(scoreGroup, numberOfFormatsPerList.Value, null, null).Take(numberOfFormatsPerList.Value);
					}).CartesianProduct<IReadOnlyList<DateTimeFormat>>()
					select g.ToList<IReadOnlyList<DateTimeFormat>>()).Select(delegate(List<IReadOnlyList<DateTimeFormat>> g)
				{
					Witnesses.<>c__DisplayClass86_9 CS$<>8__locals10 = new Witnesses.<>c__DisplayClass86_9();
					Witnesses.<>c__DisplayClass86_9 CS$<>8__locals11 = CS$<>8__locals10;
					Func<IReadOnlyList<DateTimeFormat>, IEnumerable<DateTimeFormat>> func4;
					if ((func4 = <>9__31) == null)
					{
						func4 = (<>9__31 = (IReadOnlyList<DateTimeFormat> f) => formatsForExamples.Where(new Func<DateTimeFormat, bool>(f.ContainsParsingFormat)));
					}
					CS$<>8__locals11.exampleFormats = g.SelectMany(func4).ToArray<DateTimeFormat>();
					if ((from value in substringValues.Take(numExamples)
						select value.OrElseDefault<ValueSubstring>()).Any((ValueSubstring input) => Semantics.ParsePartialDateTime(input, CS$<>8__locals10.exampleFormats) == null))
					{
						return null;
					}
					return (from f in g.Select(delegate(IReadOnlyList<DateTimeFormat> f)
						{
							Func<DateTimeFormat, bool> func5;
							if ((func5 = CS$<>8__locals10.<>9__36) == null)
							{
								func5 = (CS$<>8__locals10.<>9__36 = (DateTimeFormat format) => CS$<>8__locals10.exampleFormats.Any(new Func<DateTimeFormat, bool>(format.ContainsParsingFormat)) || CS$<>8__locals10.exampleFormats.All((DateTimeFormat exampleFormat) => !DateTimeFormatUtil.IsAmbiguous(exampleFormat, format)));
							}
							return f.Where(func5).ToList<DateTimeFormat>();
						})
						where f.Any<DateTimeFormat>()
						select f).ToList<List<DateTimeFormat>>();
				})
				where g != null && g.Any<IReadOnlyList<DateTimeFormat>>()
				group g by g.Count).ArgMax((IGrouping<int, IReadOnlyList<IReadOnlyList<DateTimeFormat>>> g) => g.Key);
			IEnumerable<DateTimeFormat[]> enumerable;
			if (grouping == null)
			{
				enumerable = null;
			}
			else
			{
				enumerable = grouping.Select((IReadOnlyList<IReadOnlyList<DateTimeFormat>> g) => g.SelectMany((IReadOnlyList<DateTimeFormat> f) => f).ToArray<DateTimeFormat>());
			}
			IEnumerable<DateTimeFormat[]> enumerable2 = enumerable;
			if (this._options.ParseSingleDateFormat)
			{
				IEnumerable<DateTimeFormat[]> enumerable3;
				if (enumerable2 == null)
				{
					enumerable3 = null;
				}
				else
				{
					enumerable3 = enumerable2.Select(delegate(DateTimeFormat[] f)
					{
						if (f.Length == 1)
						{
							return f;
						}
						string shortestFormatString = f.Where((DateTimeFormat g) => f[0].FormatString.Contains(g.FormatString)).ArgMin((DateTimeFormat format) => format.FormatString.Length).FormatString;
						return f.Where((DateTimeFormat g) => g.FormatString.Contains(shortestFormatString)).ToArray<DateTimeFormat>();
					}).Distinct(ValueEquality<DateTimeFormat[]>.Instance);
				}
				enumerable2 = enumerable3;
			}
			return enumerable2;
		}

		// Token: 0x0600FA68 RID: 64104 RVA: 0x003569B4 File Offset: 0x00354BB4
		private static HashSet<DateTimeFormat> GetDateTimeFormatsForInput(DateTimePartSet? outputFormatParts, ValueSubstring computedInputString, Optional<IReadOnlyCollection<DateTimeFormatMatch>> outputMatches)
		{
			return (from match in (from match in DateFormatCache.AllFormatMatchesFor(computedInputString, DateFormatCache.ParseMode.FullLength, HeuristicsMode.AllowMostFormats)
					where Witnesses.IsReasonableMatchOnInputHeuristic(match)
					select match).Where(delegate(DateTimeFormatMatch match)
				{
					if (outputFormatParts != null && !match.DateTimeFormat.MatchedParts.CanExplain(outputFormatParts.Value))
					{
						return false;
					}
					if (outputMatches.HasValue)
					{
						return outputMatches.Value.Select((DateTimeFormatMatch outputMatch) => outputMatch.PartialDateTime).Any((PartialDateTime outputMatch) => Witnesses.ExplainsAllowingRounding(match.PartialDateTime, outputMatch));
					}
					return true;
				})
				select match.DateTimeFormat).ConvertToHashSet<DateTimeFormat>();
		}

		// Token: 0x0600FA69 RID: 64105 RVA: 0x00356A3C File Offset: 0x00354C3C
		private static PartialDateTime SubtractDateTime(PartialDateTime dt1, PartialDateTime dt2)
		{
			long num = Semantics.GetTotalMilliseconds(dt1);
			long totalMilliseconds = Semantics.GetTotalMilliseconds(dt2);
			if (num < totalMilliseconds)
			{
				num += Semantics.GetMillisecondsForPart(Semantics.GetNextLargerPart(Semantics.GetLargestStandardTimePart(dt1) ?? DateTimePart.Hour));
			}
			return Semantics.GetPartialDateTimeOnlyTime(num - totalMilliseconds, null);
		}

		// Token: 0x0600FA6A RID: 64106 RVA: 0x00356A94 File Offset: 0x00354C94
		private bool IsReasonableDelta(PartialDateTime rangeLowerBound, long deltaMilliseconds, out uint delta, out DateTimePart deltaUnit, out PartialDateTime zero)
		{
			DateTimePart nextLargerPart = Semantics.GetNextLargerPart(DateTimePartList.StandardTimeDescending.First((DateTimePart part) => rangeLowerBound.Parts.Contains(part)));
			if (Semantics.GetMillisecondsForPart(nextLargerPart) % deltaMilliseconds != 0L)
			{
				delta = 0U;
				deltaUnit = DateTimePart.Year;
				zero = null;
				return false;
			}
			long totalMilliseconds = Semantics.GetTotalMilliseconds(rangeLowerBound);
			DateTimePart dateTimePart = nextLargerPart;
			long millisecondsForPart;
			for (;;)
			{
				dateTimePart = Semantics.GetNextSmallerPart(dateTimePart);
				millisecondsForPart = Semantics.GetMillisecondsForPart(dateTimePart);
				if (deltaMilliseconds % millisecondsForPart == 0L)
				{
					break;
				}
				if (dateTimePart == DateTimePart.Millisecond)
				{
					goto Block_4;
				}
			}
			delta = (uint)(deltaMilliseconds / millisecondsForPart);
			deltaUnit = dateTimePart;
			long num = totalMilliseconds % deltaMilliseconds;
			zero = ((num == 0L) ? PartialDateTime.Empty : Semantics.GetPartialDateTimeOnlyTime(num, null));
			return true;
			Block_4:
			delta = 0U;
			deltaUnit = DateTimePart.Year;
			zero = null;
			return false;
		}

		// Token: 0x0600FA6B RID: 64107 RVA: 0x00356B48 File Offset: 0x00354D48
		[RuleLearner("FormatDateTimeRange")]
		public Optional<ProgramSet> LearnFormatDateTimeRange(SynthesisEngine engine, BlackBoxRule rule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel)
		{
			if ((this._options.AllowedTransformations & TransformationKind.FormatDateTimeRange) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if ((this._options.AllowedTransformations & TransformationKind.ParseDateTime) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if (!this._options.OutputType.Equals(UnknownType.Instance))
			{
				return OptionalUtils.Some((T)null);
			}
			PrefixOutputSpec prefixOutputSpec;
			LearningTask<DisjunctiveExamplesSpec> taskWithoutPrefixSets = this.RemovePrefixExamplesFromTask<DisjunctiveExamplesSpec>(task, out prefixOutputSpec);
			HashSet<DateTimeFormat> formats = null;
			MultiValueDictionary<Witnesses.DateTimeRangeFormat, Witnesses.DateTimeRangeWithState> multiValueDictionary = null;
			using (IEnumerator<KeyValuePair<State, IEnumerable<object>>> enumerator = taskWithoutPrefixSets.Spec.DisjunctiveExamples.GetEnumerator())
			{
				Func<DateTimeFormatMatch, bool> <>9__2;
				while (enumerator.MoveNext())
				{
					KeyValuePair<State, IEnumerable<object>> ex = enumerator.Current;
					HashSet<DateTimeFormat> hashSet = new HashSet<DateTimeFormat>();
					MultiValueDictionary<Witnesses.DateTimeRangeFormat, Witnesses.DateTimeRangeWithState> multiValueDictionary2 = new MultiValueDictionary<Witnesses.DateTimeRangeFormat, Witnesses.DateTimeRangeWithState>();
					using (IEnumerator<ValueSubstring> enumerator2 = ex.Value.Cast<ValueSubstring>().GetEnumerator())
					{
						Func<DateTimeFormatMatch, bool> <>9__1;
						while (enumerator2.MoveNext())
						{
							ValueSubstring output = enumerator2.Current;
							IEnumerable<DateTimeFormatMatch> enumerable = DateFormatCache.AllFormatMatchesFor(output, DateFormatCache.ParseMode.Partial, HeuristicsMode.AllowMostFormats);
							Func<DateTimeFormatMatch, bool> func;
							if ((func = <>9__1) == null)
							{
								func = (<>9__1 = (DateTimeFormatMatch match) => this.IsReasonableMatchWithStateHeuristic(new Witnesses.MatchWithState(ex.Key, match)));
							}
							IEnumerable<DateTimeFormatMatch> enumerable2 = enumerable.Where(func);
							Func<DateTimeFormatMatch, bool> func2;
							if ((func2 = <>9__2) == null)
							{
								func2 = (<>9__2 = (DateTimeFormatMatch match) => match.PartialDateTime != null && match.PartialDateTime.Parts.Union(DateTimePartSet.TimeParts) == DateTimePartSet.TimeParts && (formats == null || formats.Contains(match.DateTimeFormat)));
							}
							List<DateTimeFormatMatch> list = enumerable2.Where(func2).ToList<DateTimeFormatMatch>();
							IEnumerable<DateTimeFormatMatch> enumerable3 = list.Where((DateTimeFormatMatch match1) => match1.Region.Start == output.Start);
							List<DateTimeFormatMatch> list2 = list.Where((DateTimeFormatMatch match2) => match2.Region.End == output.End).ToList<DateTimeFormatMatch>();
							foreach (DateTimeFormatMatch dateTimeFormatMatch in enumerable3)
							{
								foreach (DateTimeFormatMatch dateTimeFormatMatch2 in list2)
								{
									if (dateTimeFormatMatch.Region.End < dateTimeFormatMatch2.Region.Start && dateTimeFormatMatch.DateTimeFormat.Equals(dateTimeFormatMatch2.DateTimeFormat))
									{
										PartialDateTime partialDateTime = dateTimeFormatMatch.PartialDateTime;
										PartialDateTime partialDateTime2 = dateTimeFormatMatch2.PartialDateTime;
										string value = output.Slice(dateTimeFormatMatch.Region.End, new uint?(dateTimeFormatMatch2.Region.Start)).Value;
										if (!this._options.AllowConcat)
										{
											IEnumerable<char> enumerable4 = value;
											Func<char, bool> func3;
											if ((func3 = Witnesses.<>O.<5>__IsDigit) == null)
											{
												func3 = (Witnesses.<>O.<5>__IsDigit = new Func<char, bool>(char.IsDigit));
											}
											if (enumerable4.Any(func3))
											{
												continue;
											}
										}
										long totalMilliseconds = Semantics.GetTotalMilliseconds(Witnesses.SubtractDateTime(partialDateTime2, partialDateTime));
										if (totalMilliseconds != 0L)
										{
											foreach (Witnesses.ExclusionInfo exclusionInfo in Witnesses.DateTimeRangeExclusions)
											{
												long num = totalMilliseconds + exclusionInfo.Milliseconds;
												uint num2;
												DateTimePart dateTimePart;
												PartialDateTime partialDateTime3;
												if ((exclusionInfo.Milliseconds == 0L || num / exclusionInfo.Milliseconds * exclusionInfo.Milliseconds == num) && this.IsReasonableDelta(partialDateTime, num, out num2, out dateTimePart, out partialDateTime3))
												{
													hashSet.Add(dateTimeFormatMatch.DateTimeFormat);
													DateTimeRoundingSpec dateTimeRoundingSpec = new DateTimeRoundingSpec(partialDateTime3, num2, dateTimePart, RoundingMode.Down, null, 0U);
													DateTimeRoundingSpec dateTimeRoundingSpec2 = new DateTimeRoundingSpec(partialDateTime3, num2, dateTimePart, RoundingMode.UpOrNext, exclusionInfo.Unit, exclusionInfo.UnitAmount);
													Witnesses.DateTimeRangeFormat dateTimeRangeFormat = new Witnesses.DateTimeRangeFormat(dateTimeFormatMatch.DateTimeFormat, value, dateTimeRoundingSpec, dateTimeRoundingSpec2);
													PartialDateTime partialDateTime4 = ((exclusionInfo.Unit != null) ? Semantics.Add(partialDateTime2, (int)exclusionInfo.UnitAmount, exclusionInfo.Unit.Value) : partialDateTime2);
													Witnesses.DateTimeRangeWithState dateTimeRangeWithState = new Witnesses.DateTimeRangeWithState(ex.Key, partialDateTime, partialDateTime4);
													IReadOnlyCollection<Witnesses.DateTimeRangeWithState> readOnlyCollection;
													if (multiValueDictionary == null)
													{
														multiValueDictionary2.Add(dateTimeRangeFormat, dateTimeRangeWithState);
													}
													else if (multiValueDictionary.TryGetValue(dateTimeRangeFormat, out readOnlyCollection))
													{
														multiValueDictionary2.AddRange(dateTimeRangeFormat, readOnlyCollection);
														multiValueDictionary2.Add(dateTimeRangeFormat, dateTimeRangeWithState);
													}
												}
											}
										}
									}
								}
							}
						}
					}
					formats = hashSet;
					multiValueDictionary = multiValueDictionary2;
				}
			}
			if (multiValueDictionary == null || !multiValueDictionary.Any<KeyValuePair<Witnesses.DateTimeRangeFormat, IReadOnlyCollection<Witnesses.DateTimeRangeWithState>>>())
			{
				return OptionalUtils.Some((T)null);
			}
			ProgramSetBuilder<conv> programSetBuilder = multiValueDictionary.Select((KeyValuePair<Witnesses.DateTimeRangeFormat, IReadOnlyCollection<Witnesses.DateTimeRangeWithState>> p) => this.LearnDateRangeForOutputFormatInfo(engine, rule, taskWithoutPrefixSets, cancel, new Witnesses.OutputDateTimeFormatInfo(p))).NormalizedUnion<conv>();
			ProgramSet programSet = ((programSetBuilder != null) ? programSetBuilder.Set : null);
			if (prefixOutputSpec == null)
			{
				return programSet.Some<ProgramSet>();
			}
			return ((programSet != null) ? programSet.Filter(prefixOutputSpec) : null).Some<ProgramSet>();
		}

		// Token: 0x0600FA6C RID: 64108 RVA: 0x00357064 File Offset: 0x00355264
		private static OutputNotNullSpec WitnessFormatNumberNumber_NotNull(GrammarRule rule, int parameter, Spec spec)
		{
			if (!spec.ProvidedInputs.Any<State>())
			{
				return null;
			}
			return new OutputNotNullSpec(spec.ProvidedInputs.ToList<State>());
		}

		// Token: 0x0600FA6D RID: 64109 RVA: 0x00357088 File Offset: 0x00355288
		[RuleLearner("FormatNumericRange")]
		public Optional<ProgramSet> LearnFormatNumericRange(SynthesisEngine engine, BlackBoxRule rule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel)
		{
			if ((this._options.AllowedTransformations & TransformationKind.FormatNumericRange) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if ((this._options.AllowedTransformations & (TransformationKind.ParseNumber | TransformationKind.InputNumber)) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if (!this._options.OutputType.Equals(UnknownType.Instance))
			{
				return OptionalUtils.Some((T)null);
			}
			PrefixOutputSpec prefixOutputSpec;
			LearningTask<DisjunctiveExamplesSpec> learningTask = this.RemovePrefixExamplesFromTask<DisjunctiveExamplesSpec>(task, out prefixOutputSpec);
			MultiValueDictionary<State, Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>> numberPairs = new MultiValueDictionary<State, Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in learningTask.Spec.DisjunctiveExamples)
			{
				using (IEnumerator<ValueSubstring> enumerator2 = keyValuePair.Value.Cast<ValueSubstring>().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ValueSubstring output = enumerator2.Current;
						List<PositionMatch> list = Witnesses.NumberTokens.Take(1).SelectMany(delegate(Token token)
						{
							CachedList cachedList;
							if (!output.Cache.TryGetMatchPositionsFor(token, out cachedList))
							{
								return Enumerable.Empty<PositionMatch>();
							}
							return cachedList;
						}).Distinct<PositionMatch>()
							.ToList<PositionMatch>();
						if (list.Count == 2 && list[0].Position == output.Start && list[1].Right == output.End)
						{
							List<ValueSubstring> list2 = list.Select((PositionMatch match) => output.Slice(match.Position, new uint?(match.Right))).ToList<ValueSubstring>();
							List<decimal?> list3 = list2.Select((ValueSubstring ss) => Semantics.ParseNumber(ss, NumberFormatDetails.Default)).ToList<decimal?>();
							if (list3[0] != null && list3[1] != null)
							{
								decimal? num = list3[0];
								decimal? num2 = list3[1];
								if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
								{
									num2 = list3[0];
									num = list3[1];
									if ((num2.GetValueOrDefault() > num.GetValueOrDefault()) & ((num2 != null) & (num != null)))
									{
										num = list3[0];
										decimal num3 = 0m;
										if ((num.GetValueOrDefault() >= num3) & (num != null))
										{
											num = list3[1];
											num3 = 0m;
											if ((num.GetValueOrDefault() < num3) & (num != null))
											{
												list2[1] = output.Slice(list[1].Position + 1U, new uint?(list[1].Right));
												list3[1] = Semantics.ParseNumber(list2[1], NumberFormatDetails.Default);
											}
										}
									}
									num = list3[0];
									num2 = list3[1];
									if (!((num.GetValueOrDefault() >= num2.GetValueOrDefault()) & ((num != null) & (num2 != null))))
									{
										string value = output.Slice(list2[0].End, new uint?(list2[1].Start)).Value;
										decimal num4 = list3[1].Value - list3[0].Value;
										decimal num5;
										if (list3[0].Value % num4 == 0m)
										{
											num5 = num4;
										}
										else
										{
											if (!(num4 == Math.Truncate(num4)) || !(num4 % 10m != 1m))
											{
												continue;
											}
											num4 += 1m;
											if (!(list3[0].Value % num4 == 0m) && !(list3[1].Value % num4 == 0m))
											{
												continue;
											}
											num5 = num4;
										}
										decimal num6 = list3[0].Value % num5;
										decimal num7 = list3[1].Value % num5;
										numberPairs.Add(keyValuePair.Key, Tuple.Create<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>(list2[0], list3[0].Value, list2[1], list3[1].Value, new Witnesses.NumericRangeLearningInfo(value, num5, num6, num7)));
										if (list3[0].Value == 0m)
										{
											num5 += 1m;
											numberPairs.Add(keyValuePair.Key, Tuple.Create<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>(list2[0], list3[0].Value, list2[1], list3[1].Value, new Witnesses.NumericRangeLearningInfo(value, num5, num6, list3[1].Value % num5)));
										}
									}
								}
							}
						}
					}
				}
			}
			if (learningTask.Spec.ProvidedInputs.Any((State input) => !numberPairs.ContainsKey(input)))
			{
				return OptionalUtils.Some((T)null);
			}
			OutputNotNullSpec outputNotNullSpec = Witnesses.WitnessFormatNumberNumber_NotNull(rule, 0, learningTask.Spec);
			List<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<inputNumber>>> list4 = this._build.Set.Cast.inputNumber(engine.Learn(learningTask.MakeSubtask(rule, 0, outputNotNullSpec).WithoutTopKRequest(), cancel)).ClusterOnInputTuple(learningTask.Spec.ProvidedInputs).ToList<KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<inputNumber>>>();
			List<JoinProgramSetBuilder<conv>> list5 = new List<JoinProgramSetBuilder<conv>>(list4.Count);
			foreach (KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<inputNumber>> keyValuePair2 in list4)
			{
				Dictionary<State, Optional<decimal?>> inputToNumber = learningTask.Spec.ProvidedInputs.ZipWith(keyValuePair2.Key).ToDictionary<State, Optional<decimal?>>();
				List<List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>> list6 = learningTask.Spec.ProvidedInputs.Select((State input) => numberPairs[input].Where(delegate(Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo> pair)
				{
					decimal? num8 = inputToNumber[input].OrElseDefault<decimal?>();
					return num8 != null && !(num8.Value < pair.Item2) && !(num8.Value > pair.Item4);
				}).ToList<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>()).ToList<List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>>();
				using (IEnumerator<Witnesses.NumericRangeLearningInfo> enumerator4 = list6.SelectMany((List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>> l) => l.Select((Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo> t) => t.Item5)).GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						Witnesses.NumericRangeLearningInfo info = enumerator4.Current;
						IEnumerable<List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>> enumerable = list6;
						Func<List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>, List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>> func;
						Func<List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>, List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>> <>9__8;
						if ((func = <>9__8) == null)
						{
							Func<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>, bool> <>9__9;
							func = (<>9__8 = delegate(List<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>> l)
							{
								Func<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>, bool> func2;
								if ((func2 = <>9__9) == null)
								{
									func2 = (<>9__9 = (Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo> t) => t.Item5.Equals(info));
								}
								return l.Where(func2).ToList<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>();
							});
						}
						foreach (IEnumerable<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>> enumerable2 in enumerable.Select(func).CartesianProduct<Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo>>())
						{
							ProgramSetBuilder<numberFormat> programSetBuilder = NumberFormat.Learn(this._build, enumerable2.SelectMany((Tuple<ValueSubstring, decimal, ValueSubstring, decimal, Witnesses.NumericRangeLearningInfo> t) => new Record<ValueSubstring, decimal>[]
							{
								Record.Create<ValueSubstring, decimal>(t.Item1, t.Item2),
								Record.Create<ValueSubstring, decimal>(t.Item3, t.Item4)
							}), false);
							if (programSetBuilder != null)
							{
								RoundingSpec roundingSpec = new RoundingSpec(info.LowerZero, info.Delta, RoundingMode.Down);
								RoundingSpec roundingSpec2 = new RoundingSpec(info.UpperZero, info.Delta, (info.LowerZero == 0m) ? RoundingMode.UpOrNext : RoundingMode.Up);
								list5.Add(this._build.Set.ExplicitJoin.FormatNumericRange(keyValuePair2.Value, programSetBuilder, ProgramSetBuilder.List<s>(new s[] { this._build.Node.Rule.s(info.Separator) }), ProgramSetBuilder.List<roundingSpec>(new roundingSpec[] { this._build.Node.Rule.roundingSpec(roundingSpec) }), ProgramSetBuilder.List<roundingSpec>(new roundingSpec[] { this._build.Node.Rule.roundingSpec(roundingSpec2) })));
							}
						}
					}
				}
			}
			ProgramSetBuilder<conv> programSetBuilder2 = list5.NormalizedUnion<conv>();
			ProgramSet programSet = ((programSetBuilder2 != null) ? programSetBuilder2.Set : null);
			if (prefixOutputSpec == null)
			{
				return programSet.Some<ProgramSet>();
			}
			return ((programSet != null) ? programSet.Filter(prefixOutputSpec) : null).Some<ProgramSet>();
		}

		// Token: 0x0600FA6E RID: 64110 RVA: 0x003579B4 File Offset: 0x00355BB4
		[RuleLearner("FormatNumber")]
		public Optional<ProgramSet> LearnFormatNumber(SynthesisEngine engine, BlackBoxRule formatNumberRule, LearningTask<Spec> task, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass99_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass99_0();
			CS$<>8__locals1.<>4__this = this;
			if ((this._options.AllowedTransformations & TransformationKind.FormatNumber) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if ((this._options.AllowedTransformations & (TransformationKind.ParseNumber | TransformationKind.InputNumber)) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			if (!(this._options.OutputType is IType<decimal>) && !object.Equals(this._options.OutputType, UnknownType.Instance))
			{
				return OptionalUtils.Some((T)null);
			}
			PrefixOutputSpec prefixOutputSpec;
			LearningTask<Spec> learningTask = this.RemovePrefixExamplesFromTask<Spec>(task, out prefixOutputSpec);
			Witnesses.<>c__DisplayClass99_0 CS$<>8__locals2 = CS$<>8__locals1;
			NumberType numberType = this._options.OutputType as NumberType;
			CS$<>8__locals2.format = ((numberType != null) ? numberType.Format : null);
			DisjunctiveExamplesSpec disjunctiveExamplesSpec = learningTask.Spec as DisjunctiveExamplesSpec;
			if (CS$<>8__locals1.format == null && (disjunctiveExamplesSpec == null || disjunctiveExamplesSpec.DisjunctiveExamples.Count == 0))
			{
				CS$<>8__locals1.format = new NumberFormat(default(Optional<uint>), default(Optional<uint>), default(Optional<uint>), default(Optional<uint>), default(Optional<uint>), null);
			}
			else if (disjunctiveExamplesSpec != null && disjunctiveExamplesSpec.DisjunctiveExamples.Count != 0)
			{
				disjunctiveExamplesSpec = DisjunctiveExamplesSpec.From(disjunctiveExamplesSpec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kv) => kv.Key, delegate(KeyValuePair<State, IEnumerable<object>> kv)
				{
					IEnumerable<object> value = kv.Value;
					Func<object, bool> func;
					if ((func = CS$<>8__locals1.<>9__2) == null)
					{
						func = (CS$<>8__locals1.<>9__2 = delegate(object v)
						{
							ValueSubstring valueSubstring = v as ValueSubstring;
							if (CS$<>8__locals1.format != null)
							{
								ValueSubstring valueSubstring2 = Semantics.FormatNumber(Semantics.ParseNumber(valueSubstring, CS$<>8__locals1.format.Details), CS$<>8__locals1.format);
								return ((valueSubstring2 != null) ? valueSubstring2.Value : null) == ((valueSubstring != null) ? valueSubstring.Value : null);
							}
							return NumberFormat.IsValidNumberString(valueSubstring);
						});
					}
					return value.Where(func).ToList<object>().AsEnumerable<object>();
				}));
				if (disjunctiveExamplesSpec == null)
				{
					return OptionalUtils.Some((T)null);
				}
			}
			ProgramSetBuilder<number> programSetBuilder = this.LearnNumber(engine, learningTask, cancel);
			CS$<>8__locals1.inputIsDouble = false;
			State state = learningTask.ProvidedInputs.FirstOrDefault<State>();
			object obj;
			if (state != null && state.TryGetValue(this._build.Symbol.cell, out obj))
			{
				if (obj is double)
				{
					CS$<>8__locals1.inputIsDouble = true;
				}
				else if (obj == null)
				{
					string columnName = this.GetColumnName(learningTask);
					CS$<>8__locals1.inputIsDouble = learningTask.ProvidedInputs.Select((State i) => CS$<>8__locals1.<>4__this.BindColumnNameIfMissing(i, columnName)[CS$<>8__locals1.<>4__this._build.Symbol.cell]).FirstOrDefault((object cell) => cell != null) is double;
				}
			}
			ProgramSet programSet2;
			if (CS$<>8__locals1.format != null)
			{
				ProgramSet programSet = this._build.Set.Join.FormatNumber(programSetBuilder, CS$<>8__locals1.format.AsProgramSet(this._build)).Set;
				if (disjunctiveExamplesSpec != null)
				{
					programSet = programSet.Filter(disjunctiveExamplesSpec);
				}
				programSet2 = programSet;
			}
			else
			{
				List<ProgramSetBuilder<conv>> list = new List<ProgramSetBuilder<conv>>();
				IEnumerable<State> keys = disjunctiveExamplesSpec.DisjunctiveExamples.Keys;
				foreach (KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<number>> keyValuePair in programSetBuilder.ClusterOnInputTuple(keys))
				{
					if (!keyValuePair.Key.Any((Optional<decimal?> v) => v.OrElseDefault<decimal?>() == null))
					{
						IReadOnlyList<decimal> readOnlyList = keyValuePair.Key.Select((Optional<decimal?> key) => key.Value.Value).ToList<decimal>();
						ProgramSetBuilder<numberFormat> programSetBuilder2 = null;
						List<Record<decimal, IEnumerable<ValueSubstring>>> toVerify = (readOnlyList.Any((decimal num) => num != 0m) ? new List<Record<decimal, IEnumerable<ValueSubstring>>>() : null);
						foreach (Record<decimal, IEnumerable<ValueSubstring>> record in readOnlyList.ZipWith(disjunctiveExamplesSpec.DisjunctiveExamples.Values.Select((IEnumerable<object> v) => v.Cast<ValueSubstring>())))
						{
							decimal inputNumber = record.Item1;
							if (inputNumber == 0m && toVerify != null)
							{
								toVerify.Add(record);
							}
							else
							{
								ProgramSetBuilder<numberFormat> programSetBuilder3 = record.Item2.Select((ValueSubstring output) => NumberFormat.Learn(CS$<>8__locals1.<>4__this._build, Seq.Of<Record<ValueSubstring, decimal>>(new Record<ValueSubstring, decimal>[] { Record.Create<ValueSubstring, decimal>(output, inputNumber) }), CS$<>8__locals1.inputIsDouble)).NormalizedUnion<numberFormat>() ?? ProgramSetBuilder.Empty<numberFormat>(this._build.Symbol.numberFormat);
								programSetBuilder2 = ((programSetBuilder2 != null) ? programSetBuilder2.Intersect(programSetBuilder3) : null) ?? programSetBuilder3;
								if (ProgramSetBuilder.IsNullOrEmpty<numberFormat>(programSetBuilder2))
								{
									break;
								}
							}
						}
						List<Record<decimal, IEnumerable<ValueSubstring>>> toVerify2 = toVerify;
						if (toVerify2 != null && toVerify2.Any<Record<decimal, IEnumerable<ValueSubstring>>>() && programSetBuilder2 != null)
						{
							programSetBuilder2 = ProgramSetBuilder.List<numberFormat>(this._build.Symbol.numberFormat, programSetBuilder2.Set.RealizedPrograms.Where(delegate(ProgramNode buildNumberFormat)
							{
								NumberFormat numberFormat = (NumberFormat)buildNumberFormat.Invoke(null);
								return toVerify.All((Record<decimal, IEnumerable<ValueSubstring>> pair) => pair.Item2.Contains(Semantics.FormatNumber(new decimal?(pair.Item1), numberFormat)));
							}).Select(new Func<ProgramNode, numberFormat>(this._build.Node.Cast.numberFormat)));
						}
						if (!ProgramSetBuilder.IsNullOrEmpty<numberFormat>(programSetBuilder2))
						{
							list.Add(this._build.Set.Join.FormatNumber(keyValuePair.Value, programSetBuilder2));
						}
					}
				}
				ProgramSetBuilder<conv> programSetBuilder4 = list.NormalizedUnion<conv>();
				programSet2 = ((programSetBuilder4 != null) ? programSetBuilder4.Set : null);
			}
			if (prefixOutputSpec == null)
			{
				return programSet2.Some<ProgramSet>();
			}
			return ((programSet2 != null) ? programSet2.Filter(prefixOutputSpec) : null).Some<ProgramSet>();
		}

		// Token: 0x0600FA6F RID: 64111 RVA: 0x00357F68 File Offset: 0x00356168
		private ProgramSetBuilder<number> LearnNumber(SynthesisEngine engine, LearningTask<Spec> task, CancellationToken cancel)
		{
			LearningTask learningTask = task.Clone(this._build.Symbol.inputNumber, new OutputNotNullSpec(task.Spec.ProvidedInputs));
			ProgramSetBuilder<inputNumber> programSetBuilder = this._build.Set.Cast.inputNumber(engine.Learn(learningTask, cancel));
			ProgramSetBuilder<number> programSetBuilder2 = null;
			DisjunctiveExamplesSpec disjunctiveExamplesSpec = task.Spec as DisjunctiveExamplesSpec;
			if (disjunctiveExamplesSpec != null)
			{
				DisjunctiveExamplesSpec roundSpec = DisjunctiveExamplesSpec.From(disjunctiveExamplesSpec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kv) => kv.Key, (KeyValuePair<State, IEnumerable<object>> kv) => from v in kv.Value
					select Semantics.ParseNumber(v as ValueSubstring, NumberFormatDetails.Default) into o
					where o != null
					select o));
				if (roundSpec != null)
				{
					programSetBuilder2 = programSetBuilder.ClusterOnInputTuple(task.Spec.ProvidedInputs).Select(delegate(KeyValuePair<Optional<decimal?>[], ProgramSetBuilder<inputNumber>> cluster)
					{
						ExampleSpec numberSpec = new ExampleSpec(task.Spec.ProvidedInputs.ZipWith(cluster.Key.Select((Optional<decimal?> v) => v.OrElseDefault<decimal?>())).ToDictionary<State, object>());
						DisjunctiveExamplesSpec roundingSpecSpec = this.WitnessRoundNumberRoundingSpec(this._build.Rule.RoundNumber, roundSpec, numberSpec);
						ProgramSetBuilder<roundingSpec> programSetBuilder3 = this._build.Set.Cast.roundingSpec(engine.LearnSymbol(this._build.Symbol.roundingSpec, roundingSpecSpec, cancel));
						IEnumerable<int> enumerable = (from state in task.Spec.ProvidedInputs
							select new
							{
								number = (decimal?)numberSpec.Examples[state],
								roundedUnscaled = roundSpec.DisjunctiveExamples[state]
							} into o
							where o.number != null
							select new
							{
								logNumber = Math.Log10((double)o.number.Value),
								logRoundedUnscaled = o.roundedUnscaled.Select((object v) => Math.Log10((double)((decimal)v)))
							}).SelectMany(o => o.logRoundedUnscaled.Select((double l) => (int)Math.Truncate(o.logNumber - l))).Distinct<int>();
						Func<int, bool> func;
						if ((func = Witnesses.<>O.<6>__IsAllowedScalePower) == null)
						{
							func = (Witnesses.<>O.<6>__IsAllowedScalePower = new Func<int, bool>(NumberFormat.IsAllowedScalePower));
						}
						ProgramSetBuilder<roundingSpec> programSetBuilder4 = (from scalePow in enumerable.Where(func)
							select (decimal)Math.Pow(10.0, (double)scalePow)).ToArray<decimal>().Select(delegate(decimal scale)
						{
							Func<object, decimal?> <>9__16;
							DisjunctiveExamplesSpec disjunctiveExamplesSpec2 = DisjunctiveExamplesSpec.From(roundSpec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kv) => kv.Key, delegate(KeyValuePair<State, IEnumerable<object>> kv)
							{
								IEnumerable<object> value = kv.Value;
								Func<object, decimal?> func2;
								if ((func2 = <>9__16) == null)
								{
									func2 = (<>9__16 = delegate(object v)
									{
										decimal? num;
										try
										{
											num = (decimal?)v;
											num *= scale;
										}
										catch (OverflowException)
										{
											num = null;
										}
										return num;
									});
								}
								return value.Collect(func2).Cast<object>();
							}));
							if (disjunctiveExamplesSpec2 == null)
							{
								return null;
							}
							roundingSpecSpec = this.WitnessRoundNumberRoundingSpec(this._build.Rule.RoundNumber, disjunctiveExamplesSpec2, numberSpec);
							return this._build.Set.Cast.roundingSpec(engine.LearnSymbol(this._build.Symbol.roundingSpec, roundingSpecSpec, cancel));
						}).PrependItem(programSetBuilder3)
							.NormalizedUnion<roundingSpec>();
						return this._build.Set.Join.RoundNumber(cluster.Value, programSetBuilder4);
					}).NormalizedUnion<number>();
				}
			}
			return ProgramSetBuilder.NormalizedUnion<number>(new ProgramSetBuilder<number>[]
			{
				programSetBuilder2,
				this._build.Set.UnnamedConversion.number_inputNumber(programSetBuilder)
			});
		}

		// Token: 0x0600FA70 RID: 64112 RVA: 0x003580C9 File Offset: 0x003562C9
		[WitnessFunction("RoundNumber", 0)]
		public OutputNotNullSpec WitnessRoundNumberNumber(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			if ((this._options.AllowedTransformations & TransformationKind.RoundNumber) == TransformationKind.Unknown)
			{
				return null;
			}
			return new OutputNotNullSpec(spec.ProvidedInputs.ToList<State>());
		}

		// Token: 0x0600FA71 RID: 64113 RVA: 0x003580F0 File Offset: 0x003562F0
		private static IEnumerable<long> RoundingFactors(long d)
		{
			if (d <= 0L)
			{
				yield break;
			}
			long currentFactor = 1L;
			yield return currentFactor;
			while (d % 10L == 0L)
			{
				d /= 10L;
				currentFactor *= 5L;
				yield return currentFactor;
				currentFactor *= 2L;
				yield return currentFactor;
			}
			if (d % 5L == 0L)
			{
				yield return currentFactor * 5L;
			}
			yield break;
		}

		// Token: 0x0600FA72 RID: 64114 RVA: 0x00358100 File Offset: 0x00356300
		private static int LeastSignificantDigit(decimal number)
		{
			int num = 0;
			decimal num2 = number - Math.Truncate(number);
			if (num2 != 0m)
			{
				while (num2 % 1m != 0m)
				{
					num--;
					num2 *= 10m;
				}
			}
			else if (number != 0m)
			{
				while (number % 10m == 0m)
				{
					num++;
					number /= 10m;
				}
			}
			return num;
		}

		// Token: 0x0600FA73 RID: 64115 RVA: 0x00358194 File Offset: 0x00356394
		private static HashSet<RoundingSpec> GetRoundingSpecForExample(decimal input, IEnumerable<decimal> outputs)
		{
			HashSet<RoundingSpec> hashSet = new HashSet<RoundingSpec>();
			foreach (decimal num in outputs)
			{
				if (!(num == input))
				{
					decimal num2 = Math.Abs(input - num);
					int num3 = (int)Math.Truncate(Math.Log10((double)num2)) - 1;
					int num4 = Witnesses.LeastSignificantDigit(num);
					if (num4 >= num3)
					{
						foreach (decimal num5 in Enumerable.Range(num3, num4 - num3 + 1).SelectMany(delegate(int pow10)
						{
							decimal num6 = (decimal)Math.Pow(10.0, (double)pow10);
							return new decimal[]
							{
								num6,
								num6 * 5m
							};
						}))
						{
							if (!(num2 >= num5) && !(num % num5 != 0m))
							{
								if (num2 < num5 / 2m)
								{
									hashSet.Add(new RoundingSpec(0m, num5, RoundingMode.Nearest));
								}
								if (input < num)
								{
									hashSet.Add(new RoundingSpec(0m, num5, RoundingMode.Up));
								}
								else if (input > num)
								{
									hashSet.Add(new RoundingSpec(0m, num5, RoundingMode.Down));
								}
								if (input < num == input < 0m)
								{
									hashSet.Add(new RoundingSpec(0m, num5, RoundingMode.TowardZero));
								}
								else if (input > num == input < 0m)
								{
									hashSet.Add(new RoundingSpec(0m, num5, RoundingMode.AwayFromZero));
								}
							}
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x0600FA74 RID: 64116 RVA: 0x00358384 File Offset: 0x00356584
		[WitnessFunction("RoundNumber", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessRoundNumberRoundingSpec(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec numberSpec)
		{
			if ((this._options.AllowedTransformations & TransformationKind.RoundNumber) == TransformationKind.Unknown)
			{
				return null;
			}
			HashSet<RoundingSpec> allRoundingSpecs = new HashSet<RoundingSpec>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				decimal? num = (decimal?)numberSpec.Examples[keyValuePair.Key];
				if (num != null)
				{
					allRoundingSpecs.UnionWith(Witnesses.GetRoundingSpecForExample(num.Value, keyValuePair.Value.Cast<decimal>()));
				}
				else if (keyValuePair.Value.All((object v) => v != null))
				{
					return null;
				}
			}
			if (spec.DisjunctiveExamples.Count > 1)
			{
				using (IEnumerator<KeyValuePair<State, IEnumerable<object>>> enumerator = spec.DisjunctiveExamples.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<State, IEnumerable<object>> ex = enumerator.Current;
						if (!allRoundingSpecs.Any<RoundingSpec>())
						{
							return null;
						}
						decimal? num2 = (decimal?)numberSpec.Examples[ex.Key];
						if (num2 != null)
						{
							decimal input = num2.Value;
							allRoundingSpecs.RemoveWhere((RoundingSpec roundingSpec) => !ex.Value.Contains(Semantics.RoundNumber(new decimal?(input), roundingSpec)));
						}
					}
				}
			}
			return DisjunctiveExamplesSpec.From(spec.ProvidedInputs.ToDictionary((State state) => state, (State _) => allRoundingSpecs));
		}

		// Token: 0x0600FA75 RID: 64117 RVA: 0x00358568 File Offset: 0x00356768
		private static IEnumerable<ValueSubstring> GetAllNumberSubstrings(ValueSubstring ss)
		{
			if (ss == null)
			{
				yield break;
			}
			foreach (Token token in Witnesses.NumberTokens)
			{
				CachedList cachedList;
				if (ss.Cache.TryGetMatchPositionsFor(token, out cachedList))
				{
					foreach (PositionMatch positionMatch in cachedList)
					{
						yield return ss.Slice(positionMatch.Position, new uint?(positionMatch.Right));
					}
					List<PositionMatch>.Enumerator enumerator = default(List<PositionMatch>.Enumerator);
				}
			}
			Token[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x0600FA76 RID: 64118 RVA: 0x00358578 File Offset: 0x00356778
		[RuleLearner("ParseNumber")]
		public Optional<ProgramSet> LearnParseNumber(SynthesisEngine engine, BlackBoxRule rule, LearningTask<Spec> task, CancellationToken cancel)
		{
			Witnesses.<>c__DisplayClass108_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass108_0();
			CS$<>8__locals1.<>4__this = this;
			if ((this._options.AllowedTransformations & TransformationKind.ParseNumber) == TransformationKind.Unknown)
			{
				return OptionalUtils.Some((T)null);
			}
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			CS$<>8__locals1.parseFormat = NumberFormatDetails.Default;
			Spec spec = task.Spec;
			foreach (State state in spec.ProvidedInputs)
			{
				object obj;
				if (!state.TryGetValue(this._build.Symbol.x, out obj))
				{
					return OptionalUtils.Some((T)null);
				}
				ValueSubstring valueSubstring = (ValueSubstring)obj;
				if (valueSubstring == null)
				{
					dictionary[state] = new object[1];
				}
				else
				{
					HashSet<string> outputs = null;
					DisjunctiveExamplesSpec disjunctiveExamplesSpec = spec as DisjunctiveExamplesSpec;
					if (disjunctiveExamplesSpec != null)
					{
						outputs = new HashSet<string>();
						foreach (object obj2 in disjunctiveExamplesSpec.DisjunctiveExamples[state])
						{
							string text = ((decimal)obj2).ToString(CultureInfo.InvariantCulture);
							outputs.Add(text.Contains(NumberFormatInfo.InvariantInfo.NumberDecimalSeparator) ? text.TrimEnd(new char[] { '0' }) : text);
						}
					}
					IEnumerable<ValueSubstring> enumerable = Witnesses.GetAllNumberSubstrings(valueSubstring).Distinct<ValueSubstring>();
					Func<ValueSubstring, bool> func;
					if ((func = Witnesses.<>O.<7>__IsValidNumberString) == null)
					{
						func = (Witnesses.<>O.<7>__IsValidNumberString = new Func<ValueSubstring, bool>(NumberFormat.IsValidNumberString));
					}
					IEnumerable<ValueSubstring> enumerable2 = enumerable.Where(func);
					if (outputs != null)
					{
						enumerable2 = enumerable2.Where(delegate(ValueSubstring substring)
						{
							decimal? num;
							return outputs.Contains((Semantics.ParseNumber(substring, CS$<>8__locals1.parseFormat) != null) ? num.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : null);
						});
					}
					dictionary[state] = enumerable2.ToList<ValueSubstring>();
				}
			}
			DisjunctiveExamplesSpec disjunctiveExamplesSpec2 = DisjunctiveExamplesSpec.From(dictionary);
			LearningTask learningTask = task.MakeSubtask(rule, 0, disjunctiveExamplesSpec2).WithoutTopKRequest();
			ProgramSet programSet = engine.Learn(learningTask, cancel);
			if (ProgramSet.IsNullOrEmpty(programSet))
			{
				return OptionalUtils.Some((T)null);
			}
			Witnesses.<>c__DisplayClass108_0 CS$<>8__locals3 = CS$<>8__locals1;
			ProgramNode programNode;
			if (programSet == null)
			{
				programNode = null;
			}
			else
			{
				programNode = programSet.TopK(this._rankingScore, 1, FeatureCalculationContext.Create((disjunctiveExamplesSpec2 != null) ? disjunctiveExamplesSpec2.ProvidedInputs.ToList<State>() : null, null, task.TopProgramsFeatureOptions.Select((IFeatureOptions t) => t).OrElse(null)), null).FirstOrDefault<ProgramNode>();
			}
			CS$<>8__locals3.bestProgram = programNode;
			if (CS$<>8__locals1.bestProgram == null)
			{
				return OptionalUtils.Some((T)null);
			}
			List<State> list = task.Spec.ProvidedInputs.ToList<State>();
			CS$<>8__locals1.bestFormatDetails = NumberFormatDetails.Learn(task.Spec.ProvidedInputs.Select(delegate(State input)
			{
				ValueSubstring valueSubstring2 = (ValueSubstring)CS$<>8__locals1.bestProgram.Invoke(input);
				if (valueSubstring2 == null)
				{
					return null;
				}
				return valueSubstring2.Value;
			}).ToList<string>(), true);
			if (CS$<>8__locals1.bestFormatDetails == null)
			{
				return OptionalUtils.Some((T)null);
			}
			CS$<>8__locals1.columnName = this.GetColumnName(task);
			List<State> list2 = task.AdditionalInputs.Where(delegate(State input)
			{
				ValueSubstring valueSubstring3 = (ValueSubstring)CS$<>8__locals1.bestProgram.Invoke(CS$<>8__locals1.<>4__this.BindColumnNameIfMissing(input, CS$<>8__locals1.columnName));
				return valueSubstring3 == null || !NumberFormat.IsValidNumberString(valueSubstring3) || Semantics.ParseNumber(valueSubstring3, CS$<>8__locals1.bestFormatDetails) == null;
			}).ToList<State>();
			if (list2.Any<State>())
			{
				IReadOnlyList<State> readOnlyList = (from input in list2.RandomlySample(this._options.RandomSeed, this._options.ParseNumberAdditionalInputsSampleSize)
					select CS$<>8__locals1.<>4__this.BindColumnNameIfMissing(input, CS$<>8__locals1.columnName) into input
					where input[CS$<>8__locals1.<>4__this._build.Symbol.x] != null
					select input).ToList<State>();
				list.AddRange(readOnlyList);
				DisjunctiveExamplesSpec disjunctiveExamplesSpec3 = DisjunctiveExamplesSpec.From(readOnlyList.Distinct<State>().ToDictionary((State input) => input, (State input) => Witnesses.GetAllNumberSubstrings((ValueSubstring)input[CS$<>8__locals1.<>4__this._build.Symbol.x])));
				if (disjunctiveExamplesSpec3 == null)
				{
					return OptionalUtils.Some((T)null);
				}
				programSet = programSet.Filter(disjunctiveExamplesSpec3);
			}
			ProgramSet programSet2;
			if (programSet == null)
			{
				programSet2 = null;
			}
			else
			{
				ProgramSetBuilder<parsedNumber> programSetBuilder = programSet.ClusterOnInputTuple(list).Select(delegate(KeyValuePair<object[], ProgramSet> cluster)
				{
					IEnumerable<object> key = cluster.Key;
					ProgramSet value = cluster.Value;
					NumberFormatDetails numberFormatDetails = NumberFormatDetails.Learn(key.Select(delegate(object o)
					{
						ValueSubstring valueSubstring4 = o as ValueSubstring;
						if (valueSubstring4 == null)
						{
							return null;
						}
						return valueSubstring4.Value;
					}).ToList<string>(), true);
					if (numberFormatDetails != null)
					{
						return CS$<>8__locals1.<>4__this._build.Set.Join.ParseNumber(CS$<>8__locals1.<>4__this._build.Set.Cast.SS(value), ProgramSetBuilder.List<numberFormatDetails>(new numberFormatDetails[] { CS$<>8__locals1.<>4__this._build.Node.Rule.numberFormatDetails(numberFormatDetails) }));
					}
					return null;
				}).NormalizedUnion<parsedNumber>();
				programSet2 = ((programSetBuilder != null) ? programSetBuilder.Set : null);
			}
			return programSet2.Some<ProgramSet>();
		}

		// Token: 0x0600FA77 RID: 64119 RVA: 0x003589A4 File Offset: 0x00356BA4
		[WitnessFunction("WholeColumn", 0)]
		internal Spec WitnessWholeColumn(GrammarRule rule, Spec spec)
		{
			if ((this._options.AllowedTransformations & TransformationKind.WholeColumn) == TransformationKind.Unknown)
			{
				return null;
			}
			return spec;
		}

		// Token: 0x0600FA78 RID: 64120 RVA: 0x003589B8 File Offset: 0x00356BB8
		[WitnessFunction("SubStr", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPP(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xs)
		{
			if ((this._options.AllowedTransformations & TransformationKind.Substring) == TransformationKind.Unknown)
			{
				return null;
			}
			bool flag = false;
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in xs.Examples)
			{
				State key = keyValuePair.Key;
				ValueSubstring valueSubstring = (ValueSubstring)keyValuePair.Value;
				IEnumerable<object> enumerable = spec.DisjunctiveExamples[key];
				if (valueSubstring == null)
				{
					if (enumerable.All((object o) => o != null))
					{
						return null;
					}
				}
				else
				{
					List<object> list = new List<object>();
					foreach (ValueSubstring valueSubstring2 in enumerable.Where((object o) => !(o is StringPrefixSet)).Cast<ValueSubstring>())
					{
						string value = valueSubstring2.Value;
						if (value.Length != 0)
						{
							flag = true;
						}
						Optional<uint> optional = valueSubstring.IndexOfRelative(value, 0U, StringComparison.Ordinal);
						while (optional.HasValue && optional.Value < valueSubstring.Length)
						{
							list.Add(new Record<uint?, uint?>(new uint?(optional.Value), new uint?(optional.Value + valueSubstring2.Length)));
							optional = valueSubstring.IndexOfRelative(value, optional.Value + 1U, StringComparison.Ordinal);
						}
					}
					foreach (StringPrefixSet stringPrefixSet in enumerable.OfType<StringPrefixSet>())
					{
						string value2 = stringPrefixSet.Prefix.Value;
						if (value2.Length != 0)
						{
							flag = true;
						}
						Optional<uint> optional2 = valueSubstring.IndexOfRelative(value2, 0U, StringComparison.Ordinal);
						while (optional2.HasValue && optional2.Value <= valueSubstring.Length)
						{
							uint num = (uint)value2.Length;
							while (optional2.Value + num <= valueSubstring.Length)
							{
								list.Add(new Record<uint?, uint?>(new uint?(optional2.Value), new uint?(optional2.Value + num)));
								num += 1U;
							}
							if (optional2.Value + 1U > valueSubstring.Length)
							{
								break;
							}
							optional2 = valueSubstring.IndexOfRelative(value2, optional2.Value + 1U, StringComparison.Ordinal);
						}
					}
					if (list.Count == 0)
					{
						return null;
					}
					dictionary[key] = list;
				}
			}
			if (!flag)
			{
				return null;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA79 RID: 64121 RVA: 0x00358CB4 File Offset: 0x00356EB4
		[WitnessFunction("RelativePosition", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessRelPos(BlackBoxRule rule, DisjunctiveExamplesSpec outerSpec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				ValueSubstring valueSubstring = (ValueSubstring)xSpec.Examples[state];
				if (valueSubstring == null)
				{
					return null;
				}
				List<int> list = new List<int>();
				foreach (uint? num in outerSpec.DisjunctiveExamples[state].Cast<uint?>())
				{
					if (num != null)
					{
						list.Add((int)num.Value);
						List<int> list2 = list;
						long num2 = (long)(ulong.MaxValue - (ulong)valueSubstring.Length);
						uint? num3 = num;
						list2.Add((int)(num2 + ((num3 != null) ? new long?((long)((ulong)num3.GetValueOrDefault())) : null)).Value);
					}
				}
				dictionary[state] = list.Cast<object>().ToArray<object>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA7A RID: 64122 RVA: 0x00358E30 File Offset: 0x00357030
		[RuleLearner("RegexPositionRelative")]
		public Optional<ProgramSet> LearnRegexPositionRelative(SynthesisEngine engine, BlackBoxRule rule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel)
		{
			DisjunctiveExamplesSpec spec = task.Spec;
			HashSet<Record<Record<RegularExpression, RegularExpression>?, int>> hashSet = null;
			bool flag = (this._options.AllowedTransformations & TransformationKind.RegexPair) > TransformationKind.Unknown;
			int? num = (((this._options.AllowedTransformations & TransformationKind.MultiTokenPositionalRegex) != TransformationKind.Unknown) ? null : new int?(1));
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				ValueSubstring valueSubstring = (ValueSubstring)keyValuePair.Key[this._build.Symbol.x];
				uint start = valueSubstring.Start;
				IEnumerable<uint> enumerable = from val in keyValuePair.Value.OfType<uint>()
					select val + start;
				HashSet<Record<Record<RegularExpression, RegularExpression>?, int>> regexesAndIndexesAtPositions = RegularExpressionPositions.GetRegexesAndIndexesAtPositions(valueSubstring, enumerable, 0, num, flag);
				if (hashSet == null)
				{
					hashSet = regexesAndIndexesAtPositions;
				}
				else
				{
					hashSet.IntersectWith(regexesAndIndexesAtPositions);
				}
				if (hashSet.Count == 0)
				{
					return OptionalUtils.Some((T)null);
				}
			}
			return ProgramSetBuilder.List<pos>(hashSet.Select((Record<Record<RegularExpression, RegularExpression>?, int> opt) => this._build.Node.Rule.RegexPositionRelative(this._build.Node.Variable.x, this._build.Node.Rule.RegexPair(this._build.Node.Rule.r(opt.Item1.Value.Item1), this._build.Node.Rule.r(opt.Item1.Value.Item2)), this._build.Node.Rule.k(opt.Item2))).ToArray<pos>()).Set.Some<ProgramSet>();
		}

		// Token: 0x0600FA7B RID: 64123 RVA: 0x00358F74 File Offset: 0x00357174
		[WitnessFunction("RegexPositionPair", 1, DependsOnParameters = new int[] { 0 })]
		public DisjunctiveExamplesSpec WitnessRegPosPairRegEx(BlackBoxRule rule, DisjunctiveExamplesSpec outerSpec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				ValueSubstring valueSubstring = (ValueSubstring)xSpec.Examples[state];
				if (valueSubstring == null)
				{
					return null;
				}
				HashSet<RegularExpression> hashSet = new HashSet<RegularExpression>();
				foreach (Record<uint?, uint?>? record in outerSpec.DisjunctiveExamples[state].Cast<Record<uint?, uint?>?>())
				{
					bool flag;
					if (record == null)
					{
						flag = false;
					}
					else
					{
						Record<uint?, uint?> record2 = record.GetValueOrDefault();
						flag = record2.Item1 != null;
					}
					if (flag)
					{
						Record<uint?, uint?> record2 = record.Value;
						if (record2.Item2 != null)
						{
							HashSet<RegularExpression> hashSet2 = hashSet;
							ValueSubstring valueSubstring2 = valueSubstring;
							record2 = record.Value;
							uint value = record2.Item1.Value;
							record2 = record.Value;
							hashSet2.UnionWith(RegularExpression.LearnFullMatches(valueSubstring2.SliceRelative(value, new uint?(record2.Item2.Value)), RegularExpression.DefaultTokenCount, 0));
						}
					}
				}
				dictionary[state] = hashSet.ToArray<RegularExpression>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA7C RID: 64124 RVA: 0x003590EC File Offset: 0x003572EC
		[WitnessFunction("RegexPositionPair", 2, DependsOnParameters = new int[] { 0, 1 })]
		public DisjunctiveExamplesSpec WitnessRegPosPairK(BlackBoxRule rule, DisjunctiveExamplesSpec outerSpec, ExampleSpec xSpec, ExampleSpec regexSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				ValueSubstring x = (ValueSubstring)xSpec.Examples[state];
				RegularExpression regularExpression = (RegularExpression)regexSpec.Examples[state];
				if (x == null)
				{
					return null;
				}
				HashSet<int> hashSet = new HashSet<int>();
				PositionMatch[] array = regularExpression.Run(x);
				using (IEnumerator<Record<uint?, uint?>?> enumerator2 = outerSpec.DisjunctiveExamples[state].Cast<Record<uint?, uint?>?>().GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Record<uint?, uint?>? posPair = enumerator2.Current;
						if (posPair != null)
						{
							Record<uint?, uint?> record = posPair.Value;
							if (record.Item1 != null)
							{
								record = posPair.Value;
								if (record.Item2 != null)
								{
									int? num = array.Select(delegate(PositionMatch posMatch, int i)
									{
										uint position = posMatch.Position;
										uint? num2 = posPair.Value.Item1 + x.Start;
										if ((position == num2.GetValueOrDefault()) & (num2 != null))
										{
											uint right = posMatch.Right;
											num2 = posPair.Value.Item2 + x.Start;
											if ((right == num2.GetValueOrDefault()) & (num2 != null))
											{
												return new int?(i);
											}
										}
										return null;
									}).SingleOrDefault((int? i) => i != null);
									if (num != null)
									{
										hashSet.Add(num.Value + 1);
										hashSet.Add(num.Value - array.Length);
									}
								}
							}
						}
					}
				}
				dictionary[state] = hashSet.Cast<object>().ToArray<object>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA7D RID: 64125 RVA: 0x003592E4 File Offset: 0x003574E4
		[WitnessFunction("ExternalExtractorPositionPair", 1)]
		public DisjunctiveExamplesSpec WitnessExternalExtractorPositionPairExtractor(BlackBoxRule rule, DisjunctiveExamplesSpec spec)
		{
			if (spec == null || spec.DisjunctiveExamples == null || spec.DisjunctiveExamples.Count == 0)
			{
				return null;
			}
			State key = spec.DisjunctiveExamples.First<KeyValuePair<State, IEnumerable<object>>>().Key;
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			State state = key;
			dictionary[state] = this._options.ExternalExtractors.Cast<object>();
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600FA7E RID: 64126 RVA: 0x00359348 File Offset: 0x00357548
		[WitnessFunction("ExternalExtractorPositionPair", 2, DependsOnParameters = new int[] { 1 })]
		public DisjunctiveExamplesSpec WitnessExternalExtractorPositionPairK(BlackBoxRule rule, DisjunctiveExamplesSpec outerSpec, ExampleSpec extractorSpec)
		{
			if (extractorSpec == null || extractorSpec.Examples == null || extractorSpec.Examples.Count == 0)
			{
				return null;
			}
			CustomExtractor customExtractor = (CustomExtractor)extractorSpec.Examples.First<KeyValuePair<State, object>>().Value;
			HashSet<int> hashSet = null;
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in outerSpec.DisjunctiveExamples)
			{
				object obj;
				if (!keyValuePair.Key.TryGetValue(this._build.Symbol.x, out obj))
				{
					return null;
				}
				ValueSubstring valueSubstring = (ValueSubstring)obj;
				HashSet<int> hashSet2 = new HashSet<int>();
				foreach (Record<uint?, uint?>? record in keyValuePair.Value.Cast<Record<uint?, uint?>?>())
				{
					if (record != null)
					{
						Record<int, int>? record2 = valueSubstring.CachedMatchIndicesAt(customExtractor, record.Value);
						if (record2 != null)
						{
							hashSet2.Add(record2.Value.Item1);
							hashSet2.Add(record2.Value.Item2);
						}
					}
				}
				if (hashSet == null)
				{
					hashSet = hashSet2;
				}
				else
				{
					hashSet.IntersectWith(hashSet2);
				}
				if (hashSet.Count == 0)
				{
					return null;
				}
			}
			if (hashSet.Count == 0)
			{
				return null;
			}
			List<object> finalKValuesForExample = hashSet.Cast<object>().ToList<object>();
			return DisjunctiveExamplesSpec.From(outerSpec.DisjunctiveExamples.ToDictionary((KeyValuePair<State, IEnumerable<object>> kvp) => kvp.Key, (KeyValuePair<State, IEnumerable<object>> kvp) => finalKValuesForExample));
		}

		// Token: 0x0600FA7F RID: 64127 RVA: 0x00359518 File Offset: 0x00357718
		[WitnessFunction("LetColumnName", 0)]
		internal DisjunctiveExamplesSpec LetColumnNameWitness(GrammarRule rule, Spec spec)
		{
			Witnesses.<>c__DisplayClass117_0 CS$<>8__locals1 = new Witnesses.<>c__DisplayClass117_0();
			CS$<>8__locals1.rule = rule;
			Witnesses.<>c__DisplayClass117_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<object> learningColumns = this._learningColumns;
			IEnumerable<object> enumerable;
			if ((enumerable = learningColumns) == null)
			{
				enumerable = (from row in spec.ProvidedInputs.Select((State state) => state[CS$<>8__locals1.rule.Grammar.InputSymbol]).OfType<IRow>()
					select row.ColumnNames into columns
					where columns != null
					select columns).Intersect<string>().ToImmutableHashSet<string>();
			}
			CS$<>8__locals2.columnNames = enumerable;
			return DisjunctiveExamplesSpec.From(spec.ProvidedInputs.ToDictionary((State state) => state, (State _) => CS$<>8__locals1.columnNames));
		}

		// Token: 0x0600FA80 RID: 64128 RVA: 0x00002188 File Offset: 0x00000388
		[WitnessFunction("IndexInputString", 0)]
		[WitnessFunction("SelectIndexedInput", 0)]
		internal Spec DoNotLearn(GrammarRule rule, Spec spec)
		{
			return null;
		}

		// Token: 0x0600FA81 RID: 64129 RVA: 0x003595F0 File Offset: 0x003577F0
		[WitnessFunction("LetX", 0)]
		internal TopSpec LetVWitness(GrammarRule rule, Spec spec)
		{
			foreach (State state in spec.ProvidedInputs)
			{
				IRow row = (IRow)state[rule.Grammar.InputSymbol];
				string text = (string)state[this._build.Symbol.columnName];
				if (Semantics.ChooseInput(row, text) == null && ((this._options.AllowedTransformations & TransformationKind.Lookup) == TransformationKind.Unknown || (row.ColumnNames != null && !row.ColumnNames.Contains(text))))
				{
					return null;
				}
			}
			return TopSpec.Instance;
		}

		// Token: 0x0600FA82 RID: 64130 RVA: 0x003596A8 File Offset: 0x003578A8
		[WitnessFunction("LetCell", 0)]
		internal TopSpec LetCellWitness(GrammarRule rule, Spec spec)
		{
			foreach (State state in spec.ProvidedInputs)
			{
				IRow row = (IRow)state[rule.Grammar.InputSymbol];
				string text = (string)state[this._build.Symbol.columnName];
				object obj = Semantics.LookupInput(row, text);
				if (obj != null && !(obj is string) && !(obj is ValueSubstring))
				{
					return TopSpec.Instance;
				}
			}
			return null;
		}

		// Token: 0x0600FA83 RID: 64131 RVA: 0x00359748 File Offset: 0x00357948
		[WitnessFunction("LetPL1", 0)]
		internal ExampleSpec LetFexpr1Witness(LetRule rule, ExampleSpec outerSpec)
		{
			if ((this._options.AllowedTransformations & TransformationKind.RelativePosition) == TransformationKind.Unknown)
			{
				return null;
			}
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				Record<uint?, uint?>? record = outerSpec.Examples[state] as Record<uint?, uint?>?;
				if (record == null)
				{
					return null;
				}
				dictionary[state] = record.Value.Item1;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0600FA84 RID: 64132 RVA: 0x003597F0 File Offset: 0x003579F0
		[WitnessFunction("LetPL2", 0)]
		internal ExampleSpec LetFexpr2Witness(LetRule rule, ExampleSpec outerSpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (State state in outerSpec.ProvidedInputs)
			{
				Record<uint?, uint?>? record = outerSpec.Examples[state] as Record<uint?, uint?>?;
				if (record == null)
				{
					return null;
				}
				dictionary[state] = record.Value.Item2 - record.Value.Item1;
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0600FA85 RID: 64133 RVA: 0x00002188 File Offset: 0x00000388
		[WitnessFunction("LetSharedParsedNumber", 0)]
		[WitnessFunction("LetSharedParsedNumber", 1)]
		[WitnessFunction("LetSharedParsedDateTime", 0)]
		[WitnessFunction("LetSharedParsedDateTime", 1)]
		internal Spec LetSharedParsedNumber(LetRule rule, Spec outerSpec)
		{
			return null;
		}

		// Token: 0x0600FA86 RID: 64134 RVA: 0x003598CC File Offset: 0x00357ACC
		[WitnessFunction("SubString", 0)]
		internal Spec SubStringWitness(GrammarRule rule, Spec spec)
		{
			if ((this._options.AllowedTransformations & (TransformationKind.Substring | TransformationKind.WholeColumn)) == TransformationKind.Unknown)
			{
				return null;
			}
			if (this._options.OutputType.Equals(UnknownType.Instance))
			{
				return spec;
			}
			return null;
		}

		// Token: 0x0600FA87 RID: 64135 RVA: 0x003598FC File Offset: 0x00357AFC
		private static bool CouldStringBeDate(string str)
		{
			return DateFormatCache.AllFormatMatchesFor(ValueSubstring.Create(str, null, null, null, null), DateFormatCache.ParseMode.FullLength, HeuristicsMode.AllowMostFormats).Any<DateTimeFormatMatch>();
		}

		// Token: 0x0600FA88 RID: 64136 RVA: 0x00359930 File Offset: 0x00357B30
		private static bool IsAllowedLookup(HashSet<KeyValuePair<Optional<string>, string>> lookup)
		{
			if ((from kv in lookup
				group kv by kv.Key).Any((IGrouping<Optional<string>, KeyValuePair<Optional<string>, string>> g) => g.Count<KeyValuePair<Optional<string>, string>>() > 1))
			{
				return false;
			}
			if (lookup.All((KeyValuePair<Optional<string>, string> kv) => kv.Key.HasValue && NumberFormat.IsValidNumberString(kv.Key.Value) && NumberFormat.IsValidNumberString(kv.Value)))
			{
				return false;
			}
			if (lookup.All((KeyValuePair<Optional<string>, string> kv) => kv.Key.HasValue && Witnesses.CouldStringBeDate(kv.Key.Value) && Witnesses.CouldStringBeDate(kv.Value)))
			{
				return false;
			}
			return !lookup.All((KeyValuePair<Optional<string>, string> kv) => !kv.Key.HasValue || kv.Key.Value.Contains(kv.Value));
		}

		// Token: 0x0600FA89 RID: 64137 RVA: 0x00359A06 File Offset: 0x00357C06
		[WitnessFunction("Lookup", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessLookupDictionary(BlackBoxRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xs)
		{
			if ((this._options.AllowedTransformations & TransformationKind.Lookup) == TransformationKind.Unknown)
			{
				return null;
			}
			throw new NotImplementedException("WitnessLookupDictionary with DisjunctiveExamplesSpec is unsupported and should never get called.");
		}

		// Token: 0x0600FA8A RID: 64138 RVA: 0x00359A24 File Offset: 0x00357C24
		[WitnessFunction("Lookup", 1, DependsOnParameters = new int[] { 0 })]
		internal ExampleSpec WitnessLookupDictionary(BlackBoxRule rule, ExampleSpec spec, ExampleSpec xs)
		{
			if ((this._options.AllowedTransformations & TransformationKind.Lookup) == TransformationKind.Unknown)
			{
				return null;
			}
			HashSet<KeyValuePair<Optional<string>, string>> hashSet = xs.Examples.Where((KeyValuePair<State, object> kv) => !(kv.Value is StringPrefixSet)).Select(delegate(KeyValuePair<State, object> kv)
			{
				ValueSubstring valueSubstring = kv.Value as ValueSubstring;
				Optional<string> optional = ((valueSubstring != null) ? valueSubstring.Value : null).SomeIfNotNull<string>();
				ValueSubstring valueSubstring2 = spec.Examples[kv.Key] as ValueSubstring;
				return new KeyValuePair<Optional<string>, string>(optional, (valueSubstring2 != null) ? valueSubstring2.Value : null);
			}).ConvertToHashSet<KeyValuePair<Optional<string>, string>>();
			if (!Witnesses.IsAllowedLookup(hashSet))
			{
				return null;
			}
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			State state = spec.ProvidedInputs.First<State>();
			dictionary[state] = hashSet.ToDictionary<Optional<string>, string>();
			return new ExampleSpec(dictionary);
		}

		// Token: 0x04005CB9 RID: 23737
		private readonly IReadOnlyList<string> _learningColumns;

		// Token: 0x04005CBA RID: 23738
		private readonly Witnesses.Options _options;

		// Token: 0x04005CBB RID: 23739
		private readonly GrammarBuilders _build;

		// Token: 0x04005CBC RID: 23740
		private readonly InputsUsed _inputsUsed;

		// Token: 0x04005CBD RID: 23741
		private const int MaxColumns = 256;

		// Token: 0x04005CBE RID: 23742
		private const int PredicateExtraK = 100;

		// Token: 0x04005CBF RID: 23743
		private readonly IFeature _rankingScore;

		// Token: 0x04005CC0 RID: 23744
		private readonly BranchCount _branchCount;

		// Token: 0x04005CC1 RID: 23745
		private bool _isOutsideTopLevel = true;

		// Token: 0x04005CC2 RID: 23746
		private static readonly HashSet<string> NaValues = new HashSet<string>
		{
			"", "#N/A", "#N/A N/A", "#NA", "-1.#IND", "-1.#QNAN", "-NaN", "-nan", "1.#IND", "1.#QNAN",
			"N/A", "NA", "NULL", "NaN", "n/a", "nan", "null"
		};

		// Token: 0x04005CC6 RID: 23750
		private static readonly IEnumerable<DateTimeRoundingSpec> HourRoundings = Witnesses.PrebuildDateTimeRoundingSpecs(new int[] { 1, 2, 3, 4, 6 }, DateTimePart.Hour, new RoundingMode[]
		{
			RoundingMode.Down,
			RoundingMode.Up
		});

		// Token: 0x04005CC7 RID: 23751
		private static readonly IEnumerable<DateTimeRoundingSpec> MinuteRoundings = Witnesses.PrebuildDateTimeRoundingSpecs(new int[] { 1, 15, 30 }, DateTimePart.Minute, new RoundingMode[]
		{
			RoundingMode.Down,
			RoundingMode.Up
		});

		// Token: 0x04005CC8 RID: 23752
		private static readonly IEnumerable<DateTimeRoundingSpec> SecondRoundings = Witnesses.PrebuildDateTimeRoundingSpecs(new int[] { 1, 15, 30 }, DateTimePart.Second, new RoundingMode[]
		{
			RoundingMode.Down,
			RoundingMode.Up
		});

		// Token: 0x04005CC9 RID: 23753
		private static readonly IEnumerable<DateTimeRoundingSpec> YearRoundings = Witnesses.PrebuildDateTimeRoundingSpecs(new int[] { 1, 5, 10, 25, 50, 100 }, DateTimePart.Year, new RoundingMode[]
		{
			RoundingMode.Down,
			RoundingMode.Up
		});

		// Token: 0x04005CCA RID: 23754
		private static readonly IEnumerable<DateTimeRoundingSpec> EmptyRoundings = Enumerable.Empty<DateTimeRoundingSpec>();

		// Token: 0x04005CCB RID: 23755
		private static readonly Witnesses.ExclusionInfo[] DateTimeRangeExclusions = new Witnesses.ExclusionInfo[]
		{
			new Witnesses.ExclusionInfo(),
			new Witnesses.ExclusionInfo(1L, DateTimePart.Millisecond, 1U),
			new Witnesses.ExclusionInfo(10L, DateTimePart.Millisecond, 10U),
			new Witnesses.ExclusionInfo(100L, DateTimePart.Millisecond, 100U),
			new Witnesses.ExclusionInfo(1000L, DateTimePart.Second, 1U),
			new Witnesses.ExclusionInfo(10000L, DateTimePart.Second, 10U),
			new Witnesses.ExclusionInfo(60000L, DateTimePart.Minute, 1U),
			new Witnesses.ExclusionInfo(600000L, DateTimePart.Minute, 10U),
			new Witnesses.ExclusionInfo(3600000L, DateTimePart.Hour, 1U)
		};

		// Token: 0x04005CCC RID: 23756
		private static readonly Token[] NumberTokens = new string[] { "SignedNumber", "GeneralNumber" }.Select(new Func<string, Token>(Semantics.GetStaticTokenByName)).ToArray<Token>();

		// Token: 0x02001CD5 RID: 7381
		public class Options : DSLOptions
		{
			// Token: 0x170029E7 RID: 10727
			// (get) Token: 0x0600FA90 RID: 64144 RVA: 0x00359DF7 File Offset: 0x00357FF7
			// (set) Token: 0x0600FA91 RID: 64145 RVA: 0x00359DFF File Offset: 0x00357FFF
			public IType OutputType { get; set; } = UnknownType.Instance;

			// Token: 0x170029E8 RID: 10728
			// (get) Token: 0x0600FA92 RID: 64146 RVA: 0x00359E08 File Offset: 0x00358008
			// (set) Token: 0x0600FA93 RID: 64147 RVA: 0x00359E10 File Offset: 0x00358010
			public bool ParseSingleDateFormat { get; set; }

			// Token: 0x0600FA94 RID: 64148 RVA: 0x00359E19 File Offset: 0x00358019
			public void AllowTransformations(params TransformationKind[] transformations)
			{
				transformations.ForEach(delegate(TransformationKind t)
				{
					this.AllowedTransformations |= t;
				});
			}

			// Token: 0x0600FA95 RID: 64149 RVA: 0x00359E2D File Offset: 0x0035802D
			public void ForbidTransformations(params TransformationKind[] transformations)
			{
				transformations.ForEach(delegate(TransformationKind t)
				{
					this.AllowedTransformations &= ~t;
				});
			}

			// Token: 0x0600FA96 RID: 64150 RVA: 0x00359E41 File Offset: 0x00358041
			public void AllowAllTransformations()
			{
				this.AllowedTransformations = Enum.GetValues(typeof(TransformationKind)).OfType<TransformationKind>().Aggregate((TransformationKind acc, TransformationKind t) => acc | t);
			}

			// Token: 0x0600FA97 RID: 64151 RVA: 0x00359E81 File Offset: 0x00358081
			public void ForbidAllTransformations()
			{
				this.AllowedTransformations = TransformationKind.Unknown;
			}

			// Token: 0x170029E9 RID: 10729
			// (get) Token: 0x0600FA98 RID: 64152 RVA: 0x00359E8A File Offset: 0x0035808A
			// (set) Token: 0x0600FA99 RID: 64153 RVA: 0x00359E92 File Offset: 0x00358092
			public TransformationKind AllowedTransformations { get; set; } = Enum.GetValues(typeof(TransformationKind)).OfType<TransformationKind>().Except(new TransformationKind[]
			{
				TransformationKind.Unknown,
				TransformationKind.Lookup
			})
				.Aggregate((TransformationKind a, TransformationKind b) => a | b);

			// Token: 0x170029EA RID: 10730
			// (get) Token: 0x0600FA9A RID: 64154 RVA: 0x00359E9B File Offset: 0x0035809B
			// (set) Token: 0x0600FA9B RID: 64155 RVA: 0x00359EB5 File Offset: 0x003580B5
			public bool AllowConcat
			{
				get
				{
					return (this.AllowedTransformations & TransformationKind.Concat) != TransformationKind.Unknown && this.ConcatLocation != ConcatLocation.Nowhere;
				}
				set
				{
					if (value)
					{
						this.AllowedTransformations |= TransformationKind.Concat;
						return;
					}
					this.AllowedTransformations &= ~TransformationKind.Concat;
				}
			}

			// Token: 0x170029EB RID: 10731
			// (get) Token: 0x0600FA9C RID: 64156 RVA: 0x00359ED8 File Offset: 0x003580D8
			// (set) Token: 0x0600FA9D RID: 64157 RVA: 0x00359EE0 File Offset: 0x003580E0
			public ConcatLocation ConcatLocation { get; set; } = ConcatLocation.Nowhere | ConcatLocation.Anywhere;

			// Token: 0x170029EC RID: 10732
			// (get) Token: 0x0600FA9E RID: 64158 RVA: 0x00359EE9 File Offset: 0x003580E9
			// (set) Token: 0x0600FA9F RID: 64159 RVA: 0x00359EF1 File Offset: 0x003580F1
			public bool DisableConcatHeuristics { get; set; }

			// Token: 0x170029ED RID: 10733
			// (get) Token: 0x0600FAA0 RID: 64160 RVA: 0x00359EFA File Offset: 0x003580FA
			// (set) Token: 0x0600FAA1 RID: 64161 RVA: 0x00359F02 File Offset: 0x00358102
			public LookupFallbackMode LookupFallbackMode { get; set; }

			// Token: 0x170029EE RID: 10734
			// (get) Token: 0x0600FAA2 RID: 64162 RVA: 0x00359F0B File Offset: 0x0035810B
			// (set) Token: 0x0600FAA3 RID: 64163 RVA: 0x00359F13 File Offset: 0x00358113
			public bool ForbidConstantProgram { get; set; } = true;

			// Token: 0x170029EF RID: 10735
			// (get) Token: 0x0600FAA4 RID: 64164 RVA: 0x00359F1C File Offset: 0x0035811C
			// (set) Token: 0x0600FAA5 RID: 64165 RVA: 0x00359F24 File Offset: 0x00358124
			public IImmutableSet<string> RequiredColumns { get; set; } = ImmutableHashSet<string>.Empty;

			// Token: 0x170029F0 RID: 10736
			// (get) Token: 0x0600FAA6 RID: 64166 RVA: 0x00359F2D File Offset: 0x0035812D
			// (set) Token: 0x0600FAA7 RID: 64167 RVA: 0x00359F35 File Offset: 0x00358135
			public IImmutableSet<string> ForbiddenConstants { get; set; } = ImmutableHashSet<string>.Empty;

			// Token: 0x170029F1 RID: 10737
			// (get) Token: 0x0600FAA8 RID: 64168 RVA: 0x00359F3E File Offset: 0x0035813E
			// (set) Token: 0x0600FAA9 RID: 64169 RVA: 0x00359F46 File Offset: 0x00358146
			public int RandomSeed { get; set; } = 7715177;

			// Token: 0x170029F2 RID: 10738
			// (get) Token: 0x0600FAAA RID: 64170 RVA: 0x00359F4F File Offset: 0x0035814F
			// (set) Token: 0x0600FAAB RID: 64171 RVA: 0x00359F57 File Offset: 0x00358157
			public int AdditionalInputsSampleSize { get; set; } = 50;

			// Token: 0x170029F3 RID: 10739
			// (get) Token: 0x0600FAAC RID: 64172 RVA: 0x00359F60 File Offset: 0x00358160
			// (set) Token: 0x0600FAAD RID: 64173 RVA: 0x00359F68 File Offset: 0x00358168
			public int ParseNumberAdditionalInputsSampleSize { get; set; } = 10;

			// Token: 0x170029F4 RID: 10740
			// (get) Token: 0x0600FAAE RID: 64174 RVA: 0x00359F71 File Offset: 0x00358171
			// (set) Token: 0x0600FAAF RID: 64175 RVA: 0x00359F79 File Offset: 0x00358179
			public IImmutableSet<CustomExtractor> ExternalExtractors { get; set; } = ImmutableHashSet<CustomExtractor>.Empty;

			// Token: 0x170029F5 RID: 10741
			// (get) Token: 0x0600FAB0 RID: 64176 RVA: 0x00359F82 File Offset: 0x00358182
			// (set) Token: 0x0600FAB1 RID: 64177 RVA: 0x00359F8A File Offset: 0x0035818A
			public int? MaxBranchCount { get; set; }

			// Token: 0x170029F6 RID: 10742
			// (get) Token: 0x0600FAB2 RID: 64178 RVA: 0x00359F93 File Offset: 0x00358193
			// (set) Token: 0x0600FAB3 RID: 64179 RVA: 0x00359F9B File Offset: 0x0035819B
			public int? ConditionalClusterLimit { get; set; }

			// Token: 0x170029F7 RID: 10743
			// (get) Token: 0x0600FAB4 RID: 64180 RVA: 0x00359FA4 File Offset: 0x003581A4
			// (set) Token: 0x0600FAB5 RID: 64181 RVA: 0x00359FAC File Offset: 0x003581AC
			public bool AvoidImperialDateTimeFormat { get; set; }

			// Token: 0x0600FAB6 RID: 64182 RVA: 0x00359FB8 File Offset: 0x003581B8
			public static Witnesses.Options Copy(Witnesses.Options o)
			{
				return new Witnesses.Options
				{
					AdditionalInputsSampleSize = o.AdditionalInputsSampleSize,
					ForbidConstantProgram = o.ForbidConstantProgram,
					AllowedTransformations = o.AllowedTransformations,
					ConcatLocation = o.ConcatLocation,
					ExternalExtractors = o.ExternalExtractors,
					ForbiddenConstants = o.ForbiddenConstants,
					LookupFallbackMode = o.LookupFallbackMode,
					OutputType = o.OutputType,
					ParseNumberAdditionalInputsSampleSize = o.ParseNumberAdditionalInputsSampleSize,
					ParseSingleDateFormat = o.ParseSingleDateFormat,
					RandomSeed = o.RandomSeed,
					RequiredColumns = o.RequiredColumns,
					SynthesisLogFilenamePrefix = o.SynthesisLogFilenamePrefix,
					LogInfo = o.LogInfo,
					EntityDetectorsMap = o.EntityDetectorsMap,
					MaxBranchCount = o.MaxBranchCount,
					ConditionalClusterLimit = o.ConditionalClusterLimit
				};
			}

			// Token: 0x04005CD7 RID: 23767
			internal const int DefaultAdditionalInputsSampleSize = 50;
		}

		// Token: 0x02001CD7 RID: 7383
		private struct ConstantProgramSetPartition
		{
			// Token: 0x170029F8 RID: 10744
			// (get) Token: 0x0600FABE RID: 64190 RVA: 0x0035A17B File Offset: 0x0035837B
			// (set) Token: 0x0600FABF RID: 64191 RVA: 0x0035A183 File Offset: 0x00358383
			public ProgramSet Constant { readonly get; set; }

			// Token: 0x170029F9 RID: 10745
			// (get) Token: 0x0600FAC0 RID: 64192 RVA: 0x0035A18C File Offset: 0x0035838C
			// (set) Token: 0x0600FAC1 RID: 64193 RVA: 0x0035A194 File Offset: 0x00358394
			public ProgramSet NonConstant { readonly get; set; }
		}

		// Token: 0x02001CD8 RID: 7384
		private class ConstantProgramSetFilter : ProgramSetVisitor<Witnesses.ConstantProgramSetPartition>
		{
			// Token: 0x0600FAC2 RID: 64194 RVA: 0x0035A19D File Offset: 0x0035839D
			public ConstantProgramSetFilter(GrammarBuilders build, InputsUsed inputsUsed)
			{
				this._inputsUsed = inputsUsed;
				this._build = build;
			}

			// Token: 0x0600FAC3 RID: 64195 RVA: 0x0035A1C4 File Offset: 0x003583C4
			public override Witnesses.ConstantProgramSetPartition VisitJoin(JoinProgramSet set)
			{
				Witnesses.ConstantProgramSetPartition constantProgramSetPartition;
				if (this._cache.TryGetValue(set, out constantProgramSetPartition))
				{
					return constantProgramSetPartition;
				}
				if (set.Rule == this._build.Rule.ConstStr)
				{
					return this._cache[set] = new Witnesses.ConstantProgramSetPartition
					{
						Constant = set,
						NonConstant = ProgramSet.Empty(set.Symbol)
					};
				}
				if (set.ParameterSpaces.Length == 1)
				{
					Witnesses.ConstantProgramSetPartition constantProgramSetPartition2 = set.ParameterSpaces[0].AcceptVisitor<Witnesses.ConstantProgramSetPartition>(this);
					return this._cache[set] = new Witnesses.ConstantProgramSetPartition
					{
						Constant = ProgramSet.Join(set.Rule, new ProgramSet[] { constantProgramSetPartition2.Constant }),
						NonConstant = ProgramSet.Join(set.Rule, new ProgramSet[] { constantProgramSetPartition2.NonConstant })
					};
				}
				if (set.Symbol == this._build.Symbol.f)
				{
					return this._cache[set] = new Witnesses.ConstantProgramSetPartition
					{
						NonConstant = set,
						Constant = ProgramSet.Empty(set.Symbol)
					};
				}
				if (set.Rule != this._build.Rule.Concat)
				{
					string text = "Unexpected join rule: ";
					NonterminalRule rule = set.Rule;
					throw new NotImplementedException(text + ((rule != null) ? rule.ToString() : null));
				}
				Witnesses.ConstantProgramSetPartition constantProgramSetPartition3 = set.ParameterSpaces[0].AcceptVisitor<Witnesses.ConstantProgramSetPartition>(this);
				if (constantProgramSetPartition3.Constant.IsEmpty)
				{
					return this._cache[set] = new Witnesses.ConstantProgramSetPartition
					{
						NonConstant = set,
						Constant = ProgramSet.Empty(set.Symbol)
					};
				}
				Witnesses.ConstantProgramSetPartition constantProgramSetPartition4 = set.ParameterSpaces[1].AcceptVisitor<Witnesses.ConstantProgramSetPartition>(this);
				return this._cache[set] = new Witnesses.ConstantProgramSetPartition
				{
					NonConstant = ProgramSetBuilder.NormalizedUnion<e>(new ProgramSetBuilder<e>[]
					{
						this._build.Set.Join.Concat(this._build.Set.Cast.f(constantProgramSetPartition3.NonConstant), ProgramSetBuilder.NormalizedUnion<e>(new ProgramSetBuilder<e>[]
						{
							this._build.Set.Cast.e(constantProgramSetPartition4.Constant),
							this._build.Set.Cast.e(constantProgramSetPartition4.NonConstant)
						})),
						this._build.Set.Join.Concat(this._build.Set.Cast.f(constantProgramSetPartition3.Constant), this._build.Set.Cast.e(constantProgramSetPartition4.NonConstant))
					}).Set,
					Constant = this._build.Set.Join.Concat(this._build.Set.Cast.f(constantProgramSetPartition3.Constant), this._build.Set.Cast.e(constantProgramSetPartition4.Constant)).Set
				};
			}

			// Token: 0x0600FAC4 RID: 64196 RVA: 0x0035A4E8 File Offset: 0x003586E8
			public override Witnesses.ConstantProgramSetPartition VisitDirect(DirectProgramSet set)
			{
				Witnesses.ConstantProgramSetPartition constantProgramSetPartition;
				if (this._cache.TryGetValue(set, out constantProgramSetPartition))
				{
					return constantProgramSetPartition;
				}
				List<ProgramNode> list = new List<ProgramNode>();
				List<ProgramNode> list2 = new List<ProgramNode>();
				foreach (ProgramNode programNode in set.RealizedPrograms)
				{
					if (programNode.GetFeatureValue<IImmutableSet<string>>(this._inputsUsed, null).Count == 0)
					{
						list.Add(programNode);
					}
					else
					{
						list2.Add(programNode);
					}
				}
				return this._cache[set] = new Witnesses.ConstantProgramSetPartition
				{
					Constant = ProgramSet.List(set.Symbol, list),
					NonConstant = ProgramSet.List(set.Symbol, list2)
				};
			}

			// Token: 0x0600FAC5 RID: 64197 RVA: 0x0035A5B8 File Offset: 0x003587B8
			public override Witnesses.ConstantProgramSetPartition VisitUnion(UnionProgramSet set)
			{
				Witnesses.ConstantProgramSetPartition constantProgramSetPartition;
				if (this._cache.TryGetValue(set, out constantProgramSetPartition))
				{
					return constantProgramSetPartition;
				}
				IReadOnlyList<Witnesses.ConstantProgramSetPartition> readOnlyList = set.UnionSpaces.Select((ProgramSet space) => space.AcceptVisitor<Witnesses.ConstantProgramSetPartition>(this)).ToList<Witnesses.ConstantProgramSetPartition>();
				Dictionary<ProgramSet, Witnesses.ConstantProgramSetPartition> cache = this._cache;
				Witnesses.ConstantProgramSetPartition constantProgramSetPartition2 = default(Witnesses.ConstantProgramSetPartition);
				constantProgramSetPartition2.Constant = readOnlyList.Select((Witnesses.ConstantProgramSetPartition p) => p.Constant).NormalizedUnion();
				constantProgramSetPartition2.NonConstant = readOnlyList.Select((Witnesses.ConstantProgramSetPartition p) => p.NonConstant).NormalizedUnion();
				return cache[set] = constantProgramSetPartition2;
			}

			// Token: 0x04005CE3 RID: 23779
			private readonly InputsUsed _inputsUsed;

			// Token: 0x04005CE4 RID: 23780
			private readonly GrammarBuilders _build;

			// Token: 0x04005CE5 RID: 23781
			private readonly Dictionary<ProgramSet, Witnesses.ConstantProgramSetPartition> _cache = new Dictionary<ProgramSet, Witnesses.ConstantProgramSetPartition>(IdentityEquality.Comparer);
		}

		// Token: 0x02001CDA RID: 7386
		private class AcceptableNoConcatNodeVisitor : ProgramNodeVisitor<bool>
		{
			// Token: 0x0600FACB RID: 64203 RVA: 0x0035A695 File Offset: 0x00358895
			public AcceptableNoConcatNodeVisitor(GrammarBuilders build)
			{
				this._build = build;
			}

			// Token: 0x0600FACC RID: 64204 RVA: 0x0035A6A4 File Offset: 0x003588A4
			public override bool VisitNonterminal(NonterminalNode node)
			{
				Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes.FormatDateTimeRange formatDateTimeRange;
				return node.Rule != this._build.Rule.Lookup && node.Rule != this._build.Rule.ConstStr && node.Rule != this._build.Rule.ToLowercase && node.Rule != this._build.Rule.ToSimpleTitleCase && node.Rule != this._build.Rule.ToUppercase && (!this._build.Node.IsRule.FormatDateTimeRange(node, out formatDateTimeRange) || Witnesses.AcceptableNoConcatNodeVisitor.IsValidDateTimeRangeSeparatorNode(formatDateTimeRange.s));
			}

			// Token: 0x0600FACD RID: 64205 RVA: 0x0035A754 File Offset: 0x00358954
			public override bool VisitLet(LetNode node)
			{
				return (node.Rule != this._build.Rule.LetColumnName && node.Rule != this._build.Rule.LetX && node.Rule != this._build.Rule.LetCell) || node.BodyNode.AcceptVisitor<bool>(this);
			}

			// Token: 0x0600FACE RID: 64206 RVA: 0x000170F6 File Offset: 0x000152F6
			public override bool VisitLambda(LambdaNode node)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600FACF RID: 64207 RVA: 0x000170F6 File Offset: 0x000152F6
			public override bool VisitLiteral(LiteralNode node)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600FAD0 RID: 64208 RVA: 0x000170F6 File Offset: 0x000152F6
			public override bool VisitVariable(VariableNode node)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600FAD1 RID: 64209 RVA: 0x000170F6 File Offset: 0x000152F6
			public override bool VisitHole(Hole node)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600FAD2 RID: 64210 RVA: 0x0035A7B7 File Offset: 0x003589B7
			internal static bool IsValidDateTimeRangeSeparatorNode(s s)
			{
				IEnumerable<char> value = s.Value;
				Func<char, bool> func;
				if ((func = Witnesses.AcceptableNoConcatNodeVisitor.<>O.<0>__IsDigit) == null)
				{
					func = (Witnesses.AcceptableNoConcatNodeVisitor.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
				}
				return !value.Any(func);
			}

			// Token: 0x04005CE9 RID: 23785
			private readonly GrammarBuilders _build;

			// Token: 0x02001CDB RID: 7387
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04005CEA RID: 23786
				public static Func<char, bool> <0>__IsDigit;
			}
		}

		// Token: 0x02001CDC RID: 7388
		private class AcceptableNoConcatSetVistor : ProgramSetVisitor<bool>
		{
			// Token: 0x0600FAD3 RID: 64211 RVA: 0x0035A7E3 File Offset: 0x003589E3
			public AcceptableNoConcatSetVistor(GrammarBuilders build, Witnesses.AcceptableNoConcatNodeVisitor acceptableNoConcatNodeVisitor)
			{
				this._build = build;
				this._acceptableNoConcatNodeVisitor = acceptableNoConcatNodeVisitor;
			}

			// Token: 0x0600FAD4 RID: 64212 RVA: 0x0035A7FC File Offset: 0x003589FC
			public override bool VisitJoin(JoinProgramSet set)
			{
				if (set.Rule == this._build.Rule.LetColumnName || set.Rule == this._build.Rule.LetX || set.Rule == this._build.Rule.LetCell)
				{
					return set.ParameterSpaces[1].AcceptVisitor<bool>(this);
				}
				if (set.Rule == this._build.Rule.Lookup || set.Rule == this._build.Rule.ConstStr || set.Rule == this._build.Rule.ToLowercase || set.Rule == this._build.Rule.ToSimpleTitleCase || set.Rule == this._build.Rule.ToUppercase)
				{
					return false;
				}
				if (set.Rule == this._build.Rule.FormatDateTimeRange)
				{
					IEnumerable<s> enumerable = set.ParameterSpaces[2].RealizedPrograms.Select(new Func<ProgramNode, s>(this._build.Node.Cast.s));
					Func<s, bool> func;
					if ((func = Witnesses.AcceptableNoConcatSetVistor.<>O.<0>__IsValidDateTimeRangeSeparatorNode) == null)
					{
						func = (Witnesses.AcceptableNoConcatSetVistor.<>O.<0>__IsValidDateTimeRangeSeparatorNode = new Func<s, bool>(Witnesses.AcceptableNoConcatNodeVisitor.IsValidDateTimeRangeSeparatorNode));
					}
					return enumerable.Any(func);
				}
				return true;
			}

			// Token: 0x0600FAD5 RID: 64213 RVA: 0x0035A941 File Offset: 0x00358B41
			public override bool VisitDirect(DirectProgramSet set)
			{
				return set.RealizedPrograms.Any((ProgramNode program) => program.AcceptVisitor<bool>(this._acceptableNoConcatNodeVisitor));
			}

			// Token: 0x0600FAD6 RID: 64214 RVA: 0x0035A95A File Offset: 0x00358B5A
			public override bool VisitUnion(UnionProgramSet set)
			{
				return set.UnionSpaces.Any((ProgramSet space) => space.AcceptVisitor<bool>(this));
			}

			// Token: 0x04005CEB RID: 23787
			private readonly GrammarBuilders _build;

			// Token: 0x04005CEC RID: 23788
			private readonly Witnesses.AcceptableNoConcatNodeVisitor _acceptableNoConcatNodeVisitor;

			// Token: 0x02001CDD RID: 7389
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x04005CED RID: 23789
				public static Func<s, bool> <0>__IsValidDateTimeRangeSeparatorNode;
			}
		}

		// Token: 0x02001CDE RID: 7390
		private struct MatchWithState
		{
			// Token: 0x170029FA RID: 10746
			// (get) Token: 0x0600FAD9 RID: 64217 RVA: 0x0035A98A File Offset: 0x00358B8A
			public readonly State InputState { get; }

			// Token: 0x170029FB RID: 10747
			// (get) Token: 0x0600FADA RID: 64218 RVA: 0x0035A992 File Offset: 0x00358B92
			public readonly DateTimeFormatMatch Match { get; }

			// Token: 0x0600FADB RID: 64219 RVA: 0x0035A99A File Offset: 0x00358B9A
			public MatchWithState(State inputState, DateTimeFormatMatch match)
			{
				this.InputState = inputState;
				this.Match = match;
			}
		}

		// Token: 0x02001CDF RID: 7391
		private struct OutputDateTimeFormatInfo
		{
			// Token: 0x0600FADC RID: 64220 RVA: 0x0035A9AA File Offset: 0x00358BAA
			public OutputDateTimeFormatInfo(DateTimeFormat format, DateTimePartSet? parts, bool outputFormatIsFromType)
			{
				this.Format = format;
				this.Parts = parts;
				this.RangeFormat = null;
				this.OutputDtMatchByState = null;
				this.OutputDtRangeByState = null;
				this.OutputFormatIsFromType = outputFormatIsFromType;
			}

			// Token: 0x0600FADD RID: 64221 RVA: 0x0035A9D8 File Offset: 0x00358BD8
			public OutputDateTimeFormatInfo(IGrouping<DateTimeFormat, Witnesses.MatchWithState> g, bool outputFormatIsFromType)
			{
				this.Format = g.Key;
				this.Parts = new DateTimePartSet?(g.Key.MatchedParts);
				this.RangeFormat = null;
				this.OutputDtMatchByState = (from v in g
					group v.Match by v.InputState).ToMultiValueDictionary(null);
				this.OutputDtRangeByState = null;
				this.OutputFormatIsFromType = outputFormatIsFromType;
			}

			// Token: 0x0600FADE RID: 64222 RVA: 0x0035AA6C File Offset: 0x00358C6C
			public OutputDateTimeFormatInfo(KeyValuePair<Witnesses.DateTimeRangeFormat, IReadOnlyCollection<Witnesses.DateTimeRangeWithState>> p)
			{
				this.Format = p.Key.Format;
				this.Parts = new DateTimePartSet?(p.Key.Format.MatchedParts);
				this.RangeFormat = p.Key;
				this.OutputDtMatchByState = null;
				this.OutputDtRangeByState = (from v in p.Value
					group v.Range by v.InputState).ToMultiValueDictionary(null);
				this.OutputFormatIsFromType = false;
			}

			// Token: 0x170029FC RID: 10748
			// (get) Token: 0x0600FADF RID: 64223 RVA: 0x0035AB18 File Offset: 0x00358D18
			public readonly DateTimeFormat Format { get; }

			// Token: 0x170029FD RID: 10749
			// (get) Token: 0x0600FAE0 RID: 64224 RVA: 0x0035AB20 File Offset: 0x00358D20
			public readonly DateTimePartSet? Parts { get; }

			// Token: 0x170029FE RID: 10750
			// (get) Token: 0x0600FAE1 RID: 64225 RVA: 0x0035AB28 File Offset: 0x00358D28
			public readonly Witnesses.DateTimeRangeFormat RangeFormat { get; }

			// Token: 0x170029FF RID: 10751
			// (get) Token: 0x0600FAE2 RID: 64226 RVA: 0x0035AB30 File Offset: 0x00358D30
			public readonly MultiValueDictionary<State, DateTimeFormatMatch> OutputDtMatchByState { get; }

			// Token: 0x17002A00 RID: 10752
			// (get) Token: 0x0600FAE3 RID: 64227 RVA: 0x0035AB38 File Offset: 0x00358D38
			public readonly MultiValueDictionary<State, Witnesses.DateTimeRangeMatch> OutputDtRangeByState { get; }

			// Token: 0x17002A01 RID: 10753
			// (get) Token: 0x0600FAE4 RID: 64228 RVA: 0x0035AB40 File Offset: 0x00358D40
			public readonly bool OutputFormatIsFromType { get; }

			// Token: 0x17002A02 RID: 10754
			// (get) Token: 0x0600FAE5 RID: 64229 RVA: 0x0035AB48 File Offset: 0x00358D48
			public bool AllowNumericInputFormats
			{
				get
				{
					if (!this.OutputFormatIsFromType)
					{
						DateTimeFormat format = this.Format;
						return format == null || !format.IsNumeric;
					}
					return true;
				}
			}
		}

		// Token: 0x02001CE1 RID: 7393
		private class DateTimeRangeFormat : Tuple<DateTimeFormat, string, DateTimeRoundingSpec, DateTimeRoundingSpec>
		{
			// Token: 0x0600FAEC RID: 64236 RVA: 0x0035AB96 File Offset: 0x00358D96
			internal DateTimeRangeFormat(DateTimeFormat format, string separator, DateTimeRoundingSpec lowerRoundingSpec, DateTimeRoundingSpec upperRoundingSpec)
				: base(format, separator, lowerRoundingSpec, upperRoundingSpec)
			{
			}

			// Token: 0x17002A03 RID: 10755
			// (get) Token: 0x0600FAED RID: 64237 RVA: 0x0035ABA3 File Offset: 0x00358DA3
			internal DateTimeFormat Format
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x17002A04 RID: 10756
			// (get) Token: 0x0600FAEE RID: 64238 RVA: 0x0035ABAB File Offset: 0x00358DAB
			internal string Separator
			{
				get
				{
					return base.Item2;
				}
			}

			// Token: 0x17002A05 RID: 10757
			// (get) Token: 0x0600FAEF RID: 64239 RVA: 0x0035ABB3 File Offset: 0x00358DB3
			internal DateTimeRoundingSpec LowerRoundingSpec
			{
				get
				{
					return base.Item3;
				}
			}

			// Token: 0x17002A06 RID: 10758
			// (get) Token: 0x0600FAF0 RID: 64240 RVA: 0x0035ABBB File Offset: 0x00358DBB
			internal DateTimeRoundingSpec UpperRoundingSpec
			{
				get
				{
					return base.Item4;
				}
			}
		}

		// Token: 0x02001CE2 RID: 7394
		private class DateTimeRangeWithState
		{
			// Token: 0x0600FAF1 RID: 64241 RVA: 0x0035ABC3 File Offset: 0x00358DC3
			internal DateTimeRangeWithState(State inputState, PartialDateTime lower, PartialDateTime upper)
			{
				this.InputState = inputState;
				this.Range = new Witnesses.DateTimeRangeMatch(lower, upper);
			}

			// Token: 0x17002A07 RID: 10759
			// (get) Token: 0x0600FAF2 RID: 64242 RVA: 0x0035ABDF File Offset: 0x00358DDF
			internal State InputState { get; }

			// Token: 0x17002A08 RID: 10760
			// (get) Token: 0x0600FAF3 RID: 64243 RVA: 0x0035ABE7 File Offset: 0x00358DE7
			internal Witnesses.DateTimeRangeMatch Range { get; }
		}

		// Token: 0x02001CE3 RID: 7395
		private class DateTimeRangeMatch
		{
			// Token: 0x0600FAF4 RID: 64244 RVA: 0x0035ABEF File Offset: 0x00358DEF
			internal DateTimeRangeMatch(PartialDateTime lower, PartialDateTime upper)
			{
				this.Lower = lower;
				this.Upper = upper;
			}

			// Token: 0x17002A09 RID: 10761
			// (get) Token: 0x0600FAF5 RID: 64245 RVA: 0x0035AC05 File Offset: 0x00358E05
			internal PartialDateTime Lower { get; }

			// Token: 0x17002A0A RID: 10762
			// (get) Token: 0x0600FAF6 RID: 64246 RVA: 0x0035AC0D File Offset: 0x00358E0D
			internal PartialDateTime Upper { get; }
		}

		// Token: 0x02001CE4 RID: 7396
		private class ExclusionInfo : Tuple<long, DateTimePart?, uint>
		{
			// Token: 0x0600FAF7 RID: 64247 RVA: 0x0035AC18 File Offset: 0x00358E18
			public ExclusionInfo()
				: this(0L, null, 0U)
			{
			}

			// Token: 0x0600FAF8 RID: 64248 RVA: 0x0035AC37 File Offset: 0x00358E37
			public ExclusionInfo(long milliseconds, DateTimePart unit, uint unitAmount)
				: this(milliseconds, new DateTimePart?(unit), unitAmount)
			{
			}

			// Token: 0x0600FAF9 RID: 64249 RVA: 0x0035AC47 File Offset: 0x00358E47
			private ExclusionInfo(long milliseconds, DateTimePart? unit, uint unitAmount)
				: base(milliseconds, unit, unitAmount)
			{
			}

			// Token: 0x17002A0B RID: 10763
			// (get) Token: 0x0600FAFA RID: 64250 RVA: 0x0035AC52 File Offset: 0x00358E52
			public long Milliseconds
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x17002A0C RID: 10764
			// (get) Token: 0x0600FAFB RID: 64251 RVA: 0x0035AC5A File Offset: 0x00358E5A
			public DateTimePart? Unit
			{
				get
				{
					return base.Item2;
				}
			}

			// Token: 0x17002A0D RID: 10765
			// (get) Token: 0x0600FAFC RID: 64252 RVA: 0x0035AC62 File Offset: 0x00358E62
			public uint UnitAmount
			{
				get
				{
					return base.Item3;
				}
			}
		}

		// Token: 0x02001CE5 RID: 7397
		private class NumericRangeLearningInfo : Tuple<string, decimal, decimal, decimal>
		{
			// Token: 0x0600FAFD RID: 64253 RVA: 0x0035AC6A File Offset: 0x00358E6A
			public NumericRangeLearningInfo(string separator, decimal delta, decimal lowerZero, decimal upperZero)
				: base(separator, delta, lowerZero, upperZero)
			{
			}

			// Token: 0x17002A0E RID: 10766
			// (get) Token: 0x0600FAFE RID: 64254 RVA: 0x0035AC77 File Offset: 0x00358E77
			public string Separator
			{
				get
				{
					return base.Item1;
				}
			}

			// Token: 0x17002A0F RID: 10767
			// (get) Token: 0x0600FAFF RID: 64255 RVA: 0x0035AC7F File Offset: 0x00358E7F
			public decimal Delta
			{
				get
				{
					return base.Item2;
				}
			}

			// Token: 0x17002A10 RID: 10768
			// (get) Token: 0x0600FB00 RID: 64256 RVA: 0x0035AC87 File Offset: 0x00358E87
			public decimal LowerZero
			{
				get
				{
					return base.Item3;
				}
			}

			// Token: 0x17002A11 RID: 10769
			// (get) Token: 0x0600FB01 RID: 64257 RVA: 0x0035AC8F File Offset: 0x00358E8F
			public decimal UpperZero
			{
				get
				{
					return base.Item4;
				}
			}
		}

		// Token: 0x02001CE6 RID: 7398
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005CFF RID: 23807
			public static Func<char, bool> <0>__IsLetter;

			// Token: 0x04005D00 RID: 23808
			public static Func<ValueSubstring, ValueSubstring> <1>__ToLowercase;

			// Token: 0x04005D01 RID: 23809
			public static Func<ValueSubstring, ValueSubstring> <2>__ToUppercase;

			// Token: 0x04005D02 RID: 23810
			public static Func<ValueSubstring, ValueSubstring> <3>__ToSimpleTitleCase;

			// Token: 0x04005D03 RID: 23811
			public static Func<HashSet<DateTimeFormat>, IReadOnlyList<IReadOnlyList<DateTimeFormat>>> <4>__GroupParsingFormats;

			// Token: 0x04005D04 RID: 23812
			public static Func<char, bool> <5>__IsDigit;

			// Token: 0x04005D05 RID: 23813
			public static Func<int, bool> <6>__IsAllowedScalePower;

			// Token: 0x04005D06 RID: 23814
			public static Func<ValueSubstring, bool> <7>__IsValidNumberString;
		}
	}
}
