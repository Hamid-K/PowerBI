using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000002 RID: 2
	public static class BitOperations
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool IsPowerOf2(this uint x)
		{
			return (x & (x - 1U)) == 0U && x > 0U;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public static int CountBits(this ulong n)
		{
			ulong num = n - ((n >> 1) & 8608480567731124087UL) - ((n >> 2) & 3689348814741910323UL) - ((n >> 3) & 1229782938247303441UL);
			return (int)(((num + (num >> 4)) & 1085102592571150095UL) * 72340172838076673UL >> 56);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		public static int CountBits(this uint n)
		{
			uint num = n - ((n >> 1) & 2004318071U) - ((n >> 2) & 858993459U) - ((n >> 3) & 286331153U);
			return (int)(((num + (num >> 4)) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public static bool IsBitSet(this ulong n, int index)
		{
			return (n & (1UL << index)) > 0UL;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F7 File Offset: 0x000002F7
		public static int CountTrailingZeros(this uint n)
		{
			return (int)BitOperations.deBruijn32Positions[(int)((n & -n) * 125613361U >> 27)];
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210C File Offset: 0x0000030C
		public static int CountTrailingZeros(this ulong n)
		{
			checked
			{
				return (int)BitOperations.deBruijn64Positions[(int)((IntPtr)(unchecked((n & -n) * 151050438420815295UL) >> 58))];
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002126 File Offset: 0x00000326
		public static int Log2(this uint n)
		{
			n |= n >> 1;
			n |= n >> 2;
			n |= n >> 4;
			n |= n >> 8;
			n |= n >> 16;
			n = (n >> 1) + 1U;
			return (int)BitOperations.deBruijn32Positions[(int)(n * 125613361U >> 27)];
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002163 File Offset: 0x00000363
		public static int SetUpper16Bits(int i, int value)
		{
			return (i & 65535) | (value << 16);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002171 File Offset: 0x00000371
		public static int GetUpper16Bits(int i)
		{
			return (i >> 16) & 65535;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000217D File Offset: 0x0000037D
		public static int GetUpper32Bits(long i)
		{
			return (int)(i >> 32);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002184 File Offset: 0x00000384
		public static int GetLower32Bits(long i)
		{
			return (int)i;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002188 File Offset: 0x00000388
		public static int SetLower16Bits(int i, int value)
		{
			return (i & -65536) | (value & 65535);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002199 File Offset: 0x00000399
		public static int GetLower16Bits(int i)
		{
			return i & 65535;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021A2 File Offset: 0x000003A2
		public static byte GetUpperByte(short i)
		{
			return (byte)((i >> 8) & 255);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021AE File Offset: 0x000003AE
		public static byte GetLowerByte(short i)
		{
			return (byte)(i & 255);
		}

		// Token: 0x04000001 RID: 1
		private static readonly byte[] deBruijn32Positions = new byte[]
		{
			0, 1, 28, 2, 29, 14, 24, 3, 30, 22,
			20, 15, 25, 17, 4, 8, 31, 27, 13, 23,
			21, 19, 16, 7, 26, 12, 18, 6, 11, 5,
			10, 9
		};

		// Token: 0x04000002 RID: 2
		private static readonly byte[] deBruijn64Positions = new byte[]
		{
			0, 1, 2, 7, 3, 13, 8, 19, 4, 25,
			14, 28, 9, 34, 20, 40, 5, 17, 26, 38,
			15, 46, 29, 48, 10, 31, 35, 54, 21, 50,
			41, 57, 63, 6, 12, 18, 24, 27, 33, 39,
			16, 37, 45, 47, 30, 53, 49, 56, 62, 11,
			23, 32, 36, 44, 52, 55, 61, 22, 43, 51,
			60, 42, 59, 58
		};
	}
}
