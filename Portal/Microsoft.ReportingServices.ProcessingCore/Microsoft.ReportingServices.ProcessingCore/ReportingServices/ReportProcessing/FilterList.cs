using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200068B RID: 1675
	[Serializable]
	internal sealed class FilterList : ArrayList
	{
		// Token: 0x06005C27 RID: 23591 RVA: 0x00179471 File Offset: 0x00177671
		internal FilterList()
		{
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x00179479 File Offset: 0x00177679
		internal FilterList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002065 RID: 8293
		internal Filter this[int index]
		{
			get
			{
				return (Filter)base[index];
			}
		}
	}
}
