using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000695 RID: 1685
	[Serializable]
	internal sealed class DataFieldList : ArrayList
	{
		// Token: 0x06005C48 RID: 23624 RVA: 0x0017967F File Offset: 0x0017787F
		internal DataFieldList()
		{
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00179687 File Offset: 0x00177887
		internal DataFieldList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x1700206F RID: 8303
		internal Field this[int index]
		{
			get
			{
				return (Field)base[index];
			}
		}
	}
}
