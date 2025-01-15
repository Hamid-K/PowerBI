using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000003 RID: 3
	public static class ExtensionMethods
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000021E8 File Offset: 0x000003E8
		public static int SetUpper16Bits(this int i, int value)
		{
			return (i & 65535) | (value << 16);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021F6 File Offset: 0x000003F6
		public static int GetUpper16Bits(this int i)
		{
			return (i >> 16) & 65535;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002202 File Offset: 0x00000402
		public static int GetUpper32Bits(this long i)
		{
			return (int)(i >> 32);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002209 File Offset: 0x00000409
		public static int GetLower32Bits(this long i)
		{
			return (int)i;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000220D File Offset: 0x0000040D
		public static int SetLower16Bits(this int i, int value)
		{
			return (i & -65536) | (value & 65535);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000221E File Offset: 0x0000041E
		public static int GetLower16Bits(this int i)
		{
			return i & 65535;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002227 File Offset: 0x00000427
		public static byte GetUpperByte(this short i)
		{
			return (byte)((i >> 8) & 255);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002233 File Offset: 0x00000433
		public static byte GetLowerByte(this short i)
		{
			return (byte)(i & 255);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000223D File Offset: 0x0000043D
		public static void CopyToEx(this string str, int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (count > 0)
			{
				str.CopyTo(sourceIndex, destination, destinationIndex, count);
			}
		}
	}
}
