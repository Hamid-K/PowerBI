using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000619 RID: 1561
	[Serializable]
	internal sealed class ParameterDefList : ArrayList
	{
		// Token: 0x060055B8 RID: 21944 RVA: 0x00169B81 File Offset: 0x00167D81
		public ParameterDefList()
		{
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x00169B89 File Offset: 0x00167D89
		internal ParameterDefList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17001F55 RID: 8021
		internal ParameterDef this[int index]
		{
			get
			{
				return (ParameterDef)base[index];
			}
		}
	}
}
