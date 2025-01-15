using System;

namespace Microsoft.HostIntegration.Tracing.TiWip
{
	// Token: 0x020006EB RID: 1771
	public class TiWipTracePoint : FlagBasedTracePoint
	{
		// Token: 0x06003886 RID: 14470 RVA: 0x000BC09C File Offset: 0x000BA29C
		public TiWipTracePoint(TiWipTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public TiWipTracePoint(TiWipTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
