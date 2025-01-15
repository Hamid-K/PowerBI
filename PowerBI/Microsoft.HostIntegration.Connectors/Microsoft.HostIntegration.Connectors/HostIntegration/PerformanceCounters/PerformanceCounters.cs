using System;

namespace Microsoft.HostIntegration.PerformanceCounters
{
	// Token: 0x02000776 RID: 1910
	public class PerformanceCounters
	{
		// Token: 0x06003DCE RID: 15822 RVA: 0x000CFF18 File Offset: 0x000CE118
		public static int[] InitializeIntArrayToMinus1(int numberOfElements)
		{
			int[] array = new int[numberOfElements];
			for (int i = 0; i < numberOfElements; i++)
			{
				array[i] = -1;
			}
			return array;
		}
	}
}
