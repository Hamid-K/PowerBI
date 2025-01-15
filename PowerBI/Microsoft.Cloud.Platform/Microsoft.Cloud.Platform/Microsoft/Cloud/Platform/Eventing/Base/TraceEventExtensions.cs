using System;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003D6 RID: 982
	[CLSCompliant(false)]
	public static class TraceEventExtensions
	{
		// Token: 0x06001E4B RID: 7755 RVA: 0x000722E5 File Offset: 0x000704E5
		public static bool IsManifestEvent(this TraceEvent traceEvent)
		{
			return traceEvent.EventName.Equals("ManifestData", StringComparison.Ordinal);
		}

		// Token: 0x04000A66 RID: 2662
		private const string c_manifestDataEventName = "ManifestData";
	}
}
