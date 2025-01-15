using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Utils.Clustering
{
	// Token: 0x02000603 RID: 1539
	internal class Cover<TNode> : IEnumerable<Cluster<TNode>>, IEnumerable where TNode : IProgramNodeBuilder
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x0005F440 File Offset: 0x0005D640
		internal Cover(ClusterPool<TNode> clusterPool, IReadOnlyList<Cluster<TNode>> coveringClusters)
		{
			this._coveringClusters = coveringClusters;
			this.UncoveredExamples = clusterPool.UnionComplement(coveringClusters);
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0005F45C File Offset: 0x0005D65C
		internal ExampleSubset UncoveredExamples { get; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0005F464 File Offset: 0x0005D664
		internal uint NumberUncoveredExamples
		{
			get
			{
				return this.UncoveredExamples.Cardinality;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0005F47F File Offset: 0x0005D67F
		internal bool HasUncoveredExamples
		{
			get
			{
				return this.NumberUncoveredExamples > 0U;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x0005F48A File Offset: 0x0005D68A
		internal int Count
		{
			get
			{
				return this._coveringClusters.Count;
			}
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0005F497 File Offset: 0x0005D697
		public IEnumerator<Cluster<TNode>> GetEnumerator()
		{
			return this._coveringClusters.GetEnumerator();
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0005F4A4 File Offset: 0x0005D6A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._coveringClusters.GetEnumerator();
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0005F4B4 File Offset: 0x0005D6B4
		internal Cover<TNode> TryAppend(ClusterPool<TNode> clusterPool, Cluster<TNode> cluster)
		{
			if (this._coveringClusters.Any((Cluster<TNode> c) => clusterPool.Intersects(c, cluster)))
			{
				return null;
			}
			return new Cover<TNode>(clusterPool, this._coveringClusters.AppendItem(cluster).ToList<Cluster<TNode>>());
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0005F511 File Offset: 0x0005D711
		internal Cluster<TNode> Sample(Random rng)
		{
			return this._coveringClusters.RandomElement(rng);
		}

		// Token: 0x04000FE8 RID: 4072
		private readonly IReadOnlyList<Cluster<TNode>> _coveringClusters;
	}
}
