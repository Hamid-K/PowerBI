using System;
using System.Collections;

namespace antlr.collections.impl
{
	// Token: 0x02000002 RID: 2
	internal class BitSet : ICloneable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public BitSet()
			: this(64)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DA File Offset: 0x000002DA
		public BitSet(long[] bits_)
		{
			this.dataBits = bits_;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E9 File Offset: 0x000002E9
		public BitSet(int nbits)
		{
			this.dataBits = new long[(nbits - 1 >> 6) + 1];
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public virtual void add(int el)
		{
			int num = BitSet.wordNumber(el);
			if (num >= this.dataBits.Length)
			{
				this.growToInclude(el);
			}
			this.dataBits[num] |= BitSet.bitMask(el);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002148 File Offset: 0x00000348
		public virtual BitSet and(BitSet a)
		{
			BitSet bitSet = (BitSet)this.Clone();
			bitSet.andInPlace(a);
			return bitSet;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000216C File Offset: 0x0000036C
		public virtual void andInPlace(BitSet a)
		{
			int num = Math.Min(this.dataBits.Length, a.dataBits.Length);
			for (int i = num - 1; i >= 0; i--)
			{
				this.dataBits[i] &= a.dataBits[i];
			}
			for (int j = num; j < this.dataBits.Length; j++)
			{
				this.dataBits[j] = 0L;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DC File Offset: 0x000003DC
		private static long bitMask(int bitNumber)
		{
			int num = bitNumber & BitSet.MOD_MASK;
			return 1L << num;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public virtual void clear()
		{
			for (int i = this.dataBits.Length - 1; i >= 0; i--)
			{
				this.dataBits[i] = 0L;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002224 File Offset: 0x00000424
		public virtual void clear(int el)
		{
			int num = BitSet.wordNumber(el);
			if (num >= this.dataBits.Length)
			{
				this.growToInclude(el);
			}
			this.dataBits[num] &= ~BitSet.bitMask(el);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual object Clone()
		{
			BitSet bitSet = new BitSet();
			bitSet.dataBits = new long[this.dataBits.Length];
			Array.Copy(this.dataBits, 0, bitSet.dataBits, 0, this.dataBits.Length);
			return bitSet;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022B0 File Offset: 0x000004B0
		public virtual int degree()
		{
			int num = 0;
			for (int i = this.dataBits.Length - 1; i >= 0; i--)
			{
				long num2 = this.dataBits[i];
				if (num2 != 0L)
				{
					for (int j = 63; j >= 0; j--)
					{
						if ((num2 & (1L << j)) != 0L)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002301 File Offset: 0x00000501
		public override int GetHashCode()
		{
			return this.dataBits.GetHashCode();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002310 File Offset: 0x00000510
		public override bool Equals(object obj)
		{
			if (obj != null && obj is BitSet)
			{
				BitSet bitSet = (BitSet)obj;
				int num = Math.Min(this.dataBits.Length, bitSet.dataBits.Length);
				int num2 = num;
				while (num2-- > 0)
				{
					if (this.dataBits[num2] != bitSet.dataBits[num2])
					{
						return false;
					}
				}
				if (this.dataBits.Length > num)
				{
					int num3 = this.dataBits.Length;
					while (num3-- > num)
					{
						if (this.dataBits[num3] != 0L)
						{
							return false;
						}
					}
				}
				else if (bitSet.dataBits.Length > num)
				{
					int num4 = bitSet.dataBits.Length;
					while (num4-- > num)
					{
						if (bitSet.dataBits[num4] != 0L)
						{
							return false;
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000023CC File Offset: 0x000005CC
		public virtual void growToInclude(int bit)
		{
			int num = Math.Max(this.dataBits.Length << 1, BitSet.numWordsToHold(bit));
			long[] array = new long[num];
			Array.Copy(this.dataBits, 0, array, 0, this.dataBits.Length);
			this.dataBits = array;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002414 File Offset: 0x00000614
		public virtual bool member(int el)
		{
			int num = BitSet.wordNumber(el);
			return num < this.dataBits.Length && (this.dataBits[num] & BitSet.bitMask(el)) != 0L;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000244C File Offset: 0x0000064C
		public virtual bool nil()
		{
			for (int i = this.dataBits.Length - 1; i >= 0; i--)
			{
				if (this.dataBits[i] != 0L)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002480 File Offset: 0x00000680
		public virtual BitSet not()
		{
			BitSet bitSet = (BitSet)this.Clone();
			bitSet.notInPlace();
			return bitSet;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000024A0 File Offset: 0x000006A0
		public virtual void notInPlace()
		{
			for (int i = this.dataBits.Length - 1; i >= 0; i--)
			{
				this.dataBits[i] = ~this.dataBits[i];
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024D3 File Offset: 0x000006D3
		public virtual void notInPlace(int maxBit)
		{
			this.notInPlace(0, maxBit);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024E0 File Offset: 0x000006E0
		public virtual void notInPlace(int minBit, int maxBit)
		{
			this.growToInclude(maxBit);
			for (int i = minBit; i <= maxBit; i++)
			{
				int num = BitSet.wordNumber(i);
				this.dataBits[num] ^= BitSet.bitMask(i);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002525 File Offset: 0x00000725
		private static int numWordsToHold(int el)
		{
			return (el >> 6) + 1;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000252C File Offset: 0x0000072C
		public static BitSet of(int el)
		{
			BitSet bitSet = new BitSet(el + 1);
			bitSet.add(el);
			return bitSet;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000254C File Offset: 0x0000074C
		public virtual BitSet or(BitSet a)
		{
			BitSet bitSet = (BitSet)this.Clone();
			bitSet.orInPlace(a);
			return bitSet;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002570 File Offset: 0x00000770
		public virtual void orInPlace(BitSet a)
		{
			if (a.dataBits.Length > this.dataBits.Length)
			{
				this.setSize(a.dataBits.Length);
			}
			int num = Math.Min(this.dataBits.Length, a.dataBits.Length);
			for (int i = num - 1; i >= 0; i--)
			{
				this.dataBits[i] |= a.dataBits[i];
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025E4 File Offset: 0x000007E4
		public virtual void remove(int el)
		{
			int num = BitSet.wordNumber(el);
			if (num >= this.dataBits.Length)
			{
				this.growToInclude(el);
			}
			this.dataBits[num] &= ~BitSet.bitMask(el);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000262C File Offset: 0x0000082C
		private void setSize(int nwords)
		{
			long[] array = new long[nwords];
			int num = Math.Min(nwords, this.dataBits.Length);
			Array.Copy(this.dataBits, 0, array, 0, num);
			this.dataBits = array;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002665 File Offset: 0x00000865
		public virtual int size()
		{
			return this.dataBits.Length << 6;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002671 File Offset: 0x00000871
		public virtual int lengthInLongWords()
		{
			return this.dataBits.Length;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000267B File Offset: 0x0000087B
		public virtual bool subset(BitSet a)
		{
			return a != null && this.and(a).Equals(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002690 File Offset: 0x00000890
		public virtual void subtractInPlace(BitSet a)
		{
			if (a == null)
			{
				return;
			}
			int num = 0;
			while (num < this.dataBits.Length && num < a.dataBits.Length)
			{
				this.dataBits[num] &= ~a.dataBits[num];
				num++;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026E0 File Offset: 0x000008E0
		public virtual int[] toArray()
		{
			int[] array = new int[this.degree()];
			int num = 0;
			for (int i = 0; i < this.dataBits.Length << 6; i++)
			{
				if (this.member(i))
				{
					array[num++] = i;
				}
			}
			return array;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002722 File Offset: 0x00000922
		public virtual long[] toPackedArray()
		{
			return this.dataBits;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000272A File Offset: 0x0000092A
		public override string ToString()
		{
			return this.ToString(",");
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002738 File Offset: 0x00000938
		public virtual string ToString(string separator)
		{
			string text = "";
			for (int i = 0; i < this.dataBits.Length << 6; i++)
			{
				if (this.member(i))
				{
					if (text.Length > 0)
					{
						text += separator;
					}
					text += i;
				}
			}
			return text;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002788 File Offset: 0x00000988
		public virtual string ToString(string separator, ArrayList vocabulary)
		{
			if (vocabulary == null)
			{
				return this.ToString(separator);
			}
			string text = "";
			for (int i = 0; i < this.dataBits.Length << 6; i++)
			{
				if (this.member(i))
				{
					if (text.Length > 0)
					{
						text += separator;
					}
					if (i >= vocabulary.Count)
					{
						object obj = text;
						text = string.Concat(new object[] { obj, "<bad element ", i, ">" });
					}
					else if (vocabulary[i] == null)
					{
						object obj2 = text;
						text = string.Concat(new object[] { obj2, "<", i, ">" });
					}
					else
					{
						text += (string)vocabulary[i];
					}
				}
			}
			return text;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000286C File Offset: 0x00000A6C
		public virtual string toStringOfHalfWords()
		{
			string text = new string("".ToCharArray());
			for (int i = 0; i < this.dataBits.Length; i++)
			{
				if (i != 0)
				{
					text += ", ";
				}
				long num = this.dataBits[i];
				num &= (long)((ulong)(-1));
				text = text + num + "UL";
				text += ", ";
				num = BitSet.URShift(this.dataBits[i], 32);
				num &= (long)((ulong)(-1));
				text = text + num + "UL";
			}
			return text;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002900 File Offset: 0x00000B00
		public virtual string toStringOfWords()
		{
			string text = new string("".ToCharArray());
			for (int i = 0; i < this.dataBits.Length; i++)
			{
				if (i != 0)
				{
					text += ", ";
				}
				text = text + this.dataBits[i] + "L";
			}
			return text;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002959 File Offset: 0x00000B59
		private static int wordNumber(int bit)
		{
			return bit >> 6;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000295E File Offset: 0x00000B5E
		public static int URShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002979 File Offset: 0x00000B79
		public static int URShift(int number, long bits)
		{
			return BitSet.URShift(number, (int)bits);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002983 File Offset: 0x00000B83
		public static long URShift(long number, int bits)
		{
			if (number >= 0L)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static long URShift(long number, long bits)
		{
			return BitSet.URShift(number, (int)bits);
		}

		// Token: 0x04000001 RID: 1
		protected internal const int BITS = 64;

		// Token: 0x04000002 RID: 2
		protected internal const int NIBBLE = 4;

		// Token: 0x04000003 RID: 3
		protected internal const int LOG_BITS = 6;

		// Token: 0x04000004 RID: 4
		protected internal static readonly int MOD_MASK = 63;

		// Token: 0x04000005 RID: 5
		protected internal long[] dataBits;
	}
}
