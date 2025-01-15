using System;

namespace Microsoft.HostIntegration.Tracing.ConversionPipeline
{
	// Token: 0x02000695 RID: 1685
	public class PipelineDummyTracePoint : PipelineTracePoint
	{
		// Token: 0x17000C59 RID: 3161
		public override object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
			}
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool IsEnabled(TraceFlags traceFlags)
		{
			return false;
		}
	}
}
