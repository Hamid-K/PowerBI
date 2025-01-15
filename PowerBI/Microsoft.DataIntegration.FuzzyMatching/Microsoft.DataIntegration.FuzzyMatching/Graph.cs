using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal class Graph
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002463 File Offset: 0x00000663
		public Graph()
		{
			this.neighbor = new List<List<int>>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002476 File Offset: 0x00000676
		public void Reset()
		{
			this.neighbor.Clear();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002484 File Offset: 0x00000684
		public void AddEdge(int a, int b)
		{
			int num = ((a > b) ? a : b);
			while (this.neighbor.Count <= num)
			{
				this.neighbor.Add(new List<int>());
			}
			for (int i = 0; i < this.neighbor[a].Count; i++)
			{
				if (b == this.neighbor[a][i])
				{
					return;
				}
			}
			this.neighbor[a].Add(b);
			this.neighbor[b].Add(a);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002510 File Offset: 0x00000710
		public int GetDegree(int node)
		{
			if (node < this.neighbor.Count)
			{
				return this.neighbor[node].Count;
			}
			return 0;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002533 File Offset: 0x00000733
		public int GetNeighbor(int node, int idx)
		{
			return this.neighbor[node][idx];
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002547 File Offset: 0x00000747
		public int GetNumNodes()
		{
			return this.neighbor.Count;
		}

		// Token: 0x04000006 RID: 6
		private List<List<int>> neighbor;
	}
}
