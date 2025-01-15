using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006AF RID: 1711
	[Serializable]
	internal sealed class ListContentInstanceList : ArrayList
	{
		// Token: 0x06005CAB RID: 23723 RVA: 0x00179F40 File Offset: 0x00178140
		internal ListContentInstanceList()
		{
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x00179F48 File Offset: 0x00178148
		internal ListContentInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700208A RID: 8330
		internal ListContentInstance this[int index]
		{
			get
			{
				return (ListContentInstance)base[index];
			}
		}
	}
}
