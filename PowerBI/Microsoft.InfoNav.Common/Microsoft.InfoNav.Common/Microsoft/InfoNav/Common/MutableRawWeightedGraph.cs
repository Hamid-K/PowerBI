using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000050 RID: 80
	[ImmutableObject(false)]
	internal abstract class MutableRawWeightedGraph : RawWeightedGraph, ICostMutableRawWeightedGraph, IRawWeightedGraph
	{
		// Token: 0x06000356 RID: 854 RVA: 0x00009499 File Offset: 0x00007699
		protected MutableRawWeightedGraph(int vertexCapacity, int edgeCapacity)
			: base(vertexCapacity, edgeCapacity)
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000094A3 File Offset: 0x000076A3
		protected MutableRawWeightedGraph(IRawWeightedGraph baseGraph)
			: base(baseGraph)
		{
		}

		// Token: 0x06000358 RID: 856
		protected abstract void AddArcs(int edgeId, int fromVertexId, int toVertexId);

		// Token: 0x06000359 RID: 857 RVA: 0x000094AC File Offset: 0x000076AC
		public void SetEdgeCost(int edgeId, double newCost)
		{
			EdgeDescriptor edgeDescriptor = base.Edges[edgeId];
			base.Edges[edgeId] = new EdgeDescriptor(edgeDescriptor.From, edgeDescriptor.To, newCost);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000094E4 File Offset: 0x000076E4
		public void SetVertexCost(int vertexId, double newCost)
		{
			base.Vertices[vertexId] = new VertexDescriptor(newCost);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000094F8 File Offset: 0x000076F8
		public int AddEdge(int fromVertexId, int toVertexId, double cost)
		{
			int count = base.Edges.Count;
			base.Edges.Add(new EdgeDescriptor(fromVertexId, toVertexId, cost));
			this.AddArcs(count, fromVertexId, toVertexId);
			return count;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000952E File Offset: 0x0000772E
		public int AddVertex(double cost)
		{
			int count = base.Vertices.Count;
			base.Vertices.Add(new VertexDescriptor(cost));
			base.VerticesArcs.Add(null);
			return count;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009558 File Offset: 0x00007758
		protected void AddArc(int edgeId, int fromVertexId, int toVertexId)
		{
			List<RawGraphArc> list = base.VerticesArcs[fromVertexId];
			if (list == null)
			{
				list = new List<RawGraphArc>();
				base.VerticesArcs[fromVertexId] = list;
			}
			list.Add(new RawGraphArc(toVertexId, edgeId));
		}
	}
}
