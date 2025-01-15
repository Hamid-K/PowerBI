using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000055 RID: 85
	public abstract class RawWeightedGraph : IRawWeightedGraph
	{
		// Token: 0x06000370 RID: 880 RVA: 0x0000978A File Offset: 0x0000798A
		protected RawWeightedGraph(int vertexCapacity, int edgeCapacity)
		{
			this.Edges = new List<EdgeDescriptor>(edgeCapacity);
			this.Vertices = new List<VertexDescriptor>(vertexCapacity);
			this.VerticesArcs = new List<List<RawGraphArc>>(vertexCapacity);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000097B8 File Offset: 0x000079B8
		protected RawWeightedGraph(IRawWeightedGraph baseGraph)
		{
			EdgeDescriptor[] array = new EdgeDescriptor[baseGraph.EdgeCount];
			this.Vertices = new List<VertexDescriptor>(baseGraph.VertexCount);
			this.VerticesArcs = new List<List<RawGraphArc>>(baseGraph.VertexCount);
			for (int i = 0; i < baseGraph.VertexCount; i++)
			{
				this.Vertices.Add(new VertexDescriptor(baseGraph.GetVertexCost(i)));
				IReadOnlyList<RawGraphArc> arcsFromVertex = baseGraph.GetArcsFromVertex(i);
				if (arcsFromVertex.Count == 0)
				{
					this.VerticesArcs.Add(null);
				}
				else
				{
					this.VerticesArcs.Add(new List<RawGraphArc>(arcsFromVertex));
					for (int j = 0; j < arcsFromVertex.Count; j++)
					{
						RawGraphArc rawGraphArc = arcsFromVertex[j];
						if (array[rawGraphArc.EdgeId] == null)
						{
							array[rawGraphArc.EdgeId] = new EdgeDescriptor(i, rawGraphArc.TargetId, baseGraph.GetEdgeCost(rawGraphArc.EdgeId));
						}
					}
				}
			}
			this.Edges = new List<EdgeDescriptor>(array);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000098AA File Offset: 0x00007AAA
		public int VertexCount
		{
			get
			{
				return this.Vertices.Count;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000373 RID: 883 RVA: 0x000098B7 File Offset: 0x00007AB7
		public int EdgeCount
		{
			get
			{
				return this.Edges.Count;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000374 RID: 884 RVA: 0x000098C4 File Offset: 0x00007AC4
		protected List<EdgeDescriptor> Edges { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000375 RID: 885 RVA: 0x000098CC File Offset: 0x00007ACC
		protected List<VertexDescriptor> Vertices { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000376 RID: 886 RVA: 0x000098D4 File Offset: 0x00007AD4
		protected List<List<RawGraphArc>> VerticesArcs { get; }

		// Token: 0x06000377 RID: 887 RVA: 0x000098DC File Offset: 0x00007ADC
		public double GetEdgeCost(int edgeId)
		{
			return this.Edges[edgeId].Cost;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000098EF File Offset: 0x00007AEF
		public double GetVertexCost(int vertexId)
		{
			return this.Vertices[vertexId].Cost;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00009902 File Offset: 0x00007B02
		public void GetEndpoints(int edgeId, out int fromVertexId, out int toVertexId)
		{
			fromVertexId = this.Edges[edgeId].From;
			toVertexId = this.Edges[edgeId].To;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000992C File Offset: 0x00007B2C
		public IReadOnlyList<RawGraphArc> GetArcsFromVertex(int vertexId)
		{
			List<RawGraphArc> list = this.VerticesArcs[vertexId];
			if (list != null)
			{
				return list;
			}
			return Util.EmptyReadOnlyCollection<RawGraphArc>();
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00009954 File Offset: 0x00007B54
		public override string ToString()
		{
			return this.ToDotGraphString(null, false, null, null);
		}
	}
}
