using System;
using System.Diagnostics.Tracing;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F5 RID: 501
	[EventSource(Name = "Microsoft.Identity.Client")]
	internal class MsalEventSource : EventSource
	{
		// Token: 0x0600157C RID: 5500 RVA: 0x00047C29 File Offset: 0x00045E29
		[Event(1, Level = EventLevel.Verbose)]
		internal void Verbose(string message)
		{
			base.WriteEvent(1, message);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00047C33 File Offset: 0x00045E33
		[Event(2, Level = EventLevel.Informational)]
		internal void Information(string message)
		{
			base.WriteEvent(2, message);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00047C3D File Offset: 0x00045E3D
		[Event(3, Level = EventLevel.Warning)]
		internal void Warning(string message)
		{
			base.WriteEvent(3, message);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00047C47 File Offset: 0x00045E47
		[Event(4, Level = EventLevel.Error)]
		internal void Error(string message)
		{
			base.WriteEvent(4, message);
		}
	}
}
