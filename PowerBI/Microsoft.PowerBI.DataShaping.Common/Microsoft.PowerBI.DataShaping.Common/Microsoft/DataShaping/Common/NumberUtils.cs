using System;

namespace Microsoft.DataShaping.Common
{
	// Token: 0x02000017 RID: 23
	internal static class NumberUtils
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003AFC File Offset: 0x00001CFC
		internal static bool TryCountSignificantDigits(decimal value, out int count)
		{
			count = 0;
			int[] bits = decimal.GetBits(value);
			if (bits[2] != 0)
			{
				return false;
			}
			ulong num = (ulong)bits[1];
			num <<= 32;
			num += (ulong)bits[0];
			bool flag = false;
			while (num != 0UL)
			{
				if (!flag && num % 10UL != 0UL)
				{
					flag = true;
				}
				if (flag)
				{
					count++;
				}
				num /= 10UL;
			}
			return true;
		}
	}
}
