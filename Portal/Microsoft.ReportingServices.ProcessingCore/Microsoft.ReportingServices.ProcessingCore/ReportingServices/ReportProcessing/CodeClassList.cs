using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200068D RID: 1677
	[Serializable]
	internal sealed class CodeClassList : ArrayList
	{
		// Token: 0x06005C2D RID: 23597 RVA: 0x001794AF File Offset: 0x001776AF
		internal CodeClassList()
		{
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x001794B7 File Offset: 0x001776B7
		internal CodeClassList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002067 RID: 8295
		internal CodeClass this[int index]
		{
			get
			{
				return (CodeClass)base[index];
			}
		}
	}
}
