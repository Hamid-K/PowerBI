using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000051 RID: 81
	[ImmutableObject(false)]
	public sealed class MutableWeightedGraph<TVertex, TEdgeInfo> : IWeightedGraph<TVertex, TEdgeInfo>
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00009598 File Offset: 0x00007798
		private MutableWeightedGraph(MutableRawWeightedGraph rawGraph, int vertexCapacity, int edgeCapacity, IEqualityComparer<TVertex> vertexComparer)
		{
			this._rawGraph = rawGraph;
			this.VertexComparer = vertexComparer ?? EqualityComparer<TVertex>.Default;
			this._vertexIdToVertex = new List<TVertex>(vertexCapacity);
			this._edgeIdToEdge = new Dictionary<int, TEdgeInfo>(edgeCapacity);
			this._vertexToVertexId = new Dictionary<TVertex, int>(this.VertexComparer);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000095EC File Offset: 0x000077EC
		public IEqualityComparer<TVertex> VertexComparer { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000095F4 File Offset: 0x000077F4
		public IRawWeightedGraph RawGraph
		{
			get
			{
				return this._rawGraph;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000095FC File Offset: 0x000077FC
		public static MutableWeightedGraph<TVertex, TEdgeInfo> CreateUndirectedGraph(int vertexCapacity = 4, int edgeCapacity = 4, IEqualityComparer<TVertex> vertexComparer = null)
		{
			return new MutableWeightedGraph<TVertex, TEdgeInfo>(new MutableRawUndirectedWeightedGraph(vertexCapacity, edgeCapacity), vertexCapacity, edgeCapacity, vertexComparer);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000960D File Offset: 0x0000780D
		public static MutableWeightedGraph<TVertex, TEdgeInfo> CreateDirectedGraph(int vertexCapacity = 4, int edgeCapacity = 4, IEqualityComparer<TVertex> vertexComparer = null)
		{
			return new MutableWeightedGraph<TVertex, TEdgeInfo>(new MutableRawDirectedWeightedGraph(vertexCapacity, edgeCapacity), vertexCapacity, edgeCapacity, vertexComparer);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000961E File Offset: 0x0000781E
		public int GetVertexId(TVertex vertex)
		{
			return this._vertexToVertexId[vertex];
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000962C File Offset: 0x0000782C
		public TVertex GetVertex(int vertexId)
		{
			return this._vertexIdToVertex[vertexId];
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000963A File Offset: 0x0000783A
		public TEdgeInfo GetEdgeInfo(int edgeId)
		{
			return this._edgeIdToEdge[edgeId];
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00009648 File Offset: 0x00007848
		public bool HasVertex(TVertex vertex)
		{
			return this._vertexToVertexId.ContainsKey(vertex);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00009656 File Offset: 0x00007856
		public override string ToString()
		{
			return this.ToDotGraphString(null, false, null, null);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009662 File Offset: 0x00007862
		public int AddVertex(TVertex vertexToAdd)
		{
			return this.AddVertex(vertexToAdd, 0.0);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009674 File Offset: 0x00007874
		public int AddVertex(TVertex vertexToAdd, double cost)
		{
			int num;
			if (this._vertexToVertexId.TryGetValue(vertexToAdd, out num))
			{
				return num;
			}
			return this.AddVertexCore(vertexToAdd, cost);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000969B File Offset: 0x0000789B
		public int AddEdge(TVertex fromVertex, TVertex toVertex, TEdgeInfo connectingEdge)
		{
			return this.AddEdge(fromVertex, toVertex, connectingEdge, 1.0);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000096B0 File Offset: 0x000078B0
		public int AddEdge(TVertex fromVertex, TVertex toVertex, TEdgeInfo connectingEdge, double cost)
		{
			int num;
			if (!this._vertexToVertexId.TryGetValue(fromVertex, out num))
			{
				num = this.AddVertexCore(fromVertex, 0.0);
			}
			int num2;
			if (!this._vertexToVertexId.TryGetValue(toVertex, out num2))
			{
				num2 = this.AddVertexCore(toVertex, 0.0);
			}
			int num3 = this._rawGraph.AddEdge(num, num2, cost);
			this._edgeIdToEdge.Add(num3, connectingEdge);
			return num3;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009720 File Offset: 0x00007920
		private int AddVertexCore(TVertex vertexToAdd, double cost)
		{
			int num = this._rawGraph.AddVertex(cost);
			this._vertexToVertexId.Add(vertexToAdd, num);
			this._vertexIdToVertex.Add(vertexToAdd);
			return num;
		}

		// Token: 0x040000AA RID: 170
		private const double DefaultVertexCost = 0.0;

		// Token: 0x040000AB RID: 171
		private const double DefaultEdgeCost = 1.0;

		// Token: 0x040000AC RID: 172
		private const int DefaultVertexCapacity = 4;

		// Token: 0x040000AD RID: 173
		private const int DefaultEdgeCapacity = 4;

		// Token: 0x040000AE RID: 174
		private readonly MutableRawWeightedGraph _rawGraph;

		// Token: 0x040000AF RID: 175
		private readonly Dictionary<TVertex, int> _vertexToVertexId;

		// Token: 0x040000B0 RID: 176
		private readonly List<TVertex> _vertexIdToVertex;

		// Token: 0x040000B1 RID: 177
		private readonly Dictionary<int, TEdgeInfo> _edgeIdToEdge;
	}
}
