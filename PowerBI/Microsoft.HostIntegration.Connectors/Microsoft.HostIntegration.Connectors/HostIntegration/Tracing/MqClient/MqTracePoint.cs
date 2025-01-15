using System;

namespace Microsoft.HostIntegration.Tracing.MqClient
{
	// Token: 0x02000684 RID: 1668
	public class MqTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037BA RID: 14266 RVA: 0x000BC09C File Offset: 0x000BA29C
		public MqTracePoint(MqTraceContainer traceContainer, TracePointIdentifiers tracePointIdentifier)
			: base(traceContainer, (int)tracePointIdentifier)
		{
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public MqTracePoint(MqTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
