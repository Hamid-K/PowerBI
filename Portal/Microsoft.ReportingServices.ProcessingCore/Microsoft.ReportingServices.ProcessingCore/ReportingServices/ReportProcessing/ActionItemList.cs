using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000698 RID: 1688
	[Serializable]
	internal sealed class ActionItemList : ArrayList
	{
		// Token: 0x06005C52 RID: 23634 RVA: 0x00179725 File Offset: 0x00177925
		internal ActionItemList()
		{
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x0017972D File Offset: 0x0017792D
		internal ActionItemList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002072 RID: 8306
		internal ActionItem this[int index]
		{
			get
			{
				return (ActionItem)base[index];
			}
		}
	}
}
