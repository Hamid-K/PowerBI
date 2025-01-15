using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200069F RID: 1695
	[Serializable]
	internal sealed class MultiChartInstanceList : ArrayList
	{
		// Token: 0x06005C6D RID: 23661 RVA: 0x0017985E File Offset: 0x00177A5E
		internal MultiChartInstanceList()
		{
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00179866 File Offset: 0x00177A66
		internal MultiChartInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700207A RID: 8314
		internal MultiChartInstance this[int index]
		{
			get
			{
				return (MultiChartInstance)base[index];
			}
		}
	}
}
