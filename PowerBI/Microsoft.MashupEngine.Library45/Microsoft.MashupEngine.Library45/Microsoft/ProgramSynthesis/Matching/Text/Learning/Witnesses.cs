using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Matching.Text.Build;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Clustering;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text.Learning
{
	// Token: 0x02001212 RID: 4626
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x06008B5D RID: 35677 RVA: 0x001D2E2C File Offset: 0x001D102C
		public Witnesses(Grammar grammar, Feature<double> scoreFeature, Witnesses.Options options, HashSet<IToken> tokens)
			: base(grammar)
		{
			if (!options.IsValid())
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid range for number of disjuncts: 1 <= min({0}) <= max({1}) must hold.", new object[] { options.MinDisjuncts, options.MaxDisjuncts })));
			}
			this._build = GrammarBuilders.Instance(grammar);
			this._options = options;
			this._score = scoreFeature;
			this._allDefaultTokens = tokens;
			this._charClassTokens = tokens.OfType<CharClassToken>().ConvertToHashSet<IToken>();
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x06008B5E RID: 35678 RVA: 0x001D2EB3 File Offset: 0x001D10B3
		public static Func<double, double> ScoreTransform { get; } = new Func<double, double>(Math.Sqrt);

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x06008B5F RID: 35679 RVA: 0x001D2EBA File Offset: 0x001D10BA
		// (set) Token: 0x06008B60 RID: 35680 RVA: 0x001D2EC2 File Offset: 0x001D10C2
		public IReadOnlyList<Cluster<match>> MostRecentClustering { get; private set; }

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x06008B61 RID: 35681 RVA: 0x001D2ECB File Offset: 0x001D10CB
		// (set) Token: 0x06008B62 RID: 35682 RVA: 0x001D2ED3 File Offset: 0x001D10D3
		public ClusteringResult<match> MostRecentClusteringResult { get; private set; }

		// Token: 0x06008B63 RID: 35683 RVA: 0x001D2EDC File Offset: 0x001D10DC
		private HashSet<IToken> GetPossibleTokens(IEnumerable<SuffixRegion> sRegions)
		{
			HashSet<IToken> hashSet = new HashSet<IToken>(this._allDefaultTokens);
			Dictionary<IToken, uint> charClassMatchLengths = this._charClassTokens.ToDictionary((IToken token) => token, (IToken token) => 0U);
			using (IEnumerator<SuffixRegion> enumerator = sRegions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SuffixRegion sRegion = enumerator.Current;
					hashSet.RemoveWhere(delegate(IToken token)
					{
						uint num = sRegion.PrefixMatchLength(token);
						if (num < 1U)
						{
							charClassMatchLengths.Remove(token);
							return true;
						}
						uint num2;
						if (!charClassMatchLengths.TryGetValue(token, out num2))
						{
							return false;
						}
						if (num2 == 0U)
						{
							charClassMatchLengths[token] = num;
						}
						else if (num2 != num)
						{
							charClassMatchLengths.Remove(token);
						}
						return false;
					});
				}
			}
			hashSet.AddRange(charClassMatchLengths.Select(delegate(KeyValuePair<IToken, uint> pair)
			{
				CharClassToken charClassToken = pair.Key as CharClassToken;
				if (charClassToken == null)
				{
					return null;
				}
				return charClassToken.GetTokenForLength(pair.Value);
			}));
			return hashSet;
		}

		// Token: 0x06008B64 RID: 35684 RVA: 0x001D2FD4 File Offset: 0x001D11D4
		private int CommonPrefixLength(IReadOnlyList<SuffixRegion> sRegions)
		{
			SuffixRegion suffixRegion = sRegions[0];
			int num = suffixRegion.Length;
			foreach (SuffixRegion suffixRegion2 in sRegions.Skip(1))
			{
				num = Math.Min(num, suffixRegion2.Length);
				for (int i = 0; i < num; i++)
				{
					if (suffixRegion2[i] != suffixRegion[i])
					{
						num = i;
						break;
					}
				}
				if (num == 0)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06008B65 RID: 35685 RVA: 0x001D3064 File Offset: 0x001D1264
		[WitnessFunction("LetSplit", 0)]
		internal DisjunctiveExamplesSpec WitnessLetSplit(LetRule rule, ExampleSpec spec)
		{
			List<SuffixRegion> list = spec.ProvidedInputs.Select((State state) => state[this._build.Symbol.sRegion] as SuffixRegion).ToList<SuffixRegion>();
			if (list.Any((SuffixRegion sRegion) => sRegion == null || sRegion.Source == null || sRegion.Length == 0))
			{
				return null;
			}
			SuffixRegion suffixRegion2 = list.First<SuffixRegion>();
			int i = (char.IsWhiteSpace(suffixRegion2[0]) ? 0 : this.CommonPrefixLength(list));
			HashSet<IToken> possibleTokens;
			if (this._options.UseLongConstantOptimization && i > 0)
			{
				possibleTokens = new HashSet<IToken>
				{
					new ConstantToken(suffixRegion2.Value.Substring(0, i), null)
				};
			}
			else
			{
				possibleTokens = this.GetPossibleTokens(list);
				while (i > 0)
				{
					if (!char.IsWhiteSpace(suffixRegion2[i - 1]))
					{
						possibleTokens.Add(new ConstantToken(suffixRegion2.Value.Substring(0, i), null));
					}
					i--;
				}
			}
			return DisjunctiveExamplesSpec.From(list.Zip(spec.ProvidedInputs, (SuffixRegion suffixRegion, State input) => new { suffixRegion, input }).ToDictionary(o => o.input, o => possibleTokens.Select(new Func<IToken, SuffixRegion>(o.suffixRegion.UnmatchedSuffix)).Cast<object>()));
		}

		// Token: 0x06008B66 RID: 35686 RVA: 0x001D31D4 File Offset: 0x001D13D4
		private ProblemSpace<State> CreateStateProblemSpace(IEnumerable<KeyValuePair<State, uint>> sampleInputs, SynthesisEngine engine, OperatorRule disjunctionRule, LearningTask top1Task, CancellationToken cancel)
		{
			return new ProblemSpace<State>(sampleInputs.ToDictionary<State, uint>(), delegate(State stateA, State stateB)
			{
				Witnesses.<>c__DisplayClass20_1 CS$<>8__locals2 = new Witnesses.<>c__DisplayClass20_1();
				LearningTask top1Task2 = top1Task;
				IJoinLanguage disjunctionRule2 = disjunctionRule;
				int num = 0;
				Dictionary<State, object> dictionary = new Dictionary<State, object>();
				dictionary[stateA] = true;
				dictionary[stateB] = true;
				LearningTask learningTask = top1Task2.MakeSubtask(disjunctionRule2, num, new ExampleSpec(dictionary));
				Witnesses.<>c__DisplayClass20_1 CS$<>8__locals3 = CS$<>8__locals2;
				ProgramSet programSet = engine.Learn(learningTask, cancel);
				CS$<>8__locals3.program = ((programSet != null) ? programSet.RealizedPrograms.FirstOrDefault<ProgramNode>() : null);
				if (CS$<>8__locals2.program == null)
				{
					return new KeyValuePair<double, ProblemSpace<State>.DistanceBoundFunction>(Witnesses.ScoreTransform(-DefaultTokens.Any.Score), null);
				}
				CS$<>8__locals2.dist = Witnesses.ScoreTransform(-CS$<>8__locals2.program.GetFeatureValue<double>(this._score, null));
				return new KeyValuePair<double, ProblemSpace<State>.DistanceBoundFunction>(CS$<>8__locals2.dist, delegate(State s)
				{
					object obj = CS$<>8__locals2.program.Invoke(s);
					if (obj == null || !obj.Equals(true))
					{
						return null;
					}
					return new EquatablePair<double, double>?(EquatablePair.Create<double, double>(CS$<>8__locals2.dist, CS$<>8__locals2.dist));
				});
			}, 0.0);
		}

		// Token: 0x06008B67 RID: 35687 RVA: 0x001D322C File Offset: 0x001D142C
		[RuleLearner("Disjunction")]
		internal Optional<ProgramSet> LearnDisjunction(SynthesisEngine engine, OperatorRule rule, LearningTask<GroupedExamplesSpec> task, CancellationToken cancel)
		{
			ClusteringAlgorithm? clusteringMethod = this._options.ClusteringMethod;
			if (clusteringMethod != null)
			{
				switch (clusteringMethod.GetValueOrDefault())
				{
				case ClusteringAlgorithm.Sampling:
					return this.LearnDisjunctionSampling(engine, rule, task, cancel);
				case ClusteringAlgorithm.AHC:
					if (this._options.InSameCluster.Any<IReadOnlyList<object>>() || this._options.InDifferentCluster.Any<IReadOnlyList<object>>())
					{
						throw new NotImplementedException("AHC algorithm cannot handle cluster constraints.");
					}
					return this.LearnDisjunctionAHC(engine, rule, task, cancel);
				case ClusteringAlgorithm.None:
					return this.LearnDisjunctionNone(engine, rule, task, cancel).Set.Some<ProgramSet>();
				}
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown clustering method: {0}", new object[] { this._options.ClusteringMethod })));
		}

		// Token: 0x06008B68 RID: 35688 RVA: 0x001D32F4 File Offset: 0x001D14F4
		private PredicateLearner<match> BuildPredicateLearner(LearningTask task, NonterminalRule rule, SynthesisEngine engine)
		{
			return new PredicateLearner<match>(task.MakeSubtask(rule, 0, null).WithTopKRequest(1, this._score, null), this._score, engine, new Func<ProgramNode, match>(this._build.Node.Unsafe.match), new Func<State, object>(this.<BuildPredicateLearner>g__ExtractData|22_0), new Func<match, Predicate<object>>(this.<BuildPredicateLearner>g__PatternCompile|22_1), new Func<match, string>(this.<BuildPredicateLearner>g__Describe|22_2));
		}

		// Token: 0x06008B69 RID: 35689 RVA: 0x001D3364 File Offset: 0x001D1564
		internal ProgramSetBuilder<disjunctive_match> LearnDisjunctionNone(SynthesisEngine engine, OperatorRule rule, LearningTask<GroupedExamplesSpec> task, CancellationToken cancel)
		{
			PredicateLearner<match> predicateLearner = this.BuildPredicateLearner(task, rule, engine);
			List<State> list = task.Spec.Examples.Keys.ToList<State>();
			ClusterPredicate<match>? clusterPredicate = predicateLearner.Learn(list, cancel);
			if (clusterPredicate == null)
			{
				return null;
			}
			ClusterPredicate<match> value = clusterPredicate.Value;
			long num = task.Spec.ExamplesWithCounts.Values.Sum((KeyValuePair<object, uint> kvp) => (long)((ulong)kvp.Value));
			Cluster<match> cluster = value.ToCluster(list, list, (uint)num);
			this.MostRecentClusteringResult = new ClusteringResult<match>(new Cluster<match>[] { cluster }, null);
			this.MostRecentClustering = this.MostRecentClusteringResult.Clusters;
			return this._build.Set.Join.Disjunction(cluster.ProgramSet, ProgramSetBuilder.List<disjunctive_match>(new disjunctive_match[] { this._build.Node.Rule.NoMatch() }));
		}

		// Token: 0x06008B6A RID: 35690 RVA: 0x001D345C File Offset: 0x001D165C
		internal Optional<ProgramSet> LearnDisjunctionAHC(SynthesisEngine engine, OperatorRule rule, LearningTask<GroupedExamplesSpec> task, CancellationToken cancel)
		{
			uint num = this._options.MinDisjuncts.Value;
			uint num2 = Math.Min(this._options.MaxDisjuncts.Value, Convert.ToUInt32(task.Spec.ProvidedInputs.Count<State>()));
			uint num3 = Math.Max(this._options.MinDisjuncts.Value, 1U);
			if (num > num2)
			{
				return OptionalUtils.Some((T)null);
			}
			LearningTask learningTask = task.WithTopKRequest(1, this._score, null);
			LearningTask learningTask2 = task.WithoutTopKRequest();
			List<Cluster<match>> allClusters = new List<Cluster<match>>();
			Dictionary<State, uint> dictionary = task.Spec.ExamplesWithCounts.Select((KeyValuePair<State, KeyValuePair<object, uint>> pair) => new KeyValuePair<State, uint>(pair.Key, pair.Value.Value)).ToDictionary<State, uint>();
			SortedProgramSetIntersections sortedProgramSetIntersections = new SortedProgramSetIntersections(engine, dictionary, learningTask2, rule, this._score);
			List<KeyValuePair<State, uint>> list = dictionary.ToList<KeyValuePair<State, uint>>();
			int inputSampleSize = Convert.ToInt32(this._options.SampleSizeFactor * num2);
			Func<ClusterIntersection, int> <>9__9;
			while (list.Count > 0)
			{
				ProblemSpace<State> problemSpace = this.CreateStateProblemSpace(list.Take(inputSampleSize), engine, rule, learningTask, cancel);
				num = Math.Min(this._options.MinDisjuncts.Value, (uint)problemSpace.PointsWithCounts.Count);
				uint num4 = Math.Min(this._options.MaxDisjuncts.Value, Convert.ToUInt32(problemSpace.PointsWithCounts.Count));
				bool flag = (double)problemSpace.Count / 2.0 >= num;
				AgglomerativeHierarchicalClustering<State> agglomerativeHierarchicalClustering = new AgglomerativeHierarchicalClustering<State>(problemSpace, LinkageCriterion<State>.Complete, new uint?(flag ? num4 : num), this._options.ThetaFactor);
				using (SortedSet<Dendrogram<State>>.Enumerator enumerator = (flag ? agglomerativeHierarchicalClustering.MostLikelyClusters(num, num4) : agglomerativeHierarchicalClustering.SplitIntoKClusters(num)).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Dendrogram<State> dendrogram = enumerator.Current;
						ExampleSpec exampleSpec = new ExampleSpec(dendrogram.Data.ToDictionary((State state) => state, (State state) => true));
						LearningTask learningTask3 = learningTask2.MakeSubtask(rule, 0, exampleSpec);
						ProgramSetBuilder<match> newClusterVsa = this._build.Set.Cast.match(engine.Learn(learningTask3, cancel));
						if (ProgramSetBuilder.IsNullOrEmpty<match>(newClusterVsa))
						{
							return OptionalUtils.Some((T)null);
						}
						if (!allClusters.Any((Cluster<match> cluster) => newClusterVsa.Set.Contains(cluster.BestProgramNode.Node)))
						{
							match bestProgram = this._build.Node.Cast.match(engine.Learn(learningTask.MakeSubtask(rule, 0, exampleSpec), default(CancellationToken)).RealizedPrograms.First<ProgramNode>());
							IList<KeyValuePair<State, uint>> list2;
							IList<KeyValuePair<State, uint>> list3;
							list.PartitionByPredicate((KeyValuePair<State, uint> pair) => true.Equals(bestProgram.Node.Invoke(pair.Key)), out list2, out list3);
							list = (list3 as List<KeyValuePair<State, uint>>) ?? list3.ToList<KeyValuePair<State, uint>>();
							if (allClusters.RemoveAll((Cluster<match> cluster) => cluster.ProgramSet.Set.Contains(bestProgram.Node)) > 0)
							{
								sortedProgramSetIntersections.FilterOn(allClusters);
							}
							Cluster<match> cluster2 = new Cluster<match>(newClusterVsa, bestProgram, bestProgram.Node.GetFeatureValue<double>(this._score, null), dendrogram.Data, list2.Select((KeyValuePair<State, uint> pair) => pair.Key).ToList<State>(), (uint)list2.Sum((KeyValuePair<State, uint> pair) => (int)pair.Value), null);
							sortedProgramSetIntersections.AddAllPairsWith(cluster2, allClusters, cancel);
							allClusters.Add(cluster2);
						}
					}
					goto IL_0508;
				}
				goto IL_041E;
				IL_0508:
				if ((long)allClusters.Count <= (long)((ulong)num2))
				{
					continue;
				}
				IL_041E:
				ClusterIntersection clusterIntersection = null;
				if (num3 > 1U)
				{
					IEnumerable<ClusterIntersection> enumerable = sortedProgramSetIntersections;
					Func<ClusterIntersection, int> func;
					if ((func = <>9__9) == null)
					{
						func = (<>9__9 = (ClusterIntersection vsaIntersection) => allClusters.Count((Cluster<match> cluster) => cluster == vsaIntersection.LeftCluster || cluster == vsaIntersection.RightCluster || cluster.ProgramSet.Set.Contains(vsaIntersection.Intersection.BestProgramNode.Node)));
					}
					clusterIntersection = enumerable.ArgMin(func);
				}
				clusterIntersection = clusterIntersection ?? sortedProgramSetIntersections.Best.Value.First<ClusterIntersection>();
				Cluster<match> bestMerge = clusterIntersection.Intersection;
				list.RemoveAll((KeyValuePair<State, uint> pair) => true.Equals(bestMerge.BestProgramNode.Node.Invoke(pair.Key)));
				Cluster<match> parent1 = clusterIntersection.LeftCluster;
				Cluster<match> parent2 = clusterIntersection.RightCluster;
				allClusters.RemoveAll((Cluster<match> cluster) => cluster == parent1 || cluster == parent2 || cluster.ProgramSet.Set.Contains(bestMerge.BestProgramNode.Node));
				sortedProgramSetIntersections.FilterOn(allClusters);
				sortedProgramSetIntersections.AddAllPairsWith(bestMerge, allClusters, default(CancellationToken));
				allClusters.Add(bestMerge);
				goto IL_0508;
			}
			this.MostRecentClustering = allClusters;
			ProgramSetBuilder<disjunctive_match> programSetBuilder = ProgramSetBuilder.List<disjunctive_match>(new disjunctive_match[] { this._build.Node.Rule.NoMatch() });
			this.MostRecentClusteringResult = new ClusteringResult<match>(allClusters, null);
			return this.MostRecentClustering.Reverse<Cluster<match>>().Aggregate(programSetBuilder, (ProgramSetBuilder<disjunctive_match> resultSet, Cluster<match> cluster) => this._build.Set.Join.Disjunction(task.K.HasValue ? ProgramSetBuilder.List<match>(cluster.ProgramSet.Set.TopK(task.TopProgramsFeature.Value, task.K.Value, FeatureCalculationContext.Create(cluster.Data.Take(inputSampleSize).ToList<State>(), null, null), null).Select(new Func<ProgramNode, match>(this._build.Node.Cast.match)).ToArray<match>()) : cluster.ProgramSet, resultSet)).Set.Some<ProgramSet>();
		}

		// Token: 0x06008B6B RID: 35691 RVA: 0x001D3A20 File Offset: 0x001D1C20
		internal Optional<ProgramSet> LearnDisjunctionSampling(SynthesisEngine engine, OperatorRule rule, LearningTask<GroupedExamplesSpec> task, CancellationToken cancel)
		{
			if (task.K.HasValue && task.K.Value != 1)
			{
				throw new NotImplementedException("TopK learning not implemented yet!");
			}
			if (task.RandomK.HasValue)
			{
				throw new NotImplementedException("RandomK learning not implemented yet!");
			}
			PredicateLearner<match> predicateLearner = this.BuildPredicateLearner(task, rule, engine);
			ScoreUtils<match> scoreUtils = new ScoreUtils<match>(10.0, 10.0, 5.0);
			DisjunctionLearner<match> disjunctionLearner = new DisjunctionLearner<match>(task.Spec.ExamplesWithCounts.ToDictionary((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => kvp.Key, (KeyValuePair<State, KeyValuePair<object, uint>> kvp) => kvp.Value.Value), predicateLearner, scoreUtils, 61455);
			DisjunctionLearner<match>.CoverConstraints coverConstraints = new DisjunctionLearner<match>.CoverConstraints(this._options.MinDisjuncts ?? 1U, this._options.MaxDisjuncts ?? Convert.ToUInt32(task.Spec.ProvidedInputs.Count<State>()), (uint)Math.Floor(this._options.MaxOutlierRate * (double)task.Spec.ExamplesWithCounts.Sum((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => (long)((ulong)kvp.Value.Value))));
			ClusterConstraints clusterConstraints = new ClusterConstraints(this._options.InSameCluster.ToList<IReadOnlyList<object>>(), this._options.InDifferentCluster.ToList<IReadOnlyList<object>>());
			this.MostRecentClusteringResult = disjunctionLearner.LearnFromSampling(coverConstraints, clusterConstraints, cancel, this._options.MaxAttempts, this._options.MaxAttemptsAfterFailure);
			if (this._options.IncludeOutlierPatterns)
			{
				this.MostRecentClustering = this.MostRecentClusteringResult.InOrder().ToList<Cluster<match>>();
			}
			else
			{
				this.MostRecentClustering = this.MostRecentClusteringResult.Clusters;
			}
			ProgramSetBuilder<disjunctive_match> programSetBuilder = ProgramSetBuilder.List<disjunctive_match>(this._build.Symbol.disjunctive_match, new disjunctive_match[] { this._build.Node.Rule.NoMatch() });
			return this.MostRecentClustering.Reverse<Cluster<match>>().Aggregate(programSetBuilder, (ProgramSetBuilder<disjunctive_match> resultSet, Cluster<match> cluster) => this._build.Set.Join.Disjunction(ProgramSetBuilder.List<match>(cluster.ProgramSet.Set.Symbol, new match[] { cluster.BestProgramNode }), resultSet)).Set.Some<ProgramSet>();
		}

		// Token: 0x06008B6C RID: 35692 RVA: 0x001D3C80 File Offset: 0x001D1E80
		[WitnessFunction("SuffixAfterTokenMatch", 1)]
		internal DisjunctiveExamplesSpec WitnessTokenInSuffixAfterTokenMatch(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			return DisjunctiveExamplesSpec.From(spec.DisjunctiveExamples.Take(1).ToDictionary((KeyValuePair<State, IEnumerable<object>> example) => example.Key, (KeyValuePair<State, IEnumerable<object>> example) => from SuffixRegion s in example.Value
				select s.PrefixMatchToken));
		}

		// Token: 0x06008B6E RID: 35694 RVA: 0x001D3CF4 File Offset: 0x001D1EF4
		[CompilerGenerated]
		private object <BuildPredicateLearner>g__ExtractData|22_0(State state)
		{
			SuffixRegion suffixRegion = state[this._build.Symbol.sRegion] as SuffixRegion;
			if (suffixRegion == null)
			{
				return null;
			}
			return suffixRegion.Value;
		}

		// Token: 0x06008B6F RID: 35695 RVA: 0x001D3D1C File Offset: 0x001D1F1C
		[CompilerGenerated]
		private Predicate<object> <BuildPredicateLearner>g__PatternCompile|22_1(match programNode)
		{
			MatchingLabel matchingLabel = programNode.Node.AcceptVisitor<MatchingLabel>(new MatchingLabelCollector(this._build));
			switch (matchingLabel.Match)
			{
			case MatchingLabel.MatchType.NullMatch:
				return (object obj) => obj == null;
			case MatchingLabel.MatchType.NoMatch:
				throw new InvalidOperationException();
			case MatchingLabel.MatchType.TokenSequenceMatch:
			{
				string text = TokenUtils.BaseRegexDescriptionFor(matchingLabel.GetTokens().ToList<IToken>());
				if (text == null)
				{
					return null;
				}
				Regex regex = new Regex(text, RegexOptions.Compiled);
				return delegate(object obj)
				{
					string text2 = obj as string;
					return text2 != null && regex.IsMatch(text2);
				};
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06008B70 RID: 35696 RVA: 0x001D3DBD File Offset: 0x001D1FBD
		[CompilerGenerated]
		private string <BuildPredicateLearner>g__Describe|22_2(match programNode)
		{
			return programNode.Node.AcceptVisitor<MatchingLabel>(new MatchingLabelCollector(this._build)).Description();
		}

		// Token: 0x040038DD RID: 14557
		private readonly HashSet<IToken> _allDefaultTokens;

		// Token: 0x040038DE RID: 14558
		private readonly HashSet<IToken> _charClassTokens;

		// Token: 0x040038DF RID: 14559
		private readonly Witnesses.Options _options;

		// Token: 0x040038E0 RID: 14560
		private readonly Feature<double> _score;

		// Token: 0x040038E1 RID: 14561
		private readonly GrammarBuilders _build;

		// Token: 0x02001213 RID: 4627
		public class Options : DSLOptions
		{
			// Token: 0x06008B72 RID: 35698 RVA: 0x001D3E1C File Offset: 0x001D201C
			public Options(uint? minDisjuncts = null, uint? maxDisjuncts = null)
			{
				this.MinDisjuncts = minDisjuncts;
				this.MaxDisjuncts = maxDisjuncts;
				this.InSameCluster = new List<IReadOnlyList<object>>();
				this.InDifferentCluster = new List<IReadOnlyList<object>>();
			}

			// Token: 0x170017E1 RID: 6113
			// (get) Token: 0x06008B73 RID: 35699 RVA: 0x001D3E6D File Offset: 0x001D206D
			// (set) Token: 0x06008B74 RID: 35700 RVA: 0x001D3E75 File Offset: 0x001D2075
			public double? ThetaFactor { get; set; }

			// Token: 0x170017E2 RID: 6114
			// (get) Token: 0x06008B75 RID: 35701 RVA: 0x001D3E7E File Offset: 0x001D207E
			// (set) Token: 0x06008B76 RID: 35702 RVA: 0x001D3E86 File Offset: 0x001D2086
			public double SampleSizeFactor { get; set; } = 2.5;

			// Token: 0x170017E3 RID: 6115
			// (get) Token: 0x06008B77 RID: 35703 RVA: 0x001D3E8F File Offset: 0x001D208F
			// (set) Token: 0x06008B78 RID: 35704 RVA: 0x001D3E97 File Offset: 0x001D2097
			public double MaxOutlierRate { get; set; } = Witnesses.Options.DefaultOutlierRate;

			// Token: 0x170017E4 RID: 6116
			// (get) Token: 0x06008B79 RID: 35705 RVA: 0x001D3EA0 File Offset: 0x001D20A0
			// (set) Token: 0x06008B7A RID: 35706 RVA: 0x001D3EA8 File Offset: 0x001D20A8
			public uint? MinDisjuncts { get; set; }

			// Token: 0x170017E5 RID: 6117
			// (get) Token: 0x06008B7B RID: 35707 RVA: 0x001D3EB1 File Offset: 0x001D20B1
			// (set) Token: 0x06008B7C RID: 35708 RVA: 0x001D3EB9 File Offset: 0x001D20B9
			public uint? MaxDisjuncts { get; set; }

			// Token: 0x170017E6 RID: 6118
			// (get) Token: 0x06008B7D RID: 35709 RVA: 0x001D3EC2 File Offset: 0x001D20C2
			// (set) Token: 0x06008B7E RID: 35710 RVA: 0x001D3ECA File Offset: 0x001D20CA
			public int? MaxAttempts { get; set; }

			// Token: 0x170017E7 RID: 6119
			// (get) Token: 0x06008B7F RID: 35711 RVA: 0x001D3ED3 File Offset: 0x001D20D3
			// (set) Token: 0x06008B80 RID: 35712 RVA: 0x001D3EDB File Offset: 0x001D20DB
			public int? MaxAttemptsAfterFailure { get; set; }

			// Token: 0x170017E8 RID: 6120
			// (get) Token: 0x06008B81 RID: 35713 RVA: 0x001D3EE4 File Offset: 0x001D20E4
			// (set) Token: 0x06008B82 RID: 35714 RVA: 0x001D3EEC File Offset: 0x001D20EC
			public ClusteringAlgorithm? ClusteringMethod { get; set; }

			// Token: 0x170017E9 RID: 6121
			// (get) Token: 0x06008B83 RID: 35715 RVA: 0x001D3EF5 File Offset: 0x001D20F5
			// (set) Token: 0x06008B84 RID: 35716 RVA: 0x001D3EFD File Offset: 0x001D20FD
			public bool UseLongConstantOptimization { get; set; }

			// Token: 0x170017EA RID: 6122
			// (get) Token: 0x06008B85 RID: 35717 RVA: 0x001D3F06 File Offset: 0x001D2106
			// (set) Token: 0x06008B86 RID: 35718 RVA: 0x001D3F0E File Offset: 0x001D210E
			public bool IncludeOutlierPatterns { get; set; }

			// Token: 0x06008B87 RID: 35719 RVA: 0x001D3F18 File Offset: 0x001D2118
			public bool IsValid()
			{
				if (this.MinDisjuncts != null && this.MaxDisjuncts != null)
				{
					uint? minDisjuncts = this.MinDisjuncts;
					uint? num = this.MaxDisjuncts;
					if ((minDisjuncts.GetValueOrDefault() <= num.GetValueOrDefault()) & ((minDisjuncts != null) & (num != null)))
					{
						num = this.MinDisjuncts;
						uint num2 = 1U;
						return (num.GetValueOrDefault() >= num2) & (num != null);
					}
				}
				return false;
			}

			// Token: 0x06008B88 RID: 35720 RVA: 0x001D3F98 File Offset: 0x001D2198
			public override string ToString()
			{
				ClusteringAlgorithm? clusteringAlgorithm;
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}{{{1},{2}}}", new object[]
				{
					((this.ClusteringMethod != null) ? clusteringAlgorithm.GetValueOrDefault().ToString() : null) ?? "Null",
					this.MinDisjuncts,
					this.MaxDisjuncts
				}));
			}

			// Token: 0x040038E5 RID: 14565
			public static double DefaultOutlierRate = 0.03;

			// Token: 0x040038F0 RID: 14576
			public IList<IReadOnlyList<object>> InSameCluster;

			// Token: 0x040038F1 RID: 14577
			public IList<IReadOnlyList<object>> InDifferentCluster;
		}
	}
}
