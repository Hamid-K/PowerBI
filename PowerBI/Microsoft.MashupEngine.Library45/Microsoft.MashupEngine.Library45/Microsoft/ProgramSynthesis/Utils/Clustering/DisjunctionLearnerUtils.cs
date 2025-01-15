using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000618 RID: 1560
	internal static class DisjunctionLearnerUtils
	{
		// Token: 0x060021DC RID: 8668 RVA: 0x0006072D File Offset: 0x0005E92D
		internal static SamplingStrategy PickSamplingStrategy(this SamplingMetaStrategy metaStrategy, Random rng)
		{
			return DisjunctionLearnerUtils.MetaStrategyDistributions.StategyDistributions[metaStrategy].Sample(rng);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00060740 File Offset: 0x0005E940
		internal static Optional<int> PickBruteForceLimit(IEnumerable<int> subPoolSizes, IEnumerable<int> disjunctionSizes, int casesBound)
		{
			return (from n in subPoolSizes.Reverse<int>()
				where base.<PickBruteForceLimit>g__Estimate|0(n) <= (double)casesBound
				select n).MaybeFirst<int>();
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0006077D File Offset: 0x0005E97D
		internal static bool IsSingleton<TNode>(this Cluster<TNode> cluster) where TNode : IProgramNodeBuilder
		{
			return cluster.AllMatchingData.Count == 1;
		}

		// Token: 0x02000619 RID: 1561
		private struct MetaStrategyDistributions
		{
			// Token: 0x060021DF RID: 8671 RVA: 0x00060790 File Offset: 0x0005E990
			// Note: this type is marked as 'beforefieldinit'.
			static MetaStrategyDistributions()
			{
				Dictionary<SamplingMetaStrategy, Distribution> dictionary = new Dictionary<SamplingMetaStrategy, Distribution>();
				dictionary[SamplingMetaStrategy.Random] = DisjunctionLearnerUtils.MetaStrategyDistributions.RandomDistribution;
				dictionary[SamplingMetaStrategy.MoreCoverage] = DisjunctionLearnerUtils.MetaStrategyDistributions.MoreCoverageDistribution;
				dictionary[SamplingMetaStrategy.MoreClusters] = DisjunctionLearnerUtils.MetaStrategyDistributions.MoreClustersDistribution;
				dictionary[SamplingMetaStrategy.FewerClusters] = DisjunctionLearnerUtils.MetaStrategyDistributions.FewerClustersDistribution;
				DisjunctionLearnerUtils.MetaStrategyDistributions.StategyDistributions = dictionary;
			}

			// Token: 0x04001031 RID: 4145
			private static readonly Distribution RandomDistribution = new Distribution(new Record<SamplingStrategy, double>[] { Record.Create<SamplingStrategy, double>(SamplingStrategy.Random, 1.0) });

			// Token: 0x04001032 RID: 4146
			private static readonly Distribution MoreCoverageDistribution = new Distribution(new Record<SamplingStrategy, double>[]
			{
				Record.Create<SamplingStrategy, double>(SamplingStrategy.GrowCluster, 0.2),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.GrowCoveringCluster, 0.2),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.ClusterFromUncovered, 0.5),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.Random, 0.1)
			});

			// Token: 0x04001033 RID: 4147
			private static readonly Distribution MoreClustersDistribution = new Distribution(new Record<SamplingStrategy, double>[]
			{
				Record.Create<SamplingStrategy, double>(SamplingStrategy.SplitClusters, 0.2),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.SplitCoveringClusters, 0.7),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.Random, 0.1)
			});

			// Token: 0x04001034 RID: 4148
			private static readonly Distribution FewerClustersDistribution = new Distribution(new Record<SamplingStrategy, double>[]
			{
				Record.Create<SamplingStrategy, double>(SamplingStrategy.MergeClusters, 0.2),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.MergeCoveringClusters, 0.7),
				Record.Create<SamplingStrategy, double>(SamplingStrategy.Random, 0.1)
			});

			// Token: 0x04001035 RID: 4149
			internal static readonly IReadOnlyDictionary<SamplingMetaStrategy, Distribution> StategyDistributions;
		}
	}
}
