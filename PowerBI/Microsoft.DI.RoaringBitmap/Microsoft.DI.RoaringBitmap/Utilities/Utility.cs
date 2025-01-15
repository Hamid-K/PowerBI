using System;

namespace Microsoft.DI.RoaringBitmap.Utilities
{
	// Token: 0x0200000A RID: 10
	internal static class Utility
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000030C8 File Offset: 0x000012C8
		public static Pair<char> GetHighLowSignificantBits(uint index)
		{
			char maxValue = char.MaxValue;
			return Pair.New<char>(Convert.ToChar(index >> 16), Convert.ToChar(index & (uint)maxValue));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000030F4 File Offset: 0x000012F4
		public static void Fill<T>(this T[] arrayToFill, T with)
		{
			for (int i = 0; i < arrayToFill.Length; i++)
			{
				arrayToFill[i] = with;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003117 File Offset: 0x00001317
		public static int PopCount(ulong value)
		{
			value -= (value >> 1) & Utility.M1;
			value = (value & Utility.M2) + ((value >> 2) & Utility.M2);
			value = (value + (value >> 4)) & Utility.M4;
			return (int)(value * Utility.H01 >> 56);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003154 File Offset: 0x00001354
		public static int TrailingZeroCount(ulong value)
		{
			if (value == 0UL)
			{
				return 64;
			}
			int num = 0;
			if ((value & (ulong)(-1)) == 0UL)
			{
				num += 32;
				value >>= 32;
			}
			if ((value & 65535UL) == 0UL)
			{
				num += 16;
				value >>= 16;
			}
			if ((value & 255UL) == 0UL)
			{
				num += 8;
				value >>= 8;
			}
			if ((value & 15UL) == 0UL)
			{
				num += 4;
				value >>= 4;
			}
			if ((value & 3UL) == 0UL)
			{
				num += 2;
				value >>= 2;
			}
			if ((value & 1UL) == 0UL)
			{
				num++;
			}
			return num;
		}

		// Token: 0x0400001A RID: 26
		private static readonly ulong M1 = 6148914691236517205UL;

		// Token: 0x0400001B RID: 27
		private static readonly ulong M2 = 3689348814741910323UL;

		// Token: 0x0400001C RID: 28
		private static readonly ulong M4 = 1085102592571150095UL;

		// Token: 0x0400001D RID: 29
		private static readonly ulong H01 = 72340172838076673UL;
	}
}
