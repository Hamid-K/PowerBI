using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	internal class GreedyTokenClusterProvider : ITokenClusterMapComputer
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002A10 File Offset: 0x00000C10
		public GreedyTokenClusterProvider()
		{
			this.synRuleGraph = new Graph();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002A23 File Offset: 0x00000C23
		public void Reset()
		{
			this.synRuleGraph.Reset();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A30 File Offset: 0x00000C30
		public void AddRule(Transformation synRule)
		{
			if (synRule.IsUnitRule)
			{
				this.synRuleGraph.AddEdge(synRule.From[0], synRule.To[0]);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A60 File Offset: 0x00000C60
		public void Cluster(ITokenIdProvider tokenIdProvider, int domainId, ITokenToClusterMap tokenClusterMap)
		{
			this.clusterer = new GreedyGraphClusterer(this.synRuleGraph, GreedyTokenClusterProvider.MAX_CLUSTER_SIZE);
			this.clusterer.Cluster(tokenIdProvider, domainId, tokenClusterMap);
		}

		// Token: 0x0400000C RID: 12
		private static readonly int MAX_CLUSTER_SIZE = 100;

		// Token: 0x0400000D RID: 13
		private Graph synRuleGraph;

		// Token: 0x0400000E RID: 14
		private GreedyGraphClusterer clusterer;
	}
}
