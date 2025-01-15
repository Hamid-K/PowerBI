using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Text;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020005D2 RID: 1490
	internal class UndirectedGraph<TVertex> : InternalBase
	{
		// Token: 0x060047CF RID: 18383 RVA: 0x000FEB5E File Offset: 0x000FCD5E
		internal UndirectedGraph(IEqualityComparer<TVertex> comparer)
		{
			this.m_graph = new Graph<TVertex>(comparer);
			this.m_comparer = comparer;
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x000FEB79 File Offset: 0x000FCD79
		internal IEnumerable<TVertex> Vertices
		{
			get
			{
				return this.m_graph.Vertices;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x000FEB86 File Offset: 0x000FCD86
		internal IEnumerable<KeyValuePair<TVertex, TVertex>> Edges
		{
			get
			{
				return this.m_graph.Edges;
			}
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x000FEB93 File Offset: 0x000FCD93
		internal void AddVertex(TVertex vertex)
		{
			this.m_graph.AddVertex(vertex);
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x000FEBA1 File Offset: 0x000FCDA1
		internal void AddEdge(TVertex first, TVertex second)
		{
			this.m_graph.AddEdge(first, second);
			this.m_graph.AddEdge(second, first);
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x000FEBC0 File Offset: 0x000FCDC0
		internal KeyToListMap<int, TVertex> GenerateConnectedComponents()
		{
			int num = 0;
			Dictionary<TVertex, UndirectedGraph<TVertex>.ComponentNum> dictionary = new Dictionary<TVertex, UndirectedGraph<TVertex>.ComponentNum>(this.m_comparer);
			foreach (TVertex tvertex in this.Vertices)
			{
				dictionary.Add(tvertex, new UndirectedGraph<TVertex>.ComponentNum(num));
				num++;
			}
			foreach (KeyValuePair<TVertex, TVertex> keyValuePair in this.Edges)
			{
				if (dictionary[keyValuePair.Key].componentNum != dictionary[keyValuePair.Value].componentNum)
				{
					int componentNum = dictionary[keyValuePair.Value].componentNum;
					int componentNum2 = dictionary[keyValuePair.Key].componentNum;
					dictionary[keyValuePair.Value].componentNum = componentNum2;
					foreach (TVertex tvertex2 in dictionary.Keys)
					{
						if (dictionary[tvertex2].componentNum == componentNum)
						{
							dictionary[tvertex2].componentNum = componentNum2;
						}
					}
				}
			}
			KeyToListMap<int, TVertex> keyToListMap = new KeyToListMap<int, TVertex>(EqualityComparer<int>.Default);
			foreach (TVertex tvertex3 in this.Vertices)
			{
				int componentNum3 = dictionary[tvertex3].componentNum;
				keyToListMap.Add(componentNum3, tvertex3);
			}
			return keyToListMap;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x000FED88 File Offset: 0x000FCF88
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_graph);
		}

		// Token: 0x0400198A RID: 6538
		private readonly Graph<TVertex> m_graph;

		// Token: 0x0400198B RID: 6539
		private readonly IEqualityComparer<TVertex> m_comparer;

		// Token: 0x02000C0C RID: 3084
		private class ComponentNum
		{
			// Token: 0x0600693E RID: 26942 RVA: 0x00167DA2 File Offset: 0x00165FA2
			internal ComponentNum(int compNum)
			{
				this.componentNum = compNum;
			}

			// Token: 0x0600693F RID: 26943 RVA: 0x00167DB1 File Offset: 0x00165FB1
			public override string ToString()
			{
				return StringUtil.FormatInvariant("{0}", new object[] { this.componentNum });
			}

			// Token: 0x04002FAC RID: 12204
			internal int componentNum;
		}
	}
}
