using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x0200004B RID: 75
	public interface IMutableDirectedGraph<TVertex> : IDirectedGraph<TVertex>
	{
		// Token: 0x06000340 RID: 832
		bool AddVertex(TVertex vertexToAdd);

		// Token: 0x06000341 RID: 833
		bool RemoveVertex(TVertex vertexToRemove);

		// Token: 0x06000342 RID: 834
		bool AddEdge(TVertex fromVertex, TVertex toVertex);

		// Token: 0x06000343 RID: 835
		int AddEdges(TVertex fromVertex, IEnumerable<TVertex> toVertices);

		// Token: 0x06000344 RID: 836
		bool RemoveEdge(TVertex fromVertex, TVertex toVertex);
	}
}
