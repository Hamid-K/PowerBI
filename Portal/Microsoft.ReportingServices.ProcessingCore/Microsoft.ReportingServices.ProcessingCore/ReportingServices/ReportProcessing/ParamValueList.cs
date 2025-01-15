using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200061F RID: 1567
	public sealed class ParamValueList : ArrayList
	{
		// Token: 0x06005645 RID: 22085 RVA: 0x0016BABF File Offset: 0x00169CBF
		internal ParamValueList()
		{
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x0016BAC7 File Offset: 0x00169CC7
		internal ParamValueList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001F8B RID: 8075
		internal ParamValue this[int index]
		{
			get
			{
				return (ParamValue)base[index];
			}
		}
	}
}
