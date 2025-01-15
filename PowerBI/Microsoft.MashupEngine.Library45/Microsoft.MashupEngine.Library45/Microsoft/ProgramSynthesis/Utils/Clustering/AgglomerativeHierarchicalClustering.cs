using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x020005F7 RID: 1527
	public class AgglomerativeHierarchicalClustering<TData> where TData : IEquatable<TData>
	{
		// Token: 0x0600213E RID: 8510 RVA: 0x0005E4B0 File Offset: 0x0005C6B0
		public AgglomerativeHierarchicalClustering(ProblemSpace<TData> problemSpace, LinkageCriterion<TData>.Type linkageCriterion, uint? estimatedMaxClusterCount = null, double? thetaFactor = null)
		{
			this.ProblemSpace = problemSpace;
			this.LinkageCriterion = linkageCriterion;
			this.EstimatedMaxClusterCount = estimatedMaxClusterCount ?? Convert.ToUInt32(problemSpace.PointsWithCounts.Count);
			this.ThetaFactor = thetaFactor ?? 1.25;
			this._linkageCosts = new Dictionary<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>, double>();
			this._hierarchy = new Lazy<Dendrogram<TData>>(new Func<Dendrogram<TData>>(this.CreateHierarchy));
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0005E541 File Offset: 0x0005C741
		public double ThetaFactor { get; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x0005E549 File Offset: 0x0005C749
		public uint EstimatedMaxClusterCount { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0005E551 File Offset: 0x0005C751
		public LinkageCriterion<TData>.Type LinkageCriterion { get; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0005E559 File Offset: 0x0005C759
		public ProblemSpace<TData> ProblemSpace { get; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0005E561 File Offset: 0x0005C761
		public Dendrogram<TData> Hierarchy
		{
			get
			{
				return this._hierarchy.Value;
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0005E570 File Offset: 0x0005C770
		private Dendrogram<TData> CreateHierarchy()
		{
			SortedDictionary<double, List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>>> sortedDictionary = new SortedDictionary<double, List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>>>();
			IReadOnlyList<Dendrogram<TData>> readOnlyList = this.ProblemSpace.PointClusters.Values.ToList<Dendrogram<TData>>();
			double num = this.ThetaFactor * this.EstimatedMaxClusterCount;
			if (num < (double)readOnlyList.Count)
			{
				List<List<double>> list3 = new List<List<double>>();
				int num2 = 0;
				while ((double)list3.Count < num)
				{
					List<double> list2 = new List<double>();
					for (int n = 0; n < readOnlyList.Count; n++)
					{
						if (n == num2)
						{
							list2.Add(0.0);
						}
						else
						{
							EquatablePair<Dendrogram<TData>, Dendrogram<TData>> equatablePair = DendrogramPairUtils.CreateSorted<TData>(readOnlyList[n], readOnlyList[num2]);
							double num3;
							if (this._linkageCosts.TryGetValue(equatablePair, out num3))
							{
								list2.Add(num3);
							}
							else
							{
								num3 = this.ProblemSpace.ExactDistanceBetween(equatablePair.Item1.Data.Single<TData>(), equatablePair.Item2.Data.Single<TData>(), true);
								this._linkageCosts[equatablePair] = num3;
								sortedDictionary.GetOrCreateValue(num3).Add(equatablePair);
								list2.Add(num3);
							}
						}
					}
					list3.Add(list2);
					double num4 = 0.0;
					int i2;
					int i;
					for (i = readOnlyList.Count - 1; i >= 0; i = i2 - 1)
					{
						double num5 = list3.Select((List<double> list) => list[i]).Min();
						if (num5 > num4)
						{
							num2 = i;
							num4 = num5;
						}
						i2 = i;
					}
				}
				for (int j = 1; j < readOnlyList.Count; j++)
				{
					for (int k = 0; k < j; k++)
					{
						EquatablePair<Dendrogram<TData>, Dendrogram<TData>> equatablePair2 = EquatablePair.Create<Dendrogram<TData>, Dendrogram<TData>>(readOnlyList[j], readOnlyList[k]);
						if (!this._linkageCosts.ContainsKey(equatablePair2))
						{
							double num6 = this.ProblemSpace.MaxDistanceBetween(equatablePair2.Item1.Data.Single<TData>(), equatablePair2.Item2.Data.Single<TData>());
							this._linkageCosts[equatablePair2] = num6;
							sortedDictionary.GetOrCreateValue(num6).Add(equatablePair2);
						}
					}
				}
			}
			else
			{
				for (int l = 1; l < readOnlyList.Count; l++)
				{
					for (int m = 0; m < l; m++)
					{
						EquatablePair<Dendrogram<TData>, Dendrogram<TData>> equatablePair3 = EquatablePair.Create<Dendrogram<TData>, Dendrogram<TData>>(readOnlyList[l], readOnlyList[m]);
						double num7 = this.ProblemSpace.ExactDistanceBetween(equatablePair3.Item1.Data.Single<TData>(), equatablePair3.Item2.Data.Single<TData>(), true);
						this._linkageCosts[equatablePair3] = num7;
						sortedDictionary.GetOrCreateValue(num7).Add(equatablePair3);
					}
				}
			}
			return this.MergeClusters(sortedDictionary);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0005E848 File Offset: 0x0005CA48
		private Dendrogram<TData> MergeClusters(SortedDictionary<double, List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>>> sortedLinkageCosts)
		{
			SortedSet<Dendrogram<TData>> sortedSet = new SortedSet<Dendrogram<TData>>(this.ProblemSpace.PointClusters.Values);
			while (sortedSet.Count > 1)
			{
				double key = sortedLinkageCosts.First<KeyValuePair<double, List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>>>>().Key;
				EquatablePair<Dendrogram<TData>, Dendrogram<TData>> bestLinkage = sortedLinkageCosts.First<KeyValuePair<double, List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>>>>().Value.First<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>>();
				Dendrogram<TData> dendrogram = new Dendrogram<TData>(bestLinkage.Item1, bestLinkage.Item2, key);
				sortedSet.Remove(bestLinkage.Item1);
				sortedSet.Remove(bestLinkage.Item2);
				List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> list;
				Predicate<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> <>9__0;
				Predicate<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> <>9__1;
				foreach (Dendrogram<TData> dendrogram2 in sortedSet)
				{
					double num = this.CostOf(bestLinkage.Item1, dendrogram2);
					if (sortedLinkageCosts.TryGetValue(num, out list))
					{
						List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> list2 = list;
						Predicate<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = (EquatablePair<Dendrogram<TData>, Dendrogram<TData>> p) => p.ContainsId(bestLinkage.Item1.Id));
						}
						list2.RemoveAll(predicate);
						if (list.Count < 1)
						{
							sortedLinkageCosts.Remove(num);
						}
					}
					num = this.CostOf(bestLinkage.Item2, dendrogram2);
					if (sortedLinkageCosts.TryGetValue(num, out list))
					{
						List<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> list3 = list;
						Predicate<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>> predicate2;
						if ((predicate2 = <>9__1) == null)
						{
							predicate2 = (<>9__1 = (EquatablePair<Dendrogram<TData>, Dendrogram<TData>> p) => p.ContainsId(bestLinkage.Item2.Id));
						}
						list3.RemoveAll(predicate2);
						if (list.Count < 1)
						{
							sortedLinkageCosts.Remove(num);
						}
					}
					num = this.CostOf(dendrogram, dendrogram2);
					sortedLinkageCosts.GetOrCreateValue(num).Add(EquatablePair.Create<Dendrogram<TData>, Dendrogram<TData>>(dendrogram, dendrogram2));
				}
				if (sortedLinkageCosts.TryGetValue(key, out list))
				{
					list.Remove(bestLinkage);
					if (list.Count < 1)
					{
						sortedLinkageCosts.Remove(key);
					}
				}
				sortedSet.Add(dendrogram);
			}
			return sortedSet.Single<Dendrogram<TData>>();
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0005EA44 File Offset: 0x0005CC44
		private bool ShouldSplit(Dendrogram<TData> dendrogram, uint level)
		{
			return dendrogram.Cost != this.ProblemSpace.SingletonCost && (dendrogram.Cost - Math.Max(dendrogram.LeftChild.Cost, dendrogram.RightChild.Cost)) / this.Hierarchy.Cost > 1.0 / (level + 1U);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0005EAA8 File Offset: 0x0005CCA8
		public SortedSet<Dendrogram<TData>> MostLikelyClusters(uint minClusters, uint maxClusters)
		{
			maxClusters = Math.Min(maxClusters, this.EstimatedMaxClusterCount);
			maxClusters = Math.Max(maxClusters, 1U);
			minClusters = Math.Max(minClusters, 1U);
			if (maxClusters < minClusters)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Invalid range of clusters requested. Adjusted Min = {0}, Max = {1}", new object[] { minClusters, maxClusters })));
			}
			SortedSet<Dendrogram<TData>> sortedSet = this.SplitIntoKClusters(minClusters);
			if (maxClusters == minClusters || sortedSet.Min.Cost == this.ProblemSpace.SingletonCost)
			{
				return sortedSet;
			}
			uint num = (this.ShouldSplit(sortedSet.Min, minClusters) ? (minClusters + 1U) : minClusters);
			for (uint num2 = minClusters + 1U; num2 < maxClusters; num2 += 1U)
			{
				AgglomerativeHierarchicalClustering<TData>.SplitNextClusterIn(sortedSet);
				if (this.ShouldSplit(sortedSet.Min, num2))
				{
					num = num2 + 1U;
				}
			}
			return this.SplitIntoKClusters(num);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0005EB70 File Offset: 0x0005CD70
		private double CostOf(Dendrogram<TData> clusterA, Dendrogram<TData> clusterB)
		{
			if (clusterA.Id == clusterB.Id)
			{
				return 0.0;
			}
			EquatablePair<Dendrogram<TData>, Dendrogram<TData>> equatablePair = DendrogramPairUtils.CreateSorted<TData>(clusterA, clusterB);
			return this._linkageCosts.GetOrAdd(equatablePair, (EquatablePair<Dendrogram<TData>, Dendrogram<TData>> p) => this.LinkageCriterion(p.Item1, p.Item2, new Func<Dendrogram<TData>, Dendrogram<TData>, double>(this.CostOf)));
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0005EBB8 File Offset: 0x0005CDB8
		public SortedSet<Dendrogram<TData>> SplitIntoKClusters(uint k)
		{
			if (k < 1U || k > this.EstimatedMaxClusterCount)
			{
				throw new ArgumentException("Invalid number of clusters requested.");
			}
			SortedSet<Dendrogram<TData>> sortedSet = new SortedSet<Dendrogram<TData>> { this.Hierarchy };
			int num = 1;
			while ((long)num < (long)((ulong)k))
			{
				AgglomerativeHierarchicalClustering<TData>.SplitNextClusterIn(sortedSet);
				num++;
			}
			return sortedSet;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0005EC08 File Offset: 0x0005CE08
		private static void SplitNextClusterIn(SortedSet<Dendrogram<TData>> dendrograms)
		{
			Dendrogram<TData> min = dendrograms.Min;
			dendrograms.Remove(min);
			dendrograms.Add(min.LeftChild);
			dendrograms.Add(min.RightChild);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0005EC40 File Offset: 0x0005CE40
		public SortedSet<Dendrogram<TData>> SplitByCostThreshold(double threshold)
		{
			if (threshold < 0.0)
			{
				throw new ArgumentException("Threshold can not be negative.");
			}
			SortedSet<Dendrogram<TData>> sortedSet = new SortedSet<Dendrogram<TData>> { this.Hierarchy };
			while (sortedSet.Min.Cost > threshold)
			{
				AgglomerativeHierarchicalClustering<TData>.SplitNextClusterIn(sortedSet);
			}
			return sortedSet;
		}

		// Token: 0x04000FC1 RID: 4033
		private readonly Lazy<Dendrogram<TData>> _hierarchy;

		// Token: 0x04000FC2 RID: 4034
		private readonly Dictionary<EquatablePair<Dendrogram<TData>, Dendrogram<TData>>, double> _linkageCosts;

		// Token: 0x04000FC3 RID: 4035
		private const double DefaultThetaFactor = 1.25;
	}
}
