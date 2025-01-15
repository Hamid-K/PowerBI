using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A6 RID: 1702
	[Serializable]
	internal sealed class DataSetList : ArrayList
	{
		// Token: 0x06005C87 RID: 23687 RVA: 0x00179A1F File Offset: 0x00177C1F
		internal DataSetList()
		{
		}

		// Token: 0x06005C88 RID: 23688 RVA: 0x00179A27 File Offset: 0x00177C27
		internal DataSetList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002080 RID: 8320
		internal DataSet this[int index]
		{
			get
			{
				return (DataSet)base[index];
			}
		}
	}
}
