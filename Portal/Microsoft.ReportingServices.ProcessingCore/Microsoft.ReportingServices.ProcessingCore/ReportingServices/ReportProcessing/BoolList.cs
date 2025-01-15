using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000687 RID: 1671
	[Serializable]
	internal sealed class BoolList : ArrayList
	{
		// Token: 0x06005C16 RID: 23574 RVA: 0x00179381 File Offset: 0x00177581
		internal BoolList()
		{
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x00179389 File Offset: 0x00177589
		internal BoolList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002061 RID: 8289
		internal bool this[int index]
		{
			get
			{
				return (bool)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
