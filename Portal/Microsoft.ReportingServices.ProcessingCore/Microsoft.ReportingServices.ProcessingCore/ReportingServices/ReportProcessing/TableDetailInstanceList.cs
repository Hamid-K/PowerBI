using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B5 RID: 1717
	[Serializable]
	internal sealed class TableDetailInstanceList : ArrayList
	{
		// Token: 0x06005CC0 RID: 23744 RVA: 0x0017A29E File Offset: 0x0017849E
		internal TableDetailInstanceList()
		{
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x0017A2A6 File Offset: 0x001784A6
		internal TableDetailInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002090 RID: 8336
		internal TableDetailInstance this[int index]
		{
			get
			{
				return (TableDetailInstance)base[index];
			}
		}
	}
}
