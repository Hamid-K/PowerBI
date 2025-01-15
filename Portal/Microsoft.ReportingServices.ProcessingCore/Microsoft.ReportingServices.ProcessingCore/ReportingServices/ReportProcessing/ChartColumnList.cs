using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200069E RID: 1694
	[Serializable]
	internal sealed class ChartColumnList : ArrayList
	{
		// Token: 0x06005C6A RID: 23658 RVA: 0x0017983F File Offset: 0x00177A3F
		internal ChartColumnList()
		{
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x00179847 File Offset: 0x00177A47
		internal ChartColumnList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002079 RID: 8313
		internal ChartColumn this[int index]
		{
			get
			{
				return (ChartColumn)base[index];
			}
		}
	}
}
