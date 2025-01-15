using System;

namespace Microsoft.HostIntegration.Tracing.ConversionPipeline
{
	// Token: 0x02000693 RID: 1683
	public class ConversionPipelineTracePoint : FlagBasedTracePoint
	{
		// Token: 0x060037D6 RID: 14294 RVA: 0x000BC094 File Offset: 0x000BA294
		protected ConversionPipelineTracePoint()
		{
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000BC09C File Offset: 0x000BA29C
		public ConversionPipelineTracePoint(ConversionPipelineTraceContainer traceContainer, int tracePointIdentifier)
			: base(traceContainer, tracePointIdentifier)
		{
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000BC0A6 File Offset: 0x000BA2A6
		public ConversionPipelineTracePoint(ConversionPipelineTracePoint parentTracePoint, TracePointIdentifiers tracePointIdentifier)
			: base(parentTracePoint, (int)tracePointIdentifier)
		{
		}
	}
}
