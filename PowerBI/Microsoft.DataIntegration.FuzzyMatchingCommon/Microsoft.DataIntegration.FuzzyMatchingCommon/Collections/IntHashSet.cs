using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200008A RID: 138
	[Serializable]
	public class IntHashSet
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00021DCC File Offset: 0x0001FFCC
		public IntHashSet(int suggestedCapacity)
		{
			int num = (int)((float)suggestedCapacity * 2f);
			this.m_hashBucketMask = 1;
			while (this.m_hashBucketMask < num && this.m_hashBucketMask > 0)
			{
				this.m_hashBucketMask = (this.m_hashBucketMask << 1) | 1;
			}
			if (this.m_hashBucketMask <= 0 || this.m_hashBucketMask + 1 <= 0)
			{
				throw new InvalidOperationException();
			}
			this.m_hashBuckets = new IntHashSet.HashEntry[this.m_hashBucketMask + 1];
			this.m_curTimestamp = 1;
			this.m_fallbackDictionary = new Dictionary<int, int>();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00021E54 File Offset: 0x00020054
		public void Add(int elem)
		{
			int num = Utilities.GetHashCode(elem) & this.m_hashBucketMask;
			if (this.m_hashBuckets[num].ts < this.m_curTimestamp)
			{
				this.m_hashBuckets[num].key = elem;
				this.m_hashBuckets[num].ts = this.m_curTimestamp;
				return;
			}
			this.m_fallbackDictionary[elem] = 1;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00021EC0 File Offset: 0x000200C0
		public void AddRange(IEnumerable<int> items)
		{
			foreach (int num in items)
			{
				this.Add(num);
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00021F08 File Offset: 0x00020108
		public void Clear()
		{
			this.m_curTimestamp++;
			if (this.m_curTimestamp == 2147483647)
			{
				for (int i = 0; i < this.m_hashBuckets.Length; i++)
				{
					this.m_hashBuckets[i].ts = 0;
				}
				this.m_curTimestamp = 1;
			}
			this.m_fallbackDictionary.Clear();
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00021F68 File Offset: 0x00020168
		public bool Contains(int elem)
		{
			int num = Utilities.GetHashCode(elem) & this.m_hashBucketMask;
			return this.m_hashBuckets[num].ts >= this.m_curTimestamp && (this.m_hashBuckets[num].key == elem || this.m_fallbackDictionary.ContainsKey(elem));
		}

		// Token: 0x0400011F RID: 287
		protected IntHashSet.HashEntry[] m_hashBuckets;

		// Token: 0x04000120 RID: 288
		protected readonly int m_hashBucketMask;

		// Token: 0x04000121 RID: 289
		protected int m_curTimestamp;

		// Token: 0x04000122 RID: 290
		protected const float GrowthFactor = 2f;

		// Token: 0x04000123 RID: 291
		protected Dictionary<int, int> m_fallbackDictionary;

		// Token: 0x0200012F RID: 303
		[Serializable]
		protected struct HashEntry
		{
			// Token: 0x0400030F RID: 783
			public int key;

			// Token: 0x04000310 RID: 784
			public int ts;
		}
	}
}
