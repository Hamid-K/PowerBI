using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A0 RID: 1696
	[Serializable]
	internal sealed class ChartDataPointInstancesList : ArrayList
	{
		// Token: 0x06005C70 RID: 23664 RVA: 0x0017987D File Offset: 0x00177A7D
		internal ChartDataPointInstancesList()
		{
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x00179885 File Offset: 0x00177A85
		internal ChartDataPointInstancesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700207B RID: 8315
		internal ChartDataPointInstanceList this[int index]
		{
			get
			{
				return (ChartDataPointInstanceList)base[index];
			}
		}
	}
}
