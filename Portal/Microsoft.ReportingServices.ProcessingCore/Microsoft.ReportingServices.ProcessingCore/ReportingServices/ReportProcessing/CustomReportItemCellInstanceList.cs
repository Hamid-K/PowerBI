using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200075F RID: 1887
	[Serializable]
	internal sealed class CustomReportItemCellInstanceList : ArrayList
	{
		// Token: 0x0600685D RID: 26717 RVA: 0x001960C4 File Offset: 0x001942C4
		internal CustomReportItemCellInstanceList()
		{
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x001960CC File Offset: 0x001942CC
		internal CustomReportItemCellInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024DD RID: 9437
		internal CustomReportItemCellInstance this[int index]
		{
			get
			{
				return (CustomReportItemCellInstance)base[index];
			}
		}
	}
}
