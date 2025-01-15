using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000497 RID: 1175
	[Serializable]
	internal sealed class ChartDataPointList : CellList
	{
		// Token: 0x0600385C RID: 14428 RVA: 0x000F59DB File Offset: 0x000F3BDB
		public ChartDataPointList()
		{
		}

		// Token: 0x0600385D RID: 14429 RVA: 0x000F59E3 File Offset: 0x000F3BE3
		internal ChartDataPointList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700189E RID: 6302
		internal ChartDataPoint this[int index]
		{
			get
			{
				return (ChartDataPoint)base[index];
			}
		}
	}
}
