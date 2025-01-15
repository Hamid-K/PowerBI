using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public sealed class BitHash : IMemoryUsage, IEnumerable<KeyValuePair<int, int>>, IEnumerable
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00019525 File Offset: 0x00017725
		public BitHash(int keyBits, int valueBits)
			: this(keyBits, valueBits, 0.75f)
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00019534 File Offset: 0x00017734
		public BitHash(int keyBits, int valueBits, float load)
		{
			this.m_keyBits = keyBits;
			this.m_valueBits = valueBits;
			this.load = Math.Min(load, 0.9f);
			this.m_buckets = new byte[(int)Math.Ceiling((double)(16 * (this.m_keyBits + this.m_valueBits)) / 8.0)];
			this.m_buckets2 = new byte[0];
			this.mask = 15;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000195B1 File Offset: 0x000177B1
		private int GetBytePos(long bitPos)
		{
			return (int)((double)bitPos / 8.0);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000195C0 File Offset: 0x000177C0
		private int MapKey(int key)
		{
			int num = (int)(uint.MaxValue >> 32 - this.m_keyBits);
			return key & num;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000195E0 File Offset: 0x000177E0
		private int MapValue(int value)
		{
			int num = (int)(uint.MaxValue >> 32 - this.m_valueBits);
			return value & num;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000195FF File Offset: 0x000177FF
		private int GetBucket(int key)
		{
			return key & this.mask;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00019609 File Offset: 0x00017809
		private long BucketBits()
		{
			return (long)(this.m_keyBits + this.m_valueBits);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019619 File Offset: 0x00017819
		private int GetKey(int bucket)
		{
			return this.GetKey(this.m_buckets, bucket);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00019628 File Offset: 0x00017828
		private int GetKey(byte[] buckets, int bucket)
		{
			int num = 0;
			long num2 = (long)bucket * this.BucketBits();
			int num3 = (int)(num2 / 8L);
			int num4 = (int)(num2 % 8L);
			int i = this.m_keyBits;
			while (i > 0)
			{
				byte b = buckets[num3];
				int num5 = Math.Min(8 - num4, i);
				b = (byte)(b << num4);
				b = (byte)(b >> 8 - num5);
				num <<= num5;
				num |= (int)b;
				i -= num5;
				num3++;
				num4 = 0;
			}
			return num;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00019698 File Offset: 0x00017898
		private int GetValue(int bucket)
		{
			return this.GetValue(this.m_buckets, bucket);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000196A8 File Offset: 0x000178A8
		private int GetValue(byte[] buckets, int bucket)
		{
			int num = 0;
			long num2 = (long)bucket * this.BucketBits() + (long)this.m_keyBits;
			int num3 = (int)(num2 / 8L);
			int num4 = (int)(num2 % 8L);
			int i = this.m_valueBits;
			while (i > 0)
			{
				byte b = buckets[num3];
				int num5 = Math.Min(8 - num4, i);
				b = (byte)(b << num4);
				b = (byte)(b >> 8 - num5);
				num <<= num5;
				num |= (int)b;
				i -= num5;
				num3++;
				num4 = 0;
			}
			return num;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00019720 File Offset: 0x00017920
		private void SetKey(int bucket, int key)
		{
			this.SetKey(this.m_buckets, bucket, key);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00019730 File Offset: 0x00017930
		private void SetValue(int bucket, int value)
		{
			this.SetValue(this.m_buckets, bucket, value);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00019740 File Offset: 0x00017940
		private void SetKey(byte[] buckets, int bucket, int key)
		{
			key = this.MapKey(key);
			long num = (long)bucket * this.BucketBits();
			int num2 = (int)(num / 8L);
			int num3 = (int)(num % 8L);
			int i = this.m_keyBits;
			while (i > 0)
			{
				int num4 = Math.Min(8 - num3, i);
				byte b = buckets[num2];
				byte b2 = byte.MaxValue;
				b2 = (byte)(b2 >> 8 - num4);
				b2 = (byte)(b2 << 8 - num3 - num4);
				b &= ~b2;
				byte b3 = (byte)(key >> i - num4);
				b3 &= (byte)(255 >> num3);
				b3 = (byte)(b3 << 8 - num3 - num4);
				buckets[num2] = b | b3;
				i -= num4;
				num2++;
				num3 = 0;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000197F0 File Offset: 0x000179F0
		private void SetValue(byte[] buckets, int bucket, int value)
		{
			long num = (long)bucket * this.BucketBits() + (long)this.m_keyBits;
			int num2 = (int)(num / 8L);
			int num3 = (int)(num % 8L);
			int i = this.m_valueBits;
			while (i > 0)
			{
				int num4 = Math.Min(8 - num3, i);
				byte b = buckets[num2];
				byte b2 = byte.MaxValue;
				b2 = (byte)(b2 >> 8 - num4);
				b2 = (byte)(b2 << 8 - num3 - num4);
				b &= ~b2;
				byte b3 = (byte)(value >> i - num4);
				b3 &= (byte)(255 >> num3);
				b3 = (byte)(b3 << 8 - num3 - num4);
				buckets[num2] = b | b3;
				i -= num4;
				num2++;
				num3 = 0;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0001989D File Offset: 0x00017A9D
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000198A8 File Offset: 0x00017AA8
		public void Add(int key, int value)
		{
			key = this.MapKey(key);
			value = this.MapValue(value);
			if (value == 0)
			{
				throw new ArgumentException("Value may not be zero.");
			}
			if ((float)(this.count + 1) / (float)this.ActiveBucketCount > this.load)
			{
				this.Rehash();
			}
			int num = this.GetBucket(key);
			while (this.GetValue(num) != 0)
			{
				if (this.GetKey(num) == key)
				{
					throw new ArgumentException("Key already present!");
				}
				num = this.GetNextBucket(num);
			}
			this.SetKey(num, key);
			this.SetValue(num, value);
			this.count++;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00019942 File Offset: 0x00017B42
		private int GetNextBucket(int bucket)
		{
			return ++bucket & this.mask;
		}

		// Token: 0x1700008A RID: 138
		public int this[int key]
		{
			get
			{
				key = this.MapKey(key);
				int num = this.GetBucket(key);
				while (this.GetKey(num) != key)
				{
					if (this.GetValue(num) == 0)
					{
						throw new KeyNotFoundException();
					}
					num = this.GetNextBucket(num);
				}
				if (key == 0 && this.GetValue(num) == 0)
				{
					throw new KeyNotFoundException();
				}
				return this.GetValue(num);
			}
			set
			{
				key = this.MapKey(key);
				int num = this.MapValue(value);
				if (num == 0)
				{
					throw new ArgumentException("Value may not be zero.");
				}
				int num2 = this.GetBucket(key);
				while (this.GetKey(num2) != key)
				{
					if (this.GetValue(num2) == 0)
					{
						this.SetKey(num2, key);
						this.SetValue(num2, num);
						int num3 = this.count + 1;
						this.count = num3;
						if ((float)num3 / (float)this.ActiveBucketCount > this.load)
						{
							this.Rehash();
						}
						return;
					}
					num2 = this.GetNextBucket(num2);
				}
				if (key == 0 && this.GetValue(num2) == 0)
				{
					this.SetValue(num2, value);
					int num3 = this.count + 1;
					this.count = num3;
					if ((float)num3 / (float)this.ActiveBucketCount > this.load)
					{
						this.Rehash();
					}
					return;
				}
				this.SetKey(num2, key);
				this.SetValue(num2, num);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00019A87 File Offset: 0x00017C87
		internal int ActiveBucketCount
		{
			get
			{
				return this.mask + 1;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00019A94 File Offset: 0x00017C94
		internal void Rehash()
		{
			if (this.mask == 2147483647)
			{
				throw new OverflowException("The hash table has reached the maximum size and is unable to grow further.");
			}
			byte[] buckets = this.m_buckets;
			this.m_buckets = new byte[1 + (int)((long)(this.m_keyBits + this.m_valueBits) * (long)this.ActiveBucketCount * 2L / 8L)];
			int activeBucketCount = this.ActiveBucketCount;
			int num = this.mask;
			this.mask = (this.mask << 1) | 1;
			int num2 = this.count;
			this.count = 0;
			for (int i = 0; i < activeBucketCount; i++)
			{
				int value = this.GetValue(buckets, i);
				if (value != 0)
				{
					this.Add(this.GetKey(buckets, i), value);
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00019B40 File Offset: 0x00017D40
		public bool ContainsKey(int key)
		{
			key = this.MapKey(key);
			int num = this.GetBucket(key);
			while (this.GetKey(num) != key)
			{
				if (this.GetValue(num) == 0)
				{
					return false;
				}
				num = this.GetNextBucket(num);
			}
			return key != 0 || this.GetValue(num) != 0;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00019B8C File Offset: 0x00017D8C
		public bool TryGetValue(int key, out int value)
		{
			key = this.MapKey(key);
			int num = this.GetBucket(key);
			while (this.GetKey(num) != key)
			{
				if (this.GetValue(num) == 0)
				{
					value = 0;
					return false;
				}
				num = this.GetNextBucket(num);
			}
			value = this.GetValue(num);
			return key != 0 || value != 0;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00019BE0 File Offset: 0x00017DE0
		public void Remove(int key)
		{
			key = this.MapKey(key);
			int num = this.GetBucket(key);
			while (this.GetKey(num) != key)
			{
				if (this.GetValue(num) == 0)
				{
					throw new KeyNotFoundException();
				}
				num = this.GetNextBucket(num);
			}
			if (key == 0 && this.GetValue(num) == 0)
			{
				throw new KeyNotFoundException();
			}
			this.SetKey(num, 0);
			this.SetValue(num, 0);
			this.count--;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00019C51 File Offset: 0x00017E51
		public void Clear()
		{
			Array.Clear(this.m_buckets, 0, this.m_buckets.Length);
			this.count = 0;
			this.mask = 15;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00019C76 File Offset: 0x00017E76
		public long MemoryUsage
		{
			get
			{
				return (long)(this.m_buckets.Length + this.m_buckets2.Length);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00019C8A File Offset: 0x00017E8A
		public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
		{
			return new BitHash.Enumerator(this);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00019C92 File Offset: 0x00017E92
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new BitHash.Enumerator(this);
		}

		// Token: 0x0400008F RID: 143
		private readonly float load = 0.75f;

		// Token: 0x04000090 RID: 144
		private readonly int m_keyBits;

		// Token: 0x04000091 RID: 145
		private readonly int m_valueBits;

		// Token: 0x04000092 RID: 146
		private byte[] m_buckets;

		// Token: 0x04000093 RID: 147
		private byte[] m_buckets2;

		// Token: 0x04000094 RID: 148
		private int mask;

		// Token: 0x04000095 RID: 149
		private int count;

		// Token: 0x020000E5 RID: 229
		[Serializable]
		private class Enumerator : IEnumerator<KeyValuePair<int, int>>, IDisposable, IEnumerator
		{
			// Token: 0x060008DF RID: 2271 RVA: 0x0002CB27 File Offset: 0x0002AD27
			public Enumerator(BitHash hashSet)
			{
				this.m_hash = hashSet;
				this.m_buckets = hashSet.m_buckets;
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0002CB49 File Offset: 0x0002AD49
			public KeyValuePair<int, int> Current
			{
				get
				{
					return new KeyValuePair<int, int>(this.m_hash.GetKey(this.m_idx), this.m_hash.GetValue(this.m_idx));
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002CB72 File Offset: 0x0002AD72
			object IEnumerator.Current
			{
				get
				{
					return this.m_buckets[this.m_idx];
				}
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x0002CB88 File Offset: 0x0002AD88
			public bool MoveNext()
			{
				do
				{
					int num = this.m_idx + 1;
					this.m_idx = num;
					if (num >= this.m_hash.ActiveBucketCount)
					{
						return false;
					}
				}
				while (this.m_hash.GetValue(this.m_idx) == 0);
				return true;
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x0002CBCB File Offset: 0x0002ADCB
			public void Reset()
			{
				this.m_idx = -1;
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
			public void Dispose()
			{
			}

			// Token: 0x04000249 RID: 585
			private BitHash m_hash;

			// Token: 0x0400024A RID: 586
			private byte[] m_buckets;

			// Token: 0x0400024B RID: 587
			private int m_idx = -1;
		}
	}
}
