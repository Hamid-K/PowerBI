using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public sealed class BitVector
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00019C9A File Offset: 0x00017E9A
		public BitVector(int numBits)
		{
			if (numBits == 0)
			{
				this.m_words = new ulong[0];
				return;
			}
			this.ResizeAndClear(numBits);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00019CB9 File Offset: 0x00017EB9
		private static int Bits2Words(int cNumBits)
		{
			return (cNumBits + 64 - 1) / 64;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00019CC4 File Offset: 0x00017EC4
		public static int BitCount(uint u)
		{
			uint num = u - ((u >> 1) & 3681400539U) - ((u >> 2) & 1227133513U);
			return (int)(((num + (num >> 3)) & 3340530119U) % 63U);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public static uint MinPowerOfTwo(uint v)
		{
			v -= 1U;
			v |= v >> 1;
			v |= v >> 2;
			v |= v >> 4;
			v |= v >> 8;
			v |= v >> 16;
			v += 1U;
			return v;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00019D1C File Offset: 0x00017F1C
		public static int MostSignificantBit(uint n)
		{
			int num = 0;
			if ((n & 4294901760U) != 0U)
			{
				num |= 16;
				n >>= 16;
			}
			if ((n & 4294967040U) != 0U)
			{
				num |= 8;
				n >>= 8;
			}
			if ((n & 4294967280U) != 0U)
			{
				num |= 4;
				n >>= 4;
			}
			if ((n & 4294967292U) != 0U)
			{
				num |= 2;
				n >>= 2;
			}
			if ((n & 4294967294U) != 0U)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00019D7C File Offset: 0x00017F7C
		public static int MostSignificantBit(ulong n)
		{
			if (n == 0UL)
			{
				return -1;
			}
			int num = 0;
			if ((n & 18446744069414584320UL) != 0UL)
			{
				num |= 32;
				n >>= 32;
			}
			if ((n & 18446744073709486080UL) != 0UL)
			{
				num |= 16;
				n >>= 16;
			}
			if ((n & 18446744073709551360UL) != 0UL)
			{
				num |= 8;
				n >>= 8;
			}
			if ((n & 18446744073709551600UL) != 0UL)
			{
				num |= 4;
				n >>= 4;
			}
			if ((n & 18446744073709551612UL) != 0UL)
			{
				num |= 2;
				n >>= 2;
			}
			if ((n & 18446744073709551614UL) != 0UL)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00019DFC File Offset: 0x00017FFC
		public static int LeastSignificantBit(uint n)
		{
			int num = 0;
			if ((n & 65535U) == 0U)
			{
				num |= 16;
				n >>= 16;
			}
			if ((n & 255U) == 0U)
			{
				num |= 8;
				n >>= 8;
			}
			if ((n & 15U) == 0U)
			{
				num |= 4;
				n >>= 4;
			}
			if ((n & 3U) == 0U)
			{
				num |= 2;
				n >>= 2;
			}
			if ((n & 1U) == 0U)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00019E58 File Offset: 0x00018058
		public static int LeastSignificantBit(ulong n)
		{
			int num = 0;
			if ((n & (ulong)(-1)) == 0UL)
			{
				num |= 32;
				n >>= 32;
			}
			if ((n & 65535UL) == 0UL)
			{
				num |= 16;
				n >>= 16;
			}
			if ((n & 255UL) == 0UL)
			{
				num |= 8;
				n >>= 8;
			}
			if ((n & 15UL) == 0UL)
			{
				num |= 4;
				n >>= 4;
			}
			if ((n & 3UL) == 0UL)
			{
				num |= 2;
				n >>= 2;
			}
			if ((n & 1UL) == 0UL)
			{
				num |= 1;
			}
			return num;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00019ECC File Offset: 0x000180CC
		public static int BitCount(ulong x)
		{
			x -= (x >> 1) & 6148914691236517205UL;
			x = (x & 3689348814741910323UL) + ((x >> 2) & 3689348814741910323UL);
			x = (x + (x >> 4)) & 1085102592571150095UL;
			return (int)(x * 72340172838076673UL >> 56);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00019F28 File Offset: 0x00018128
		public int BitCount()
		{
			int num = 0;
			int i = 0;
			int num2 = this.m_words.Length;
			while (i < num2)
			{
				num += BitVector.BitCount(this.m_words[i]);
				i++;
			}
			return num;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00019F60 File Offset: 0x00018160
		public int MostSignificantBit()
		{
			int num = 0;
			int num2 = this.m_words.Length;
			int num3 = 0;
			while (num < num2 && (num3 = BitVector.MostSignificantBit(this.m_words[num])) == -1)
			{
				num++;
			}
			if (num == num2)
			{
				return -1;
			}
			return num3 + num * 64;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00019FA4 File Offset: 0x000181A4
		public void CopyFrom(BitVector bv)
		{
			int num = bv.m_words.Length;
			if (num > this.m_words.Length)
			{
				Array.Resize<ulong>(ref this.m_words, num);
			}
			for (int i = 0; i < num; i++)
			{
				this.m_words[i] = bv.m_words[i];
			}
			this.m_numBits = bv.m_numBits;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00019FFC File Offset: 0x000181FC
		public void ResizeAndClear(int cNumBits)
		{
			if (this.m_numBits != cNumBits)
			{
				int num = BitVector.Bits2Words(cNumBits);
				int num2 = ((this.m_words == null) ? 0 : this.m_words.Length);
				if (num > num2)
				{
					Array.Resize<ulong>(ref this.m_words, num);
				}
				for (int i = 0; i < num2; i++)
				{
					this.m_words[i] = 0UL;
				}
				this.m_numBits = cNumBits;
				return;
			}
			this.Clear();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001A064 File Offset: 0x00018264
		public void Resize(int cNumBits)
		{
			if (this.m_numBits != cNumBits)
			{
				int num = BitVector.Bits2Words(cNumBits);
				int num2 = ((this.m_words == null) ? 0 : this.m_words.Length);
				if (num > num2)
				{
					Array.Resize<ulong>(ref this.m_words, num);
				}
				this.m_numBits = cNumBits;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001A0AC File Offset: 0x000182AC
		public int Size
		{
			get
			{
				return this.m_numBits;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001A0B4 File Offset: 0x000182B4
		public void Clear()
		{
			for (int i = this.m_words.Length - 1; i >= 0; i--)
			{
				this.m_words[i] = 0UL;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001A0E0 File Offset: 0x000182E0
		public bool GetBit(int bitIndex)
		{
			return (this.m_words[bitIndex / 64] & (1UL << bitIndex % 64)) > 0UL;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001A0FC File Offset: 0x000182FC
		public void SetBit(int bitIndex, bool b)
		{
			if (b)
			{
				this.m_words[bitIndex / 64] |= 1UL << bitIndex % 64;
				return;
			}
			this.m_words[bitIndex / 64] &= ~(1UL << bitIndex % 64);
		}

		// Token: 0x1700008E RID: 142
		public bool this[int bitIndex]
		{
			get
			{
				return this.GetBit(bitIndex);
			}
			set
			{
				this.SetBit(bitIndex, value);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001A15C File Offset: 0x0001835C
		public void Or(BitVector bv)
		{
			for (int i = BitVector.Bits2Words(this.m_numBits) - 1; i >= 0; i--)
			{
				this.m_words[i] |= bv.m_words[i];
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001A19C File Offset: 0x0001839C
		public void And(BitVector bv)
		{
			for (int i = BitVector.Bits2Words(this.m_numBits) - 1; i >= 0; i--)
			{
				this.m_words[i] &= bv.m_words[i];
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001A1DC File Offset: 0x000183DC
		public bool Equals(BitVector bv)
		{
			for (int i = BitVector.Bits2Words(this.m_numBits) - 1; i >= 0; i--)
			{
				if (this.m_words[i] != bv.m_words[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001A218 File Offset: 0x00018418
		public bool Contains(BitVector bv)
		{
			for (int i = BitVector.Bits2Words(this.m_numBits) - 1; i >= 0; i--)
			{
				if ((bv.m_words[i] & ~(this.m_words[i] != 0UL)) != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000096 RID: 150
		private const int BITS_PER_WORD = 64;

		// Token: 0x04000097 RID: 151
		private int m_numBits;

		// Token: 0x04000098 RID: 152
		private ulong[] m_words;
	}
}
