using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000759 RID: 1881
	[Serializable]
	internal sealed class DataCellsList : ArrayList
	{
		// Token: 0x0600683C RID: 26684 RVA: 0x00195C21 File Offset: 0x00193E21
		internal DataCellsList()
		{
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x00195C29 File Offset: 0x00193E29
		internal DataCellsList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024D6 RID: 9430
		internal DataCellList this[int index]
		{
			get
			{
				return (DataCellList)base[index];
			}
		}
	}
}
