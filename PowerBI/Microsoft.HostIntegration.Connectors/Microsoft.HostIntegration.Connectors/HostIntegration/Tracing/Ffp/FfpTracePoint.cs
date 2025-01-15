using System;

namespace Microsoft.HostIntegration.Tracing.Ffp
{
	// Token: 0x0200067E RID: 1662
	public class FfpTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037AF RID: 14255 RVA: 0x000BC094 File Offset: 0x000BA294
		protected FfpTracePoint()
		{
		}

		// Token: 0x060037B0 RID: 14256 RVA: 0x000BC09C File Offset: 0x000BA29C
		public FfpTracePoint(FlatFileProcessorTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x060037B1 RID: 14257 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public FfpTracePoint(FfpTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
