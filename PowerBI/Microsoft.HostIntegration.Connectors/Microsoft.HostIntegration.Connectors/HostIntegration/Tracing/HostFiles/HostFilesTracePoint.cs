using System;

namespace Microsoft.HostIntegration.Tracing.HostFiles
{
	// Token: 0x02000699 RID: 1689
	public class HostFilesTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037E5 RID: 14309 RVA: 0x000BC094 File Offset: 0x000BA294
		protected HostFilesTracePoint()
		{
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000BC09C File Offset: 0x000BA29C
		public HostFilesTracePoint(HostFilesTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public HostFilesTracePoint(HostFilesTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
