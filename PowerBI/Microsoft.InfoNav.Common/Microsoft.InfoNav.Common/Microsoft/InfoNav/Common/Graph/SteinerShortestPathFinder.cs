using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.InfoNav.Common.Graph
{
	// Token: 0x02000084 RID: 132
	[ImmutableObject(true)]
	public sealed class SteinerShortestPathFinder : IShortestPathFinder
	{
		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C781 File Offset: 0x0000A981
		private SteinerShortestPathFinder()
		{
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C78C File Offset: 0x0000A98C
		public ShortestPathSolution Solve(ShortestPathProblem problem)
		{
			int vertexCount = problem.Graph.VertexCount;
			VertexHeap vertexHeap = new VertexHeap(vertexCount);
			int[] array = new int[vertexCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -2;
			}
			double[] array2 = new double[vertexCount];
			HashSet<int> hashSet = new HashSet<int>();
			bool flag = false;
			foreach (int num in problem.SearchRoots)
			{
				if (SteinerShortestPathFinder.SolveWithRoot(problem, vertexHeap, num, hashSet, array, array2))
				{
					break;
				}
				flag = true;
			}
			return new ShortestPathSolution(hashSet, flag);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C838 File Offset: 0x0000AA38
		private static bool SolveWithRoot(ShortestPathProblem problem, VertexHeap heap, int root, ISet<int> solutionEdges, int[] parentArc, double[] distance)
		{
			IRawWeightedGraph graph = problem.Graph;
			heap.Insert(root, double.Epsilon);
			parentArc[root] = -1;
			distance[root] = -1.0;
			int i = (problem.SearchTerminals.Contains(root) ? 1 : 0);
			int count = problem.SearchTerminals.Count;
			while (i < count)
			{
				if (heap.IsEmpty())
				{
					return false;
				}
				int num;
				double epsilon;
				heap.RemoveFirst(out num, out epsilon);
				if (parentArc[num] >= 0 && problem.SearchTerminals.Contains(num))
				{
					int num2 = num;
					double num3 = 0.0;
					do
					{
						int num4 = parentArc[num2];
						parentArc[num2] = -1;
						distance[num2] = double.Epsilon;
						if (num2 != num)
						{
							heap.Insert(num2, double.Epsilon);
						}
						if (!solutionEdges.Add(num4))
						{
							return false;
						}
						num3 += graph.GetEdgeCost(num4) + graph.GetVertexCost(num2);
						num2 = graph.GetOtherEndpoint(num4, num2);
					}
					while (parentArc[num2] >= 0);
					epsilon = double.Epsilon;
					i++;
					if (i != count)
					{
						goto IL_010B;
					}
					break;
				}
				IL_010B:
				IReadOnlyList<RawGraphArc> arcsFromVertex = graph.GetArcsFromVertex(num);
				for (int j = 0; j < arcsFromVertex.Count; j++)
				{
					RawGraphArc rawGraphArc = arcsFromVertex[j];
					int targetId = rawGraphArc.TargetId;
					if (parentArc[targetId] != -1)
					{
						double edgeCost = graph.GetEdgeCost(rawGraphArc.EdgeId);
						double vertexCost = graph.GetVertexCost(targetId);
						double num5 = edgeCost + vertexCost + epsilon;
						if (num5 < distance[targetId] || distance[targetId] == 0.0)
						{
							distance[targetId] = num5;
							heap.Insert(targetId, num5);
							parentArc[targetId] = rawGraphArc.EdgeId;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000C9EC File Offset: 0x0000ABEC
		[Conditional("DEBUG")]
		public static void VerifyPathCostConsistency(double a, double b)
		{
		}

		// Token: 0x0400011A RID: 282
		private const double CostEpsilon = 1E-05;

		// Token: 0x0400011B RID: 283
		private const int EdgeNotVisited = -2;

		// Token: 0x0400011C RID: 284
		private const int EdgeAlreadyInSolution = -1;

		// Token: 0x0400011D RID: 285
		public static readonly SteinerShortestPathFinder Instance = new SteinerShortestPathFinder();
	}
}
