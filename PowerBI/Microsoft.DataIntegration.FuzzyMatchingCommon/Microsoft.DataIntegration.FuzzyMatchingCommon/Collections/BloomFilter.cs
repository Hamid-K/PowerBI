using System;
using System.Collections;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public class BloomFilter<TKey> : IMemoryUsage
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001A254 File Offset: 0x00018454
		public BloomFilter(int tableSize, int numElements)
		{
			this.Initialize(tableSize, (int)Math.Round(Math.Log(2.0) * (double)tableSize / (double)numElements));
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001A27D File Offset: 0x0001847D
		public BloomFilter(int numElements, double falsePositiveProb)
			: this(numElements, falsePositiveProb, new BloomFilter<TKey>.GetHashCodeDelegate(BloomFilter<TKey>.GetHashCodeDefault))
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001A293 File Offset: 0x00018493
		public BloomFilter(int numElements, double falsePositiveProb, BloomFilter<TKey>.GetHashCodeDelegate hash)
		{
			this.m_hash0 = hash;
			this.Init(numElements, falsePositiveProb, hash, new BloomFilter<TKey>.GetHashCodeDelegate(this.GetHashCode2));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001A2B7 File Offset: 0x000184B7
		public BloomFilter(int numElements, double falsePositiveProb, BloomFilter<TKey>.GetHashCodeDelegate hash1, BloomFilter<TKey>.GetHashCodeDelegate hash2)
		{
			this.Init(numElements, falsePositiveProb, hash1, hash2);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001A2CA File Offset: 0x000184CA
		public void Clear()
		{
			this.m_table.SetAll(false);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001A2D8 File Offset: 0x000184D8
		private void Init(int initialNumElements, double falsePositiveProb, BloomFilter<TKey>.GetHashCodeDelegate hash1, BloomFilter<TKey>.GetHashCodeDelegate hash2)
		{
			this.m_hash1 = hash1;
			this.m_hash2 = hash2;
			double num = 1.0 / Math.Pow(2.0, Math.Log(2.0));
			int num2 = (int)Math.Ceiling((double)initialNumElements * Math.Log(falsePositiveProb, num));
			this.Initialize(num2, (int)Math.Round(Math.Log(2.0) * (double)num2 / (double)initialNumElements));
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001A34D File Offset: 0x0001854D
		private void Initialize(int tableSize, int numHashes)
		{
			this.m_numHashes = numHashes;
			this.m_table = new BitArray(Math.Max(1, tableSize), false);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001A369 File Offset: 0x00018569
		private static int GetHashCodeDefault(TKey key)
		{
			return key.GetHashCode();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001A378 File Offset: 0x00018578
		private int GetHashCode1(TKey key)
		{
			return Utilities.GetHashCode(101, this.m_hash0(key));
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001A38D File Offset: 0x0001858D
		private int GetHashCode2(TKey key)
		{
			return Utilities.GetHashCode(179, this.m_hash0(key));
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001A3A5 File Offset: 0x000185A5
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_table.Length / 8);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001A3B5 File Offset: 0x000185B5
		public int Size
		{
			get
			{
				return (this.m_table.Count + 8 - 1) / 8;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001A3C8 File Offset: 0x000185C8
		public void Insert(TKey obj)
		{
			int num = this.m_hash1(obj);
			int num2 = this.m_hash2(obj);
			num = (int)((ulong)num % (ulong)((long)this.m_table.Count));
			num2 = (int)((ulong)num2 % (ulong)((long)this.m_table.Count));
			for (int i = 0; i < this.m_numHashes; i++)
			{
				this.m_table[num] = true;
				num = (int)((ulong)(num + num2) % (ulong)((long)this.m_table.Count));
				num2 = (int)((ulong)(num2 + i) % (ulong)((long)this.m_table.Count));
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001A458 File Offset: 0x00018658
		public void Insert(TKey obj, out bool alreadyPresent)
		{
			int num = this.m_hash1(obj);
			int num2 = this.m_hash2(obj);
			num = (int)((ulong)num % (ulong)((long)this.m_table.Count));
			num2 = (int)((ulong)num2 % (ulong)((long)this.m_table.Count));
			alreadyPresent = true;
			for (int i = 0; i < this.m_numHashes; i++)
			{
				alreadyPresent &= this.m_table[num];
				this.m_table[num] = true;
				num = (int)((ulong)(num + num2) % (ulong)((long)this.m_table.Count));
				num2 = (int)((ulong)(num2 + i) % (ulong)((long)this.m_table.Count));
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001A4FC File Offset: 0x000186FC
		public bool Contains(TKey obj)
		{
			int num = this.m_hash1(obj);
			int num2 = this.m_hash2(obj);
			num = (int)((ulong)num % (ulong)((long)this.m_table.Count));
			num2 = (int)((ulong)num2 % (ulong)((long)this.m_table.Count));
			for (int i = 0; i < this.m_numHashes; i++)
			{
				if (!this.m_table[num])
				{
					return false;
				}
				num = (int)((ulong)(num + num2) % (ulong)((long)this.m_table.Count));
				num2 = (int)((ulong)(num2 + i) % (ulong)((long)this.m_table.Count));
			}
			return true;
		}

		// Token: 0x04000099 RID: 153
		private BloomFilter<TKey>.GetHashCodeDelegate m_hash0;

		// Token: 0x0400009A RID: 154
		private BloomFilter<TKey>.GetHashCodeDelegate m_hash1;

		// Token: 0x0400009B RID: 155
		private BloomFilter<TKey>.GetHashCodeDelegate m_hash2;

		// Token: 0x0400009C RID: 156
		private int m_numHashes;

		// Token: 0x0400009D RID: 157
		private BitArray m_table;

		// Token: 0x020000E6 RID: 230
		// (Invoke) Token: 0x060008E6 RID: 2278
		public delegate int GetHashCodeDelegate(TKey key);
	}
}
