using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000760 RID: 1888
	[Serializable]
	internal sealed class DataValueInstanceList : ArrayList
	{
		// Token: 0x06006860 RID: 26720 RVA: 0x001960E3 File Offset: 0x001942E3
		internal DataValueInstanceList()
		{
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x001960EB File Offset: 0x001942EB
		internal DataValueInstanceList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x170024DE RID: 9438
		internal DataValueInstance this[int index]
		{
			get
			{
				return (DataValueInstance)base[index];
			}
		}
	}
}
