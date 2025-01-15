using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003BD RID: 957
	[Serializable]
	public sealed class InScopeSortFilterHashtable : Hashtable
	{
		// Token: 0x060026CE RID: 9934 RVA: 0x000B9CE6 File Offset: 0x000B7EE6
		public InScopeSortFilterHashtable()
		{
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000B9CEE File Offset: 0x000B7EEE
		internal InScopeSortFilterHashtable(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170013E7 RID: 5095
		internal List<int> this[int index]
		{
			get
			{
				return (List<int>)base[index];
			}
		}
	}
}
