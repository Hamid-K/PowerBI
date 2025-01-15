using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal static class DDMath
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000224F File Offset: 0x0000044F
		public static int Min3(int v1, int v2, int v3)
		{
			if (v1 < v2 && v1 < v3)
			{
				return v1;
			}
			if (v2 >= v3)
			{
				return v3;
			}
			return v2;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002262 File Offset: 0x00000462
		public static int Max3(int v1, int v2, int v3)
		{
			if (v1 > v2 && v1 > v3)
			{
				return v1;
			}
			if (v2 <= v3)
			{
				return v3;
			}
			return v2;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002275 File Offset: 0x00000475
		public static double Min3(double v1, double v2, double v3)
		{
			if (v1 < v2 && v1 < v3)
			{
				return v1;
			}
			if (v2 >= v3)
			{
				return v3;
			}
			return v2;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002288 File Offset: 0x00000488
		public static double Max3(double v1, double v2, double v3)
		{
			if (v1 > v2 && v1 > v3)
			{
				return v1;
			}
			if (v2 <= v3)
			{
				return v3;
			}
			return v2;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000229B File Offset: 0x0000049B
		public static float Min3(float v1, float v2, float v3)
		{
			if (v1 < v2 && v1 < v3)
			{
				return v1;
			}
			if (v2 >= v3)
			{
				return v3;
			}
			return v2;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022AE File Offset: 0x000004AE
		public static float Max3(float v1, float v2, float v3)
		{
			if (v1 > v2 && v1 > v3)
			{
				return v1;
			}
			if (v2 <= v3)
			{
				return v3;
			}
			return v2;
		}
	}
}
