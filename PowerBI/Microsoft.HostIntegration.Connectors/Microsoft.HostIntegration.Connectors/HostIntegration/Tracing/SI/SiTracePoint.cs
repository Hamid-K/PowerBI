using System;

namespace Microsoft.HostIntegration.Tracing.SI
{
	// Token: 0x020006DE RID: 1758
	public class SiTracePoint : FlagBasedTracePoint
	{
		// Token: 0x06003870 RID: 14448 RVA: 0x000BC09C File Offset: 0x000BA29C
		public SiTracePoint(SiTraceContainer traceContainer, TracePointIdentifiers tracePointIdentifier)
			: base(traceContainer, (int)tracePointIdentifier)
		{
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public SiTracePoint(SiTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
