using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000688 RID: 1672
	[Serializable]
	internal sealed class StringList : ArrayList
	{
		// Token: 0x06005C1A RID: 23578 RVA: 0x001793AF File Offset: 0x001775AF
		internal StringList()
		{
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x001793B7 File Offset: 0x001775B7
		internal StringList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002062 RID: 8290
		internal string this[int index]
		{
			get
			{
				return (string)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
