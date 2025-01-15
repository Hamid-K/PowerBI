using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x0200000F RID: 15
	internal class GreedyMerger
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003081 File Offset: 0x00001281
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00003089 File Offset: 0x00001289
		private int MaxNumOfTopIds { get; set; }

		// Token: 0x06000044 RID: 68 RVA: 0x00003094 File Offset: 0x00001294
		internal GreedyMerger(DedupContext context, MergeClusterStrategy strategy)
		{
			this.context = context;
			this.strategy = strategy;
			this.clusterMap = new List<GreedyMerger.ClusterPrivate>(this.context.MaxValueId + 1);
			for (int i = 0; i <= this.context.MaxValueId; i++)
			{
				this.clusterMap.Add(null);
			}
			this.MaxNumOfTopIds = 10;
			this.InitializeClusterFromPositiveConstraints();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003100 File Offset: 0x00001300
		private void InitializeClusterFromPositiveConstraints()
		{
			if (this.context.PositiveCstrtDedupIdGroups != null)
			{
				foreach (List<int> list in this.context.PositiveCstrtDedupIdGroups)
				{
					GreedyMerger.ClusterPrivate clusterPrivate = new GreedyMerger.ClusterPrivate();
					clusterPrivate.Cluster = new Cluster();
					int i = 1;
					int count = list.Count;
					while (i < count)
					{
						int num = list[0];
						this.context.GetValue(num);
						int num2 = list[i];
						this.context.GetValue(num2);
						FuzzyLookupMatch fuzzyLookupMatch;
						if (num < num2)
						{
							fuzzyLookupMatch = new FuzzyLookupMatch(num, num2, 1f);
						}
						else
						{
							fuzzyLookupMatch = new FuzzyLookupMatch(num2, num, 1f);
						}
						clusterPrivate.Cluster.AddSingleColumnFuzzyLookupMatch(fuzzyLookupMatch);
						i++;
					}
					clusterPrivate.DedupIds = new List<int>(list);
					clusterPrivate.TopDedupIds = new List<int>();
					int j = 0;
					int count2 = clusterPrivate.DedupIds.Count;
					while (j < count2)
					{
						this.InsertNewId(clusterPrivate.TopDedupIds, clusterPrivate.DedupIds[j]);
						j++;
					}
					clusterPrivate.AllCount = 0;
					int k = 0;
					int count3 = clusterPrivate.DedupIds.Count;
					while (k < count3)
					{
						clusterPrivate.AllCount += this.context.GetFrequency(clusterPrivate.DedupIds[k]);
						k++;
					}
					int l = 0;
					int count4 = clusterPrivate.DedupIds.Count;
					while (l < count4)
					{
						int num3 = clusterPrivate.DedupIds[l];
						if (this.context.HasIncompatibleValueIds(num3))
						{
							if (clusterPrivate.IncompatibleDedupIds == null)
							{
								clusterPrivate.IncompatibleDedupIds = new HashSet<int>();
							}
							clusterPrivate.IncompatibleDedupIds.UnionWith(this.context.GetIncompatibleValueIds(num3));
						}
						l++;
					}
					int m = 0;
					int count5 = clusterPrivate.DedupIds.Count;
					while (m < count5)
					{
						int num4 = clusterPrivate.DedupIds[m];
						this.clusterMap[num4] = clusterPrivate;
						m++;
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003338 File Offset: 0x00001538
		private bool CanBeMergedMax(GreedyMerger.ClusterPrivate clusterForValue, GreedyMerger.ClusterPrivate clusterForMatch, FuzzyLookupMatch match)
		{
			List<int> dedupIds = clusterForValue.DedupIds;
			List<int> dedupIds2 = clusterForMatch.DedupIds;
			int i = 0;
			int count = dedupIds.Count;
			while (i < count)
			{
				int j = 0;
				int count2 = dedupIds2.Count;
				while (j < count2)
				{
					int num = dedupIds[i];
					int num2 = dedupIds2[j];
					FuzzyLookupMatch fuzzyLookupMatch;
					if (!this.matchLookup.TryGetMatch(num, num2, out fuzzyLookupMatch))
					{
						return false;
					}
					j++;
				}
				i++;
			}
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000033A8 File Offset: 0x000015A8
		private static bool IsIncompatible(GreedyMerger.ClusterPrivate clusterForValue, GreedyMerger.ClusterPrivate clusterForMatch)
		{
			return (clusterForMatch.IncompatibleDedupIds != null && Enumerable.Any<int>(clusterForValue.DedupIds, (int x) => clusterForMatch.IncompatibleDedupIds.Contains(x))) || (clusterForValue.IncompatibleDedupIds != null && Enumerable.Any<int>(clusterForMatch.DedupIds, (int x) => clusterForValue.IncompatibleDedupIds.Contains(x)));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003421 File Offset: 0x00001621
		private static bool IsIncompatible(GreedyMerger.ClusterPrivate cluster, int newId)
		{
			return cluster.IncompatibleDedupIds != null && cluster.IncompatibleDedupIds.Contains(newId);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000343C File Offset: 0x0000163C
		private bool CanBeMerged(GreedyMerger.ClusterPrivate clusterForValue, GreedyMerger.ClusterPrivate clusterForMatch, FuzzyLookupMatch match)
		{
			if (GreedyMerger.IsIncompatible(clusterForValue, clusterForMatch))
			{
				return false;
			}
			if (this.strategy == MergeClusterStrategy.None)
			{
				return clusterForValue.Cluster.MinSimilarity > 0.99f && clusterForMatch.Cluster.MinSimilarity > 0.99f;
			}
			if (this.strategy == MergeClusterStrategy.Weighted)
			{
				return this.CanBeMergedWeighted(clusterForValue, clusterForMatch, match);
			}
			return this.strategy != MergeClusterStrategy.Max || this.CanBeMergedMax(clusterForValue, clusterForMatch, match);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034AC File Offset: 0x000016AC
		private bool CanBeMergedWeighted(GreedyMerger.ClusterPrivate clusterForValue, GreedyMerger.ClusterPrivate clusterForMatch, FuzzyLookupMatch match)
		{
			List<int> topDedupIds = clusterForValue.TopDedupIds;
			List<int> topDedupIds2 = clusterForMatch.TopDedupIds;
			long num = 0L;
			bool[] array = new bool[topDedupIds2.Count];
			int i = 0;
			int count = topDedupIds.Count;
			while (i < count)
			{
				bool flag = false;
				int num2 = topDedupIds[i];
				int j = 0;
				int count2 = topDedupIds2.Count;
				while (j < count2)
				{
					int num3 = topDedupIds2[j];
					FuzzyLookupMatch fuzzyLookupMatch;
					if (this.matchLookup.TryGetMatch(num2, num3, out fuzzyLookupMatch))
					{
						flag = true;
						array[j] = true;
					}
					j++;
				}
				if (flag)
				{
					num += (long)this.context.GetFrequency(num2);
				}
				i++;
			}
			int num4 = 0;
			int k = 0;
			int count3 = topDedupIds2.Count;
			while (k < count3)
			{
				if (array[k])
				{
					num4 += this.context.GetFrequency(topDedupIds2[k]);
				}
				k++;
			}
			float num5 = (float)num / (float)clusterForValue.AllCount;
			float num6 = (float)num4 / (float)clusterForMatch.AllCount;
			return num5 > 0.4f && num6 > 0.4f;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000035B4 File Offset: 0x000017B4
		private void MergeClusterBy(FuzzyLookupMatch match)
		{
			int dedupId = match.DedupId;
			int matchDedupId = match.MatchDedupId;
			GreedyMerger.ClusterPrivate clusterPrivate = this.clusterMap[dedupId];
			GreedyMerger.ClusterPrivate clusterPrivate2 = this.clusterMap[matchDedupId];
			clusterPrivate.Cluster.Merge(clusterPrivate2.Cluster);
			clusterPrivate.Cluster.AddSingleColumnFuzzyLookupMatch(match);
			List<int> dedupIds = clusterPrivate2.DedupIds;
			clusterPrivate.DedupIds.AddRange(dedupIds);
			List<int> topDedupIds = clusterPrivate2.TopDedupIds;
			int num = 0;
			int count = topDedupIds.Count;
			while (num < count && this.InsertNewId(clusterPrivate.TopDedupIds, topDedupIds[num]))
			{
				num++;
			}
			int i = 0;
			int count2 = dedupIds.Count;
			while (i < count2)
			{
				int num2 = dedupIds[i];
				this.clusterMap[num2] = clusterPrivate;
				i++;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003688 File Offset: 0x00001888
		private void NewCluster(FuzzyLookupMatch match)
		{
			int dedupId = match.DedupId;
			int matchDedupId = match.MatchDedupId;
			Cluster cluster = new Cluster();
			cluster.AddSingleColumnFuzzyLookupMatch(match);
			List<int> list = new List<int>();
			list.Add(dedupId);
			list.Add(matchDedupId);
			HashSet<int> hashSet = null;
			if (this.context.HasIncompatibleValueIds(dedupId) || this.context.HasIncompatibleValueIds(matchDedupId))
			{
				hashSet = new HashSet<int>();
				if (this.context.HasIncompatibleValueIds(dedupId))
				{
					hashSet.UnionWith(this.context.GetIncompatibleValueIds(dedupId));
				}
				if (this.context.HasIncompatibleValueIds(matchDedupId))
				{
					hashSet.UnionWith(this.context.GetIncompatibleValueIds(matchDedupId));
				}
			}
			List<int> list2 = new List<int>();
			int frequency = this.context.GetFrequency(dedupId);
			int frequency2 = this.context.GetFrequency(matchDedupId);
			if (frequency >= frequency2)
			{
				list2.Add(dedupId);
				list2.Add(matchDedupId);
			}
			else
			{
				list2.Add(matchDedupId);
				list2.Add(dedupId);
			}
			GreedyMerger.ClusterPrivate clusterPrivate = new GreedyMerger.ClusterPrivate
			{
				IncompatibleDedupIds = hashSet,
				Cluster = cluster,
				DedupIds = list,
				TopDedupIds = list2,
				AllCount = frequency + frequency2
			};
			this.clusterMap[dedupId] = clusterPrivate;
			this.clusterMap[matchDedupId] = clusterPrivate;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000037C4 File Offset: 0x000019C4
		private int FindInsertionAfterIndex(List<int> topIds, int id)
		{
			int frequency = this.context.GetFrequency(id);
			for (int i = topIds.Count - 1; i >= 0; i--)
			{
				if (this.context.GetFrequency(topIds[i]) >= frequency)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000380C File Offset: 0x00001A0C
		private bool InsertNewId(List<int> topIds, int id)
		{
			int num = this.FindInsertionAfterIndex(topIds, id);
			if (topIds.Count < this.MaxNumOfTopIds)
			{
				topIds.Insert(num + 1, id);
				return true;
			}
			if (num < topIds.Count - 1)
			{
				for (int i = topIds.Count - 2; i > num; i--)
				{
					topIds[i + 1] = topIds[i];
				}
				topIds[num + 1] = id;
				return true;
			}
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003878 File Offset: 0x00001A78
		private void ExtendClusterBy(FuzzyLookupMatch match, int idInExistingCluster, int newId)
		{
			GreedyMerger.ClusterPrivate clusterPrivate = this.clusterMap[idInExistingCluster];
			if (this.context.HasIncompatibleValueIds(newId))
			{
				if (clusterPrivate.IncompatibleDedupIds == null)
				{
					clusterPrivate.IncompatibleDedupIds = new HashSet<int>(this.context.GetIncompatibleValueIds(newId));
				}
				else
				{
					clusterPrivate.IncompatibleDedupIds.UnionWith(this.context.GetIncompatibleValueIds(newId));
				}
			}
			clusterPrivate.Cluster.AddSingleColumnFuzzyLookupMatch(match);
			clusterPrivate.DedupIds.Add(newId);
			this.InsertNewId(clusterPrivate.TopDedupIds, newId);
			this.clusterMap[newId] = clusterPrivate;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000390C File Offset: 0x00001B0C
		public IEnumerable<Cluster> ClusterMatches(IEnumerable<FuzzyLookupMatch> matches)
		{
			List<FuzzyLookupMatch> list = new List<FuzzyLookupMatch>();
			List<FuzzyLookupMatch> list2 = Enumerable.ToList<FuzzyLookupMatch>(Enumerable.OrderByDescending<FuzzyLookupMatch, float>(matches, (FuzzyLookupMatch x) => x.Similarity));
			this.matchLookup = new GreedyMerger.MatchLookup2(list2);
			foreach (FuzzyLookupMatch fuzzyLookupMatch in list2)
			{
				int dedupId = fuzzyLookupMatch.DedupId;
				int matchDedupId = fuzzyLookupMatch.MatchDedupId;
				GreedyMerger.ClusterPrivate clusterPrivate = this.clusterMap[dedupId];
				GreedyMerger.ClusterPrivate clusterPrivate2 = this.clusterMap[matchDedupId];
				if (clusterPrivate != null && clusterPrivate2 != null)
				{
					if (clusterPrivate != clusterPrivate2)
					{
						if (this.CanBeMerged(clusterPrivate, clusterPrivate2, fuzzyLookupMatch))
						{
							this.MergeClusterBy(fuzzyLookupMatch);
						}
						else
						{
							list.Add(fuzzyLookupMatch);
						}
					}
				}
				else if (clusterPrivate2 == null && clusterPrivate == null)
				{
					this.NewCluster(fuzzyLookupMatch);
				}
				else if (clusterPrivate == null)
				{
					if (GreedyMerger.IsIncompatible(clusterPrivate2, dedupId))
					{
						list.Add(fuzzyLookupMatch);
					}
					else
					{
						this.ExtendClusterBy(fuzzyLookupMatch, matchDedupId, dedupId);
					}
				}
				else if (GreedyMerger.IsIncompatible(clusterPrivate, matchDedupId))
				{
					list.Add(fuzzyLookupMatch);
				}
				else
				{
					this.ExtendClusterBy(fuzzyLookupMatch, dedupId, matchDedupId);
				}
			}
			return Enumerable.Distinct<Cluster>(Enumerable.Select<GreedyMerger.ClusterPrivate, Cluster>(Enumerable.Where<GreedyMerger.ClusterPrivate>(this.clusterMap, (GreedyMerger.ClusterPrivate x) => x != null), (GreedyMerger.ClusterPrivate x) => x.Cluster));
		}

		// Token: 0x04000024 RID: 36
		private const int DefaultMaxNumOfTopIds = 10;

		// Token: 0x04000025 RID: 37
		private DedupContext context;

		// Token: 0x04000026 RID: 38
		private List<GreedyMerger.ClusterPrivate> clusterMap;

		// Token: 0x04000027 RID: 39
		private MergeClusterStrategy strategy;

		// Token: 0x04000028 RID: 40
		private GreedyMerger.MatchLookup2 matchLookup;

		// Token: 0x02000019 RID: 25
		private class ClusterPrivate
		{
			// Token: 0x04000041 RID: 65
			public Cluster Cluster;

			// Token: 0x04000042 RID: 66
			public List<int> DedupIds;

			// Token: 0x04000043 RID: 67
			public List<int> TopDedupIds;

			// Token: 0x04000044 RID: 68
			public int AllCount;

			// Token: 0x04000045 RID: 69
			public HashSet<int> IncompatibleDedupIds;
		}

		// Token: 0x0200001A RID: 26
		private class MatchLookup2
		{
			// Token: 0x06000083 RID: 131 RVA: 0x00004640 File Offset: 0x00002840
			internal MatchLookup2(List<FuzzyLookupMatch> matches)
			{
				this.matches = new List<FuzzyLookupMatch>();
				this.map = new FastInt64ToIntHash(0.5f);
				foreach (FuzzyLookupMatch fuzzyLookupMatch in matches)
				{
					long key = DeduplicationUtils.GetKey(fuzzyLookupMatch);
					this.matches.Add(fuzzyLookupMatch);
					int count = this.matches.Count;
					this.map.Add(key, count);
				}
			}

			// Token: 0x06000084 RID: 132 RVA: 0x000046D4 File Offset: 0x000028D4
			internal bool TryGetMatch(int valueId, int matchId, out FuzzyLookupMatch match)
			{
				match = null;
				long key = DeduplicationUtils.GetKey(valueId, matchId);
				int num;
				if (this.map.TryGetValue(key, out num))
				{
					match = this.matches[num - 1];
					return true;
				}
				return false;
			}

			// Token: 0x04000046 RID: 70
			private FastInt64ToIntHash map;

			// Token: 0x04000047 RID: 71
			private List<FuzzyLookupMatch> matches;
		}
	}
}
