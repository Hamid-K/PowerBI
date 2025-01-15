using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals.Learning.Clustering
{
	// Token: 0x02000A64 RID: 2660
	internal class BisectingClustering
	{
		// Token: 0x06004204 RID: 16900 RVA: 0x000CE70B File Offset: 0x000CC90B
		public BisectingClustering(Cluster rootCluster, IReadOnlyList<HashSet<string>> inSameCluster, IReadOnlyList<HashSet<string>> inDifferentClusters)
		{
			this._clusters = new List<Cluster> { rootCluster };
			this._inSameCluster = inSameCluster;
			this._inDifferentClusters = inDifferentClusters;
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000CE733 File Offset: 0x000CC933
		public IReadOnlyCollection<Cluster> TopClusters()
		{
			return this.TopKClusters(2);
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000CE73C File Offset: 0x000CC93C
		public IReadOnlyCollection<Cluster> TopKClusters(int k)
		{
			if (k < this._clusters.Count)
			{
				throw new ArgumentException("Number of clusters is smaller than that in previous call", "k");
			}
			HashSet<Cluster> hashSet = new HashSet<Cluster>();
			bool flag = false;
			while (this._clusters.Count < k)
			{
				if (flag)
				{
					if (hashSet.Count == 0)
					{
						break;
					}
					hashSet.Clear();
				}
				for (int i = 0; i < this._clusters.Count; i++)
				{
					flag = true;
					if (this._clusters.Count == k)
					{
						break;
					}
					Cluster cluster = this._clusters[i];
					if (!hashSet.Contains(cluster))
					{
						Record<Cluster, Cluster>? record = cluster.Bisect(this._inSameCluster, this._inDifferentClusters);
						if (record != null)
						{
							flag = false;
							hashSet.Add(record.Value.Item1);
							this._clusters[i] = record.Value.Item1;
							this._clusters.Insert(i + 1, record.Value.Item2);
							i++;
						}
					}
				}
			}
			if (this._clusters.Count < k)
			{
				return null;
			}
			Cluster cluster2 = this._clusters.Last<Cluster>();
			if (this._inDifferentClusters.Count > 0)
			{
				HashSet<string> lastClusterInputs = cluster2.DataPoints.Select((DataPoint p) => p.Input).ConvertToHashSet<string>();
				if (this._inDifferentClusters.Any((HashSet<string> diff) => diff.Intersect(lastClusterInputs).Count<string>() > 1))
				{
					return null;
				}
			}
			Cluster cluster3 = new Cluster(cluster2.DataPoints, cluster2.AllFeatures, new BasicPredicateFeature[]
			{
				new BasicPredicateFeature(PredicateType.True, false, 1)
			});
			List<Cluster> list = new List<Cluster>(this._clusters);
			int num = this._clusters.Count - 1;
			list[num] = cluster3;
			return list;
		}

		// Token: 0x04001DB1 RID: 7601
		private readonly IList<Cluster> _clusters;

		// Token: 0x04001DB2 RID: 7602
		private readonly IReadOnlyList<HashSet<string>> _inDifferentClusters;

		// Token: 0x04001DB3 RID: 7603
		private readonly IReadOnlyList<HashSet<string>> _inSameCluster;
	}
}
