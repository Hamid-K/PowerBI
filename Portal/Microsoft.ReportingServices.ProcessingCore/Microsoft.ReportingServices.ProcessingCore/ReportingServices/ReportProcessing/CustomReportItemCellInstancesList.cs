using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200075E RID: 1886
	[Serializable]
	internal sealed class CustomReportItemCellInstancesList : ArrayList
	{
		// Token: 0x0600685A RID: 26714 RVA: 0x001960A5 File Offset: 0x001942A5
		internal CustomReportItemCellInstancesList()
		{
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x001960AD File Offset: 0x001942AD
		internal CustomReportItemCellInstancesList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024DC RID: 9436
		internal CustomReportItemCellInstanceList this[int index]
		{
			get
			{
				return (CustomReportItemCellInstanceList)base[index];
			}
		}
	}
}
