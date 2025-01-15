using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000040 RID: 64
	public sealed class DijkstraAlgorithm
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x00008698 File Offset: 0x00006898
		public DijkstraAlgorithm(IRawWeightedGraph graph, PriorityQueue<DijkstraAlgorithm.Node> queue = null)
		{
			this._graph = graph;
			int vertexCount = graph.VertexCount;
			this.ShortestPathProvider = new DijkstraAlgorithm.SingleSourceShortestPathProvider(vertexCount);
			this._queue = queue ?? new PriorityQueue<DijkstraAlgorithm.Node>(2 * vertexCount, DijkstraAlgorithm.NodeComparer.Instance);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000086DD File Offset: 0x000068DD
		internal DijkstraAlgorithm.SingleSourceShortestPathProvider ShortestPathProvider { get; }

		// Token: 0x060002F6 RID: 758 RVA: 0x000086E8 File Offset: 0x000068E8
		public void FindShortestPaths(int source, int k = 1)
		{
			if (k <= 0)
			{
				throw Contract.ExceptParam("k");
			}
			int vertexCount = this._graph.VertexCount;
			this.ShortestPathProvider.InitializeSource(source);
			this._queue.Clear();
			for (int i = 0; i < vertexCount; i++)
			{
				this._queue.Push(new DijkstraAlgorithm.Node(i, this.ShortestPathProvider.GetCost(i)));
			}
			while (this._queue.Count > 0)
			{
				DijkstraAlgorithm.Node node = this._queue.Pop();
				double cost = this.ShortestPathProvider.GetCost(node.Id);
				if (cost != 1.7976931348623157E+308)
				{
					foreach (RawGraphArc rawGraphArc in this._graph.GetArcsFromVertex(node.Id))
					{
						int targetId = rawGraphArc.TargetId;
						double edgeCost = this._graph.GetEdgeCost(rawGraphArc.EdgeId);
						double num = cost + edgeCost;
						if (num < this.ShortestPathProvider.GetCost(targetId))
						{
							this.ShortestPathProvider.SetCost(targetId, num);
							this.ShortestPathProvider.SetPreviousNode(targetId, node.Id);
							this._queue.Push(new DijkstraAlgorithm.Node(targetId, num));
						}
					}
				}
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000884C File Offset: 0x00006A4C
		public double GetCost(int target)
		{
			return this.ShortestPathProvider.GetCost(target);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000885A File Offset: 0x00006A5A
		internal IReadOnlyList<double> GetCosts()
		{
			return this.ShortestPathProvider.Costs;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00008867 File Offset: 0x00006A67
		internal IReadOnlyList<int> GetPreviousNodes()
		{
			return this.ShortestPathProvider.PreviousNodes;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008874 File Offset: 0x00006A74
		public bool TryGetPath(int source, int target, out IReadOnlyList<int> path)
		{
			return this.ShortestPathProvider.TryGetPath(source, target, out path);
		}

		// Token: 0x0400009E RID: 158
		private readonly IRawWeightedGraph _graph;

		// Token: 0x0400009F RID: 159
		private readonly PriorityQueue<DijkstraAlgorithm.Node> _queue;

		// Token: 0x020000B7 RID: 183
		public struct Node
		{
			// Token: 0x060005B1 RID: 1457 RVA: 0x0000EDC9 File Offset: 0x0000CFC9
			internal Node(int id, double cost)
			{
				this.Id = id;
				this.Cost = cost;
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
			internal readonly int Id { get; }

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000EDE1 File Offset: 0x0000CFE1
			internal readonly double Cost { get; }
		}

		// Token: 0x020000B8 RID: 184
		public sealed class NodeComparer : IComparer<DijkstraAlgorithm.Node>
		{
			// Token: 0x060005B4 RID: 1460 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
			private NodeComparer()
			{
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
			public int Compare(DijkstraAlgorithm.Node x, DijkstraAlgorithm.Node y)
			{
				return x.Cost.CompareTo(y.Cost);
			}

			// Token: 0x040001D5 RID: 469
			public static readonly DijkstraAlgorithm.NodeComparer Instance = new DijkstraAlgorithm.NodeComparer();
		}

		// Token: 0x020000B9 RID: 185
		public sealed class SingleSourceShortestPathProvider
		{
			// Token: 0x060005B7 RID: 1463 RVA: 0x0000EE23 File Offset: 0x0000D023
			public SingleSourceShortestPathProvider(int vertexCount)
			{
				this._previousNodes = new int[vertexCount];
				this._costs = new double[vertexCount];
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000EE43 File Offset: 0x0000D043
			internal IReadOnlyList<int> PreviousNodes
			{
				get
				{
					return this._previousNodes;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000EE4B File Offset: 0x0000D04B
			internal IReadOnlyList<double> Costs
			{
				get
				{
					return this._costs;
				}
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x0000EE53 File Offset: 0x0000D053
			public void SetCost(int target, double cost)
			{
				this._costs[target] = cost;
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x0000EE5E File Offset: 0x0000D05E
			public double GetCost(int target)
			{
				return this._costs[target];
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x0000EE68 File Offset: 0x0000D068
			public void SetPreviousNode(int source, int value)
			{
				this._previousNodes[source] = value;
			}

			// Token: 0x060005BD RID: 1469 RVA: 0x0000EE73 File Offset: 0x0000D073
			public int GetPreviousNode(int source)
			{
				return this._previousNodes[source];
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0000EE80 File Offset: 0x0000D080
			public void InitializeSource(int source)
			{
				int num = this._costs.Length;
				for (int i = 0; i < num; i++)
				{
					this._previousNodes[i] = -1;
					this._costs[i] = ((i == source) ? 0.0 : double.MaxValue);
				}
			}

			// Token: 0x060005BF RID: 1471 RVA: 0x0000EECC File Offset: 0x0000D0CC
			public bool TryGetPath(int source, int target, out IReadOnlyList<int> path)
			{
				List<int> list = null;
				while (target != source)
				{
					target = this.PreviousNodes[target];
					if (target == -1)
					{
						path = null;
						return false;
					}
					if (target != source)
					{
						if (list == null)
						{
							list = new List<int>();
						}
						list.Add(target);
					}
				}
				if (list == null)
				{
					path = Util.EmptyReadOnlyCollection<int>();
				}
				else
				{
					list.Reverse();
					path = list;
				}
				return true;
			}

			// Token: 0x040001D6 RID: 470
			private const int UndefinedNode = -1;

			// Token: 0x040001D7 RID: 471
			private readonly double[] _costs;

			// Token: 0x040001D8 RID: 472
			private readonly int[] _previousNodes;
		}
	}
}
