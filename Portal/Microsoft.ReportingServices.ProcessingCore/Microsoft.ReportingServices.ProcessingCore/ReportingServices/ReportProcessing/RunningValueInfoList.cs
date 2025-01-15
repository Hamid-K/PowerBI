using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000692 RID: 1682
	[Serializable]
	internal sealed class RunningValueInfoList : ArrayList
	{
		// Token: 0x06005C3F RID: 23615 RVA: 0x00179622 File Offset: 0x00177822
		internal RunningValueInfoList()
		{
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x0017962A File Offset: 0x0017782A
		internal RunningValueInfoList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700206C RID: 8300
		internal RunningValueInfo this[int index]
		{
			get
			{
				return (RunningValueInfo)base[index];
			}
		}
	}
}
