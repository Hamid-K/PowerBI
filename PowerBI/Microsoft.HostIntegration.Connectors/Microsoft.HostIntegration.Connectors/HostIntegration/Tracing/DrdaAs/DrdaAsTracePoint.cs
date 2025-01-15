using System;

namespace Microsoft.HostIntegration.Tracing.DrdaAs
{
	// Token: 0x020006AB RID: 1707
	public class DrdaAsTracePoint : FlagBasedTracePoint
	{
		// Token: 0x0600380A RID: 14346 RVA: 0x000BC09C File Offset: 0x000BA29C
		public DrdaAsTracePoint(DrdaAsTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x0600380B RID: 14347 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public DrdaAsTracePoint(DrdaAsTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
