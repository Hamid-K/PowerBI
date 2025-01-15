using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000047 RID: 71
	public interface ICostMutableRawWeightedGraph : IRawWeightedGraph
	{
		// Token: 0x06000333 RID: 819
		void SetEdgeCost(int edgeId, double newCost);

		// Token: 0x06000334 RID: 820
		void SetVertexCost(int vertexId, double newCost);
	}
}
