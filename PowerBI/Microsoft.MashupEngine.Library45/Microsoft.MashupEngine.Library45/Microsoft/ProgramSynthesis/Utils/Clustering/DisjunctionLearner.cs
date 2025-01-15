using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x0200060B RID: 1547
	public class DisjunctionLearner<TNode> where TNode : struct, IProgramNodeBuilder
	{
		// Token: 0x060021A6 RID: 8614 RVA: 0x0005F838 File Offset: 0x0005DA38
		public DisjunctionLearner(IReadOnlyDictionary<State, uint> examplesWithCounts, PredicateLearner<TNode> predicateLearner, ScoreUtils<TNode> scoreUtils, int randomSeed = 61455)
		{
			this._predicateLearner = predicateLearner;
			this._rng = new Random(randomSeed);
			this._scoreUtils = scoreUtils;
			this._examples = examplesWithCounts.Keys.ToList<State>();
			this._exampleCounts = examplesWithCounts.Values.ToList<uint>();
			this._totalExamples = this._exampleCounts.Sum((uint c) => (int)c);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0005F8B8 File Offset: 0x0005DAB8
		internal void AddInitialClusters(ClusterPool<TNode> clusterPool, CancellationToken cancel)
		{
			if ((double)this._totalExamples < 100.0)
			{
				return;
			}
			double limit = (double)this._totalExamples * 0.05;
			foreach (State state in this._examples.Where((State ex, int i) => this._exampleCounts[i] >= limit))
			{
				this.AddClusterFromSamples(clusterPool, state.Yield<State>(), cancel);
			}
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0005F958 File Offset: 0x0005DB58
		internal void InitialSetup()
		{
			if ((double)this._totalExamples < 10.0)
			{
				this._scoreUtils.SetCardinalityExponent(3.0);
			}
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0005F980 File Offset: 0x0005DB80
		internal Optional<Cover<TNode>> TryCategoricalCover(ClusterPool<TNode> clusterPool, CancellationToken cancel)
		{
			if ((long)this._examples.Count > 10L)
			{
				return Optional<Cover<TNode>>.Nothing;
			}
			if ((long)this._totalExamples < 500L)
			{
				return Optional<Cover<TNode>>.Nothing;
			}
			IEnumerable<Cluster<TNode>> enumerable = this._examples.Select((State ex) => this.AddClusterFromSamples(clusterPool, ex.Yield<State>(), cancel));
			return new Cover<TNode>(clusterPool, enumerable.ToList<Cluster<TNode>>()).Some<Cover<TNode>>();
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0005FA04 File Offset: 0x0005DC04
		public ClusteringResult<TNode> LearnFromSampling(DisjunctionLearner<TNode>.CoverConstraints coverConstraints, ClusterConstraints clusterConstraints, CancellationToken cancel, int? maxAttempts = null, int? maxAttemptsAfterFailure = null)
		{
			int num = maxAttempts.GetValueOrDefault();
			if (maxAttempts == null)
			{
				num = 50;
				maxAttempts = new int?(num);
			}
			num = maxAttemptsAfterFailure.GetValueOrDefault();
			if (maxAttemptsAfterFailure == null)
			{
				num = 10;
				maxAttemptsAfterFailure = new int?(num);
			}
			ClusterPool<TNode> clusterPool = new ClusterPool<TNode>(this._examples, this._exampleCounts, clusterConstraints);
			this.InitialSetup();
			Optional<Cover<TNode>> optional = this.TryCategoricalCover(clusterPool, cancel);
			if (optional.HasValue && coverConstraints.Check(optional.Value))
			{
				return new ClusteringResult<TNode>(optional.Value.ToList<Cluster<TNode>>(), null);
			}
			this.AddInitialClusters(clusterPool, cancel);
			int num2 = Math.Min(12, 2 * coverConstraints.MaxDisjuncts);
			SamplingMetaStrategy samplingMetaStrategy = SamplingMetaStrategy.Random;
			CoveringStrategy coveringStrategy = CoveringStrategy.Greedy;
			Cover<TNode> cover = new Cover<TNode>(clusterPool, new List<Cluster<TNode>>());
			int num3 = 0;
			int num4 = 0;
			int? num6;
			for (;;)
			{
				int num5 = num4;
				num6 = maxAttempts;
				if (!((num5 < num6.GetValueOrDefault()) & (num6 != null)))
				{
					break;
				}
				int num7 = num3;
				num6 = maxAttemptsAfterFailure;
				if ((num7 >= num6.GetValueOrDefault()) & (num6 != null))
				{
					break;
				}
				this.LearnNewClusters(num2, samplingMetaStrategy, clusterPool, cover, cancel);
				Cover<TNode> cover2 = this.PickCover(clusterPool, coverConstraints, coveringStrategy, cancel);
				num3 = (cover2.SequenceEqual(cover) ? (num3 + 1) : 0);
				cover = cover2;
				if (coverConstraints.Check(cover))
				{
					if (coverConstraints.MaxDisjuncts <= cover.Count || !cover.HasUncoveredExamples || num3 >= 4)
					{
						break;
					}
					samplingMetaStrategy = SamplingMetaStrategy.MoreCoverage;
				}
				else
				{
					samplingMetaStrategy = coverConstraints.PickSamplingMetaStrategy(cover).OrElse(samplingMetaStrategy);
					coveringStrategy = coverConstraints.PickCoveringStrategy(cover).OrElse(coveringStrategy);
					if (num3 == 6)
					{
						coveringStrategy = CoveringStrategy.BruteForce;
					}
				}
				num4++;
			}
			int num8 = num4;
			num6 = maxAttempts;
			if ((num8 >= num6.GetValueOrDefault()) & (num6 != null))
			{
				return new ClusteringResult<TNode>(cover.ToList<Cluster<TNode>>(), null);
			}
			if (!coverConstraints.CheckOutliers(cover))
			{
				cover = this.RecoveryHighOutlierRate(clusterPool, coverConstraints, clusterConstraints, cover, cancel, maxAttempts.Value - num4 / 2);
			}
			if (!coverConstraints.CheckMaxDisjuncts(cover))
			{
				cover = this.RecoveryTooManyDisjuncts(clusterPool, coverConstraints, cover);
			}
			double num9 = Math.Min(0.5, 2.0 * (double)coverConstraints.MaxOutliers / (double)this._totalExamples);
			DisjunctionLearner<TNode>.CoverConstraints coverConstraints2 = new DisjunctionLearner<TNode>.CoverConstraints(1U, (uint)coverConstraints.MaxDisjuncts, (uint)Math.Floor(cover.NumberUncoveredExamples * num9));
			ClusteringResult<TNode> clusteringResult = this.ClusterUncoveredExamples(coverConstraints2, clusterConstraints, cover, cancel, maxAttempts.Value - num4 / 2);
			return new ClusteringResult<TNode>(cover.ToList<Cluster<TNode>>(), clusteringResult);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0005FC70 File Offset: 0x0005DE70
		private Cover<TNode> RecoveryHighOutlierRate(ClusterPool<TNode> clusterPool, DisjunctionLearner<TNode>.CoverConstraints constraints, ClusterConstraints clusterConstraints, Cover<TNode> cover, CancellationToken cancel, int recursiveMaxAttempts)
		{
			DisjunctionLearner<TNode>.CoverConstraints coverConstraints = new DisjunctionLearner<TNode>.CoverConstraints(1U, (uint)Math.Min(constraints.MaxDisjuncts * 2, 10), (uint)constraints.MaxOutliers);
			ClusteringResult<TNode> clusteringResult = this.ClusterUncoveredExamples(coverConstraints, clusterConstraints, cover, cancel, recursiveMaxAttempts);
			if (clusteringResult == null)
			{
				return cover;
			}
			foreach (Cluster<TNode> cluster in clusteringResult.InOrder())
			{
				if (cover.Count >= constraints.MaxDisjuncts)
				{
					break;
				}
				if (constraints.CheckOutliers(cover))
				{
					break;
				}
				Cluster<TNode> cluster2;
				if ((cluster2 = clusterPool.FindCluster(cluster.BestProgramNode)) == null)
				{
					this.AddClusterFromSamples(clusterPool, cluster.Data, cancel);
					cluster2 = clusterPool.FindCluster(cluster.BestProgramNode);
				}
				if (cluster2 != null)
				{
					cover = cover.TryAppend(clusterPool, cluster2) ?? cover;
				}
			}
			return cover;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0005FD5C File Offset: 0x0005DF5C
		private Cover<TNode> RecoveryTooManyDisjuncts(ClusterPool<TNode> clusterPool, DisjunctionLearner<TNode>.CoverConstraints constraints, Cover<TNode> cover)
		{
			List<Cluster<TNode>> list = cover.OrderByDescending((Cluster<TNode> c) => c.DataCount).Take(constraints.MaxDisjuncts).ToList<Cluster<TNode>>();
			if ((ulong)((uint)list.Sum((Cluster<TNode> c) => (long)((ulong)c.DataCount))) >= (ulong)((long)(this._totalExamples - constraints.MaxOutliers)))
			{
				return new Cover<TNode>(clusterPool, list);
			}
			return cover;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
		internal IEnumerable<State> Sample(SamplingStrategy strategy, ClusterPool<TNode> clusterPool, Cover<TNode> cover)
		{
			Func<Random, Cluster<TNode>> func = ((strategy == SamplingStrategy.GrowCoveringCluster || strategy == SamplingStrategy.MergeCoveringClusters || strategy == SamplingStrategy.SplitCoveringClusters) ? new Func<Random, Cluster<TNode>>(cover.Sample) : new Func<Random, Cluster<TNode>>(clusterPool.Sample));
			switch (strategy)
			{
			case SamplingStrategy.Random:
				break;
			case SamplingStrategy.ClusterFromUncovered:
				return cover.UncoveredExamples.SampleWithReplacement(this._rng, 2);
			case SamplingStrategy.GrowCluster:
			case SamplingStrategy.GrowCoveringCluster:
			{
				Cluster<TNode> cluster = func(this._rng);
				if (!(cluster == null))
				{
					return cluster.Data.Concat(cover.UncoveredExamples.SampleWithReplacement(this._rng, 1));
				}
				break;
			}
			case SamplingStrategy.MergeClusters:
			case SamplingStrategy.MergeCoveringClusters:
			{
				Cluster<TNode> cluster2 = func(this._rng);
				Cluster<TNode> cluster3 = func(this._rng);
				if (!(cluster2 == null) && !(cluster3 == null))
				{
					return cluster2.Data.Concat(cluster3.Data);
				}
				break;
			}
			case SamplingStrategy.SplitClusters:
			case SamplingStrategy.SplitCoveringClusters:
			{
				Cluster<TNode> cluster = func(this._rng);
				return (((cluster != null) ? cluster.AllMatchingData : null) ?? this._examples).RandomlySampleWithReplacement(this._rng, 2);
			}
			default:
				throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Missing case for {0}", new object[] { strategy })));
			}
			return this._examples.RandomlySampleWithReplacement(this._rng, 2);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0005FF39 File Offset: 0x0005E139
		private IEnumerable<State> Sample(SamplingMetaStrategy metaStrategy, ClusterPool<TNode> clusterPool, Cover<TNode> cover)
		{
			return this.Sample(metaStrategy.PickSamplingStrategy(this._rng), clusterPool, cover);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0005FF50 File Offset: 0x0005E150
		private void LearnNewClusters(int attemptLimit, SamplingMetaStrategy metaStrategy, ClusterPool<TNode> clusterPool, Cover<TNode> cover, CancellationToken cancel)
		{
			for (int i = 0; i < attemptLimit; i++)
			{
				IReadOnlyList<State> readOnlyList = this.Sample(metaStrategy, clusterPool, cover).ToList<State>();
				this.AddClusterFromSamples(clusterPool, readOnlyList, cancel);
			}
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0005FF84 File Offset: 0x0005E184
		private Cluster<TNode> AddClusterFromSamples(ClusterPool<TNode> clusterPool, IEnumerable<State> samples, CancellationToken cancel)
		{
			IReadOnlyList<State> readOnlyList = samples.Distinct<State>().ToList<State>();
			ClusterPredicate<TNode>? clusterPredicate = this._predicateLearner.Learn(readOnlyList, cancel);
			if (clusterPredicate == null)
			{
				return null;
			}
			ClusterPredicate<TNode> value = clusterPredicate.Value;
			return clusterPool.AddClusterFromPredicate(value, readOnlyList);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0005FFC8 File Offset: 0x0005E1C8
		private Cover<TNode> PickCover(ClusterPool<TNode> clusterPool, DisjunctionLearner<TNode>.CoverConstraints constraints, CoveringStrategy coveringStrategy, CancellationToken cancel)
		{
			if (coveringStrategy == CoveringStrategy.Greedy)
			{
				return this.CoverGreedy(clusterPool, cancel);
			}
			if (coveringStrategy == CoveringStrategy.BruteForce)
			{
				return this.CoverBruteForce(clusterPool, constraints, cancel);
			}
			throw new NotImplementedException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown covering strategy: {0}", new object[] { coveringStrategy })));
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00060014 File Offset: 0x0005E214
		private Cover<TNode> CoverGreedy(ClusterPool<TNode> clusterPool, CancellationToken cancel)
		{
			List<Cluster<TNode>> list = new List<Cluster<TNode>>();
			using (IEnumerator<Cluster<TNode>> enumerator = clusterPool.OrderByDescending(new Func<Cluster<TNode>, double>(this._scoreUtils.Preference)).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Cluster<TNode> selected = enumerator.Current;
					cancel.ThrowIfCancellationRequested();
					if (!list.IsEmpty<Cluster<TNode>>() && this._scoreUtils.SignificantlyWorseThan(selected, list.Last<Cluster<TNode>>()) && !list.Last<Cluster<TNode>>().IsSingleton<TNode>())
					{
						break;
					}
					IList<Cluster<TNode>> list2;
					IList<Cluster<TNode>> list3;
					list.PartitionByPredicate((Cluster<TNode> cc) => clusterPool.Intersects(cc, selected), out list2, out list3);
					if (list2.Any<Cluster<TNode>>())
					{
						IEnumerable<Cluster<TNode>> enumerable = list2;
						Func<Cluster<TNode>, bool> func;
						if ((func = DisjunctionLearner<TNode>.<>O.<0>__IsSingleton) == null)
						{
							func = (DisjunctionLearner<TNode>.<>O.<0>__IsSingleton = new Func<Cluster<TNode>, bool>(DisjunctionLearnerUtils.IsSingleton<TNode>));
						}
						if (enumerable.All(func))
						{
							if (list2.Sum((Cluster<TNode> c) => (long)((ulong)c.DataCount)) < (long)((ulong)selected.DataCount))
							{
								list = list3.AppendItem(selected).ToList<Cluster<TNode>>();
							}
						}
					}
					else
					{
						list.Add(selected);
					}
				}
			}
			return new Cover<TNode>(clusterPool, list);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0006017C File Offset: 0x0005E37C
		private Cover<TNode> CoverBruteForce(ClusterPool<TNode> clusterPool, DisjunctionLearner<TNode>.CoverConstraints constraints, CancellationToken cancel)
		{
			Optional<int> optional = DisjunctionLearnerUtils.PickBruteForceLimit((clusterPool.Count >= constraints.MaxDisjuncts) ? Enumerable.Range(constraints.MaxDisjuncts, clusterPool.Count - constraints.MaxDisjuncts + 1) : Enumerable.Empty<int>(), constraints.DisjunctionSizes, 2000);
			if (!optional.HasValue)
			{
				return this.CoverGreedy(clusterPool, cancel);
			}
			List<Cluster<TNode>> subPool = clusterPool.OrderByDescending(new Func<Cluster<TNode>, double>(this._scoreUtils.Preference)).Take(optional.Value).ToList<Cluster<TNode>>();
			Func<Cluster<TNode>, double> <>9__5;
			List<Cluster<TNode>> list = (from candidateCover in constraints.DisjunctionSizes.SelectMany((int size) => subPool.Choose(size))
				select candidateCover.ToList<Cluster<TNode>>()).Where(delegate(List<Cluster<TNode>> candidateCover)
			{
				if (clusterPool.ArePairwiseDisjoint(candidateCover))
				{
					return (long)this._totalExamples - candidateCover.Sum((Cluster<TNode> c) => (long)((ulong)c.DataCount)) <= (long)constraints.MaxOutliers;
				}
				return false;
			}).ArgMin(delegate(List<Cluster<TNode>> candidateCover)
			{
				Func<Cluster<TNode>, double> func;
				if ((func = <>9__5) == null)
				{
					func = (<>9__5 = (Cluster<TNode> c) => this._scoreUtils.Score(c));
				}
				return candidateCover.Sum(func);
			});
			if (list == null)
			{
				return this.CoverGreedy(clusterPool, cancel);
			}
			return new Cover<TNode>(clusterPool, list);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000602CC File Offset: 0x0005E4CC
		private ClusteringResult<TNode> ClusterUncoveredExamples(DisjunctionLearner<TNode>.CoverConstraints constraints, ClusterConstraints clusterConstraints, Cover<TNode> cover, CancellationToken cancel, int recursiveMaxAttempts)
		{
			if (!cover.HasUncoveredExamples)
			{
				return null;
			}
			return new DisjunctionLearner<TNode>(cover.UncoveredExamples.GetEnabledIndices().ToDictionary((int i) => this._examples[i], (int i) => this._exampleCounts[i]), this._predicateLearner, this._scoreUtils, 61455).LearnFromSampling(constraints, clusterConstraints, cancel, new int?(recursiveMaxAttempts), null);
		}

		// Token: 0x04001005 RID: 4101
		private readonly IReadOnlyList<State> _examples;

		// Token: 0x04001006 RID: 4102
		private readonly IReadOnlyList<uint> _exampleCounts;

		// Token: 0x04001007 RID: 4103
		private readonly PredicateLearner<TNode> _predicateLearner;

		// Token: 0x04001008 RID: 4104
		private readonly Random _rng;

		// Token: 0x04001009 RID: 4105
		private readonly int _totalExamples;

		// Token: 0x0400100A RID: 4106
		private readonly ScoreUtils<TNode> _scoreUtils;

		// Token: 0x0200060C RID: 1548
		private struct MainLoopConfiguration
		{
			// Token: 0x0400100B RID: 4107
			internal const int MaxAttemptAfterFailure = 10;

			// Token: 0x0400100C RID: 4108
			internal const int GreedyBruteForceSwitchOver = 6;

			// Token: 0x0400100D RID: 4109
			internal const int ContinueDespiteSuccess = 4;

			// Token: 0x0400100E RID: 4110
			internal const int MaxAttempts = 50;
		}

		// Token: 0x0200060D RID: 1549
		private struct ModalConfiguration
		{
			// Token: 0x0400100F RID: 4111
			internal const double LargeProblemLimit = 100.0;

			// Token: 0x04001010 RID: 4112
			internal const double SingletonLimit = 0.05;

			// Token: 0x04001011 RID: 4113
			internal const double SmallProblemLimit = 10.0;

			// Token: 0x04001012 RID: 4114
			internal const uint CategoryLimit = 10U;

			// Token: 0x04001013 RID: 4115
			internal const uint CategoricalDataSetSizeLimit = 500U;
		}

		// Token: 0x0200060E RID: 1550
		public struct CoverConstraints
		{
			// Token: 0x170005DC RID: 1500
			// (get) Token: 0x060021B7 RID: 8631 RVA: 0x00060359 File Offset: 0x0005E559
			internal readonly int MaxDisjuncts { get; }

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00060361 File Offset: 0x0005E561
			internal readonly int MinDisjuncts { get; }

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00060369 File Offset: 0x0005E569
			internal readonly int MaxOutliers { get; }

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x060021BA RID: 8634 RVA: 0x00060371 File Offset: 0x0005E571
			internal readonly IEnumerable<int> DisjunctionSizes { get; }

			// Token: 0x060021BB RID: 8635 RVA: 0x00060379 File Offset: 0x0005E579
			public CoverConstraints(uint minDisjuncts, uint maxDisjuncts, uint maxOutliers)
			{
				this.MaxDisjuncts = (int)maxDisjuncts;
				this.MinDisjuncts = (int)minDisjuncts;
				this.MaxOutliers = (int)maxOutliers;
				this.DisjunctionSizes = Enumerable.Range(this.MinDisjuncts, this.MaxDisjuncts - this.MinDisjuncts + 1).ToList<int>();
			}

			// Token: 0x060021BC RID: 8636 RVA: 0x000603B5 File Offset: 0x0005E5B5
			internal bool Check(Cover<TNode> cover)
			{
				return this.CheckMinDisjuncts(cover) && this.CheckMaxDisjuncts(cover) && this.CheckOutliers(cover);
			}

			// Token: 0x060021BD RID: 8637 RVA: 0x000603D2 File Offset: 0x0005E5D2
			internal bool CheckMinDisjuncts(Cover<TNode> cover)
			{
				return cover.Count >= this.MinDisjuncts;
			}

			// Token: 0x060021BE RID: 8638 RVA: 0x000603E5 File Offset: 0x0005E5E5
			internal bool CheckMaxDisjuncts(Cover<TNode> cover)
			{
				return cover.Count <= this.MaxDisjuncts;
			}

			// Token: 0x060021BF RID: 8639 RVA: 0x000603F8 File Offset: 0x0005E5F8
			internal bool CheckOutliers(Cover<TNode> cover)
			{
				return (ulong)cover.NumberUncoveredExamples <= (ulong)((long)this.MaxOutliers);
			}

			// Token: 0x060021C0 RID: 8640 RVA: 0x0006040D File Offset: 0x0005E60D
			internal Optional<SamplingMetaStrategy> PickSamplingMetaStrategy(Cover<TNode> cover)
			{
				if (!this.CheckOutliers(cover))
				{
					return SamplingMetaStrategy.MoreCoverage.Some<SamplingMetaStrategy>();
				}
				if (!this.CheckMinDisjuncts(cover))
				{
					return SamplingMetaStrategy.MoreClusters.Some<SamplingMetaStrategy>();
				}
				if (!this.CheckMaxDisjuncts(cover))
				{
					return SamplingMetaStrategy.FewerClusters.Some<SamplingMetaStrategy>();
				}
				return Optional<SamplingMetaStrategy>.Nothing;
			}

			// Token: 0x060021C1 RID: 8641 RVA: 0x00060444 File Offset: 0x0005E644
			internal Optional<CoveringStrategy> PickCoveringStrategy(Cover<TNode> cover)
			{
				if (this.CheckOutliers(cover) && (!this.CheckMinDisjuncts(cover) || !this.CheckMaxDisjuncts(cover)))
				{
					return CoveringStrategy.BruteForce.Some<CoveringStrategy>();
				}
				return Optional<CoveringStrategy>.Nothing;
			}
		}

		// Token: 0x0200060F RID: 1551
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001018 RID: 4120
			public static Func<Cluster<TNode>, bool> <0>__IsSingleton;
		}
	}
}
