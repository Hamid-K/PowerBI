using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000616 RID: 1558
	[Serializable]
	public sealed class ValidValueList : ArrayList
	{
		// Token: 0x06005577 RID: 21879 RVA: 0x0016882B File Offset: 0x00166A2B
		public ValidValueList()
		{
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x00168833 File Offset: 0x00166A33
		public ValidValueList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001F43 RID: 8003
		public ValidValue this[int index]
		{
			get
			{
				return (ValidValue)base[index];
			}
		}
	}
}
