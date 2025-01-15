using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200004D RID: 77
	public interface IWeightedGraph<TVertex, TEdgeInfo>
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600034B RID: 843
		IEqualityComparer<TVertex> VertexComparer { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600034C RID: 844
		IRawWeightedGraph RawGraph { get; }

		// Token: 0x0600034D RID: 845
		int GetVertexId(TVertex vertex);

		// Token: 0x0600034E RID: 846
		TVertex GetVertex(int vertexId);

		// Token: 0x0600034F RID: 847
		TEdgeInfo GetEdgeInfo(int edgeId);

		// Token: 0x06000350 RID: 848
		bool HasVertex(TVertex vertex);
	}
}
