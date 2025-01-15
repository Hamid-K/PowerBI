using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A6 RID: 1702
	public class DrdaCommonTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037FD RID: 14333 RVA: 0x000BC09C File Offset: 0x000BA29C
		public DrdaCommonTracePoint(DrdaClientTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x060037FE RID: 14334 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public DrdaCommonTracePoint(DrdaCommonTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
