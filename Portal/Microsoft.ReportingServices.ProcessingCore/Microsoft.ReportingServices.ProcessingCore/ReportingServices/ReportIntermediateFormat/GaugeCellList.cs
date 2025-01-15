using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003E9 RID: 1001
	internal sealed class GaugeCellList : CellList
	{
		// Token: 0x17001484 RID: 5252
		internal GaugeCell this[int index]
		{
			get
			{
				return (GaugeCell)base[index];
			}
		}
	}
}
