using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A1 RID: 1697
	[Serializable]
	internal sealed class ChartDataPointInstanceList : ArrayList
	{
		// Token: 0x06005C73 RID: 23667 RVA: 0x0017989C File Offset: 0x00177A9C
		internal ChartDataPointInstanceList()
		{
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x001798A4 File Offset: 0x00177AA4
		internal ChartDataPointInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700207C RID: 8316
		internal ChartDataPointInstance this[int index]
		{
			get
			{
				return (ChartDataPointInstance)base[index];
			}
		}
	}
}
