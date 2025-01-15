using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Etw;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000334 RID: 820
	public static class EventLevelExtensions
	{
		// Token: 0x06001828 RID: 6184 RVA: 0x00058A80 File Offset: 0x00056C80
		public static EtwTraceLevel ToEtwTraceLevel(this EventLevel eventLevel)
		{
			switch (eventLevel)
			{
			case EventLevel.Verbose:
				return EtwTraceLevel.Verbose;
			case EventLevel.Informational:
				return EtwTraceLevel.Info;
			case EventLevel.Warning:
				return EtwTraceLevel.Warning;
			case EventLevel.Error:
				return EtwTraceLevel.Error;
			case EventLevel.Critical:
				return EtwTraceLevel.Critical;
			default:
				return EtwTraceLevel.Unassigned;
			}
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00058A80 File Offset: 0x00056C80
		public static EventLevel ToEventLevel(this EventLevel eventLevel)
		{
			switch (eventLevel)
			{
			case EventLevel.Verbose:
				return 5;
			case EventLevel.Informational:
				return 4;
			case EventLevel.Warning:
				return 3;
			case EventLevel.Error:
				return 2;
			case EventLevel.Critical:
				return 1;
			default:
				return 0;
			}
		}
	}
}
