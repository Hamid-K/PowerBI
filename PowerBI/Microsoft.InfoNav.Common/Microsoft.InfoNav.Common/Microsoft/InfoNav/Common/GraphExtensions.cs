using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000046 RID: 70
	public static class GraphExtensions
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00008EF3 File Offset: 0x000070F3
		public static IEnumerable<TVertex> GetVertices<TVertex, TEdgeInfo>(this IWeightedGraph<TVertex, TEdgeInfo> graph)
		{
			int num;
			for (int i = 0; i < graph.RawGraph.VertexCount; i = num + 1)
			{
				yield return graph.GetVertex(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008F04 File Offset: 0x00007104
		public static int GetOtherEndpoint(this IRawWeightedGraph graph, int edgeId, int endpointId)
		{
			int num;
			int num2;
			graph.GetEndpoints(edgeId, out num, out num2);
			if (num == endpointId)
			{
				return num2;
			}
			if (num2 == endpointId)
			{
				return num;
			}
			throw Contract.ExceptRange("endpointId");
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008F32 File Offset: 0x00007132
		public static ReadOnlyRawWeightedGraph AsReadOnlyGraph(this IRawWeightedGraph baseGraph)
		{
			return (baseGraph as ReadOnlyRawWeightedGraph) ?? new ReadOnlyRawWeightedGraph(baseGraph);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008F44 File Offset: 0x00007144
		public static ReadOnlyWeightedGraph<TVertex, TEdgeInfo> AsReadOnlyGraph<TVertex, TEdgeInfo>(this IWeightedGraph<TVertex, TEdgeInfo> baseGraph)
		{
			return (baseGraph as ReadOnlyWeightedGraph<TVertex, TEdgeInfo>) ?? new ReadOnlyWeightedGraph<TVertex, TEdgeInfo>(baseGraph);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008F56 File Offset: 0x00007156
		public static IEnumerable<ReadOnlyCollection<int>> GetVertexIslands(this IRawWeightedGraph graph, ISet<int> rootVertices = null, ISet<int> activeVertices = null, ISet<int> activeEdges = null)
		{
			activeVertices = activeVertices ?? rootVertices;
			HashSet<int> visitedVertices = new HashSet<int>();
			Stack<int> stack = new Stack<int>();
			IEnumerable<int> enumerable = ((rootVertices != null) ? rootVertices : Enumerable.Range(0, graph.VertexCount));
			IEnumerator<int> enumerator;
			if ((activeEdges != null && activeEdges.Count == 0) || (activeVertices != null && activeVertices.Count == 0))
			{
				foreach (int num in enumerable)
				{
					yield return num.ArrayWrap<int>().AsReadOnlyCollection<int>();
				}
				enumerator = null;
				yield break;
			}
			foreach (int num2 in enumerable)
			{
				if (visitedVertices.Add(num2))
				{
					List<int> list = new List<int>();
					list.Add(num2);
					stack.Push(num2);
					do
					{
						int num3 = stack.Pop();
						IReadOnlyList<RawGraphArc> arcsFromVertex = graph.GetArcsFromVertex(num3);
						for (int i = 0; i < arcsFromVertex.Count; i++)
						{
							RawGraphArc rawGraphArc = arcsFromVertex[i];
							if ((activeEdges == null || activeEdges.Contains(rawGraphArc.EdgeId)) && (activeVertices == null || activeVertices.Contains(rawGraphArc.TargetId)) && visitedVertices.Add(rawGraphArc.TargetId))
							{
								list.Add(rawGraphArc.TargetId);
								stack.Push(rawGraphArc.TargetId);
							}
						}
					}
					while (stack.Count != 0);
					yield return list.AsReadOnlyCollection<int>();
				}
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00008F7C File Offset: 0x0000717C
		public static bool HasIsland(this IRawWeightedGraph graph, ISet<int> activeVertices = null, ISet<int> activeEdges = null)
		{
			int num = ((activeVertices == null) ? graph.VertexCount : activeVertices.Count);
			if (num == 0)
			{
				return false;
			}
			if (((activeEdges == null) ? graph.EdgeCount : activeEdges.Count) == 0)
			{
				return num > 1;
			}
			HashSet<int> hashSet = new HashSet<int>();
			Stack<int> stack = new Stack<int>();
			int num2 = ((activeVertices == null) ? 0 : activeVertices.First<int>());
			hashSet.Add(num2);
			stack.Push(num2);
			do
			{
				int num3 = stack.Pop();
				IReadOnlyList<RawGraphArc> arcsFromVertex = graph.GetArcsFromVertex(num3);
				for (int i = 0; i < arcsFromVertex.Count; i++)
				{
					RawGraphArc rawGraphArc = arcsFromVertex[i];
					if ((activeEdges == null || activeEdges.Contains(rawGraphArc.EdgeId)) && (activeVertices == null || activeVertices.Contains(rawGraphArc.TargetId)) && hashSet.Add(rawGraphArc.TargetId))
					{
						stack.Push(rawGraphArc.TargetId);
					}
				}
			}
			while (stack.Count > 0);
			return hashSet.Count != num;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00009068 File Offset: 0x00007268
		public static MutableWeightedGraph<TVertex, TEdgeInfo> Filter<TVertex, TEdgeInfo>(this IWeightedGraph<TVertex, TEdgeInfo> graph, ISet<TVertex> selectedVertices)
		{
			int vertexCount = graph.RawGraph.VertexCount;
			int edgeCount = graph.RawGraph.EdgeCount;
			MutableWeightedGraph<TVertex, TEdgeInfo> mutableWeightedGraph = MutableWeightedGraph<TVertex, TEdgeInfo>.CreateUndirectedGraph(vertexCount, edgeCount, null);
			for (int i = 0; i < vertexCount; i++)
			{
				TVertex vertex = graph.GetVertex(i);
				if (selectedVertices.Contains(vertex))
				{
					mutableWeightedGraph.AddVertex(vertex, graph.RawGraph.GetVertexCost(i));
				}
			}
			for (int j = 0; j < edgeCount; j++)
			{
				int num;
				int num2;
				graph.RawGraph.GetEndpoints(j, out num, out num2);
				TVertex vertex2 = graph.GetVertex(num);
				TVertex vertex3 = graph.GetVertex(num2);
				if (mutableWeightedGraph.HasVertex(vertex2) && mutableWeightedGraph.HasVertex(vertex3))
				{
					mutableWeightedGraph.AddEdge(vertex2, vertex3, graph.GetEdgeInfo(j), graph.RawGraph.GetEdgeCost(j));
				}
			}
			return mutableWeightedGraph;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00009134 File Offset: 0x00007334
		public static string ToDotGraphString(this IRawWeightedGraph graph, IDotGraphCustomization dot = null, bool isDirectedGraph = false, Func<int, string> getVertexName = null, Func<int, string> getEdgeName = null)
		{
			if (dot == null)
			{
				dot = (graph as IDotGraphCustomization) ?? DefaultDotGraph.DefaultRawDotGraph;
			}
			return GraphExtensions.WriteGraphString(graph, dot, isDirectedGraph, getVertexName, getEdgeName);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00009155 File Offset: 0x00007355
		public static string ToDotGraphString<TVertex, TEdgeInfo>(this IWeightedGraph<TVertex, TEdgeInfo> graph, IDotGraphCustomization dot = null, bool isDirectedGraph = false, Func<int, string> getVertexName = null, Func<int, string> getEdgeName = null)
		{
			if (dot == null)
			{
				dot = (graph as IDotGraphCustomization) ?? new DefaultDotGraph<TVertex, TEdgeInfo>(graph);
			}
			return GraphExtensions.WriteGraphString(graph.RawGraph, dot, isDirectedGraph, getVertexName, getEdgeName);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000917C File Offset: 0x0000737C
		private static string WriteGraphString(IRawWeightedGraph graph, IDotGraphCustomization dot, bool isDirectedGraph, Func<int, string> getVertexName, Func<int, string> getEdgeName)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			stringWriter.WriteLine("{0} G {{", isDirectedGraph ? "digraph" : "graph");
			string text = GraphExtensions.FormatDotAttributes(dot.GetGraphAttributes());
			if (text != null)
			{
				stringWriter.Write("graph");
				stringWriter.Write(text);
				stringWriter.WriteLine();
				stringWriter.WriteLine();
			}
			List<string> list = new List<string>(graph.VertexCount + graph.EdgeCount);
			string[] array = new string[graph.VertexCount];
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < graph.VertexCount; i++)
			{
				double vertexCost = graph.GetVertexCost(i);
				string text2 = ((getVertexName == null) ? i.ToStringInvariant() : getVertexName(i));
				string text3 = StringUtil.MakeUniqueName(string.Concat(new string[]
				{
					text2,
					":",
					dot.GetVertexName(i),
					":",
					vertexCost.ToStringInvariant("#.##########")
				}), hashSet);
				array[i] = text3;
				hashSet.Add(text3);
				list.Add(StringUtil.FormatInvariant("\"{0}\"{1}", text3, GraphExtensions.FormatDotAttributes(dot.GetVertexAttributes(i))));
			}
			for (int j = 0; j < graph.EdgeCount; j++)
			{
				int num;
				int num2;
				graph.GetEndpoints(j, out num, out num2);
				if (dot.ComparePrecedence(num, num2) > 0)
				{
					Util.Swap<int>(ref num, ref num2);
				}
				double edgeCost = graph.GetEdgeCost(j);
				string text4 = ((getEdgeName == null) ? j.ToStringInvariant() : getEdgeName(j));
				string text5 = GraphExtensions.FormatDotAttributes(dot.GetEdgeAttributes(j).Concat(Util.ToKeyValuePair<string, string>("label", string.Concat(new string[]
				{
					"\"",
					text4,
					":",
					edgeCost.ToStringInvariant("#.##########"),
					"\""
				}))));
				list.Add(StringUtil.FormatInvariant("\"{0}\" {1} \"{2}\"{3}", new object[]
				{
					array[num],
					isDirectedGraph ? "->" : "--",
					array[num2],
					text5
				}));
			}
			if (dot.SortContent)
			{
				list.Sort(StringComparer.OrdinalIgnoreCase);
			}
			for (int k = 0; k < list.Count; k++)
			{
				stringWriter.WriteLine(list[k]);
			}
			stringWriter.Write("}");
			return stringWriter.ToString();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000093EC File Offset: 0x000075EC
		private static string FormatDotAttributes(IEnumerable<KeyValuePair<string, string>> dotAttrs)
		{
			if (dotAttrs == null)
			{
				return null;
			}
			string text = string.Join(",", dotAttrs.Select((KeyValuePair<string, string> a) => a.Key + '=' + a.Value));
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return StringUtil.FormatInvariant("  [{0}]", text);
		}
	}
}
