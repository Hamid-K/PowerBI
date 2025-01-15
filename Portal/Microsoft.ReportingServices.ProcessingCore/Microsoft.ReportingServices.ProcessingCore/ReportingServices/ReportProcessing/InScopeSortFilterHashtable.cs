using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006AE RID: 1710
	[Serializable]
	internal sealed class InScopeSortFilterHashtable : Hashtable
	{
		// Token: 0x06005CA8 RID: 23720 RVA: 0x00179F1C File Offset: 0x0017811C
		internal InScopeSortFilterHashtable()
		{
		}

		// Token: 0x06005CA9 RID: 23721 RVA: 0x00179F24 File Offset: 0x00178124
		internal InScopeSortFilterHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002089 RID: 8329
		internal IntList this[int index]
		{
			get
			{
				return (IntList)base[index];
			}
		}
	}
}
