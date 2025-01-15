using System;
using System.Collections;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001D RID: 29
	internal sealed class SQLScheduleSet : ArrayList
	{
		// Token: 0x17000043 RID: 67
		public SQLScheduleParameters this[int index]
		{
			get
			{
				return base[index] as SQLScheduleParameters;
			}
		}
	}
}
