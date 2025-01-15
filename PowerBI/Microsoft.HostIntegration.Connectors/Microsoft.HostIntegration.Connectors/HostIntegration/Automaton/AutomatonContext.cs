using System;
using Microsoft.HostIntegration.EventLogging;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.Automaton
{
	// Token: 0x020004C1 RID: 1217
	public abstract class AutomatonContext
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x0007D1C8 File Offset: 0x0007B3C8
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x0007D1D0 File Offset: 0x0007B3D0
		public StateAsCodeDriver[] CurrentState { get; set; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x0007D1D9 File Offset: 0x0007B3D9
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x0007D1E1 File Offset: 0x0007B3E1
		public FlagBasedTracePoint AutomatonTracePoint { get; set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x0007D1EA File Offset: 0x0007B3EA
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x0007D1F2 File Offset: 0x0007B3F2
		public EventLogContainer AutomatonEventLog { get; set; }
	}
}
