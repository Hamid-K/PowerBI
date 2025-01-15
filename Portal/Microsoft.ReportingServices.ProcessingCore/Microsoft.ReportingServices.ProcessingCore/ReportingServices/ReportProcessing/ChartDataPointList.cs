using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A2 RID: 1698
	[Serializable]
	internal sealed class ChartDataPointList : ArrayList
	{
		// Token: 0x06005C76 RID: 23670 RVA: 0x001798BB File Offset: 0x00177ABB
		internal ChartDataPointList()
		{
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x001798C3 File Offset: 0x00177AC3
		internal ChartDataPointList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700207D RID: 8317
		internal ChartDataPoint this[int index]
		{
			get
			{
				return (ChartDataPoint)base[index];
			}
		}
	}
}
