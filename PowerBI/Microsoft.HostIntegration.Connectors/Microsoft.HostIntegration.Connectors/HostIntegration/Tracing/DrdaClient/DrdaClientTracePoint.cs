using System;

namespace Microsoft.HostIntegration.Tracing.DrdaClient
{
	// Token: 0x020006A7 RID: 1703
	public class DrdaClientTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037FF RID: 14335 RVA: 0x000BC09C File Offset: 0x000BA29C
		public DrdaClientTracePoint(DrdaClientTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public DrdaClientTracePoint(DrdaClientTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
