using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000057 RID: 87
	[ImmutableObject(true)]
	public class ReadOnlyWeightedGraph<TVertex, TEdgeInfo> : IWeightedGraph<TVertex, TEdgeInfo>
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0000997F File Offset: 0x00007B7F
		private ReadOnlyWeightedGraph()
		{
			this.VertexComparer = EqualityComparer<TVertex>.Default;
			this._vertexToVertexId = Util.EmptyReadOnlyDictionary<TVertex, int>();
			this._vertexIdToVertex = Util.EmptyReadOnlyCollection<TVertex>();
			this._edgeIdToEdge = Util.EmptyReadOnlyCollection<TEdgeInfo>();
			this.RawGraph = ReadOnlyRawWeightedGraph.Empty;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000099C0 File Offset: 0x00007BC0
		public ReadOnlyWeightedGraph(IWeightedGraph<TVertex, TEdgeInfo> baseGraph)
		{
			this.VertexComparer = baseGraph.VertexComparer;
			int vertexCount = baseGraph.RawGraph.VertexCount;
			int edgeCount = baseGraph.RawGraph.EdgeCount;
			Dictionary<TVertex, int> dictionary = new Dictionary<TVertex, int>(vertexCount, this.VertexComparer);
			TVertex[] array = new TVertex[vertexCount];
			TEdgeInfo[] array2 = new TEdgeInfo[edgeCount];
			for (int i = 0; i < vertexCount; i++)
			{
				TVertex vertex = baseGraph.GetVertex(i);
				dictionary.Add(vertex, i);
				array[i] = vertex;
			}
			for (int j = 0; j < edgeCount; j++)
			{
				array2[j] = baseGraph.GetEdgeInfo(j);
			}
			this._vertexToVertexId = dictionary.AsReadOnlyDictionary<TVertex, int>();
			this._vertexIdToVertex = array.AsReadOnlyCollection<TVertex>();
			this._edgeIdToEdge = array2.AsReadOnlyCollection<TEdgeInfo>();
			this.RawGraph = baseGraph.RawGraph.AsReadOnlyGraph();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00009A98 File Offset: 0x00007C98
		public IEqualityComparer<TVertex> VertexComparer { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public IRawWeightedGraph RawGraph { get; }

		// Token: 0x06000383 RID: 899 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public int GetVertexId(TVertex vertex)
		{
			return this._vertexToVertexId[vertex];
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00009AB6 File Offset: 0x00007CB6
		public TVertex GetVertex(int vertexId)
		{
			return this._vertexIdToVertex[vertexId];
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00009AC4 File Offset: 0x00007CC4
		public TEdgeInfo GetEdgeInfo(int edgeId)
		{
			return this._edgeIdToEdge[edgeId];
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009AD2 File Offset: 0x00007CD2
		public bool HasVertex(TVertex vertex)
		{
			return this._vertexToVertexId.ContainsKey(vertex);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00009AE0 File Offset: 0x00007CE0
		public override string ToString()
		{
			return this.ToDotGraphString(null, false, null, null);
		}

		// Token: 0x040000BD RID: 189
		public static readonly ReadOnlyWeightedGraph<TVertex, TEdgeInfo> Empty = new ReadOnlyWeightedGraph<TVertex, TEdgeInfo>();

		// Token: 0x040000BE RID: 190
		private readonly ReadOnlyDictionary<TVertex, int> _vertexToVertexId;

		// Token: 0x040000BF RID: 191
		private readonly ReadOnlyCollection<TVertex> _vertexIdToVertex;

		// Token: 0x040000C0 RID: 192
		private readonly ReadOnlyCollection<TEdgeInfo> _edgeIdToEdge;
	}
}
