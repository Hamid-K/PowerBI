using System;

namespace Microsoft.HostIntegration.Tracing.ConversionPipeline
{
	// Token: 0x02000694 RID: 1684
	public class PipelineTracePoint : ConversionPipelineTracePoint
	{
		// Token: 0x060037D9 RID: 14297 RVA: 0x000BC613 File Offset: 0x000BA813
		protected PipelineTracePoint()
		{
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000BC61B File Offset: 0x000BA81B
		public PipelineTracePoint(ConversionPipelineTraceContainer traceContainer)
			: base(traceContainer, 0)
		{
		}

		// Token: 0x17000C58 RID: 3160
		public virtual object this[TracePointPropertyIdentifiers propertyIdentifier]
		{
			set
			{
				base[(int)propertyIdentifier] = value;
			}
		}
	}
}
