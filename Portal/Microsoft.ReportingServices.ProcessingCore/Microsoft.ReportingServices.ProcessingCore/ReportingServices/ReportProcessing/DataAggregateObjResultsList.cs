using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000678 RID: 1656
	internal sealed class DataAggregateObjResultsList : ArrayList
	{
		// Token: 0x17002041 RID: 8257
		internal DataAggregateObjResult[] this[int index]
		{
			get
			{
				return (DataAggregateObjResult[])base[index];
			}
		}
	}
}
