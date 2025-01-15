using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000492 RID: 1170
	public static class GraphUtils
	{
		// Token: 0x06001A5A RID: 6746 RVA: 0x0004F5D4 File Offset: 0x0004D7D4
		public static HashSet<T> ReachableFrom<T, TCollection>(this IDictionary<T, TCollection> graph, T start) where TCollection : IEnumerable<T>
		{
			HashSet<T> hashSet = new HashSet<T>();
			Queue<T> queue = new Queue<T>(graph.Count);
			queue.Enqueue(start);
			hashSet.Add(start);
			while (queue.Count > 0)
			{
				T t = queue.Dequeue();
				TCollection tcollection = graph[t];
				foreach (T t2 in tcollection)
				{
					if (!hashSet.Contains(t2))
					{
						queue.Enqueue(t2);
						hashSet.Add(t2);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x0004F678 File Offset: 0x0004D878
		public static List<T> TopologicalSort<T>(this MultiValueDictionary<T, T> graph)
		{
			return graph.ToDictionary<T, IReadOnlyCollection<T>>().TopologicalSort<T, IReadOnlyCollection<T>>();
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x0004F688 File Offset: 0x0004D888
		public static List<T> TopologicalSort<T, TCollection>(this IDictionary<T, TCollection> graph) where TCollection : IEnumerable<T>
		{
			List<T> list = new List<T>();
			Dictionary<T, GraphUtils.VisitStatus> dictionary = new Dictionary<T, GraphUtils.VisitStatus>();
			HashSet<T> allNodes = graph.GetAllNodes<T, TCollection>();
			foreach (T t in allNodes)
			{
				dictionary[t] = GraphUtils.VisitStatus.None;
			}
			try
			{
				foreach (T t2 in allNodes)
				{
					if (dictionary[t2] == GraphUtils.VisitStatus.None)
					{
						list.AddRange(GraphUtils.TopologicalSortImpl<T, TCollection>(t2, graph, dictionary));
					}
				}
			}
			catch (GraphUtils.CycleException)
			{
				return null;
			}
			list.Reverse();
			return list;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x0004F75C File Offset: 0x0004D95C
		private static IEnumerable<T> TopologicalSortImpl<T, TCollection>(T node, IDictionary<T, TCollection> graph, IDictionary<T, GraphUtils.VisitStatus> visited) where TCollection : IEnumerable<T>
		{
			visited[node] = GraphUtils.VisitStatus.InProgress;
			TCollection tcollection;
			if (graph.TryGetValue(node, out tcollection))
			{
				foreach (T t in tcollection)
				{
					GraphUtils.VisitStatus visitStatus = visited[t];
					if (visitStatus != GraphUtils.VisitStatus.None)
					{
						if (visitStatus == GraphUtils.VisitStatus.InProgress)
						{
							throw new GraphUtils.CycleException();
						}
					}
					else
					{
						foreach (T t2 in GraphUtils.TopologicalSortImpl<T, TCollection>(t, graph, visited))
						{
							yield return t2;
						}
						IEnumerator<T> enumerator2 = null;
					}
				}
				IEnumerator<T> enumerator = null;
			}
			visited[node] = GraphUtils.VisitStatus.Processed;
			yield return node;
			yield break;
			yield break;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0004F77C File Offset: 0x0004D97C
		public static HashSet<T> GetAllNodes<T, TCollection>(this IDictionary<T, TCollection> graph) where TCollection : IEnumerable<T>
		{
			HashSet<T> hashSet = graph.Keys.ConvertToHashSet<T>();
			foreach (TCollection tcollection in graph.Values)
			{
				foreach (T t in tcollection)
				{
					hashSet.Add(t);
				}
			}
			return hashSet;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0004F810 File Offset: 0x0004DA10
		public static List<T> SpanningTree<T, TCollection>(this IDictionary<T, TCollection> graph) where TCollection : IEnumerable<T>
		{
			List<T> list = new List<T>();
			HashSet<T> visited = new HashSet<T>();
			HashSet<T> nodes = graph.GetAllNodes<T, TCollection>();
			List<T> list2 = graph.Keys.Where((T x) => graph.ReachableFrom(x).SetEquals(nodes)).ToList<T>();
			if (list2.IsEmpty<T>())
			{
				return null;
			}
			T t = list2.First<T>();
			list.Add(t);
			visited.Add(t);
			int num = 0;
			Func<T, bool> <>9__1;
			while (visited.Count != nodes.Count && num < list.Count)
			{
				TCollection tcollection = graph[list[num]];
				List<T> list3 = list;
				IEnumerable<T> enumerable = tcollection;
				Func<T, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (T x) => !visited.Contains(x));
				}
				list3.AddRange(enumerable.Where(func));
				visited.AddRange(tcollection);
				num++;
			}
			return list;
		}

		// Token: 0x02000493 RID: 1171
		private enum VisitStatus
		{
			// Token: 0x04000CFA RID: 3322
			None,
			// Token: 0x04000CFB RID: 3323
			InProgress,
			// Token: 0x04000CFC RID: 3324
			Processed
		}

		// Token: 0x02000494 RID: 1172
		[Serializable]
		private class CycleException : Exception
		{
		}
	}
}
