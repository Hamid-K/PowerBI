using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.ProgramSynthesis.Conditionals.Build;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering;
using Microsoft.ProgramSynthesis.Conditionals.Learning.Specifications;
using Microsoft.ProgramSynthesis.Conditionals.Semantics;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning
{
	// Token: 0x02000A5B RID: 2651
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x060041CF RID: 16847 RVA: 0x000CDB1C File Offset: 0x000CBD1C
		public Witnesses(Grammar grammar, Witnesses.Options options)
			: base(grammar)
		{
			this._options = options;
			this._build = GrammarBuilders.Instance(grammar);
			this._alphabetToken = (this._options.EnableMatchUnicode ? Token.NonDisjunctiveTokens : Token.NonDisjunctiveTokensAscii).MaybeGet("Alphabet").OrElseDefault<Token>();
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x000CDB74 File Offset: 0x000CBD74
		[RuleLearner("Start")]
		internal Optional<ProgramSet> LearnDisjunction(SynthesisEngine engine, OperatorRule rule, LearningTask<ExampleSpec> task, CancellationToken cancel)
		{
			List<LearningCacheSubstring> list = task.Spec.Examples.Keys.Select((State state) => (LearningCacheSubstring)state[base.Grammar.InputSymbol]).ToList<LearningCacheSubstring>();
			List<LearningCacheSubstring> list2 = list.Where((LearningCacheSubstring input) => !Semantics.IsNullOrWhiteSpace(input)).ToList<LearningCacheSubstring>();
			IReadOnlyList<Token> readOnlyList = this.ExtractDynamicTokens(list2, null);
			foreach (LearningCacheSubstring learningCacheSubstring in list2)
			{
				learningCacheSubstring.Cache.AddTokens(readOnlyList);
			}
			BisectingClustering bisectingClustering = new BisectingClustering(new Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering.Cluster(list, this._options.SupportRegex), this._options.InSameCluster, this._options.InDifferentClusters);
			IReadOnlyCollection<Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering.Cluster> readOnlyCollection = ((this._options.ClusterCount != null) ? bisectingClustering.TopKClusters(this._options.ClusterCount.Value) : bisectingClustering.TopClusters());
			if (readOnlyCollection == null || readOnlyCollection.Count == 0)
			{
				return ProgramSet.Empty(base.Grammar.StartSymbol).Some<ProgramSet>();
			}
			List<conjunct> list3 = readOnlyCollection.Select(new Func<Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering.Cluster, conjunct>(this.GenerateConjunctiveDescription)).ToList<conjunct>();
			if (list3.Count == 0)
			{
				return OptionalUtils.Some((T)null);
			}
			disjunct disjunct = this._build.Node.Rule.ConvertDisjunctConjunct(list3.Last<conjunct>());
			for (int i = list3.Count - 2; i >= 0; i--)
			{
				disjunct = this._build.Node.Rule.Disjunction(list3[i], disjunct);
			}
			return ProgramSetBuilder.List<output>(new output[] { this._build.Node.Rule.Start(disjunct) }).Set.Some<ProgramSet>();
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x000CDD6C File Offset: 0x000CBF6C
		[RuleLearner("Conjunct")]
		internal Optional<ProgramSet> LearnMatches(SynthesisEngine engine, ConversionRule rule, LearningTask<BisectSpec> task, CancellationToken cancel)
		{
			List<LearningCacheSubstring> list = task.Spec.Positives.Select((State state) => (LearningCacheSubstring)state[this._build.Symbol.s]).ToList<LearningCacheSubstring>();
			List<LearningCacheSubstring> list2 = task.Spec.LikelyNegatives.Select((State state) => (LearningCacheSubstring)state[this._build.Symbol.s]).ToList<LearningCacheSubstring>();
			List<LearningCacheSubstring> list3 = (from state in task.ProvidedInputs
				select (LearningCacheSubstring)state[this._build.Symbol.s] into input
				where !Semantics.IsNullOrWhiteSpace(input)
				select input).Distinct<LearningCacheSubstring>().ToList<LearningCacheSubstring>();
			IReadOnlyList<Token> readOnlyList = this.ExtractDynamicTokens(list3, new int?(1));
			foreach (LearningCacheSubstring learningCacheSubstring in list3)
			{
				learningCacheSubstring.Cache.AddTokens(readOnlyList);
			}
			Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering.Cluster cluster = new Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering.Cluster(list, this._options.SupportRegex);
			HashSet<PredicateFeature> hashSet = list2.SelectMany((LearningCacheSubstring neg) => PredicateFeature.GetFeaturesFor(neg, this._options.SupportRegex)).ConvertToHashSet<PredicateFeature>();
			List<PredicateFeature> list4 = new List<PredicateFeature>();
			int count = cluster.AllFeatures.Count;
			if (list2.Count > 0)
			{
				int k;
				int i;
				for (i = 0; i < count; i = k + 1)
				{
					PredicateFeature predicateFeature = cluster.AllFeatures[i];
					if (!hashSet.Contains(predicateFeature) && cluster.DataPoints.Count((DataPoint p) => p.FeatureVector[i]) == cluster.DataPoints.Count)
					{
						list4.Add(predicateFeature);
					}
					k = i;
				}
			}
			if (list4.Count == 0)
			{
				BitArray bitArray = new BitArray(count);
				bitArray.SetAll(true);
				foreach (DataPoint dataPoint in cluster.DataPoints)
				{
					bitArray.And(dataPoint.FeatureVector);
				}
				for (int j = 0; j < bitArray.Count; j++)
				{
					if (!bitArray[j])
					{
						list4.Add(cluster.AllFeatures[j]);
					}
				}
			}
			conjunct[] array = list4.Select((PredicateFeature feature) => this._build.Node.Rule.Conjunct(this._build.Node.UnnamedConversion.baseConjunct_pred(feature.GenerateProgramNode(this._build)))).ToArray<conjunct>();
			return ProgramSetBuilder.List<conjunct>(this._build.Symbol.conjunct, array).Set.Some<ProgramSet>();
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x000CDFFC File Offset: 0x000CC1FC
		private IReadOnlyList<Token> ExtractDynamicTokens(IEnumerable<LearningCacheSubstring> inputs, int? score = null)
		{
			List<string> list = new List<string>();
			using (IEnumerator<LearningCacheSubstring> enumerator = inputs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LearningCacheSubstring input = enumerator.Current;
					if (input.Cache.GetStaticTokenByName(this._alphabetToken.Name) != null)
					{
						CachedList cachedList;
						if (input.Cache.TryGetMatchPositionsFor(this._alphabetToken, out cachedList))
						{
							list.AddRange(from w in cachedList
								where w.Length >= 3U
								select w into m
								select input.Source.Substring((int)m.Position, (int)m.Length));
						}
					}
					else
					{
						foreach (PositionMatch positionMatch in this._alphabetToken.GetMatches(input.Value))
						{
							if (positionMatch.Length >= 3U)
							{
								string text = input.Value.Substring((int)positionMatch.Position, (int)positionMatch.Length);
								list.Add(text);
							}
						}
					}
				}
			}
			return (from word in list
				group word by word into @group
				select new
				{
					Word = @group.Key,
					Count = @group.Count<string>()
				} into w
				where w.Count > 1
				orderby w.Count descending
				select Token.BuildDynamic(w.Word, score ?? (100 + w.Count))).ToList<StringToken>();
		}

		// Token: 0x060041D3 RID: 16851 RVA: 0x000CE21C File Offset: 0x000CC41C
		private conjunct GenerateConjunctiveDescription(Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering.Cluster cluster)
		{
			IReadOnlyList<PredicateFeature> descriptiveFeatures = cluster.DescriptiveFeatures;
			baseConjunct baseConjunct;
			if (descriptiveFeatures.Count <= 1)
			{
				PredicateFeature predicateFeature = descriptiveFeatures.FirstOrDefault<PredicateFeature>();
				pred pred = ((predicateFeature != null) ? predicateFeature.GenerateProgramNode(this._build) : this._build.Node.UnnamedConversion.pred_match(this._build.Node.Rule.True()));
				baseConjunct = this._build.Node.UnnamedConversion.baseConjunct_pred(pred);
			}
			else
			{
				baseConjunct = this._build.Node.UnnamedConversion.baseConjunct_pred(descriptiveFeatures.Last<PredicateFeature>().GenerateProgramNode(this._build));
				for (int i = descriptiveFeatures.Count - 2; i >= 0; i--)
				{
					baseConjunct = this._build.Node.Rule.Conjunction(descriptiveFeatures[i].GenerateProgramNode(this._build), baseConjunct);
				}
			}
			return this._build.Node.Rule.Conjunct(baseConjunct);
		}

		// Token: 0x04001D97 RID: 7575
		private readonly GrammarBuilders _build;

		// Token: 0x04001D98 RID: 7576
		private readonly Witnesses.Options _options;

		// Token: 0x04001D99 RID: 7577
		private readonly Token _alphabetToken;

		// Token: 0x04001D9A RID: 7578
		private const int MinDynamicTokenLength = 3;

		// Token: 0x02000A5C RID: 2652
		public class Options : DSLOptions
		{
			// Token: 0x17000B6D RID: 2925
			// (get) Token: 0x060041DA RID: 16858 RVA: 0x000CE38D File Offset: 0x000CC58D
			// (set) Token: 0x060041DB RID: 16859 RVA: 0x000CE395 File Offset: 0x000CC595
			public int? ClusterCount { get; set; }

			// Token: 0x17000B6E RID: 2926
			// (get) Token: 0x060041DC RID: 16860 RVA: 0x000CE39E File Offset: 0x000CC59E
			// (set) Token: 0x060041DD RID: 16861 RVA: 0x000CE3A6 File Offset: 0x000CC5A6
			public IReadOnlyList<HashSet<string>> InDifferentClusters { get; set; } = new HashSet<string>[0];

			// Token: 0x17000B6F RID: 2927
			// (get) Token: 0x060041DE RID: 16862 RVA: 0x000CE3AF File Offset: 0x000CC5AF
			// (set) Token: 0x060041DF RID: 16863 RVA: 0x000CE3B7 File Offset: 0x000CC5B7
			public IReadOnlyList<HashSet<string>> InSameCluster { get; set; } = new HashSet<string>[0];

			// Token: 0x17000B70 RID: 2928
			// (get) Token: 0x060041E0 RID: 16864 RVA: 0x000CE3C0 File Offset: 0x000CC5C0
			// (set) Token: 0x060041E1 RID: 16865 RVA: 0x000CE3C8 File Offset: 0x000CC5C8
			public bool SupportRegex { get; set; } = true;

			// Token: 0x17000B71 RID: 2929
			// (get) Token: 0x060041E2 RID: 16866 RVA: 0x000CE3D1 File Offset: 0x000CC5D1
			// (set) Token: 0x060041E3 RID: 16867 RVA: 0x000CE3D9 File Offset: 0x000CC5D9
			public bool EnableMatchUnicode { get; set; } = true;
		}
	}
}
