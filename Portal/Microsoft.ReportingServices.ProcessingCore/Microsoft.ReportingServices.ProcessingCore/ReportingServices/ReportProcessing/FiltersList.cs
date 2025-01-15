using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000680 RID: 1664
	internal sealed class FiltersList : ArrayList
	{
		// Token: 0x17002050 RID: 8272
		internal Filters this[int index]
		{
			get
			{
				return (Filters)base[index];
			}
		}
	}
}
