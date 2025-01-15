using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000048 RID: 72
	public interface IDirectedGraph<TVertex>
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000335 RID: 821
		IEqualityComparer<TVertex> Comparer { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000336 RID: 822
		int VertexCount { get; }

		// Token: 0x06000337 RID: 823
		TVertex GetVertex(int index);

		// Token: 0x06000338 RID: 824
		IReadOnlyList<TVertex> GetEdgesFromVertex(TVertex fromVertex);

		// Token: 0x06000339 RID: 825
		bool TryGetEdgesFromVertex(TVertex fromVertex, out IReadOnlyList<TVertex> edgesFromVertex);

		// Token: 0x0600033A RID: 826
		bool HasVertex(TVertex vertex);

		// Token: 0x0600033B RID: 827
		bool HasEdge(TVertex fromVertex, TVertex toVertex);

		// Token: 0x0600033C RID: 828
		string ToString(IVertexStringifier<TVertex> stringifier, IComparer<TVertex> comparer = null);
	}
}
