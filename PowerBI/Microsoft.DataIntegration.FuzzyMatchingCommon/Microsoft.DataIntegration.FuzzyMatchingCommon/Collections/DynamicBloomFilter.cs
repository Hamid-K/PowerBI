using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	public class DynamicBloomFilter<TKey> : IMemoryUsage
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x0001C3BC File Offset: 0x0001A5BC
		public DynamicBloomFilter(double falsePositiveProb)
			: this(falsePositiveProb, (int)Math.Pow(2.0, 16.0))
		{
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001C3DD File Offset: 0x0001A5DD
		public DynamicBloomFilter(double falsePositiveProb, int initialNumElements)
			: this(falsePositiveProb, initialNumElements, new BloomFilter<TKey>.GetHashCodeDelegate(DynamicBloomFilter<TKey>.GetHashCodeDefault))
		{
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001C3F3 File Offset: 0x0001A5F3
		public DynamicBloomFilter(double falsePositiveProb, int initialNumElements, BloomFilter<TKey>.GetHashCodeDelegate hash)
		{
			this.m_hash0 = hash;
			this.Init(falsePositiveProb, initialNumElements, hash, new BloomFilter<TKey>.GetHashCodeDelegate(this.GetHashCode2));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001C417 File Offset: 0x0001A617
		public DynamicBloomFilter(double falsePositiveProb, int initialNumElements, BloomFilter<TKey>.GetHashCodeDelegate hash1, BloomFilter<TKey>.GetHashCodeDelegate hash2)
		{
			this.Init(falsePositiveProb, initialNumElements, hash1, hash2);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001C42C File Offset: 0x0001A62C
		public void Clear()
		{
			for (int i = 0; i < this.m_filters.Length; i++)
			{
				this.m_filters[i].Clear();
			}
			this.m_numElements = 0;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001C460 File Offset: 0x0001A660
		private void Init(double falsePositiveProb, int initialNumElements, BloomFilter<TKey>.GetHashCodeDelegate hash1, BloomFilter<TKey>.GetHashCodeDelegate hash2)
		{
			this.m_falsePositiveProb = falsePositiveProb;
			this.m_initialNumElements = initialNumElements;
			this.m_hash1 = hash1;
			this.m_hash2 = hash2;
			this.m_filters = new BloomFilter<TKey>[1];
			this.m_filters[0] = new BloomFilter<TKey>(initialNumElements, falsePositiveProb, this.m_hash1, this.m_hash2);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001C4B1 File Offset: 0x0001A6B1
		private int GetHashCode1(TKey key)
		{
			return Utilities.GetHashCode(101, this.m_hash0(key));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001C4C6 File Offset: 0x0001A6C6
		private int GetHashCode2(TKey key)
		{
			return Utilities.GetHashCode(179, this.m_hash0(key));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001C4DE File Offset: 0x0001A6DE
		private static int GetHashCodeDefault(TKey key)
		{
			return key.GetHashCode();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001C4F0 File Offset: 0x0001A6F0
		public long MemoryUsage
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < this.m_filters.Length; i++)
				{
					num += this.m_filters[i].MemoryUsage;
				}
				return num;
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001C524 File Offset: 0x0001A724
		public void Insert(TKey obj)
		{
			if (!this.Contains(obj))
			{
				if (this.m_numElements == this.m_initialNumElements * (int)Math.Pow(2.0, (double)(this.m_filters.Length - 1)))
				{
					Array.Resize<BloomFilter<TKey>>(ref this.m_filters, this.m_filters.Length + 1);
					this.m_filters[this.m_filters.Length - 1] = new BloomFilter<TKey>(this.m_initialNumElements * (int)Math.Pow(2.0, (double)(this.m_filters.Length - 1)), this.m_falsePositiveProb, this.m_hash1, this.m_hash2);
					this.m_numElements = 0;
				}
				this.m_filters[this.m_filters.Length - 1].Insert(obj);
				this.m_numElements++;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		public bool Contains(TKey obj)
		{
			for (int i = this.m_filters.Length - 1; i >= 0; i--)
			{
				if (this.m_filters[i].Contains(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000CA RID: 202
		private double m_falsePositiveProb;

		// Token: 0x040000CB RID: 203
		private int m_initialNumElements;

		// Token: 0x040000CC RID: 204
		private BloomFilter<TKey>.GetHashCodeDelegate m_hash0;

		// Token: 0x040000CD RID: 205
		private BloomFilter<TKey>.GetHashCodeDelegate m_hash1;

		// Token: 0x040000CE RID: 206
		private BloomFilter<TKey>.GetHashCodeDelegate m_hash2;

		// Token: 0x040000CF RID: 207
		private BloomFilter<TKey>[] m_filters;

		// Token: 0x040000D0 RID: 208
		private int m_numElements;
	}
}
