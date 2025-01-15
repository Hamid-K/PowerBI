using System;

namespace Microsoft.HostIntegration.Tracing.TiHip
{
	// Token: 0x020006C2 RID: 1730
	public class TiHipTracePoint : FlagBasedTracePoint
	{
		// Token: 0x06003834 RID: 14388 RVA: 0x000BC094 File Offset: 0x000BA294
		protected TiHipTracePoint()
		{
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x000BC09C File Offset: 0x000BA29C
		public TiHipTracePoint(TiHipTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public TiHipTracePoint(TiHipTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
