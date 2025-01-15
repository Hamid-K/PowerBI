using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000679 RID: 1657
	internal sealed class DataAggregateObjList : ArrayList
	{
		// Token: 0x17002042 RID: 8258
		internal DataAggregateObj this[int index]
		{
			get
			{
				return (DataAggregateObj)base[index];
			}
		}
	}
}
