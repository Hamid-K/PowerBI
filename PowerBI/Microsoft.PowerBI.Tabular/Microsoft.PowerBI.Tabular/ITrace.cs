using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D5 RID: 213
	public interface ITrace
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000DC7 RID: 3527
		// (remove) Token: 0x06000DC8 RID: 3528
		event TraceEventHandler OnEvent;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000DC9 RID: 3529
		// (remove) Token: 0x06000DCA RID: 3530
		event TraceStoppedEventHandler Stopped;

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DCB RID: 3531
		bool IsStarted { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DCC RID: 3532
		Server Parent { get; }

		// Token: 0x06000DCD RID: 3533
		void Start();

		// Token: 0x06000DCE RID: 3534
		void Stop();
	}
}
