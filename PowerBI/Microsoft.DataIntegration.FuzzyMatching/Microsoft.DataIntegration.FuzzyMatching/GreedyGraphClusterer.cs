using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class GreedyGraphClusterer
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002554 File Offset: 0x00000754
		public GreedyGraphClusterer(Graph graph, int maxClusterSize)
		{
			int numNodes = graph.GetNumNodes();
			this.nodes = new GreedyGraphClusterer.Node[numNodes];
			Random random = new Random(4);
			for (int i = 0; i < numNodes; i++)
			{
				this.nodes[i].numNeighbors = 0;
				this.nodes[i].rand = random.Next();
				this.nodes[i].clusterId = -1;
			}
			this.maxClusterSize = maxClusterSize;
			this.graph = graph;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025D8 File Offset: 0x000007D8
		public void Cluster(ITokenIdProvider tokenIdProvider, int domainId, ITokenToClusterMap tokenClusterMap)
		{
			this.BuildHeap();
			int num = tokenIdProvider.CreateTokenId(domainId);
			int num2 = 0;
			while (this.pqSize > 0)
			{
				int num3 = this.priorityQueue[1];
				this.nodes[num3].clusterId = num;
				tokenClusterMap.AddTokenClusterMapping(num3, num);
				num2++;
				this.nodes[num3].pqId = -1;
				int[] array = this.priorityQueue;
				int num4 = 1;
				int[] array2 = this.priorityQueue;
				int num5 = this.pqSize;
				this.pqSize = num5 - 1;
				array[num4] = array2[num5];
				this.nodes[this.priorityQueue[1]].pqId = 1;
				this.Heapify(1);
				for (int i = 0; i < this.graph.GetDegree(num3); i++)
				{
					int neighbor = this.graph.GetNeighbor(num3, i);
					if (this.nodes[neighbor].clusterId == -1)
					{
						GreedyGraphClusterer.Node[] array3 = this.nodes;
						int num6 = neighbor;
						array3[num6].numNeighbors = array3[num6].numNeighbors + 1;
						int num7 = this.nodes[neighbor].pqId / 2;
						while (num7 > 0 && this.Heapify(num7))
						{
							num7 /= 2;
						}
					}
				}
				if (num2 == this.maxClusterSize || this.nodes[this.priorityQueue[1]].numNeighbors == 0)
				{
					this.ResetNumNeighbors(1);
					num = tokenIdProvider.CreateTokenId(domainId);
					num2 = 0;
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002738 File Offset: 0x00000938
		private bool HasHigherPriority(int n1, int n2)
		{
			return this.nodes[this.priorityQueue[n1]].numNeighbors > this.nodes[this.priorityQueue[n2]].numNeighbors || (this.nodes[this.priorityQueue[n1]].numNeighbors == this.nodes[this.priorityQueue[n2]].numNeighbors && this.nodes[this.priorityQueue[n1]].rand >= this.nodes[this.priorityQueue[n2]].rand);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000027E4 File Offset: 0x000009E4
		private bool Heapify(int node)
		{
			bool flag = false;
			int i = node * 2;
			int num = i + 1;
			while (i <= this.pqSize)
			{
				if (num > this.pqSize || this.HasHigherPriority(i, num))
				{
					if (this.HasHigherPriority(node, i))
					{
						break;
					}
					int num2 = this.priorityQueue[node];
					this.priorityQueue[node] = this.priorityQueue[i];
					this.priorityQueue[i] = num2;
					this.nodes[this.priorityQueue[node]].pqId = node;
					this.nodes[this.priorityQueue[i]].pqId = i;
					node = i;
					flag = true;
				}
				else
				{
					if (this.HasHigherPriority(node, num))
					{
						break;
					}
					int num3 = this.priorityQueue[node];
					this.priorityQueue[node] = this.priorityQueue[num];
					this.priorityQueue[num] = num3;
					this.nodes[this.priorityQueue[node]].pqId = node;
					this.nodes[this.priorityQueue[num]].pqId = num;
					node = num;
					flag = true;
				}
				i = node * 2;
				num = i + 1;
			}
			return flag;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000028F8 File Offset: 0x00000AF8
		private void BuildHeap()
		{
			this.priorityQueue = new int[this.nodes.Length + 1];
			this.pqSize = 0;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.graph.GetDegree(i) == 0)
				{
					this.nodes[i].pqId = -1;
				}
				else
				{
					this.pqSize++;
					this.priorityQueue[this.pqSize] = i;
					this.nodes[i].pqId = this.pqSize;
				}
			}
			for (int j = this.pqSize; j > 0; j--)
			{
				this.Heapify(j);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000029A4 File Offset: 0x00000BA4
		private void ResetNumNeighbors(int node)
		{
			int num = this.priorityQueue[node];
			if (this.nodes[num].numNeighbors == 0)
			{
				return;
			}
			this.nodes[num].numNeighbors = 0;
			int num2 = node * 2;
			int num3 = num2 + 1;
			if (num2 <= this.pqSize)
			{
				this.ResetNumNeighbors(num2);
				if (num3 <= this.pqSize)
				{
					this.ResetNumNeighbors(num3);
				}
			}
			this.Heapify(node);
		}

		// Token: 0x04000007 RID: 7
		private Graph graph;

		// Token: 0x04000008 RID: 8
		private GreedyGraphClusterer.Node[] nodes;

		// Token: 0x04000009 RID: 9
		private int[] priorityQueue;

		// Token: 0x0400000A RID: 10
		private int pqSize;

		// Token: 0x0400000B RID: 11
		private int maxClusterSize;

		// Token: 0x0200011C RID: 284
		[Serializable]
		private struct Node
		{
			// Token: 0x04000479 RID: 1145
			public int numNeighbors;

			// Token: 0x0400047A RID: 1146
			public int rand;

			// Token: 0x0400047B RID: 1147
			public int pqId;

			// Token: 0x0400047C RID: 1148
			public int clusterId;
		}
	}
}
