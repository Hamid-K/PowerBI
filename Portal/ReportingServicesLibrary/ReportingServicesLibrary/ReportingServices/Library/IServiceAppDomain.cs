using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200022D RID: 557
	internal interface IServiceAppDomain
	{
		// Token: 0x060013F7 RID: 5111
		void StartService(IServiceAppDomainController controller, bool firstStart);

		// Token: 0x060013F8 RID: 5112
		void EndService(Globals.ServiceStopMode stopMode);

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060013F9 RID: 5113
		bool StillProcessing { get; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060013FA RID: 5114
		bool IsServiceStarted { get; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060013FB RID: 5115
		bool IsServiceWorking { get; }

		// Token: 0x060013FC RID: 5116
		void Trace(TraceLevel level, string message);
	}
}
