using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	internal sealed class MinWtHashTable
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00021D6F File Offset: 0x0001FF6F
		public MinWtHashTable(int initialCapacity)
		{
			this.m_htkeyComparer = new MinWtHashTable.EqComparer();
			this.m_minWtHT = new Dictionary<MinWtHashTable.HTKey, MinWtHashTable.HTValue>(this.m_htkeyComparer);
			this.m_intvecPool = new ObjectVector<IntVector>(initialCapacity);
			this.m_selectedIds = new IntHashSet(100);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00021DAC File Offset: 0x0001FFAC
		public void Add(int pos, int len, IntVector rhsSet, int tmId, int wt)
		{
			MinWtHashTable.HTKey htkey;
			htkey.pos = pos;
			htkey.len = len;
			htkey.rhsSet = rhsSet;
			MinWtHashTable.HTValue htvalue;
			if (!this.m_minWtHT.TryGetValue(htkey, ref htvalue))
			{
				htvalue.minWt = wt;
				htvalue.tranMatchId = tmId;
				IntVector intVector = this.m_intvecPool.Add();
				intVector.Clear();
				for (int i = 0; i < rhsSet.Count; i++)
				{
					intVector.Add(rhsSet[i]);
				}
				htkey.rhsSet = intVector;
				this.m_minWtHT.Add(htkey, htvalue);
				return;
			}
			if (htvalue.minWt <= wt)
			{
				return;
			}
			htvalue.minWt = wt;
			htvalue.tranMatchId = tmId;
			this.m_minWtHT[htkey] = htvalue;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00021E63 File Offset: 0x00020063
		public void Clear()
		{
			this.m_minWtHT.Clear();
			this.m_intvecPool.Clear();
			this.m_selectedIds.Clear();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00021E86 File Offset: 0x00020086
		public void Open()
		{
			this.m_selectedIds.AddRange(Enumerable.Select<MinWtHashTable.HTValue, int>(this.m_minWtHT.Values, (MinWtHashTable.HTValue v) => v.tranMatchId));
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00021EC2 File Offset: 0x000200C2
		public bool ContainsId(int Id)
		{
			return this.m_selectedIds.Contains(Id);
		}

		// Token: 0x04000314 RID: 788
		private Dictionary<MinWtHashTable.HTKey, MinWtHashTable.HTValue> m_minWtHT;

		// Token: 0x04000315 RID: 789
		private MinWtHashTable.EqComparer m_htkeyComparer;

		// Token: 0x04000316 RID: 790
		private IntHashSet m_selectedIds;

		// Token: 0x04000317 RID: 791
		private ObjectVector<IntVector> m_intvecPool;

		// Token: 0x02000176 RID: 374
		[Serializable]
		private struct HTKey
		{
			// Token: 0x06000CF5 RID: 3317 RVA: 0x00037794 File Offset: 0x00035994
			public override int GetHashCode()
			{
				int num = 0;
				num += this.pos;
				num += ~(num << 15);
				num ^= num >> 10;
				num += num << 3;
				num ^= num >> 6;
				num += ~(num << 11);
				num ^= num >> 16;
				num += this.len;
				num += ~(num << 15);
				num ^= num >> 10;
				num += num << 3;
				num ^= num >> 6;
				num += ~(num << 11);
				num ^= num >> 16;
				for (int i = 0; i < this.rhsSet.Count; i++)
				{
					num += this.rhsSet[i];
					num += ~(num << 15);
					num ^= num >> 10;
					num += num << 3;
					num ^= num >> 6;
					num += ~(num << 11);
					num ^= num >> 16;
				}
				return num;
			}

			// Token: 0x040005FC RID: 1532
			public int pos;

			// Token: 0x040005FD RID: 1533
			public int len;

			// Token: 0x040005FE RID: 1534
			public IntVector rhsSet;
		}

		// Token: 0x02000177 RID: 375
		[Serializable]
		private sealed class EqComparer : IEqualityComparer<MinWtHashTable.HTKey>
		{
			// Token: 0x06000CF6 RID: 3318 RVA: 0x0003785C File Offset: 0x00035A5C
			public bool Equals(MinWtHashTable.HTKey hx, MinWtHashTable.HTKey hy)
			{
				if (hx.pos != hy.pos || hx.len != hy.len)
				{
					return false;
				}
				if (hx.rhsSet.Count != hy.rhsSet.Count)
				{
					return false;
				}
				for (int i = 0; i < hx.rhsSet.Count; i++)
				{
					int num = 0;
					while (num < hy.rhsSet.Count && hy.rhsSet[num] != hx.rhsSet[i])
					{
						num++;
					}
					if (num == hy.rhsSet.Count)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06000CF7 RID: 3319 RVA: 0x000378F8 File Offset: 0x00035AF8
			public int GetHashCode(MinWtHashTable.HTKey hx)
			{
				return hx.GetHashCode();
			}
		}

		// Token: 0x02000178 RID: 376
		[Serializable]
		private struct HTValue
		{
			// Token: 0x040005FF RID: 1535
			public int tranMatchId;

			// Token: 0x04000600 RID: 1536
			public int minWt;
		}
	}
}
