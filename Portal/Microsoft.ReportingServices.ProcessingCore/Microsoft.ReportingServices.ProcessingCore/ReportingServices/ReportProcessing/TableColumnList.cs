using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000690 RID: 1680
	[Serializable]
	internal sealed class TableColumnList : ArrayList
	{
		// Token: 0x06005C36 RID: 23606 RVA: 0x0017950C File Offset: 0x0017770C
		internal TableColumnList()
		{
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x00179514 File Offset: 0x00177714
		internal TableColumnList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700206A RID: 8298
		internal TableColumn this[int index]
		{
			get
			{
				return (TableColumn)base[index];
			}
		}
	}
}
