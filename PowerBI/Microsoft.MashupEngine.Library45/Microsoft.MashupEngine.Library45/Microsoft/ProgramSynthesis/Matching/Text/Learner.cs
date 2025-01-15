using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Logging;
using Microsoft.ProgramSynthesis.Learning.Strategies;
using Microsoft.ProgramSynthesis.Matching.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Matching.Text.Learning;
using Microsoft.ProgramSynthesis.Matching.Text.Semantics;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Clustering;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Matching.Text
{
	// Token: 0x020011AC RID: 4524
	public class Learner : ProgramLearner<Program, string, bool>
	{
		// Token: 0x060086B3 RID: 34483 RVA: 0x001C4605 File Offset: 0x001C2805
		public Learner(Feature<double> scorefeature)
			: base(false, false)
		{
			this.ScoreFeature = scorefeature;
		}

		// Token: 0x17001713 RID: 5907
		// (get) Token: 0x060086B4 RID: 34484 RVA: 0x001C4636 File Offset: 0x001C2836
		public static Learner Instance { get; } = new Learner(new RankingScore(Language.Grammar));

		// Token: 0x17001714 RID: 5908
		// (get) Token: 0x060086B5 RID: 34485 RVA: 0x001C463D File Offset: 0x001C283D
		public override Feature<double> ScoreFeature { get; }

		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x060086B6 RID: 34486 RVA: 0x001C4645 File Offset: 0x001C2845
		public Feature<IEnumerable<ProgramNode>> DisjunctsFeature { get; } = new DisjunctsFeature(Language.Grammar);

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x060086B7 RID: 34487 RVA: 0x001C464D File Offset: 0x001C284D
		public Feature<ProgramNode> LabelFeature { get; } = new LabelFeature(Language.Grammar);

		// Token: 0x060086B8 RID: 34488 RVA: 0x001C4658 File Offset: 0x001C2858
		private static DisjunctionsLimit<string, bool> DecideDisjunctionsLimit(GroupedExamplesSpec spec, ClusteringMethod<string, bool> clusteringMethod)
		{
			uint num = Convert.ToUInt32(spec.ExamplesWithCounts.Sum((KeyValuePair<State, KeyValuePair<object, uint>> examples) => (long)((ulong)examples.Value.Value)));
			ClusteringAlgorithm? algorithm = clusteringMethod.Algorithm;
			if (algorithm != null)
			{
				switch (algorithm.GetValueOrDefault())
				{
				case ClusteringAlgorithm.Sampling:
					return new DisjunctionsLimit<string, bool>(new uint?(1U), new uint?((num == 0U) ? 1U : num));
				case ClusteringAlgorithm.AHC:
				{
					long num2 = (long)spec.ExamplesWithCounts.Count;
					uint? num3 = DisjunctionsLimit<string, bool>.DefaultQuantitativeLimit.MaxDisjuncts;
					long? num4 = ((num3 != null) ? new long?((long)((ulong)num3.GetValueOrDefault())) : null);
					if (!((num2 <= num4.GetValueOrDefault()) & (num4 != null)))
					{
						long num5 = (long)spec.ExamplesWithCounts.Count;
						num3 = DisjunctionsLimit<string, bool>.DefaultCategoricalLimit.MaxDisjuncts;
						num4 = ((num3 != null) ? new long?((long)((ulong)num3.GetValueOrDefault())) : null);
						if (!((num5 > num4.GetValueOrDefault()) & (num4 != null)) && (ulong)num >= (ulong)((long)(2 * spec.ExamplesWithCounts.Count)))
						{
							return DisjunctionsLimit<string, bool>.DefaultCategoricalLimit;
						}
					}
					return DisjunctionsLimit<string, bool>.DefaultQuantitativeLimit;
				}
				case ClusteringAlgorithm.None:
					return new DisjunctionsLimit<string, bool>(new uint?(1U), new uint?(1U));
				}
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown clustering algorithm: {0}", new object[] { clusteringMethod.Algorithm })));
		}

		// Token: 0x060086B9 RID: 34489 RVA: 0x001C47D8 File Offset: 0x001C29D8
		private ProgramSet LearnFromSpec(GroupedExamplesSpec spec, HashSet<IToken> allowedTokens, Witnesses.Options options, out IReadOnlyList<Cluster<match>> clusters, out ClusteringResult<match> clusteringResult, int? k, CancellationToken cancel = default(CancellationToken))
		{
			Grammar grammar = Language.Grammar;
			Witnesses witnesses = new Witnesses(grammar, this.ScoreFeature, options, allowedTokens);
			LogListener logListenerIfEnabled = options.GetLogListenerIfEnabled(null);
			SynthesisEngine synthesisEngine = new SynthesisEngine(grammar, new SynthesisEngine.Config
			{
				Strategies = new ISynthesisStrategy[]
				{
					new DeductiveSynthesis(witnesses, null)
				},
				CacheSize = int.MaxValue,
				UseThreads = false,
				LogListener = logListenerIfEnabled,
				UseDynamicSoundnessCheck = options.UseDynamicSoundnessCheck
			}, null);
			LearningTask learningTask = ((k != null) ? new LearningTask(grammar.StartSymbol, spec, k.Value, this.ScoreFeature, null) : new LearningTask(grammar.StartSymbol, spec));
			ProgramSet programSet = synthesisEngine.Learn(learningTask, cancel);
			clusters = witnesses.MostRecentClustering;
			clusteringResult = witnesses.MostRecentClusteringResult;
			options.SaveLogToXMLIfEnabled(logListenerIfEnabled, null);
			if (k == null)
			{
				return programSet;
			}
			return ProgramSet.List(programSet.Symbol, programSet.TopK(this.ScoreFeature, k.Value, learningTask.FeatureCalculationContext, null));
		}

		// Token: 0x060086BA RID: 34490 RVA: 0x001C48E4 File Offset: 0x001C2AE4
		private IEnumerable<KeyValuePair<State, KeyValuePair<object, uint>>> GroupInputs(IEnumerable<string> inputs)
		{
			return (from s in inputs
				group s by s).Select(delegate(IGrouping<string, string> g)
			{
				State state = State.CreateForLearning(Language.Grammar.InputSymbol, new SuffixRegion(g.Key, 0U));
				KeyValuePair<object, uint> keyValuePair = new KeyValuePair<object, uint>(true, (uint)g.Count<string>());
				return new KeyValuePair<State, KeyValuePair<object, uint>>(state, keyValuePair);
			});
		}

		// Token: 0x060086BB RID: 34491 RVA: 0x001C493C File Offset: 0x001C2B3C
		private Witnesses.Options GetOptions(IReadOnlyList<Constraint<string, bool>> constraints, GroupedExamplesSpec spec)
		{
			ClusteringMethod<string, bool> clusteringMethod = constraints.OfType<ClusteringMethod<string, bool>>().Single<ClusteringMethod<string, bool>>();
			Witnesses.Options options = new Witnesses.Options(null, null);
			Learner.DecideDisjunctionsLimit(spec, clusteringMethod).SetOptions(options);
			constraints.OfType<IOptionConstraint<Witnesses.Options>>().ForEach(delegate(IOptionConstraint<Witnesses.Options> c)
			{
				c.SetOptions(options);
			});
			return options;
		}

		// Token: 0x060086BC RID: 34492 RVA: 0x001C49A8 File Offset: 0x001C2BA8
		private HashSet<IToken> GetTokens(IReadOnlyList<Constraint<string, bool>> constraints)
		{
			List<AllowedTokens<string, bool>> list = constraints.OfType<AllowedTokens<string, bool>>().ToList<AllowedTokens<string, bool>>();
			if (list.Any<AllowedTokens<string, bool>>())
			{
				return list.SelectMany((AllowedTokens<string, bool> constraint) => constraint.Tokens).ConvertToHashSet<IToken>();
			}
			return DefaultTokens.AllTokens.Cast<IToken>().ConvertToHashSet<IToken>();
		}

		// Token: 0x060086BD RID: 34493 RVA: 0x001C4A04 File Offset: 0x001C2C04
		private Record<GroupedExamplesSpec, Witnesses.Options, HashSet<IToken>> Configure(IEnumerable<Constraint<string, bool>> constraints, IEnumerable<string> additionalInputs)
		{
			List<string> list = new List<string>(additionalInputs ?? Enumerable.Empty<string>());
			List<Constraint<string, bool>> list2 = new List<Constraint<string, bool>>();
			foreach (Constraint<string, bool> constraint in constraints)
			{
				Example<string, bool> example = constraint as Example<string, bool>;
				if (example != null)
				{
					if (!example.Output)
					{
						throw new NotImplementedException("Negative examples are not handled yet.");
					}
					if (!example.IsSoft)
					{
						throw new NotImplementedException("Hard examples (not allowed to be outliers) are not handled yet.");
					}
					list.Add(example.Input);
				}
				else
				{
					list2.Add(constraint);
				}
			}
			return this.Configure(list, list2);
		}

		// Token: 0x060086BE RID: 34494 RVA: 0x001C4AB0 File Offset: 0x001C2CB0
		private Record<GroupedExamplesSpec, Witnesses.Options, HashSet<IToken>> Configure(IEnumerable<KeyValuePair<State, KeyValuePair<object, uint>>> examplesWithCounts, IReadOnlyList<Constraint<string, bool>> constraints)
		{
			GroupedExamplesSpec groupedExamplesSpec = new GroupedExamplesSpec(examplesWithCounts);
			Witnesses.Options options = this.GetOptions(constraints, groupedExamplesSpec);
			HashSet<IToken> tokens = this.GetTokens(constraints);
			return new Record<GroupedExamplesSpec, Witnesses.Options, HashSet<IToken>>(groupedExamplesSpec, options, tokens);
		}

		// Token: 0x060086BF RID: 34495 RVA: 0x001C4ADD File Offset: 0x001C2CDD
		private Record<GroupedExamplesSpec, Witnesses.Options, HashSet<IToken>> Configure(IEnumerable<string> inputs, IReadOnlyList<Constraint<string, bool>> otherConstraints)
		{
			return this.Configure(this.GroupInputs(inputs), otherConstraints);
		}

		// Token: 0x060086C0 RID: 34496 RVA: 0x001C4AF0 File Offset: 0x001C2CF0
		private ProgramSet LearnImpl(IEnumerable<Constraint<string, bool>> constraints, IEnumerable<string> additionalInputs, out IReadOnlyList<Cluster<match>> clusters, int? k, CancellationToken cancel = default(CancellationToken))
		{
			ClusteringResult<match> clusteringResult;
			return this.LearnImpl(constraints, additionalInputs, out clusters, out clusteringResult, k, cancel);
		}

		// Token: 0x060086C1 RID: 34497 RVA: 0x001C4B0C File Offset: 0x001C2D0C
		private ProgramSet LearnImpl(IEnumerable<Constraint<string, bool>> constraints, IEnumerable<string> additionalInputs, out IReadOnlyList<Cluster<match>> clusters, out ClusteringResult<match> clusteringResult, int? k, CancellationToken cancel = default(CancellationToken))
		{
			Record<GroupedExamplesSpec, Witnesses.Options, HashSet<IToken>> record = this.Configure((constraints as IReadOnlyList<Constraint<string, bool>>) ?? constraints.ToList<Constraint<string, bool>>(), additionalInputs);
			GroupedExamplesSpec item = record.Item1;
			Witnesses.Options item2 = record.Item2;
			HashSet<IToken> item3 = record.Item3;
			return this.LearnFromSpec(item, item3, item2, out clusters, out clusteringResult, k, cancel);
		}

		// Token: 0x060086C2 RID: 34498 RVA: 0x001C4B54 File Offset: 0x001C2D54
		protected override ProgramCollection<Program, string, bool, TFeatureValue> LearnTopKUnchecked<TFeatureValue>(IEnumerable<Constraint<string, bool>> constraints, Feature<TFeatureValue> feature, int k, int? numRandomProgramsToInclude, ProgramSamplingStrategy samplingStrategy, IEnumerable<string> additionalInputs, CancellationToken cancel = default(CancellationToken), Guid? guid = null)
		{
			ClusteringResult<match> clusteringResult;
			return new ProgramCollection<Program, string, bool, TFeatureValue>(this.LearnTopK(constraints, k, out clusteringResult, additionalInputs, cancel), null, null, null);
		}

		// Token: 0x060086C3 RID: 34499 RVA: 0x001C4B78 File Offset: 0x001C2D78
		public IEnumerable<Program> LearnTopK(IEnumerable<Constraint<string, bool>> constraints, int k, out ClusteringResult<match> clusteringResult, IEnumerable<string> additionalInputs, CancellationToken cancel = default(CancellationToken))
		{
			if (k != 1)
			{
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("LearnTopK implemented only for k = 1. Input k is {0}.", new object[] { k })));
			}
			IReadOnlyList<Cluster<match>> readOnlyList;
			ProgramNode programNode = this.LearnImpl(constraints, additionalInputs, out readOnlyList, out clusteringResult, new int?(k), cancel).RealizedPrograms.FirstOrDefault<ProgramNode>();
			if (programNode == null)
			{
				return Enumerable.Empty<Program>();
			}
			IReadOnlyDictionary<ProgramNode, IReadOnlyList<string>> readOnlyDictionary = readOnlyList.ToDictionary((Cluster<match> cluster) => cluster.BestProgramNode.Node, (Cluster<match> cluster) => cluster.Data.Select((State state) => ((SuffixRegion)state[Learner.InputSymbol]).Value).ToList<string>());
			IReadOnlyDictionary<ProgramNode, uint> readOnlyDictionary2 = readOnlyList.ToDictionary((Cluster<match> cluster) => cluster.BestProgramNode.Node, (Cluster<match> cluster) => cluster.DataCount);
			ClusteringResult<match> outliers = clusteringResult.Outliers;
			IReadOnlyList<string> readOnlyList2;
			if (outliers == null)
			{
				readOnlyList2 = null;
			}
			else
			{
				IEnumerable<State> enumerable = outliers.SamplesInOrder();
				if (enumerable == null)
				{
					readOnlyList2 = null;
				}
				else
				{
					IEnumerable<string> enumerable2 = enumerable.Select((State state) => ((SuffixRegion)state[Learner.InputSymbol]).Value);
					readOnlyList2 = ((enumerable2 != null) ? enumerable2.ToList<string>() : null);
				}
			}
			IReadOnlyList<string> readOnlyList3 = readOnlyList2;
			return new Program(programNode, readOnlyDictionary, readOnlyDictionary2, readOnlyList3, (uint)additionalInputs.Count<string>()).Yield<Program>();
		}

		// Token: 0x060086C4 RID: 34500 RVA: 0x001C4CC8 File Offset: 0x001C2EC8
		public override ProgramSet LearnAll(IEnumerable<Constraint<string, bool>> constraints, IEnumerable<string> additionalInputs = null, CancellationToken cancel = default(CancellationToken))
		{
			IReadOnlyList<Cluster<match>> readOnlyList;
			return this.LearnImpl(constraints, additionalInputs, out readOnlyList, null, cancel);
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x001C4CE9 File Offset: 0x001C2EE9
		private static string ExtractInput(State s)
		{
			return ((SuffixRegion)s[Learner.InputSymbol]).Value;
		}

		// Token: 0x060086C6 RID: 34502 RVA: 0x001C4D00 File Offset: 0x001C2F00
		private PatternInfo PatternFromCluster(Cluster<match> c, int inputCount, MatchingLabel label, RegexProfile regexProfile)
		{
			double num = c.DataCount / (double)inputCount;
			switch (label.Match)
			{
			case MatchingLabel.MatchType.NullMatch:
				return PatternInfo.IsNullPatternInfo(num);
			case MatchingLabel.MatchType.NoMatch:
				throw new InvalidOperationException();
			case MatchingLabel.MatchType.TokenSequenceMatch:
			{
				string text = label.Description();
				string regex = regexProfile.Regex;
				IReadOnlyList<string> regexesToExclude = regexProfile.RegexesToExclude;
				double num2 = num;
				IEnumerable<State> data = c.Data;
				Func<State, string> func;
				if ((func = Learner.<>O.<0>__ExtractInput) == null)
				{
					func = (Learner.<>O.<0>__ExtractInput = new Func<State, string>(Learner.ExtractInput));
				}
				return PatternInfo.TokenSequencePattern(text, regex, regexesToExclude, num2, data.Select(func).ToList<string>(), label.GetTokens());
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x001C4D94 File Offset: 0x001C2F94
		public IReadOnlyList<PatternInfo> LearnPatterns(IEnumerable<string> inputs, IReadOnlyList<Constraint<string, bool>> constraints, CancellationToken cancel = default(CancellationToken))
		{
			Record<GroupedExamplesSpec, Witnesses.Options, HashSet<IToken>> record = this.Configure(inputs, constraints);
			GroupedExamplesSpec item = record.Item1;
			Witnesses.Options item2 = record.Item2;
			HashSet<IToken> item3 = record.Item3;
			IReadOnlyList<Cluster<match>> readOnlyList;
			ClusteringResult<match> clusteringResult;
			if (this.LearnFromSpec(item, item3, item2, out readOnlyList, out clusteringResult, null, cancel).RealizedPrograms.Select((ProgramNode p) => new Program(p)).FirstOrDefault<Program>() == null)
			{
				return null;
			}
			if (clusteringResult != null)
			{
				readOnlyList = clusteringResult.InOrder().ToList<Cluster<match>>();
			}
			int inputCount = (int)item.ExamplesWithCounts.Sum((KeyValuePair<State, KeyValuePair<object, uint>> kvp) => (long)((ulong)kvp.Value.Value));
			Dictionary<Cluster<match>, MatchingLabel> labels = readOnlyList.ToDictionary((Cluster<match> c) => c, (Cluster<match> c) => c.BestProgramNode.Node.AcceptVisitor<MatchingLabel>(new MatchingLabelCollector(Language.Build)));
			IReadOnlyList<IReadOnlyList<IToken>> readOnlyList2 = (from label in labels.Values
				where label.Match == MatchingLabel.MatchType.TokenSequenceMatch
				select label.GetTokens()).ToList<IReadOnlyList<IToken>>();
			IReadOnlyDictionary<IReadOnlyList<IToken>, RegexProfile> regexProfiles = readOnlyList2.RegexDescriptions();
			return readOnlyList.Select(delegate(Cluster<match> c)
			{
				if (labels[c].Match != MatchingLabel.MatchType.TokenSequenceMatch)
				{
					return this.PatternFromCluster(c, inputCount, labels[c], default(RegexProfile));
				}
				return this.PatternFromCluster(c, inputCount, labels[c], regexProfiles[labels[c].GetTokens()]);
			}).ToList<PatternInfo>();
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x001C4F27 File Offset: 0x001C3127
		public override Constraint<string, bool> BuildNegativeConstraint(string input, bool output, bool isSoft)
		{
			throw new NotImplementedException("Negative examples are unsupported.");
		}

		// Token: 0x060086C9 RID: 34505 RVA: 0x001C4F33 File Offset: 0x001C3133
		public override Constraint<string, bool> BuildPositiveConstraint(string input, bool output, bool isSoft)
		{
			if (!isSoft)
			{
				throw new Exception("Non-soft example are unsupported.");
			}
			return base.BuildPositiveConstraint(input, output, isSoft);
		}

		// Token: 0x040037A0 RID: 14240
		private static readonly Symbol InputSymbol = Language.Grammar.InputSymbol;

		// Token: 0x040037A1 RID: 14241
		public const int MaxExamples = 5;

		// Token: 0x040037A2 RID: 14242
		public const int SampleFactor = 20;

		// Token: 0x020011AD RID: 4525
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040037A3 RID: 14243
			public static Func<State, string> <0>__ExtractInput;
		}
	}
}
