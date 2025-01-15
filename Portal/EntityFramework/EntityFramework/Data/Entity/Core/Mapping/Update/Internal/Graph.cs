using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005C8 RID: 1480
	internal class Graph<TVertex>
	{
		// Token: 0x0600477D RID: 18301 RVA: 0x000FD538 File Offset: 0x000FB738
		internal Graph(IEqualityComparer<TVertex> comparer)
		{
			this.m_comparer = comparer;
			this.m_successorMap = new Dictionary<TVertex, HashSet<TVertex>>(comparer);
			this.m_predecessorCounts = new Dictionary<TVertex, int>(comparer);
			this.m_vertices = new HashSet<TVertex>(comparer);
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x000FD56B File Offset: 0x000FB76B
		internal IEnumerable<TVertex> Vertices
		{
			get
			{
				return this.m_vertices;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x0600477F RID: 18303 RVA: 0x000FD573 File Offset: 0x000FB773
		internal IEnumerable<KeyValuePair<TVertex, TVertex>> Edges
		{
			get
			{
				foreach (KeyValuePair<TVertex, HashSet<TVertex>> successors in this.m_successorMap)
				{
					foreach (TVertex tvertex in successors.Value)
					{
						yield return new KeyValuePair<TVertex, TVertex>(successors.Key, tvertex);
					}
					HashSet<TVertex>.Enumerator enumerator2 = default(HashSet<TVertex>.Enumerator);
					successors = default(KeyValuePair<TVertex, HashSet<TVertex>>);
				}
				Dictionary<TVertex, HashSet<TVertex>>.Enumerator enumerator = default(Dictionary<TVertex, HashSet<TVertex>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000FD583 File Offset: 0x000FB783
		internal void AddVertex(TVertex vertex)
		{
			this.m_vertices.Add(vertex);
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x000FD594 File Offset: 0x000FB794
		internal void AddEdge(TVertex from, TVertex to)
		{
			if (this.m_vertices.Contains(from) && this.m_vertices.Contains(to))
			{
				HashSet<TVertex> hashSet;
				if (!this.m_successorMap.TryGetValue(from, out hashSet))
				{
					hashSet = new HashSet<TVertex>(this.m_comparer);
					this.m_successorMap.Add(from, hashSet);
				}
				if (hashSet.Add(to))
				{
					int num;
					if (!this.m_predecessorCounts.TryGetValue(to, out num))
					{
						num = 1;
					}
					else
					{
						num++;
					}
					this.m_predecessorCounts[to] = num;
				}
			}
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x000FD614 File Offset: 0x000FB814
		internal bool TryTopologicalSort(out IEnumerable<TVertex> orderedVertices, out IEnumerable<TVertex> remainder)
		{
			SortedSet<TVertex> sortedSet = new SortedSet<TVertex>(Comparer<TVertex>.Default);
			foreach (TVertex tvertex in this.m_vertices)
			{
				int num;
				if (!this.m_predecessorCounts.TryGetValue(tvertex, out num) || num == 0)
				{
					sortedSet.Add(tvertex);
				}
			}
			TVertex[] array = new TVertex[this.m_vertices.Count];
			int num2 = 0;
			while (0 < sortedSet.Count)
			{
				TVertex min = sortedSet.Min;
				sortedSet.Remove(min);
				HashSet<TVertex> hashSet;
				if (this.m_successorMap.TryGetValue(min, out hashSet))
				{
					foreach (TVertex tvertex2 in hashSet)
					{
						int num3 = this.m_predecessorCounts[tvertex2] - 1;
						this.m_predecessorCounts[tvertex2] = num3;
						if (num3 == 0)
						{
							sortedSet.Add(tvertex2);
						}
					}
					this.m_successorMap.Remove(min);
				}
				array[num2++] = min;
				this.m_vertices.Remove(min);
			}
			if (this.m_vertices.Count == 0)
			{
				orderedVertices = array;
				remainder = Enumerable.Empty<TVertex>();
				return true;
			}
			orderedVertices = array.Take(num2);
			remainder = this.m_vertices;
			return false;
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x000FD78C File Offset: 0x000FB98C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<TVertex, HashSet<TVertex>> keyValuePair in this.m_successorMap)
			{
				bool flag = true;
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}] --> ", new object[] { keyValuePair.Key });
				foreach (TVertex tvertex in keyValuePair.Value)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "[{0}]", new object[] { tvertex });
				}
				stringBuilder.Append("; ");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001965 RID: 6501
		private readonly Dictionary<TVertex, HashSet<TVertex>> m_successorMap;

		// Token: 0x04001966 RID: 6502
		private readonly Dictionary<TVertex, int> m_predecessorCounts;

		// Token: 0x04001967 RID: 6503
		private readonly HashSet<TVertex> m_vertices;

		// Token: 0x04001968 RID: 6504
		private readonly IEqualityComparer<TVertex> m_comparer;
	}
}
