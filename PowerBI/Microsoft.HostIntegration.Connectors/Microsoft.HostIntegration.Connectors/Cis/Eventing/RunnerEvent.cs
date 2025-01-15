using System;
using System.Diagnostics;
using Microsoft.Cis.Eventing.Listeners;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x0200049A RID: 1178
	[RDEvent(64260, TraceEventType.Information)]
	public class RunnerEvent : RDEventBase
	{
		// Token: 0x060028E6 RID: 10470 RVA: 0x0007CC36 File Offset: 0x0007AE36
		static RunnerEvent()
		{
			RunnerEvent.source.Listeners.Add(RunnerEvent.listener);
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0007CC6C File Offset: 0x0007AE6C
		public RunnerEvent()
		{
			this.UserField1 = "";
			this.UserField2 = "";
			this.UserField3 = "";
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x0007CC95 File Offset: 0x0007AE95
		// (set) Token: 0x060028E9 RID: 10473 RVA: 0x0007CC9D File Offset: 0x0007AE9D
		[RDEventProperty]
		public string Name { get; set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x0007CCA6 File Offset: 0x0007AEA6
		// (set) Token: 0x060028EB RID: 10475 RVA: 0x0007CCAE File Offset: 0x0007AEAE
		[RDEventProperty]
		public string InstanceName { get; set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x0007CCB7 File Offset: 0x0007AEB7
		// (set) Token: 0x060028ED RID: 10477 RVA: 0x0007CCBF File Offset: 0x0007AEBF
		[RDEventProperty]
		public RunnerStatus Status { get; set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x060028EE RID: 10478 RVA: 0x0007CCC8 File Offset: 0x0007AEC8
		// (set) Token: 0x060028EF RID: 10479 RVA: 0x0007CCD0 File Offset: 0x0007AED0
		[RDEventProperty]
		public string UserField1 { get; set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060028F0 RID: 10480 RVA: 0x0007CCD9 File Offset: 0x0007AED9
		// (set) Token: 0x060028F1 RID: 10481 RVA: 0x0007CCE1 File Offset: 0x0007AEE1
		[RDEventProperty]
		public string UserField2 { get; set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x0007CCEA File Offset: 0x0007AEEA
		// (set) Token: 0x060028F3 RID: 10483 RVA: 0x0007CCF2 File Offset: 0x0007AEF2
		[RDEventProperty]
		public string UserField3 { get; set; }

		// Token: 0x060028F4 RID: 10484 RVA: 0x0007CCFB File Offset: 0x0007AEFB
		public void Trace()
		{
			if (this.Name == null || this.InstanceName == null || this.Status == (RunnerStatus)0)
			{
				throw new ArgumentException("Must set the Name, InstanceName and Status properties before calling Trace");
			}
			base.TraceTo(RunnerEvent.source);
		}

		// Token: 0x0400180F RID: 6159
		private static TraceSource source = new TraceSource("Runners", SourceLevels.All);

		// Token: 0x04001810 RID: 6160
		private static RDEventMonitoringAgentListener listener = new RDEventMonitoringAgentListener(AnalyticsWellKnownProviderGuid.RunnerProvider);
	}
}
