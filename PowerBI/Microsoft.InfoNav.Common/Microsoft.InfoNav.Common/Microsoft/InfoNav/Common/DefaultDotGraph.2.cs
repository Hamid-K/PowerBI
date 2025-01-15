using System;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000045 RID: 69
	public class DefaultDotGraph<TVertex, TEdgeInfo> : DefaultDotGraph
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00008E90 File Offset: 0x00007090
		public DefaultDotGraph(IWeightedGraph<TVertex, TEdgeInfo> graph)
		{
			this._graph = graph;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00008E9F File Offset: 0x0000709F
		public override bool SortContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008EA4 File Offset: 0x000070A4
		public override string GetVertexName(int vertexId)
		{
			TVertex vertex = this._graph.GetVertex(vertexId);
			return vertex.ToString();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008ECC File Offset: 0x000070CC
		public override string GetEdgeName(int edgeId)
		{
			TEdgeInfo edgeInfo = this._graph.GetEdgeInfo(edgeId);
			return edgeInfo.ToString();
		}

		// Token: 0x040000A9 RID: 169
		private readonly IWeightedGraph<TVertex, TEdgeInfo> _graph;
	}
}
