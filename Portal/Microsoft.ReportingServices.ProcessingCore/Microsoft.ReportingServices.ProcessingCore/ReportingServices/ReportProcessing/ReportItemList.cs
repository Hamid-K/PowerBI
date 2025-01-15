using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200068E RID: 1678
	[Serializable]
	internal sealed class ReportItemList : ArrayList
	{
		// Token: 0x06005C30 RID: 23600 RVA: 0x001794CE File Offset: 0x001776CE
		internal ReportItemList()
		{
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x001794D6 File Offset: 0x001776D6
		internal ReportItemList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002068 RID: 8296
		internal ReportItem this[int index]
		{
			get
			{
				return (ReportItem)base[index];
			}
		}
	}
}
