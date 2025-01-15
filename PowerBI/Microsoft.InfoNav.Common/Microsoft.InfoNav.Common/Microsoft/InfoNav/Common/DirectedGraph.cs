using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000041 RID: 65
	public class DirectedGraph<TVertex> : IMutableDirectedGraph<TVertex>, IDirectedGraph<TVertex>
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00008884 File Offset: 0x00006A84
		public DirectedGraph()
		{
			this._vertices = new List<TVertex>();
			this._edges = new Dictionary<TVertex, DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?>(this.Comparer);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000088A8 File Offset: 0x00006AA8
		public DirectedGraph(int vertexCount)
		{
			this._vertices = new List<TVertex>(vertexCount);
			this._edges = new Dictionary<TVertex, DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?>(vertexCount, this.Comparer);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000088CE File Offset: 0x00006ACE
		public int VertexCount
		{
			get
			{
				return this._vertices.Count;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000088DB File Offset: 0x00006ADB
		protected List<TVertex> Vertices
		{
			get
			{
				return this._vertices;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000088E3 File Offset: 0x00006AE3
		protected Dictionary<TVertex, DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?> Edges
		{
			get
			{
				return this._edges;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000088EB File Offset: 0x00006AEB
		public IEqualityComparer<TVertex> Comparer
		{
			get
			{
				return EqualityComparer<TVertex>.Default;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000088F4 File Offset: 0x00006AF4
		public static DirectedGraph<TVertex> CreateReverseDirectedGraph(IDirectedGraph<TVertex> originalGraph)
		{
			if (originalGraph == null)
			{
				return null;
			}
			DirectedGraph<TVertex> directedGraph = new DirectedGraph<TVertex>(originalGraph.VertexCount);
			for (int i = 0; i < originalGraph.VertexCount; i++)
			{
				TVertex vertex = originalGraph.GetVertex(i);
				directedGraph.AddVertex(vertex);
				IReadOnlyList<TVertex> edgesFromVertex = originalGraph.GetEdgesFromVertex(vertex);
				if (edgesFromVertex != null)
				{
					foreach (TVertex tvertex in edgesFromVertex)
					{
						directedGraph.AddEdge(tvertex, vertex);
					}
				}
			}
			return directedGraph;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00008984 File Offset: 0x00006B84
		public TVertex GetVertex(int index)
		{
			return this._vertices[index];
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00008994 File Offset: 0x00006B94
		public IReadOnlyList<TVertex> GetEdgesFromVertex(TVertex fromVertex)
		{
			IReadOnlyList<TVertex> readOnlyList;
			if (this.TryGetEdgesFromVertex(fromVertex, out readOnlyList))
			{
				return readOnlyList;
			}
			return Util.EmptyReadOnlyCollection<TVertex>();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000089B4 File Offset: 0x00006BB4
		public bool TryGetEdgesFromVertex(TVertex fromVertex, out IReadOnlyList<TVertex> edgesFromVertex)
		{
			DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>? mutableReadonlyPair;
			if (this._edges.TryGetValue(fromVertex, out mutableReadonlyPair))
			{
				if (mutableReadonlyPair == null)
				{
					mutableReadonlyPair = new DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?(this.CreateInitialEdgesFromVertex(fromVertex));
					this._edges[fromVertex] = mutableReadonlyPair;
					if (mutableReadonlyPair.Value.ReadonlyObj.Count > 0)
					{
						foreach (TVertex tvertex in mutableReadonlyPair.Value.ReadonlyObj)
						{
							this.AddVertex(tvertex);
						}
					}
				}
				edgesFromVertex = mutableReadonlyPair.Value.ReadonlyObj;
				return true;
			}
			edgesFromVertex = null;
			return false;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00008A70 File Offset: 0x00006C70
		public bool HasVertex(TVertex vertex)
		{
			return this._edges.ContainsKey(vertex);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008A80 File Offset: 0x00006C80
		public bool HasEdge(TVertex fromVertex, TVertex toVertex)
		{
			IReadOnlyList<TVertex> readOnlyList;
			return this.TryGetEdgesFromVertex(fromVertex, out readOnlyList) && readOnlyList != null && readOnlyList.Contains(toVertex);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public bool AddVertex(TVertex vertexToAdd)
		{
			bool flag = false;
			if (!this._edges.ContainsKey(vertexToAdd))
			{
				this.AddVertexCore(vertexToAdd, null);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public bool RemoveVertex(TVertex vertexToRemove)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008ADB File Offset: 0x00006CDB
		public bool AddEdge(TVertex fromVertex, TVertex toVertex)
		{
			return this.AddEdgeWithVertexCheck(this.GetOrCreateEdgesFromVertex(fromVertex), toVertex);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008AEC File Offset: 0x00006CEC
		public int AddEdges(TVertex fromVertex, IEnumerable<TVertex> toVertices)
		{
			int num = 0;
			List<TVertex> orCreateEdgesFromVertex = this.GetOrCreateEdgesFromVertex(fromVertex);
			if (toVertices != null)
			{
				foreach (TVertex tvertex in toVertices)
				{
					if (this.AddEdgeWithVertexCheck(orCreateEdgesFromVertex, tvertex))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008B4C File Offset: 0x00006D4C
		public bool RemoveEdge(TVertex fromVertex, TVertex toVertex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008B53 File Offset: 0x00006D53
		public override string ToString()
		{
			return this.ToString(new DefaultVertexStringifier<TVertex>(), null);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00008B64 File Offset: 0x00006D64
		public string ToString(IVertexStringifier<TVertex> stringifier, IComparer<TVertex> comparer = null)
		{
			StringBuilder stringBuilder = new StringBuilder(base.GetType().Name);
			stringBuilder.Append(", ");
			stringBuilder.Append("VertexCount=");
			stringBuilder.Append(this.VertexCount.ToString(CultureInfo.InvariantCulture));
			IEnumerable<TVertex> enumerable;
			if (comparer == null)
			{
				IEnumerable<TVertex> vertices = this.Vertices;
				enumerable = vertices;
			}
			else
			{
				enumerable = this.Vertices.OrderBy(comparer);
			}
			foreach (TVertex tvertex in enumerable)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(" {");
				stringBuilder.Append(stringifier.VertexToString(tvertex));
				stringBuilder.Append(" -> ");
				DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>? mutableReadonlyPair = null;
				if (this.Edges.TryGetValue(tvertex, out mutableReadonlyPair))
				{
					if (mutableReadonlyPair == null)
					{
						stringBuilder.Append("-not probed yet-");
					}
					else if (mutableReadonlyPair.Value.ReadonlyObj.Count > 0)
					{
						IReadOnlyList<TVertex> readOnlyList;
						if (comparer == null)
						{
							readOnlyList = mutableReadonlyPair.Value.ReadonlyObj;
						}
						else
						{
							IReadOnlyList<TVertex> readOnlyList2 = mutableReadonlyPair.Value.ReadonlyObj.OrderBy(comparer).ToList<TVertex>();
							readOnlyList = readOnlyList2;
						}
						IReadOnlyList<TVertex> readOnlyList3 = readOnlyList;
						stringBuilder.Append(stringifier.VertexToString(readOnlyList3[0]));
						for (int i = 1; i < readOnlyList3.Count; i++)
						{
							stringBuilder.Append(", ");
							stringBuilder.Append(stringifier.VertexToString(readOnlyList3[i]));
						}
					}
					else
					{
						stringBuilder.Append("-none-");
					}
				}
				else
				{
					stringBuilder.Append("-vertex not found in edge map, inconsistent data!-");
				}
				stringBuilder.Append("}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008D44 File Offset: 0x00006F44
		protected virtual List<TVertex> CreateInitialEdgesFromVertexOverride(TVertex fromVertex)
		{
			return new List<TVertex>();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00008D4B File Offset: 0x00006F4B
		protected bool IsVertexCountConsistent()
		{
			return this._vertices.Count == this._edges.Count;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008D65 File Offset: 0x00006F65
		private DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>> CreateInitialEdgesFromVertex(TVertex fromVertex)
		{
			List<TVertex> list = this.CreateInitialEdgesFromVertexOverride(fromVertex);
			return new DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>(list, new ReadOnlyCollection<TVertex>(list));
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00008D7C File Offset: 0x00006F7C
		private List<TVertex> GetOrCreateEdgesFromVertex(TVertex fromVertex)
		{
			DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>? mutableReadonlyPair = null;
			if (!this._edges.TryGetValue(fromVertex, out mutableReadonlyPair))
			{
				mutableReadonlyPair = new DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?(this.CreateInitialEdgesFromVertex(fromVertex));
				this.AddVertexCore(fromVertex, mutableReadonlyPair);
			}
			else if (mutableReadonlyPair == null)
			{
				mutableReadonlyPair = new DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?(this.CreateInitialEdgesFromVertex(fromVertex));
				this._edges[fromVertex] = mutableReadonlyPair;
			}
			return mutableReadonlyPair.Value.MutableObj;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008DEC File Offset: 0x00006FEC
		private bool AddEdgeWithVertexCheck(List<TVertex> destinationEdges, TVertex toVertex)
		{
			if (!destinationEdges.Contains(toVertex))
			{
				destinationEdges.Add(toVertex);
				if (!this._edges.ContainsKey(toVertex))
				{
					this.AddVertexCore(toVertex, null);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008E2A File Offset: 0x0000702A
		private void AddVertexCore(TVertex vertex, DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>? initialEdges)
		{
			this._vertices.Add(vertex);
			this._edges.Add(vertex, initialEdges);
		}

		// Token: 0x040000A1 RID: 161
		private readonly List<TVertex> _vertices;

		// Token: 0x040000A2 RID: 162
		private readonly Dictionary<TVertex, DirectedGraph<TVertex>.MutableReadonlyPair<List<TVertex>, IReadOnlyList<TVertex>>?> _edges;

		// Token: 0x020000BA RID: 186
		public struct MutableReadonlyPair<TMutable, TReadonly> where TMutable : class where TReadonly : class
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x0000EF22 File Offset: 0x0000D122
			public MutableReadonlyPair(TMutable mutableObj, TReadonly readonlyObj)
			{
				this.m_mutable = mutableObj;
				this.m_readonly = readonlyObj;
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000EF32 File Offset: 0x0000D132
			public TMutable MutableObj
			{
				get
				{
					return this.m_mutable;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0000EF3A File Offset: 0x0000D13A
			public TReadonly ReadonlyObj
			{
				get
				{
					return this.m_readonly;
				}
			}

			// Token: 0x040001D9 RID: 473
			private readonly TMutable m_mutable;

			// Token: 0x040001DA RID: 474
			private readonly TReadonly m_readonly;
		}
	}
}
