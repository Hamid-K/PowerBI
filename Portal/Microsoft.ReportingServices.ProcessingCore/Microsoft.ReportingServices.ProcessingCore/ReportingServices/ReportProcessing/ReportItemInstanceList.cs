using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006B0 RID: 1712
	[Serializable]
	internal sealed class ReportItemInstanceList : ArrayList
	{
		// Token: 0x06005CAE RID: 23726 RVA: 0x00179F5F File Offset: 0x0017815F
		internal ReportItemInstanceList()
		{
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00179F67 File Offset: 0x00178167
		internal ReportItemInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700208B RID: 8331
		internal ReportItemInstance this[int index]
		{
			get
			{
				return (ReportItemInstance)base[index];
			}
		}
	}
}
