using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200068C RID: 1676
	[Serializable]
	internal sealed class ParameterValueList : ArrayList
	{
		// Token: 0x06005C2A RID: 23594 RVA: 0x00179490 File Offset: 0x00177690
		internal ParameterValueList()
		{
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x00179498 File Offset: 0x00177698
		internal ParameterValueList(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x17002066 RID: 8294
		internal ParameterValue this[int index]
		{
			get
			{
				return (ParameterValue)base[index];
			}
		}
	}
}
