using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B4 RID: 1716
	[Serializable]
	internal sealed class TableGroupInstanceList : ArrayList
	{
		// Token: 0x06005CBD RID: 23741 RVA: 0x0017A27F File Offset: 0x0017847F
		internal TableGroupInstanceList()
		{
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x0017A287 File Offset: 0x00178487
		internal TableGroupInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700208F RID: 8335
		internal TableGroupInstance this[int index]
		{
			get
			{
				return (TableGroupInstance)base[index];
			}
		}
	}
}
