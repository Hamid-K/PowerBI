using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000696 RID: 1686
	[Serializable]
	internal sealed class ReportItemIndexerList : ArrayList
	{
		// Token: 0x06005C4B RID: 23627 RVA: 0x0017969E File Offset: 0x0017789E
		internal ReportItemIndexerList()
		{
		}

		// Token: 0x06005C4C RID: 23628 RVA: 0x001796A6 File Offset: 0x001778A6
		internal ReportItemIndexerList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002070 RID: 8304
		internal ReportItemIndexer this[int index]
		{
			get
			{
				return (ReportItemIndexer)base[index];
			}
		}
	}
}
