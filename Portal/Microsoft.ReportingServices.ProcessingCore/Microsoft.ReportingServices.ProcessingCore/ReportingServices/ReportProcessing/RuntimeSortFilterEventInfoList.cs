using System;
using System.Collections;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200077C RID: 1916
	internal sealed class RuntimeSortFilterEventInfoList : ArrayList
	{
		// Token: 0x06006AD9 RID: 27353 RVA: 0x001AF6FF File Offset: 0x001AD8FF
		internal RuntimeSortFilterEventInfoList()
		{
		}

		// Token: 0x17002553 RID: 9555
		internal RuntimeSortFilterEventInfo this[int index]
		{
			get
			{
				return (RuntimeSortFilterEventInfo)base[index];
			}
		}
	}
}
