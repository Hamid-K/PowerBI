using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000BE RID: 190
	[Serializable]
	internal sealed class WeightModifier
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0001FEFC File Offset: 0x0001E0FC
		public static int round(double f)
		{
			double num = (double)1000 * f;
			int num2 = Convert.ToInt32(num);
			if ((double)num2 >= num)
			{
				return num2;
			}
			return num2 + 1;
		}
	}
}
