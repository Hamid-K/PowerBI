using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x0200000C RID: 12
	internal class UnionMerge
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002EE4 File Offset: 0x000010E4
		internal UnionMerge(int count)
		{
			this.parents = new List<int>(count);
			this.ranks = new List<int>(count);
			for (int i = 0; i < count; i++)
			{
				this.parents.Add(i);
				this.ranks.Add(1);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002F34 File Offset: 0x00001134
		internal int GetRoot(int x)
		{
			int num = this.parents[x];
			while (this.parents[num] != num)
			{
				num = this.parents[num];
			}
			while (this.parents[x] != x)
			{
				int num2 = this.parents[x];
				this.parents[x] = num;
				x = num2;
			}
			return num;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F9C File Offset: 0x0000119C
		internal void Union(int x, int y)
		{
			int root = this.GetRoot(x);
			int root2 = this.GetRoot(y);
			if (root == root2)
			{
				return;
			}
			List<int> list;
			int num;
			if (this.ranks[root] >= this.ranks[root2])
			{
				this.parents[root2] = root;
				list = this.ranks;
				num = root;
				list[num] += this.ranks[root2];
				return;
			}
			this.parents[root] = root2;
			list = this.ranks;
			num = root2;
			list[num] += this.ranks[root];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000303B File Offset: 0x0000123B
		internal bool IsSame(int x, int y)
		{
			return this.GetRoot(x) == this.GetRoot(y);
		}

		// Token: 0x0400001D RID: 29
		private List<int> parents;

		// Token: 0x0400001E RID: 30
		private List<int> ranks;
	}
}
