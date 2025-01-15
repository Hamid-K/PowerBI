using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000140 RID: 320
	public class MonitoredActivitySequencerResult
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x0001C25B File Offset: 0x0001A45B
		public MonitoredActivitySequencerResult(IMonitoredError error, ActivityResult result)
		{
			ExtendedDiagnostics.EnsureOperation(result == ActivityResult.Success || (result == ActivityResult.Failure && error != null), "An activity ending with failure must provide an error which caused the failure");
			this.Error = error;
			this.Result = result;
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0001C28C File Offset: 0x0001A48C
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001C294 File Offset: 0x0001A494
		public IMonitoredError Error { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001C29D File Offset: 0x0001A49D
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0001C2A5 File Offset: 0x0001A4A5
		public ActivityResult Result { get; private set; }
	}
}
