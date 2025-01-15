using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000699 RID: 1689
	[Serializable]
	internal sealed class ActionItemInstanceList : ArrayList
	{
		// Token: 0x06005C55 RID: 23637 RVA: 0x00179744 File Offset: 0x00177944
		internal ActionItemInstanceList()
		{
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x0017974C File Offset: 0x0017794C
		internal ActionItemInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002073 RID: 8307
		internal ActionItemInstance this[int index]
		{
			get
			{
				return (ActionItemInstance)base[index];
			}
		}
	}
}
