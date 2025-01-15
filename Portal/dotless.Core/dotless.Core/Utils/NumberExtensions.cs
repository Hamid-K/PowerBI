using System;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Utils
{
	// Token: 0x0200000E RID: 14
	public static class NumberExtensions
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003295 File Offset: 0x00001495
		public static double Normalize(this Number value)
		{
			return value.Normalize(1.0);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000032A6 File Offset: 0x000014A6
		public static double Normalize(this Number value, double max)
		{
			return value.Normalize(0.0, max);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000032B8 File Offset: 0x000014B8
		public static double Normalize(this Number value, double min, double max)
		{
			return NumberExtensions.Normalize(value.ToNumber(max), min, max);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000032C8 File Offset: 0x000014C8
		public static double Normalize(double value)
		{
			return NumberExtensions.Normalize(value, 1.0);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032D9 File Offset: 0x000014D9
		public static double Normalize(double value, double max)
		{
			return NumberExtensions.Normalize(value, 0.0, max);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000032EB File Offset: 0x000014EB
		public static double Normalize(double value, double min, double max)
		{
			if (value < min)
			{
				return min;
			}
			if (value <= max)
			{
				return value;
			}
			return max;
		}
	}
}
