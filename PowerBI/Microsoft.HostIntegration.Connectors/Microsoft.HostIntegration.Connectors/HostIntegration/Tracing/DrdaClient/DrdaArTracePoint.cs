using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x0200069E RID: 1694
	public class DrdaArTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037F4 RID: 14324 RVA: 0x000BC09C File Offset: 0x000BA29C
		public DrdaArTracePoint(DrdaClientTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public DrdaArTracePoint(DrdaArTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
