using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200037D RID: 893
	internal static class MeanVarUtils
	{
		// Token: 0x0600133B RID: 4923 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
		internal static void AdjustForZeros(ref double mean, ref double m2, ref long count, long numZeros)
		{
			if (numZeros == 0L)
			{
				return;
			}
			count += numZeros;
			double num = 0.0 - mean;
			mean += num * (double)numZeros / (double)count;
			double num2 = num * (0.0 - mean);
			m2 += num2 * (double)numZeros;
		}
	}
}
