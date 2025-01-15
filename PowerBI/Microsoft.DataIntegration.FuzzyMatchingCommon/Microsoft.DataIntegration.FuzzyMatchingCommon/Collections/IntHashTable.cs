using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	internal sealed class IntHashTable : IMemoryUsage
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x0002219C File Offset: 0x0002039C
		public IntHashTable(int suggestedCapacity)
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
			this.m_hashBuckets = new IntHashTable.HashEntry[this.m_hashBucketMask + 1];
			this.m_curTimestamp = 1;
			this.m_fallbackDictionary = new Dictionary<int, int>();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00022223 File Offset: 0x00020423
		private static int GetHashCode(int key)
		{
			return Utilities.GetHashCode(key);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002222C File Offset: 0x0002042C
		public void Add(int key, int value)
		{
			int num = IntHashTable.GetHashCode(key) & this.m_hashBucketMask;
			if (this.m_hashBuckets[num].ts < this.m_curTimestamp)
			{
				this.m_hashBuckets[num].key = key;
				this.m_hashBuckets[num].value = value;
				this.m_hashBuckets[num].ts = this.m_curTimestamp;
				return;
			}
			if (this.m_hashBuckets[num].key == key)
			{
				throw new InvalidOperationException(Microsoft_DataIntegration_Common_Resources.HashKeyAlreadyPresent);
			}
			this.m_fallbackDictionary.Add(key, value);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000222C8 File Offset: 0x000204C8
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

		// Token: 0x06000611 RID: 1553 RVA: 0x00022328 File Offset: 0x00020528
		public bool ContainsKey(int key)
		{
			int num = IntHashTable.GetHashCode(key) & this.m_hashBucketMask;
			return this.m_hashBuckets[num].ts >= this.m_curTimestamp && (this.m_hashBuckets[num].key == key || this.m_fallbackDictionary.ContainsKey(key));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00022380 File Offset: 0x00020580
		public bool TryGetValue(int key, out int value)
		{
			int num = IntHashTable.GetHashCode(key) & this.m_hashBucketMask;
			if (this.m_hashBuckets[num].ts < this.m_curTimestamp)
			{
				value = 0;
				return false;
			}
			if (this.m_hashBuckets[num].key == key)
			{
				value = this.m_hashBuckets[num].value;
				return true;
			}
			return this.m_fallbackDictionary.TryGetValue(key, ref value);
		}

		// Token: 0x170000E1 RID: 225
		public int this[int key]
		{
			get
			{
				int num = IntHashTable.GetHashCode(key) & this.m_hashBucketMask;
				if (this.m_hashBuckets[num].ts < this.m_curTimestamp)
				{
					throw new InvalidOperationException(Microsoft_DataIntegration_Common_Resources.HashKeyNotPresent);
				}
				if (this.m_hashBuckets[num].key == key)
				{
					return this.m_hashBuckets[num].value;
				}
				return this.m_fallbackDictionary[key];
			}
			set
			{
				int num = IntHashTable.GetHashCode(key) & this.m_hashBucketMask;
				if (this.m_hashBuckets[num].ts < this.m_curTimestamp)
				{
					this.m_hashBuckets[num].key = key;
					this.m_hashBuckets[num].value = value;
					this.m_hashBuckets[num].ts = this.m_curTimestamp;
					return;
				}
				if (this.m_hashBuckets[num].key == key)
				{
					this.m_hashBuckets[num].value = value;
					return;
				}
				this.m_fallbackDictionary[key] = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00022508 File Offset: 0x00020708
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_hashBuckets.Length * 4 * 2 + this.m_fallbackDictionary.Count * 4 * 2);
			}
		}

		// Token: 0x04000129 RID: 297
		private const float GrowthFactor = 2f;

		// Token: 0x0400012A RID: 298
		private IntHashTable.HashEntry[] m_hashBuckets;

		// Token: 0x0400012B RID: 299
		private readonly int m_hashBucketMask;

		// Token: 0x0400012C RID: 300
		private int m_curTimestamp;

		// Token: 0x0400012D RID: 301
		private Dictionary<int, int> m_fallbackDictionary;

		// Token: 0x02000130 RID: 304
		[Serializable]
		private struct HashEntry
		{
			// Token: 0x04000311 RID: 785
			public int key;

			// Token: 0x04000312 RID: 786
			public int value;

			// Token: 0x04000313 RID: 787
			public int ts;
		}
	}
}
