using System;
using System.Collections;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000258 RID: 600
	internal sealed class SQLScheduleSet : ArrayList
	{
		// Token: 0x17000642 RID: 1602
		public SQLScheduleParameters this[int index]
		{
			get
			{
				return base[index] as SQLScheduleParameters;
			}
		}
	}
}
