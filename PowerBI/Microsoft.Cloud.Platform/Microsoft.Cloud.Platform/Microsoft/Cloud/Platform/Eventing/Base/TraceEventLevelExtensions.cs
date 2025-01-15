using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D5 RID: 981
	[CLSCompliant(false)]
	public static class TraceEventLevelExtensions
	{
		// Token: 0x06001E4A RID: 7754 RVA: 0x000722B6 File Offset: 0x000704B6
		public static EventLevel ToTraceVerbosity(this TraceEventLevel level)
		{
			switch (level)
			{
			case 0:
				return EventLevel.Verbose;
			case 1:
				return EventLevel.Critical;
			case 2:
				return EventLevel.Error;
			case 3:
				return EventLevel.Warning;
			case 4:
				return EventLevel.Informational;
			case 5:
				return EventLevel.Verbose;
			default:
				return EventLevel.Verbose;
			}
		}
	}
}
