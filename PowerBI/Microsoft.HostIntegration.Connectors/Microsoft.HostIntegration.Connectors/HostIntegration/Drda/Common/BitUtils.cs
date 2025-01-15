using System;
using System.Text;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200084D RID: 2125
	public class BitUtils
	{
		// Token: 0x06004366 RID: 17254 RVA: 0x000E2628 File Offset: 0x000E0828
		public static int UnsignedRightShift(int number, int bits)
		{
			if (number >= 0)
			{
				return number >> bits;
			}
			return (number >> bits) + (2 << ~bits);
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x000E2643 File Offset: 0x000E0843
		public static int UnsignedRightShift(int number, long bits)
		{
			return BitUtils.UnsignedRightShift(number, (int)bits);
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x000E264D File Offset: 0x000E084D
		public static long UnsignedRightShift(long number, int bits)
		{
			if (number >= 0L)
			{
				return number >> bits;
			}
			return (number >> bits) + (2L << ~bits);
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x000E266A File Offset: 0x000E086A
		public static long UnsignedRightShift(long number, long bits)
		{
			return BitUtils.UnsignedRightShift(number, (int)bits);
		}

		// Token: 0x0600436A RID: 17258 RVA: 0x000E2674 File Offset: 0x000E0874
		public static string ConvertToHexString(byte[] array)
		{
			if (array == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("X2"));
			}
			return stringBuilder.ToString();
		}
	}
}
