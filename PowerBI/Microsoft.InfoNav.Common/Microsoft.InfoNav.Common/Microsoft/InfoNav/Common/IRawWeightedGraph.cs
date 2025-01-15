using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200004C RID: 76
	public interface IRawWeightedGraph
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000345 RID: 837
		int VertexCount { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000346 RID: 838
		int EdgeCount { get; }

		// Token: 0x06000347 RID: 839
		double GetEdgeCost(int edgeId);

		// Token: 0x06000348 RID: 840
		double GetVertexCost(int vertexId);

		// Token: 0x06000349 RID: 841
		void GetEndpoints(int edgeId, out int fromVertexId, out int toVertexId);

		// Token: 0x0600034A RID: 842
		IReadOnlyList<RawGraphArc> GetArcsFromVertex(int vertexId);
	}
}
